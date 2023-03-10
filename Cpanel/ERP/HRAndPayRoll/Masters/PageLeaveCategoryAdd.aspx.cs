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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageLeaveCategoryAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtLeaveCategory.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_LeaveCategory_ MLC = new Model_LeaveCategory_();
            MLC.IDCheck = "GetByIDUniq";
            MLC.LeaveCategoryID = new Guid(Request.QueryString["ID"]);
            MLC.LeaveCategoryName = string.Empty;
            MLC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_LeaveCategory_ RLC = new Repostry_LeaveCategory_();
            dt = RLC.BErp_LeaveCategory_Manage(MLC);
            if (dt.Rows.Count > 0)
                txtLeaveCategory.Text = dt.Rows[0]["LeaveCategory"].ToString();
            else
                Response.Redirect("PageLeaveCategory.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageLeaveCategory.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_LeaveCategory_ MLC = new Model_LeaveCategory_()
                {
                    IDCheck = "Add",
                    LeaveCategoryID = Guid.NewGuid(),
                    LeaveCategoryName = txtLeaveCategory.Text.Trim(),
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_LeaveCategory_ RLC = new Repostry_LeaveCategory_();
                string Xresult = RLC.FErp_LeaveCategory_Add(MLC);
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
                Model_LeaveCategory_ MLC = new Model_LeaveCategory_()
                {
                    IDCheck = "Edit",
                    LeaveCategoryID = new Guid(Request.QueryString["id"]),
                    LeaveCategoryName = txtLeaveCategory.Text.Trim(),
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_LeaveCategory_ RLC = new Repostry_LeaveCategory_();
                string Xresult = RLC.FErp_LeaveCategory_Add(MLC);
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
        Response.Redirect("PageLeaveCategory.aspx");
    }

}