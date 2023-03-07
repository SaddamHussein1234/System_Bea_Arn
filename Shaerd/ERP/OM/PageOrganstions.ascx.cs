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

public partial class Shaerd_ERP_OM_PageOrganstions : System.Web.UI.UserControl
{
    public string XIdentifier = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblStatusAdd.Text = ClassSaddam.FCheckStatusArchive("إضافة");
            lblStatusEdit.Text = ClassSaddam.FCheckStatusArchive("تعديل");
            Repostry_Category_.FGetDropList(1, "_ID", "_Ar", DL_Category_Add);
            Repostry_Category_.FGetDropList(1, "_ID", "_Ar", DL_Category_Edit);
            txt_Order_Add.Text = Repostry_Organstions_.FGetLastRecord(XIdentifier).ToString();
            Repostry_Country_.FErp_Country_Manage(ddlCountry_Add);
            Repostry_Country_.FErp_Country_Manage(ddlCountry_Edit);
            ddlCountry_Add.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            ddlCountry_Edit.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FGeAllByDate();
        }
    }

    private void FGeAllByDate()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_Organstions_.FGetDataInDataTable("GeAllByDate", 1000, Guid.Empty, XIdentifier,
               txtSearch.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
            if (dt.Rows.Count > 0)
            {
                GVOrganstions.DataSource = dt;
                GVOrganstions.DataBind();
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
            foreach (GridViewRow row in GVOrganstions.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVOrganstions.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_Organstions_.FAPP_Add("Delete", _XID, Guid.Empty, XIdentifier, string.Empty, string.Empty, 0,
                    string.Empty, 0, 0, Test_Saddam.FGetIDUsiq(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
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
            string Xresult = Repostry_Organstions_.FAPP_Add("Add", Guid.NewGuid(), new Guid(DL_Category_Add.SelectedValue), XIdentifier, 
                DLType_Customer_Add.SelectedValue, txtName_Ar_Add.Text.Trim(), Convert.ToInt64(txt_Order_Add.Text.Trim()),
                txtPhone_Number1_Add.Text.Trim(), Test_Saddam.FGetIDUsiq(), 0, 0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
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
                txt_Order_Add.Text = Repostry_Organstions_.FGetLastRecord(XIdentifier).ToString();
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
            dt = Repostry_Organstions_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(HF_ID.Value),
                XIdentifier, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                DL_Category_Edit.SelectedValue = dt.Rows[0]["_ID_Category_"].ToString();
                DLType_Customer_Edit.SelectedValue = dt.Rows[0]["_Type_Customer_"].ToString();
                txtName_Ar_Edit.Text = dt.Rows[0]["_Name_"].ToString();
                txt_Order_Edit.Text = dt.Rows[0]["_Registration_No_"].ToString();
                txtPhone_Number1_Edit.Text = dt.Rows[0]["_Phone_Number_"].ToString();
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
            string Xresult = Repostry_Organstions_.FAPP_Add("Edit", new Guid(HF_ID.Value), new Guid(DL_Category_Edit.SelectedValue), XIdentifier, DLType_Customer_Edit.SelectedValue,
                txtName_Ar_Edit.Text.Trim(), Convert.ToInt64(txt_Order_Edit.Text.Trim()),
                txtPhone_Number1_Edit.Text.Trim(), 0, Test_Saddam.FGetIDUsiq(), 0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
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