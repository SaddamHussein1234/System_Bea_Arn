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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeGradeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtEmployee_Grade.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Employee_Grade_ MEG = new Model_Employee_Grade_();
            MEG.IDCheck = "GetByIDUniq";
            MEG.EmployeeGradeID = new Guid(Request.QueryString["ID"]);
            MEG.EmployeeGrade = string.Empty;
            MEG.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_Grade_ REG = new Repostry_Employee_Grade_();
            dt = REG.BErp_Employee_Grade_Manage(MEG);
            if (dt.Rows.Count > 0)
                txtEmployee_Grade.Text = dt.Rows[0]["EmployeeGrade"].ToString();
            else
                Response.Redirect("PageEmployeeGrade.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeGrade.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Employee_Grade_ MEG = new Model_Employee_Grade_()
                {
                    IDCheck = "Add",
                    EmployeeGradeID = Guid.NewGuid(),
                    EmployeeGrade = txtEmployee_Grade.Text.Trim(),
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Employee_Grade_ REG = new Repostry_Employee_Grade_();
                string Xresult = REG.FErp_Employee_Grade_Add(MEG);
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
                Model_Employee_Grade_ MEG = new Model_Employee_Grade_()
                {
                    IDCheck = "Edit",
                    EmployeeGradeID = new Guid(Request.QueryString["id"]),
                    EmployeeGrade = txtEmployee_Grade.Text.Trim(),
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Employee_Grade_ REG = new Repostry_Employee_Grade_();
                string Xresult = REG.FErp_Employee_Grade_Add(MEG);
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
        Response.Redirect("PageEmployeeGrade.aspx");
    }

}