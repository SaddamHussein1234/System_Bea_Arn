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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageAccountables : System.Web.UI.Page
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
            Model_AccountableType_ MAT = new Model_AccountableType_();
            MAT.IDCheck = "GetAll";
            MAT.AccountableID = Guid.NewGuid();
            MAT.AccountableName = txtSearch.Text.Trim();
            MAT.IsActive = true;

            DataTable dt = new DataTable();
            Repostry_AccountableType_ RAT = new Repostry_AccountableType_();
            dt = RAT.BErp_Accountable_Manage(MAT);
            if (dt.Rows.Count > 0)
            {
                GVAccountables.DataSource = dt;
                GVAccountables.DataBind();
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
        Response.Redirect("PageAccountables.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}