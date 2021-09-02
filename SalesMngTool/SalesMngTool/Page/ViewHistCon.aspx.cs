using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SalesMngTool.Page
{
    public partial class ViewHistCon : System.Web.UI.Page
    {
        // === 定数 ===
        // データベースサーバ
        const string DB_SERVER_NAME = @"Data Source=LAPTOP-V78PVCL1\SQLEXPRESS;";
        //const string DB_SERVER_NAME_HOME = @"Data Source=DESKTOP-T0VJFQG\SQLEXPRESS;";
        // システムメッセージ
        const string MESSAGE_ERR_CSTSELECT = "連絡履歴情報が選択されていません。";


        protected void Page_Load(object sender, EventArgs e)
        {
            // 初回のみ実行する
            if (!IsPostBack)
            {
                // グリッドビュー設定
                GridViewOption();
            }

            // 検索結果の場合
            //SearchCstRepre();
            SearchCnctHis();
        }
        protected void test(object sender, EventArgs e)
        {
            // 選択した行の背景色を変更
            //selectRow=選択した行
            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.Blue;
        }


        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    Label1.Text = TextBox1.Text + "さん、こんにちは。";

        //    Label1.Text = GetData().Rows.ToString();
        //}

        // *** データベース接続用 ***
        private string GetConnectionString()
        {
            return DB_SERVER_NAME                               // データベースサーバ
                    + @"Integrated Security=False;"             // Integrated Security
                    + @"User ID=root;"                          // SQL接続するユーザID
                    + @"Password=password";                     // ユーザのパスワード
        }

        private void GridViewOption()
        {
            // データソースの取得
            GridView1.DataSource = GetDataSource();
            // データのバインド
            GridView1.DataBind();

            // 列名の変更
            GridView1.HeaderRow.Cells[0].Text = "";
            GridView1.HeaderRow.Cells[1].Text = "日付";
            GridView1.HeaderRow.Cells[2].Text = "企業名";
            GridView1.HeaderRow.Cells[3].Text = "顧客側担当者";
            GridView1.HeaderRow.Cells[4].Text = "自社担当者";
            GridView1.HeaderRow.Cells[5].Text = "契約形態";
            GridView1.HeaderRow.Cells[6].Text = "接触形式";
            GridView1.HeaderRow.Cells[7].Text = "コメント";
        }

        private void SearchCnctHis()
        {
            // SQL文の設定
            //string sql = @"SELECT CmpName, CstRepre, Department, Position, ConInfo, DConInfo, Mail, Role "
            //            + "FROM SalesSupportTool.dbo.CstRepre "
            //            + "INNER JOIN SalesSupportTool.dbo.Company "
            //            + "ON SalesSupportTool.dbo.CstRepre.CmpCode = SalesSupportTool.dbo.Company.CmpCode "
            //            + "WHERE ";

            string sql = @"SELECT FORMAT(Date,'yyyy/MM/dd') AS 'yyyy/MM/dd', CmpName, CstRepre, OwnComRepre, ConShape, ConType, Comment "
                        + "FROM SalesSupportTool.dbo.ComHistory "
                        + "INNER JOIN SalesSupportTool.dbo.Company "
                        + "ON SalesSupportTool.dbo.ComHistory.CmpCode = SalesSupportTool.dbo.Company.CmpCode "
                        + "WHERE ";

            // セッション変数を使用し、検索条件を追記
            if (Session["Date"] != null)
                if (Session["Date"].ToString() != "")
                {
                    DateTime date = DateTime.Parse(Session["Date"].ToString());
                    sql += "Date = '" + date + "' AND";
                }
            if (Session["CmpName"] != null)
                if (Session["CmpName"].ToString() != "選択してください")
                    sql += " CmpName = '" + Session["CmpName"] + "' AND";
            if (Session["CstRepre"] != null)
                if (Session["CstRepre"].ToString() != "選択してください")
                    sql += " CstRepre = '" + Session["CstRepre"] + "' AND";
            if (Session["OwnComRepre"] != null)
                if (Session["OwnComRepre"].ToString() != "選択してください")
                    sql += " OwnComRepre = '" + Session["OwnComRepre"] + "' AND";
            if (Session["ConShape"] != null)
                if (Session["ConShape"].ToString() != "選択してください")
                    sql += " ConShape = '" + Session["ConShape"] + "' AND";
            if (Session["ConType"] != null)
                if (Session["ConType"].ToString() != "選択してください")
                    sql += " ConType = '" + Session["ConType"] + "' AND";
            if (Session["Comment"] != null)
                if (Session["Comment"].ToString() != "選択してください")
                    sql += " Comment = '" + Session["Comment"] + "' AND";

            // 余った文字を切り取る
            if (sql.Substring(sql.Length - 1) == "D")
            {
                // 一番最後の","より前を取り出す
                sql = sql.Substring(0, sql.LastIndexOf("AND"));
            }
            else
            {
                // SQL文を実行しない
                return;
            }

            // データソースの取得
            GridView1.DataSource = SearchCnctHis(sql);
            // データのバインド
            GridView1.DataBind();

            // 列名の変更
            GridView1.HeaderRow.Cells[0].Text = "";
            GridView1.HeaderRow.Cells[1].Text = "日付";
            GridView1.HeaderRow.Cells[2].Text = "企業名";
            GridView1.HeaderRow.Cells[3].Text = "顧客側担当者";
            GridView1.HeaderRow.Cells[4].Text = "自社担当者";
            GridView1.HeaderRow.Cells[5].Text = "契約形態";
            GridView1.HeaderRow.Cells[6].Text = "接触形式";
            GridView1.HeaderRow.Cells[7].Text = "コメント";

            // セッション変数を初期化
            Session["Date"] = Session["CmpName"] = Session["CstRepre"] = Session["OwnComRepre"]
             = Session["ConShape"] = Session["ConType"] = Session["Comment"] = null;
        }


        //++++++++++++++++++++++++++++++++++++++
        private DataSet GetDataSource()
        {
            var database = new DataSet();

            // 接続文字列の取得
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    //command.CommandText = @"SELECT CmpName, CstRepre, Department, Position, ConInfo, DConInfo, Mail, Role "
                    //                      + "FROM SalesSupportTool.dbo.CstRepre "
                    //                      + "INNER JOIN SalesSupportTool.dbo.Company "
                    //                      + "ON SalesSupportTool.dbo.CstRepre.CmpCode = SalesSupportTool.dbo.Company.CmpCode";

                    //string sql = @"SELECT Data, CmpName, CstRepre, OwnComRepre, ConShape, ConType, Comment "
                    //    + "FROM SalesSupportTool.dbo.ComHistory "
                    //    + "INNER JOIN SalesSupportTool.dbo.Compay "
                    //    + "ON SalesSupportTool.dbo.ComHistory.CmpCode = SalesSupportTool.dbo.Company.CmpCode "
                    //    + "WHERE ";

                    command.CommandText = @"SELECT FORMAT(Date,'yyyy/MM/dd') AS 'yyyy/MM/dd', CmpName, CstRepre, OwnComRepre, ConShape, ConType, Comment "
                                        + "FROM SalesSupportTool.dbo.ComHistory "
                                        + "INNER JOIN SalesSupportTool.dbo.Company "
                                        + "ON SalesSupportTool.dbo.ComHistory.CmpCode = SalesSupportTool.dbo.Company.CmpCode ";


                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(database);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
                finally
                {
                    // データベースの接続終了
                    connection.Close();
                }
            }

            return database;
        }

        //++++++++++++++++++++++++++++++++++++++++++
        private DataSet SearchCnctHis(string sql)
        {
            var database = new DataSet();

            // 接続文字列の取得
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    command.CommandText = sql;

                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(database);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
                finally
                {
                    // データベースの接続終了
                    connection.Close();
                }
            }

            return database;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //編集ボタン
        protected void Button1_Click(object sender, EventArgs e)
        {
            // どの行も選択されていなかったら
            if (GridView1.SelectedRow == null)
            {
                // エラーメッセージ
                //Label_Message.Text = MESSAGE_ERR_CSTSELECT;
                //Label_Message.ForeColor = System.Drawing.Color.Red;
                return;
            }

            //SELECT Date, CmpName, CstRepre, OwnComRepre, ConShape, ConType, Comment

            // 選択した変数をセッション変数へ代入
            Session["Date"] = GridView1.SelectedRow.Cells[1].Text;
            Session["CmpName"] = GridView1.SelectedRow.Cells[2].Text;
            Session["CstRepre"] = GridView1.SelectedRow.Cells[3].Text;
            Session["OwnComRepre"] = GridView1.SelectedRow.Cells[4].Text;
            Session["ConShape"] = GridView1.SelectedRow.Cells[5].Text;
            Session["ConType"] = GridView1.SelectedRow.Cells[6].Text;
            Session["Comment"] = GridView1.SelectedRow.Cells[7].Text;

            Response.Redirect("EditHistCon.aspx");
        }

        //public DataTable GetData()
        //{
        //    var table = new DataTable();
        //    GridView gridview = new GridView();

        //    // 接続文字列の取得
        //    var connectionString = GetConnectionString();

        //    using (var connection = new SqlConnection(connectionString))
        //    using (var command = connection.CreateCommand())
        //    {
        //        try
        //        {
        //            // データベースの接続開始
        //            connection.Open();

        //            // SQLの設定
        //            //command.CommandText = @"SELECT count(*) FROM SalesSupportTool.dbo.ComHistory";
        //            command.CommandText = @"SELECT count(*) FROM sales_support.dbo.cntct_view";
        //            //SqlDataReader redar = command.ExecuteReader();

        //            //while (redar.Read() == true)
        //            //{
        //            //    string ID = (string)redar["登録ID"];
        //            //    string CmpCode = (string)redar["日付"];
        //            //    string Department = (string)redar["企業名"];
        //            //    string Position = (string)redar["顧客側担当者"];
        //            //    string Conlnfo = (string)redar["自社担当者"];
        //            //    string DConlnfo = (string)redar["契約形態"];
        //            //    string Mail = (string)redar["接触形態"];
        //            //    string Role = (string)redar["コメント"];
        //            //    GridView1.Rows.Add(ID, CmpCode, Department, Position,
        //            //        Conlnfo, DConlnfo, Mail, Role);
        //            //}

        //            // SQLの実行
        //            var adapter = new SqlDataAdapter(command);
        //            adapter.Fill(table);
        //        }
        //        catch (Exception exception)
        //        {
        //            Console.WriteLine(exception.Message);
        //            throw;
        //        }
        //        finally
        //        {

        //            // データベースの接続終了
        //            connection.Close();
        //        }
        //    }

        //    return table;
        //}



        //public DataSet GetDataSet()
        //{

        //    var database = new DataSet();
        //    GridView gridview = new GridView();

        //    // 接続文字列の取得
        //    var connectionString = GetConnectionString();

        //    using (var connection = new SqlConnection(connectionString))
        //    using (var command = connection.CreateCommand())
        //    {
        //        try
        //        {
        //            // データベースの接続開始
        //            connection.Open();

        //            // SQLの設定
        //            //command.CommandText = @"SELECT count(*) FROM sales_support.dbo.cntct_view";
        //            command.CommandText = @"SELECT [";

        //            // SQLの実行
        //            var adapter = new SqlDataAdapter(command);
        //            adapter.Fill(database);
        //        }
        //        catch (Exception exception)
        //        {
        //            Console.WriteLine(exception.Message);
        //            throw;
        //        }
        //        finally
        //        {
        //            // データベースの接続終了
        //            connection.Close();
        //        }
        //    }

        //    return database;
        //}

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
    }
}
