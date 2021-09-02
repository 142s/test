<%@ Page Title="" Language="C#" MasterPageFile="~/salesMng.Master" AutoEventWireup="true" CodeBehind="RegistMaster.aspx.cs" Inherits="SalesMngTool.Page.RegistMaster" %>

<%@ Register Src="../Controll/SearchOppo.ascx" TagName="header" TagPrefix="my" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <my:header ID="header" runat="server" />

    <p>
        <asp:Label ID="Label_CmpCode" runat="server" Text="企業コード"></asp:Label>
        <asp:Label ID="Label_ConInfo" runat="server" Text="連絡先"></asp:Label>
    </p>
    <asp:TextBox ID="TextBox_CmpCode" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox_ConInfo" runat="server"></asp:TextBox>
    <p>
        <asp:Label ID="Label_CmpName" runat="server" Text="企業名"></asp:Label>
        <asp:Label ID="Label_DConInfo" runat="server" Text="直接連絡先"></asp:Label>
    </p>
    <p>
        <asp:TextBox ID="TextBox_CmpName" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox_DConInfo" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label_CstRepre" runat="server" Text="顧客側担当者"></asp:Label>
        <asp:Label ID="Label_Mail" runat="server" Text="メールアドレス"></asp:Label>
    </p>
    <p>
        <asp:TextBox ID="TextBox_CstRepre" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox_Mail" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label_Department" runat="server" Text="部署名"></asp:Label>
        <asp:Label ID="Label_Role" runat="server" Text="役割"></asp:Label>
    </p>
    <p>
        <asp:TextBox ID="TextBox_Department" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox_Role" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label_Position" runat="server" Text="役職"></asp:Label>
    </p>
    <p>
        <asp:TextBox ID="TextBox_Position" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label_Message" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Button ID="Button_Regist" runat="server" Text="登録" OnClick="Button_Regist_Click" />
    </p>


</asp:Content>
