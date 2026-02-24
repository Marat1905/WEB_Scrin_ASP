<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="GreenCross.aspx.cs" Inherits="WEB_Scrin_ASP.GreenCross" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <style>
        /* Общие сбросы */
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body, html {
            height: 100%;
            overflow: hidden;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        /* Основной контейнер страницы */
        .green-cross-container {
            height: 100%;
            display: flex;
            flex-direction: column;
            background: linear-gradient(145deg, #0a0f1e 0%, #1a1f2f 100%);
            padding: 1vh 1vw;
            gap: 1vh;
            overflow: hidden;
            color: #fff;
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

        /* Основной двухколоночный макет */
        .main-content {
            flex: 1;
            display: flex;
            gap: 1vw;
            min-height: 0;
        }

        /* Левая колонка – статистика */
        .stats-panel {
            flex: 1 1 25%;
            background: rgba(30, 40, 60, 0.6);
            backdrop-filter: blur(8px);
            border-radius: 24px;
            padding: 2vh 1vw;
            box-shadow: 0 10px 30px rgba(0,0,0,0.5);
            border: 1px solid rgba(255,255,255,0.1);
            display: flex;
            flex-direction: column;
            gap: 2vh;
            overflow-y: auto;
        }

        .stats-title {
            font-size: 2.2vh;
            font-weight: 600;
            color: #ffd966;
            display: flex;
            align-items: center;
            gap: 0.5vw;
            border-bottom: 1px solid #3a4a6a;
            padding-bottom: 1vh;
        }

        .stat-item {
            display: flex;
            align-items: center;
            gap: 1vw;
        }

        .stat-icon {
            width: 5vh;
            height: 5vh;
            background: #1e3a5f;
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 2.5vh;
            color: #ffd966;
        }

        .stat-content {
            flex: 1;
        }

        .stat-label {
            font-size: 1.8vh;
            color: #b0b7d0;
        }

        .stat-value {
            font-size: 3.5vh;
            font-weight: 700;
            color: #fff;
            line-height: 1.2;
        }

        .stat-note {
            font-size: 1.6vh;
            color: #b0b7d0;
        }

        .legend {
            margin-top: auto;
            background: rgba(0, 0, 0, 0.3);
            border-radius: 16px;
            padding: 1.5vh 1vw;
            border: 1px solid rgba(255,255,255,0.1);
        }

        .legend-item {
            display: flex;
            align-items: center;
            gap: 0.8vw;
            margin-bottom: 1vh;
            font-size: 1.8vh;
            color: #b0b7d0;
        }

        .legend-color {
            width: 2.5vh;
            height: 2.5vh;
            border-radius: 6px;
        }

        .color-safe { background: linear-gradient(145deg, #2e7d32, #1b5e20); }
        .color-injury { background: linear-gradient(145deg, #b71c1c, #8b0000); }
        .color-future { background: #4a4f5f; }
        .color-today { border: 3px solid #2196f3; background: transparent; }

        /* Правая колонка – визуализация */
        .visual-panel {
            flex: 1 1 75%;
            background: rgba(30, 40, 60, 0.6);
            backdrop-filter: blur(8px);
            border-radius: 24px;
            padding: 2vh 1vw;
            box-shadow: 0 10px 30px rgba(0,0,0,0.5);
            border: 1px solid rgba(255,255,255,0.1);
            display: flex;
            flex-direction: column;
            overflow: hidden;
        }

        /* Внутренняя шапка правой панели: навигация слева, переключатель справа */
        .visual-header {
            flex: 0 0 auto;
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 1vh;
            padding: 0 0.5vw;
        }

        .nav-left {
            display: flex;
            align-items: center;
            gap: 0.8vw;
        }

        .nav-btn {
            background: #2a2f3f;
            border: 1px solid #3a4a6a;
            border-radius: 12px;
            padding: 0.6vh 1vw;
            font-size: 2vh;
            font-weight: 600;
            color: #fff;
            cursor: pointer;
            transition: 0.2s;
            display: flex;
            align-items: center;
            gap: 4px;
            text-decoration: none;
        }

        .nav-btn:hover {
            background: #3a4055;
            border-color: #5a6a8a;
        }

        .current-date {
            font-size: 2.2vh;
            font-weight: 700;
            color: #ffd966;
            min-width: 12vw;
            text-align: center;
        }

        .mode-switch {
            display: flex;
            background: #2a2f3f;
            border-radius: 12px;
            padding: 2px;
            border: 1px solid #3a4a6a;
        }

        .mode-btn {
            padding: 0.6vh 1.2vw;
            border: none;
            border-radius: 10px;
            font-size: 2vh;
            font-weight: 600;
            cursor: pointer;
            transition: 0.2s;
            background: transparent;
            color: #b0b7d0;
            text-decoration: none;
        }

        .mode-btn.active {
            background: #1e3a5f;
            color: #fff;
            box-shadow: 0 2px 8px rgba(0,0,0,0.3);
        }

        .visual-content {
            flex: 1;
            overflow-y: auto;
            min-height: 0;
        }

        /* Стили для креста (сетка 7x7) */
        .cross-grid {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            gap: 0.6vh;
            aspect-ratio: 1 / 1;
            width: 100%;
            max-width: 70vh;
            margin: 0 auto;
        }

        .cell {
            aspect-ratio: 1 / 1;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 12px;
            font-weight: 700;
            font-size: 2.2vh;
            color: white;
            cursor: pointer;
            transition: transform 0.1s, box-shadow 0.1s;
            box-shadow: 0 4px 6px rgba(0,0,0,0.3);
        }

        .cell.safe { background: linear-gradient(145deg, #2e7d32, #1b5e20); }
        .cell.injury { background: linear-gradient(145deg, #b71c1c, #8b0000); }
        .cell.future { background: #4a4f5f; cursor: not-allowed; }
        .cell.today { border: 3px solid #2196f3; }
        
        .cell.empty {
            background: #3a4055;
            box-shadow: none;
            cursor: default;
            opacity: 0.5;
        }

        .cell.out {
            background: transparent;
            box-shadow: none;
            cursor: default;
            opacity: 0;
        }

        .cell:hover:not(.future):not(.empty):not(.out) {
            transform: scale(1.02);
            box-shadow: 0 8px 12px rgba(0,0,0,0.5);
        }

        /* Стили для календаря года */
        .year-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 1.5vh 1vw;
            width: 100%;
        }

        .month-card {
            background: rgba(0, 0, 0, 0.3);
            border-radius: 16px;
            padding: 1vh 0.5vw;
            border: 1px solid rgba(255,255,255,0.1);
        }

        .month-name {
            font-size: 1.8vh;
            font-weight: 600;
            text-align: center;
            color: #ffd966;
            margin-bottom: 0.8vh;
        }

        .month-weekdays {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            font-size: 1.2vh;
            color: #b0b7d0;
            text-align: center;
            margin-bottom: 0.5vh;
        }

        .month-days {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            gap: 2px;
        }

        .month-day {
            aspect-ratio: 1 / 1;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.4vh;
            font-weight: 500;
            border-radius: 4px;
            color: white;
            cursor: pointer;
            transition: 0.1s;
        }

        .month-day.safe { background: #2e7d32; }
        .month-day.injury { background: #b71c1c; }
        .month-day.future { background: #4a4f5f; cursor: not-allowed; }
        .month-day.today { border: 2px solid #2196f3; }
        .month-day:hover:not(.future) { filter: brightness(1.2); }

        /* Модальное окно */
        .modal {
            display: none;
            position: fixed;
            top: 0; left: 0; width: 100%; height: 100%;
            background: rgba(0,0,0,0.7);
            backdrop-filter: blur(8px);
            align-items: center;
            justify-content: center;
            z-index: 1000;
        }

        .modal-content {
            background: #1a1f2f;
            border-radius: 24px;
            padding: 2vh 2vw;
            max-width: 40vw;
            width: 90%;
            box-shadow: 0 20px 40px rgba(0,0,0,0.5);
            border: 1px solid rgba(255,255,255,0.1);
            color: #fff;
        }

        .modal-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #3a4a6a;
            padding-bottom: 1vh;
            margin-bottom: 2vh;
        }

        .modal-header h3 {
            font-size: 2.5vh;
            color: #ffd966;
        }

        .close-btn {
            background: none;
            border: none;
            font-size: 3vh;
            cursor: pointer;
            color: #b0b7d0;
        }

        .close-btn:hover {
            color: #fff;
        }

        .modal-body {
            font-size: 2vh;
            line-height: 1.5;
            color: #b0b7d0;
        }

        .modal-body p {
            margin-bottom: 1vh;
        }

        .modal-body strong {
            color: #ffd966;
        }

        /* Адаптация */
        @media (max-width: 1280px) {
            .stat-value { font-size: 3vh; }
            .cell { font-size: 2vh; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="green-cross-container">
        <!-- СТАТУСНАЯ СТРОКА БДМ -->
        <div class="status-bar">
            <asp:TextBox ID="textboxStatus" CssClass="status-text" runat="server" ReadOnly="True" />
        </div>

        <!-- Две колонки -->
        <div class="main-content">
            <!-- Левая колонка – статистика -->
            <div class="stats-panel">
                <div class="stats-title">
                    <span>📊 Статистика</span>
                </div>
                <div class="stat-item">
                    <div class="stat-icon">📅</div>
                    <div class="stat-content">
                        <div class="stat-label">Травм за месяц</div>
                        <div class="stat-value"><%= MonthInjuriesCount %></div>
                    </div>
                </div>
                <div class="stat-item">
                    <div class="stat-icon">📈</div>
                    <div class="stat-content">
                        <div class="stat-label">Травм за год</div>
                        <div class="stat-value"><%= YearInjuriesCount %></div>
                    </div>
                </div>
                <div class="stat-item">
                    <div class="stat-icon">⏳</div>
                    <div class="stat-content">
                        <div class="stat-label">Дней без травм</div>
                        <div class="stat-value"><%= DaysWithoutInjury %></div>
                        <% if (!string.IsNullOrEmpty(LastInjuryDateStr)) { %>
                        <div class="stat-note">последняя: <%= LastInjuryDateStr %></div>
                        <% } else { %>
                        <div class="stat-note">травм не было</div>
                        <% } %>
                    </div>
                </div>

                <div class="legend">
                    <div class="legend-item"><span class="legend-color color-safe"></span> Нет травм (прошлое/сегодня)</div>
                    <div class="legend-item"><span class="legend-color color-injury"></span> Есть травма</div>
                    <div class="legend-item"><span class="legend-color color-future"></span> Будущие дни</div>
                    <div class="legend-item"><span class="legend-color color-today"></span> Сегодня</div>
                </div>
            </div>

            <!-- Правая колонка – визуализация + навигация слева + переключатель справа -->
            <div class="visual-panel">
                <div class="visual-header">
                    <!-- Навигация (пред./след.) слева -->
                    <div class="nav-left">
                        <a href="?mode=<%= ViewMode %>&year=<%= PrevYear %>&month=<%= PrevMonth %>" class="nav-btn">◀ Пред.</a>
                        <span class="current-date"><%= CurrentDisplay %></span>
                        <a href="?mode=<%= ViewMode %>&year=<%= NextYear %>&month=<%= NextMonth %>" class="nav-btn">След. ▶</a>
                    </div>
                    <!-- Переключатель режимов (крест/год) справа -->
                    <div class="mode-switch">
                        <a href="?mode=cross&year=<%= CurrentDate.Year %>&month=<%= CurrentDate.Month %>" 
                           class="mode-btn <%= ViewMode == "cross" ? "active" : "" %>">Крест</a>
                        <a href="?mode=year&year=<%= CurrentDate.Year %>&month=1" 
                           class="mode-btn <%= ViewMode == "year" ? "active" : "" %>">Год</a>
                    </div>
                </div>
                <div class="visual-content">
                    <asp:Literal ID="litVisual" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <!-- Модальное окно -->
    <div id="modal" class="modal" onclick="closeModal(event)">
        <div class="modal-content" onclick="event.stopPropagation()">
            <div class="modal-header">
                <h3 id="modal-title">Информация о травме</h3>
                <button class="close-btn" onclick="closeModal(event)">✕</button>
            </div>
            <div class="modal-body">
                <p><strong>Дата:</strong> <span id="modal-date"></span></p>
                <p><strong>Тип:</strong> <span id="modal-type"></span></p>
                <p><strong>Описание:</strong> <span id="modal-desc"></span></p>
            </div>
        </div>
    </div>

    <!-- ScriptManager -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />


    <asp:HiddenField ID="hfMonthName" runat="server" />
    <script type="text/javascript">
        var monthName = '<%= CurrentDate.ToString("MMMM", new System.Globalization.CultureInfo("ru-RU")) %>';

        function showInfo(day, hasInjury, type, desc) {
            document.getElementById('modal-date').innerText = day + ' ' + monthName;
            if (hasInjury) {
                document.getElementById('modal-type').innerText = type;
                document.getElementById('modal-desc').innerText = desc;
            } else {
                document.getElementById('modal-type').innerText = 'Нет травмы';
                document.getElementById('modal-desc').innerText = '';
            }
            document.getElementById('modal').style.display = 'flex';
        }

        function closeModal(e) {
            if (e) e.preventDefault();
            document.getElementById('modal').style.display = 'none';
        }

        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') closeModal();
        });
    </script>
</asp:Content>