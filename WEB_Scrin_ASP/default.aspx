<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB_Scrin_ASP.Default" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="content_Mod">
        <!-- Статусная строка -->
        <div class="status_mod">
            <asp:TextBox ID="textbox46" Class="textbox_break_Mod" runat="server" ReadOnly="True" />
        </div>

        <!-- Две колонки под статусом -->
        <div class="columns">
            <!-- Левая колонка (layer1) -->
            <div class="layer1">
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label3" CssClass="label_Ri" runat="server">Фактическая скорость сетки.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label5" CssClass="label_Ri" runat="server">Фактическая скорость наката.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox5" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label6" CssClass="label_Ri" runat="server">Заданный граммаж.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox7" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label9" CssClass="label_Ri" runat="server">Фактический граммаж.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox13" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label7" CssClass="label_Ri" runat="server">Кол-во обрывов за тек. смену.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox9" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label8" CssClass="label_Ri" runat="server">Кол-во обрывов за пред. смену.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox11" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label12" CssClass="label_Ri" runat="server">Заданный план на тек.смену.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox27" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1L"><asp:Label ID="Label16" CssClass="label_Ri" runat="server">Выпол-ный план на тек.смену.</asp:Label></div>
                    <div class="col-1-2L"><asp:TextBox ID="textbox29" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1La"><asp:Label ID="Label1" CssClass="label_Ri" runat="server">Заданный план на тек.год.</asp:Label></div>
                    <div class="col-1-2La"><asp:TextBox ID="textbox3" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1La"><asp:Label ID="Label2" CssClass="label_Ri" runat="server">Выполненный план на тек. год.</asp:Label></div>
                    <div class="col-1-2La"><asp:TextBox ID="textbox21" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
            </div>

            <!-- Правая колонка (layer2) -->
            <div class="layer2">
                <div class="row_Ri">
                    <div class="col-1-1Li"><asp:Label ID="Label20" CssClass="label_Li" runat="server">Выполненный план на пред. год.</asp:Label></div>
                    <div class="col-1-2Li"><asp:TextBox ID="textbox16" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1Li"><asp:Label ID="Label4" CssClass="label_Li" runat="server">Отклонение от плана на тек. год.</asp:Label></div>
                    <div class="col-1-2Li"><asp:TextBox ID="textbox15" Class="textbox_otkl_red" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1Li"><asp:Label ID="Label10" CssClass="label_Li" runat="server">Заданный план на тек.месяц.</asp:Label></div>
                    <div class="col-1-2Li"><asp:TextBox ID="textbox23" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1Li"><asp:Label ID="Label13" CssClass="label_Li" runat="server">Выполненный план на тек.месяц.</asp:Label></div>
                    <div class="col-1-2Li"><asp:TextBox ID="textbox25" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-1-1Li"><asp:Label ID="Label14" CssClass="label_Li" runat="server">Отклонение от плана на тек.месяц.</asp:Label></div>
                    <div class="col-1-2Li"><asp:TextBox ID="textbox19" Class="textbox_otkl_red" runat="server" ReadOnly="True" /></div>
                </div>

                <!-- Блок работы БДМ (объединённое время) -->
                <div class="row_Ri">
                    <div class="col-2-1Li"><asp:Label ID="Label11" CssClass="label_Li" runat="server">Работа БДМ тек.год.</asp:Label></div>
                    <div class="col-2-time"><asp:TextBox ID="textbox100" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-2-1Li"><asp:Label ID="Label15" CssClass="label_Li" runat="server">Простой БДМ тек.год.</asp:Label></div>
                    <div class="col-2-time"><asp:TextBox ID="textbox33" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-2-1Li"><asp:Label ID="Label17" CssClass="label_Li" runat="server">Простой БДМ тек.месяц.</asp:Label></div>
                    <div class="col-2-time"><asp:TextBox ID="textbox37" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-2-1Li"><asp:Label ID="Label18" CssClass="label_Li" runat="server">Простой БДМ тек.смену.</asp:Label></div>
                    <div class="col-2-time"><asp:TextBox ID="textbox41" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
                <div class="row_Ri">
                    <div class="col-2-1Li"><asp:Label ID="Label19" CssClass="label_Li" runat="server">Простой БДМ пред. смену.</asp:Label></div>
                    <div class="col-2-time"><asp:TextBox ID="textbox45" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                </div>
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="30000" />
</asp:Content>