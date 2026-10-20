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
            background: linear-gradient(145deg, #0f172a 0%, #1e293b 100%);
            padding: 1dvh 1vw;
            gap: 1dvh;
            overflow: hidden;
            color: #fff;
        }

        /* Статусная строка */
        .status-bar {
            flex: 0 0 auto;
            height: 7dvh;
            min-height: 40px;
        }

        .status-text {
            width: 100%;
            height: 100%;
            text-align: center;
            font-size: 3dvh;
            font-weight: bold;
            border: none;
            border-radius: 12px;
            background-color: #334155;
            color: #f8fafc;
            box-shadow: 0 4px 15px rgba(0,0,0,0.3);
        }

        /* Основной двухколоночный макет */
        .main-content {
            flex: 1;
            display: flex;
            gap: 1.5vw;
            min-height: 0;
        }

        /* Левая колонка – статистика (с прокруткой) */
        .stats-panel {
            flex: 1 1 25%;
            background: rgba(30, 41, 59, 0.8);
            backdrop-filter: blur(12px);
            border-radius: 24px;
            /* УМЕНЬШЕНО: padding для экономии места по вертикали */
            padding: 1.5dvh 1vw; 
            box-shadow: 0 10px 30px rgba(0,0,0,0.4);
            border: 1px solid rgba(255,255,255,0.1);
            display: flex;
            flex-direction: column;
            /* УМЕНЬШЕНО: gap между блоками */
            gap: 1.5dvh; 
            overflow-y: auto;
        }

        .stats-title {
            /* УМЕНЬШЕНО: размер шрифта заголовка */
            font-size: 2dvh; 
            font-weight: 600;
            color: #fbbf24;
            display: flex;
            align-items: center;
            gap: 0.5vw;
            border-bottom: 1px solid #475569;
            /* УМЕНЬШЕНО: отступ под заголовком */
            padding-bottom: 0.5dvh; 
        }

        .stat-item {
            display: flex;
            align-items: center;
            /* УМЕНЬШЕНО: отступ между иконкой и текстом */
            gap: 0.8vw; 
        }

        .stat-icon {
            /* УМЕНЬШЕНО: размер иконки */
            width: 4.5dvh; 
            height: 4.5dvh;
            background: #1e3a8a;
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            /* УМЕНЬШЕНО: размер шрифта внутри иконки */
            font-size: 2.2dvh; 
            color: #60a5fa;
        }

        .stat-content {
            flex: 1;
        }

        .stat-label {
            /* УМЕНЬШЕНО: размер шрифта подписи */
            font-size: 1.6dvh; 
            color: #94a3b8;
        }

        .stat-value {
            /* УМЕНЬШЕНО: размер шрифта значения */
            font-size: 3dvh; 
            font-weight: 700;
            color: #f8fafc;
            line-height: 1.2;
        }

        .stat-note {
            font-size: 1.5dvh;
            color: #94a3b8;
        }

        .legend {
            /* Изменено: auto прижимает к низу, но мы уменьшили отступы, чтобы он не выезжал */
            margin-top: auto; 
            background: rgba(15, 23, 42, 0.5);
            border-radius: 16px;
            /* УМЕНЬШЕНО: padding легенды */
            padding: 1dvh 1vw; 
            border: 1px solid rgba(255,255,255,0.1);
        }

        .legend-item {
            display: flex;
            align-items: center;
            gap: 0.8vw;
            /* УМЕНЬШЕНО: отступ между пунктами легенды */
            margin-bottom: 0.5dvh; 
            /* УМЕНЬШЕНО: размер шрифта легенды */
            font-size: 1.6dvh; 
            color: #cbd5e1;
        }

        .legend-item:last-child {
            margin-bottom: 0;
        }

        .legend-color {
            /* УМЕНЬШЕНО: размер цветовых квадратиков */
            width: 2dvh; 
            height: 2dvh;
            border-radius: 6px;
            flex-shrink: 0;
        }

        /* ИЗМЕНЕНО: зелёный цвет сделан ещё темнее и приглушённее */
        .color-safe { background: linear-gradient(145deg, #15803d, #14532d); }
        .color-injury-significant { background: linear-gradient(145deg, #f87171, #dc2626); }
        .color-injury-minor { background: linear-gradient(145deg, #fbbf24, #d97706); }
        .color-future { background: #4a4f5f; }
        .color-today { border: 3px solid #3b82f6; background: transparent; }

        /* Блок Ноль травм */
        .zero-injury-panel {
            background: rgba(15, 23, 42, 0.6);
            border-radius: 16px;
            /* УМЕНЬШЕНО: padding панели */
            padding: 1dvh 1vw; 
            border: 1px solid rgba(255,255,255,0.1);
            /* УМЕНЬШЕНО: отступ снизу */
            margin-bottom: 0.5dvh; 
        }
        .zero-injury-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #475569;
            padding-bottom: 0.5dvh;
            margin-bottom: 0.5dvh;
        }
        .zero-injury-title {
            /* УМЕНЬШЕНО: размер шрифта заголовка */
            font-size: 2dvh; 
            font-weight: 600;
            color: #fbbf24;
        }
        .zero-injury-link {
            color: #60a5fa;
            font-size: 1.5dvh;
            text-decoration: none;
            cursor: pointer;
        }
        .zero-injury-link:hover { text-decoration: underline; }
        .zero-progress-bg {
            width: 100%;
            height: 6px;
            background: #334155;
            border-radius: 3px;
            overflow: hidden;
        }
        .zero-progress-fill {
            height: 100%;
            background: #10b981;
        }

        /* Правая колонка – визуализация */
        .visual-panel {
            flex: 1 1 75%;
            background: rgba(30, 41, 59, 0.8);
            backdrop-filter: blur(12px);
            border-radius: 24px;
            padding: 2dvh 1.5vw;
            box-shadow: 0 10px 30px rgba(0,0,0,0.4);
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
            margin-bottom: 1.5dvh;
            padding: 0 0.5vw;
        }

        .nav-left {
            display: flex;
            align-items: center;
            gap: 0.8vw;
        }

        .nav-btn {
            background: #334155;
            border: 1px solid #475569;
            border-radius: 12px;
            padding: 0.6dvh 1.2vw;
            font-size: 2dvh;
            font-weight: 600;
            color: #f8fafc;
            cursor: pointer;
            transition: 0.2s;
            display: flex;
            align-items: center;
            gap: 4px;
            text-decoration: none;
        }

        .nav-btn:hover {
            background: #475569;
            border-color: #64748b;
        }

        .current-date {
            font-size: 2.2dvh;
            font-weight: 700;
            color: #fbbf24;
            min-width: 12vw;
            text-align: center;
        }

        .mode-switch {
            display: flex;
            background: #334155;
            border-radius: 12px;
            padding: 3px;
            border: 1px solid #475569;
        }

        .mode-btn {
            padding: 0.6dvh 1.5vw;
            border: none;
            border-radius: 10px;
            font-size: 2dvh;
            font-weight: 600;
            cursor: pointer;
            transition: 0.2s;
            background: transparent;
            color: #94a3b8;
            text-decoration: none;
        }

        .mode-btn.active {
            background: #1e3a8a;
            color: #f8fafc;
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
            gap: 0.6dvh;
            aspect-ratio: 1 / 1;
            width: 100%;
            max-width: min(65dvh, 100%);
            margin: 0 auto;
        }

        .cell {
            aspect-ratio: 1 / 1;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 12px;
            font-weight: 700;
            font-size: clamp(1.2rem, 2.2dvh, 2rem);
            color: white;
            cursor: pointer;
            transition: transform 0.15s, box-shadow 0.15s;
            box-shadow: 0 4px 6px rgba(0,0,0,0.2);
            border: none;
            user-select: none;
        }

        /* ИЗМЕНЕНО: зелёный цвет сделан ещё темнее и приглушённее */
        .cell.safe {
            background: linear-gradient(145deg, #15803d, #14532d);
            box-shadow: 0 4px 10px rgba(20, 83, 45, 0.4);
        }
        .cell.injury-significant {
            background: linear-gradient(145deg, #f87171, #dc2626);
            box-shadow: 0 4px 10px rgba(248, 113, 113, 0.3);
        }
        .cell.injury-minor {
            background: linear-gradient(145deg, #fbbf24, #d97706);
            box-shadow: 0 4px 10px rgba(251, 191, 36, 0.3);
        }
        .cell.future {
            background: #4a4f5f;
            cursor: not-allowed;
            box-shadow: none;
            color: #94a3b8;
        }
        .cell.today {
            border: 3px solid #3b82f6;
            box-shadow: 0 0 0 2px rgba(59, 130, 246, 0.3);
        }
        .cell.empty {
            background: #475569;
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
            transform: scale(1.05);
            box-shadow: 0 8px 15px rgba(0,0,0,0.4);
            z-index: 10;
        }

        /* Стили для календаря года */
        .year-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 1.5dvh 1vw;
            width: 100%;
        }

        .month-card {
            background: rgba(15, 23, 42, 0.5);
            border-radius: 16px;
            padding: 1dvh 0.5vw;
            border: 1px solid rgba(255,255,255,0.1);
        }

        .month-name {
            font-size: 1.8dvh;
            font-weight: 600;
            text-align: center;
            color: #fbbf24;
            margin-bottom: 0.8dvh;
        }

        .month-weekdays {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            font-size: 1.2dvh;
            color: #94a3b8;
            text-align: center;
            margin-bottom: 0.5dvh;
        }

        .month-days {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            gap: 3px;
        }

        .month-day {
            aspect-ratio: 1 / 1;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: clamp(0.8rem, 1.8dvh, 1.4rem);
            font-weight: 500;
            border-radius: 6px;
            color: white;
            cursor: pointer;
            transition: 0.15s;
            border: none;
            user-select: none;
        }

        /* ИЗМЕНЕНО: зелёный цвет сделан темнее */
        .month-day.safe { background: #166534; }
        .month-day.injury-significant { background: #dc2626; }
        .month-day.injury-minor { background: #d97706; }
        .month-day.future { background: #4a4f5f; cursor: not-allowed; color: #94a3b8; }
        .month-day.today { border: 2px solid #3b82f6; }

        .month-day:hover:not(.future) {
            filter: brightness(1.2);
            transform: scale(1.1);
        }

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
            background: #1e293b;
            border-radius: 24px;
            padding: 2.5dvh 2.5vw;
            max-width: 40vw;
            width: 90%;
            box-shadow: 0 20px 40px rgba(0,0,0,0.6);
            border: 1px solid rgba(255,255,255,0.1);
            color: #f8fafc;
        }

        /* Специфичное модальное окно для Ноль Травм (шире из-за таблицы) */
        .zero-modal-content {
            max-width: 60vw;
        }

        .modal-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #475569;
            padding-bottom: 1.5dvh;
            margin-bottom: 2dvh;
        }

        .modal-header h3 {
            font-size: 2.5dvh;
            color: #fbbf24;
            margin: 0;
        }

        .close-btn {
            background: none;
            border: none;
            font-size: 3dvh;
            cursor: pointer;
            color: #94a3b8;
            transition: color 0.2s;
        }

        .close-btn:hover {
            color: #f8fafc;
        }

        .modal-body {
            font-size: 2dvh;
            line-height: 1.6;
            color: #cbd5e1;
        }

        .modal-body p {
            margin-bottom: 1.5dvh;
        }

        .modal-body strong {
            color: #fbbf24;
            display: inline-block;
            min-width: 100px;
        }

        /* Таблица истории в модальном окне Ноль Травм */
        .history-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 1.8dvh;
        }
        .history-table th {
            text-align: left;
            padding: 1dvh;
            color: #fbbf24;
            border-bottom: 2px solid #475569;
        }
        .history-table td {
            padding: 1.5dvh 1dvh;
            border-bottom: 1px solid #334155;
            color: #cbd5e1;
        }
        .badge {
            padding: 0.5dvh 1vw;
            border-radius: 8px;
            font-size: 1.6dvh;
            font-weight: 600;
            white-space: nowrap;
        }
        .badge-active { background: rgba(59, 130, 246, 0.2); color: #60a5fa; }
        .badge-success { background: rgba(16, 185, 129, 0.2); color: #34d399; }
        .badge-interrupted { background: rgba(239, 68, 68, 0.2); color: #f87171; }

        /* Адаптация */
        @media (max-width: 1280px) {
            .stat-value { font-size: 2.8dvh; }
            .cell { font-size: clamp(1rem, 2dvh, 1.8rem); }
            .year-grid { grid-template-columns: repeat(3, 1fr); }
            .zero-modal-content { max-width: 80vw; }
        }

        @media (max-width: 768px) {
            .main-content { flex-direction: column; }
            .stats-panel { flex: 0 0 auto; }
            .visual-panel { flex: 1 1 auto; }
            .year-grid { grid-template-columns: repeat(2, 1fr); }
            .modal-content { max-width: 90vw; }
            .zero-modal-content { max-width: 95vw; }
        }

        @media (max-width: 480px) {
            .cross-grid { gap: 0.3dvh; }
            .cell { font-size: 1.8dvh; border-radius: 8px; }
            .year-grid { grid-template-columns: 1fr; }
        }

        /* ===== КАСТОМНЫЙ СКРОЛЛБАР ДЛЯ ПРОКРУЧИВАЕМЫХ ОБЛАСТЕЙ ===== */
        .stats-panel::-webkit-scrollbar,
        .visual-content::-webkit-scrollbar,
        .modal-body::-webkit-scrollbar {
            width: 8px;
            height: 8px;
        }

        .stats-panel::-webkit-scrollbar-track,
        .visual-content::-webkit-scrollbar-track,
        .modal-body::-webkit-scrollbar-track {
            background: #334155;
            border-radius: 10px;
        }

        .stats-panel::-webkit-scrollbar-thumb,
        .visual-content::-webkit-scrollbar-thumb,
        .modal-body::-webkit-scrollbar-thumb {
            background: #475569;
            border-radius: 10px;
            border: 2px solid #334155;
        }

        .stats-panel::-webkit-scrollbar-thumb:hover,
        .visual-content::-webkit-scrollbar-thumb:hover,
        .modal-body::-webkit-scrollbar-thumb:hover {
            background: #64748b;
        }

        /* Firefox */
        .stats-panel,
        .visual-content,
        .modal-body {
            scrollbar-width: thin;
            scrollbar-color: #475569 #334155;
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
                
                <!-- БЛОК НОЛЬ ТРАВМ (ВЫНЕСЕН НАВЕРХ) -->
                <div class="zero-injury-panel">
                    <div class="zero-injury-header">
                        <span class="zero-injury-title">🛡️ Ноль травм</span>
                        <!-- Вызов модального окна вместо перехода на страницу -->
                        <a href="javascript:void(0);" onclick="openZeroModal();" class="zero-injury-link">Подробнее ➔</a>
                    </div>
                    <% if (ZeroInjuryStatus != null && ZeroInjuryStatus.CurrentProgram != null) { 
                        var prog = ZeroInjuryStatus.CurrentProgram;
                    %>
                        <!-- УВЕЛИЧЕНЫ ЦИФРЫ -->
                        <div style="display: flex; justify-content: space-between; align-items: baseline; margin-bottom: 0.5dvh;">
                            <div style="font-size: 3.5dvh; font-weight: 700; color: #f8fafc; line-height: 1;">
                                <%= prog.DaysPassed %> <span style="font-size: 2.2dvh; color: #94a3b8; font-weight: 400;">/ <%= prog.TargetDays %> дн.</span>
                            </div>
                            <div style="font-size: 1.4dvh; color: #10b981; background: rgba(16,185,129,0.2); padding: 0.2dvh 0.6vw; border-radius: 8px;">Активна</div>
                        </div>
                        <div class="zero-progress-bg" style="margin-top: 1dvh;">
                            <div class="zero-progress-fill" style="width: <%= Math.Min(100, (int)((prog.DaysPassed * 100.0) / prog.TargetDays)) %>%;"></div>
                        </div>
                        <div style="font-size: 1.4dvh; color: #94a3b8; margin-top: 0.5dvh;">Начало: <%= prog.StartDate.ToString("dd.MM.yyyy") %></div>
                    <% } else if (ZeroInjuryStatus != null && ZeroInjuryStatus.LastFinalizedProgram != null) { 
                        var last = ZeroInjuryStatus.LastFinalizedProgram;
                    %>
                        <div style="font-size: 1.6dvh; color: #94a3b8;">Программа не запущена.</div>
                        <% if (last.IsSuccessfullyCompleted) { %>
                            <div style="font-size: 1.4dvh; color: #10b981; margin-top: 0.5dvh;">
                                Последняя завершена успешно (<%= last.DaysPassed %> дн.)
                                <% if (last.CompletedAt.HasValue) { %>
                                    <br/><span style="color: #94a3b8; font-size: 1.3dvh;">Завершена: <%= last.CompletedAt.Value.ToString("dd.MM.yyyy") %></span>
                                <% } %>
                            </div>
                        <% } else { %>
                            <div style="font-size: 1.4dvh; color: #ef4444; margin-top: 0.5dvh;">
                                Последняя прервана травмой (<%= last.DaysPassed %> дн.)
                                <% if (last.CompletedAt.HasValue) { %>
                                    <br/><span style="color: #94a3b8; font-size: 1.3dvh;">Прервана: <%= last.CompletedAt.Value.ToString("dd.MM.yyyy") %></span>
                                <% } %>
                            </div>
                        <% } %>
                    <% } else { %>
                        <div style="font-size: 1.6dvh; color: #94a3b8;">Программа ещё не запускалась.</div>
                    <% } %>
                </div>

                <!-- БЛОК СТАТИСТИКИ -->
                <div class="stats-title">
                    <span>📊 Статистика (П1+П2)</span>
                </div>
                <div class="stat-item">
                    <div class="stat-icon">📅</div>
                    <div class="stat-content">
                        <div class="stat-label">Травм П1-П2 за месяц</div>
                        <div class="stat-value"><%= MonthSignificantCount %></div>
                    </div>
                </div>
                <div class="stat-item">
                    <div class="stat-icon">📈</div>
                    <div class="stat-content">
                        <div class="stat-label">Травм П1-П2 за год</div>
                        <div class="stat-value"><%= YearSignificantCount %></div>
                    </div>
                </div>
                <div class="stat-item">
                    <div class="stat-icon">⏳</div>
                    <div class="stat-content">
                        <div class="stat-label">Дней без травм (П1+П2)</div>
                        <div class="stat-value"><%= DaysWithoutInjury %></div>
                        <% if (!string.IsNullOrEmpty(LastSignificantDateStr)) { %>
                            <div class="stat-note">последняя: <%= LastSignificantDateStr %></div>
                        <% } else { %>
                            <div class="stat-note">значимых травм не было</div>
                        <% } %>
                    </div>
                </div>

                <!-- ЛЕГЕНДА -->
                <div class="legend">
                    <div class="legend-item"><span class="legend-color color-safe"></span> Нет травм (прошлое/сегодня)</div>
                    <div class="legend-item"><span class="legend-color color-injury-significant"></span> Травмы П1 или П2</div>
                    <div class="legend-item"><span class="legend-color color-injury-minor"></span> Травмы П3-П6</div>
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

    <!-- МОДАЛЬНОЕ ОКНО: ИНФОРМАЦИЯ О ТРАВМЕ -->
    <div id="modal" class="modal" onclick="closeModal(event)">
        <div class="modal-content" onclick="event.stopPropagation()">
            <div class="modal-header">
                <h3 id="modal-title">Информация о травме</h3>
                <button type="button" class="close-btn" onclick="closeModal(event)">✕</button>
            </div>
            <div class="modal-body">
                <p><strong>Дата:</strong> <span id="modal-date"></span></p>
                <p><strong>Категория:</strong> <span id="modal-category"></span></p>
                <p><strong>Тип:</strong> <span id="modal-type"></span></p>
                <p><strong>Описание:</strong> <span id="modal-desc"></span></p>
            </div>
        </div>
    </div>

    <!-- МОДАЛЬНОЕ ОКНО: НОЛЬ ТРАВМ -->
    <div id="zeroInjuryModal" class="modal" onclick="closeZeroModal(event)">
        <div class="modal-content zero-modal-content" onclick="event.stopPropagation()">
            <div class="modal-header">
                <h3>🛡️ Программа «Ноль травм»</h3>
                <button type="button" class="close-btn" onclick="closeZeroModal(event)">✕</button>
            </div>
            <!-- Контент модального окна генерируется на бэкенде -->
            <div class="modal-body" style="max-height: 75vh; overflow-y: auto;">
                <asp:Literal ID="litZeroModalContent" runat="server" />
            </div>
        </div>
    </div>

    <!-- ScriptManager -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:HiddenField ID="hfMonthName" runat="server" />

    <script type="text/javascript">
        // =========================================================
        // ФУНКЦИИ ДЛЯ МОДАЛЬНОГО ОКНА ТРАВМ
        // =========================================================
        function showInfo(event, dateStr, hasInjury, category, type, desc) {
            if (event) {
                event.stopPropagation();
                event.preventDefault();
            }

            document.getElementById('modal-date').innerText = dateStr;
            if (hasInjury) {
                document.getElementById('modal-category').innerText = category || 'Не указана';
                document.getElementById('modal-type').innerText = type || 'Не указан';
                document.getElementById('modal-desc').innerText = desc || 'Нет описания';
            } else {
                document.getElementById('modal-category').innerText = 'Нет травмы';
                document.getElementById('modal-type').innerText = '-';
                document.getElementById('modal-desc').innerText = '-';
            }
            document.getElementById('modal').style.display = 'flex';
        }

        function closeModal(e) {
            if (e) {
                e.preventDefault();
                e.stopPropagation();
            }
            document.getElementById('modal').style.display = 'none';
        }

        // =========================================================
        // ФУНКЦИИ ДЛЯ МОДАЛЬНОГО ОКНА "НОЛЬ ТРАВМ"
        // =========================================================
        function openZeroModal() {
            document.getElementById('zeroInjuryModal').style.display = 'flex';
        }

        function closeZeroModal(e) {
            if (e) {
                e.preventDefault();
                e.stopPropagation();
            }
            document.getElementById('zeroInjuryModal').style.display = 'none';
        }

        // Обработка клавиши Escape для закрытия обоих модальных окон
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') {
                var modal = document.getElementById('modal');
                if (modal && modal.style.display === 'flex') {
                    closeModal(e);
                }
                
                var zeroModal = document.getElementById('zeroInjuryModal');
                if (zeroModal && zeroModal.style.display === 'flex') {
                    closeZeroModal(e);
                }
            }
        });
    </script>
</asp:Content>