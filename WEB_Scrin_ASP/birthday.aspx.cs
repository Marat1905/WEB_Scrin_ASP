using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace WEB_Scrin_ASP
{
    public partial class birthday : BasePage   // изменено с System.Web.UI.Page на BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Загружаем статус обрыва из базы (метод BasePage)
            LoadBreakStatus();

            if (DateTime.Now.Year < 2050)
            {
                DateTime date = DateTime.Now;
                int month = date.Month;

                // Устанавливаем заголовок в зависимости от месяца (предложный падеж)
                string[] monthNamesPrepositional = {
                    "", "январе", "феврале", "марте", "апреле", "мае", "июне",
                    "июле", "августе", "сентябре", "октябре", "ноябре", "декабре"
                };
                Label1.Text = $"Дни рождения сотрудников в {monthNamesPrepositional[month]} месяце";

                try
                {
                    string connectionString = @"Data Source=10.0.9.7\WINCC;Initial Catalog=Control;User ID=admin;Password=123";
                    string sqlExpression = "SELECT * FROM [Control].[dbo].[DOB] WHERE MONTH(db) = @mes ORDER BY DAY(db) asc";

                    var birthdays = new List<BirthdayItem>();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.AddWithValue("@mes", month);
                        SqlDataReader reader = command.ExecuteReader();

                        // Русская культура для правильного склонения месяцев
                        CultureInfo ruCulture = CultureInfo.GetCultureInfo("ru-RU");

                        while (reader.Read())
                        {
                            string fio = reader.GetValue(2).ToString();
                            DateTime dbDate = DateTime.Parse(reader.GetValue(4).ToString());

                            // Форматируем дату: "7 февраля" (родительный падеж)
                            string displayDate = dbDate.ToString("d MMMM", ruCulture);

                            birthdays.Add(new BirthdayItem
                            {
                                FullName = fio,
                                DisplayDate = displayDate
                            });
                        }
                        reader.Close();
                    }

                    RepeaterBirthdays.DataSource = birthdays;
                    RepeaterBirthdays.DataBind();
                }
                catch (Exception ex)
                {
                    // Логирование ошибки (можно заменить на более подходящее)
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        // Добавлен обработчик Page_LoadComplete для обновления статусной строки
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            // Обновляем статусную строку через базовый метод
            UpdateStatusTextBox(textbox200);
        }

        // Вспомогательный класс для хранения данных именинника
        public class BirthdayItem
        {
            public string FullName { get; set; }
            public string DisplayDate { get; set; }
        }
    }
}