<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="birthday.aspx.cs" Inherits="WEB_Scrin_ASP.birthday" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Стили, специфичные для страницы дней рождения */
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
        }

        .birthday-header {
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

        .birthday-grid {
            flex: 1;
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 1vh 1vw;
            overflow: hidden;
            min-height: 0;
        }

        .birthday-card {
            background: rgba(30, 40, 60, 0.6);
            backdrop-filter: blur(4px);
            border-radius: 16px;
            padding: 1vh 1vw;
            display: flex;
            align-items: center;
            border: 1px solid rgba(255, 255, 255, 0.1);
            box-shadow: 0 4px 10px rgba(0,0,0,0.3);
            transition: transform 0.2s;
            height: 100%;
            box-sizing: border-box;
        }

        .birthday-card:nth-child(odd) {
            background: rgba(40, 50, 70, 0.7);
        }

        .birthday-icon {
            font-size: clamp(1.2rem, 3vh, 2rem);
            margin-right: 1vw;
            color: #ffb347;
        }

        .birthday-name {
            font-size: clamp(0.9rem, 2.2vh, 1.4rem);
            font-weight: 500;
            color: #ffffff;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            flex: 1;
        }

        .birthday-date {
            font-size: clamp(0.8rem, 2vh, 1.2rem);
            font-weight: 400;
            color: #a0b0c0;
            margin-left: 1vw;
            white-space: nowrap;
        }

        /* Адаптация для очень маленьких экранов */
        @media (max-width: 768px) {
            .birthday-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="birthday-container">
        <div class="birthday-header">
            <asp:Label ID="Label1" runat="server" CssClass="" Text="" />
        </div>

        <div class="birthday-grid">
            <asp:Repeater ID="RepeaterBirthdays" runat="server">
                <ItemTemplate>
                    <div class="birthday-card">
                        <span class="birthday-icon">🎂</span>
                        <span class="birthday-name"><%# Eval("FullName") %></span>
                        <span class="birthday-date"><%# Eval("DisplayDate") %></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="50000" />
</asp:Content>