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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageHoliday : System.Web.UI.Page
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
            Model_Holiday_ MH = new Model_Holiday_();
            MH.IDCheck = "GetAll";
            MH.HolidayID = Guid.NewGuid();
            MH.Title = txtSearch.Text.Trim();
            MH.StartDate = string.Empty;
            MH.EndDate = string.Empty;
            MH.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Holiday_ RH = new Repostry_Holiday_();
            dt = RH.BErp_Holiday_Manage(MH);
            if (dt.Rows.Count > 0)
            {
                GVHoliday.DataSource = dt;
                GVHoliday.DataBind();
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
        Response.Redirect("PageHoliday.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}