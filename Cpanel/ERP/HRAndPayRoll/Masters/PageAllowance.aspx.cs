using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageAllowance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            FGetData();
        } 
    }

    private void FGetData()
    {
        try
        {
            Model_Allowance_ MA = new Model_Allowance_();
            MA.IDCheck = "GetAll";
            MA.AllowanceID = Guid.NewGuid();
            MA.AllowanceName = txtSearch.Text.Trim();
            MA.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Allowance_ RA = new Repostry_Allowance_();
            dt = RA.BErp_Allowance_Manage(MA);
            if (dt.Rows.Count > 0)
            {
                GVAllowance.DataSource = dt;
                GVAllowance.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAllowance.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}