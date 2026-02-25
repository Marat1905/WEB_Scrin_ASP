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

        // Статистика
        public int MonthInjuriesCount => InjuriesMonth?.Count ?? 0;
        public int YearInjuriesCount => InjuriesYear?.Count ?? 0;
        public int DaysWithoutInjury { get; set; }
        public string LastInjuryDateStr { get; set; } = "";

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
                    client.BaseAddress = new Uri("http://10.21.2.95:30006/api/");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    // Запрос за месяц
                    string monthUrl = $"safety/injuries?year={CurrentDate.Year}&month={CurrentDate.Month}";
                    var responseMonth = await client.GetAsync(monthUrl);
                    if (responseMonth.IsSuccessStatusCode)
                    {
                        var jsonMonth = await responseMonth.Content.ReadAsStringAsync();
                        InjuriesMonth = JsonConvert.DeserializeObject<List<InjuryDto>>(jsonMonth) ?? new List<InjuryDto>();
                    }

                    // Запрос за год
                    string yearUrl = $"safety/injuries/year/{CurrentDate.Year}";
                    var responseYear = await client.GetAsync(yearUrl);
                    if (responseYear.IsSuccessStatusCode)
                    {
                        var jsonYear = await responseYear.Content.ReadAsStringAsync();
                        InjuriesYear = JsonConvert.DeserializeObject<List<InjuryDto>>(jsonYear) ?? new List<InjuryDto>();
                    }

                    // Вычисляем статистику
                    CalculateStats();
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

        private void CalculateStats()
        {
            int year = CurrentDate.Year;
            DateTime today = DateTime.Today;
            DateTime endDate;

            if (year == today.Year)
            {
                // Не включаем сегодняшний день – берём вчера
                endDate = today.AddDays(-1);
            }
            else if (year < today.Year)
            {
                // Прошедший год – последний день года
                endDate = new DateTime(year, 12, 31);
            }
            else
            {
                // Будущий год (не должен встречаться в статистике) – для безопасности берём конец года
                endDate = new DateTime(year, 12, 31);
            }

            if (InjuriesYear == null || InjuriesYear.Count == 0)
            {
                // Нет травм – считаем полные дни с начала года до endDate включительно
                DateTime startOfYear = new DateTime(year, 1, 1);
                if (endDate < startOfYear)
                {
                    // Например, сегодня 1 января, тогда endDate – 31 декабря прошлого года
                    DaysWithoutInjury = 0;
                }
                else
                {
                    DaysWithoutInjury = (int)(endDate - startOfYear).TotalDays + 1;
                }
                LastInjuryDateStr = "";
            }
            else
            {
                // Последняя травма в выбранном году
                var lastInjury = InjuriesYear
                    .Select(i => DateTime.Parse(i.Date).Date)
                    .Max();

                if (lastInjury > endDate)
                {
                    // Травма произошла сегодня или позже endDate (например, сегодня, а endDate = вчера)
                    DaysWithoutInjury = 0;
                }
                else
                {
                    DaysWithoutInjury = (int)(endDate - lastInjury).TotalDays;
                }
                LastInjuryDateStr = lastInjury.ToString("dd.MM.yyyy");
            }
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

            // Список ключей клеток креста в формате "row_col" (все 33 позиции)
            var crossCellKeys = new List<string>();
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if ((row >= 2 && row <= 4) || (col >= 2 && col <= 4))
                        crossCellKeys.Add($"{row}_{col}");
                }
            }
            crossCellKeys.Sort(); // сортировка по row, затем col

            // Словарь: ключ "row_col" -> номер дня (если есть)
            var dayMap = new Dictionary<string, int>();

            // Заполняем последовательно: первый день в первую ячейку креста и т.д.
            for (int i = 0; i < daysInMonth && i < crossCellKeys.Count; i++)
            {
                dayMap[crossCellKeys[i]] = i + 1;
            }

            var sb = new StringBuilder();
            sb.AppendLine("<div class='cross-grid'>");

            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    string key = $"{row}_{col}";
                    // Проверяем, является ли эта ячейка частью креста
                    if (crossCellKeys.Contains(key))
                    {
                        if (dayMap.TryGetValue(key, out int day))
                        {
                            DateTime cellDate = new DateTime(year, month, day);
                            bool hasInjury = InjuriesMonth.Any(inj => DateTime.Parse(inj.Date).Date == cellDate.Date);
                            bool isFuture = cellDate > DateTime.Today;
                            bool isToday = cellDate == DateTime.Today;

                            string cssClass = "cell";
                            if (isFuture)
                                cssClass += " future";
                            else if (hasInjury)
                                cssClass += " injury";
                            else
                                cssClass += " safe";

                            if (isToday)
                                cssClass += " today";

                            string onClick;
                            if (hasInjury)
                            {
                                var injury = InjuriesMonth.First(inj => DateTime.Parse(inj.Date).Date == cellDate);
                                onClick = $"showInfo({day}, true, '{HttpUtility.HtmlAttributeEncode(injury.Type)}', '{HttpUtility.HtmlAttributeEncode(injury.Description)}')";
                            }
                            else
                            {
                                onClick = $"showInfo({day}, false, '', '')";
                            }

                            sb.AppendLine($"<div class='{cssClass}' onclick='{onClick}'>{day}</div>");
                        }
                        else
                        {
                            // Пустая ячейка креста (нет дня) — делаем видимой, но не кликабельной
                            sb.AppendLine("<div class='cell empty'></div>");
                        }
                    }
                    else
                    {
                        // Ячейка вне формы креста (фоновая) — невидимая
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
                    bool hasInjury = InjuriesYear.Any(inj => DateTime.Parse(inj.Date).Date == cellDate.Date);
                    bool isFuture = cellDate > DateTime.Today;
                    bool isToday = cellDate == DateTime.Today;

                    string cssClass = "month-day";
                    if (isFuture)
                        cssClass += " future";
                    else if (hasInjury)
                        cssClass += " injury";
                    else
                        cssClass += " safe";

                    if (isToday)
                        cssClass += " today";

                    string onClick;
                    if (hasInjury)
                    {
                        var injury = InjuriesYear.First(inj => DateTime.Parse(inj.Date).Date == cellDate);
                        onClick = $" onclick='showInfo({d}, true, \"{HttpUtility.HtmlAttributeEncode(injury.Type)}\", \"{HttpUtility.HtmlAttributeEncode(injury.Description)}\")'";
                    }
                    else
                    {
                        onClick = $" onclick='showInfo({d}, false, \"\", \"\")'";
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
}