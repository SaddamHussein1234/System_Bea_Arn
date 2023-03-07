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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEducation : System.Web.UI.Page
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
            Model_Education_ ME = new Model_Education_();
            ME.IDCheck = "GetAll";
            ME.EducationID = Guid.NewGuid();
            ME.EducationName = txtSearch.Text.Trim();
            ME.IsActive = true;

            DataTable dt = new DataTable();
            Repostry_Education_ RE = new Repostry_Education_();
            dt = RE.BErp_Education_Manage(ME);
            if (dt.Rows.Count > 0)
            {
                GVEducation.DataSource = dt;
                GVEducation.DataBind();
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
        Response.Redirect("PageEducation.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}