using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
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

public partial class Shaerd_ERP_FMS_Receipt_PageAdd : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_Bank_.FGetDropList("WithNull", "Ar", DL_Bank);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Category_.FGetDropList(1, "_ID", "_Ar", D_Category);
            Repostry_Category_.FGetDropList(1, "_ID", "_Ar", D_CategoryEdit);
            Repostry_Organstions_.FGetDropList(1, "_Receipt", "_ID", "_Ar", DLCompany);
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
            ClassQuaem.FGetSupportType(1, DL_Project);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            FGetLastBill(); FGetByDropList("MainItems", string.Empty);

            DataTable dtYearH = new DataTable();
            dtYearH.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassSaddam.GetCurrentTime().ToString("yyyy")) - 578; i >= Convert.ToInt32(ClassSaddam.GetCurrentTime().ToString("yyyy")) - 700; i--)
            {
                dtYearH.Rows.Add(i);
            }
            ddlYearsH.Items.Clear();
            ddlYearsH.Items.Add("");
            ddlYearsH.AppendDataBoundItems = true;
            ddlYearsH.DataTextField = "Year";
            ddlYearsH.DataValueField = "Year";
            ddlYearsH.DataSource = dtYearH;
            ddlYearsH.DataBind();

            if (Request.QueryString["ID"] != null)
                FGetData();
            System.Threading.Thread.Sleep(100);
        }
    }

    private void FGetByDropList(string XValue, string XMainItem)
    {
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();

            MMI.Top = 1000;
            MMI.ID_Item = Guid.Empty;
            MMI.Type = "_Receipt";
            if (XValue == "MainItems")
            {
                MMI.IDCheck = "GetByDropList";
                MMI.ID_Part = Guid.Empty;
            }
            else if (XValue == "SubItems")
            {
                MMI.IDCheck = "GetByDropListOne";
                MMI.ID_Part = new Guid(XMainItem);
            }
            else if (XValue == "SubItemsTow")
            {
                MMI.IDCheck = "GetByDropListTow";
                MMI.ID_Part = new Guid(XMainItem);
            }
            else if (XValue == "SubItemsThree")
            {
                MMI.IDCheck = "GetByDropListThree";
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
                    DLMainItems.Items.Clear(); DLMainItems.Items.Add("");
                    DLMainItems.AppendDataBoundItems = true; DLMainItems.DataValueField = "_ID";
                    DLMainItems.DataTextField = "_Ar"; DLMainItems.DataSource = dt;
                    DLMainItems.DataBind();

                }
                else if (XValue == "SubItems")
                {
                    DLSubItems.Items.Clear();
                    DLSubItems.Items.Add(""); DLSubItems.AppendDataBoundItems = true;
                    DLSubItems.DataValueField = "_ID"; DLSubItems.DataTextField = "_Ar";
                    DLSubItems.DataSource = dt; DLSubItems.DataBind();
                }
                else if (XValue == "SubItemsTow")
                {
                    DLSubItemsTow.Items.Clear();
                    DLSubItemsTow.Items.Add(""); DLSubItemsTow.AppendDataBoundItems = true;
                    DLSubItemsTow.DataValueField = "_ID"; DLSubItemsTow.DataTextField = "_Ar";
                    DLSubItemsTow.DataSource = dt; DLSubItemsTow.DataBind();
                }
                else if (XValue == "SubItemsThree")
                {
                    DLSubItemsThree.Items.Clear();
                    DLSubItemsThree.Items.Add(""); DLSubItemsThree.AppendDataBoundItems = true;
                    DLSubItemsThree.DataValueField = "_ID"; DLSubItemsThree.DataTextField = "_Ar";
                    DLSubItemsThree.DataSource = dt; DLSubItemsThree.DataBind();
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Receipt_ MR = new Model_Receipt_();
            MR.IDCheck = "GetByID";
            MR.ID_Item = new Guid(Request.QueryString["ID"]);
            MR.bill_Number = 0;
            MR.ID_Donor = Guid.Empty;
            MR.FilterSearch = string.Empty;
            MR.Start_Date = string.Empty;
            MR.End_Date = string.Empty;
            MR.DataCheck = string.Empty;
            MR.DataCheck2 = string.Empty;
            MR.DataCheck3 = string.Empty;
            MR.DataCheck4 = string.Empty;
            MR.DataCheck5 = string.Empty;
            MR.DataCheck6 = string.Empty;
            MR.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Receipt_ RR = new Repostry_Receipt_();
            dt = RR.BOM_Receipt_Manage(MR);
            if (dt.Rows.Count > 0)
            {
                //ddlYears.Enabled = false;
                ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                DLCount.SelectedValue = dt.Rows[0]["_Count_Part_"].ToString();
                DLMainItems.SelectedValue = dt.Rows[0]["_ID_Main_Item_"].ToString();
                FGetByDropList("SubItems", DLMainItems.SelectedValue);
                DLSubItems.SelectedValue = dt.Rows[0]["_ID_Sub_Item_"].ToString();

                if (DLCount.SelectedValue == "1")
                {
                    DLSubItemsTow.Enabled = false; DLSubItemsThree.Enabled = false;
                    DLSubItemsTow.SelectedValue = null; DLSubItemsThree.SelectedValue = null;
                }
                else if (DLCount.SelectedValue == "2")
                {
                    DLSubItemsTow.Enabled = true; DLSubItemsThree.Enabled = false;
                    FGetByDropList("SubItemsTow", DLSubItems.SelectedValue);
                    DLSubItemsTow.SelectedValue = dt.Rows[0]["_ID_Sub_Item_Tow_"].ToString();
                    DLSubItemsThree.SelectedValue = null;
                }
                else if (DLCount.SelectedValue == "3")
                {
                    DLSubItemsTow.Enabled = true; DLSubItemsThree.Enabled = true;
                    FGetByDropList("SubItemsTow", DLSubItems.SelectedValue);
                    DLSubItemsTow.SelectedValue = dt.Rows[0]["_ID_Sub_Item_Tow_"].ToString();
                    FGetByDropList("SubItemsThree", DLSubItemsTow.SelectedValue);
                    DLSubItemsThree.SelectedValue = dt.Rows[0]["_ID_Sub_Item_Three_"].ToString();
                }

                txtNumberBill.Text = dt.Rows[0]["_bill_Number_"].ToString();
                DLInitiatives.SelectedValue = dt.Rows[0]["_The_Initiative_"].ToString();
                DL_Project.SelectedValue = dt.Rows[0]["_ID_Project_"].ToString();
                DLCompany.SelectedValue = dt.Rows[0]["_ID_Donor_"].ToString();
                FGetPhoneCRM();

                txt_Note.Text = dt.Rows[0]["_Note_Bill_"].ToString();
                txtThe_Mony.Text = dt.Rows[0]["_The_Mony_"].ToString();
                RBIsCash_Money.Checked = Convert.ToBoolean(dt.Rows[0]["_IsCash_Money_"]);
                RBIsShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["_IsShayk_Bank_"]);
                RBIsConvert_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["_Transfer_On_Account_"]);
                FCheck();
                txtNumber_Shayk_Bank.Text = dt.Rows[0]["_Number_Shayk_Bank_"].ToString();
                txtDate_Shayk.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Shayk_"]).ToString("yyyy-MM-dd");
                txtFor_Bank.Text = dt.Rows[0]["_For_Bank_"].ToString();

                txtNumber_Account.Text = dt.Rows[0]["_Number_Account_"].ToString();
                txtDate_Bank_Transfer.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Bank_Transfer_"]).ToString("yyyy-MM-dd");
                txtFor_Bank_Transfer.Text = dt.Rows[0]["_For_Bank_Transfer_"].ToString();

                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah_"].ToString();
                DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["_IDAmmenAlSondoq_"].ToString();
                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["_ID_Moder_"].ToString();
                DLAccount.SelectedValue = dt.Rows[0]["_Finance_Account_"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Bank_"]))
                {
                    DL_Bank.SelectedValue = dt.Rows[0]["_ID_Bank_"].ToString();
                    Repostry_Account_.FGetDropList(1, "_ID", "_Ar", new Guid(DL_Bank.SelectedValue), DL_Account);
                    DL_Account.SelectedValue = dt.Rows[0]["_ID_Account_"].ToString();
                }
                if (dt.Rows[0]["_Type_Date_"].ToString() == "Melady")
                { RBDateM.Checked = true; RBDateH.Checked = false; }
                else if (dt.Rows[0]["_Type_Date_"].ToString() == "Hijri")
                { RBDateM.Checked = false; RBDateH.Checked = true; }
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");

                string DateHijri;
                DateTime dob = Convert.ToDateTime(txtDateAdd.Text);
                DateHijri = Convert.ToDateTime(ClassSaddam.ConvertDateCalendar(Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("dd/MM/yyyy")), "Hijri", "en-US")).ToString("dd/MM/yyyy");
                ddlDatesH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("dd");
                ddlMonthsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("MM");
                ddlYearsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("yyyy");
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

    private void FGetLastBill()
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Model_Receipt_ MR = new Model_Receipt_();
            MR.IDCheck = "GetLastBill";
            MR.ID_Item = new Guid(ddlYears.SelectedValue);
            MR.bill_Number = 0;
            MR.ID_Donor = Guid.Empty;
            MR.FilterSearch = string.Empty;
            MR.Start_Date = string.Empty;
            MR.End_Date = string.Empty;
            MR.DataCheck = string.Empty;
            MR.DataCheck2 = string.Empty;
            MR.DataCheck3 = string.Empty;
            MR.DataCheck4 = string.Empty;
            MR.DataCheck5 = string.Empty;
            MR.DataCheck6 = string.Empty;
            MR.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Receipt_ RR = new Repostry_Receipt_();
            dt = RR.BOM_Receipt_Manage(MR);
            if (dt.Rows.Count > 0)
                txtNumberBill.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["bill_Number"]) + 1);
            else
                txtNumberBill.Text = ClassSaddam.FGetNumberBillStart().ToString();

        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAdd.aspx");
    }

    protected void RBIsCash_Money_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FCheck();
    }

    protected void RBIsShayk_Bank_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FCheck();
    }

    protected void RBIsConvert_Bank_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FCheck();
    }

    private void FCheck()
    {
        if (RBIsCash_Money.Checked)
        {
            NumberShayk.Visible = false;
            Transfer_On_Account.Visible = false;
            FEmptyData();
        }
        else if (RBIsShayk_Bank.Checked)
        {
            NumberShayk.Visible = true;
            Transfer_On_Account.Visible = false;
            txtNumber_Shayk_Bank.Focus();
            FEmptyData();
        }
        else if (RBIsConvert_Bank.Checked)
        {
            NumberShayk.Visible = false;
            Transfer_On_Account.Visible = true;
            txtNumber_Account.Focus();
            FEmptyData();
        }
    }

    private void FEmptyData()
    {
        txtNumber_Shayk_Bank.Text = string.Empty;
        txtDate_Shayk.Text = string.Empty;
        txtFor_Bank.Text = string.Empty;
        txtNumber_Account.Text = string.Empty;
        txtDate_Bank_Transfer.Text = string.Empty;
        txtFor_Bank_Transfer.Text = string.Empty;
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if(DLAccount.SelectedValue == "البنك")
            {
                if(DL_Bank.SelectedValue == string.Empty || DL_Account.SelectedValue == string.Empty)
                {
                    lblWarning.Text = "يُرجى تحديد الحساب البنكي ,,, ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
            }
            if (txtNumberBill.Text.Trim() != string.Empty)
                if (RBIsCash_Money.Checked == false && RBIsShayk_Bank.Checked == false && RBIsConvert_Bank.Checked == false)
                {
                    lblWarning.Text = "الرجاء تحديد نوع الدفع ... ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
                else
                {
                    //if (Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                    //{
                    if (DLCount.SelectedValue == "1")
                        FOM_Receipt_Add();
                    else if (DLCount.SelectedValue == "2")
                    {
                        if (DLSubItemsTow.SelectedValue != string.Empty && DLSubItemsThree.SelectedValue == string.Empty)
                            FOM_Receipt_Add();
                        else if (DLSubItemsTow.SelectedValue == string.Empty && DLSubItemsThree.SelectedValue == string.Empty)
                        {
                            IDMessageWarning.Visible = true;
                            IDMessageSuccess.Visible = false;
                            lblWarning.Text = "يجب تحديد القوائم ... ";
                            return;
                        }
                    }
                    else if (DLCount.SelectedValue == "3")
                    {
                        if (DLSubItemsTow.SelectedValue != string.Empty && DLSubItemsThree.SelectedValue != string.Empty)
                            FOM_Receipt_Add();
                        else if (DLSubItemsTow.SelectedValue == string.Empty || DLSubItemsThree.SelectedValue == string.Empty)
                        {
                            IDMessageWarning.Visible = true;
                            IDMessageSuccess.Visible = false;
                            lblWarning.Text = "يجب تحديد القوائم ... ";
                            return;
                        }
                    }
                    //}
                    //else
                    //{
                    //    lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                    //    IDMessageSuccess.Visible = false;
                    //    IDMessageWarning.Visible = true;
                    //    return;
                    //}
                }
            else
            {
                lblWarning.Text = "يُرجى إدخال رقم الفاتورة ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            lblWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FOM_Receipt_Add()
    {
        string XType_Date = string.Empty;
        bool XIS_Bank = false;
        Guid XID_Bank = Guid.Empty, XID_Account = Guid.Empty;
        if (DLAccount.SelectedValue == "البنك")
        {
            XIS_Bank = true;
            XID_Bank = new Guid(DL_Bank.SelectedValue);
            XID_Account = new Guid(DL_Account.SelectedValue);
        }
        else if (DLAccount.SelectedValue != "البنك")
        {
            XIS_Bank = false; XID_Bank = Guid.Empty; XID_Account = Guid.Empty;
        }
        if (RBDateM.Checked == false && RBDateH.Checked)
        {
            if (ddlDatesH.SelectedValue != string.Empty && ddlMonthsH.SelectedValue != string.Empty && ddlYearsH.SelectedValue != string.Empty)
            {
                DateTime today = ClassSaddam.GetCurrentTime();
                Dates dateConvert = new Dates();
                string DateGen = dateConvert.HijriToGreg(Convert.ToDateTime(ddlYearsH.SelectedValue + "/" + ddlMonthsH.SelectedValue + "/" + ddlDatesH.SelectedValue).ToString("yyyy/MM/dd"));
                string yearH = ddlYearsH.SelectedValue;
                string monthH = ddlMonthsH.SelectedValue;
                string dateH = ddlDatesH.SelectedValue;
                txtDateAdd.Text = Convert.ToDateTime(DateGen).ToString("yyyy") + "-" + Convert.ToDateTime(DateGen).ToString("MM") + "-" + Convert.ToDateTime(DateGen).ToString("dd");
                XType_Date = "Hijri";
            }
            else
            {
                lblWarning.Text = "يُرجى تحديد تاريخ الفاتورة ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        else if (RBDateM.Checked && RBDateH.Checked == false)
        {
            if (txtDateAdd.Text != string.Empty)
                XType_Date = "Melady";
            else
            {
                lblWarning.Text = "يُرجى تحديد تاريخ الفاتورة ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }

        string Xresult = string.Empty;
        Guid _ID_Part_Tow = Guid.Empty, _ID_Part_Three = Guid.Empty;
        if (DLSubItemsTow.SelectedValue != string.Empty)
            _ID_Part_Tow = new Guid(DLSubItemsTow.SelectedValue);
        if (DLSubItemsThree.SelectedValue != string.Empty)
            _ID_Part_Three = new Guid(DLSubItemsThree.SelectedValue);
        int XID = Test_Saddam.FGetIDUsiq();
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        if (Request.QueryString["ID"] == null)
        {
            Model_Receipt_ MR = new Model_Receipt_()
            {
                IDCheck = "Add",
                ID_Item = Guid.NewGuid(),
                ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                Count_Part = Convert.ToInt32(DLCount.SelectedValue),
                ID_Main_Item = new Guid(DLMainItems.SelectedValue),
                ID_Sub_Item = new Guid(DLSubItems.SelectedValue),
                ID_Sub_Item_Tow = _ID_Part_Tow,
                ID_Sub_Item_Three = _ID_Part_Three,
                bill_Number = Convert.ToInt64(txtNumberBill.Text.Trim()),
                The_Initiative = Convert.ToInt32(DLInitiatives.SelectedValue),
                ID_Donor = new Guid(DLCompany.SelectedValue),
                ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
                Note_Bill = txt_Note.Text.Trim(),
                The_Mony = Convert.ToDecimal(txtThe_Mony.Text.Trim()),
                IsCash_Money = Convert.ToBoolean(RBIsCash_Money.Checked),
                IsShayk_Bank = Convert.ToBoolean(RBIsShayk_Bank.Checked),
                Number_Shayk_Bank = txtNumber_Shayk_Bank.Text.Trim(),
                Date_Shayk = txtDate_Shayk.Text.Trim(),
                For_Bank = txtFor_Bank.Text.Trim(),
                Transfer_On_Account = Convert.ToBoolean(RBIsConvert_Bank.Checked),
                Number_Account = txtNumber_Account.Text.Trim(),
                For_Bank_Transfer = txtFor_Bank_Transfer.Text.Trim(),
                Date_Bank_Transfer = txtDate_Bank_Transfer.Text.Trim(),
                IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                IsRaeesMaglisAlEdarah = Convert.ToBoolean(CBRaeesAlmaglis.Checked),
                IDRaees_Allow = 0,
                IDRaees_Date_Allow = XDate,
                IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                IsAmmenAlSondoq = Convert.ToBoolean(CBAmeenAlsondoq.Checked),
                IDAmmen_Allow = 0,
                IDAmmen_Date_Allow = XDate,
                ID_Moder = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Moder_Comment = string.Empty,
                ID_Moder_Allow = 0,
                ID_Moder_Date_Allow = XDate,
                Finance_Account = DLAccount.SelectedValue,
                Is_Bank = XIS_Bank,
                ID_Bank = XID_Bank,
                ID_Account = XID_Account,
                Type_Date = XType_Date,
                CreatedBy = XID,
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                ModifiedBy = 0,
                ModifiedDate = XDate,
                DeleteBy = 0,
                DeleteDate = XDate,
                IsActive = true
            };
            Repostry_Receipt_ RR = new Repostry_Receipt_();
            Xresult = RR.FOM_Receipt_Add(MR);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "لا يمكن تكرار رقم الفاتورة ... ";
                return;
            }
            else if (Xresult == "IsExistsAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة هذه الفاتورة مسبقاً , قم يتغير رقم الفاتورة ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                if (DLSend.SelectedValue == "Yes")
                {
                    string XShort_URL = ClassRandomURL.GetURL();
                    string XLong_URL = ClassSetting.FGetNameServer() + "/ar/Receipt/PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue;
                    string XResult = Attach_Repostry_Short_URL_.FShort_URL_Add("Add", XShort_URL, XLong_URL, "Om", "Receipt", XID, 0, XDate);
                    if (XResult == "IsSuccessAdd")
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhoneSender.Value, "سند قبض" + "\n" + ClassSetting.FGetNameServer() + "/Check.aspx?ID=" + XShort_URL, "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                }
                if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة سند قبض" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_Receipt_ MR = new Model_Receipt_()
            {
                IDCheck = "Edit",
                ID_Item = new Guid(Request.QueryString["ID"]),
                ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                Count_Part = Convert.ToInt32(DLCount.SelectedValue),
                ID_Main_Item = new Guid(DLMainItems.SelectedValue),
                ID_Sub_Item = new Guid(DLSubItems.SelectedValue),
                ID_Sub_Item_Tow = _ID_Part_Tow,
                ID_Sub_Item_Three = _ID_Part_Three,
                bill_Number = Convert.ToInt64(txtNumberBill.Text.Trim()),
                The_Initiative = Convert.ToInt32(DLInitiatives.SelectedValue),
                ID_Donor = new Guid(DLCompany.SelectedValue),
                ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
                Note_Bill = txt_Note.Text.Trim(),
                The_Mony = Convert.ToDecimal(txtThe_Mony.Text.Trim()),
                IsCash_Money = Convert.ToBoolean(RBIsCash_Money.Checked),
                IsShayk_Bank = Convert.ToBoolean(RBIsShayk_Bank.Checked),
                Number_Shayk_Bank = txtNumber_Shayk_Bank.Text.Trim(),
                Date_Shayk = txtDate_Shayk.Text.Trim(),
                For_Bank = txtFor_Bank.Text.Trim(),
                Transfer_On_Account = Convert.ToBoolean(RBIsConvert_Bank.Checked),
                Number_Account = txtNumber_Account.Text.Trim(),
                For_Bank_Transfer = txtFor_Bank_Transfer.Text.Trim(),
                Date_Bank_Transfer = txtDate_Bank_Transfer.Text.Trim(),
                IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                IsRaeesMaglisAlEdarah = false,
                IDRaees_Allow = 0,
                IDRaees_Date_Allow = XDate,
                IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                IsAmmenAlSondoq = false,
                IDAmmen_Allow = 0,
                IDAmmen_Date_Allow = XDate,
                ID_Moder = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Moder_Comment = string.Empty,
                ID_Moder_Allow = 0,
                ID_Moder_Date_Allow = XDate,
                Finance_Account = DLAccount.SelectedValue,
                Is_Bank = XIS_Bank,
                ID_Bank = XID_Bank,
                ID_Account = XID_Account,
                Type_Date = XType_Date,
                CreatedBy = 0,
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                ModifiedBy = XID,
                ModifiedDate = XDate,
                DeleteBy = 0,
                DeleteDate = string.Empty,
                IsActive = true
            };
            Repostry_Receipt_ RR = new Repostry_Receipt_();
            Xresult = RR.FOM_Receipt_Add(MR);
            if (Xresult == "IsExistsNumberEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "لا يمكن تكرار رقم الفاتورة ... ";
                return;
            }
            if (Xresult == "IsExistsEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة هذه الفاتورة مسبقاً , قم يتغير رقم الفاتورة ... ";
                return;
            }
            else if (Xresult == "IsSuccessEdit")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                if (DLSend.SelectedValue == "Yes")
                {
                    string XShort_URL = ClassRandomURL.GetURL();
                    string XLong_URL = ClassSetting.FGetNameServer() + "/ar/Receipt/PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue;
                    string XResult = Attach_Repostry_Short_URL_.FShort_URL_Add("Add", XShort_URL, XLong_URL, "Om", "Receipt", XID, 0, XDate);
                    if (XResult == "IsSuccessAdd")
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhoneSender.Value, "سند قبض" + "\n" + ClassSetting.FGetNameServer() + "/Check.aspx?ID=" + XShort_URL, "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                }
                if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل سند قبض" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
                if (Request.QueryString["ID"] != null)
                    FGetData();
            }
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
        FGetLastBill();
    }

    private void FAdd(string XIDCheck)
    {
        Guid XID_Item = Guid.Empty, XID_Category = Guid.Empty;
        string XType_Customer = string.Empty, XName = string.Empty, XPhone_Number = string.Empty;
        int XIDAdd = 0, XIDUpdate = 0;
        if (XIDCheck == "Add")
        {
            XID_Item = Guid.NewGuid();
            XID_Category = new Guid(D_Category.SelectedValue);
            XType_Customer = DLType_Customer.SelectedValue;
            XName = txtName.Text.Trim();
            XPhone_Number = txtPhone_Number1.Text.Trim();
            XIDAdd = Test_Saddam.FGetIDUsiq();
        }
        if (XIDCheck == "Edit")
        {
            XID_Item = new Guid(HFID.Value);
            XID_Category = new Guid(D_CategoryEdit.SelectedValue);
            XType_Customer = DLType_CustomerEdit.SelectedValue;
            XName = txtNameEdit.Text.Trim();
            XPhone_Number = txtPhone_Number1Edit.Text.Trim();
            XIDUpdate = Test_Saddam.FGetIDUsiq();
        }

        string Xresult = Repostry_Organstions_.FAPP_Add(XIDCheck, XID_Item, XID_Category, "_Receipt", XType_Customer,
            XName, Repostry_Organstions_.FGetCount("GetLastRecord", 1, Guid.Empty, "_Receipt", string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1,
            XPhone_Number, XIDAdd, XIDUpdate, 0, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsExists")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
            txtName.Focus();
            return;
        }
        else if (Xresult == "IsSuccess")
        {
            Repostry_Organstions_.FGetDropList(1, "_Receipt", "_ID", "_Ar", DLCompany);
            DLCompany.SelectedValue = XID_Item.ToString();
            HFPhone.Value = XPhone_Number;
            HFPhoneSender.Value = HFPhone.Value;
            lblPhone.InnerHtml = XPhone_Number;
            LBEdit.Text = "تعديل الرقم <i class='fa fa-phone'></i>";
            HFID.Value = XID_Item.ToString();
        }
    }

    protected void LBSave2_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FAdd("Add");
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LB_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void DLCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FGetPhoneCRM();
        }
        catch (Exception)
        {

        }
    }

    private void FGetPhoneCRM()
    {
        DataTable dt = new DataTable();
        dt = Repostry_Organstions_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(DLCompany.SelectedValue), "_Receipt", string.Empty, string.Empty, string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            HFID.Value = dt.Rows[0]["_ID_Item_"].ToString();
            HFPhoneSender.Value = dt.Rows[0]["_Phone_Number_"].ToString();
            lblPhone.InnerHtml = HFPhoneSender.Value;
            LBEdit.Text = "تعديل الرقم <i class='fa fa-phone'></i>";
        }
    }

    private void FCountCheck()
    {
        if (DLCount.SelectedValue == "1")
        {
            DLSubItemsTow.Enabled = false; DLSubItemsThree.Enabled = false;
        }
        if (DLCount.SelectedValue == "2")
        {
            DLSubItemsTow.Enabled = true; DLSubItemsThree.Enabled = false;
        }
        if (DLCount.SelectedValue == "3")
        {
            DLSubItemsTow.Enabled = true; DLSubItemsThree.Enabled = true;
        }
    }

    protected void DLMainItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetByDropList("SubItems", DLMainItems.SelectedValue);
        FCountCheck(); txt_Note.Focus();
    }

    protected void DLCount_Load(object sender, EventArgs e)
    {
        DLCount.Attributes["onchange"] = "ValidateAdd();";
    }

    protected void DLSubItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLSubItemsTow.Items.Clear(); DLSubItemsTow.Items.Add("");
            DLSubItemsTow.AppendDataBoundItems = true;
            FGetByDropList("SubItemsTow", DLSubItems.SelectedValue);
            FCountCheck(); txt_Note.Focus();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLSubItemsTow_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLSubItemsThree.Items.Clear(); DLSubItemsThree.Items.Add("");
            DLSubItemsThree.AppendDataBoundItems = true;
            FGetByDropList("SubItemsThree", DLSubItemsTow.SelectedValue);
            FCountCheck(); txt_Note.Focus();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBEdit_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {

            DataTable dt = new DataTable();
            dt = Repostry_Organstions_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(HFID.Value), string.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                D_CategoryEdit.SelectedValue = dt.Rows[0]["_ID_Category_"].ToString();
                DLType_CustomerEdit.SelectedValue = dt.Rows[0]["_Type_Customer_"].ToString();
                txtNameEdit.Text = dt.Rows[0]["_Name_"].ToString();
                txtPhone_Number1Edit.Text = dt.Rows[0]["_Phone_Number_"].ToString();
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

    protected void LBSaveEdit_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FAdd("Edit");
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "Melady")
        {
            if (RBDateM.Checked)
                XResult = "display:block;";
            else if (RBDateM.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Hijri")
        {
            if (RBDateH.Checked)
                XResult = "display:block;";
            else if (RBDateH.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Bank")
        {
            if (DLAccount.SelectedValue == "البنك")
                XResult = "display:block;";
            else
                XResult = "display:none;";
        }
        return XResult;
    }

    protected void DLAccount_Load(object sender, EventArgs e)
    {
        DLAccount.Attributes["onchange"] = "Validate();";
    }

    protected void DL_Bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Repostry_Account_.FGetDropList(1, "_ID", "_Ar", new Guid(DL_Bank.SelectedValue), DL_Account);
        }
        catch (Exception)
        {

        }
    }

    protected void DL_Account_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        RBIsCash_Money.Checked = false; RBIsShayk_Bank.Checked = false; RBIsConvert_Bank.Checked = true;
        FCheck();
        txtFor_Bank_Transfer.Text = DL_Bank.SelectedItem.Text;
        txtDate_Bank_Transfer.Text = txtDateAdd.Text.Trim();
        string XAccount = DL_Account.SelectedItem.Text.Split(new char[] { '[', ']' })[1];
        txtNumber_Account.Text = XAccount.Trim();
        txtThe_Mony.Focus();
    }

}