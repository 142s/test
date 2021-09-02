using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SalesMngTool.Page
{
    public partial class RegistCon : System.Web.UI.Page
    {
        const string DATASOURCE = @"LAPTOP-V78PVCL1\SQLEXPRESS";
        const string USERID = "root";
        const string PASSWORD = "password";
        const string DBNAME = "SalesSupportTool";

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        DataTable dt = new DataTable();

        public void ListInitialize(SqlCommand command, string tableName, string columnName, DropDownList dropDownList)
        {
            //ドロップダウンリストのリストを削除
            dropDownList.Items.Clear();

            //ドロップダウンリストの初期値を挿入
            int num = 0;
            ListItem list = new ListItem("選択してください", num.ToString());
            dropDownList.Items.Add(list);
            num++;

            //項目取得SQL文
            command.CommandText = @"select * from SalesSupportTool.dbo." + tableName;
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                //ドロップダウンリストに項目を追加
                list = new ListItem(reader[columnName] as string, num.ToString());
                dropDownList.Items.Add(list);
                num++;
            }
            reader.Close();
        }

        //メッセージを表示する処理
        public void MessageBox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                builder.DataSource = DATASOURCE;   // 接続先の SQL Server インスタンス
                builder.UserID = USERID;              // 接続ユーザー名
                builder.Password = PASSWORD; // 接続パスワード
                builder.InitialCatalog = DBNAME;  // 接続するデータベース

                //SqlServer接続
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    var command = connection.CreateCommand();

                    //１度だけ行う処理
                    if (!IsPostBack)
                    {
                        //各ドロップダウンリストの初期化
                        ListInitialize(command, "Company", "CmpName", DDListCompany);
                        ListInitialize(command, "CstRepre", "CstRepre", DDListCstRepre);
                        ListInitialize(command, "OwnComRepre", "OwnComRepre", DDListOwnComRepre);
                        ListInitialize(command, "ConShape", "ConShape", DDListConShape);
                        ListInitialize(command, "ConType", "ConType", DDListConType);
                    }
                    //接続解除
                    connection.Close();
                }
            }
            catch (SqlException s)
            {
                MessageBox(s.ToString());
            }

            if (TextBoxDate.Text == "")
            {
                TextBoxDate.Text = DateTime.Today.ToString("D");
            }

        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            //カレンダーの表示/非表示
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }


        }



        //カレンダーの日付が選択させた時の処理
        protected void Calendar1_SelectionChanged1(object sender, EventArgs e)
        {
            //選択した日付をTextBoxに表示
            TextBoxDate.Text = Calendar1.SelectedDate.ToString("D");
            //カレンダーを非表示にする
            Calendar1.Visible = false;
        }


        protected void BtnEntry_Click(object sender, EventArgs e)
        {
            //ドロップダウンリストの選択確認
            if (DDListCompany.SelectedIndex == 0 || DDListCstRepre.SelectedIndex == 0 ||
                DDListOwnComRepre.SelectedIndex == 0 || DDListConShape.SelectedIndex == 0 ||
                DDListConType.SelectedIndex == 0)
            {
                MessageBox("選択されていない項目があります");
                return;
            }

            try
            {
                builder.DataSource = DATASOURCE;   // 接続先の SQL Server インスタンス
                builder.UserID = USERID;              // 接続ユーザー名
                builder.Password = PASSWORD; // 接続パスワード
                builder.InitialCatalog = DBNAME;  // 接続するデータベース

                //SqlServer接続
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    int i = 0;
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select * from SalesSupportTool.dbo.Company where CmpName='" + DDListCompany.SelectedItem + "'";
                    var reader = command.ExecuteScalar();

                    int cmpCode = Int32.Parse(reader.ToString());


                    DateTime date = DateTime.Parse(TextBoxDate.Text);

                    command.CommandText = @"insert into SalesSupportTool.dbo.ComHistory(CmpCode,Date,CstRepre,OwnComRepre, ConShape,ConType,Comment) values (" + cmpCode + ",'" + date + "','" + DDListCstRepre.SelectedItem + "','" + DDListOwnComRepre.SelectedItem + "','" + DDListConShape.SelectedItem + "','" + DDListConType.SelectedItem + "','" + TextBoxComment.Text + "')";


                    command.ExecuteNonQuery();
                }

                Response.Redirect("ViewHistCon.aspx");
            }
            catch (SqlException s)
            {
                MessageBox(s.ToString());
            }
        }
    }
}