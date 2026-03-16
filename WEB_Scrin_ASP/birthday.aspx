<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Birthday.aspx.cs" Inherits="WEB_Scrin_ASP.Birthday" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Контейнер всей страницы */
        .birthday-container {
            height: 100%;
            display: flex;
            flex-direction: column;
            background: linear-gradient(145deg, #0a0f1e 0%, #1a1f2f 100%);
            color: #fff;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            padding: 1vh 1vw;
            box-sizing: border-box;
            gap: 1vh;
            overflow: hidden;
        }

        /* Статусная строка */
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

        /* Заголовок */
        .birthday-header {
            flex: 0 0 auto;
            text-align: center;
            font-size: clamp(1.8rem, 3.5vh, 2.5rem);
            font-weight: 600;
            color: #ffd966;
            text-shadow: 0 0 10px rgba(255, 220, 100, 0.5);
            padding: 1vh 0;
            border-bottom: 1px solid rgba(255,255,255,0.1);
            margin-bottom: 1vh;
        }

        /* Ряд с двумя колонками */
        .birthday-row {
            flex: 1;
            display: flex;
            flex-direction: row;
            gap: 1vw;
            min-height: 0;  /* необходимо для правильного расчёта высоты */
            overflow: hidden;
        }

        /* Каждая колонка – flex-контейнер с вертикальным направлением */
        .birthday-column {
            flex: 1 1 0;          /* равная ширина */
            display: flex;
            flex-direction: column;
            gap: 1vh;
            min-height: 0;
            overflow: hidden;
        }

        /* Карточка именинника (обычная) */
        .birthday-card {
            flex: 1 1 0;           /* равномерное распределение по высоте внутри колонки */
            display: flex;
            align-items: center;
            background: rgba(30, 40, 60, 0.6);
            backdrop-filter: blur(4px);
            border-radius: 16px;
            padding: 0.5vh 1vw;
            border: 1px solid rgba(255, 255, 255, 0.1);
            box-shadow: 0 4px 10px rgba(0,0,0,0.3);
            box-sizing: border-box;
            overflow: hidden;
            transition: all 0.2s ease;
        }
        .birthday-card:nth-child(odd) {
            background: rgba(40, 50, 70, 0.7);
        }

        /* Выделение для сегодняшних именинников – без изменения размеров */
        .birthday-today {
            border: 1px solid #ffd966;           /* золотая рамка, но той же толщины */
            background: rgba(70, 90, 140, 0.9);   /* более светлый фон */
            box-shadow: inset 0 0 12px rgba(255, 215, 0, 0.4); /* внутреннее свечение */
            transform: none;                       /* отключаем масштабирование */
        }
        .birthday-today .birthday-name {
            font-weight: 700;
            color: #fff3c9;
        }
        .birthday-today .birthday-icon {
            color: #ffaa00;
            text-shadow: 0 0 8px #ffaa00;
        }

        /* Именинник: значок */
        .birthday-icon {
            font-size: clamp(1.0rem, 2.5vh, 2rem);
            margin-right: 0.8vw;
            color: #ffb347;
            flex-shrink: 0;
        }

        /* Имя: занимает оставшееся место, обрезается с многоточием */
        .birthday-name {
            font-size: clamp(0.9rem, 2.2vh, 1.4rem);
            font-weight: 500;
            color: #ffffff;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            flex: 1 1 auto;
            line-height: 1.2;
        }

        /* Дата: фиксированной ширины */
        .birthday-date {
            font-size: clamp(0.8rem, 2vh, 1.2rem);
            font-weight: 400;
            color: #a0b0c0;
            margin-left: 1vw;
            white-space: nowrap;
            flex-shrink: 0;
        }

        /* Адаптация для узких экранов */
        @media (max-width: 768px) {
            .birthday-card {
                padding: 0.3vh 2vw;
            }
            .birthday-name {
                font-size: clamp(0.8rem, 1.8vh, 1rem);
            }
            .birthday-date {
                font-size: clamp(0.7rem, 1.6vh, 0.9rem);
            }
            .birthday-icon {
                font-size: clamp(1.2rem, 2.5vh, 2rem);
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="birthday-container">
        <!-- СТАТУСНАЯ СТРОКА -->
        <div class="status-bar">
            <asp:TextBox ID="textbox200" CssClass="status-text" runat="server" ReadOnly="True" />
        </div>

        <!-- ЗАГОЛОВОК -->
        <div class="birthday-header">
            <asp:Label ID="Label1" runat="server" Text="" />
        </div>

        <!-- ДВЕ КОЛОНКИ -->
        <div class="birthday-row">
            <!-- Левая колонка -->
            <div class="birthday-column">
                <asp:Repeater ID="RepeaterLeft" runat="server">
                    <ItemTemplate>
                        <div class="birthday-card <%# (bool)Eval("IsToday") ? "birthday-today" : "" %>">
                            <span class="birthday-icon">🎂</span>
                            <span class="birthday-name"><%# Eval("FullName") %></span>
                            <span class="birthday-date"><%# Eval("DisplayDate") %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Правая колонка -->
            <div class="birthday-column">
                <asp:Repeater ID="RepeaterRight" runat="server">
                    <ItemTemplate>
                        <div class="birthday-card <%# (bool)Eval("IsToday") ? "birthday-today" : "" %>">
                            <span class="birthday-icon">🎂</span>
                            <span class="birthday-name"><%# Eval("FullName") %></span>
                            <span class="birthday-date"><%# Eval("DisplayDate") %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
</asp:Content>