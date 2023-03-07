using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_FMS_PageBank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            txt_Order.Text = (Repostry_Bank_.FGetCount("GetLast", 1, Guid.Empty,
                string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
            lblStatusAdd.Text = ClassSaddam.FCheckStatusArchive("إضافة");
            lblStatusEdit.Text = ClassSaddam.FCheckStatusArchive("تعديل");
        }
    }

    private void FGeAllByDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_Bank_.FGetDataInDataTable("GeAllByDate", 5000, Guid.Empty,
                txtSearch.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
            if (dt.Rows.Count > 0)
            {
                GVBanksName.DataSource = dt;
                GVBanksName.DataBind();
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
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVBanksName.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVBanksName.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_Bank_.FAPP_Add("Delete", _XID, string.Empty, string.Empty, 0, 0, 0, Test_Saddam.FGetIDUsiq(),
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
        Response.Redirect("PageBank.aspx");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        FGeAllByDate();
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            string Xresult = Repostry_Bank_.FAPP_Add("Add", Guid.NewGuid(),
                txtName_Bank_Ar.Text.Trim(), txtName_Bank_En.Text.Trim(), Convert.ToInt32(txt_Order.Text.Trim()), Test_Saddam.FGetIDUsiq(),
                0, 0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
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
                txt_Order.Text = (Repostry_Bank_.FGetCount("GetLast", 1, Guid.Empty,
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
            dt = Repostry_Bank_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(Convert.ToString((((LinkButton)sender).CommandArgument)).ToString()),
                string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                txtName_Bank_Ar_Edit.Text = dt.Rows[0]["_Name_Bank_Ar_"].ToString();
                txtName_Bank_En_Edit.Text = dt.Rows[0]["_Name_Bank_En_"].ToString();
                txt_Order_Edi.Text = dt.Rows[0]["_ID_Order_"].ToString();
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

    protected void LBEdit_Click1(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;

            string Xresult = Repostry_Bank_.FAPP_Add("Edit", new Guid(HF_ID.Value),
                txtName_Bank_Ar_Edit.Text.Trim(), txtName_Bank_En_Edit.Text.Trim(), Convert.ToInt32(txt_Order_Edi.Text.Trim()), 0, Test_Saddam.FGetIDUsiq(),
                0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
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

    protected void GVBanksName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVBanksName.PageIndex = e.NewPageIndex;
        this.FGeAllByDate();
    }

    protected void DLCountRows_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        GVBanksName.Columns[0].Visible = true;
        GVBanksName.UseAccessibleHeader = false;
        if (DLCountRows.SelectedValue == "10")
            GVBanksName.PageSize = 10;
        else if (DLCountRows.SelectedValue == "25")
            GVBanksName.PageSize = 25;
        else if (DLCountRows.SelectedValue == "50")
            GVBanksName.PageSize = 50;
        else if (DLCountRows.SelectedValue == "100")
            GVBanksName.PageSize = 100;
        else if (DLCountRows.SelectedValue == "250")
            GVBanksName.PageSize = 250;
        else if (DLCountRows.SelectedValue == "500")
            GVBanksName.PageSize = 500;
        else if (DLCountRows.SelectedValue == "1000")
            GVBanksName.PageSize = 1000;
        FGeAllByDate();
    }

}