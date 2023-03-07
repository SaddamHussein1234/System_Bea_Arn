using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Models;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_Cash_Donation_PageView : System.Web.UI.Page
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
        ID_Edit.HRef = "PageAdd.aspx?ID=" + PageView.XID();
    }

}