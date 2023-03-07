using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_Setting_PageCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblStatusAdd.Text = ClassSaddam.FCheckStatusArchive("إضافة");
            lblStatusEdit.Text = ClassSaddam.FCheckStatusArchive("تعديل");
            FGeAllByDate();
            txt_Order_Add.Text = (Repostry_Category_.FGetCount("GetLastRecord", 1, Guid.Empty, string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
            FGeAllByDate();
        }
    }

    private void FGeAllByDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_Category_.FGetDataInDataTable("GeAll", 1000, Guid.Empty,
                txtSearch.Text.Trim(), string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                GVCategory.DataSource = dt;
                GVCategory.DataBind();
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVCategory.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVCategory.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_Category_.FAPP_Add("Delete", _XID, string.Empty, string.Empty, 0,
                    false, false, 0, 0, Test_Saddam.FGetIDUsiq(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGeAllByDate();
    }

    protected void LBEdit_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            HF_ID.Value = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            DataTable dt = new DataTable();
            dt = Repostry_Category_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(HF_ID.Value),
                string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                txtName_Ar_Edit.Text = dt.Rows[0]["_Name_Ar_"].ToString();
                txtName_En_Edit.Text = dt.Rows[0]["_Name_En_"].ToString();
                txt_Order_Edit.Text = dt.Rows[0]["_ID_Order_"].ToString();
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

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = Repostry_Category_.FAPP_Add("Add", Guid.NewGuid(), txtName_Ar_Add.Text.Trim(), txtName_En_Add.Text.Trim(),
                Convert.ToInt32(txt_Order_Add.Text.Trim()), Convert.ToBoolean(CBAllow_Ar_Add.Checked), Convert.ToBoolean(CBAllow_En_Add.Checked), Test_Saddam.FGetIDUsiq(), 0, 0,
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
                txt_Order_Add.Text = (Repostry_Category_.FGetCount("GetLastRecord", 1, Guid.Empty, string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
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

    protected void LBEditData_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = Repostry_Category_.FAPP_Add("Edit", new Guid(HF_ID.Value), txtName_Ar_Edit.Text.Trim(), txtName_En_Edit.Text.Trim(),
                Convert.ToInt32(txt_Order_Edit.Text.Trim()), Convert.ToBoolean(CBAllow_Ar_Edit.Checked), Convert.ToBoolean(CBAllow_En_Edit.Checked), 0, Test_Saddam.FGetIDUsiq(), 0,
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