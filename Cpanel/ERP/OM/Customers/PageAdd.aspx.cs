using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_Customers_PageAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtRegistration_No.Text = (Repostry_Customers_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(ddlYears.SelectedValue), string.Empty, 
                string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
            txtAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        DataTable dt = new DataTable();
        dt = Repostry_Customers_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(Request.QueryString["ID"]), Guid.Empty, string.Empty, string.Empty, string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
            txtRegistration_No.Text = dt.Rows[0]["_Registration_No_"].ToString();
            txtFirstName.Text = dt.Rows[0]["_First_Name_"].ToString();
            txtFamilyName.Text = dt.Rows[0]["_Last_Name_"].ToString();
            txtOrganization.Text = dt.Rows[0]["_Organization_"].ToString();
            txtEmail.Text = dt.Rows[0]["_Email_Address_"].ToString();
            txtPhone.Text = dt.Rows[0]["_Phone_Address_"].ToString();
            txtDirect_Number.Text = dt.Rows[0]["_Direct_Number_"].ToString();
            txtOffice_Number.Text = dt.Rows[0]["_Office_Number_"].ToString();
            txtAddress_line_1.Text = dt.Rows[0]["_Address_line_1_"].ToString();
            txtAddress_line_2.Text = dt.Rows[0]["_Address_line_2_"].ToString();
            txtCity.Text = dt.Rows[0]["_City_"].ToString();

            txtNote.Text = dt.Rows[0]["_Note_"].ToString();
            txtAdd.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;

            if (Convert.ToDateTime(txtAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FAPP_Customers_Add();
            else
            {
                lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
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

    private void FAPP_Customers_Add()
    {
        string XCheck = string.Empty;
        Guid XID = Guid.Empty; Guid XID_Marketed = Guid.Empty;
        int XIDAdd = 0, XUpdate = 0;
        if (Request.QueryString["ID"] == null)
        {
            XCheck = "Add"; XID = Guid.NewGuid(); XIDAdd = Test_Saddam.FGetIDUsiq();
        }
        if (Request.QueryString["ID"] != null)
        {
            XCheck = "Edit"; XID = new Guid(Request.QueryString["ID"]); XUpdate = Test_Saddam.FGetIDUsiq();
        }

        string Xresult = Repostry_Customers_.FAPP_Add(XCheck, XID, new Guid(ddlYears.SelectedValue), Convert.ToInt64(txtRegistration_No.Text.Trim()),
            txtFirstName.Text.Trim(), txtFamilyName.Text.Trim(), txtOrganization.Text.Trim(), txtEmail.Text.Trim(),
            txtPhone.Text.Trim(), txtDirect_Number.Text.Trim(), txtOffice_Number.Text.Trim(),
            txtAddress_line_1.Text.Trim(), txtAddress_line_2.Text.Trim(), txtCity.Text.Trim(), txtNote.Text.Trim(), false,
            XIDAdd, XUpdate, 0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsExistsNumber")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "رقم التسجيل مستخدم بالفعل , قم بتغييرة !!! ";
            txtRegistration_No.Focus();
            return;
        }
        else if (Xresult == "IsExistsName")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "هذا الأسم تم إضافة مسبقاً ... ";
            txtFirstName.Focus();
            return;
        }
        else if (Xresult == "IsSuccess")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم تحديث الحساب بنجاح ... ";
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtRegistration_No.Text = (Repostry_Customers_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(ddlYears.SelectedValue), string.Empty,
                string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
    }

}