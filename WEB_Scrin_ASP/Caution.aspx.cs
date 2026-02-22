using System;

namespace WEB_Scrin_ASP
{
    public partial class Caution : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
    }
}