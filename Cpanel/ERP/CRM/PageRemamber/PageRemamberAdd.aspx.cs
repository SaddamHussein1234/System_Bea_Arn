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

public partial class Cpanel_ERP_CRM_PageRemamber_PageRemamberAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlSelect.Visible = true;
                Repostry_Company_.FCRM_Company_Manage(DLCompany);
                txtRemamberDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
                if (Request.QueryString["ID"] != null)
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = false;
                    DLCompany.SelectedValue = Request.QueryString["ID"];
                    Pnl_Account.Visible = true;
                    pnlSelect.Visible = false;
                    txtRemamberDate.Focus();
                    FGetDataList();
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

    protected void LBRefrsh_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageRemamber.aspx");
    }

    protected void btn_Add_To__Click(object sender, EventArgs e)
    {
        try
        {
            Model_Remamber_ MR = new Model_Remamber_()
            {
                IDCheck = "Add",
                ID_Item = Guid.NewGuid(),
                ID_Company = new Guid(DLCompany.SelectedValue),
                Remamber_Date = txtRemamberDate.Text.Trim(),
                Remamber_Desc = txtDesc.Text.Trim(),
                Is_Active = true,
                CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedBy = 0,
                ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                Is_Delete = false
            };

            Repostry_Remamber_ RR = new Repostry_Remamber_();
            string Xresult = RR.FCRM_Remamber_Add(MR);
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
                FGetDataList();
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

    protected void LBDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;

            Guid IDItem = new Guid((((LinkButton)sender).CommandArgument));
            Model_Remamber_ MR = new Model_Remamber_()
            {
                IDCheck = "Delete",
                ID_Item = IDItem,
                ID_Company = Guid.Empty,
                Remamber_Date = string.Empty,
                Remamber_Desc = string.Empty,
                Is_Active = false,
                CreatedDate = string.Empty,
                CreatedBy = 0,
                ModifiedBy = 0,
                ModifiedDate = string.Empty,
                Is_Delete = true
            };

            Repostry_Remamber_ RR = new Repostry_Remamber_();
            string Xresult = RR.FCRM_Remamber_Add(MR);
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                FGetDataList();
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

    protected void DLCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            Pnl_Account.Visible = true;
            pnlSelect.Visible = false;
            txtRemamberDate.Focus();
            FGetDataList();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetDataList()
    {
        Model_Remamber_ MR = new Model_Remamber_();
        MR.IDCheck = "GetByID";
        MR.ID_Item = Guid.Empty;
        MR.ID_Company = new Guid(DLCompany.SelectedValue);
        MR.Start_Date = string.Empty;
        MR.End_Date = string.Empty;
        MR.Remamber_Date = string.Empty;
        MR.Is_Active = true;
        MR.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_Remamber_ RR = new Repostry_Remamber_();
        dt = RR.BCRM_Remamber_Manage(MR);
        if (dt.Rows.Count > 0)
        {
            RPT_Remamber_.DataSource = dt;
            RPT_Remamber_.DataBind();
            pnlNull_To.Visible = false;
            pnlData_To.Visible = true;
        }
        else
        {
            pnlNull_To.Visible = true;
            pnlData_To.Visible = false;
        }
    }

}