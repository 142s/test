<%@ Page Title="" Language="C#" MasterPageFile="~/salesMng.Master" AutoEventWireup="true" CodeBehind="ViewHistCon.aspx.cs" Inherits="SalesMngTool.Page.ViewHistCon" %>

<%@ Register Src="../Controll/SearchOppo.ascx" TagName="header" TagPrefix="my" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <my:header ID="header" runat="server" />
    <style type="text/css">
        #form1 {
            height: 251px;
        }
    </style>

    <div style="height: 195px">
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Both">
            <asp:GridView ID="GridView1" runat="server" OnRowCommand="test" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%">
                <Columns>
                    <asp:CommandField ButtonType="Button" ShowCancelButton="False" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="編集" />
    </div>
</asp:Content>
