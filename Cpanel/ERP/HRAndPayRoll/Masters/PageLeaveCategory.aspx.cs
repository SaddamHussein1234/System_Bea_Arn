﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageLeaveCategory : System.Web.UI.Page
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
            Model_LeaveCategory_ MLC = new Model_LeaveCategory_();
            MLC.IDCheck = "GetAll";
            MLC.LeaveCategoryID = Guid.NewGuid();
            MLC.LeaveCategoryName = txtSearch.Text.Trim();
            MLC.IsActive = true;

            DataTable dt = new DataTable();
            Repostry_LeaveCategory_ RLC = new Repostry_LeaveCategory_();
            dt = RLC.BErp_LeaveCategory_Manage(MLC);
            if (dt.Rows.Count > 0)
            {
                GVLeaveCategory.DataSource = dt;
                GVLeaveCategory.DataBind();
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
        Response.Redirect("PageLeaveCategory.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}