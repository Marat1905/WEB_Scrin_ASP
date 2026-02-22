using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB_Scrin_ASP
{
    /// <summary>
    /// Базовый класс для страниц, которым требуется получение статуса обрыва (break_status)
    /// из таблицы [Control].[dbo].[page_1].
    /// </summary>
    public class BasePage : Page
    {
        /// <summary>
        /// Текущий статус обрыва, полученный из базы данных.
        /// </summary>
        protected string breakStatus = "0";

        /// <summary>
        /// Загружает статус обрыва (break_status) из таблицы page_1.
        /// Вызывать в Page_Load до использования breakStatus.
        /// </summary>
        protected void LoadBreakStatus()
        {
            try
            {
                string connectionString = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                string sqlExpression = "SELECT TOP 1 * FROM [Control].[dbo].[page_1] ORDER BY dt DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // break_status находится в колонке с индексом 21 (нумерация с 0)
                            object break_stat = reader.GetValue(21);
                            breakStatus = break_stat?.ToString() ?? "0";
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                // В случае ошибки оставляем значение по умолчанию "0"
                breakStatus = "0";
            }
        }

        /// <summary>
        /// Обновляет переданный TextBox в соответствии с текущим значением breakStatus.
        /// Устанавливает текст и CSS-класс.
        /// Вызывать в Page_LoadComplete.
        /// </summary>
        /// <param name="statusTextBox">Текстовое поле, в которое выводится статус.</param>
        protected void UpdateStatusTextBox(TextBox statusTextBox)
        {
            if (statusTextBox == null)
                return;

            switch (breakStatus)
            {
                case "0":
                    statusTextBox.Text = "НЕТ СВЯЗИ";
                    statusTextBox.CssClass = "blnktext0";
                    break;
                case "1":
                    statusTextBox.Text = "ОБРЫВ ПОЛОТНА, НО БДМ В РАБОТЕ";
                    statusTextBox.CssClass = "blnktext1";
                    break;
                case "2":
                    statusTextBox.Text = "ОБРЫВ ПОЛОТНА, МАССА СНЯТА С СЕТОЧНОГО СТОЛА";
                    statusTextBox.CssClass = "blnktext2";
                    break;
                case "3":
                    statusTextBox.Text = "ОБРЫВ ПОЛОТНА, МАССА СНЯТА С ВЕРХНЕГО СЕТОЧНОГО СТОЛА";
                    statusTextBox.CssClass = "blnktext2";
                    break;
                case "4":
                    statusTextBox.Text = "ОБРЫВ ПОЛОТНА, НЕ ЗАПЛАНИРОВАННЫЙ ОСТАНОВ";
                    statusTextBox.CssClass = "blnktext4";
                    break;
                case "5":
                    statusTextBox.Text = "ОБРЫВ ПОЛОТНА, ЗАПЛАНИРОВАННЫЙ ОСТАНОВ";
                    statusTextBox.CssClass = "blnktext5";
                    break;
                case "6":
                    statusTextBox.Text = "ХМ... ОСТАНОВ МАШИНЫ БОЛЬШЕ ЗАПЛАНИРОВАННОГО";
                    statusTextBox.CssClass = "blnktext6";
                    break;
                case "7":
                    statusTextBox.Text = "БДМ В РАБОТЕ";
                    statusTextBox.CssClass = "blnktext7";
                    break;
                default:
                    statusTextBox.Text = "НЕИЗВЕСТНЫЙ СТАТУС";
                    statusTextBox.CssClass = "blnktext0";
                    break;
            }
        }
    }
}