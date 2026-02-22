using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB_Scrin_ASP
{
    public partial class birthday : System.Web.UI.Page
    {
        string mes_rod;
        protected void Page_Load(object sender, EventArgs e)
        {
            // блокировка по году 2022г
            if (DateTime.Now.Year < 2025)
            {
                // нужны дата и время чтоб делать выборки
                DateTime date = DateTime.Now;

                // для выборки текущий месяц
                int month = date.Month;

                switch (month)
                {
                    case 1:
                        mes_rod = "Января";
                        Label1.Text = "Дни рождения сотрудников в январе месяце ";
                        break;
                    case 2:
                        mes_rod = "Февраля";
                        Label1.Text = "Дни рождения сотрудников в феврале месяце ";
                        break;
                    case 3:
                        mes_rod = "Марта";
                        Label1.Text = "Дни рождения сотрудников в марте месяце ";
                        break;
                    case 4:
                        mes_rod = "Апреля";
                        Label1.Text = "Дни рождения сотрудников в апреле месяце ";
                        break;
                    case 5:
                        mes_rod = "Мая";
                        Label1.Text = "Дни рождения сотрудников в мае месяце ";
                        break;
                    case 6:
                        mes_rod = "Июня";
                        Label1.Text = "Дни рождения сотрудников в июне месяце ";
                        break;
                    case 7:
                        mes_rod = "Июля";
                        Label1.Text = "Дни рождения сотрудников в июле месяце ";
                        break;
                    case 8:
                        mes_rod = "Августа";
                        Label1.Text = "Дни рождения сотрудников в августе месяце ";
                        break;
                    case 9:
                        mes_rod = "Сентября";
                        Label1.Text = "Дни рождения сотрудников в сентябре месяце ";
                        break;
                    case 10:
                        mes_rod = "Октября";
                        Label1.Text = "Дни рождения сотрудников в октябре месяце ";
                        break;
                    case 11:
                        mes_rod = "Ноября";
                        Label1.Text = "Дни рождения сотрудников в ноябре месяце ";
                        break;
                    case 12:
                        mes_rod = "Декабря";
                        Label1.Text = "Дни рождения сотрудников в декабре месяце ";
                        break;
                }
                try
                {
                    string connectionString = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                    string sqlExpression = "SELECT*FROM[Control].[dbo].[DOB]where MONTH(db)= @mes order by DAY(db) asc";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.AddWithValue("@mes", month);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows) // если есть данные
                        {
                            int n = 1;
                            while (reader.Read()) // построчно считываем данные
                            {
                                object Date_FIO = reader.GetValue(2);
                                object Date_Post = reader.GetValue(3);
                                object Date_DB = reader.GetValue(4);
                                //textbox19.Text = String.Format("{0:0,0}", otkl_mes);
                                //textbox15.Text = String.Format("{0:0,0}", otkl_god);

                                if (n == 1)
                                {
                                    Label2.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 2)
                                {
                                    Label3.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 3)
                                {
                                    Label4.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 4)
                                {
                                    Label5.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 5)
                                {
                                    Label6.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 6)
                                {
                                    Label7.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }

                                if (n == 7)
                                {
                                    Label8.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 8)
                                {
                                    Label9.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 9)
                                {
                                    Label10.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 10)
                                {
                                    Label11.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 11)
                                {
                                    Label12.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 12)
                                {
                                    Label13.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }

                                if (n == 13)
                                {
                                    Label14.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 14)
                                {
                                    Label15.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 15)
                                {
                                    Label16.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 16)
                                {
                                    Label17.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 17)
                                {
                                    Label18.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 18)
                                {
                                    Label19.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }

                                if (n == 19)
                                {
                                    Label20.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 20)
                                {
                                    Label21.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 21)
                                {
                                    Label22.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
                                }
                                if (n == 22)
                                {
                                    Label23.Text = Date_FIO + " - " + (DateTime.Parse((Date_DB.ToString())).Day).ToString() + " " + mes_rod;
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
            }

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}