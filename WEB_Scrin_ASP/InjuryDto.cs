using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Scrin_ASP
{
    public class InjuryDto
    {
        public string Id { get; set; }
        public string Date { get; set; } // ISO формат
        public string Type { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } // Добавлено для поддержки категорий (П1, П2 и т.д.)
    }

    // Новый DTO для получения статистики, рассчитанной на бэкенде
    public class InjuryStatisticsDto
    {
        public int MonthSignificantCount { get; set; }
        public int YearSignificantCount { get; set; }
        public DateTime? LastSignificantDate { get; set; }
        public int DaysWithoutInjury { get; set; }
    }
}