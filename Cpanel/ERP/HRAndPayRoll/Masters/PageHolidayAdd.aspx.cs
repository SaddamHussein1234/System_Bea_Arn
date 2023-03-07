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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageHolidayAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtTitle.Focus();
            txtDateFrom.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDateTo.Text = txtDateFrom.Text;
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Holiday_ MH = new Model_Holiday_();
            MH.IDCheck = "GetByIDUniq";
            MH.HolidayID = new Guid(Request.QueryString["ID"]);
            MH.Title = string.Empty;
            MH.StartDate = string.Empty;
            MH.EndDate = string.Empty;
            MH.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Holiday_ RH = new Repostry_Holiday_();
            dt = RH.BErp_Holiday_Manage(MH);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtDescription.Text = dt.Rows[0]["Description"].ToString();
                txtDateFrom.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("yyyy-MM-dd");
                txtDateTo.Text = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("yyyy-MM-dd");
            }
            else
                Response.Redirect("PageHoliday.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageHoliday.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Holiday_ MH = new Model_Holiday_()
                {
                    IDCheck = "Add",
                    HolidayID = Guid.NewGuid(),
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    StartDate = txtDateFrom.Text.Trim(),
                    EndDate = txtDateTo.Text.Trim(),
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Holiday_ RH = new Repostry_Holiday_();
                string Xresult = RH.FErp_Holiday_Add(MH);
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
                Model_Holiday_ MH = new Model_Holiday_()
                {
                    IDCheck = "Edit",
                    HolidayID = new Guid(Request.QueryString["id"]),
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    StartDate = txtDateFrom.Text.Trim(),
                    EndDate = txtDateTo.Text.Trim(),
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Holiday_ RD = new Repostry_Holiday_();
                string Xresult = RD.FErp_Holiday_Add(MH);
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
        Response.Redirect("PageHoliday.aspx");
    }

}