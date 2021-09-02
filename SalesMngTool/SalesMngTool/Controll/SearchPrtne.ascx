<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPrtne.ascx.cs" Inherits="SalesMngTool.Controll.SearchPrtne" %>
企業名<br />
<asp:TextBox ID="inCmpName" runat="server"></asp:TextBox>
<br />
<br />
顧客側担当者<br />
<asp:TextBox ID="inCstRepre" runat="server"></asp:TextBox>
<br />
<br />
部署名<br />
<asp:TextBox ID="inDepartment" runat="server"></asp:TextBox>
<br />
<br />
役職<br />
<asp:TextBox ID="inPosition" runat="server"></asp:TextBox>
<br />
<br />
連絡先<br />
<asp:TextBox ID="inConInfo" runat="server"></asp:TextBox>
<br />
<br />
直接連絡先<br />
<asp:TextBox ID="inDConInfo" runat="server"></asp:TextBox>
<br />
<br />
メールアドレス<br />
<asp:TextBox ID="inMail" runat="server"></asp:TextBox>
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
