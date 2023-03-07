using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageSetting_PageCompanyType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FGetData();
    }

    private void FGetData()
    {
        try
        {
            Model_Company_Type_ MCT = new Model_Company_Type_();
            MCT.IDCheck = "GetAll";
            MCT.ID_Item = Guid.NewGuid();
            MCT.Company_Type = txtSearch.Text.Trim();
            MCT.Is_Active = true;
            MCT.Is_Delete = false;
            DataTable dt = new DataTable();
            Repostry_Company_Type_ RCT = new Repostry_Company_Type_();
            dt = RCT.BCRM_Company_Type_Manage(MCT);
            if (dt.Rows.Count > 0)
            {
                GVCompanyType.DataSource = dt;
                GVCompanyType.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageCompanyType.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVCompanyType.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVCompanyType.DataKeys[row.RowIndex].Value);
                    Model_Company_Type_ MCT = new Model_Company_Type_()
                    {
                        IDCheck = "Delete",
                        ID_Item = new Guid(Comp_ID),
                        Company_Type = string.Empty,
                        CreatedDate = string.Empty,
                        Is_Active = false,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        Is_Delete = true
                    };
                    Repostry_Company_Type_ RCT = new Repostry_Company_Type_();
                    string Xresult = RCT.FCRM_Company_Type_Add(MCT);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FGetData();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

}