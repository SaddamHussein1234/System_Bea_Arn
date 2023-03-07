using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
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

public partial class Shaerd_ERP_FMS_Cash_Donation_PageAdd : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_Bank_.FGetDropList("WithNull", "Ar", DL_Bank);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            Repostry_Company_Type_.FCRM_Company_Type_Manage(ddlCompanyType);
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
            ClassQuaem.FGetSupportType(1, DL_Project);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            FGetLastBill();

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
            Repostry_Country_.FErp_Country_Manage(ddlCountry);
            ddlCountry.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            System.Threading.Thread.Sleep(100);
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Cash_Donation_ MCD = new Model_Cash_Donation_();
            MCD.IDCheck = "GetByID";
            MCD.ID_Item = new Guid(Request.QueryString["ID"]);
            MCD.bill_Number = 0;
            MCD.ID_Donor = Guid.Empty;
            MCD.Start_Date = string.Empty;
            MCD.End_Date = string.Empty;
            MCD.DataCheck = string.Empty;
            MCD.DataCheck2 = string.Empty;
            MCD.DataCheck3 = string.Empty;
            MCD.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Cash_Donation_ RCD = new Repostry_Cash_Donation_();
            dt = RCD.BOM_Cash_Donation_Manage(MCD);
            if (dt.Rows.Count > 0)
            {
                //ddlYears.Enabled = false;
                ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                txtNumberBill.Text = dt.Rows[0]["_bill_Number_"].ToString();
                DLInitiatives.SelectedValue = dt.Rows[0]["_The_Initiative_"].ToString();
                DLCompany.SelectedValue = dt.Rows[0]["_ID_Donor_"].ToString();
                FGetPhoneCRM();
                DL_Project.SelectedValue = dt.Rows[0]["_ID_Project_"].ToString();
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
                DL_For.SelectedValue = dt.Rows[0]["_ThatsAbout_"].ToString();
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
            Model_Cash_Donation_ MCD = new Model_Cash_Donation_();
            MCD.IDCheck = "GetLastBill";
            MCD.ID_Item = new Guid(ddlYears.SelectedValue);
            MCD.bill_Number = 0;
            MCD.ID_Donor = Guid.Empty;
            MCD.Start_Date = string.Empty;
            MCD.End_Date = string.Empty;
            MCD.DataCheck = string.Empty;
            MCD.DataCheck2 = string.Empty;
            MCD.DataCheck3 = string.Empty;
            MCD.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Cash_Donation_ RCD = new Repostry_Cash_Donation_();
            dt = RCD.BOM_Cash_Donation_Manage(MCD);
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
            if (DLAccount.SelectedValue == "البنك")
            {
                if (DL_Bank.SelectedValue == string.Empty || DL_Account.SelectedValue == string.Empty)
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
                    //    FOM_Cash_Donation_Add();
                    //else
                    //{
                    //    lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                    //    IDMessageSuccess.Visible = false;
                    //    IDMessageWarning.Visible = true;
                    //    return;
                    //}
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
                            FOM_Cash_Donation_Add("Hijri");
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
                            FOM_Cash_Donation_Add("Melady");
                        else
                        {
                            lblWarning.Text = "يُرجى تحديد تاريخ الفاتورة ... ";
                            IDMessageSuccess.Visible = false;
                            IDMessageWarning.Visible = true;
                            return;
                        }
                    }
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

    private void FOM_Cash_Donation_Add(string XType_Date)
    {
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
        int XID = Test_Saddam.FGetIDUsiq();
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        if (Request.QueryString["ID"] == null)
        {
            Model_Cash_Donation_ MCD = new Model_Cash_Donation_()
            {
                IDCheck = "Add",
                ID_Item = Guid.NewGuid(),
                ID_FinancialYear = new Guid(ddlYears.SelectedValue),
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
                ThatsAbout = DL_For.SelectedValue,
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
            Repostry_Cash_Donation_ RCD = new Repostry_Cash_Donation_();
            string Xresult = RCD.FOM_Cash_Donation_Add(MCD);
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
                    string XLong_URL = ClassSetting.FGetNameServer() + "/ar/Cash_Donation/PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue;
                    string XResult = Attach_Repostry_Short_URL_.FShort_URL_Add("Add", XShort_URL, XLong_URL, "Om", "Cash_Donation", XID, 0, XDate);
                    if (XResult == "IsSuccessAdd")
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "إيصال تبرع نقدي" + "\n" + ClassSetting.FGetNameServer() + "/Check.aspx?ID=" + XShort_URL, "BerArn", "Add", XID);
                }
                if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة إيصال تبرع نقدي" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", XID);
                Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue + "");
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_Cash_Donation_ MCD = new Model_Cash_Donation_()
            {
                IDCheck = "Edit",
                ID_Item = new Guid(Request.QueryString["ID"]),
                ID_FinancialYear = new Guid(ddlYears.SelectedValue),
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
                ThatsAbout = DL_For.SelectedValue,
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
            Repostry_Cash_Donation_ RCD = new Repostry_Cash_Donation_();
            string Xresult = RCD.FOM_Cash_Donation_Add(MCD);
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
                    string XLong_URL = ClassSetting.FGetNameServer() + "/ar/Cash_Donation/PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue;
                    string XResult = Attach_Repostry_Short_URL_.FShort_URL_Add("Add", XShort_URL, XLong_URL, "Om", "Cash_Donation", XID, 0, XDate);
                    if (XResult == "IsSuccessAdd")
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "إيصال تبرع نقدي" + "\n" + ClassSetting.FGetNameServer() + "/Check.aspx?ID=" + XShort_URL, "BerArn", "Edit", XID);
                }
                if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل إيصال تبرع نقدي" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", XID);
                if (Request.QueryString["ID"] != null)
                    FGetData();
            }
            lbmsg.Text = Xresult;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
        FGetLastBill();
    }

    private void FAdd()
    {
        Guid XID = Guid.NewGuid();
        Model_Company_ MC = new Model_Company_()
        {
            IDCheck = "Add",
            ID_Item = XID,
            Type_Customer = DLType_Customer.SelectedValue,
            Company_Name = txtCompanyName.Text.Trim(),
            Company_Type = new Guid(ddlCompanyType.SelectedValue),
            Registration_No = Repostry_Company_.FCRM_Company_Manage() + 1,
            Address = string.Empty,
            Country = new Guid(ddlCountry.SelectedValue),
            City = string.Empty,
            Website = string.Empty,
            Email_Address = ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com",
            Established_Year = 0,
            Fax = string.Empty,
            Phone_Number1 = txtPhone_Number1.Text.Trim(),
            Mobile_Number1 = string.Empty,
            Phone_Number2 = string.Empty,
            Icon_Img = "ImgSystem/ImgCompany/no-img.jpg",
            Is_Active = true,
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            Is_Delete = false
        };

        Repostry_Company_ RC = new Repostry_Company_();
        string Xresult = RC.FCRM_Company_Add(MC);
        if (Xresult == "IsSuccessAdd")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            DLCompany.SelectedValue = XID.ToString(); ;
            HFPhone.Value = txtPhone_Number1.Text.Trim();
            lblPhone.InnerHtml = txtPhone_Number1.Text.Trim() + " <a href='/Cpanel/ERP/CRM/PageCompany/PageCompanyAdd.aspx?ID=" + DLCompany.SelectedValue
                + "'>تعديل الرقم <i class='fa fa-phone'></i></a>";
        }
    }

    protected void LBSave2_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FAdd();
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
        Response.Redirect("PageCash_Donation.aspx");
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
        Model_Company_ MC = new Model_Company_();
        MC.IDCheck = "GetByIDUniq";
        MC.ID_Item = new Guid(DLCompany.SelectedValue);
        MC.Company_Name = string.Empty;
        MC.Is_Active = false;
        MC.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_Company_ RC = new Repostry_Company_();
        dt = RC.BCRM_Company_Manage(MC);
        if (dt.Rows.Count > 0)
        {
            HFPhone.Value = dt.Rows[0]["_Phone_Number1_"].ToString();
            lblPhone.InnerHtml = HFPhone.Value + " <a href='/Cpanel/ERP/CRM/PageCompany/PageCompanyAdd.aspx?ID=" + DLCompany.SelectedValue
                + "'>تعديل الرقم <i class='fa fa-phone'></i></a>";
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