using System;
using System.Data.SqlClient;
using System.Globalization;

namespace WEB_Scrin_ASP
{
    public partial class Screen : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // нужны дата и время чтоб делать выборки
            DateTime date = DateTime.Now;
            string text;
            // для выборки текущий месяц
            int year = date.Year;
            string month = date.ToString("MM", CultureInfo.InvariantCulture);
            string den = date.ToString("dd", CultureInfo.InvariantCulture);
            string time_tek_start = year + month + "01 00:00:00";
            string time_tek_end = year + month + den.ToString() + " 23:59:59";
            // для выборки предыдущий месяц
            DateTime date1 = date.AddMonths(-1);
            int year_pred = date1.Year;
            string month_pred = date1.ToString("MM", CultureInfo.InvariantCulture);
            string days_Mo = DateTime.DaysInMonth(year_pred, Convert.ToInt32(month_pred)).ToString();
            string time_pred_start = year_pred + month_pred + "01 00:00:00";
            string time_pred_end = year_pred + month_pred + days_Mo.ToString() + " 23:59:59";
            try
            {
                string connectionString = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                string sqlExpression = @"DECLARE @userData TABLE(
    fact_GP int ,
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

DECLARE @user TABLE(
    otcl_GP_Good int,
    brigada_Good int,
    otcl_GP_Bad int,
    brigada_Bad int
);

INSERT INTO @userData (fact_GP, zad_GP,otcl_GP,kol_break,break_set,break_sush,run_BDM,id,fact_GP_sred,brigada)
SELECT SUM(fact_GP)fact_GP,SUM(zad_GP)zad_GP,SUM(otcl_GP)otcl_GP,SUM(kol_break)kol_break,SUM(break_set)break_set,SUM(break_sush) break_sush,SUM(run_BDM)run_BDM,count(id)id,avg(fact_GP)fact_GP_sred,brigada FROM[Control].[dbo].[rep_BDM] as t1 Where dt Between CONVERT(date, @time1, 104) AND CONVERT(date, @time2, 104) GROUP BY t1.[brigada] ;

insert into @user(otcl_GP_Good,brigada_Good,otcl_GP_Bad,brigada_Bad) values (
(select MAX(otcl_GP) from @userData),
(select brigada from @userData where otcl_GP=(select MAX(otcl_GP) from @userData)),
(select MIN(otcl_GP) from @userData),
(select brigada from @userData where otcl_GP=(select MIN(otcl_GP) from @userData)))
select*from @user
";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@time1", time_tek_start);
                    command.Parameters.AddWithValue("@time2", time_tek_end);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            object otcl_GP_Good = reader.GetValue(0);
                            object brigada_Good = reader.GetValue(1);
                            object otcl_GP_Bad = reader.GetValue(2);
                            object brigada_Bad = reader.GetValue(3);

                            if (date.Day > 3)
                            {
                                Label1.Text = @"<span class=""colorBluetext""> По итогам месяца:</ span > <span class=""colorGreentext""> Cамая лучшая смена № " + brigada_Good.ToString() + ". Отклонение от плана: " + String.Format("{0:0,0}", otcl_GP_Good) + " кг.</span >"+
                                     @"<span class=""colorRedtext""> Cамая худшая смена № " + brigada_Bad.ToString() + ". Отклонение от плана: " + String.Format("{0:0,0}", otcl_GP_Bad) +" кг.</ span >";
                            }
                            else
                            {
                                Label1.Text = @" ООО ""Завод Николь-Пак"" г.Учалы ул.Кровельная 1";
                                Label1.CssClass = "marg_blue";
                            }
                        }
                    }

                }
            }catch(Exception ex)
            {

            }
        }
    }
}