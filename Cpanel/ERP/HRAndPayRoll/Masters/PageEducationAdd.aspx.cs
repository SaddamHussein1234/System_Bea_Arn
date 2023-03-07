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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEducationAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtEducation.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Education_ ME = new Model_Education_();
            ME.IDCheck = "GetByIDUniq";
            ME.EducationID = new Guid(Request.QueryString["ID"]);
            ME.EducationName = string.Empty;
            ME.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Education_ RE = new Repostry_Education_();
            dt = RE.BErp_Education_Manage(ME);
            if (dt.Rows.Count > 0)
                txtEducation.Text = dt.Rows[0]["EducationName"].ToString();
            else
                Response.Redirect("PageEducation.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEducation.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Education_ ME = new Model_Education_()
                {
                    IDCheck = "Add",
                    EducationID = Guid.NewGuid(),
                    EducationName = txtEducation.Text.Trim(),
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Education_ RE = new Repostry_Education_();
                string Xresult = RE.FErp_Education_Add(ME);
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
                Model_Education_ ME = new Model_Education_()
                {
                    IDCheck = "Edit",
                    EducationID = new Guid(Request.QueryString["id"]),
                    EducationName = txtEducation.Text.Trim(),
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Education_ RE = new Repostry_Education_();
                string Xresult = RE.FErp_Education_Add(ME);
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
        Response.Redirect("PageEducation.aspx");
    }

}