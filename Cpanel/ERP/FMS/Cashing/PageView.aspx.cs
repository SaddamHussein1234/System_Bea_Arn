using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_FMS_Cashing_PageView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtSearch.Text = Request.QueryString["ID"];
            ddlYears.SelectedValue = Request.QueryString["IDUniq"];
            txtSearch.Focus();
            if (txtSearch.Text.Trim() != string.Empty)
            {
                PageView.SetData(ddlYears.SelectedValue, txtSearch.Text.Trim());
                ID_Edit.Visible = true;
                ID_Edit.HRef = "PageAdd.aspx?ID=" + PageView.XID();
            }
        }
    }

    protected void LbRefreshSaraf_Click(object sender, EventArgs e)
    {
        PageView.SetData(ddlYears.SelectedValue, txtSearch.Text.Trim());
        ID_Edit.HRef = "PageAdd.aspx?ID=" + PageView.XID();
    }

    protected void LBPrintSaraf_Click(object sender, EventArgs e)
    {
        PageView.FPrint();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PageView.SetData(ddlYears.SelectedValue, txtSearch.Text.Trim());
        ID_Edit.Visible = true;
        ID_Edit.HRef = "PageAdd.aspx?ID=" + PageView.XID();
    }

}