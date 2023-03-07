using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageSetting_PageCompanyTypeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCompanyType.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Company_Type_ MCT = new Model_Company_Type_();
            MCT.IDCheck = "GetByIDUniq";
            MCT.ID_Item = new Guid(Request.QueryString["ID"]);
            MCT.Company_Type = string.Empty;
            MCT.Is_Active = false;
            MCT.Is_Delete = false;
            DataTable dt = new DataTable();
            Repostry_Company_Type_ RCT = new Repostry_Company_Type_();
            dt = RCT.BCRM_Company_Type_Manage(MCT);
            if (dt.Rows.Count > 0)
            {
                txtCompanyType.Text = dt.Rows[0]["_Company_Type_"].ToString();
                CBIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Active_"]);
            }
            else
                Response.Redirect("PageCompanyType.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageCompanyType.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Company_Type_ MCT = new Model_Company_Type_()
                {
                    IDCheck = "Add",
                    ID_Item = Guid.NewGuid(),
                    Company_Type = txtCompanyType.Text.Trim(),
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    Is_Active = Convert.ToBoolean(CBIsActive.Checked),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    Is_Delete = false
                };

                Repostry_Company_Type_ RCT = new Repostry_Company_Type_();
                string Xresult = RCT.FCRM_Company_Type_Add(MCT);
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
                Model_Company_Type_ MCT = new Model_Company_Type_()
                {
                    IDCheck = "Edit",
                    ID_Item = new Guid(Request.QueryString["id"]),
                    Company_Type = txtCompanyType.Text.Trim(),
                    Is_Active = Convert.ToBoolean(CBIsActive.Checked),
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    Is_Delete = true
                };

                Repostry_Company_Type_ RCT = new Repostry_Company_Type_();
                string Xresult = RCT.FCRM_Company_Type_Add(MCT);
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
        Response.Redirect("PageCompanyType.aspx");
    }

}