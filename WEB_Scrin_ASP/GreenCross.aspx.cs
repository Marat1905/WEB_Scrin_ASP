using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace WEB_Scrin_ASP
{
    public partial class GreenCross : BasePage
    {
        // Текущая дата (год/месяц)
        public DateTime CurrentDate { get; set; }
        public string ViewMode { get; set; } = "cross"; // cross / year

        // Данные о травмах
        private List<InjuryDto> InjuriesMonth { get; set; } = new List<InjuryDto>();
        private List<InjuryDto> InjuriesYear { get; set; } = new List<InjuryDto>();

        // Статистика (приходит с бэкенда)
        public int MonthSignificantCount { get; set; }
        public int YearSignificantCount { get; set; }
        public int DaysWithoutInjury { get; set; }
        public string LastSignificantDateStr { get; set; } = "";

        // Данные для программы «Ноль травм»
        public ZeroInjuryStatusDto ZeroInjuryStatus { get; set; }
        public List<ZeroInjuryProgramDto> ZeroInjuryHistory { get; set; } = new List<ZeroInjuryProgramDto>();

        // Для навигации
        public int PrevYear, PrevMonth, NextYear, NextMonth;
        public string CurrentDisplay;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Загружаем статус обрыва из базы (метод BasePage)
            LoadBreakStatus();

            // Определяем режим и дату из QueryString
            string mode = Request.QueryString["mode"];
            if (!string.IsNullOrEmpty(mode) && (mode == "cross" || mode == "year"))
                ViewMode = mode;

            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;

            if (!string.IsNullOrEmpty(Request.QueryString["year"]))
                int.TryParse(Request.QueryString["year"], out year);

            if (!string.IsNullOrEmpty(Request.QueryString["month"]) && ViewMode == "cross")
                int.TryParse(Request.QueryString["month"], out month);

            CurrentDate = new DateTime(year, month, 1);

            // Вычисляем предыдущий/следующий месяц/год для ссылок
            if (ViewMode == "cross")
            {
                var prev = CurrentDate.AddMonths(-1);
                PrevYear = prev.Year; PrevMonth = prev.Month;
                var next = CurrentDate.AddMonths(1);
                NextYear = next.Year; NextMonth = next.Month;
                CurrentDisplay = CurrentDate.ToString("MMMM yyyy", new System.Globalization.CultureInfo("ru-RU"));
            }
            else
            {
                PrevYear = year - 1; PrevMonth = 1;
                NextYear = year + 1; NextMonth = 1;
                CurrentDisplay = year.ToString();
            }

            // Загружаем данные асинхронно
            Page.RegisterAsyncTask(new PageAsyncTask(LoadDataAsync));
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Базовый адрес API
                    client.BaseAddress = new Uri("http://10.21.2.95:30006/safety/api/v1/");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    // Запрос за месяц
                    string monthUrl = $"injuries?year={CurrentDate.Year}&month={CurrentDate.Month}";
                    var responseMonth = await client.GetAsync(monthUrl);
                    if (responseMonth.IsSuccessStatusCode)
                    {
                        var jsonMonth = await responseMonth.Content.ReadAsStringAsync();
                        InjuriesMonth = JsonConvert.DeserializeObject<List<InjuryDto>>(jsonMonth) ?? new List<InjuryDto>();
                    }

                    // Запрос за год
                    string yearUrl = $"injuries/year/{CurrentDate.Year}";
                    var responseYear = await client.GetAsync(yearUrl);
                    if (responseYear.IsSuccessStatusCode)
                    {
                        var jsonYear = await responseYear.Content.ReadAsStringAsync();
                        InjuriesYear = JsonConvert.DeserializeObject<List<InjuryDto>>(jsonYear) ?? new List<InjuryDto>();
                    }

                    // Запрос статистики (расчет выполняется на бэкенде)
                    string statsUrl = $"injuries/statistics?year={CurrentDate.Year}&month={CurrentDate.Month}";
                    var responseStats = await client.GetAsync(statsUrl);
                    if (responseStats.IsSuccessStatusCode)
                    {
                        var jsonStats = await responseStats.Content.ReadAsStringAsync();
                        var stats = JsonConvert.DeserializeObject<InjuryStatisticsDto>(jsonStats);
                        if (stats != null)
                        {
                            MonthSignificantCount = stats.MonthSignificantCount;
                            YearSignificantCount = stats.YearSignificantCount;
                            DaysWithoutInjury = stats.DaysWithoutInjury;
                            LastSignificantDateStr = stats.LastSignificantDate.HasValue
                                ? stats.LastSignificantDate.Value.ToString("dd.MM.yyyy")
                                : "";
                        }
                    }

                    // Запрос статуса программы «Ноль травм»
                    string zeroUrl = "zero-injury/status";
                    var responseZero = await client.GetAsync(zeroUrl);
                    if (responseZero.IsSuccessStatusCode)
                    {
                        var jsonZero = await responseZero.Content.ReadAsStringAsync();
                        ZeroInjuryStatus = JsonConvert.DeserializeObject<ZeroInjuryStatusDto>(jsonZero);
                    }

                    // Запрос истории программ «Ноль травм»
                    string zeroHistoryUrl = "zero-injury/history";
                    var responseZeroHistory = await client.GetAsync(zeroHistoryUrl);
                    if (responseZeroHistory.IsSuccessStatusCode)
                    {
                        var jsonZeroHistory = await responseZeroHistory.Content.ReadAsStringAsync();
                        ZeroInjuryHistory = JsonConvert.DeserializeObject<List<ZeroInjuryProgramDto>>(jsonZeroHistory) ?? new List<ZeroInjuryProgramDto>();
                    }
                }
            }
            catch (Exception ex)
            {
                // Логирование (в реальном проекте заменить на нормальный лог)
                System.Diagnostics.Debug.WriteLine(ex);
            }

            // Генерация HTML для визуализации календаря
            GenerateVisual();

            // Генерация HTML для модального окна "Ноль травм"
            BuildZeroInjuryModalContent();
        }

        /// <summary>
        /// Собирает HTML-разметку для отображения внутри модального окна "Ноль травм".
        /// </summary>
        private void BuildZeroInjuryModalContent()
        {
            if (ZeroInjuryStatus == null) return;

            var sb = new StringBuilder();

            // 1. Блок текущей / последней программы
            var prog = ZeroInjuryStatus.CurrentProgram ?? ZeroInjuryStatus.LastFinalizedProgram;
            if (prog != null)
            {
                sb.Append("<div class='modal-section' style='margin-bottom: 3dvh;'>");

                // Большой счетчик дней
                sb.AppendFormat("<div style='font-size: 3dvh; font-weight: bold; color: #f8fafc; margin-bottom: 1dvh;'>{0} <span style='font-size: 2dvh; color: #94a3b8; font-weight: normal;'>/ {1} дн.</span></div>", prog.DaysPassed, prog.TargetDays);

                // Прогресс-бар
                int percent = prog.TargetDays > 0 ? (int)((prog.DaysPassed * 100.0) / prog.TargetDays) : 0;
                if (percent > 100) percent = 100;
                sb.Append("<div style='width: 100%; height: 2dvh; background: #334155; border-radius: 10px; overflow: hidden; margin: 1.5dvh 0;'>");
                sb.AppendFormat("<div style='height: 100%; background: #10b981; width: {0}%;'></div>", percent);
                sb.Append("</div>");

                // Статус
                string statusText = prog.Status == "Completed" ? "Успешно завершена" : (prog.Status == "Interrupted" ? "Прервана травмой" : "Активна");
                string statusColor = prog.Status == "Completed" ? "#10b981" : (prog.Status == "Interrupted" ? "#ef4444" : "#3b82f6");

                sb.Append("<div style='display: flex; flex-direction: column; gap: 1dvh; margin-top: 2dvh; font-size: 1.8dvh;'>");
                sb.AppendFormat("<div style='display: flex; justify-content: space-between;'><span>Статус:</span><span style='color: {0}; font-weight: bold;'>{1}</span></div>", statusColor, statusText);
                sb.AppendFormat("<div style='display: flex; justify-content: space-between;'><span>Дата начала:</span><span>{0:dd.MM.yyyy}</span></div>", prog.StartDate);

                if (prog.CompletedAt.HasValue)
                    sb.AppendFormat("<div style='display: flex; justify-content: space-between;'><span>Дата завершения:</span><span>{0:dd.MM.yyyy}</span></div>", prog.CompletedAt.Value);

                sb.AppendFormat("<div style='display: flex; justify-content: space-between;'><span>Описание:</span><span>{0}</span></div>", prog.Description);


                sb.Append("</div></div>");
            }

            // 2. Блок истории
            if (ZeroInjuryHistory != null && ZeroInjuryHistory.Count > 0)
            {
                sb.Append("<div style='border-top: 1px solid #475569; padding-top: 2dvh;'>");
                sb.Append("<h4 style='color: #fbbf24; font-size: 2.2dvh; margin-bottom: 1.5dvh;'>История периодов</h4>");
                sb.Append("<table class='history-table'>");
                sb.Append("<thead><tr><th>Начало</th><th>Завершение</th><th>Статус</th><th>Дней</th><th>Описание</th></tr></thead><tbody>");

                foreach (var h in ZeroInjuryHistory)
                {
                    string stText = h.Status == "Completed" ? "Успешно" : (h.Status == "Interrupted" ? "Прервана" : "Активна");
                    string stClass = h.Status == "Completed" ? "badge-success" : (h.Status == "Interrupted" ? "badge-interrupted" : "badge-active");

                    sb.AppendFormat("<tr><td>{0:dd.MM.yyyy}</td><td>{1}</td><td><span class='badge {2}'>{3}</span></td><td>{4}</td><td>{5}</td></tr>",
                        h.StartDate,
                        h.CompletedAt.HasValue ? h.CompletedAt.Value.ToString("dd.MM.yyyy") : "—",
                        stClass, stText, h.DaysPassed, h.Description);
                }
                sb.Append("</tbody></table></div>");
            }

            litZeroModalContent.Text = sb.ToString();
        }

        private void GenerateVisual()
        {
            if (ViewMode == "cross")
                litVisual.Text = BuildCrossHtml();
            else
                litVisual.Text = BuildYearHtml();
        }

        private string BuildCrossHtml()
        {
            int year = CurrentDate.Year;
            int month = CurrentDate.Month;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            var ru = new System.Globalization.CultureInfo("ru-RU");

            // Координаты клеток, образующих крест (7x7, где центральные строки и столбцы)
            var crossCells = new List<CellCoord>();
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if ((row >= 2 && row <= 4) || (col >= 2 && col <= 4))
                    {
                        crossCells.Add(new CellCoord { Row = row, Col = col });
                    }
                }
            }
            crossCells.Sort((a, b) => (a.Row == b.Row ? a.Col - b.Col : a.Row - b.Row));

            // Особое размещение для месяцев с 31 днём
            var cellValues = new int?[33];
            if (daysInMonth == 31)
            {
                for (int i = 0; i < 30; i++) cellValues[i] = i + 1;
                cellValues[31] = 31;
            }
            else
            {
                for (int i = 0; i < daysInMonth; i++) cellValues[i] = i + 1;
            }

            var sb = new StringBuilder();
            sb.AppendLine("<div class='cross-grid'>");

            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    var cellIndex = crossCells.FindIndex(c => c.Row == row && c.Col == col);
                    if (cellIndex != -1)
                    {
                        int? dayNumber = cellValues[cellIndex];
                        if (dayNumber.HasValue)
                        {
                            DateTime cellDate = new DateTime(year, month, dayNumber.Value);
                            var injury = InjuriesMonth.FirstOrDefault(inj =>
                                DateTime.Parse(inj.Date, System.Globalization.CultureInfo.InvariantCulture).Date == cellDate.Date);

                            bool isFuture = cellDate.Date > DateTime.Today.Date;
                            bool isToday = cellDate.Date == DateTime.Today.Date;

                            string cssClass = "cell";
                            if (isFuture)
                            {
                                cssClass += " future";
                            }
                            else if (injury != null)
                            {
                                if (InjuryCategoryHelper.IsSignificant(injury.Category))
                                    cssClass += " injury-significant";
                                else
                                    cssClass += " injury-minor";
                            }
                            else
                            {
                                cssClass += " safe";
                            }

                            if (isToday)
                                cssClass += " today";

                            string onClick = "";
                            if (injury != null)
                            {
                                string dateStr = cellDate.ToString("d MMMM", ru);
                                string encodedDate = HttpUtility.JavaScriptStringEncode(dateStr);
                                string encodedType = HttpUtility.JavaScriptStringEncode(injury.Type);
                                string encodedDesc = HttpUtility.JavaScriptStringEncode(injury.Description);
                                string categoryLabel = InjuryCategoryHelper.GetLabel(injury.Category);
                                string encodedCategory = HttpUtility.JavaScriptStringEncode(categoryLabel);
                                onClick = $" onclick='showInfo(event, \"{encodedDate}\", true, \"{encodedCategory}\", \"{encodedType}\", \"{encodedDesc}\")'";
                            }

                            sb.AppendLine($"<div class='{cssClass}'{onClick}>{dayNumber.Value}</div>");
                        }
                        else
                        {
                            sb.AppendLine("<div class='cell empty'></div>");
                        }
                    }
                    else
                    {
                        sb.AppendLine("<div class='cell out'></div>");
                    }
                }
            }
            sb.AppendLine("</div>");
            return sb.ToString();
        }

        private string BuildYearHtml()
        {
            int year = CurrentDate.Year;
            var ru = new System.Globalization.CultureInfo("ru-RU");
            var sb = new StringBuilder();
            sb.AppendLine("<div class='year-grid'>");

            for (int month = 1; month <= 12; month++)
            {
                DateTime firstDay = new DateTime(year, month, 1);
                int daysInMonth = DateTime.DaysInMonth(year, month);
                int offset = ((int)firstDay.DayOfWeek - 1 + 7) % 7;

                sb.AppendLine("<div class='month-card'>");
                sb.AppendLine($"<div class='month-name'>{firstDay.ToString("MMMM", ru)}</div>");
                sb.AppendLine("<div class='month-weekdays'><span>Пн</span><span>Вт</span><span>Ср</span><span>Чт</span><span>Пт</span><span>Сб</span><span>Вс</span></div>");
                sb.AppendLine("<div class='month-days'>");

                for (int i = 0; i < offset; i++)
                    sb.AppendLine("<div class='month-day' style='background:transparent;'></div>");

                for (int d = 1; d <= daysInMonth; d++)
                {
                    DateTime cellDate = new DateTime(year, month, d);
                    var injury = InjuriesYear.FirstOrDefault(inj =>
                        DateTime.Parse(inj.Date, System.Globalization.CultureInfo.InvariantCulture).Date == cellDate.Date);

                    bool isFuture = cellDate.Date > DateTime.Today.Date;
                    bool isToday = cellDate.Date == DateTime.Today.Date;

                    string cssClass = "month-day";
                    if (isFuture)
                    {
                        cssClass += " future";
                    }
                    else if (injury != null)
                    {
                        if (InjuryCategoryHelper.IsSignificant(injury.Category))
                            cssClass += " injury-significant";
                        else
                            cssClass += " injury-minor";
                    }
                    else
                    {
                        cssClass += " safe";
                    }

                    if (isToday)
                        cssClass += " today";

                    string onClick = "";
                    if (injury != null)
                    {
                        string dateStr = cellDate.ToString("d MMMM", ru);
                        string encodedDate = HttpUtility.JavaScriptStringEncode(dateStr);
                        string encodedType = HttpUtility.JavaScriptStringEncode(injury.Type);
                        string encodedDesc = HttpUtility.JavaScriptStringEncode(injury.Description);
                        string categoryLabel = InjuryCategoryHelper.GetLabel(injury.Category);
                        string encodedCategory = HttpUtility.JavaScriptStringEncode(categoryLabel);
                        onClick = $" onclick='showInfo(event, \"{encodedDate}\", true, \"{encodedCategory}\", \"{encodedType}\", \"{encodedDesc}\")'";
                    }

                    sb.AppendLine($"<div class='{cssClass}'{onClick}>{d}</div>");
                }

                int totalCells = offset + daysInMonth;
                int remainder = (7 - (totalCells % 7)) % 7;
                for (int i = 0; i < remainder; i++)
                    sb.AppendLine("<div class='month-day' style='background:transparent;'></div>");

                sb.AppendLine("</div></div>");
            }

            sb.AppendLine("</div>");
            return sb.ToString();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            // Обновляем статусную строку (из BasePage)
            UpdateStatusTextBox(textboxStatus);
        }
    }

    // Вспомогательный класс для замены ValueTuple в старых версиях .NET Framework
    public class CellCoord
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}