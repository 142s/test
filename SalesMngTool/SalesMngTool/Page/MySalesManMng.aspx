<%@ Page Title="" Language="C#" MasterPageFile="~/salesMng.Master" AutoEventWireup="true" CodeBehind="MySalesManMng.aspx.cs" Inherits="SalesMngTool.Page.MySalesManMng" %>

<%@ Register Src="../Controll/SearchOppo.ascx" TagName="header" TagPrefix="my" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <my:header ID="header" runat="server" />

        <style type="text/css">
        .auto-style1 {
            font-size: small;
        }
        .auto-style2 {
            text-align: left;
            height: 25px;
        }
        .auto-style3 {
            text-align: left;
            height: 49px;
        }
        .auto-style4 {
            text-align: center;
            height: 49px;
            width: 306px;
        }
        .auto-style6 {
            width: 306px;
            text-align: center;
        }
        .auto-style7 {
            text-align: left;
        }
        .auto-style8 {
            height: 49px;
        }
        .auto-style11 {
            width: 169px;
            height: 175px;
        }
        .auto-style12 {
            text-align: center;
            height: 49px;
            width: 169px;
        }
        .auto-style13 {
            width: 169px;
            text-align: center;
        }
        .auto-style14 {
            width: 306px;
            height: 175px;
            text-align: center;
        }
        .auto-style15 {
            height: 175px;
        }
    </style>


    <layouttemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0" style="height:463px;width:837px;">
                                <tr>
                                    <td class="auto-style2" colspan="3">
                                        <asp:Label ID="Own_Label" runat="server"  CssClass="auto-style1">自社担当者管理</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3" colspan="3"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style12"></td>
                                    <td class="auto-style4">
                                        <asp:Label ID="OwnName_Label" runat="server" >自社担当者名</asp:Label>
                                    </td>
                                    <td class="auto-style7" rowspan="2">
                                        <asp:Button ID="Entry_Button" runat="server" Height="63px" OnClick="Entry_Button_Click" Text="登録" Width="94px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style13">&nbsp;</td>
                                    <td class="auto-style6">
                                        <asp:TextBox ID="Own_TextBox" runat="server"> </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="auto-style11"></td>
                                    <td class="auto-style14">
                                        <asp:Panel ID="Panel1" runat="server" Height="143px" ScrollBars="Both" Width="257px">
                                            <asp:GridView ID="Own_View" runat="server" Width="100%" Height="100%">
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style15">
                                        <asp:Button ID="Delete_Button" runat="server" Height="63px" OnClick="Delete_Button_Click" Text="削除" Width="94px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="auto-style8" colspan="3" style="color:Red;">
                                        <asp:Literal ID="Failure_Text" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="3">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </layouttemplate>

</asp:Content>
