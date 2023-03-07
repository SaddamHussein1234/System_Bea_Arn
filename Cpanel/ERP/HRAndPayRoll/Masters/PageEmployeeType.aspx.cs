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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeType : System.Web.UI.Page
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
            Model_Employee_Type_ MET = new Model_Employee_Type_();
            MET.IDCheck = "GetAll";
            MET.EmployeeTypeID = Guid.NewGuid();
            MET.EmployeeType = txtSearch.Text.Trim();
            MET.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_Type_ RET = new Repostry_Employee_Type_();
            dt = RET.BErp_EmployeeType_Manage(MET);
            if (dt.Rows.Count > 0)
            {
                GVEmployeeType.DataSource = dt;
                GVEmployeeType.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }
    
    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeType.aspx");
    }

}