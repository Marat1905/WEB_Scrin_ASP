<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="Caution.aspx.cs" Inherits="WEB_Scrin_ASP.Caution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <meta http-equiv="refresh" content="80"/>
     <style type="text/css">
         .auto-style1 {
             width: 1234px;
         }
     </style>
     </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
        <div class="title_Fire">
<p id="label_Countion_lbl">В Н И М А Н И Е !!!!!</p>
<p id="label_Warn_txt"="label_Fio1">&nbsp;&nbsp;&nbsp; 14 апреля 2019 г. в ночное время, неустановленное лицо, незаконно проникнув на промплощадку ООО «Завод Николь-Пак», совершило умышленный поджог, хранящейся на территории макулатуры.

</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp; В результате указанных преступных действий предприятию причинен ущерб. 

</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp; Нашему предприятию просто необходимо дальнейшее развитие (замена верхнего напорного ящика, замена вакуумных насосов на турбовоздуходувки, установка парового ящика и т.д.) для улучшения качества ГП, снижения затрат на энергоресурсы.</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp;
Такие преступные действия могут отразится не только на предприятии в целом, но и на каждом работнике, что просто не допустимо. 

</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp; Администрация уверена, что основная часть коллектива, это честные добросовестные работники, которым не безразлична судьба предприятия.

</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp; Если Вам что - либо известно о личности лица, причастного к совершению преступления, либо имеется информация, относящаяся к поджогу, которую необходимо проверить, ООО «Завод Николь-Пак» просит сообщить по телефону: 

</p>
    <p id="label_Countion_Nomer">8 960 386 73 17</p>
    <p id="label_Warn_txt_bolt">Информацию возможно направить смс-сообщением или сообщением на WhatsApp Messenger

</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ООО «Завод Николь-Пак» гарантирует полную конфиденциальность и анонимность обращения, а также выплату материального вознаграждения в сумме 200 000 (двести тысяч) рублей за предоставление достоверной информации способствующей привлечению виновного лица к уголовной ответственности.

</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp; Также если Вам известны другие случаи (хищения, злоупотребление служебным положением и т.д.), которые противоречат интересам предприятия, просьба сообщать об этом по вышеуказанному телефону.</p>
    <p id="label_Warn_txt">&nbsp;&nbsp;&nbsp; 
В случае подтверждения изложенных фактов вознаграждение гарантируется. 
    <p id="label_Warn_Admin">Администрация предприятия </p>
        <div>
                </div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="70000">
            </asp:Timer>
        </div>
    </asp:Timer>
    </div>
</asp:Content>
