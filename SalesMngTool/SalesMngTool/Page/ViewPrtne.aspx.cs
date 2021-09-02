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
    public partial class prtneMng : System.Web.UI.Page
    {
        // === 定数 ===
        // データベースサーバ
        const string DB_SERVER_NAME = @"Data Source=C-001\SQLEXPRESS;";
        const string DB_SERVER_NAME_HOME = @"Data Source=LAPTOP-V78PVCL1\SQLEXPRESS;";
        // システムメッセージ
        const string MESSAGE_ERR_CSTSELECT = "顧客側担当者が選択されていません。";

        protected void Page_Load(object sender, EventArgs e)
        {
            // 初回のみ実行する
            if (!IsPostBack)
            {
                // グリッドビュー設定
                GridViewOption();
            }

            // 検索結果の場合
            SearchCstRepre();
        }

        // *** 編集移動ボタンクリックイベント ***
        protected void Button_GoEdit_Click(object sender, EventArgs e)
        {
            // どの行も選択されていなかったら
            if (GridView_CstRepreList.SelectedRow == null)
            {
                // エラーメッセージ
                Label_Message.Text = MESSAGE_ERR_CSTSELECT;
                Label_Message.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 選択した変数をセッション変数へ代入
            //Session["CmpName"] = GridView_CstRepreList.SelectedRow.Cells[1].Text;
            //Session["CstRepre"] = GridView_CstRepreList.SelectedRow.Cells[2].Text;
            //Session["Department"] = GridView_CstRepreList.SelectedRow.Cells[3].Text;
            //Session["Position"] = GridView_CstRepreList.SelectedRow.Cells[4].Text;
            //Session["ConInfo"] = GridView_CstRepreList.SelectedRow.Cells[5].Text;
            //Session["DConInfo"] = GridView_CstRepreList.SelectedRow.Cells[6].Text;
            //Session["Mail"] = GridView_CstRepreList.SelectedRow.Cells[7].Text;
            //Session["Role"] = GridView_CstRepreList.SelectedRow.Cells[8].Text;

            string sql = @"SELECT ID "
            + "FROM SalesSupportTool.dbo.CstRepre "
            + "INNER JOIN SalesSupportTool.dbo.Company "
            + "ON SalesSupportTool.dbo.CstRepre.CmpCode = SalesSupportTool.dbo.Company.CmpCode "
            + "WHERE "
            + "CmpName = '" + GridView_CstRepreList.SelectedRow.Cells[1].Text + "'AND "
            + "CstRepre = '" + GridView_CstRepreList.SelectedRow.Cells[2].Text + "'AND "
            + "Department = '" + GridView_CstRepreList.SelectedRow.Cells[3].Text + "'AND "
            + "Position = '" + GridView_CstRepreList.SelectedRow.Cells[4].Text + "'AND "
            + "ConInfo = '" + GridView_CstRepreList.SelectedRow.Cells[5].Text + "'AND "
            + "DConInfo = '" + GridView_CstRepreList.SelectedRow.Cells[6].Text + "'AND "
            + "Mail = '" + GridView_CstRepreList.SelectedRow.Cells[7].Text + "'";

            if (GridView_CstRepreList.SelectedRow.Cells[8].Text != "&nbsp;")
                sql += " AND Role = '" + GridView_CstRepreList.SelectedRow.Cells[8].Text + "'";

            Session["ID"] = GetCstRepreID(sql);

            Response.Redirect("EditMaster.aspx");
        }

        // *** 選択ボタンクリックイベント ***
        protected void GridView_CstRepreList_RowCommand(object sender, EventArgs e)
        {
            // 選択した行の背景色を変更
            GridView_CstRepreList.SelectedRowStyle.BackColor = System.Drawing.Color.Blue;
        }

        // *** データベース接続用 ***
        private string GetConnectionString()
        {
            return DB_SERVER_NAME_HOME                  // データベースサーバ
                    + @"Integrated Security=False;"     // Integrated Security
                    + @"User ID=root;"                  // SQL接続するユーザID
                    + @"Password=password";             // ユーザのパスワード
        }

        // *** グリッドビュー設定 ***
        private void GridViewOption()
        {
            // データソースの取得
            GridView_CstRepreList.DataSource = GetDataSource();
            // データのバインド
            GridView_CstRepreList.DataBind();

            // 列名の変更
            GridView_CstRepreList.HeaderRow.Cells[0].Text = "";
            GridView_CstRepreList.HeaderRow.Cells[1].Text = "企業名";
            GridView_CstRepreList.HeaderRow.Cells[2].Text = "顧客側担当者";
            GridView_CstRepreList.HeaderRow.Cells[3].Text = "部署名";
            GridView_CstRepreList.HeaderRow.Cells[4].Text = "役職";
            GridView_CstRepreList.HeaderRow.Cells[5].Text = "連絡先";
            GridView_CstRepreList.HeaderRow.Cells[6].Text = "直接連絡先";
            GridView_CstRepreList.HeaderRow.Cells[7].Text = "メールアドレス";
            GridView_CstRepreList.HeaderRow.Cells[8].Text = "役割";
        }

        // *** 検索結果表示 ***
        private void SearchCstRepre()
        {
            // SQL文の設定
            string sql = @"SELECT CmpName, CstRepre, Department, Position, ConInfo, DConInfo, Mail, Role "
                        + "FROM SalesSupportTool.dbo.CstRepre "
                        + "INNER JOIN SalesSupportTool.dbo.Company "
                        + "ON SalesSupportTool.dbo.CstRepre.CmpCode = SalesSupportTool.dbo.Company.CmpCode "
                        + "WHERE ";

            // セッション変数を使用し、検索条件を追記
            if (Session["CmpName"] != null)
                if (Session["CmpName"].ToString() != "")
                    sql += "CmpName = '" + Session["CmpName"] + "' AND";

            if (Session["CstRepre"] != null)
                if (Session["CstRepre"].ToString() != "")
                    sql += " CstRepre = '" + Session["CstRepre"] + "' AND";

            if (Session["Department"] != null)
                if (Session["Department"].ToString() != "")
                    sql += " Department = '" + Session["Department"] + "' AND";

            if (Session["Position"] != null)
                if (Session["Position"].ToString() != "")
                    sql += " Position = '" + Session["Position"] + "' AND";

            if (Session["ConInfo"] != null)
                if (Session["ConInfo"].ToString() != "")
                    sql += " ConInfo = '" + Session["ConInfo"] + "' AND";

            if (Session["DConInfo"] != null)
                if (Session["DConInfo"].ToString() != "")
                    sql += " DConInfo = '" + Session["DConInfo"] + "' AND";

            if (Session["Mail"] != null)
                if (Session["Mail"].ToString() != "")
                    sql += " Mail = '" + Session["Mail"] + "' AND";

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
            GridView_CstRepreList.DataSource = SearchCstRepre(sql);
            // データのバインド
            GridView_CstRepreList.DataBind();

            // 列名の変更
            GridView_CstRepreList.HeaderRow.Cells[0].Text = "";
            GridView_CstRepreList.HeaderRow.Cells[1].Text = "企業名";
            GridView_CstRepreList.HeaderRow.Cells[2].Text = "顧客側担当者";
            GridView_CstRepreList.HeaderRow.Cells[3].Text = "部署名";
            GridView_CstRepreList.HeaderRow.Cells[4].Text = "役職";
            GridView_CstRepreList.HeaderRow.Cells[5].Text = "連絡先";
            GridView_CstRepreList.HeaderRow.Cells[6].Text = "直接連絡先";
            GridView_CstRepreList.HeaderRow.Cells[7].Text = "メールアドレス";
            GridView_CstRepreList.HeaderRow.Cells[8].Text = "役割";

            // セッション変数を初期化
            Session["CmpName"] = Session["CstRepre"] = Session["Department"] = Session["Position"]
             = Session["ConInfo"] = Session["DConInfo"] = Session["Mail"] = null;
        }

        // *** データソース取得 ***
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
                    command.CommandText = @"SELECT CmpName, CstRepre, Department, Position, ConInfo, DConInfo, Mail, Role "
                                          + "FROM SalesSupportTool.dbo.CstRepre "
                                          + "INNER JOIN SalesSupportTool.dbo.Company "
                                          + "ON SalesSupportTool.dbo.CstRepre.CmpCode = SalesSupportTool.dbo.Company.CmpCode";

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

        // *** データソース取得 ***
        private DataSet SearchCstRepre(string sql)
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

        protected void GridView_CstRepreList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // *** ID取得 ***
        private int GetCstRepreID(string sql)
        {
            int ID = 0;

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
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            ID = int.Parse(reader["ID"].ToString());
                        }
                    }
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

            return ID;
        }


    }
}