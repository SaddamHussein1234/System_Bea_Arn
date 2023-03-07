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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageDeductionAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtDeduction.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Deduction_ MD = new Model_Deduction_();
            MD.IDCheck = "GetByIDUniq";
            MD.DeductionID = new Guid(Request.QueryString["ID"]);
            MD.DeductionName = string.Empty;
            MD.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Deduction_ RD = new Repostry_Deduction_();
            dt = RD.BErp_Deduction_Manage(MD);
            if (dt.Rows.Count > 0)
            {
                txtDeduction.Text = dt.Rows[0]["Deduction"].ToString();
                CBIsConsider.Checked = Convert.ToBoolean(dt.Rows[0]["IsConsider"]);
            }
            else
                Response.Redirect("PageDeduction.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageDeduction.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Deduction_ MD = new Model_Deduction_()
                {
                    IDCheck = "Add",
                    DeductionID = Guid.NewGuid(),
                    DeductionName = txtDeduction.Text.Trim(),
                    IsConsider = Convert.ToBoolean(CBIsConsider.Checked),
                    SortNumber = 0,
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Deduction_ RD = new Repostry_Deduction_();
                string Xresult = RD.FErp_Deduction_Add(MD);
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
                Model_Deduction_ MD = new Model_Deduction_()
                {
                    IDCheck = "Edit",
                    DeductionID = new Guid(Request.QueryString["id"]),
                    DeductionName = txtDeduction.Text.Trim(),
                    IsConsider = Convert.ToBoolean(CBIsConsider.Checked),
                    SortNumber = 0,
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Deduction_ RD = new Repostry_Deduction_();
                string Xresult = RD.FErp_Deduction_Add(MD);
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
        Response.Redirect("PageDeduction.aspx");
    }

}