<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="smena.aspx.cs" Inherits="WEB_Scrin_ASP.smena" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <meta http-equiv="refresh" content="65"/>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
      <div class="content_Mod">
         <div class="status_Mod">

              <asp:TextBox ID="textbox200" Class="textbox_break_Mod" runat="server" ReadOnly="True"  />
         </div>
          <div class="status_Mod1">

             
        
                <div class="status_Sm_Line"> 
                     <div class="Idle_Mod_Line1">
                         <asp:Label ID="Label1" Class=" label_mes_txt" runat="server" Text="Label">Текущий месяц</asp:Label>
                     </div>

                    <div class="Idle_Mod_Line2"> 

   <table class="smena">
<tr>
  <th class="lbl1"></th>
  <th class="lbl1">Смена</th>
  <th  class="lbl1">Кол-во <br /> смен</th>
  <th  class="lbl1">Обрывы <br /> сеточ.</th>
  <th  class="lbl1">Обрывы <br /> суш.</th>
  <th  class="lbl2">План</th>
      <th class="lbl2">Факт.</th>
      <th class="lbl2">Отклонение</th>
      <th class="lbl2">Простои</th>
      <th class="lbl2">Средняя<br /> сменная выр-ка</th>
  </tr>
 <tr>
  <td class="round-top">1 место</td>
  <td><asp:TextBox ID="Tab_1_1_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_1_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
 </tr>
<tr>
  <td>2 место</td>
  <td><asp:TextBox ID="Tab_1_2_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_2_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
</tr>
<tr>
  <td>3 место</td>
  <td><asp:TextBox ID="Tab_1_3_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_3_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
</tr>
<tr class="size2">
  <td class="round-bottom">4 место</td>
  <td><asp:TextBox ID="Tab_1_4_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_1_4_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
</tr>
</table>


                    </div>
                </div>
          
               

   
         <div class="status_Sm_Line">

                    <div class="Idle_Mod_Line1" > 
                         <asp:Label ID="Label2" Class=" label_mes_txt" runat="server" Text="Label">Предыдущий месяц</asp:Label>
                     </div>
              <div class="Idle_Mod_Line2"> 
  <table class="smena">
<tr>
  <th class="lbl1"></th>
  <th class="lbl1">Смена</th>
  <th  class="lbl1">Кол-во <br /> смен</th>
  <th  class="lbl1">Обрывы <br /> сеточ.</th>
  <th  class="lbl1">Обрывы <br /> суш.</th>
  <th  class="lbl2">План</th>
      <th class="lbl2">Факт.</th>
      <th class="lbl2">Отклонение</th>
      <th class="lbl2">Простои</th>
      <th class="lbl2">Средняя<br /> сменная выр-ка</th>
 <tr>
  <td class="round-top">1 место</td>
  <td><asp:TextBox ID="Tab_2_1_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_1_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
 </tr>
<tr>
  <td>2 место</td>
  <td><asp:TextBox ID="Tab_2_2_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_2_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
</tr>
<tr>
  <td>3 место</td>
  <td><asp:TextBox ID="Tab_2_3_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_3_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
</tr>
<tr>
  <td class="round-bottom">4 место</td>
  <td><asp:TextBox ID="Tab_2_4_1" class="shift" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_2" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_3" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_4" class="sm" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_5" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_6" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_7" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_8" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
  <td><asp:TextBox ID="Tab_2_4_9" class="PL" runat="server" ReadOnly="True"></asp:TextBox></td>
</tr>
</table>


            </div>

        </div>
     </div>

    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="40000">
            </asp:Timer>
        </div>
    </asp:Timer>
    </div>

</asp:Content>
