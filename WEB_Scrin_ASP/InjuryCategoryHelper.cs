using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_Scrin_ASP
{
    /// <summary>
    /// Вспомогательный класс для работы с категориями травм.
    /// Содержит словарь категорий (аналог categoryOptions из React-проекта)
    /// и методы для получения человекочитаемых меток.
    /// </summary>
    public static class InjuryCategoryHelper
    {
        /// <summary>
        /// Словарь категорий травм.
        /// Ключ - техническое значение категории (как приходит с API),
        /// Значение - человекочитаемая метка для отображения в UI.
        /// </summary>
        private static readonly Dictionary<string, string> CategoryLabels = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Fatality",              "П1 - Смертельный случай" },
            { "LostWorkdayCase",       "П2 - Травма с потерей трудоспособности" },
            { "FirstAidCase",          "П3 - Микротравма" },
            { "AccidentOrNearMiss",    "П4 - Авария / Near Miss" },
            { "PreventedIncident",     "П5 - Предотвращенное происшествие" },
            { "ThirdPartyInjury",      "П6 - Травма третьего лица" }
        };

        /// <summary>
        /// Список значимых категорий (П1 и П2), по которым считается счётчик дней без травм.
        /// </summary>
        private static readonly HashSet<string> SignificantCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Fatality",
            "LostWorkdayCase",
            "П1",
            "П2"
        };

        /// <summary>
        /// Получить человекочитаемую метку для категории.
        /// Если категория не найдена в словаре, возвращается исходное значение
        /// или "Не указана", если строка пустая.
        /// </summary>
        /// <param name="category">Техническое значение категории (например, "Fatality")</param>
        /// <returns>Человекочитаемая метка (например, "П1 - Смертельный случай")</returns>
        public static string GetLabel(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return "Не указана";

            if (CategoryLabels.TryGetValue(category, out string label))
                return label;

            // Если категория не найдена в словаре, возвращаем её как есть
            return category;
        }

        /// <summary>
        /// Проверить, относится ли категория к значимым (П1/П2).
        /// Используется для определения цвета ячейки и расчёта дней без травм.
        /// </summary>
        /// <param name="category">Категория травмы</param>
        /// <returns>true, если категория значимая</returns>
        public static bool IsSignificant(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return false;

            return SignificantCategories.Contains(category);
        }

        /// <summary>
        /// Получить список всех категорий в виде пар (значение, метка).
        /// Может использоваться для формирования выпадающих списков и т.п.
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetAllCategories()
        {
            return CategoryLabels.AsEnumerable();
        }
    }
}