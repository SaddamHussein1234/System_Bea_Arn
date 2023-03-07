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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageDepartment : System.Web.UI.Page
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
            Model_Department_ MD = new Model_Department_();
            MD.IDCheck = "GetAll";
            MD.DepartmentID = Guid.NewGuid();
            MD.DepartmentName = txtSearch.Text.Trim();
            MD.IsActive = true;

            DataTable dt = new DataTable();
            Repostry_Department_ RP = new Repostry_Department_();
            dt = RP.BErp_Department_Manage(MD);
            if (dt.Rows.Count > 0)
            {
                GVDepartment.DataSource = dt;
                GVDepartment.DataBind();
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
        Response.Redirect("PageDepartment.aspx");
    }

}