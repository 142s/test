using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SalesMngTool.Controll
{
    public partial class SearchPrtne : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSerach_Click(object sender, EventArgs e)
        {
            Session["CmpName"] = inCmpName.Text;
            Session["CstRepre"] = inCstRepre.Text;
            Session["Department"] = inDepartment.Text;
            Session["Position"] = inPosition.Text;
            Session["ConInfo"] = inConInfo.Text;
            Session["DConInfo"] = inDConInfo.Text;
            Session["Mail"] = inMail.Text;

            Response.Redirect("ViewPrtne.aspx");
        }

        protected void BtnClr_Click(object sender, EventArgs e)
        {
            inCmpName.Text = "";
            inCstRepre.Text = "";
            inDepartment.Text = "";
            inPosition.Text = "";
            inConInfo.Text = "";
            inDConInfo.Text = "";
            inMail.Text = "";
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