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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeGrade : System.Web.UI.Page
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
            Model_Employee_Grade_ MEG = new Model_Employee_Grade_();
            MEG.IDCheck = "GetAll";
            MEG.EmployeeGradeID = Guid.NewGuid();
            MEG.EmployeeGrade = txtSearch.Text.Trim();
            MEG.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_Grade_ REG = new Repostry_Employee_Grade_();
            dt = REG.BErp_Employee_Grade_Manage(MEG);
            if (dt.Rows.Count > 0)
            {
                GVEmployeeGrade.DataSource = dt;
                GVEmployeeGrade.DataBind();
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
        Response.Redirect("PageEmployeeGrade.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}