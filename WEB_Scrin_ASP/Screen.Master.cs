using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.UI;

namespace WEB_Scrin_ASP
{
    public partial class Screen : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Регистрируем асинхронную задачу для загрузки данных
            Page.RegisterAsyncTask(new PageAsyncTask(LoadDataAsync));
        }

        private async Task LoadDataAsync()
        {
            try
            {
                DateTime date = DateTime.Now;

                // Период для текущего месяца (с 1 по текущее число)
                int year = date.Year;
                string month = date.ToString("MM", CultureInfo.InvariantCulture);
                string day = date.ToString("dd", CultureInfo.InvariantCulture);
                string timeTekStart = $"{year}{month}01 00:00:00";
                string timeTekEnd = $"{year}{month}{day} 23:59:59";

                // Период для предыдущего месяца (весь месяц)
                DateTime prevMonthDate = date.AddMonths(-1);
                int prevYear = prevMonthDate.Year;
                string prevMonth = prevMonthDate.ToString("MM", CultureInfo.InvariantCulture);
                int daysInPrevMonth = DateTime.DaysInMonth(prevYear, Convert.ToInt32(prevMonth));
                string timePredStart = $"{prevYear}{prevMonth}01 00:00:00";
                string timePredEnd = $"{prevYear}{prevMonth}{daysInPrevMonth} 23:59:59";

                string connectionString = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                string sqlExpression = @"
                    DECLARE @userData TABLE (
                        fact_GP int,
                        zad_GP int,
                        otcl_GP int,
                        kol_break int,
                        break_set int,
                        break_sush int,
                        run_BDM int,
                        id int,
                        fact_GP_sred int,
                        brigada int
                    );

                    DECLARE @user TABLE (
                        otcl_GP_Good int,
                        brigada_Good int,
                        otcl_GP_Bad int,
                        brigada_Bad int
                    );

                    INSERT INTO @userData (fact_GP, zad_GP, otcl_GP, kol_break, break_set, break_sush, run_BDM, id, fact_GP_sred, brigada)
                    SELECT
                        SUM(fact_GP) AS fact_GP,
                        SUM(zad_GP) AS zad_GP,
                        SUM(otcl_GP) AS otcl_GP,
                        SUM(kol_break) AS kol_break,
                        SUM(break_set) AS break_set,
                        SUM(break_sush) AS break_sush,
                        SUM(run_BDM) AS run_BDM,
                        COUNT(id) AS id,
                        AVG(fact_GP) AS fact_GP_sred,
                        brigada
                    FROM [Control].[dbo].[rep_BDM] AS t1
                    WHERE dt BETWEEN CONVERT(date, @time1, 104) AND CONVERT(date, @time2, 104)
                    GROUP BY t1.[brigada];

                    INSERT INTO @user (otcl_GP_Good, brigada_Good, otcl_GP_Bad, brigada_Bad)
                    VALUES (
                        (SELECT MAX(otcl_GP) FROM @userData),
                        (SELECT brigada FROM @userData WHERE otcl_GP = (SELECT MAX(otcl_GP) FROM @userData)),
                        (SELECT MIN(otcl_GP) FROM @userData),
                        (SELECT brigada FROM @userData WHERE otcl_GP = (SELECT MIN(otcl_GP) FROM @userData))
                    );

                    SELECT * FROM @user;
                ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@time1", timeTekStart);
                    command.Parameters.AddWithValue("@time2", timeTekEnd);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                object otcl_GP_Good = reader.GetValue(0);
                                object brigada_Good = reader.GetValue(1);
                                object otcl_GP_Bad = reader.GetValue(2);
                                object brigada_Bad = reader.GetValue(3);

                                if (date.Day > 3)
                                {
                                    Label1.Text = $@"
                                        <span class='colorBluetext'>По итогам месяца:</span>
                                        <span class='colorGreentext'> Самая лучшая смена № {brigada_Good}. Отклонение от плана: {Convert.ToInt32(otcl_GP_Good):N0} кг.</span>
                                        <span class='colorRedtext'> Самая худшая смена № {brigada_Bad}. Отклонение от плана: {Convert.ToInt32(otcl_GP_Bad):N0} кг.</span>
                                    ";
                                }
                                else
                                {
                                    Label1.Text = @"ООО ""Завод Николь-Пак"" г.Учалы ул.Кровельная 1";
                                    Label1.CssClass = "marg_blue";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // При необходимости можно залогировать ошибку
                // System.Diagnostics.Trace.TraceError(ex.ToString());
            }
        }
    }
}