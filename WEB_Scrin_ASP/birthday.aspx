 <%@ Page Title="" Language="C#" MasterPageFile="~/Screen.Master" AutoEventWireup="true" CodeBehind="birthday.aspx.cs" Inherits="WEB_Scrin_ASP.birthday" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="title">
         <div class="DN">
            <%-- --%>
        <asp:Label ID="Label1" runat="server" CssClass="label_DN" Text=""> </asp:Label>
              </div>
        
        <marquee direction="up"  scrollamount=3  width="600px" height="520px">
                             
                                  <div class="step">
                                  <asp:Label ID="Label2" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                      </div>
                                  <div class="step1">
                                   <asp:Label ID="Label3" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label4" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label5" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label6" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label7" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label8" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label9" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label10" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label11" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                   <div class="step">
                                  <asp:Label ID="Label12" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                      </div>
                                  <div class="step1">
                                   <asp:Label ID="Label13" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label14" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label15" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label16" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label17" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label18" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label19" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                  <div class="step">
                                   <asp:Label ID="Label20" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label21" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div>
                                   <div class="step">
                                   <asp:Label ID="Label22" runat="server" CssClass="label_Fio" Text=""></asp:Label>
                                       </div>
                                  <div class="step1">
                                   <asp:Label ID="Label23" runat="server" CssClass="label_Fio1" Text=""></asp:Label>
                                       </div></marquee>
        
           </div>

 <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="50000">
            </asp:Timer>
        </div>
    </asp:Timer>
    </div>
</asp:Content>
