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

public partial class Shaerd_ERP_OM_MainItems_PageMainItems : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetByDropList("MainItems", string.Empty);
            FGeAll();
            lblStatusAdd.Text = ClassSaddam.FCheckStatusArchive("إضافة");
            lblStatusEdit.Text = ClassSaddam.FCheckStatusArchive("تعديل");
        }
    }

    public string FGetName(Guid XID)
    {
        string XResult = string.Empty;
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();
            MMI.IDCheck = "GetNameByID";
            MMI.Top = 1;
            MMI.ID_Item = XID;
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
                XResult = dt.Rows[0]["_Ar"].ToString();
            else
                XResult = "لم يحدد";
        }
        catch (Exception)
        {

        }
        return XResult;
    }

    private void FGetByDropList(string XValue, string XMainItem)
    {
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();

            MMI.Top = 1000;
            MMI.ID_Item = Guid.Empty;
            MMI.Type = XType;
            if (XValue == "MainItems")
            {
                MMI.IDCheck = "GetByDropList";
                MMI.ID_Part = Guid.Empty;
            }
            else if (XValue == "SubItems" || XValue == "SubItemsEdit")
            {
                MMI.IDCheck = "GetByDropListOne";
                MMI.ID_Part = new Guid(XMainItem);
            }
            else if (XValue == "SubItemsTow" || XValue == "SubItemsTowEdit")
            {
                MMI.IDCheck = "GetByDropListTow";
                MMI.ID_Part = new Guid(XMainItem);
            }
            MMI.FilterSearch = Guid.Empty.ToString();
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
                if (XValue == "MainItems")
                {
                    DLMain_ItemsAdd.Items.Clear(); DLMain_ItemsAdd.Items.Add("");
                    DLMain_ItemsAdd.AppendDataBoundItems = true; DLMain_ItemsAdd.DataValueField = "_ID";
                    DLMain_ItemsAdd.DataTextField = "_Ar"; DLMain_ItemsAdd.DataSource = dt;
                    DLMain_ItemsAdd.DataBind();

                    DLMain_Items_Edit.Items.Clear(); DLMain_Items_Edit.Items.Add("");
                    DLMain_Items_Edit.AppendDataBoundItems = true; DLMain_Items_Edit.DataValueField = "_ID";
                    DLMain_Items_Edit.DataTextField = "_Ar"; DLMain_Items_Edit.DataSource = dt;
                    DLMain_Items_Edit.DataBind();
                }
                else if (XValue == "SubItems")
                {
                    DLSubItemsAdd.Items.Clear();
                    DLSubItemsAdd.Items.Add(""); DLSubItemsAdd.AppendDataBoundItems = true;
                    DLSubItemsAdd.DataValueField = "_ID"; DLSubItemsAdd.DataTextField = "_Ar";
                    DLSubItemsAdd.DataSource = dt; DLSubItemsAdd.DataBind();
                }
                else if (XValue == "SubItemsTow")
                {
                    DLSubItemsTowAdd.Items.Clear();
                    DLSubItemsTowAdd.Items.Add(""); DLSubItemsTowAdd.AppendDataBoundItems = true;
                    DLSubItemsTowAdd.DataValueField = "_ID"; DLSubItemsTowAdd.DataTextField = "_Ar";
                    DLSubItemsTowAdd.DataSource = dt; DLSubItemsTowAdd.DataBind();
                }
                else if (XValue == "SubItemsEdit")
                {
                    DLSubItemsEdit.Items.Clear();
                    DLSubItemsEdit.Items.Add(""); DLSubItemsEdit.AppendDataBoundItems = true;
                    DLSubItemsEdit.DataValueField = "_ID"; DLSubItemsEdit.DataTextField = "_Ar";
                    DLSubItemsEdit.DataSource = dt; DLSubItemsEdit.DataBind();
                }
                else if (XValue == "SubItemsTowEdit")
                {
                    DLSubItemsTowEdit.Items.Clear();
                    DLSubItemsTowEdit.Items.Add(""); DLSubItemsTowEdit.AppendDataBoundItems = true;
                    DLSubItemsTowEdit.DataValueField = "_ID"; DLSubItemsTowEdit.DataTextField = "_Ar";
                    DLSubItemsTowEdit.DataSource = dt; DLSubItemsTowEdit.DataBind();
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetLast(string XValue, string XType, Guid XID)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();
            if (XValue == "One")
                MMI.IDCheck = "GetLast";
            else if (XValue == "Tow")
                MMI.IDCheck = "GetLastTow";
            else if (XValue == "Three")
                MMI.IDCheck = "GetLastThree";
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
            {
                if (XType == "Add")
                    txt_Order_Add.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDOrder"]) + 1);
                else if (XType == "Edit")
                    txt_Order_Edit.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDOrder"]) + 1);
            }
            else
            {
                if (XType == "Add")
                    txt_Order_Add.Text = ClassSaddam.FGetNumberBillStart().ToString();
                else if (XType == "Edit")
                    txt_Order_Edit.Text = ClassSaddam.FGetNumberBillStart().ToString();
            }

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
            MMI.IDCheck = "GetAllPart";
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
                GVSubItem.DataSource = dt;
                GVSubItem.DataBind();
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
            foreach (GridViewRow row in GVSubItem.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVSubItem.DataKeys[row.RowIndex].Value.ToString());
                    Model_Main_Items_ MMI = new Model_Main_Items_()
                    {
                        IDCheck = "Delete",
                        ID_Item = _XID,
                        Type = string.Empty,
                        Count_Part = 0,
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
        Response.Redirect("PageSubItems.aspx");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        FGeAll();
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (DLCountAdd.SelectedValue == "1")
            FAdd();
        else if (DLCountAdd.SelectedValue == "2")
        {
            if (DLSubItemsAdd.SelectedValue != string.Empty && DLSubItemsTowAdd.SelectedValue == string.Empty)
                FAdd();
            else if (DLSubItemsAdd.SelectedValue == string.Empty && DLSubItemsTowAdd.SelectedValue == string.Empty)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "يجب تحديد القوائم ... ";
                return;
            }
        }
        else if (DLCountAdd.SelectedValue == "3")
        {
            if (DLSubItemsAdd.SelectedValue != string.Empty && DLSubItemsTowAdd.SelectedValue != string.Empty)
                FAdd();
            else if (DLSubItemsAdd.SelectedValue == string.Empty || DLSubItemsTowAdd.SelectedValue == string.Empty)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "يجب تحديد القوائم ... ";
                return;
            }
        }
    }

    private void FAdd()
    {
        try
        {
            Guid _ID_Part_Tow = Guid.Empty, _ID_Part_Three = Guid.Empty;
            if (DLSubItemsAdd.SelectedValue != string.Empty)
                _ID_Part_Tow = new Guid(DLSubItemsAdd.SelectedValue);
            if (DLSubItemsTowAdd.SelectedValue != string.Empty)
                _ID_Part_Three = new Guid(DLSubItemsTowAdd.SelectedValue);
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss");
            Model_Main_Items_ MMI = new Model_Main_Items_()
            {
                IDCheck = "Add",
                ID_Item = Guid.NewGuid(),
                Type = XType,
                Count_Part = Convert.ToInt32(DLCountAdd.SelectedValue),
                ID_Part = new Guid(DLMain_ItemsAdd.SelectedValue),
                ID_Part_Tow = _ID_Part_Tow,
                ID_Part_Three = _ID_Part_Three,
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
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة بند فرعي" + "\n" + "لسندات القبض البند : " + "\n" + txtName_Ar_Add.Text.Trim(), "BerArn", "Add", Test_Saddam.FGetIDUsiq());
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
                DLCountEdit.SelectedValue = dt.Rows[0]["_Count_Part_"].ToString();
                DLMain_Items_Edit.SelectedValue = dt.Rows[0]["_ID_Part_"].ToString();
                if (DLCountEdit.SelectedValue == "1")
                {
                    DLSubItemsEdit.Enabled = false; DLSubItemsTowEdit.Enabled = false;
                    DLSubItemsEdit.SelectedValue = null; DLSubItemsTowEdit.SelectedValue = null;
                }
                else if (DLCountEdit.SelectedValue == "2")
                {
                    DLSubItemsEdit.Enabled = true; DLSubItemsTowEdit.Enabled = false;
                    FGetByDropList("SubItemsEdit", DLMain_Items_Edit.SelectedValue);
                    DLSubItemsEdit.SelectedValue = dt.Rows[0]["_ID_Part_Tow_"].ToString();
                    DLSubItemsTowEdit.SelectedValue = null;
                }
                else if (DLCountEdit.SelectedValue == "3")
                {
                    DLSubItemsEdit.Enabled = true; DLSubItemsTowEdit.Enabled = true;
                    FGetByDropList("SubItemsEdit", DLMain_Items_Edit.SelectedValue);
                    DLSubItemsEdit.SelectedValue = dt.Rows[0]["_ID_Part_Tow_"].ToString();
                    FGetByDropList("SubItemsTowEdit", DLSubItemsEdit.SelectedValue);
                    DLSubItemsTowEdit.SelectedValue = dt.Rows[0]["_ID_Part_Three_"].ToString();
                }
                txtName_Ar_Edit.Text = dt.Rows[0]["_Name_Ar_"].ToString();
                txtName_En_Edit.Text = dt.Rows[0]["_Name_En_"].ToString();
                txt_Order_Edit.Text = dt.Rows[0]["_Order_"].ToString();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowIDModelEdit();", true);
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
        if (DLCountEdit.SelectedValue == "1")
            FEdit();
        else if (DLCountEdit.SelectedValue == "2")
        {
            if (DLSubItemsEdit.SelectedValue != string.Empty && DLSubItemsTowEdit.SelectedValue == string.Empty)
                FEdit();
            else if (DLSubItemsEdit.SelectedValue == string.Empty && DLSubItemsTowEdit.SelectedValue == string.Empty)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "يجب تحديد القوائم ... ";
                return;
            }
        }
        else if (DLCountEdit.SelectedValue == "3")
        {
            if (DLSubItemsEdit.SelectedValue != string.Empty && DLSubItemsTowEdit.SelectedValue != string.Empty)
                FEdit();
            else if (DLSubItemsEdit.SelectedValue == string.Empty || DLSubItemsTowEdit.SelectedValue == string.Empty)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "يجب تحديد القوائم ... ";
                return;
            }
        }
    }

    private void FEdit()
    {
        try
        {
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss");
            Model_Main_Items_ MMI = new Model_Main_Items_()
            {
                IDCheck = "Edit",
                ID_Item = new Guid(HF_ID.Value),
                Type = XType,
                Count_Part = Convert.ToInt32(DLCountAdd.SelectedValue),
                ID_Part = new Guid(DLMain_Items_Edit.SelectedValue),
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

    protected void DLMain_ItemsAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLSubItemsAdd.Items.Clear(); DLSubItemsAdd.Items.Add("");
            DLSubItemsAdd.AppendDataBoundItems = true;
            FCountCheck("Add");
            FGetByDropList("SubItems", DLMain_ItemsAdd.SelectedValue);
            FGetLast("One", "Add", new Guid(DLMain_ItemsAdd.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowIDModelAdd();", true);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLSubItemsAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLSubItemsTowAdd.Items.Clear(); DLSubItemsTowAdd.Items.Add("");
            DLSubItemsTowAdd.AppendDataBoundItems = true;
            FCountCheck("Add");
            FGetByDropList("SubItemsTow", DLSubItemsAdd.SelectedValue);
            FGetLast("Tow", "Add", new Guid(DLSubItemsAdd.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowIDModelAdd();", true);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLSubItemsTowAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FCountCheck("Add");
            FGetLast("Three", "Add", new Guid(DLSubItemsTowAdd.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowIDModelAdd();", true);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLMain_Items_Edit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FGetLast("One", "Edit", new Guid(DLMain_ItemsAdd.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowIDModelEdit();", true);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLCountAdd_Load(object sender, EventArgs e)
    {
        DLCountAdd.Attributes["onchange"] = "ValidateAdd();";
    }

    protected void DLCountEdit_Load(object sender, EventArgs e)
    {
        DLCountEdit.Attributes["onchange"] = "ValidateEdit();";
    }

    private void FCountCheck(string XType)
    {
        if (XType == "Add")
        {
            if (DLCountAdd.SelectedValue == "1")
            {
                DLSubItemsAdd.Enabled = false; DLSubItemsTowAdd.Enabled = false;
            }
            if (DLCountAdd.SelectedValue == "2")
            {
                DLSubItemsAdd.Enabled = true; DLSubItemsTowAdd.Enabled = false;
            }
            if (DLCountAdd.SelectedValue == "3")
            {
                DLSubItemsAdd.Enabled = true; DLSubItemsTowAdd.Enabled = true;
            }
        }
        else if (XType == "Edit")
        {
            if (DLCountEdit.SelectedValue == "1")
            {
                DLSubItemsEdit.Enabled = false; DLSubItemsTowEdit.Enabled = false;
            }
            if (DLCountEdit.SelectedValue == "2")
            {
                DLSubItemsEdit.Enabled = true; DLSubItemsTowEdit.Enabled = false;
            }
            if (DLCountEdit.SelectedValue == "3")
            {
                DLSubItemsEdit.Enabled = true; DLSubItemsTowEdit.Enabled = true;
            }
        }
    }

    protected void DLSubItemsEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLSubItemsTowEdit.Items.Clear(); DLSubItemsTowEdit.Items.Add("");
            DLSubItemsTowEdit.AppendDataBoundItems = true;
            FCountCheck("Edit");
            FGetByDropList("SubItemsTow", DLSubItemsEdit.SelectedValue);
            FGetLast("Tow", "Edit", new Guid(DLSubItemsEdit.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowIDModelEdit();", true);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLSubItemsTowEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FCountCheck("Edit");
            FGetLast("Three", "Edit", new Guid(DLSubItemsTowEdit.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowIDModelEdit();", true);
        }
        catch (Exception)
        {
            return;
        }
    }

}