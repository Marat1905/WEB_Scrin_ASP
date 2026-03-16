<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB_Scrin_ASP.Default" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <style>
        /* Переопределения для равномерного распределения строк по высоте */
        .layer1, .layer2 {
            display: flex;
            flex-direction: column;
            gap: 0.5dvh;               /* отступ между секциями */
        }

        /* Базовая настройка секции – она будет flex-контейнером для своих строк */
        .section {
            display: flex;
            flex-direction: column;
            min-height: 0;
        }

        .section > .section-title {
            flex-shrink: 0;             /* заголовок не сжимается */
            margin-bottom: 0.3dvh;
        }

        /* Строки внутри секции равномерно делят её высоту */
        .section .row_Ri {
            flex: 1 1 0;
            min-height: 0;
            align-items: stretch;        /* растягиваем дочерние элементы по высоте */
            margin-bottom: 0.2dvh;       /* небольшой отступ между строками */
        }

        /* Текстовые поля занимают всю высоту строки и центрируют содержимое */
        .row_Ri .textbox_Ri,
        .row_Ri .textbox_Li,
        .row_Ri .textbox_otkl_red,
        .row_Ri .textbox_otkl_grin {
            height: 100%;
            box-sizing: border-box;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        /* Для маленьких экранов чуть уменьшаем отступы */
        @media (max-width: 768px) {
            .layer1, .layer2 {
                gap: 0.3dvh;
            }
            .section .row_Ri {
                margin-bottom: 0.1dvh;
            }
        }
    </style>
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
                <!-- Блок: Параметры работы БДМ (4 строки) -->
                <div class="section" style="flex: 4 1 0;">
                    <div class="section-title">📊 Параметры работы БДМ</div>
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
                </div>

                <!-- Блок: Обрывы (2 строки) -->
                <div class="section" style="flex: 2 1 0;">
                    <div class="section-title">⚠️ Обрывы полотна</div>
                    <div class="row_Ri">
                        <div class="col-1-1L"><asp:Label ID="Label7" CssClass="label_Ri" runat="server">Кол-во обрывов за тек. смену.</asp:Label></div>
                        <div class="col-1-2L"><asp:TextBox ID="textbox9" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-1-1L"><asp:Label ID="Label8" CssClass="label_Ri" runat="server">Кол-во обрывов за пред. смену.</asp:Label></div>
                        <div class="col-1-2L"><asp:TextBox ID="textbox11" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                    </div>
                </div>

                <!-- Блок: План на текущую смену (2 строки) -->
                <div class="section" style="flex: 2 1 0;">
                    <div class="section-title">⏱️ План на текущую смену</div>
                    <div class="row_Ri">
                        <div class="col-1-1L"><asp:Label ID="Label12" CssClass="label_Ri" runat="server">Заданный план.</asp:Label></div>
                        <div class="col-1-2L"><asp:TextBox ID="textbox27" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-1-1L"><asp:Label ID="Label16" CssClass="label_Ri" runat="server">Выполненный план.</asp:Label></div>
                        <div class="col-1-2L"><asp:TextBox ID="textbox29" Class="textbox_Ri" runat="server" ReadOnly="True" /></div>
                    </div>
                </div>

                <!-- Блок: План на текущий год (3 строки) -->
                <div class="section" style="flex: 3 1 0;">
                    <div class="section-title">📅 План на текущий год</div>
                    <div class="row_Ri">
                        <div class="col-1-1La"><asp:Label ID="Label1" CssClass="label_Ri" runat="server">Заданный план.</asp:Label></div>
                        <div class="col-1-2La"><asp:TextBox ID="textbox3" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-1-1La"><asp:Label ID="Label2" CssClass="label_Ri" runat="server">Выполненный план.</asp:Label></div>
                        <div class="col-1-2La"><asp:TextBox ID="textbox21" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-1-1La"><asp:Label ID="Label4" CssClass="label_Ri" runat="server">Отклонение от плана.</asp:Label></div>
                        <div class="col-1-2La"><asp:TextBox ID="textbox15" Class="textbox_otkl_red" runat="server" ReadOnly="True" /></div>
                    </div>
                </div>
            </div> <!-- левая колонка -->

            <!-- Правая колонка (layer2) -->
            <div class="layer2">
                <!-- Блок: План на текущий месяц (3 строки) -->
                <div class="section" style="flex: 3 1 0;">
                    <div class="section-title">📆 План на текущий месяц</div>
                    <div class="row_Ri">
                        <div class="col-1-1Li"><asp:Label ID="Label10" CssClass="label_Li" runat="server">Заданный план.</asp:Label></div>
                        <div class="col-1-2Li"><asp:TextBox ID="textbox23" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-1-1Li"><asp:Label ID="Label13" CssClass="label_Li" runat="server">Выполненный план.</asp:Label></div>
                        <div class="col-1-2Li"><asp:TextBox ID="textbox25" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-1-1Li"><asp:Label ID="Label14" CssClass="label_Li" runat="server">Отклонение от плана.</asp:Label></div>
                        <div class="col-1-2Li"><asp:TextBox ID="textbox19" Class="textbox_otkl_red" runat="server" ReadOnly="True" /></div>
                    </div>
                </div>

                <!-- Блок: Простои и работа БДМ (5 строк) -->
                <div class="section" style="flex: 5 1 0;">
                    <div class="section-title">⏳ Простои и работа БДМ</div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label11" CssClass="label_Li" runat="server">Работа БДМ (текущий год).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textbox100" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label15" CssClass="label_Li" runat="server">Простой БДМ (текущий год).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textbox33" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label17" CssClass="label_Li" runat="server">Простой БДМ (текущий месяц).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textbox37" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label18" CssClass="label_Li" runat="server">Простой БДМ (текущая смена).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textbox41" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label19" CssClass="label_Li" runat="server">Простой БДМ (пред. смена).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textbox45" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                </div>

                <!-- Блок: Показатели за предыдущие периоды (3 строки) -->
                <div class="section" style="flex: 3 1 0;">
                    <div class="section-title">📜 Предыдущие периоды</div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label20" CssClass="label_Li" runat="server">Выполненный план (пред. год).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textbox16" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label22" CssClass="label_Li" runat="server">Выполненный план (пред. месяц).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textboxPredMes" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                    <div class="row_Ri">
                        <div class="col-2-1Li"><asp:Label ID="Label21" CssClass="label_Li" runat="server">Выполненный план (пред. смена).</asp:Label></div>
                        <div class="col-2-time"><asp:TextBox ID="textboxPredSmena" Class="textbox_Li" runat="server" ReadOnly="True" /></div>
                    </div>
                </div>
            </div> <!-- правая колонка -->
        </div> <!-- columns -->
    </div> <!-- content_Mod -->

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
</asp:Content>