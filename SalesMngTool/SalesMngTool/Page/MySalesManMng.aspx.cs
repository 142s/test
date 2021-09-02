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
    public partial class MySalesManMng : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            Own_View.DataSource = GetDataSet();
            Own_View.DataMember = GetData().TableName;
            Own_View.DataBind();

            Own_View.HeaderRow.Cells[0].Text = "自社担当者一覧";
        }

        // *** データベース接続用 ***
        private string GetConnectionString()
        {
            return @"Data Source=LAPTOP-V78PVCL1\SQLEXPRESS;"             // データベースサーバ
            + @"Integrated Security=False;"                     // Integrated Security
            + @"User ID=root;"                                  // SQL接続するユーザID
            + @"Password=password";                             // ユーザのパスワード
        }

        public DataTable GetData()
        {
            var table = new DataTable();
            GridView gridview = new GridView();

            // 接続文字列の取得
            var connectionString = GetConnectionString();

            using (var con = new SqlConnection(connectionString))
            using (var com = con.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    con.Open();

                    // SQLの設定
                    com.CommandText = @"SELECT count(*) FROM SalesSupportTool.dbo.OwnComRepre";

                    // SQLの実行
                    var adapter = new SqlDataAdapter(com);
                    adapter.Fill(table);
                }
                catch (Exception exception)
                {
                    Failure_Text.Text = "エラーが発生しました。";
                }
                finally
                {
                    // データベースの接続終了
                    con.Close();
                }
            }

            return table;
        }

        public DataSet GetDataSet()
        {

            var database = new DataSet();
            GridView gridview = new GridView();

            // 接続文字列の取得
            var connectionString = GetConnectionString();

            using (var con = new SqlConnection(connectionString))
            using (var com = con.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    con.Open();

                    // SQLの設定
                    com.CommandText = @"SELECT * FROM SalesSupportTool.dbo.OwnComRepre";

                    // SQLの実行
                    var adapter = new SqlDataAdapter(com);
                    adapter.Fill(database);
                }
                catch (Exception exception)
                {
                    Failure_Text.Text = "エラーが発生しました。";
                }
                finally
                {
                    // データベースの接続終了
                    con.Close();
                }
            }

            return database;
        }
        protected void Entry_Button_Click(object sender, EventArgs e)
        {


            try
            {
                if (Own_TextBox.Text == " ")
                {
                    Failure_Text.Text = "入力されていません。";
                    return;
                }
                else
                {

                    // 接続文字列の取得
                    var connectionString = GetConnectionString();

                    //SqlServer接続
                    using (SqlConnection con = new SqlConnection(connectionString))
                    using (var com = con.CreateCommand())
                    {
                        try
                        {
                            con.Open();
                            com.CommandText = @"insert into SalesSupportTool.dbo.OwnComRepre(OwnComRepre) values ('" + Own_TextBox.Text + "')";

                            com.ExecuteNonQuery();
                        }
                        catch (SqlException s)
                        {
                            Failure_Text.Text = "エラーが発生しました。";
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (SqlException s)
            {
                Failure_Text.Text = "エラーが発生しました。";
            }

        }
        protected void Delete_Button_Click(object sender, EventArgs e)
        {



            try
            {
                if (Own_TextBox.Text == " ")
                {
                    Failure_Text.Text = "入力されていません。";
                    return;
                }
                else
                {
                    // 接続文字列の取得
                    var connectionString = GetConnectionString();

                    //SqlServer接続
                    using (SqlConnection con = new SqlConnection(connectionString))
                    using (var com = con.CreateCommand())
                    {
                        try
                        {
                            con.Open();

                            com.CommandText = @"select * from SalesSupportTool.dbo.OwnComRepre where OwnComRepre = '" + Own_TextBox.Text + "'";
                            var reader = com.ExecuteReader();
                            if (reader.Read())
                            {
                                com.CommandText = @"delete from SalesSupportTool.dbo.OwnComRepre where OwnComRepre ='" + reader["OwnComRepre"] + "'";
                                reader.Close();
                                com.ExecuteNonQuery();
                            }
                            else
                            {
                                Failure_Text.Text = "該当する担当者が見つかりません。";
                            }


                        }
                        catch (SqlException s)
                        {
                            Failure_Text.Text = "エラーが発生しました。";
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (SqlException s)
            {
                Failure_Text.Text = "エラーが発生しました。";
            }
        }
    }
}
