<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Smena.aspx.cs" Inherits="WEB_Scrin_ASP.Smena" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="refresh" content="65" />
    <style>
        /* Специфические стили для smena.aspx (дополнение к общим) */
        .smena-dashboard {
            height: 100%;
            display: flex;
            flex-direction: column;
            background: linear-gradient(145deg, #0a0f1e 0%, #1a1f2f 100%);
            color: #fff;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 0.5vh 0.5vw;
            box-sizing: border-box;
            gap: 1vh;
        }

        .smena-grid {
            flex: 1;
            display: flex;
            flex-direction: column;
            gap: 1vh;
            min-height: 0; /* важно для Firefox */
        }

        .smena-month {
            flex: 1 1 0;
            display: flex;
            flex-direction: column;
            background: rgba(255, 255, 255, 0.03);
            border-radius: 16px;
            padding: 1vh 0.5vw;
            box-shadow: inset 0 0 10px rgba(0,0,0,0.6);
            border: 1px solid rgba(255,255,255,0.1);
            min-height: 0;
        }

        .smena-month-header {
            font-size: 2.2vh;
            font-weight: 600;
            color: #b0b7d0;
            text-align: center;
            padding: 0.5vh 0;
            margin-bottom: 0.5vh;
            border-bottom: 1px solid rgba(255,255,255,0.1);
            flex-shrink: 0;
        }

        .smena-table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
            font-size: 1.8vh;
            flex: 1;
            min-height: 0;
        }

        .smena-table th {
            color: #b0b7d0;
            font-weight: 500;
            padding: 0.5vh 0.2vw;
            text-align: center;
            border-bottom: 1px solid #3a4a6a;
            white-space: normal;
            word-break: break-word;
        }

        .smena-table td {
            padding: 0.3vh 0.2vw;
            text-align: center;
            border-bottom: 1px solid rgba(255,255,255,0.05);
        }

        .smena-table tr:last-child td {
            border-bottom: none;
        }

        /* Распределение ширины колонок (подобрано под содержимое) */
        .smena-table th:nth-child(1) { width: 8%; }   /* Место */
        .smena-table th:nth-child(2) { width: 10%; }  /* Смена */
        .smena-table th:nth-child(3) { width: 9%; }   /* Кол-во смен */
        .smena-table th:nth-child(4) { width: 9%; }   /* Обрывы сеточ. */
        .smena-table th:nth-child(5) { width: 9%; }   /* Обрывы суш. */
        .smena-table th:nth-child(6) { width: 10%; }  /* План */
        .smena-table th:nth-child(7) { width: 10%; }  /* Факт */
        .smena-table th:nth-child(8) { width: 10%; }  /* Отклонение */
        .smena-table th:nth-child(9) { width: 12%; }  /* Простои */
        .smena-table th:nth-child(10) { width: 13%; } /* Средняя выр-ка */

        /* Стили для текстовых полей внутри таблицы (переопределение старых классов) */
        .smena-table .shift,
        .smena-table .sm,
        .smena-table .PL,
        .smena-table .shift_red,
        .smena-table .sm_red,
        .smena-table .PL_red,
        .smena-table .PL_otkl {
            width: 100%;
            height: 4vh;
            border: none;
            background: transparent;
            color: #ffffff;
            font-size: 1.8vh;
            font-weight: 500;
            text-align: center;
            padding: 0;
            box-sizing: border-box;
            border-radius: 4px;
            transition: all 0.1s;
            resize: none;
        }

        .smena-table .shift_red,
        .smena-table .sm_red,
        .smena-table .PL_red {
            color: #ff6b6b;
            font-weight: bold;
        }

        .smena-table .PL_otkl {
            color: #ffaa00;
            animation: otkl-blink 2s infinite;
        }

        @keyframes otkl-blink {
            0% { opacity: 1; }
            50% { opacity: 0.5; }
            100% { opacity: 1; }
        }

        /* Адаптация для очень маленьких экранов */
        @media (max-width: 1280px) {
            .smena-table th,
            .smena-table .shift,
            .smena-table .sm,
            .smena-table .PL {
                font-size: 1.6vh;
            }
        }

        @media (max-width: 1024px) {
            .smena-table th,
            .smena-table .shift,
            .smena-table .sm,
            .smena-table .PL {
                font-size: 1.4vh;
            }
        }

        /* Статусная строка (как в idle time) */
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="smena-dashboard">
        <!-- Статусная строка -->
        <div class="status-bar">
            <asp:TextBox ID="textbox200" CssClass="status-text" runat="server" ReadOnly="True" />
        </div>

        <!-- Сетка с двумя месяцами -->
        <div class="smena-grid">
            <!-- Текущий месяц -->
            <div class="smena-month">
                <div class="smena-month-header">
                    <asp:Label ID="Label1" runat="server" Text="Текущий месяц" />
                </div>
                <table class="smena-table">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Смена</th>
                            <th>Кол-во<br />смен</th>
                            <th>Обрывы<br />сеточ.</th>
                            <th>Обрывы<br />суш.</th>
                            <th>План</th>
                            <th>Факт</th>
                            <th>Отклонение</th>
                            <th>Простои</th>
                            <th>Средняя<br />выр-ка</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>1 место</td>
                            <td><asp:TextBox ID="Tab_1_1_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_1_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                        <tr>
                            <td>2 место</td>
                            <td><asp:TextBox ID="Tab_1_2_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_2_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                        <tr>
                            <td>3 место</td>
                            <td><asp:TextBox ID="Tab_1_3_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_3_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                        <tr>
                            <td>4 место</td>
                            <td><asp:TextBox ID="Tab_1_4_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_1_4_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <!-- Предыдущий месяц -->
            <div class="smena-month">
                <div class="smena-month-header">
                    <asp:Label ID="Label2" runat="server" Text="Предыдущий месяц" />
                </div>
                <table class="smena-table">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Смена</th>
                            <th>Кол-во<br />смен</th>
                            <th>Обрывы<br />сеточ.</th>
                            <th>Обрывы<br />суш.</th>
                            <th>План</th>
                            <th>Факт</th>
                            <th>Отклонение</th>
                            <th>Простои</th>
                            <th>Средняя<br />выр-ка</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>1 место</td>
                            <td><asp:TextBox ID="Tab_2_1_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_1_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                        <tr>
                            <td>2 место</td>
                            <td><asp:TextBox ID="Tab_2_2_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_2_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                        <tr>
                            <td>3 место</td>
                            <td><asp:TextBox ID="Tab_2_3_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_3_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                        <tr>
                            <td>4 место</td>
                            <td><asp:TextBox ID="Tab_2_4_1" CssClass="shift" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_2" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_3" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_4" CssClass="sm" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_5" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_6" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_7" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_8" CssClass="PL" runat="server" ReadOnly="True" /></td>
                            <td><asp:TextBox ID="Tab_2_4_9" CssClass="PL" runat="server" ReadOnly="True" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <!-- Таймеры (оставлены как есть) -->
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="40000" />
    </div>
</asp:Content>