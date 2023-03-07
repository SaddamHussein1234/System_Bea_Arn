using Library_CLS_Arn.DMS.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_DMS_Setting_PageParty : System.Web.UI.Page
{
    public string XIdentifier = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblStatusAdd.Text = ClassSaddam.FCheckStatusArchive("إضافة");
            lblStatusEdit.Text = ClassSaddam.FCheckStatusArchive("تعديل");
            HFIDStore.Value = Guid.Empty.ToString();
            txt_Order_Add.Text = Repostry_DMS_Party_.FGetLastRecord(new Guid(HFIDStore.Value)).ToString();
            Repostry_Country_.FErp_Country_Manage(ddlCountry_Add);
            Repostry_Country_.FErp_Country_Manage(ddlCountry_Edit);
            ddlCountry_Add.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            txtEmail_Add.Text = ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com";
            FGeAllByDate();
        }
    }

    private void FGeAllByDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_DMS_Party_.FGetDataInDataTable("GeAll", 1000, Guid.Empty, new Guid(HFIDStore.Value),
               txtSearch.Text.Trim(), string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                GVParty.DataSource = dt;
                GVParty.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                btnDelete.Visible = true;
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVParty.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVParty.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_DMS_Party_.FAPP("Delete", _XID, new Guid(HFIDStore.Value), string.Empty, string.Empty, string.Empty,
                    0, string.Empty, Guid.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty,
                    string.Empty, string.Empty, false, false, 0, 0, Test_Saddam.FGetIDUsiq(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                System.Threading.Thread.Sleep(100);
                FGeAllByDate();
            }
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        FGeAllByDate();
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = Repostry_DMS_Party_.FAPP("Add", Guid.NewGuid(), new Guid(HFIDStore.Value), DLType_Customer_Add.SelectedValue, txtName_Ar_Add.Text.Trim(),
                txtName_En_Add.Text.Trim(), Convert.ToInt64(txt_Order_Add.Text.Trim()), txtAddress_Add.Text.Trim(), new Guid(ddlCountry_Add.SelectedValue),
                txt_City_Add.Text.Trim(), txtWebSite_Add.Text.Trim(), txtEmail_Add.Text.Trim(), Convert.ToInt32(txtEstablished_Year_Add.Text.Trim()),
                txt_Fax_Add.Text.Trim(), txtPhone_Number1_Add.Text.Trim(), txtMobile_Number1_Add.Text.Trim(), txtPhone_Number2_Add.Text.Trim(),
                string.Empty, Convert.ToBoolean(CBAllow_Ar_Add.Checked), Convert.ToBoolean(CBAllow_En_Add.Checked), Test_Saddam.FGetIDUsiq(), 0, 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
            if (Xresult == "IsExists")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                txt_Order_Add.Text = Repostry_DMS_Party_.FGetLastRecord(new Guid(HFIDStore.Value)).ToString();
                FGeAllByDate();
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

    protected void LBEdit_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            HF_ID.Value = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            DataTable dt = new DataTable();
            dt = Repostry_DMS_Party_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(HF_ID.Value),
                new Guid(HFIDStore.Value), string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                DLType_Customer_Edit.SelectedValue = dt.Rows[0]["_Type_Customer_"].ToString();
                txtName_Ar_Edit.Text = dt.Rows[0]["_Name_Ar_"].ToString();
                txtName_En_Edit.Text = dt.Rows[0]["_Name_En_"].ToString();
                txt_Order_Edit.Text = dt.Rows[0]["_Registration_No_"].ToString();
                txtAddress_Edit.Text = dt.Rows[0]["_Address_"].ToString();
                ddlCountry_Edit.SelectedValue = dt.Rows[0]["_Country_"].ToString();
                txt_City_Edit.Text = dt.Rows[0]["_City_"].ToString();
                txtWebSite_Edit.Text = dt.Rows[0]["_Website_"].ToString();
                txtEmail_Edit.Text = dt.Rows[0]["_Email_Address_"].ToString();
                txtEstablished_Year_Edit.Text = dt.Rows[0]["_Established_Year_"].ToString();
                txt_Fax_Edit.Text = dt.Rows[0]["_Fax_"].ToString();
                txtPhone_Number1_Edit.Text = dt.Rows[0]["_Phone_Number1_"].ToString();
                txtMobile_Number1_Edit.Text = dt.Rows[0]["_Mobile_Number1_"].ToString();
                txtPhone_Number2_Edit.Text = dt.Rows[0]["_Phone_Number2_"].ToString();
                CBAllow_Ar_Edit.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Allow_Ar_"]);
                CBAllow_En_Edit.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Allow_En_"]);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBEditData_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = Repostry_DMS_Party_.FAPP("Edit", new Guid(HF_ID.Value), new Guid(HFIDStore.Value), DLType_Customer_Edit.SelectedValue, txtName_Ar_Edit.Text.Trim(),
                txtName_En_Edit.Text.Trim(), Convert.ToInt64(txt_Order_Edit.Text.Trim()), txtAddress_Edit.Text.Trim(), new Guid(ddlCountry_Edit.SelectedValue),
                txt_City_Edit.Text.Trim(), txtWebSite_Edit.Text.Trim(), txtEmail_Edit.Text.Trim(), Convert.ToInt32(txtEstablished_Year_Edit.Text.Trim()),
                txt_Fax_Edit.Text.Trim(), txtPhone_Number1_Edit.Text.Trim(), txtMobile_Number1_Edit.Text.Trim(), txtPhone_Number2_Edit.Text.Trim(),
                string.Empty, Convert.ToBoolean(CBAllow_Ar_Edit.Checked), Convert.ToBoolean(CBAllow_En_Edit.Checked), 0, Test_Saddam.FGetIDUsiq(), 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
            if (Xresult == "IsExists")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGeAllByDate();
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

}