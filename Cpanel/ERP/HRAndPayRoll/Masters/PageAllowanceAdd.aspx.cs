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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageAllowanceAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtAllowance.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Allowance_ MA = new Model_Allowance_();
            MA.IDCheck = "GetByIDUniq";
            MA.AllowanceID = new Guid(Request.QueryString["ID"]);
            MA.AllowanceName = string.Empty;
            MA.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Allowance_ RA = new Repostry_Allowance_();
            dt = RA.BErp_Allowance_Manage(MA);
            if (dt.Rows.Count > 0)
            {
                txtAllowance.Text = dt.Rows[0]["Allowance"].ToString();
                CBIsConsider.Checked = Convert.ToBoolean(dt.Rows[0]["IsConsider"]);
            }
            else
                Response.Redirect("PageAllowance.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageAllowance.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Allowance_ MA = new Model_Allowance_()
                {
                    IDCheck = "Add",
                    AllowanceID = Guid.NewGuid(),
                    AllowanceName = txtAllowance.Text.Trim(),
                    IsConsider = Convert.ToBoolean(CBIsConsider.Checked),
                    SortNumber = 0,
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Allowance_ RA = new Repostry_Allowance_();
                string Xresult = RA.FErp_Allowance_Add(MA);
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
                Model_Allowance_ MA = new Model_Allowance_()
                {
                    IDCheck = "Edit",
                    AllowanceID = new Guid(Request.QueryString["id"]),
                    AllowanceName = txtAllowance.Text.Trim(),
                    IsConsider = Convert.ToBoolean(CBIsConsider.Checked),
                    SortNumber = 0,
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Allowance_ RA = new Repostry_Allowance_();
                string Xresult = RA.FErp_Allowance_Add(MA);
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
        Response.Redirect("PageAllowance.aspx");
    }

}