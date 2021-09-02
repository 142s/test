using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;

namespace SalesMngTool.Page
{
    public partial class LogIn : System.Web.UI.Page
    {
        string ANS_ID = "user1";
        string ANS_PASS = "Cosmowinds@052";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["FreeCmpCodeMin"] = 100;
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Login();
        }

        private bool Login()
        {
            bool success = false;

            try
            {
                if (UserName_TextBox.Text == ANS_ID)
                {
                    if (Password_TextBox.Text == ANS_PASS)
                    {
                        // System.Diagnostics.Process.Start("https://cosmowinds365.sharepoint.com/sites/share/Shared%20Documents/Forms/AllItems.aspx?viewid=5fb2e03b%2D7cb3%2D4db6%2Da7d0%2Db67f5b65a3a9&id=%2Fsites%2Fshare%2FShared%20Documents%2F%E3%80%90SB%E4%BA%8B%E6%A5%AD%E9%83%A8%E3%80%91%5F%E7%A0%94%E4%BF%AE%E5%AE%A4%E5%85%B1%E6%9C%89%2F02%20%E7%A4%BE%E5%86%85%E9%96%8B%E7%99%BA%2F%E5%96%B6%E6%A5%AD%E6%94%AF%E6%8F%B4%E3%83%84%E3%83%BC%E3%83%AB");

                        Response.Redirect("ViewHistCon.aspx");
                        success = true;
                    }
                    else
                    {
                        Failure_Text.Text = "正しいユーザーID,パスワードを入力してください。";
                    }
                }
                else
                {
                    Failure_Text.Text = "正しいユーザーID,パスワードを入力してください。";
                }

            }
            catch
            {
                Failure_Text.Text = "正しいユーザーID,パスワードを入力してください。";
            }

            return success;
        }
    }
}
