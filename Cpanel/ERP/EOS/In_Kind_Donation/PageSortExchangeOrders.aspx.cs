using Library_CLS_Arn.ERP.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_In_Kind_Donation_PageSortExchangeOrders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A59");
            pnlSelect.Visible = true;
        }
    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathith.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = true;
            IDTarmem.Visible = false;
            IDTalef.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RPTarmem_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTarmem.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = true;
            IDTalef.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RPTalef_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTalef.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = false;
            IDTalef.Visible = true;
            IDPrisms.Visible = false;
        }
    }

    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        if (RPSupportForPrisms.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = false;
            IDTalef.Visible = false;
            IDPrisms.Visible = true;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSortExchangeOrders.aspx");
    }

}