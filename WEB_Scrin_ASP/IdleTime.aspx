<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="IdleTime.aspx.cs" Inherits="WEB_Scrin_ASP.IdleTime" Async="true" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="idle-dashboard">
        <!-- Статусная строка -->
        <div class="status-bar">
            <asp:TextBox ID="textbox200" CssClass="status-text" runat="server" ReadOnly="True" />
        </div>

        <!-- Сетка с карточками простоев -->
        <div class="idle-grid">
            <!-- Первый ряд -->
            <div class="idle-row">
                <!-- Электрослужба -->
                <div id="Electro_smail" runat="server" class="idle-card">
                    <div class="card-header">Простои Электрослужба</div>
                    <div class="card-body">
                        <div class="stat-item">
                            <span class="stat-label">За тек. месяц</span>
                            <asp:Label ID="lblElecTekMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. месяц</span>
                            <asp:Label ID="lblElecPredMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За тек. год</span>
                            <asp:Label ID="lblElecTekGod" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. год</span>
                            <asp:Label ID="lblElecPredGod" CssClass="stat-value" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Мехслужба -->
                <div id="Mex_smail" runat="server" class="idle-card">
                    <div class="card-header">Простои службы механиков</div>
                    <div class="card-body">
                        <div class="stat-item">
                            <span class="stat-label">За тек. месяц</span>
                            <asp:Label ID="lblMexTekMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. месяц</span>
                            <asp:Label ID="lblMexPredMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За тек. год</span>
                            <asp:Label ID="lblMexTekGod" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. год</span>
                            <asp:Label ID="lblMexPredGod" CssClass="stat-value" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Технологи -->
                <div id="Tex_smail" runat="server" class="idle-card">
                    <div class="card-header">Простои технологов</div>
                    <div class="card-body">
                        <div class="stat-item">
                            <span class="stat-label">За тек. месяц</span>
                            <asp:Label ID="lblTexTekMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. месяц</span>
                            <asp:Label ID="lblTexPredMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За тек. год</span>
                            <asp:Label ID="lblTexTekGod" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. год</span>
                            <asp:Label ID="lblTexPredGod" CssClass="stat-value" runat="server" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Второй ряд -->
            <div class="idle-row">
                <!-- Плановые работы и ПТО -->
                <div class="idle-card">
                    <div class="card-header">Плановые работы и ПТО</div>
                    <div class="card-body">
                        <div class="stat-item">
                            <span class="stat-label">За тек. месяц</span>
                            <asp:Label ID="lblPprTekMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. месяц</span>
                            <asp:Label ID="lblPprPredMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За тек. год</span>
                            <asp:Label ID="lblPprTekGod" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. год</span>
                            <asp:Label ID="lblPprPredGod" CssClass="stat-value" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Прочее -->
                <div class="idle-card">
                    <div class="card-header">Прочее</div>
                    <div class="card-body">
                        <div class="stat-item">
                            <span class="stat-label">За тек. месяц</span>
                            <asp:Label ID="lblOtherTekMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. месяц</span>
                            <asp:Label ID="lblOtherPredMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За тек. год</span>
                            <asp:Label ID="lblOtherTekGod" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. год</span>
                            <asp:Label ID="lblOtherPredGod" CssClass="stat-value" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Общее -->
                <div class="idle-card">
                    <div class="card-header">Общее</div>
                    <div class="card-body">
                        <div class="stat-item">
                            <span class="stat-label">За тек. месяц</span>
                            <asp:Label ID="lblTotalTekMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. месяц</span>
                            <asp:Label ID="lblTotalPredMes" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За тек. год</span>
                            <asp:Label ID="lblTotalTekGod" CssClass="stat-value" runat="server" />
                        </div>
                        <div class="stat-item">
                            <span class="stat-label">За пред. год</span>
                            <asp:Label ID="lblTotalPredGod" CssClass="stat-value" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Таймеры и скрипты -->
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    </div>
</asp:Content>