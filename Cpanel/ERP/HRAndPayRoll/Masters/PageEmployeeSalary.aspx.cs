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
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeSalary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A153", "A154", btnDelete, GVEmplyoeeSalary, 0, 11);
            FGetData();
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   "عرض رواتب الموظفين ", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    private void FGetData()
    {
        try
        {
            Model_EmployeeSalary_ MES = new Model_EmployeeSalary_();
            MES.IDCheck = "GetAll";
            MES.EmployeeSalaryID = Guid.NewGuid();
            MES.CreatedDate = txtSearch.Text.Trim();
            MES.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeSalary_ RES = new Repostry_EmployeeSalary_();
            dt = RES.BErp_EmployeeSalary_Manage(MES);
            if (dt.Rows.Count > 0)
            {
                GVEmplyoeeSalary.DataSource = dt;
                GVEmplyoeeSalary.DataBind();
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
        Response.Redirect("PageEmployeeSalary.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "بحث", "بحث : " + txtSearch.Text.Trim() + " في قائمة رواتب الموظفين", ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVEmplyoeeSalary.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmplyoeeSalary.DataKeys[row.RowIndex].Value);
                    Model_EmployeeSalary_ MES = new Model_EmployeeSalary_()
                    {
                        IDCheck = "Delete",
                        EmployeeSalaryID = new Guid(Comp_ID),
                        EmployeeId = Guid.NewGuid(),
                        Basic = 0,
                        TotalEarning = 0,
                        TotalDeduction = 0,
                        TotalSalary = 0,
                        IsMonthlySalary = false,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };

                    Repostry_EmployeeSalary_ RES = new Repostry_EmployeeSalary_();
                    string Xresult = RES.FErp_EmployeeSalary_Add(MES);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FGetData();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

}