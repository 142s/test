//********************************
//
// 変更履歴
// ・定数を追加
// ・Button_Regist_Click()
//    RegistErrCheck()の呼び出しを追加
// ・CheckTextBoxNull()
//  　関数を削除
// ・RegistErrCheck()
//    関数を作成
// ・Regist()
//    エラーチェックのif文を削除
//    SELECT文の部分をCheckCmpCodeExists()を使用する形に変更
// ・GetCmpCodeEnd()
//    関数を作成
// ・CheckCmpCodeExists()
//    関数を作成
// ※接続先サーバー名を変更する必要アリ
// ・ログインでsession変数を定義
//
//********************************
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
    public partial class RegistMaster : System.Web.UI.Page
    {
        // === 定数 ===
        // データベースサーバ
        const string DB_SERVER_NAME = @"Data Source=C-001\SQLEXPRESS;";
        const string DB_SERVER_NAME_HOME = @"Data Source=LAPTOP-V78PVCL1\SQLEXPRESS;";
        // システムメッセージ
        const string MESSAGE_ERR_INPUT = "役割以外で入力されていない項目があります。";
        const string MESSAGE_ERR_NOTINT = "企業コードが正しくありません。";
        const string MESSAGE_ERR_CODE = "企業コードが規定入力範囲外です。";
        const string MESSAGE_ERR_STROVER = "入力が正しくありません。";
        const string MESSAGE_SUCCES_REGIST = "登録が完了しました。";

        protected void Page_Load(object sender, EventArgs e)
        {
            // 初回のみ実行する
            if (!IsPostBack)
            {
                TextBox_CmpCode.Text = GetCmpCodeEnd().ToString();

                // デフォルトボタンを登録ボタンに設定
                this.Form.DefaultButton = Button_Regist.UniqueID;
            }
        }

        // *** 登録ボタンクリックイベント ***
        protected void Button_Regist_Click(object sender, EventArgs e)
        {
            // エラーチェック
            if (!RegistErrCheck())
                return;

            // マスタ登録
            if (Regist())
            {
                // 登録成功
                Label_Message.Text = MESSAGE_SUCCES_REGIST;
                Label_Message.ForeColor = System.Drawing.Color.Black;

                // テキストボックス中身削除
                DeleteAllTexBox();

                Response.Redirect("ViewPrtne.aspx");
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

        // *** マスタ登録 ***
        private bool Regist()
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

                    if (TextBox_Role.Text == null)
                    {
                        // SQLの設定
                        command.CommandText = @"INSERT INTO SalesSupportTool.dbo.CstRepre("
                                            + "CmpCode, CstRepre, Department, Position, ConInfo, DConInfo, Mail) "
                                            + "VALUES ("
                                            + int.Parse(TextBox_CmpCode.Text) + ",'"
                                            + TextBox_CstRepre.Text + "','"
                                            + TextBox_Department.Text + "','"
                                            + TextBox_Position.Text + "','"
                                            + TextBox_ConInfo.Text + "','"
                                            + TextBox_DConInfo.Text + "','"
                                            + TextBox_Mail.Text + "');";
                    }
                    else
                    {
                        // SQLの設定
                        command.CommandText = @"INSERT INTO SalesSupportTool.dbo.CstRepre("
                                            + "CmpCode, CstRepre, Department, Position, ConInfo, DConInfo, Mail, Role) "
                                            + "VALUES ("
                                            + int.Parse(TextBox_CmpCode.Text) + ",'"
                                            + TextBox_CstRepre.Text + "','"
                                            + TextBox_Department.Text + "','"
                                            + TextBox_Position.Text + "','"
                                            + TextBox_ConInfo.Text + "','"
                                            + TextBox_DConInfo.Text + "','"
                                            + TextBox_Mail.Text + "','"
                                            + TextBox_Role.Text + "')";
                    }

                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    command.ExecuteNonQuery();

                    // 企業コードが存在していない場合
                    if (!CheckCmpCodeExists(int.Parse(TextBox_CmpCode.Text)))
                    {
                        // SQLの設定
                        command.CommandText = @"INSERT INTO SalesSupportTool.dbo.Company(CmpCode,CmpName) "
                                            + "VALUES (" + int.Parse(TextBox_CmpCode.Text) + ",'" + TextBox_CmpName.Text + "');";

                        // SQLの実行
                        adapter = new SqlDataAdapter(command);
                        command.ExecuteNonQuery();
                    }

                    // 成功フラグ
                    success = true;
                }
                catch (SqlException sqlexception)
                {
                    // 登録成功
                    Label_Message.Text = MESSAGE_ERR_STROVER;
                    Label_Message.ForeColor = System.Drawing.Color.Red;
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
        private bool RegistErrCheck()
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
                                            + "GROUP BY CmpCode HAVING CmpCode < "+ Session["FreeCmpCodeMin"].ToString()
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