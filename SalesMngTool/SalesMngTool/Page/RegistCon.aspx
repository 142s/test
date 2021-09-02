<%@ Page Title="" Language="C#" MasterPageFile="~/salesMng.Master" AutoEventWireup="true" CodeBehind="RegistCon.aspx.cs" Inherits="SalesMngTool.Page.RegistCon" %>

<%@ Register Src="../Controll/SearchOppo.ascx" TagName="header" TagPrefix="my" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <my:header ID="header" runat="server" />

    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="日付　"></asp:Label>
    <asp:Label ID="Label8" runat="server" Text="契約形態"></asp:Label>
    <br />
    <asp:TextBox ID="TextBoxDate" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
    <asp:Button ID="BtnCalendar" runat="server" OnClick="Button2_Click" Text="カレンダー" />
    <asp:DropDownList ID="DDListConShape" runat="server" CssClass="DropDownListClass" Height="22px" Width="200px">
    </asp:DropDownList>
    <asp:Calendar ID="Calendar1" runat="server" Height="137px" OnSelectionChanged="Calendar1_SelectionChanged1" Visible="False" Width="197px"></asp:Calendar>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="企業名"></asp:Label>
    接触形式<br />
    <asp:DropDownList ID="DDListCompany" runat="server" Style="margin-bottom: 0px" Height="22px" Width="200px">
    </asp:DropDownList>
    <asp:DropDownList ID="DDListConType" runat="server" Height="22px" Width="200px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="顧客担当者　"></asp:Label>
    <asp:Label ID="Label6" runat="server" Text="コメント"></asp:Label>
    <br />
    <asp:DropDownList ID="DDListCstRepre" runat="server" Height="22px" Width="200px">
    </asp:DropDownList>
    <asp:TextBox ID="TextBoxComment" runat="server" Height="16px" Width="278px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="自社担当者"></asp:Label>
    <br />
    <asp:DropDownList ID="DDListOwnComRepre" runat="server" Height="22px" Width="200px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Button ID="BtnEntry" runat="server" Text="登録" OnClick="BtnEntry_Click" Height="33px" Width="98px" />
    <br />



</asp:Content>
