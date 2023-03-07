using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_FMS_PageAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            pnlSelect.Visible = true;
            txt_Order.Text = (Repostry_Account_.FGetCount("GetLast", 1, Guid.Empty, Guid.Empty,
                string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();

            Repostry_Bank_.FGetDropList("WithNull", "Ar", DLBankName);

            lblStatusAdd.Text = ClassSaddam.FCheckStatusArchive("إضافة");
            lblStatusEdit.Text = ClassSaddam.FCheckStatusArchive("تعديل");
        }
    }

    private void FGeAllByDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_Account_.FGetDataInDataTable("GeAllByDate", 5000, Guid.Empty, Guid.Empty,
                txtSearch.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
            if (dt.Rows.Count > 0)
            {
                GVBanksAccount.DataSource = dt;
                GVBanksAccount.DataBind();
                btnDelete.Visible = true;
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
            }
            else
            {
                btnDelete.Visible = false;
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
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
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVBanksAccount.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVBanksAccount.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_Account_.FAPP_Add("Delete", _XID, Guid.Empty,
                     string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0, Test_Saddam.FGetIDUsiq(),
                     ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
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
        Response.Redirect("PageAccount.aspx");
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
            string Xresult = Repostry_Account_.FAPP_Add("Add", Guid.NewGuid(),
                new Guid(DLBankName.SelectedValue), txtAccount_Owner_Ar.Text.Trim(), txtAccount_Owner_En.Text.Trim(), txtAccount_Number.Text.Trim(), txtIBAN_Number.Text.Trim(),
                Convert.ToInt32(txt_Order.Text.Trim()), Test_Saddam.FGetIDUsiq(), 0, 0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
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
                txt_Order.Text = (Repostry_Account_.FGetCount("GetLast", 1, Guid.Empty, Guid.Empty,
                    string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
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
        try
        {
            HF_ID.Value = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            DataTable dt = new DataTable();
            dt = Repostry_Account_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(Convert.ToString((((LinkButton)sender).CommandArgument)).ToString()),
                Guid.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                Repostry_Bank_.FGetDropList("WithNull", "Ar", DLBankNameEdit);
                DLBankNameEdit.SelectedValue = dt.Rows[0]["_ID_Bank_"].ToString();
                txtAccount_Owner_Ar_Edit.Text = dt.Rows[0]["_Account_Owner_Ar_"].ToString();
                txtAccount_Owner_En_Edit.Text = dt.Rows[0]["_Account_Owner_En_"].ToString();
                txtAccount_Number_Edit.Text = dt.Rows[0]["_Account_Number_"].ToString();
                txtIBAN_Number_Edit.Text = dt.Rows[0]["_IBAN_Number_"].ToString();
                txt_Order_Edit.Text = dt.Rows[0]["_ID_Order_"].ToString();
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

    protected void LBEditAccount_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;

            string Xresult = Repostry_Account_.FAPP_Add("Edit", new Guid(HF_ID.Value),
                new Guid(DLBankNameEdit.SelectedValue), txtAccount_Owner_Ar_Edit.Text.Trim(), txtAccount_Owner_En_Edit.Text.Trim(), txtAccount_Number_Edit.Text.Trim(), txtIBAN_Number_Edit.Text.Trim(),
                Convert.ToInt32(txt_Order_Edit.Text.Trim()), 0, Test_Saddam.FGetIDUsiq(), 0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
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

    protected void GVBanksAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVBanksAccount.PageIndex = e.NewPageIndex;
        this.FGeAllByDate();
    }

    protected void DLCountRows_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        GVBanksAccount.Columns[0].Visible = true;
        GVBanksAccount.UseAccessibleHeader = false;
        if (DLCountRows.SelectedValue == "10")
            GVBanksAccount.PageSize = 10;
        else if (DLCountRows.SelectedValue == "25")
            GVBanksAccount.PageSize = 25;
        else if (DLCountRows.SelectedValue == "50")
            GVBanksAccount.PageSize = 50;
        else if (DLCountRows.SelectedValue == "100")
            GVBanksAccount.PageSize = 100;
        else if (DLCountRows.SelectedValue == "250")
            GVBanksAccount.PageSize = 250;
        else if (DLCountRows.SelectedValue == "500")
            GVBanksAccount.PageSize = 500;
        else if (DLCountRows.SelectedValue == "1000")
            GVBanksAccount.PageSize = 1000;
        FGeAllByDate();
    }

}