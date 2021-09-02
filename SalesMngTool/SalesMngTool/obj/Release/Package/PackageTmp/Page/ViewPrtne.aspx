<%@ Page Title="" Language="C#" MasterPageFile="~/salesMng.Master" AutoEventWireup="true" CodeBehind="ViewPrtne.aspx.cs" Inherits="SalesMngTool.Page.prtneMng" %>

<%@ Register Src="../Controll/SearchPrtne.ascx" TagName="header" TagPrefix="my" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <my:header ID="header" runat="server" />

    <div>
        <asp:Label ID="Label_CstRepreList" runat="server" Text="顧客側担当者一覧"></asp:Label>
    </div>
    <asp:Panel ID="ListFlame" runat="server" Height="190px" ScrollBars="Vertical" BorderStyle="Solid" BorderWidth="2px">
        <asp:GridView ID="GridView_CstRepreList" runat="server" OnRowCommand="GridView_CstRepreList_RowCommand" Width="100%" OnSelectedIndexChanged="GridView_CstRepreList_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ButtonType="Button" ShowCancelButton="False" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Label ID="Label_Message" runat="server"></asp:Label>
    <p>
        <asp:Button ID="Button_GoEdit" runat="server" OnClick="Button_GoEdit_Click" Text="編集へ" />
    </p>


</asp:Content>
