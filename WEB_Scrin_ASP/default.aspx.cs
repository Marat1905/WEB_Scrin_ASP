using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;

namespace WEB_Scrin_ASP
{
    public partial class _default : BasePage
    {
        int otkl_tek_mes, otkl_tek_god;

        private const string key = "counter";
        protected int Counter
        {
            get
            {
                object obj = ViewState[key];
                if (obj != null)
                {
                    return (int)obj;
                }
                else
                {
                    ViewState[key] = 0;
                    return 0;
                }
            }
            set
            {
                ViewState[key] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Загружаем статус обрыва из базы
            LoadBreakStatus();

            // блокировка по году 2050г
            if (DateTime.Now.Year < 2050)
            {
                Page.Server.ScriptTimeout = 180;
                if (Counter >= 1)
                {
                    Response.Redirect("idle time.aspx");
                }
                else
                {
                    // ничего
                }

                try
                {
                    string connectionString = @"Data Source=10.0.9.7\WINCC;Initial Catalog=Control;User ID=admin;Password=123";
                    string sqlExpression = @"delete FROM [Control].[dbo].[page_1]
                where dt not in (SELECT top 1 dt FROM[Control].[dbo].[page_1] order by dt desc)
                SELECT top 1*FROM[Control].[dbo].[page_1] order by dt desc";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                object fact_setki = reader.GetValue(0);
                                object fact_nakat = reader.GetValue(1);
                                object target_gramm = reader.GetValue(2);
                                object fact_gramm = reader.GetValue(3);

                                object tek_break = reader.GetValue(4);
                                object pred_break = reader.GetValue(5);
                                object pred_PRS = reader.GetValue(6);
                                object tek_PRS = reader.GetValue(7);

                                object target_smena = reader.GetValue(8);
                                object target_god = reader.GetValue(9);
                                object fact_god = reader.GetValue(10);
                                object target_mes = reader.GetValue(11);

                                object fact_mes = reader.GetValue(12);
                                object prost_tek_god_chas = reader.GetValue(13);
                                object prost_tek_god_min = reader.GetValue(14);
                                object prost_tek_mes_chas = reader.GetValue(15);

                                object prost_tek_mes_min = reader.GetValue(16);
                                object prost_tek_smena_chas = reader.GetValue(17);
                                object prost_tek_smena_min = reader.GetValue(18);
                                object prost_pred_smena_chas = reader.GetValue(19);

                                object prost_pred_smena_min = reader.GetValue(20);
                                object dt = reader.GetValue(22);

                                textbox.Text = string.Format("{0:0.#}", fact_setki);
                                textbox5.Text = string.Format("{0:0.#}", fact_nakat);
                                textbox7.Text = target_gramm.ToString();
                                textbox13.Text = string.Format("{0:0.#}", fact_gramm);
                                textbox9.Text = tek_break.ToString();
                                textbox3.Text = Convert.ToString(target_god).Replace(" ", "");
                                textbox21.Text = Convert.ToString(fact_god).Replace(" ", "");
                                textbox23.Text = Convert.ToString(target_mes).Replace(" ", "");
                                textbox25.Text = Convert.ToString(fact_mes).Replace(" ", "");
                                textbox27.Text = Convert.ToString(target_smena).Replace(" ", "");
                                textbox29.Text = Convert.ToString(tek_PRS).Replace(" ", "");
                                textbox11.Text = pred_break.ToString();

                                // Объединённое время работы БДМ за год
                                int temp = Convert.ToInt32(prost_tek_god_chas.ToString()) * 60 + Convert.ToInt32(prost_tek_god_min.ToString());
                                DateTime tekYear = new DateTime(DateTime.Now.Year, 1, 1);
                                TimeSpan ts = DateTime.Now - tekYear;
                                int temp2 = Convert.ToInt32(ts.TotalMinutes) - temp;
                                if (temp2 > 0)
                                {
                                    int hours = temp2 / 60;
                                    int minutes = temp2 % 60;
                                    textbox100.Text = $"{hours} ч {minutes} мин";
                                }
                                else
                                {
                                    textbox100.Text = "0 ч 0 мин";
                                }

                                // Объединённое время простоев
                                textbox33.Text = $"{prost_tek_god_chas} ч {prost_tek_god_min} мин";
                                textbox37.Text = $"{prost_tek_mes_chas} ч {prost_tek_mes_min} мин";
                                textbox41.Text = $"{prost_tek_smena_chas} ч {prost_tek_smena_min} мин";
                                textbox45.Text = $"{prost_pred_smena_chas} ч {prost_pred_smena_min} мин";

                            }
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибки, можно логировать
                }

                try
                {
                    // нужны дата и время чтоб делать выборки
                    DateTime date = DateTime.Now;

                    // для выборки текущий месяц
                    int year = date.Year;
                    string month = date.ToString("MM", CultureInfo.InvariantCulture);
                    string den = date.ToString("dd", CultureInfo.InvariantCulture);
                    string time_mes_start = year + month + "01 00:00:00";
                    string time_mes_end = year + month + den.ToString() + " 23:59:59";
                    string time_god_start = year + "0101 00:00:00";
                    string time_god_end = year + "1231 23:59:59";
                    object otkl_mes = 0, otkl_god = 0;
                    object temp_otkl_mes = 0, temp_otkl_god = 0;

                    string connectionString = @"Data Source=10.0.9.7\WINCC;Initial Catalog=Control;User ID=admin;Password=123";
                    string sqlExpression = "SELECT (SELECT SUM(otcl_GP) FROM[Control].[dbo].[rep_BDM] WHERE dt BETWEEN CONVERT(DATE, @time_mes_start, 104) AND CONVERT(DATE, @time_mes_end, 104)) AS mes, (SELECT  SUM(otcl_GP) FROM[Control].[dbo].[rep_BDM] WHERE dt BETWEEN CONVERT(DATE, @time_god_start, 104) AND CONVERT(DATE, @time_god_end, 104)) AS god";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.AddWithValue("@time_mes_start", time_mes_start);
                        command.Parameters.AddWithValue("@time_mes_end", time_mes_end);
                        command.Parameters.AddWithValue("@time_god_start", time_god_start);
                        command.Parameters.AddWithValue("@time_god_end", time_god_end);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                otkl_mes = reader.GetValue(0);
                                otkl_god = reader.GetValue(1);
                            }
                        }
                        reader.Close();
                    }

