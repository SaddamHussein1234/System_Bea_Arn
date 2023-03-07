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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageAccountableAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtAccountable.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_AccountableType_ MAT = new Model_AccountableType_();
            MAT.IDCheck = "GetByIDUniq";
            MAT.AccountableID = new Guid(Request.QueryString["ID"]);
            MAT.AccountableName = string.Empty;
            MAT.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_AccountableType_ RAT = new Repostry_AccountableType_();
            dt = RAT.BErp_Accountable_Manage(MAT);
            if (dt.Rows.Count > 0)
                txtAccountable.Text = dt.Rows[0]["Accountable"].ToString();
            else
                Response.Redirect("PageAccountables.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageAccountables.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_AccountableType_ MAT = new Model_AccountableType_()
                {
                    IDCheck = "Add",
                    AccountableID = Guid.NewGuid(),
                    AccountableName  = txtAccountable.Text.Trim(),
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_AccountableType_ RAT = new Repostry_AccountableType_();
                string Xresult = RAT.FErp_Accountable_Add(MAT);
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
                Model_AccountableType_ MAT = new Model_AccountableType_()
                {
                    IDCheck = "Edit",
                    AccountableID = new Guid(Request.QueryString["id"]),
                    AccountableName = txtAccountable.Text.Trim(),
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_AccountableType_ RAT = new Repostry_AccountableType_();
                string Xresult = RAT.FErp_Accountable_Add(MAT);
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
        Response.Redirect("PageAccountables.aspx");
    }

}