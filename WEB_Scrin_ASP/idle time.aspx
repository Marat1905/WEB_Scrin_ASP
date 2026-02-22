<%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="idle time.aspx.cs" Inherits="WEB_Scrin_ASP.idle_time" Async="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <meta http-equiv="refresh" content="65"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server"> 
   
     <div class="content_Mod">
         <div class="status_mod">

              <asp:TextBox ID="textbox200" Class="textbox_break_Mod" runat="server" ReadOnly="True"  />
         </div>
      
          <div class="status_Mod_Line"> 

               <div id="Electro_smail" class="layer_Stag" runat="server"> 

                   <div class="row_Rif">
                       <asp:label ID="Label4" CssClass="label_slujba" runat="server">Простои Электрослужба</asp:label>
                   </div>

                    <div class="row_Prob">
                  
                   </div>
                   <div class="row_Rif">
                   <div class="col-2-1Fi">
                <asp:label ID="Label11" CssClass="label_prostoi" runat="server">За тек. месяц</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox_chas_1" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th class="sub" id="textbox_min_1" runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
            
                   </div>
                   <div class="row_Rif">
                  
                 <div class="col-2-1Fi">
                <asp:label ID="Label1" CssClass="label_prostoi" runat="server">За пред. месяц</asp:label>
                 </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox4" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox2" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
            
                   </div>
                   <div class="row_Rif">

                                         
                 <div class="col-2-1Fi">
                <asp:label ID="Label2" CssClass="label_prostoi" runat="server">За тек. год</asp:label>
            </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox8" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox6" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

                   </div>
                   <div class="row_Rif">

                         <div class="col-2-1Fi">
                <asp:label ID="Label3" CssClass="label_prostoi" runat="server">За пред. год</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox12" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox10" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

   </div>
            
  </div>



               <div id="Mex_smail" class="layer_Stag" runat="server">


                    <div class="row_Rif">
                       <asp:label ID="Label5" CssClass="label_slujba" runat="server">Простои службы механиков</asp:label>
                   </div>

                    <div class="row_Prob">
                  
                   </div>
                   <div class="row_Rif">
                   <div class="col-2-1Fi">
                <asp:label ID="Label6" CssClass="label_prostoi" runat="server">За тек. месяц</asp:label>
            </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox16" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox14" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">
                  
                 <div class="col-2-1Fi">
                <asp:label ID="Label7" CssClass="label_prostoi" runat="server">За пред. месяц</asp:label>
            </div>
              <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox20" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox18" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

                   </div>
                   <div class="row_Rif">

                                         
                 <div class="col-2-1Fi">
                <asp:label ID="Label8" CssClass="label_prostoi" runat="server">За тек. год</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox24" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox22" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                         <div class="col-2-1Fi">
                <asp:label ID="Label9" CssClass="label_prostoi" runat="server">За пред. год</asp:label>
            </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox28" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox26" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

   </div>
            
               </div>
               <div id="Tex_smail"  class="layer_Stag" runat="server">

                      <div class="row_Rif">
                       <asp:label ID="Label10" CssClass="label_slujba" runat="server">Простои технологов</asp:label>
                   </div>

                    <div class="row_Prob">
                  
                   </div>
                   <div class="row_Rif">
                   <div class="col-2-1Fi">
                <asp:label ID="Label12" CssClass="label_prostoi" runat="server">За тек. месяц</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox32" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox30" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">
                  
                 <div class="col-2-1Fi">
                <asp:label ID="Label13" CssClass="label_prostoi" runat="server">За пред. месяц</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox36" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox34" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                                         
                 <div class="col-2-1Fi">
                <asp:label ID="Label14" CssClass="label_prostoi" runat="server">За тек. год</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox40" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox38" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                         <div class="col-2-1Fi">
                <asp:label ID="Label15" CssClass="label_prostoi" runat="server">За пред. год</asp:label>
            </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox44" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox42" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

   </div>
            


               </div>

        
          </div>
         <div class="status_Mod_Line"> 
               <div class="layer_Stag">

                      <div class="row_Rif">
                       <asp:label ID="Label16" CssClass="label_slujba" runat="server">Плановые работы и ПТО</asp:label>
                   </div>

                    <div class="row_Prob">
                  
                   </div>
                   <div class="row_Rif">
                   <div class="col-2-1Fi">
                <asp:label ID="Label17" CssClass="label_prostoi" runat="server">За тек. месяц</asp:label>
            </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox48" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox46" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">
                  
                 <div class="col-2-1Fi">
                <asp:label ID="Label18" CssClass="label_prostoi" runat="server">За пред. месяц</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox52" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox50" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                                         
                 <div class="col-2-1Fi">
                <asp:label ID="Label19" CssClass="label_prostoi" runat="server">За тек. год</asp:label>
            </div>
           <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox56" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox54" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

                   </div>
                   <div class="row_Rif">

                         <div class="col-2-1Fi">
                <asp:label ID="Label20" CssClass="label_prostoi" runat="server">За пред. год</asp:label>
            </div>
           <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox60" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox58" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

   </div>
            




               </div>
               <div class="layer_Stag"> 


                                        <div class="row_Rif">
                       <asp:label ID="Label21" CssClass="label_slujba" runat="server">Прочее</asp:label>
                   </div>

                    <div class="row_Prob">
                  
                   </div>
                   <div class="row_Rif">
                   <div class="col-2-1Fi">
                <asp:label ID="Label22" CssClass="label_prostoi" runat="server">За тек. месяц</asp:label>
            </div>
           <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox64" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox62" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">
                  
                 <div class="col-2-1Fi">
                <asp:label ID="Label23" CssClass="label_prostoi" runat="server">За пред. месяц</asp:label>
            </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox68" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox66" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                                         
                 <div class="col-2-1Fi">
                <asp:label ID="Label24" CssClass="label_prostoi" runat="server">За тек. год</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox72" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox70" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                         <div class="col-2-1Fi">
                <asp:label ID="Label25" CssClass="label_prostoi" runat="server">За пред. год</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="textbox76" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="textbox74" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

   </div>

               </div>
               <div class="layer_Stag"> 

                                        <div class="row_Rif">
                       <asp:label ID="Label26" CssClass="label_slujba" runat="server">Общее</asp:label>
                   </div>

                    <div class="row_Prob">
                  
                   </div>
                   <div class="row_Rif">
                   <div class="col-2-1Fi">
                <asp:label ID="Label27" CssClass="label_prostoi" runat="server">За тек. месяц</asp:label>
            </div>
           <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="Th1" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="Th2" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">
                  
                 <div class="col-2-1Fi">
                <asp:label ID="Label28" CssClass="label_prostoi" runat="server">За пред. месяц</asp:label>
            </div>
             <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="Th3" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="Th4" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                                         
                 <div class="col-2-1Fi">
                <asp:label ID="Label29" CssClass="label_prostoi" runat="server">За тек. год</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="Th5" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="Th6" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>
                   </div>
                   <div class="row_Rif">

                         <div class="col-2-1Fi">
                <asp:label ID="Label30" CssClass="label_prostoi" runat="server">За пред. год</asp:label>
            </div>
            <div class="col-2-2Fit">
                <table class="table_q">
                    <tr>
                      <th id="Th7" class="sub" runat="server"></th>
                      <th class="lbl">час</th>
                      <th id="Th8" class="sub"  runat="server"></th>
                      <th class="lbl">мин.</th>
                   </tr>
                </table>

            </div>

   </div>
               </div>
         </div>

    </div>
    

        
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="40000">
            </asp:Timer>
        </div>
    
</asp:Content>
