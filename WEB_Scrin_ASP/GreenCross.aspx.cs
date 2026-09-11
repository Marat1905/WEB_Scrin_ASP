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

        // Данные
        private List<InjuryDto> InjuriesMonth { get; set; } = new List<InjuryDto>();
        private List<InjuryDto> InjuriesYear { get; set; } = new List<InjuryDto>();

        // Статистика (теперь приходит с бэкенда, а не рассчитывается локально)
        public int MonthSignificantCount { get; set; }
        public int YearSignificantCount { get; set; }
        public int DaysWithoutInjury { get; set; }
        public string LastSignificantDateStr { get; set; } = "";

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
                    // НОВЫЙ БАЗОВЫЙ АДРЕС API (в соответствии с SafetyInjuryRegistry)
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

                    // Запрос статистики (теперь расчет выполняется на бэкенде)
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
                }
            }
            catch (Exception ex)
            {
                // Логирование (в реальном проекте заменить на нормальный лог)
                System.Diagnostics.Debug.WriteLine(ex);
            }

            // Генерация HTML для визуализации
            GenerateVisual();
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
            // Используем класс CellCoord вместо ValueTuple для совместимости со старыми версиями .NET Framework
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

            // Особое размещение для месяцев с 31 днём (30-й и 31-й дни), как в React-проекте
            var cellValues = new int?[33];
            if (daysInMonth == 31)
            {
                for (int i = 0; i < 30; i++) cellValues[i] = i + 1;
                cellValues[31] = 31; // 31-й день в предпоследнюю ячейку
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
                                // Используем InjuryCategoryHelper для определения значимости категории
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

                            // onclick добавляется ТОЛЬКО если есть травма
                            // Для безопасных дней (без травм) клик ничего не делает
                            string onClick = "";
                            if (injury != null)
                            {
                                string dateStr = cellDate.ToString("d MMMM", ru);
                                string encodedDate = HttpUtility.JavaScriptStringEncode(dateStr);
                                string encodedType = HttpUtility.JavaScriptStringEncode(injury.Type);
                                string encodedDesc = HttpUtility.JavaScriptStringEncode(injury.Description);
                                // ИЗМЕНЕНО: передаём в модальное окно человекочитаемую метку категории
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
                int offset = ((int)firstDay.DayOfWeek - 1 + 7) % 7; // понедельник = 0

                sb.AppendLine("<div class='month-card'>");
                sb.AppendLine($"<div class='month-name'>{firstDay.ToString("MMMM", ru)}</div>");
                sb.AppendLine("<div class='month-weekdays'><span>Пн</span><span>Вт</span><span>Ср</span><span>Чт</span><span>Пт</span><span>Сб</span><span>Вс</span></div>");
                sb.AppendLine("<div class='month-days'>");

                // Пустые ячейки до первого дня
                for (int i = 0; i < offset; i++)
                    sb.AppendLine("<div class='month-day' style='background:transparent;'></div>");

                // Дни месяца
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
                        // Используем InjuryCategoryHelper для определения значимости категории
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

                    // onclick добавляется ТОЛЬКО если есть травма
                    // Для безопасных дней (без травм) клик ничего не делает
                    string onClick = "";
                    if (injury != null)
                    {
                        string dateStr = cellDate.ToString("d MMMM", ru);
                        string encodedDate = HttpUtility.JavaScriptStringEncode(dateStr);
                        string encodedType = HttpUtility.JavaScriptStringEncode(injury.Type);
                        string encodedDesc = HttpUtility.JavaScriptStringEncode(injury.Description);
                        // ИЗМЕНЕНО: передаём в модальное окно человекочитаемую метку категории
                        string categoryLabel = InjuryCategoryHelper.GetLabel(injury.Category);
                        string encodedCategory = HttpUtility.JavaScriptStringEncode(categoryLabel);
                        onClick = $" onclick='showInfo(event, \"{encodedDate}\", true, \"{encodedCategory}\", \"{encodedType}\", \"{encodedDesc}\")'";
                    }

                    sb.AppendLine($"<div class='{cssClass}'{onClick}>{d}</div>");
                }

                // Остаток до конца сетки
                int totalCells = offset + daysInMonth;
                int remainder = (7 - (totalCells % 7)) % 7;
                for (int i = 0; i < remainder; i++)
                    sb.AppendLine("<div class='month-day' style='background:transparent;'></div>");

                sb.AppendLine("</div></div>"); // закрываем month-days и month-card
            }

            sb.AppendLine("</div>");
            return sb.ToString();
        }

        // Добавлен обработчик Page_LoadComplete для обновления статусной строки
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            // Обновляем статусную строку через базовый метод
            UpdateStatusTextBox(textboxStatus);
        }
    }

    // Вспомогательный класс для замены ValueTuple в старых версиях .NET Framework (исправление ошибки CS8179)
    public class CellCoord
    {
        public int Row { get; set; }
        public int Col { get; set; }
    }
}