                    string sqlExpression1 = "SELECT *FROM [Control].[dbo].[difference]where id=1";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression1, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                temp_otkl_mes = reader.GetValue(2);
                                temp_otkl_god = reader.GetValue(1);
                            }
                        }
                        reader.Close();
                    }

                    string connectionString1 = @"Data Source=10.0.9.4;Initial Catalog=kiu_opc; User ID = mes; pwd = mes";
                    string sqlExp_Pred = "SELECT top 1 WrDate ,VypuskGod FROM[kiu_opc].[dbo].[VypuskRS] where bd like 'zavod' and Date between DATEADD(HOUR,-12, DATEADD(year,-1, CONVERT(Datetime, @Date_Start, 104) ))  And DATEADD(year,-1, CONVERT(Datetime, @Date_End, 104) ) order by WrDate desc";
                    using (SqlConnection connection = new SqlConnection(connectionString1))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExp_Pred, connection);
                        string ds = DateTime.Now.AddHours(-48).ToString("yyyyMMdd HH:mm:ss");
                        command.Parameters.AddWithValue("@Date_Start", ds);
                        string dd = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                        command.Parameters.AddWithValue("@Date_End", dd);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textbox16.Text = reader.GetValue(1).ToString();
                            }
                        }
                        reader.Close();
                    }

                    if (Convert.ToInt32(otkl_god) > 0)
                    {
                        textbox15.Text = String.Format("{0:0,0}", otkl_god);
                        otkl_tek_god = Convert.ToInt32(otkl_god);
                    }
                    else
                    {
                        textbox15.Text = String.Format("{0:0,0}", Convert.ToInt32(otkl_god) + Convert.ToInt32(temp_otkl_god));
                        otkl_tek_god = Convert.ToInt32(otkl_god) + Convert.ToInt32(temp_otkl_god);
                    }

                    if (Convert.ToInt32(otkl_mes) > 0)
                    {
                        textbox19.Text = String.Format("{0:0,0}", otkl_mes);
                        otkl_tek_mes = Convert.ToInt32(otkl_mes);
                    }
                    else
                    {
                        textbox19.Text = String.Format("{0:0,0}", Convert.ToInt32(otkl_mes) + Convert.ToInt32(temp_otkl_mes));
                        otkl_tek_mes = Convert.ToInt32(otkl_mes) + Convert.ToInt32(temp_otkl_mes);
                    }
                }
                catch (Exception ep)
                {
                    string ddf = ep.Message.ToString();
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Counter += 1;
        }

        protected void textbox31_TextChanged(object sender, EventArgs e)
        {
            // удалено
        }

        protected void textbox4_TextChanged(object sender, EventArgs e)
        {
            // удалено
        }

        protected void textbox46_TextChanged(object sender, EventArgs e)
        {
            // удалено
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            try
            {
                // Обновляем статусную строку через базовый метод
                UpdateStatusTextBox(textbox46);

                if (otkl_tek_mes < 0)
                {
                    textbox19.Text = String.Format("{0:0,0}", otkl_tek_mes);
                    textbox19.CssClass = "textbox_otkl_red";
                }
                else if ((otkl_tek_mes > 0))
                {
                    string plan_mes = String.Format("{0:0,0}", otkl_tek_mes);
                    textbox19.Text = "+" + plan_mes;
                    textbox19.CssClass = "textbox_otkl_grin";
                }

                if (otkl_tek_god < 0)
                {
                    textbox15.Text = String.Format("{0:0,0}", otkl_tek_god);
                    textbox15.CssClass = "textbox_otkl_red";
                }
                else if ((otkl_tek_god > 0))
                {
                    string plan_god = String.Format("{0:0,0}", otkl_tek_god);
                    textbox15.Text = "+ " + plan_god;
                    textbox15.CssClass = "textbox_otkl_grin";
                }
            }
            catch
            {
                // игнорируем ошибки в LoadComplete
            }
        }
    }
}