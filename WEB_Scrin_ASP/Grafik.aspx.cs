using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB_Scrin_ASP
{
    public partial class Grafik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // блокировка по дате
            if (DateTime.Now.Year < 2025)
            {
                // нужны дата и время чтоб делать выборки
                DateTime date = DateTime.Now;

                // для выборки текущий месяц
                int month = date.Month;
                try
                {
                    switch (month)
                    {
                        case 1:

                            Label_Grafik.Text = "График работы персонала на январь месяц.";
                            break;
                        case 2:
                            Label_Grafik.Text = "График работы персонала на февраль месяц.";
                            break;
                        case 3:
                            Label_Grafik.Text = "График работы персонала на март месяц.";
                            break;
                        case 4:
                            Label_Grafik.Text = "График работы персонала на апрель месяц.";
                            break;
                        case 5:
                            Label_Grafik.Text = "График работы персонала на май месяц.";
                            break;
                        case 6:
                            Label_Grafik.Text = "График работы персонала на июнь месяц.";
                            break;
                        case 7:
                            Label_Grafik.Text = "График работы персонала на июль месяц.";
                            break;
                        case 8:
                            Label_Grafik.Text = "График работы персонала на август месяц.";
                            break;
                        case 9:
                            Label_Grafik.Text = "График работы персонала на сентябрь месяц.";
                            break;
                        case 10:
                            Label_Grafik.Text = "График работы персонала на октябрь месяц.";
                            break;
                        case 11:
                            Label_Grafik.Text = "График работы персонала на ноябрь месяц.";
                            break;
                        case 12:
                            Label_Grafik.Text = "График работы персонала на декабрь месяц.";
                            break;
                    }

                }
                catch (Exception)
                {


                }



                // для выборки текущий месяц
                int year = date.Year;
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
                string month_propis = date.ToString("MMMM");
                int i;
                string text = "";
                string breakTxT = "";


                /////////////////////////////////////////
                // читаем данные с sql машинисты
                try
                {

                    for (i = 1; i < 10; i++)
                    {
                        try
                        {
                            string dolnost = "";
                            switch (i)
                            {
                                case 1:
                                    dolnost = "ст. мастер смены";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ISNULL(t3.Breake, 0) as 'Обрывы'";
                                    breakTxT = ",(SUM(CAST(SYX AS int)) + SUM(CAST(MOKR AS int))) as Breake";
                                    break;
                                case 2:
                                    dolnost = "машинист";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ',ISNULL(t3.Breake, 0) as 'Обрывы'";
                                    breakTxT = ",SUM(CAST(MOKR AS int))as Breake";
                                    break;
                                case 3:
                                    dolnost = "сушильщик 4 р.";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ',ISNULL(t3.Breake, 0) as 'Обрывы'";
                                    breakTxT = ",SUM(CAST(SYX AS int))as Breake";
                                    break;
                                case 4:
                                    dolnost = "сушильщик 3 р";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ',ISNULL(t3.Breake, 0) as 'Обрывы'";
                                    breakTxT = ",SUM(CAST(SYX AS int))as Breake";
                                    break;
                                case 5:
                                    dolnost = "резчик 4 р";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ'";
                                    break;
                                case 6:
                                    dolnost = "оператор РПО";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ'";
                                    break;
                                case 7:
                                    dolnost = "дежурный электрик";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ'";
                                    break;
                                case 8:
                                    dolnost = "дежурный слесарь";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ'";
                                    break;
                                case 9:
                                    dolnost = "Оператор БС";
                                    text = "t3.FIO as 'ФИО',ISNULL(t3.Smen, 0) as 'Смен',ROUND(t3.KTY, 2) as 'КТУ'";
                                    break;

                            }
                            string connectionString = @"Data Source=NICOLPAK\WINCC;Initial Catalog=Control;Integrated Security=True";
                            string sqlExpression = "select " + text + ",t3.[1],t3.[2],t3.[3]" +
            ",t3.[4],t3.[5],t3.[6],t3.[7],t3.[8],t3.[9],t3.[10],t3.[11],t3.[12],t3.[13],t3.[14],t3.[15]" +
            ",t3.[16],t3.[17],t3.[18],t3.[19],t3.[20],t3.[21],t3.[22],t3.[23],t3.[24],t3.[25],t3.[26],t3.[27]" +
            ", t3.[28],t3.[29],t3.[30],t3.[31] " +
            "from( " +
            "SELECT * " +
            "FROM " +
             " ( SELECT *" +
             " FROM [Control].[dbo].[Grafik] " +
             " where month like N'%" + Convert.ToString(month_propis + "_" + year) + "%'" +
              " ) T1" +
            " LEFT OUTER JOIN " +
              "(SELECT  FIO as 'FIO1', COUNT(FIO) AS Smen, AVG(CAST( REPLACE(KTY, ',','.') AS float))as KTY" + breakTxT + " , MAX(DATA_TIME) as DATA_TIME " +
              "FROM [Control].[dbo].[KTY] " +
            " where MONTH like N'%" + Convert.ToString(month_propis + "_" + year) + "%' " +
             " group by FIO " +
             "  ) T2" +
            " on T2.FIO1 = T1.FIO " +
            ")t3 " +
            " where T3.dolnost like N'%" + dolnost + "%' " +
            " order by T3.ID asc";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = new SqlCommand(sqlExpression, connection);
                                command.Parameters.AddWithValue("@month", Convert.ToString(month_propis + "_" + year));
                                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                                var dataTable = new DataTable();
                                dataTable.Load(reader);
                                reader.Close();


                                switch (i)
                                {
                                    case 1:
                                        GridView1.DataSource = dataTable;
                                        GridView1.DataBind();
                                        break;
                                    case 2:
                                        GridView2.DataSource = dataTable;
                                        GridView2.DataBind();
                                        break;
                                    case 3:
                                        GridView3.DataSource = dataTable;
                                        GridView3.DataBind();
                                        break;
                                    case 4:
                                        GridView4.DataSource = dataTable;
                                        GridView4.DataBind();
                                        break;
                                    case 5:
                                        GridView5.DataSource = dataTable;
                                        GridView5.DataBind();
                                        break;
                                    case 6:
                                        GridView6.DataSource = dataTable;
                                        GridView6.DataBind();
                                        break;
                                    case 7:
                                        GridView7.DataSource = dataTable;
                                        GridView7.DataBind();
                                        break;
                                    case 8:
                                        GridView8.DataSource = dataTable;
                                        GridView8.DataBind();
                                        break;
                                    case 9:
                                        GridView9.DataSource = dataTable;
                                        GridView9.DataBind();
                                        break;
                                }
                            }

                        }
                        catch
                        {

                        }
                    }

                }
                catch (Exception ex)
                {

                }
                //GridView1.DataSource = dataTable;
                //GridView1.DataBind();

            }

        }
         //подсвечиваем столбец по дате
        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 2];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        protected void GridView2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 3];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        protected void GridView3_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 3];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        protected void GridView4_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 3];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        protected void GridView5_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 2];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        protected void GridView6_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 2];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        protected void GridView7_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 2];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }
        }
        protected void GridView8_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 2];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {

               
            }

           
        }

        protected void GridView9_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime date = DateTime.Now;
                    int day = date.Day;
                    TableCell cell = e.Row.Cells[day + 2];
                    if (cell.Text != "")
                    {
                        cell.BackColor = Color.Blue;
                    }
                }
            }
            catch (Exception)
            {


            }


        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}