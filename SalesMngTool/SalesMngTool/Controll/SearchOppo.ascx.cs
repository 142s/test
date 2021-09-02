using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace SalesMngTool
{
    public partial class normalHeader : System.Web.UI.UserControl
    {
        const string DATASOURCE = @"C-001\SQLEXPRESS";
        const string USERID = "root";
        const string PASSWORD = "password";
        const string DBNAME = "SalesSupportTool";
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        DataTable dt = new DataTable();

        /// <summary>
        /// メッセージ表示
        /// </summary>
        /// <param name="xMessage">表示する文字列</param>
        public void MessageBox(string xMessage)
        {
            Response.Write("<script>alert('" + xMessage + "')</script>");
        }

        /// <summary>
        /// ページが読み込まれた際の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                builder.DataSource = DATASOURCE;    // 接続先の SQL Server インスタンス
                builder.UserID = USERID;            // 接続ユーザー名
                builder.Password = PASSWORD;        // 接続パスワード
                builder.InitialCatalog = DBNAME;    // 接続するデータベース

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
                    }
                    //接続解除
                    connection.Close();
                }
            }
            catch (SqlException s)
            {
                MessageBox(s.ToString());
            }

            //if (inDate.Text == "")
            //{
            //    inDate.Text = DateTime.Today.ToString("D");
            //}
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSerach_Click(object sender, EventArgs e)
        {
            Session["Date"] = inDate.Text;
            Session["CmpName"] = DDListCompany.SelectedItem;
            Session["CstRepre"] = DDListCstRepre.SelectedItem;
            Session["OwnComRepre"] = DDListOwnComRepre.SelectedItem;
            Session["ConShape"] = DDListConShape.SelectedItem;
            Session["ConType"] = DDListConType.SelectedItem;
            Response.Redirect("ViewHistCon.aspx");
        }

        /// <summary>
        /// 入力項目クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnClr_Click(object sender, EventArgs e)
        {
            inDate.Text = "";
            DDListCompany.SelectedIndex = 0;
            DDListCstRepre.SelectedIndex = 0;
            DDListOwnComRepre.SelectedIndex = 0;
            DDListConShape.SelectedIndex = 0;
            DDListConType.SelectedIndex = 0;
        }

        /// <summary>
        /// 日付選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //選択した日付をTextBoxに挿入
            inDate.Text = Calendar1.SelectedDate.ToString("D");
        }

        /// <summary>
        /// ドロップダウンリスト初期化
        /// </summary>
        /// <param name="command"></param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="dropDownList">初期化するリスト</param>
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

        protected void BtnJnpMySales_Click(object sender, EventArgs e)
        {
            Response.Redirect("MySalesManMng.aspx");
        }

        protected void BtnJnpRegistCon_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistCon.aspx");
        }

        protected void BtnJnpViewPrtnr_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewPrtne.aspx");
        }

        protected void BtnJnpRegistMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistMaster.aspx");
        }

        protected void BtnJnpHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewHistCon.aspx");
        }

    }
}