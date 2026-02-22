using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;

namespace WEB_Scrin_ASP
{
    public partial class idle_time : BasePage
    {
        // Переменные для хранения данных из БД (минуты)
        object TekMes_Electro, PredMes_Electro, tekGod_Electro, PredGod_Electro;
        object TekMes_Mex, PredMes_Mex, tekGod_Mex, PredGod_Mex;
        object TekMes_Tex, PredMes_Tex, tekGod_Tex, PredGod_Tex;
        object TekMes_PPR, PredMes_PPR, tekGod_PPR, PredGod_PPR;
        object TekMes_Other, PredMes_Other, tekGod_Other, PredGod_Other;
        object temp_tekGod_Electro, temp_tekGod_Mex, temp_tekGod_Tex, temp_tekGod_PPR, temp_tekGod_Other;
        int Count_All_TekMes, Count_All_PredMes, Count_All_tekGod, Count_All_PredGod;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBreakStatus(); // из BasePage

            if (DateTime.Now.Year < 2050)
            {
                Page.Server.ScriptTimeout = 65;

                DateTime date = DateTime.Now;
                int year = date.Year;
                string month = date.ToString("MM", CultureInfo.InvariantCulture);
                string den = date.ToString("dd", CultureInfo.InvariantCulture);
                string time_tek_start = year + month + "01 00:00:00";
                string time_tek_end = year + month + den + " 23:59:59";

                DateTime date1 = date.AddMonths(-1);
                int year_pred = date1.Year;
                string month_pred = date1.ToString("MM", CultureInfo.InvariantCulture);
                string days_Mo = DateTime.DaysInMonth(year_pred, Convert.ToInt32(month_pred)).ToString();
                string time_pred_start = year_pred + month_pred + "01 00:00:00";
                string time_pred_end = year_pred + month_pred + days_Mo + " 23:59:59";

                string time_tekGod_start = year + "0101 00:00:00";
                string time_tekGod_end = year + month + den + " 23:59:59";

                DateTime date2 = date.AddYears(-1);
                int year_predGod = date2.Year;
                string time_predGod_start = year_predGod + "0101 00:00:00";
                string time_predGod_end = year_predGod + month + den + " 23:59:59";

                try
                {
                    string connectionString = @"Data Source=10.0.9.4;Initial Catalog=kiu_opc;User ID=mes;Password=mes";
                    string sqlExpression = @"
                        SET ANSI_NULLS OFF
                        SELECT
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104)) AS TekMes_Electro,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104)) AS PredMes_Electro,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekGod_end, 104)) AS tekGod_Electro,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104)) AS PredGod_Electro,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Мех.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104)) AS TekMes_Mex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Мех.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104)) AS PredMes_Mex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Мех.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekGod_end, 104)) AS tekGod_Mex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр1 IN('Мех.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104)) AS PredGod_Mex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр2 IN('Технологическая') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104)) AS TekMes_Tex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр2 IN('Технологическая') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104)) AS PredMes_Tex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр2 IN('Технологическая') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekGod_end, 104)) AS tekGod_Tex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE ПричинаУр2 IN('Технологическая') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104)) AS PredGod_Tex,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина IN('ПТО', 'ППР, останов на ППР','ППР') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104)) AS TekMes_PPR,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина IN('ПТО', 'ППР, останов на ППР','ППР') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104)) AS PredMes_PPR,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина IN('ПТО', 'ППР, останов на ППР','ППР') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekGod_end, 104)) AS tekGod_PPR,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина IN('ПТО', 'ППР, останов на ППР','ППР') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104)) AS PredGod_PPR,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина NOT IN('ПТО','ППР, останов на ППР','ППР') AND ПричинаУр2 NOT IN('Технологическая') AND ПричинаУр1 NOT IN('Мех.часть','Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekMes_start, 104) AND CONVERT(datetime, @time_tekMes_end, 104)) AS TekMes_Other,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина NOT IN('ПТО','ППР, останов на ППР','ППР') AND ПричинаУр2 NOT IN('Технологическая') AND ПричинаУр1 NOT IN('Мех.часть','Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predMes_start, 104) AND CONVERT(datetime, @time_predMes_end, 104)) AS PredMes_Other,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина NOT IN('ПТО','ППР, останов на ППР','ППР') AND ПричинаУр2 NOT IN('Технологическая') AND ПричинаУр1 NOT IN('Мех.часть','Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_tekGod_start, 104) AND CONVERT(datetime, @time_tekGod_end, 104)) AS tekGod_Other,
                            (SELECT SUM(Продолжительность) FROM [kiu_opc].[dbo].[Простои] WHERE Причина NOT IN('ПТО','ППР, останов на ППР','ППР') AND ПричинаУр2 NOT IN('Технологическая') AND ПричинаУр1 NOT IN('Мех.часть','Эл.часть') AND [ДатаДокумента] BETWEEN CONVERT(datetime, @time_predGod_start, 104) AND CONVERT(datetime, @time_predGod_end, 104)) AS PredGod_Other
                        SET ANSI_NULLS ON";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.AddWithValue("@time_tekMes_start", time_tek_start);
                        command.Parameters.AddWithValue("@time_tekMes_end", time_tek_end);
                        command.Parameters.AddWithValue("@time_predMes_start", time_pred_start);
                        command.Parameters.AddWithValue("@time_predMes_end", time_pred_end);
                        command.Parameters.AddWithValue("@time_tekGod_start", time_tekGod_start);
                        command.Parameters.AddWithValue("@time_tekGod_end", time_tekGod_end);
                        command.Parameters.AddWithValue("@time_predGod_start", time_predGod_start);
                        command.Parameters.AddWithValue("@time_predGod_end", time_predGod_end);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
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

                                Count_All_TekMes = TekMes_Electro.IfNullThenZero() + TekMes_Mex.IfNullThenZero() + TekMes_Tex.IfNullThenZero() + TekMes_PPR.IfNullThenZero() + TekMes_Other.IfNullThenZero();
                                Count_All_PredMes = PredMes_Electro.IfNullThenZero() + PredMes_Mex.IfNullThenZero() + PredMes_Tex.IfNullThenZero() + PredMes_PPR.IfNullThenZero() + PredMes_Other.IfNullThenZero();
                                Count_All_tekGod = tekGod_Electro.IfNullThenZero() + tekGod_Mex.IfNullThenZero() + tekGod_Tex.IfNullThenZero() + tekGod_PPR.IfNullThenZero() + tekGod_Other.IfNullThenZero();
                                Count_All_PredGod = PredGod_Electro.IfNullThenZero() + PredGod_Mex.IfNullThenZero() + PredGod_Tex.IfNullThenZero() + PredGod_PPR.IfNullThenZero() + PredGod_Other.IfNullThenZero();
                            }
                        }
                        reader.Close();
                    }

                    // Данные корректировки (difference)
                    string connectionString1 = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                    string sqlExpression1 = "SELECT * FROM [Control].[dbo].[difference] WHERE id=1";
                    using (SqlConnection connection = new SqlConnection(connectionString1))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression1, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
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
                }
                catch (Exception ex)
                {
                    // логирование при необходимости
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
                // Обновляем статусную строку (из BasePage)
                UpdateStatusTextBox(textbox200);

                // Вспомогательная функция форматирования минут в "X ч Y мин"
                string FormatTime(object minutesObj)
                {
                    int minutes = minutesObj.IfNullThenZero();
                    int hours = minutes / 60;
                    int mins = minutes % 60;
                    return $"{hours} ч {mins} мин";
                }

                // Электрики
                lblElecTekMes.Text = FormatTime(TekMes_Electro);
                lblElecPredMes.Text = FormatTime(PredMes_Electro);
                // Для текущего года с учётом корректировки
                int tekGodElec = tekGod_Electro.IfNullThenZero();
                int tempGodElec = temp_tekGod_Electro.IfNullThenZero();
                if (tempGodElec > tekGodElec / 60) // старая логика: если коррекция больше часов
                {
                    lblElecTekGod.Text = FormatTime(tekGodElec);
                }
                else
                {
                    int adjusted = tekGodElec - tempGodElec * 60;
                    lblElecTekGod.Text = FormatTime(adjusted);
                }
                lblElecPredGod.Text = FormatTime(PredGod_Electro);

                // Механики
                lblMexTekMes.Text = FormatTime(TekMes_Mex);
                lblMexPredMes.Text = FormatTime(PredMes_Mex);
                int tekGodMex = tekGod_Mex.IfNullThenZero();
                int tempGodMex = temp_tekGod_Mex.IfNullThenZero();
                if (tempGodMex > tekGodMex / 60)
                    lblMexTekGod.Text = FormatTime(tekGodMex);
                else
                    lblMexTekGod.Text = FormatTime(tekGodMex - tempGodMex * 60);
                lblMexPredGod.Text = FormatTime(PredGod_Mex);

                // Технологи
                lblTexTekMes.Text = FormatTime(TekMes_Tex);
                lblTexPredMes.Text = FormatTime(PredMes_Tex);
                int tekGodTex = tekGod_Tex.IfNullThenZero();
                int tempGodTex = temp_tekGod_Tex.IfNullThenZero();
                if (tempGodTex > tekGodTex / 60)
                    lblTexTekGod.Text = FormatTime(tekGodTex);
                else
                    lblTexTekGod.Text = FormatTime(tekGodTex - tempGodTex * 60);
                lblTexPredGod.Text = FormatTime(PredGod_Tex);

                // Плановые (ППР)
                lblPprTekMes.Text = FormatTime(TekMes_PPR);
                lblPprPredMes.Text = FormatTime(PredMes_PPR);
                int tekGodPpr = tekGod_PPR.IfNullThenZero();
                int tempGodPpr = temp_tekGod_PPR.IfNullThenZero();
                if (tempGodPpr > tekGodPpr / 60)
                    lblPprTekGod.Text = FormatTime(tekGodPpr);
                else
                    lblPprTekGod.Text = FormatTime(tekGodPpr - tempGodPpr * 60);
                lblPprPredGod.Text = FormatTime(PredGod_PPR);

                // Прочее
                lblOtherTekMes.Text = FormatTime(TekMes_Other);
                lblOtherPredMes.Text = FormatTime(PredMes_Other);
                lblOtherTekGod.Text = FormatTime(tekGod_Other);
                lblOtherPredGod.Text = FormatTime(PredGod_Other);

                // Общее
                lblTotalTekMes.Text = FormatTime(Count_All_TekMes);
                lblTotalPredMes.Text = FormatTime(Count_All_PredMes);
                lblTotalTekGod.Text = FormatTime(Count_All_tekGod);
                lblTotalPredGod.Text = FormatTime(Count_All_PredGod);

                // Цветовая индикация блоков (если простои за текущий месяц > 10 часов)
                if (TekMes_Electro.IfNullThenZero() > 10 * 60)
                    Electro_smail.Attributes["class"] = "idle-card card-warning";
                else
                    Electro_smail.Attributes["class"] = "idle-card";

                if (TekMes_Mex.IfNullThenZero() > 10 * 60)
                    Mex_smail.Attributes["class"] = "idle-card card-warning";
                else
                    Mex_smail.Attributes["class"] = "idle-card";

                if (TekMes_Tex.IfNullThenZero() > 10 * 60)
                    Tex_smail.Attributes["class"] = "idle-card card-warning";
                else
                    Tex_smail.Attributes["class"] = "idle-card";
            }
            catch
            {
                // Игнорируем ошибки в LoadComplete
            }
        }
    }
}