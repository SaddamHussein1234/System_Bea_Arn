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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageShift : System.Web.UI.Page
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
            Model_Shift_ MS = new Model_Shift_();
            MS.IDCheck = "GetAll";
            MS.ShiftID = Guid.NewGuid();
            MS.Shift = txtSearch.Text.Trim();
            MS.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Shift_ RS = new Repostry_Shift_();
            dt = RS.BErp_Shift_Manage(MS);
            if (dt.Rows.Count > 0)
            {
                GVShift.DataSource = dt;
                GVShift.DataBind();
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
        Response.Redirect("PageShift.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}