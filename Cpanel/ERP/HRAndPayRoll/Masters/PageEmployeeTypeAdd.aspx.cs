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
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeTypeAdd : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtDepartment.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Employee_Type_ MET = new Model_Employee_Type_();
            MET.IDCheck = "GetByIDUniq";
            MET.EmployeeTypeID = new Guid(Request.QueryString["ID"]);
            MET.EmployeeType = string.Empty;
            MET.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_Type_ RET = new Repostry_Employee_Type_();
            dt = RET.BErp_EmployeeType_Manage(MET);
            if (dt.Rows.Count > 0)
                txtDepartment.Text = dt.Rows[0]["EmployeeType"].ToString();
            else
                Response.Redirect("PageEmployeeType.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeType.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Employee_Type_ MET = new Model_Employee_Type_()
                {
                    IDCheck = "Add",
                    EmployeeTypeID = Guid.NewGuid(),
                    EmployeeType = txtDepartment.Text.Trim(),
                    NoOfLeavePerMonth = Convert.ToDecimal(ddlLeave.SelectedValue),
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Employee_Type_ RET = new Repostry_Employee_Type_();
                string Xresult = RET.FErp_EmployeeType_Add(MET);
                if (Xresult == "IsExistsAdd")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                    return;
                }
                else if (Xresult == "IsSuccessAdd")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                }
            }
            else if (Request.QueryString["id"] != null)
            {
                Model_Employee_Type_ MET = new Model_Employee_Type_()
                {
                    IDCheck = "Edit",
                    EmployeeTypeID = new Guid(Request.QueryString["id"]),
                    EmployeeType = txtDepartment.Text.Trim(),
                    NoOfLeavePerMonth = Convert.ToDecimal(ddlLeave.SelectedValue),
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Employee_Type_ RET = new Repostry_Employee_Type_();
                string Xresult = RET.FErp_EmployeeType_Add(MET);
                if (Xresult == "IsExistsEdit")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                    return;
                }
                else if (Xresult == "IsSuccessEdit")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                    FGetData();
                }
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeType.aspx");
    }

}