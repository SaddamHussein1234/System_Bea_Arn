using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.OM.Models;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_ERP_OM_MainItems_PageSubItems : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetLast();
            FGeAll();
            lblStatusAdd.Text = ClassSaddam.FCheckStatusArchive("إضافة");
            lblStatusEdit.Text = ClassSaddam.FCheckStatusArchive("تعديل");
        }
    }

    private void FGetLast()
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();
            MMI.IDCheck = "GetLast";
            MMI.Top = 1;
            MMI.ID_Item = Guid.Empty;
            MMI.Type = XType;
            MMI.ID_Part = Guid.Empty;
            MMI.FilterSearch = string.Empty;
            MMI.Start_Date = string.Empty;
            MMI.End_Date = string.Empty;
            MMI.DataCheck1 = string.Empty;
            MMI.DataCheck2 = string.Empty;
            MMI.DataCheck3 = string.Empty;
            MMI.DataCheck4 = string.Empty;
            MMI.DataCheck5 = string.Empty;
            MMI.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
            dt = RMI.BOM_Main_Items_Manage(MMI);
            if (dt.Rows.Count > 0)
                txt_Order_Add.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDOrder"]) + 1);
            else
                txt_Order_Add.Text = ClassSaddam.FGetNumberBillStart().ToString();

        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGeAll()
    {
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();
            MMI.IDCheck = "GetAll";
            MMI.Top = 1000;
            MMI.ID_Item = Guid.Empty;
            MMI.Type = XType;
            MMI.ID_Part = Guid.Empty;
            MMI.FilterSearch = txtSearch.Text.Trim();
            MMI.Start_Date = string.Empty;
            MMI.End_Date = string.Empty;
            MMI.DataCheck1 = string.Empty;
            MMI.DataCheck2 = string.Empty;
            MMI.DataCheck3 = string.Empty;
            MMI.DataCheck4 = string.Empty;
            MMI.DataCheck5 = string.Empty;
            MMI.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
            dt = RMI.BOM_Main_Items_Manage(MMI);
            if (dt.Rows.Count > 0)
            {
                GVMainItem.DataSource = dt;
                GVMainItem.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                btnDelete.Visible = false;
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
            foreach (GridViewRow row in GVMainItem.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVMainItem.DataKeys[row.RowIndex].Value.ToString());
                    Model_Main_Items_ MMI = new Model_Main_Items_()
                    {
                        IDCheck = "Delete",
                        ID_Item = _XID,
                        Type = string.Empty,
                        ID_Part = Guid.Empty,
                        ID_Part_Tow = Guid.Empty,
                        ID_Part_Three = Guid.Empty,
                        Name_Ar = string.Empty,
                        Name_En = string.Empty,
                        Order = 0,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
                    Xresult = RMI.FOM_Main_Items_Add(MMI);
                }
            }
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGeAll();
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
        Response.Redirect("PageMainItems.aspx");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        FGeAll();
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss");
            Model_Main_Items_ MMI = new Model_Main_Items_()
            {
                IDCheck = "Add",
                ID_Item = Guid.NewGuid(),
                Type = XType,
                Count_Part = 0,
                ID_Part = Guid.Empty,
                ID_Part_Tow = Guid.Empty,
                ID_Part_Three = Guid.Empty,
                Name_Ar = txtName_Ar_Add.Text.Trim(),
                Name_En = txtName_En_Add.Text.Trim(),
                Order = Convert.ToInt32(txt_Order_Add.Text.Trim()),
                CreatedBy = Test_Saddam.FGetIDUsiq(),
                CreatedDate = XDate,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                DeleteBy = 0,
                DeleteDate = XDate,
                IsActive = true
            };
            Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
            string Xresult = RMI.FOM_Main_Items_Add(MMI);
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
                FGeAll();
                if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة بند رئيسي" + "\n" + "لسندات القبض البند : " + "\n" + txtName_Ar_Add.Text.Trim(), "BerArn", "Add", Test_Saddam.FGetIDUsiq());
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
            Model_Main_Items_ MMI = new Model_Main_Items_();
            MMI.IDCheck = "GetByIDUniq";
            MMI.Top = 1;
            MMI.ID_Item = new Guid(HF_ID.Value);
            MMI.Type = XType;
            MMI.ID_Part = Guid.Empty;
            MMI.FilterSearch = string.Empty;
            MMI.Start_Date = string.Empty;
            MMI.End_Date = string.Empty;
            MMI.DataCheck1 = string.Empty;
            MMI.DataCheck2 = string.Empty;
            MMI.DataCheck3 = string.Empty;
            MMI.DataCheck4 = string.Empty;
            MMI.DataCheck5 = string.Empty;
            MMI.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
            dt = RMI.BOM_Main_Items_Manage(MMI);
            if (dt.Rows.Count > 0)
            {
                txtName_Ar_Edit.Text = dt.Rows[0]["_Name_Ar_"].ToString();
                txtName_En_Edit.Text = dt.Rows[0]["_Name_En_"].ToString();
                txt_Order_Edit.Text = dt.Rows[0]["_Order_"].ToString();
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss");
            Model_Main_Items_ MMI = new Model_Main_Items_()
            {
                IDCheck = "Edit",
                ID_Item = new Guid(HF_ID.Value),
                Type = XType,
                Count_Part = 0,
                ID_Part = Guid.Empty,
                ID_Part_Tow = Guid.Empty,
                ID_Part_Three = Guid.Empty,
                Name_Ar = txtName_Ar_Edit.Text.Trim(),
                Name_En = txtName_En_Edit.Text.Trim(),
                Order = Convert.ToInt32(txt_Order_Edit.Text.Trim()),
                CreatedBy = 0,
                CreatedDate = XDate,
                ModifiedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedDate = XDate,
                DeleteBy = 0,
                DeleteDate = XDate,
                IsActive = true
            };
            Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
            string Xresult = RMI.FOM_Main_Items_Add(MMI);
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
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGeAll();
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

    public int FGetCount(Guid XID)
    {
        int XResult = 0;
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();
            MMI.IDCheck = "GetCount";
            MMI.Top = 1;
            MMI.ID_Item = Guid.Empty;
            MMI.Type = XType;
            MMI.ID_Part = XID;
            MMI.FilterSearch = string.Empty;
            MMI.Start_Date = string.Empty;
            MMI.End_Date = string.Empty;
            MMI.DataCheck1 = string.Empty;
            MMI.DataCheck2 = string.Empty;
            MMI.DataCheck3 = string.Empty;
            MMI.DataCheck4 = string.Empty;
            MMI.DataCheck5 = string.Empty;
            MMI.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
            dt = RMI.BOM_Main_Items_Manage(MMI);
            if (dt.Rows.Count > 0)
                XResult = Convert.ToInt32(dt.Rows[0]["_Count"]);
            else
                XResult = 0;
        }
        catch (Exception)
        {

        }
        return XResult;
    }

}