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
    public partial class EditMaster : System.Web.UI.Page
    {
        // === 定数 ===
        // データベースサーバ
        const string DB_SERVER_NAME = @"Data Source=C-001\SQLEXPRESS;";
        const string DB_SERVER_NAME_HOME = @"Data Source=LAPTOP-V78PVCL1\SQLEXPRESS;";
        // システムメッセージ
        const string MESSAGE_ERR_INPUT = "役割以外で入力されていない項目があります。";
        const string MESSAGE_ERR_NOTINT = "企業コードが正しくありません。";
        const string MESSAGE_ERR_CODE = "企業コードが規定入力範囲外です。";
        const string MESSAGE_SUCCES_EDIT = "更新が完了しました。";
        const string MESSAGE_SUCCES_DELETE = "削除が完了しました。";


        protected void Page_Load(object sender, EventArgs e)
        {
            // データを取得していなければ
            if (TextBox_CmpCode.Text == "")
            {
                // 編集元データ取得
                GetData();

                // デフォルトボタンを編集ボタンに設定
                this.Form.DefaultButton = Button_Edit.UniqueID;
            }
        }

        // *** 編集ボタンクリックイベント ***
        protected void Button_Edit_Click(object sender, EventArgs e)
        {
            // エラーチェック
            if (!EditErrCheck())
                return;

            // マスタ編集
            if (Edit())
            {
                // 編集成功
                Label_Message.Text = MESSAGE_SUCCES_EDIT;
                Label_Message.ForeColor = System.Drawing.Color.Black;

                Response.Redirect("ViewPrtne.aspx");
            }
        }

        // *** 削除ボタンクリックイベント ***
        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            // マスタ削除
            if (DeleteMaster())
            {
                // 削除完了
                Label_Message.Text = MESSAGE_SUCCES_DELETE;
                Label_Message.ForeColor = System.Drawing.Color.Black;

                // テキストボックス中身削除
                DeleteAllTexBox();
            }
        }

        // *** データベース接続用 ***
        private string GetConnectionString()
        {
            return DB_SERVER_NAME_HOME                       // データベースサーバ
                    + @"Integrated Security=False;"     // Integrated Security
                    + @"User ID=root;"                  // SQL接続するユーザID
                    + @"Password=password";             // ユーザのパスワード
        }

        // *** 編集元データ取得 ***
        private void GetData()
        {
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
                    command.CommandText = @"SELECT * FROM SalesSupportTool.dbo.CstRepre WHERE ID=" + Session["ID"] + ";";

                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            TextBox_CmpCode.Text = reader["CmpCode"].ToString();
                            TextBox_CstRepre.Text = reader["CstRepre"].ToString();
                            TextBox_Department.Text = reader["Department"].ToString();
                            TextBox_Position.Text = reader["Position"].ToString();
                            TextBox_ConInfo.Text = reader["ConInfo"].ToString();
                            TextBox_DConInfo.Text = reader["DConInfo"].ToString();
                            TextBox_Mail.Text = reader["Mail"].ToString();
                            TextBox_Role.Text = reader["Role"].ToString();
                        }

                        // SQLの設定
                        command.CommandText = @"SELECT CmpName FROM SalesSupportTool.dbo.Company WHERE CmpCode=" + reader["CmpCode"] + ";";

                        reader.Close();

                        // SQLの実行
                        adapter = new SqlDataAdapter(command);
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                TextBox_CmpName.Text = reader["CmpName"].ToString();
                            }
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
        }

        // *** マスタ編集 ***
        private bool Edit()
        {
            // 成功フラグ
            bool success = false;
            // 企業コードバッファ
            int CmpCodeBuff = 0;

            // 接続文字列の取得
            var connectionString = GetConnectionString();

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // 企業コード取得
                    command.CommandText = @"SELECT CmpCode FROM SalesSupportTool.dbo.CstRepre WHERE ID=" + Session["ID"] + ";";
                    var adapter = new SqlDataAdapter(command);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            CmpCodeBuff = int.Parse(reader["CmpCode"].ToString());
                        }
                    }

                    reader.Close();

                    if (TextBox_Role.Text == null)
                    {
                        // SQLの設定
                        command.CommandText = @"UPDATE SalesSupportTool.dbo.CstRepre SET "
                                            + "CmpCode=" + int.Parse(TextBox_CmpCode.Text) + ","
                                            + "CstRepre='" + TextBox_CstRepre.Text + "',"
                                            + "Department='" + TextBox_Department.Text + "',"
                                            + "Position='" + TextBox_Position.Text + "',"
                                            + "ConInfo='" + TextBox_ConInfo.Text + "',"
                                            + "DConInfo='" + TextBox_DConInfo.Text + "',"
                                            + "Mail='" + TextBox_Mail.Text + "'"
                                            + " WHERE ID=" + Session["ID"] + ";";
                    }
                    else
                    {
                        // SQLの設定
                        command.CommandText = @"UPDATE SalesSupportTool.dbo.CstRepre SET "
                                            + "CmpCode=" + int.Parse(TextBox_CmpCode.Text) + ","
                                            + "CstRepre='" + TextBox_CstRepre.Text + "',"
                                            + "Department='" + TextBox_Department.Text + "',"
                                            + "Position='" + TextBox_Position.Text + "',"
                                            + "ConInfo='" + TextBox_ConInfo.Text + "',"
                                            + "DConInfo='" + TextBox_DConInfo.Text + "',"
                                            + "Mail='" + TextBox_Mail.Text + "',"
                                            + "Role='" + TextBox_Role.Text + "'"
                                            + " WHERE ID=" + Session["ID"] + ";";
                    }

                    // SQLの実行
                    adapter = new SqlDataAdapter(command);
                    command.ExecuteNonQuery();

                    // SQLの設定
                    command.CommandText = @"UPDATE SalesSupportTool.dbo.Company SET "
                                        + "CmpCode=" + int.Parse(TextBox_CmpCode.Text) + ","
                                        + "CmpName='" + TextBox_CmpName.Text + "'"
                                        + " WHERE CmpCode=" + CmpCodeBuff + ";";

                    // SQLの実行
                    adapter = new SqlDataAdapter(command);
                    command.ExecuteNonQuery();

                    // 成功フラグ
                    success = true;

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

            return success;
        }

        // *** マスタ削除 ***
        private bool DeleteMaster()
        {
            // 成功フラグ
            bool success = false;

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
                    command.CommandText = @"DELETE FROM SalesSupportTool.dbo.CstRepre WHERE ID=" + Session["ID"] + ";";

                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    command.ExecuteNonQuery();

                    // 成功フラグ
                    success = true;

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

            return success;
        }

        // *** エラーチェック ***
        private bool EditErrCheck()
        {
            // テキストボックスの中身が空白かチェック
            if (
            (TextBox_CmpCode.Text == "") ||
            (TextBox_CmpName.Text == "") ||
            (TextBox_CstRepre.Text == "") ||
            (TextBox_Department.Text == "") ||
            (TextBox_Position.Text == "") ||
            (TextBox_ConInfo.Text == "") ||
            (TextBox_DConInfo.Text == "") ||
            (TextBox_Mail.Text == ""))
            {
                // エラーメッセージ
                Label_Message.Text = MESSAGE_ERR_INPUT;
                Label_Message.ForeColor = System.Drawing.Color.Red;

                return false;
            }
            // 企業コードがint入力されてるかどうか
            if (!int.TryParse(TextBox_CmpCode.Text, out int cmpCode))
            {
                Label_Message.Text = MESSAGE_ERR_NOTINT;
                Label_Message.ForeColor = System.Drawing.Color.Red;

                return false;
            }
            // 企業コードが連番かどうか
            if (cmpCode != GetCmpCodeEnd())
            {
                // 企業コードが連番の範囲内で存在しないものなら
                if (cmpCode < int.Parse(Session["FreeCmpCodeMin"].ToString())
                    && !CheckCmpCodeExists(cmpCode))
                {
                    Label_Message.Text = MESSAGE_ERR_CODE;
                    Label_Message.ForeColor = System.Drawing.Color.Red;

                    return false;
                }
            }

            return true;
        }

        // *** 企業コード最終値取得 ***
        private int GetCmpCodeEnd()
        {
            int endCode = 0;

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
                    command.CommandText = @"SELECT CmpCode FROM SalesSupportTool.dbo.Company "
                                            + "GROUP BY CmpCode HAVING CmpCode < " + Session["FreeCmpCodeMin"].ToString()
                                            + " ORDER BY CmpCode DESC";

                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    var reader = command.ExecuteReader();

                    // 結果が存在
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            endCode = int.Parse(reader["CmpCode"].ToString());
                        }
                    }

                    reader.Close();
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

            return endCode + 1;
        }

        // *** 既存企業コード検索 ***
        private bool CheckCmpCodeExists(int id)
        {
            bool result = false;

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
                    command.CommandText = @"SELECT * FROM SalesSupportTool.dbo.Company "
                                            + "WHERE CmpCode = " + id;

                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    var reader = command.ExecuteReader();

                    // 結果が存在
                    if (reader.HasRows)
                    {
                        result = true;
                    }

                    reader.Close();
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

            return result;
        }

        // *** テキストボックス中身削除 ***
        private void DeleteAllTexBox()
        {
            TextBox_CmpCode.Text
            = TextBox_CmpName.Text
            = TextBox_CstRepre.Text
            = TextBox_Department.Text
            = TextBox_Position.Text
            = TextBox_ConInfo.Text
            = TextBox_DConInfo.Text
            = TextBox_Mail.Text
            = TextBox_Role.Text = null;
        }
    }
}