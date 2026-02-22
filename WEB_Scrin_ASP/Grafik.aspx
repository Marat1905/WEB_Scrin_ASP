<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Grafik.aspx.cs" Inherits="WEB_Scrin_ASP.Grafik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!-- Место отображения элементов, цель которых — помочь браузеру в работе с данными. Здесь могут находиться метатеги, используемые для хранения информации предназначенной для браузеров и поисковых систем. Каждая дочерняя страница может размещать свою индивидуальную информацию между тегами с идентификатором
        ID="head" -->
    <meta http-equiv="refresh" content="65"/>
    <%--<meta http-equiv="refresh" content="3; url=">--%>
   
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" runat="server">

     <div class="DN">
            <%-- --%>
        <asp:Label ID="Label_Grafik" runat="server" CssClass="label_DN" Text=""> </asp:Label>
              </div>
    <div style="height:100%;width:100%;overflow:scroll;">

         <div class="label_mes">
                   <asp:Label ID="Label1" Class=" label_mes_txt" runat="server" Text="Label">Начальники смен</asp:Label>
                   </div>

        <asp:GridView class="table_dark" ID ="GridView1" runat="server" OnRowDataBound = "GridView1_OnRowDataBound">
             
        </asp:GridView>

         <div class="label_mes">
                   <asp:Label ID="Label2" Class=" label_mes_txt" runat="server" Text="Label">Машинисты</asp:Label>
                   </div>
        <asp:GridView class="table_dark" ID ="GridView2" runat="server" OnRowDataBound = "GridView2_OnRowDataBound" >
             
        </asp:GridView>

         <div class="label_mes">
                   <asp:Label ID="Label3" Class=" label_mes_txt" runat="server" Text="Label">Старшие сушильщики</asp:Label>
          </div>
        <asp:GridView class="table_dark" ID ="GridView3" runat="server" OnRowDataBound = "GridView3_OnRowDataBound" >
             
        </asp:GridView>

          <div class="label_mes">
                   <asp:Label ID="Label4" Class=" label_mes_txt" runat="server" Text="Label">Cушильщики</asp:Label>
          </div>
        <asp:GridView class="table_dark" ID ="GridView4" runat="server" OnRowDataBound = "GridView4_OnRowDataBound">
             
        </asp:GridView>

         <div class="label_mes">
                   <asp:Label ID="Label5" Class=" label_mes_txt" runat="server" Text="Label">Резчики</asp:Label>
          </div>
        <asp:GridView class="table_dark" ID ="GridView5" runat="server" OnRowDataBound = "GridView5_OnRowDataBound">
             
        </asp:GridView>

         <div class="label_mes">
                   <asp:Label ID="Label6" Class=" label_mes_txt" runat="server" Text="Label">Операторы РПО</asp:Label>
          </div>
        <asp:GridView class="table_dark" ID ="GridView6" runat="server" OnRowDataBound = "GridView6_OnRowDataBound">
             
        </asp:GridView>

           <div class="label_mes">
                   <asp:Label ID="Label7" Class=" label_mes_txt" runat="server" Text="Label">Дежурные электрики</asp:Label>
          </div>
        <asp:GridView class="table_dark" ID ="GridView7" runat="server"  OnRowDataBound = "GridView7_OnRowDataBound">
             
        </asp:GridView>

          <div class="label_mes">
                   <asp:Label ID="Label8" Class=" label_mes_txt" runat="server" Text="Label">Дежурные слесари</asp:Label>
          </div>
        <asp:GridView class="table_dark" ID ="GridView8" runat="server" OnRowDataBound = "GridView8_OnRowDataBound">
             
        </asp:GridView>

         <div class="label_mes">
                   <asp:Label ID="Label10" Class=" label_mes_txt" runat="server" Text="Label">Операторы бобинореза</asp:Label>
          </div>
        <asp:GridView class="table_dark" ID ="GridView9" runat="server" OnRowDataBound = "GridView9_OnRowDataBound">
             
        </asp:GridView>

          <div class="label_mes">
                   <asp:Label ID="Label9" Class=" label_mes_txt" runat="server" > </asp:Label>
          </div>

    </div>

    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="60000">
            </asp:Timer>
        </div>
 
   


</asp:Content>

