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
    public partial class EditHistCon : System.Web.UI.Page
    {
        const string DATASOURCE = @"LAPTOP-V78PVCL1\SQLEXPRESS";
        const string USERID = "root";
        const string PASSWORD = "password";
        const string DBNAME = "SalesSupportTool";
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        DataTable dt = new DataTable();

        int id = 1;

        public void ListIntialize(SqlCommand command, string tableName, string columnName, DropDownList dropDownList)
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

        public void InitializeDropDownListText(SqlCommand command, string columnName, int id, DropDownList dropDownList)
        {
            //Sql文
            command.CommandText = @"select * from SalesSupportTool.dbo.ComHistory where id = " + id + ";";
            var reader = command.ExecuteReader();
            string values = "";
            //値を取得
            while (reader.Read())
            {
                values = reader[columnName].ToString();
            }

            if (columnName == "CmpCode")
            {
                reader.Close();
                command.CommandText = @"select * from SalesSupportTool.dbo.Company where " + columnName + " = " + values + ";";
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    values = reader["CmpName"] as string;
                }
            }


            int index = 0;
            //データベースの値を初期値として入れる
            foreach (ListItem item in dropDownList.Items)
            {
                //ドロップダウンリストとデータベースの値を比べる
                if (item.Text == values)
                {
                    dropDownList.SelectedIndex = index;
                    break;
                }
                index++;
            }

            reader.Close();
        }

        public void InitializeTextBox(SqlCommand command, string columnName, int id, TextBox textBox)
        {
            command.CommandText = @"select * from SalesSupportTool.dbo.ComHistory where id = " + id + ";";
            var reader = command.ExecuteReader();
            if (columnName == "Date")
            {
                DateTime dateTime = DateTime.MaxValue;
                //値を取得
                while (reader.Read())
                {
                    dateTime = (DateTime)reader[columnName];
                }
                textBox.Text = dateTime.ToString("yyyy/MM/dd");
            }
            else if (columnName == "Comment")
            {
                //値を取得
                while (reader.Read())
                {
                    textBox.Text = reader[columnName] as string;
                }
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
                //builder.MultipleActiveResultSets = true;

                //SqlServer接続
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {

                    connection.Open();
                    var command = connection.CreateCommand();

                    //１度だけ行う処理
                    if (!IsPostBack)
                    {
                        //各ドロップダウンリストの初期化
                        ListIntialize(command, "Company", "CmpName", DDListCompany);
                        ListIntialize(command, "CstRepre", "CstRepre", DDListCstRepre);
                        ListIntialize(command, "OwnComRepre", "OwnComRepre", DDListOwnComRepre);
                        ListIntialize(command, "ConShape", "ConShape", DDListConShape);
                        ListIntialize(command, "ConType", "ConType", DDListConType);
                        //データベースから値を初期値として入れる
                        InitializeDropDownListText(command, "CmpCode", id, DDListCompany);
                        InitializeDropDownListText(command, "CstRepre", id, DDListCstRepre);
                        InitializeDropDownListText(command, "OwnComRepre", id, DDListOwnComRepre);
                        InitializeDropDownListText(command, "ConShape", id, DDListConShape);
                        InitializeDropDownListText(command, "ConType", id, DDListConType);
                        InitializeTextBox(command, "Date", id, TextBoxDate);
                        InitializeTextBox(command, "Comment", id, TextBoxComment);
                    }
                    //接続解除
                    connection.Close();
                }
            }
            catch (SqlException s)
            {
                MessageBox(s.ToString());
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
                //SqlServer接続
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select * from SalesSupportTool.dbo.Company where CmpName='" + DDListCompany.SelectedItem + "'";
                    var reader = command.ExecuteScalar();

                    int cmpCode = Int32.Parse(reader.ToString());


                    DateTime date = DateTime.Parse(TextBoxDate.Text);

                    command.CommandText = @"update SalesSupportTool.dbo.ComHistory set CmpCode = " + cmpCode + ",Date = '" + date + "',CstRepre = '" + DDListCstRepre.SelectedItem + "',OwnComRepre = '" + DDListOwnComRepre.SelectedItem + "', ConShape = '" + DDListConShape.SelectedItem + "',ConType = '" + DDListConType.SelectedItem + "',Comment = '" + TextBoxComment.Text + "' where id = " + id;


                    command.ExecuteNonQuery();
                }

                Session["Date"] = Session["CmpName"] = Session["CstRepre"] = Session["OwnComRepre"]
                = Session["ConShape"] = Session["ConType"] = Session["Comment"] = null;

                Response.Redirect("ViewHistCon.aspx");
            }
            catch (SqlException s)
            {
                MessageBox(s.ToString());
            }
        }

        protected void BtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlServer接続
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"select * from SalesSupportTool.dbo.Company where CmpName='" + DDListCompany.SelectedItem + "'";
                    var reader = command.ExecuteScalar();

                    int cmpCode = Int32.Parse(reader.ToString());


                    DateTime date = DateTime.Parse(TextBoxDate.Text);

                    command.CommandText = @"delete from SalesSupportTool.dbo.ComHistory where id = " + id;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException s)
            {
                MessageBox(s.ToString());
            }
        }
    }
}