using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace WEB_Scrin_ASP
{
    public partial class idle_time : System.Web.UI.Page
    {
        string break_status = "0";
        object TekMes_Electro, PredMes_Electro, tekGod_Electro, PredGod_Electro, TekMes_Mex, PredMes_Mex, tekGod_Mex, PredGod_Mex, TekMes_Tex, PredMes_Tex;


        object tekGod_Tex, PredGod_Tex, TekMes_PPR, PredMes_PPR, tekGod_PPR, PredGod_PPR, TekMes_Other, PredMes_Other, tekGod_Other, PredGod_Other;

        object temp_tekGod_Electro, temp_tekGod_Mex, temp_tekGod_Tex, temp_tekGod_PPR, temp_tekGod_Other;
        int Count_All_TekMes, Count_All_PredMes, Count_All_tekGod, Count_All_PredGod;
        protected void Page_Load(object sender, EventArgs e)
        {
            // блокировка по году 2022г
            if (DateTime.Now.Year < 2025)
            {
                Page.Server.ScriptTimeout = 65;

                try
                {
                    string connectionString = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                    string sqlExpression = "SELECT TOP 1*FROM [Control].[dbo].[page_1] order by dt desc";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                object break_stat = reader.GetValue(21);
                                break_status = break_stat.ToString();
                            }
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {

                }

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
                // для выборки текущий год 

                string time_tekGod_start = year + "0101 00:00:00";
                string time_tekGod_end = year + month + den.ToString() + " 23:59:59";
                // для выборки предыдущий год
                DateTime date2 = date.AddYears(-1);
                int year_predGod = date2.Year;
                string time_predGod_start = year_predGod + "0101 00:00:00";
                string time_predGod_end = year_predGod + month + den.ToString() + " 23:59:59";
               

                /////////////////////////////////////////
                // читаем данные с sql за текущий месяц
                try
                {
                    string connectionString = @"Data Source=10.0.9.4;Initial Catalog=kiu_opc; User ID = mes; pwd = mes";
                    string sqlExpression = @" SET ANSI_NULLS off  SELECT
 (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Эл.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104)) as TekMes_Electro,
  (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Эл.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104))as PredMes_Electro,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Эл.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekMGod_end, 104))as tekGod_Electro,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Эл.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104))as PredGod_Electro,
   
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Мех.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104))as TekMes_Mex,
  (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Мех.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104))as PredMes_Mex,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Мех.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekMGod_end, 104))as tekGod_Mex,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр1 IN('Мех.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104))as PredGod_Mex,
   
      (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр2 IN('Технологическая') and[ДатаДокумента] Between CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104))as TekMes_Tex,
  (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр2 IN('Технологическая') and[ДатаДокумента] Between CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104))as PredMes_Tex,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр2 IN('Технологическая') and[ДатаДокумента] Between CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekMGod_end, 104))as tekGod_Tex,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where ПричинаУр2 IN('Технологическая') and[ДатаДокумента] Between CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104))as PredGod_Tex,
   
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина IN('ПТО', 'ППР, останов на ППР','ППР') and[ДатаДокумента] Between CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104))as TekMes_PPR,
  (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина IN('ПТО', 'ППР, останов на ППР','ППР') and[ДатаДокумента] Between CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104))as PredMes_PPR,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина IN('ПТО', 'ППР, останов на ППР','ППР') and[ДатаДокумента] Between CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekMGod_end, 104))as tekGod_PPR,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина IN('ПТО', 'ППР, останов на ППР','ППР') and[ДатаДокумента] Between CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104))as PredGod_PPR,
   
      (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина not IN('ПТО','ППР, останов на ППР','ППР')and ПричинаУр2 not IN('Технологическая') and ПричинаУр1 not IN('Мех.часть','Эл.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104))as TekMes_Other,
  (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина not IN('ПТО','ППР, останов на ППР','ППР')and ПричинаУр2 not IN('Технологическая') and ПричинаУр1 not IN('Мех.часть','Эл.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104))as PredMes_Other,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина not IN('ПТО','ППР, останов на ППР','ППР')and ПричинаУр2 not IN('Технологическая')and ПричинаУр1 not IN('Мех.часть','Эл.часть')  and[ДатаДокумента] Between CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekMGod_end, 104))as tekGod_Other,
   (select sum(Продолжительность)FROM[kiu_opc].[dbo].[Простои]Where Причина not in ('ПТО', 'ППР, останов на ППР','ППР')and ПричинаУр2 not IN('Технологическая')and ПричинаУр1 not IN('Мех.часть','Эл.часть') and[ДатаДокумента] Between CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104))as PredGod_Other   SET ANSI_NULLS on";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.AddWithValue("@time_tekMes_start", time_tek_start);
                        command.Parameters.AddWithValue("@time_tekMes_end", time_tek_end);
                        command.Parameters.AddWithValue("@time_predMes_start", time_pred_start);
                        command.Parameters.AddWithValue("@time_predMes_end", time_pred_end);
                        command.Parameters.AddWithValue("@time_tekGod_start", time_tekGod_start);
                        command.Parameters.AddWithValue("@time_tekMGod_end", time_tekGod_end);
                        command.Parameters.AddWithValue("@time_predGod_start", time_predGod_start);
                        command.Parameters.AddWithValue("@time_predGod_end", time_predGod_end);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {

                                TekMes_Electro = reader.GetValue(0);
                                PredMes_Electro = reader.GetValue(1);
                                tekGod_Electro = reader.GetValue(2);
                                PredGod_Electro = reader.GetValue(3);

                                TekMes_Mex = reader.GetValue(4);
                                PredMes_Mex = reader.GetValue(5);
                                tekGod_Mex = reader.GetValue(6);
                                PredGod_Mex = reader.GetValue(7);

                                TekMes_Tex = reader.GetValue(8);
                                PredMes_Tex = reader.GetValue(9);
                                tekGod_Tex = reader.GetValue(10);
                                PredGod_Tex = reader.GetValue(11);

                                TekMes_PPR = reader.GetValue(12);
                                PredMes_PPR = reader.GetValue(13);
                                tekGod_PPR = reader.GetValue(14);
                                PredGod_PPR = reader.GetValue(15);

                                TekMes_Other = reader.GetValue(16);
                                PredMes_Other = reader.GetValue(17);
                                tekGod_Other = reader.GetValue(18);
                                PredGod_Other = reader.GetValue(19);

                               
                                Count_All_TekMes = TekMes_Electro.IfNullThenZero()+ TekMes_Mex.IfNullThenZero()+ TekMes_Tex.IfNullThenZero()+ TekMes_PPR.IfNullThenZero() + TekMes_Other.IfNullThenZero();
                                Count_All_PredMes = PredMes_Electro.IfNullThenZero() + PredMes_Mex.IfNullThenZero() + PredMes_Tex.IfNullThenZero() + PredMes_PPR.IfNullThenZero() + PredMes_Other.IfNullThenZero();
                                Count_All_tekGod = tekGod_Electro.IfNullThenZero() + tekGod_Mex.IfNullThenZero() + tekGod_Tex.IfNullThenZero() + tekGod_PPR.IfNullThenZero() + tekGod_Other.IfNullThenZero();
                                Count_All_PredGod = PredGod_Electro.IfNullThenZero() + PredGod_Mex.IfNullThenZero() + PredGod_Tex.IfNullThenZero() + PredGod_PPR.IfNullThenZero() + PredGod_Other.IfNullThenZero();
                            }
                        }
                        reader.Close();
                    }


                    /////////////////////////////////////////

                    string connectionString1 = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                    string sqlExpression1 = "SELECT *FROM [Control].[dbo].[difference]where id=1";
                    using (SqlConnection connection = new SqlConnection(connectionString1))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression1, connection);
                       
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) // если есть данные
                        {
                            while (reader.Read()) // построчно считываем данные
                            {
                                temp_tekGod_Electro = reader.GetValue(3);
                                temp_tekGod_Mex = reader.GetValue(4);
                                temp_tekGod_Tex = reader.GetValue(5);

                                temp_tekGod_PPR = reader.GetValue(6);
                                temp_tekGod_Other = reader.GetValue(7);
                               

                              
                            }
                        }
                        reader.Close();
                    }
                    ///////////////////////////////////////


                }
                catch (Exception ex)
                {

                }

            }



        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("smena.aspx");
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            try
            {


                if (break_status == "0")
                {
                    textbox200.Text = "НЕТ СВЯЗИ";
                    textbox200.CssClass = "blnktext0";
                }
                else if (break_status == "1")
                {
                    textbox200.Text = "ОБРЫВ ПОЛОТНА, НО БДМ В РАБОТЕ";
                    textbox200.CssClass = "blnktext1";
                }
                else if (break_status == "2")
                {
                    textbox200.Text = "ОБРЫВ ПОЛОТНА, МАССА СНЯТА С СЕТОЧНОГО СТОЛА";
                    textbox200.CssClass = "blnktext2";
                }
                else if (break_status == "3")
                {
                    textbox200.Text = "ОБРЫВ ПОЛОТНА, МАССА СНЯТА С ВЕРХНЕГО СЕТОЧНОГО СТОЛА";
                    textbox200.CssClass = "blnktext2";
                }
                else if (break_status == "4")
                {
                    textbox200.Text = "ОБРЫВ ПОЛОТНА, НЕ ЗАПЛАНИРОВАННЫЙ ОСТАНОВ";
                    textbox200.CssClass = "blnktext4";
                }
                else if (break_status == "5")
                {
                    textbox200.Text = "ОБРЫВ ПОЛОТНА, ЗАПЛАНИРОВАННЫЙ ОСТАНОВ";
                    textbox200.CssClass = "blnktext5";
                }
                else if (break_status == "6")
                {
                    textbox200.Text = "ХМ... ОСТАНОВ МАШИНЫ БОЛЬШЕ ЗАПЛАНИРОВАННОГО";
                    textbox200.CssClass = "blnktext6";
                }
                else if (break_status == "7")
                {
                    textbox200.Text = "БДМ В РАБОТЕ";
                    textbox200.CssClass = "blnktext7";
                }
               // электрики
                if (string.IsNullOrEmpty(Convert.ToString(TekMes_Electro)))
                {
                    textbox_chas_1.InnerText = "0";
                    textbox_min_1.InnerText = "0";
                }
                else
                {
                    textbox_chas_1.InnerText = (Convert.ToInt32(TekMes_Electro) / 60).ToString();
                    textbox_min_1.InnerText = (Convert.ToInt32(TekMes_Electro) - (Convert.ToInt32(textbox_chas_1.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredMes_Electro)))
                {
                    textbox4.InnerText = "0";
                    textbox2.InnerText = "0";
                }
                else
                {
                    textbox4.InnerText = (Convert.ToInt32(PredMes_Electro) / 60).ToString();
                    textbox2.InnerText = (Convert.ToInt32(PredMes_Electro) - (Convert.ToInt32(textbox4.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(tekGod_Electro)))
                {
                    textbox8.InnerText = "0";
                    textbox6.InnerText = "0";
                }
                else
                {
                    if(Convert.ToInt32(temp_tekGod_Electro)> Convert.ToInt32(tekGod_Electro) / 60)
                    {
                      
                        textbox8.InnerText = (Convert.ToInt32(tekGod_Electro) / 60).ToString();
                        textbox6.InnerText = (Convert.ToInt32(tekGod_Electro) - (Convert.ToInt32(textbox8.InnerText) * 60)).ToString();
                    }
                    else
                    {
                        textbox8.InnerText = ((Convert.ToInt32(tekGod_Electro) / 60)- Convert.ToInt32(temp_tekGod_Electro)).ToString();
                        textbox6.InnerText = (Convert.ToInt32(tekGod_Electro) - ((Convert.ToInt32(textbox8.InnerText)+Convert.ToInt32(temp_tekGod_Electro)) * 60)).ToString();
                    }
                   
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredGod_Electro)))
                {
                    textbox12.InnerText = "0";
                    textbox10.InnerText = "0";
                }
                else
                {
                    textbox12.InnerText = (Convert.ToInt32(PredGod_Electro) / 60).ToString();
                    textbox10.InnerText = (Convert.ToInt32(PredGod_Electro) - (Convert.ToInt32(textbox12.InnerText) * 60)).ToString();
                }
                //Механики
                if (string.IsNullOrEmpty(Convert.ToString(TekMes_Mex)))
                {
                    textbox16.InnerText = "0";
                    textbox14.InnerText = "0";
                }
                else
                {
                    textbox16.InnerText = (Convert.ToInt32(TekMes_Mex) / 60).ToString();
                    textbox14.InnerText = (Convert.ToInt32(TekMes_Mex) - (Convert.ToInt32(textbox16.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredMes_Mex)))
                {
                    textbox20.InnerText = "0";
                    textbox18.InnerText = "0";
                }
                else
                {
                    textbox20.InnerText = (Convert.ToInt32(PredMes_Mex) / 60).ToString();
                    textbox18.InnerText = (Convert.ToInt32(PredMes_Mex) - (Convert.ToInt32(textbox20.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(tekGod_Mex)))
                {
                    textbox24.InnerText = "0";
                    textbox22.InnerText = "0";
                }
                else
                {
                    if(Convert.ToInt32(temp_tekGod_Mex)> Convert.ToInt32(tekGod_Mex) / 60)
                    {
                        textbox24.InnerText = (Convert.ToInt32(tekGod_Mex) / 60).ToString();
                        textbox22.InnerText = (Convert.ToInt32(tekGod_Mex) - (Convert.ToInt32(textbox24.InnerText) * 60)).ToString();
                    }
                    else
                    {
                        
                        textbox24.InnerText = ((Convert.ToInt32(tekGod_Mex) / 60)- Convert.ToInt32(temp_tekGod_Mex)).ToString();
                        textbox22.InnerText = (Convert.ToInt32(tekGod_Mex) - ((Convert.ToInt32(textbox24.InnerText)+ Convert.ToInt32(temp_tekGod_Mex)) * 60)).ToString();
                    }
                    
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredGod_Mex)))
                {
                    textbox28.InnerText = "0";
                    textbox26.InnerText = "0";
                }
                else
                {
                    textbox28.InnerText = (Convert.ToInt32(PredGod_Mex) / 60).ToString();
                    textbox26.InnerText = (Convert.ToInt32(PredGod_Mex) - (Convert.ToInt32(textbox28.InnerText) * 60)).ToString();
                }
                //технологи
                if (string.IsNullOrEmpty(Convert.ToString(TekMes_Tex)))
                {
                    textbox32.InnerText = "0";
                    textbox30.InnerText = "0";
                }
                else
                {
                    textbox32.InnerText = (Convert.ToInt32(TekMes_Tex) / 60).ToString();
                    textbox30.InnerText = (Convert.ToInt32(TekMes_Tex) - (Convert.ToInt32(textbox32.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredMes_Tex)))
                {
                    textbox36.InnerText = "0";
                    textbox34.InnerText = "0";
                }
                else
                {
                    textbox36.InnerText = (Convert.ToInt32(PredMes_Tex) / 60).ToString();
                    textbox34.InnerText = (Convert.ToInt32(PredMes_Tex) - (Convert.ToInt32(textbox36.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(tekGod_Tex)))
                {
                    textbox40.InnerText = "0";
                    textbox38.InnerText = "0";
                }
                else
                {
                    if (Convert.ToInt32(temp_tekGod_Tex)> Convert.ToInt32(tekGod_Tex))
                    {
                        textbox40.InnerText = (Convert.ToInt32(tekGod_Tex) / 60).ToString();
                        textbox38.InnerText = (Convert.ToInt32(tekGod_Tex) - (Convert.ToInt32(textbox40.InnerText) * 60)).ToString();
                    }
                    else
                    {
                        textbox40.InnerText = ((Convert.ToInt32(tekGod_Tex) / 60)- Convert.ToInt32(temp_tekGod_Tex)).ToString();
                        textbox38.InnerText = (Convert.ToInt32(tekGod_Tex) - ((Convert.ToInt32(textbox40.InnerText)+ Convert.ToInt32(temp_tekGod_Tex)) * 60)).ToString();
                    }
                   
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredGod_Tex)))
                {
                    textbox44.InnerText = "0";
                    textbox42.InnerText = "0";
                }
                else
                {
                    textbox44.InnerText = (Convert.ToInt32(PredGod_Tex) / 60).ToString();
                    textbox42.InnerText = (Convert.ToInt32(PredGod_Tex) - (Convert.ToInt32(textbox44.InnerText) * 60)).ToString();
                }

                //Плановые работы
                if (string.IsNullOrEmpty(Convert.ToString(TekMes_PPR)))
                {
                    textbox48.InnerText = "0";
                    textbox46.InnerText = "0";
                }
                else
                {
                    textbox48.InnerText = (Convert.ToInt32(TekMes_PPR) / 60).ToString();
                    textbox46.InnerText = (Convert.ToInt32(TekMes_PPR) - (Convert.ToInt32(textbox48.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredMes_PPR)))
                {
                    textbox52.InnerText = "0";
                    textbox50.InnerText = "0";
                }
                else
                {
                    textbox52.InnerText = (Convert.ToInt32(PredMes_PPR) / 60).ToString();
                    textbox50.InnerText = (Convert.ToInt32(PredMes_PPR) - (Convert.ToInt32(textbox52.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(tekGod_PPR)))
                {
                    textbox56.InnerText = "0";
                    textbox54.InnerText = "0";
                }
                else
                {
                    if (Convert.ToInt32(temp_tekGod_PPR)> Convert.ToInt32(tekGod_PPR))
                    {
                        textbox56.InnerText = (Convert.ToInt32(tekGod_PPR) / 60).ToString();
                        textbox54.InnerText = (Convert.ToInt32(tekGod_PPR) - (Convert.ToInt32(textbox56.InnerText) * 60)).ToString();
                    }
                    else
                    {
                        textbox56.InnerText = ((Convert.ToInt32(tekGod_PPR) / 60) - Convert.ToInt32(temp_tekGod_PPR)).ToString();
                        textbox54.InnerText = (Convert.ToInt32(tekGod_PPR) - ((Convert.ToInt32(textbox56.InnerText)+ Convert.ToInt32(temp_tekGod_PPR)) * 60)).ToString();
                    }
                   
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredGod_PPR)))
                {
                    textbox60.InnerText = "0";
                    textbox58.InnerText = "0";
                }
                else
                {
                    textbox60.InnerText = (Convert.ToInt32(PredGod_PPR) / 60).ToString();
                    textbox58.InnerText = (Convert.ToInt32(PredGod_PPR) - (Convert.ToInt32(textbox60.InnerText) * 60)).ToString();
                }

                //Прочее
                if (string.IsNullOrEmpty(Convert.ToString(TekMes_Other)))
                {
                    textbox64.InnerText = "0";
                    textbox62.InnerText = "0";
                }
                else
                {
                    textbox64.InnerText = (Convert.ToInt32(TekMes_Other) / 60).ToString();
                    textbox62.InnerText = (Convert.ToInt32(TekMes_Other) - (Convert.ToInt32(textbox64.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredMes_Other)))
                {
                    textbox68.InnerText = "0";
                    textbox66.InnerText = "0";
                }
                else
                {
                    textbox68.InnerText = (Convert.ToInt32(PredMes_Other) / 60).ToString();
                    textbox66.InnerText = (Convert.ToInt32(PredMes_Other) - (Convert.ToInt32(textbox68.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(tekGod_Other)))
                {
                    textbox72.InnerText = "0";
                    textbox70.InnerText = "0";
                }
                else
                {
                    textbox72.InnerText = (Convert.ToInt32(tekGod_Other) / 60).ToString();
                    textbox70.InnerText = (Convert.ToInt32(tekGod_Other) - (Convert.ToInt32(textbox72.InnerText) * 60)).ToString();
                }
                if (string.IsNullOrEmpty(Convert.ToString(PredGod_Other)))
                {
                    textbox76.InnerText = "0";
                    textbox74.InnerText = "0";
                }
                else
                {
                    textbox76.InnerText = (Convert.ToInt32(PredGod_Other) / 60).ToString();
                    textbox74.InnerText = (Convert.ToInt32(PredGod_Other) - (Convert.ToInt32(textbox76.InnerText) * 60)).ToString();
                }
                //Выбор цвета
                if (Convert.ToInt32(textbox_chas_1.InnerText) > 10 || (Convert.ToInt32(textbox_chas_1.InnerText) == 10 && Convert.ToInt32(textbox_min_1.InnerText) > 0))
                {
                    Electro_smail.Attributes["class"] = "layer_Stag_red";
                    //textbox_chas_1.Attributes["class"] = "textbox_prost_chas";
                }
                //else if (Convert.ToInt32(textbox_chas_1.Text)> Convert.ToInt32(textbox4.Text)|| (Convert.ToInt32(textbox8.Text) > Convert.ToInt32(textbox12.Text)))
                //{
                //    Electro_smail.Attributes["class"] = "layer_elec_yellow";
                //}
                else
                {

                    Electro_smail.Attributes["class"] = "layer_Stag";

                    //textbox_chas_1.Attributes["class"] = "textbox_prost_chas_red";

                };
                //Выбор цвета для механиков
                if (Convert.ToInt32(textbox16.InnerText) > 10 || (Convert.ToInt32(textbox16.InnerText) == 10 && Convert.ToInt32(textbox14.InnerText) > 0))
                {
                    Mex_smail.Attributes["class"] = "layer_Stag_red";
                }
                else
                {
                    Mex_smail.Attributes["class"] = "layer_Stag";
                };
                //Выбор цвета для технологов
                if (Convert.ToInt32(textbox32.InnerText) > 10 || (Convert.ToInt32(textbox32.InnerText) == 10 && Convert.ToInt32(textbox30.InnerText) > 0))
                {
                    Tex_smail.Attributes["class"] = "layer_Stag_red";
                }
                else
                {
                    Tex_smail.Attributes["class"] = "layer_Stag";
                };

                if (Count_All_TekMes == 0)
                {
                    Th1.InnerText = "0";
                    Th2.InnerText = "0";
                }
                else
                {
                    Th1.InnerText = (Count_All_TekMes / 60).ToString();
                    Th2.InnerText = (Count_All_TekMes - (Convert.ToInt32(Th1.InnerText) * 60)).ToString();
                }
                if (Count_All_PredMes == 0)
                {
                    Th3.InnerText = "0";
                    Th4.InnerText = "0";
                }
                else
                {
                    Th3.InnerText = (Count_All_PredMes / 60).ToString();
                    Th4.InnerText = (Count_All_PredMes - (Convert.ToInt32(Th3.InnerText) * 60)).ToString();
                }

                if (Count_All_tekGod == 0)
                {
                    Th5.InnerText = "0";
                    Th6.InnerText = "0";
                }
                else
                {
                    Th5.InnerText = (Count_All_tekGod / 60).ToString();
                    Th6.InnerText = (Count_All_tekGod - (Convert.ToInt32(Th5.InnerText) * 60)).ToString();
                }
                if (Count_All_PredGod== 0)
                {
                    Th7.InnerText = "0";
                    Th8.InnerText = "0";
                }
                else
                {
                    Th7.InnerText = (Count_All_PredGod / 60).ToString();
                    Th8.InnerText = (Count_All_PredGod - (Convert.ToInt32(Th7.InnerText) * 60)).ToString();
                }

            }
            catch
            {

            }

        }
       
    }
   
}