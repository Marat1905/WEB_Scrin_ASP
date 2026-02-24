using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace WEB_Scrin_ASP
{
    public partial class Smena : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Загружаем статус обрыва
            LoadBreakStatus();

            // блокировка по году 2022г
            if (DateTime.Now.Year < 2050)
            {
                // нужны дата и время чтоб делать выборки
                DateTime date = DateTime.Now;

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

                /////////////////////////////////////////
                // читаем данные с sql за текущий месяц
                try
                {
                    string connectionString = @"Data Source=10.0.9.7\WINCC;Initial Catalog=Control;User ID=admin;Password=123";
                    string sqlExpression = "SELECT SUM(fact_GP),SUM(zad_GP),SUM(otcl_GP),SUM(kol_break),SUM(break_set),SUM(break_sush),SUM(run_BDM),count(id),avg(fact_GP),brigada FROM[Control].[dbo].[rep_BDM] Where dt Between CONVERT(date, @time1, 104) AND CONVERT(date, @time2, 104) GROUP BY[Control].[dbo].[rep_BDM].[brigada] order by SUM(otcl_GP) DESC";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.AddWithValue("@time1", time_tek_start);
                        command.Parameters.AddWithValue("@time2", time_tek_end);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            int n = 1;
                            while (reader.Read()) // построчно считываем данные
                            {
                                object fact_GP = reader.GetValue(0);
                                object zad_GP = reader.GetValue(1);
                                object otcl_GP = reader.GetValue(2);
                                object break_set = reader.GetValue(4);
                                object break_sush = reader.GetValue(5);
                                object run_BDM = reader.GetValue(6);
                                object count = reader.GetValue(7);
                                object avrfact_GP = reader.GetValue(8);
                                object brigada = reader.GetValue(9);
                                if (n == 1)
                                {
                                    Tab_1_1_1.Text = "Смена  " + brigada.ToString();
                                    Tab_1_1_2.Text = count.ToString();
                                    Tab_1_1_3.Text = break_set.ToString();
                                    Tab_1_1_4.Text = break_sush.ToString();
                                    Tab_1_1_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_1_1_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_1_1_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    if (Convert.ToInt32(otcl_GP) < 0)
                                    {
                                        Tab_1_1_7.CssClass = "PL_otkl";
                                    }
                                    else
                                    {
                                        Tab_1_1_7.CssClass = "PL";
                                    }
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_1_1_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_1_1_9.Text = String.Format("{0:0,0}", avrfact_GP);

                                }
                                if (n == 2)
                                {
                                    Tab_1_2_1.Text = "Смена  " + brigada.ToString();
                                    Tab_1_2_2.Text = count.ToString();
                                    Tab_1_2_3.Text = break_set.ToString();
                                    Tab_1_2_4.Text = break_sush.ToString();
                                    Tab_1_2_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_1_2_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_1_2_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    if (Convert.ToInt32(otcl_GP) < 0)
                                    {
                                        Tab_1_2_7.CssClass = "PL_otkl";
                                    }
                                    else
                                    {
                                        Tab_1_2_7.CssClass = "PL";
                                    }
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_1_2_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_1_2_9.Text = String.Format("{0:0,0}", avrfact_GP);
                                }
                                if (n == 3)
                                {
                                    Tab_1_3_1.Text = "Смена  " + brigada.ToString();
                                    Tab_1_3_2.Text = count.ToString();
                                    Tab_1_3_3.Text = break_set.ToString();
                                    Tab_1_3_4.Text = break_sush.ToString();
                                    Tab_1_3_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_1_3_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_1_3_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    if (Convert.ToInt32(otcl_GP) < 0)
                                    {
                                        Tab_1_3_7.CssClass = "PL_otkl";
                                    }
                                    else
                                    {
                                        Tab_1_3_7.CssClass = "PL";
                                    }
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_1_3_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_1_3_9.Text = String.Format("{0:0,0}", avrfact_GP);
                                }
                                if (n == 4)
                                {
                                    Tab_1_4_1.Text = "Смена  " + brigada.ToString();
                                    Tab_1_4_1.CssClass = "shift_red";
                                    Tab_1_4_2.Text = count.ToString();
                                    Tab_1_4_2.CssClass = "sm_red";
                                    Tab_1_4_3.Text = break_set.ToString();
                                    Tab_1_4_3.CssClass = "sm_red";
                                    Tab_1_4_4.Text = break_sush.ToString();
                                    Tab_1_4_4.CssClass = "sm_red";
                                    Tab_1_4_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_1_4_5.CssClass = "PL_red";
                                    Tab_1_4_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_1_4_6.CssClass = "PL_red";
                                    Tab_1_4_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    Tab_1_4_7.CssClass = "PL_red";
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_1_4_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_1_4_8.CssClass = "PL_red";
                                    Tab_1_4_9.Text = String.Format("{0:0,0}", avrfact_GP);
                                    Tab_1_4_9.CssClass = "PL_red";
                                }
                                n++;
                            }
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {

                }

                try
                {
                    // читаем данные с sql за предыдущий месяц
                    string connectionString1 = @"Data Source=10.0.9.7\WINCC;Initial Catalog=Control;User ID=admin;Password=123";

                    string sqlExpression1 = "SELECT SUM(fact_GP),SUM(zad_GP),SUM(otcl_GP),SUM(kol_break),SUM(break_set),SUM(break_sush),SUM(run_BDM),count(id),avg(fact_GP),brigada FROM[Control].[dbo].[rep_BDM] Where dt Between CONVERT(date, @time1, 104) AND CONVERT(date, @time2, 104) GROUP BY[Control].[dbo].[rep_BDM].[brigada] order by SUM(otcl_GP) DESC";
                    using (SqlConnection connection1 = new SqlConnection(connectionString1))
                    {
                        connection1.Open();
                        SqlCommand command1 = new SqlCommand(sqlExpression1, connection1);
                        command1.Parameters.AddWithValue("@time1", time_pred_start);
                        command1.Parameters.AddWithValue("@time2", time_pred_end);
                        SqlDataReader reader1 = command1.ExecuteReader();

                        if (reader1.HasRows) // если есть данные
                        {
                            int n = 1;
                            while (reader1.Read()) // построчно считываем данные
                            {
                                object fact_GP = reader1.GetValue(0);
                                object zad_GP = reader1.GetValue(1);
                                object otcl_GP = reader1.GetValue(2);
                                object break_set = reader1.GetValue(4);
                                object break_sush = reader1.GetValue(5);
                                object run_BDM = reader1.GetValue(6);
                                object count = reader1.GetValue(7);
                                object avrfact_GP = reader1.GetValue(8);
                                object brigada = reader1.GetValue(9);
                                if (n == 1)
                                {
                                    Tab_2_1_1.Text = "Смена  " + brigada.ToString();
                                    Tab_2_1_2.Text = count.ToString();
                                    Tab_2_1_3.Text = break_set.ToString();
                                    Tab_2_1_4.Text = break_sush.ToString();
                                    Tab_2_1_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_2_1_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_2_1_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_2_1_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_2_1_9.Text = String.Format("{0:0,0}", avrfact_GP);

                                }
                                if (n == 2)
                                {
                                    Tab_2_2_1.Text = "Смена  " + brigada.ToString();
                                    Tab_2_2_2.Text = count.ToString();
                                    Tab_2_2_3.Text = break_set.ToString();
                                    Tab_2_2_4.Text = break_sush.ToString();
                                    Tab_2_2_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_2_2_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_2_2_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_2_2_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_2_2_9.Text = String.Format("{0:0,0}", avrfact_GP);
                                }
                                if (n == 3)
                                {
                                    Tab_2_3_1.Text = "Смена  " + brigada.ToString();
                                    Tab_2_3_2.Text = count.ToString();
                                    Tab_2_3_3.Text = break_set.ToString();
                                    Tab_2_3_4.Text = break_sush.ToString();
                                    Tab_2_3_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_2_3_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_2_3_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_2_3_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_2_3_9.Text = String.Format("{0:0,0}", avrfact_GP);
                                }
                                if (n == 4)
                                {
                                    Tab_2_4_1.Text = "Смена  " + brigada.ToString();
                                    Tab_2_4_2.Text = count.ToString();
                                    Tab_2_4_3.Text = break_set.ToString();
                                    Tab_2_4_4.Text = break_sush.ToString();
                                    Tab_2_4_5.Text = String.Format("{0:0,0}", zad_GP);
                                    Tab_2_4_6.Text = String.Format("{0:0,0}", fact_GP);
                                    Tab_2_4_7.Text = String.Format("{0:0,0}", otcl_GP);
                                    int chas = Convert.ToInt32(run_BDM) / 60;
                                    int min = Convert.ToInt32(run_BDM) - chas * 60;
                                    Tab_2_4_8.Text = chas.ToString() + "  ч.  " + min.ToString() + " мин.";
                                    Tab_2_4_9.Text = String.Format("{0:0,0}", avrfact_GP);
                                }
                                n++;
                            }
                        }

                        reader1.Close();
                    }
                }
                catch (Exception ex)
                {

                }


            }

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("birthday.aspx");
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            // Обновляем статусную строку через базовый метод
            UpdateStatusTextBox(textbox200);
        }
    }
}