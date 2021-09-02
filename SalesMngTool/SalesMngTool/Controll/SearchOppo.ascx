<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchOppo.ascx.cs" Inherits="SalesMngTool.normalHeader" %>
<hr />
日付<br />
<asp:TextBox ID="inDate" runat="server" ReadOnly="True"></asp:TextBox>
<br />
<asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
<br />
企業名<br />
<asp:DropDownList ID="DDListCompany" runat="server" Style="margin-bottom: 0px" Height="22px" Width="200px">
</asp:DropDownList>
<br />
<br />
顧客側担当者<br />
<asp:DropDownList ID="DDListCstRepre" runat="server" Height="22px" Width="200px">
</asp:DropDownList>
<br />
<br />
自社担当者<br />
<asp:DropDownList ID="DDListOwnComRepre" runat="server" Height="22px" Width="200px">
</asp:DropDownList>
<br />
<br />
契約形態<br />
<asp:DropDownList ID="DDListConShape" runat="server" CssClass="DropDownListClass" Height="22px" Width="200px">
</asp:DropDownList>
<br />
<br />
接触形式<br />
<asp:DropDownList ID="DDListConType" runat="server" Height="22px" Width="200px">
</asp:DropDownList>
<br />
<br />
<asp:Button ID="BtnSerach" runat="server" OnClick="BtnSerach_Click" Text="検索" />
<asp:Button ID="BtnClr" runat="server" Text="クリア" OnClick="BtnClr_Click" />
<br />
<br />

<asp:Button ID="BtnJnpMySales" runat="server" OnClick="BtnJnpMySales_Click" Text="自社担当者管理" />
<asp:Button ID="BtnJnpRegistCon" runat="server" OnClick="BtnJnpRegistCon_Click" Text="連絡履歴登録" />
<asp:Button ID="BtnJnpViewPrtnr" runat="server" OnClick="BtnJnpViewPrtnr_Click" Text="顧客側担当者一覧" />
<asp:Button ID="BtnJnpRegistMaster" runat="server" OnClick="BtnJnpRegistMaster_Click" Text="マスタ登録" />
<asp:Button ID="BtnJnpHome" runat="server" OnClick="BtnJnpHome_Click" Text="ホーム" />
