<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Grafik.aspx.cs" Inherits="WEB_Scrin_ASP.Grafik" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="refresh" content="65"/>
</asp:Content>

<asp:Content ContentPlaceHolderID="Content" runat="server">
    <!-- СТАТУСНАЯ СТРОКА БДМ (добавлено) -->
    <div class="status-bar">
        <asp:TextBox ID="textboxStatus" CssClass="status-text" runat="server" ReadOnly="True" />
    </div>

    <div class="DN">
        <asp:Label ID="Label_Grafik" runat="server" CssClass="label_DN" Text="" />
    </div>
    
    <div style="height:100%; width:100%; overflow:scroll;">
        <div class="label_mes">
            <asp:Label ID="Label1" CssClass="label_mes_txt" runat="server" Text="Начальники смен" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView1" runat="server" OnRowDataBound="GridView1_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label2" CssClass="label_mes_txt" runat="server" Text="Машинисты" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView2" runat="server" OnRowDataBound="GridView2_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label3" CssClass="label_mes_txt" runat="server" Text="Старшие сушильщики" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView3" runat="server" OnRowDataBound="GridView3_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label4" CssClass="label_mes_txt" runat="server" Text="Сушильщики" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView4" runat="server" OnRowDataBound="GridView4_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label5" CssClass="label_mes_txt" runat="server" Text="Резчики" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView5" runat="server" OnRowDataBound="GridView5_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label6" CssClass="label_mes_txt" runat="server" Text="Операторы РПО" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView6" runat="server" OnRowDataBound="GridView6_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label7" CssClass="label_mes_txt" runat="server" Text="Дежурные электрики" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView7" runat="server" OnRowDataBound="GridView7_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label8" CssClass="label_mes_txt" runat="server" Text="Дежурные слесари" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView8" runat="server" OnRowDataBound="GridView8_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label10" CssClass="label_mes_txt" runat="server" Text="Операторы бобинореза" />
        </div>
        <asp:GridView CssClass="table_dark" ID="GridView9" runat="server" OnRowDataBound="GridView9_OnRowDataBound" />

        <div class="label_mes">
            <asp:Label ID="Label9" CssClass="label_mes_txt" runat="server" />
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="60000" />
    </div>
</asp:Content>