<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="SalesMngTool.Page.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <style type="text/css">
        .auto-style5 {
            text-align: center;
            height: 42px;
        }
        .auto-style6 {
            font-size: xx-large;
        }
        .auto-style7 {
            text-align: center;
        }
        .auto-style8 {
            font-size: x-large;
        }
        .auto-style10 {
            text-align: center;
            height: 21px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
          <div class="auto-style5">
            <asp:Label ID="Title_Label" runat="server" Text="営業支援ツール" CssClass="auto-style6"></asp:Label>
          </div>
          <p class="auto-style7">
            &nbsp;
            <asp:Label ID="UserName_Label" runat="server"  Text="ユーザ名" style="font-size: x-large"></asp:Label>
          </p>
          <p class="auto-style7">
            <asp:TextBox ID="UserName_TextBox" runat="server" Height="20px" Width="170px"></asp:TextBox>
          </p>
          <p class="auto-style7">
            <asp:Label ID="Password_Label" runat="server" Text="パスワード" style="font-size: x-large"></asp:Label>
          </p>
          <p class="auto-style7">
            <asp:TextBox ID="Password_TextBox" runat="server" Height="20px" TextMode="Password" Width="170px" CssClass="auto-style8"></asp:TextBox>
          </p>
          <p class="auto-style7">
            <asp:Button ID="Login_Button" runat="server" CommandName="Login" Height="47px" OnClick="LoginButton_Click" Text="ログイン" Width="100px" />                
          </p>
          <p class="auto-style7" style="color:Red;">
            <asp:Literal ID="Failure_Text" runat="server" EnableViewState="False"></asp:Literal>
          </p>
    </form>
</body>
</html>
