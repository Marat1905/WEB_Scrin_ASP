<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Grafik.aspx.cs" Inherits="WEB_Scrin_ASP.Grafik" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Специфические стили для страницы графика */
        .grafik-dashboard {
            height: 100%;
            display: flex;
            flex-direction: column;
            background: linear-gradient(145deg, #0a0f1e 0%, #1a1f2f 100%);
            color: #fff;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 1vh 1vw;
            box-sizing: border-box;
            gap: 1vh;
        }

        /* Статусная строка (общий стиль) */
        .status-bar {
            flex: 0 0 auto;
            height: 7vh;
            min-height: 40px;
        }

        .status-text {
            width: 100%;
            height: 100%;
            text-align: center;
            font-size: 3vh;
            font-weight: bold;
            border: none;
            border-radius: 8px;
            background-color: #2a2f3f;
            color: white;
            box-shadow: 0 4px 15px rgba(0,0,0,0.5);
        }

        /* Заголовок страницы */
        .grafik-header {
            flex: 0 0 auto;
            text-align: center;
            font-size: clamp(1.8rem, 5vh, 3rem);
            font-weight: 600;
            color: #ffd966;
            text-shadow: 0 0 10px rgba(255, 220, 100, 0.5);
            padding: 1vh 0;
            border-bottom: 1px solid rgba(255,255,255,0.1);
            margin-bottom: 1vh;
        }

        /* Контейнер для всех таблиц (с прокруткой) */
        .grafik-tables {
            flex: 1;
            overflow-y: auto;
            min-height: 0;
            display: flex;
            flex-direction: column;
            gap: 2vh;
            padding-right: 0.5vw;
        }

        /* Блок одной таблицы */
        .grafik-section {
            background: rgba(30, 40, 60, 0.5);
            backdrop-filter: blur(4px);
            border-radius: 16px;
            padding: 1vh 1vw;
            border: 1px solid rgba(255, 255, 255, 0.1);
            box-shadow: 0 4px 15px rgba(0,0,0,0.3);
        }

        .section-title {
            font-size: 2.2vh;
            font-weight: 600;
            color: #b0b7d0;
            margin-bottom: 1vh;
            padding-bottom: 0.5vh;
            border-bottom: 1px solid #3a4a6a;
        }

        /* Стили для таблицы (заменяет .table_dark) */
        .grafik-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 1.8vh;
            background: transparent;
            color: #ffffff;
        }

        .grafik-table th {
            background: #1e3a5f;
            color: #ffd966;
            font-weight: 600;
            padding: 1vh 0.5vw;
            text-align: center;
            border: 1px solid #3a4a6a;
            white-space: nowrap;
        }

        .grafik-table td {
            padding: 0.8vh 0.5vw;
            text-align: center;
            border: 1px solid #2a3a50;
            color: #cad4d6;
        }

        .grafik-table tr:nth-child(even) td {
            background: rgba(255, 255, 255, 0.02);
        }

        .grafik-table tr:nth-child(odd) td {
            background: rgba(0, 0, 0, 0.2);
        }

        .grafik-table tr:hover td {
            background: #f3bd48;
            color: #0a0f1e;
            /* Убрано font-weight: bold для предотвращения изменения ширины ячеек при наведении */
            transition: background 0.1s, color 0.1s;
        }

        /* Подсветка текущего дня (синий фон, будет добавлен кодом) */
        .grafik-table td.highlight {
            background: #0078d7 !important;
            color: white !important;
            font-weight: bold;
        }

        /* Адаптация для очень маленьких экранов */
        @media (max-width: 1280px) {
            .grafik-table {
                font-size: 1.6vh;
            }
        }

        @media (max-width: 1024px) {
            .grafik-table {
                font-size: 1.4vh;
            }
        }

        /* ===== КАСТОМНЫЙ СКРОЛЛБАР ДЛЯ .grafik-tables ===== */
        .grafik-tables::-webkit-scrollbar {
            width: 8px;
            height: 8px;
        }

        .grafik-tables::-webkit-scrollbar-track {
            background: #2a2f3f;
            border-radius: 10px;
        }

        .grafik-tables::-webkit-scrollbar-thumb {
            background: #4a4f5f;
            border-radius: 10px;
            border: 2px solid #2a2f3f;
        }

        .grafik-tables::-webkit-scrollbar-thumb:hover {
            background: #5a6a8a;
        }

        /* Firefox */
        .grafik-tables {
            scrollbar-width: thin;
            scrollbar-color: #4a4f5f #2a2f3f;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="grafik-dashboard">
        <!-- СТАТУСНАЯ СТРОКА -->
        <div class="status-bar">
            <asp:TextBox ID="textboxStatus" CssClass="status-text" runat="server" ReadOnly="True" />
        </div>

        <!-- ЗАГОЛОВОК СТРАНИЦЫ -->
        <div class="grafik-header">
            <asp:Label ID="Label_Grafik" runat="server" Text="" />
        </div>

        <!-- КОНТЕЙНЕР С ТАБЛИЦАМИ -->
        <div class="grafik-tables">
            <!-- Таблица 1: Начальники смен -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label1" runat="server" Text="Начальники смен" />
                </div>
                <asp:GridView ID="GridView1" runat="server" CssClass="grafik-table" OnRowDataBound="GridView1_OnRowDataBound" />
            </div>

            <!-- Таблица 2: Машинисты -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label2" runat="server" Text="Машинисты" />
                </div>
                <asp:GridView ID="GridView2" runat="server" CssClass="grafik-table" OnRowDataBound="GridView2_OnRowDataBound" />
            </div>

            <!-- Таблица 3: Старшие сушильщики -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label3" runat="server" Text="Старшие сушильщики" />
                </div>
                <asp:GridView ID="GridView3" runat="server" CssClass="grafik-table" OnRowDataBound="GridView3_OnRowDataBound" />
            </div>

            <!-- Таблица 4: Сушильщики -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label4" runat="server" Text="Сушильщики" />
                </div>
                <asp:GridView ID="GridView4" runat="server" CssClass="grafik-table" OnRowDataBound="GridView4_OnRowDataBound" />
            </div>

            <!-- Таблица 5: Резчики -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label5" runat="server" Text="Резчики" />
                </div>
                <asp:GridView ID="GridView5" runat="server" CssClass="grafik-table" OnRowDataBound="GridView5_OnRowDataBound" />
            </div>

            <!-- Таблица 6: Операторы РПО -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label6" runat="server" Text="Операторы РПО" />
                </div>
                <asp:GridView ID="GridView6" runat="server" CssClass="grafik-table" OnRowDataBound="GridView6_OnRowDataBound" />
            </div>

            <!-- Таблица 7: Дежурные электрики -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label7" runat="server" Text="Дежурные электрики" />
                </div>
                <asp:GridView ID="GridView7" runat="server" CssClass="grafik-table" OnRowDataBound="GridView7_OnRowDataBound" />
            </div>

            <!-- Таблица 8: Дежурные слесари -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label8" runat="server" Text="Дежурные слесари" />
                </div>
                <asp:GridView ID="GridView8" runat="server" CssClass="grafik-table" OnRowDataBound="GridView8_OnRowDataBound" />
            </div>

            <!-- Таблица 9: Операторы бобинореза -->
            <div class="grafik-section">
                <div class="section-title">
                    <asp:Label ID="Label10" runat="server" Text="Операторы бобинореза" />
                </div>
                <asp:GridView ID="GridView9" runat="server" CssClass="grafik-table" OnRowDataBound="GridView9_OnRowDataBound" />
            </div>

            <!-- Пустой заголовок (если нужен) -->
            <div class="grafik-section">
                <asp:Label ID="Label9" runat="server" CssClass="section-title" />
            </div>
        </div>

        <!-- Таймеры (без изменений) -->
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="60000" />
    </div>
</asp:Content>