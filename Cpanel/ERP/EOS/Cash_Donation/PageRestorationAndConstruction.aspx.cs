using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_Cash_Donation_PageRestorationAndConstruction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A106");
            Repostry_Bank_.FGetDropList("WithNull", "Ar", DL_Bank);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            pnlStar.Visible = true;
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            txtNumberMostafeed.Focus();
            FGetAmeenAlmostodaa();
            txtProductionDate.Text = ClassSaddam.FGetDateTo();
            txt_Add.Text = txtProductionDate.Text;
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
            pnlDataMosTafeed.Visible = false;
            pnlMostafeed.Visible = true;
            pnlAlDaam.Visible = false;
            txtNumberMostafeed.Focus();
            ClassQuaem.FGetSupportType(1, "'4'", DLSupportType);
            ClassMosTafeed.FGetName(DLName);
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_();
            MBAT.IDCheck = "GetByID";
            MBAT.IDUniq = new Guid(Request.QueryString["ID"]);
            MBAT.ID_FinancialYear = Guid.Empty;
            MBAT.ID_Donor = Guid.Empty;
            MBAT.NumberMostafeed = 0;
            MBAT.billNumber = 0;
            MBAT.ID_Project = 0;
            MBAT.Start_Date = string.Empty;
            MBAT.End_Date = string.Empty;
            MBAT.DataCheck = string.Empty;
            MBAT.DataCheck2 = string.Empty;
            MBAT.DataCheck3 = string.Empty;
            MBAT.IsTarmem = false;
            MBAT.IsBena = false;
            MBAT.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            dt = RBAT.BArn_BenaaAndTarmim_Manage(MBAT);

            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                DLCompany.SelectedValue = dt.Rows[0]["_ID_Donor_"].ToString();

                RBTarmimCheck.Checked = Convert.ToBoolean(dt.Rows[0]["IsTarmem"]);
                RBBenaCheck.Checked = Convert.ToBoolean(dt.Rows[0]["IsBena"]);

                txtNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                DLName.SelectedValue = txtNumberMostafeed.Text;
                txtNumberOrder.Text = dt.Rows[0]["billNumber_"].ToString();
                pnlStar.Visible = false;
                txtNumberMostafeed.Enabled = true;
                DLName.Enabled = true;
                DLInitiatives.Enabled = true;
                txtNumberOrder.Enabled = true; DLCompany.Enabled = true;
                FGetDataMostafed();
                txtThe_Mony.Text = dt.Rows[0]["The_Mony"].ToString();
                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(txtThe_Mony.Text), currencies[Convert.ToInt32(0)]);
                txtThe_Mony_Word.Text = toWord.ConvertToArabic();

                RBIsCash_Money.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                RBIsShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                RBIsConvert_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["_Transfer_On_Account_"]);
                FCheck();
                txtNumber_Shayk_Bank.Text = dt.Rows[0]["Number_Shayk_Bank"].ToString();
                txtDate_Shayk.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy-MM-dd");
                txtFor_Bank.Text = dt.Rows[0]["_For_Bank_"].ToString();

                txtNumber_Account.Text = dt.Rows[0]["_Number_Account_"].ToString();
                txtDate_Bank_Transfer.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Bank_Transfer_"]).ToString("yyyy-MM-dd");
                txtFor_Bank_Transfer.Text = dt.Rows[0]["_For_Bank_Transfer_"].ToString();

                txtProductionDate.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
                DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["IDAmeenAlsondoq"].ToString();
                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["IDRaeesAlMagles"].ToString();
                DLSupportType.SelectedValue = dt.Rows[0]["ID_Type"].ToString();
                txt_Add.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
                txtMoreDetails.Text = dt.Rows[0]["More_Details"].ToString();
                DLInitiatives.SelectedValue = dt.Rows[0]["_ID_DLInitiatives_"].ToString();
                txt_Note.Text = dt.Rows[0]["_Note_"].ToString();
                DLAccount.SelectedValue = dt.Rows[0]["_Finance_Account_"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Bank_"]))
                {
                    DL_Bank.SelectedValue = dt.Rows[0]["_ID_Bank_"].ToString();
                    Repostry_Account_.FGetDropList(1, "_ID", "_Ar", new Guid(DL_Bank.SelectedValue), DL_Account);
                    DL_Account.SelectedValue = dt.Rows[0]["_ID_Account_"].ToString();
                }
                if (DLAccount.SelectedValue != string.Empty)
                    FGetMony();
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FGetAmeenAlmostodaa()
    {
        ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
        ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
        ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);

        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsNaebMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLNaeebRaeesMagles.DataValueField = "ID_Item";
            DLNaeebRaeesMagles.DataTextField = "FirstName";
            DLNaeebRaeesMagles.DataSource = dt;
            DLNaeebRaeesMagles.DataBind();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageRestorationAndConstruction.aspx");
    }

    protected void RBBenaCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            DLSupportType.SelectedValue = "10"; txtNumberMostafeed.Focus();
            pnlStar.Visible = false; txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true; DLInitiatives.Enabled = true;
            txtNumberOrder.Enabled = true; DLCompany.Enabled = true;
            txtMoreDetails.Text = "وذلك عن مشروع / بناء منزل المستفيد الموضح بياناته بعالية , بناء على الفواتير المرفقة .";
            FGetLastBill();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void RBTarmimCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            DLSupportType.SelectedValue = "11"; txtNumberMostafeed.Focus();
            pnlStar.Visible = false; txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true; DLInitiatives.Enabled = true;
            txtNumberOrder.Enabled = true; DLCompany.Enabled = true;
            txtMoreDetails.Text = "وذلك عن مشروع / ترميم منزل المستفيد الموضح بياناته بعالية , بناء على الفواتير المرفقة .";
            FGetLastBill();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;

        FGetDataMostafed();
    }

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And TypeMostafeed = @1 And IsDelete = @2", txtNumberMostafeed.Text.Trim(), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            pnlDataMosTafeed.Visible = true;
            pnlAlDaam.Visible = true;
            //if (GVMatterOfExchangeByID.Rows.Count > 0)
            //{
            //    FArnProductShopMatterOfExchangeByUser();
            //}
            //else
            //{
            //    Response.Redirect("PageManageProductMatterOfExchange.aspx");
            //}
            txtThe_Mony.Focus();
        }
        else
        {
            lblWarning.Text = "يبدو ان هذا المستفيد مستبعد";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;

            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
        }
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetDataMostafedByName();
    }

    private void FGetDataMostafedByName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            pnlDataMosTafeed.Visible = true;
            pnlAlDaam.Visible = true;
            txtThe_Mony.Focus();
        }
        else
        {
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
        }
    }

    protected void txtThe_Mony_TextChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(txtThe_Mony.Text), currencies[Convert.ToInt32(0)]);
            txtThe_Mony_Word.Text = toWord.ConvertToArabic();
        }
        catch (Exception)
        {
            return;
        }
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

    protected void btnAdd_Click(object sender, EventArgs e)
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
            if (txtNumberOrder.Text.Trim() != string.Empty)
            {
                if (RBIsCash_Money.Checked == false && RBIsShayk_Bank.Checked == false && RBIsConvert_Bank.Checked == false)
                {
                    lblWarning.Text = "الرجاء تحديد نوع الدفع ... ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
                else
                {
                    if (Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                        FCheckNumberOrder();
                    else
                    {
                        lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
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

    private void FCheckNumberOrder()
    {
        if (DLSupportType.SelectedValue == "10")
        {
            //DataTable dtRequestOfThrBeneficiary = new DataTable();
            //dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And BenaManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
            //if (dtRequestOfThrBeneficiary.Rows.Count > 0)
                FArn_BenaaAndTarmim_Add();
            //else
            //{
            //    lblWarning.Text = "المستفيد لا يحتاج إلى بناء منزل";
            //    IDMessageSuccess.Visible = false;
            //    IDMessageWarning.Visible = true;
            //}
        }
        else if (DLSupportType.SelectedValue == "11")
        {
            //DataTable dtRequestOfThrBeneficiary = new DataTable();
            //dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And TarmemManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
            //if (dtRequestOfThrBeneficiary.Rows.Count > 0)
                FArn_BenaaAndTarmim_Add();
            //else
            //{
            //    lblWarning.Text = "المستفيد لا يحتاج إلى ترميم منزل";
            //    IDMessageSuccess.Visible = false;
            //    IDMessageWarning.Visible = true;
            //}
        }

    }

    private void FArn_BenaaAndTarmim_Add()
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
        int ID_Qariah = ClassMosTafeed.FGetMosTafeedIDQariah(Convert.ToInt32(txtNumberMostafeed.Text.Trim()));
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        bool IsTarmem_ = false, IsBena_ = false;
        if (DLSupportType.SelectedValue == "10")
        {IsTarmem_ = false;IsBena_ = true;}
        else if (DLSupportType.SelectedValue == "11")
        {IsTarmem_ = true;IsBena_ = false;}
        string Xresult = string.Empty;
        if (Request.QueryString["id"] == null)
        {
            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_()
            {
                IDCheck = "Add",
                IDUniq = Guid.NewGuid(),
                ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                ID_Donor = new Guid(DLCompany.SelectedValue),
                NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
                billNumber = Convert.ToInt32(txtNumberOrder.Text.Trim()),
                The_Mony = Convert.ToDecimal(txtThe_Mony.Text.Trim()),
                IsCash_Money = RBIsCash_Money.Checked,
                IsShayk_Bank = RBIsShayk_Bank.Checked,
                Number_Shayk_Bank = txtNumber_Shayk_Bank.Text.Trim(),
                Date_Get = txtDate_Shayk.Text.Trim(),
                For_Bank = txtFor_Bank.Text.Trim(),
                Transfer_On_Account = RBIsConvert_Bank.Checked,
                Number_Account = txtNumber_Account.Text.Trim(),
                For_Bank_Transfer = txtFor_Bank_Transfer.Text.Trim(),
                Date_Bank_Transfer = txtDate_Bank_Transfer.Text.Trim(),
                IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                IsAllowModer = false,
                IDModer_Allow = 0,
                IDModer_Date_Allow = XDate,
                IDAmeenAlsondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                AllowState = false,
                AllowStateDetalis = string.Empty,
                NotAllowState = false,
                WhayNotAllow = string.Empty,
                ID_Allow_Ameen = 0,
                Date_AllowOrNotAllow = XDate,
                IDRaeesAlMagles = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                IsAllowRaeesAlMagles = false,
                IDRaees_Allow = 0,
                IDRaees_Date_Allow = XDate,
                IDAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue),
                ID_Project = Convert.ToInt64(DLSupportType.SelectedValue),
                More_Details = txtMoreDetails.Text.Trim(),
                IsTarmem = IsTarmem_,
                IsBena = IsBena_,
                ID_DLInitiatives = Convert.ToInt32(DLInitiatives.SelectedValue),
                Note_ = txt_Note.Text.Trim(),
                Finance_Account = DLAccount.SelectedValue,
                Is_Bank = XIS_Bank,
                ID_Bank = XID_Bank,
                ID_Account = XID_Account,
                CreatedBy = Test_Saddam.FGetIDUsiq(),
                CreatedDate = Convert.ToDateTime(txt_Add.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                ModifiedBy = 0,
                ModifiedDate = XDate,
                DeleteBy = 0,
                DeleteDate = XDate,
                IsDelete = false,
                Al_Qaryah = ID_Qariah
            };

            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            Xresult = RBAT.FArn_BenaaAndTarmim_Add(MBAT);
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

                if(RBBenaCheck.Checked && RBTarmimCheck.Checked == false)
                {
                    if (Attach_Repostry_SMS_Send_.AllSendSystemEOS())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة صرف نقدي" + "\n" + "لمشروع: بناء المنازل" + "\n" + "ر/الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                }
                else if (RBBenaCheck.Checked == false && RBTarmimCheck.Checked)
                {
                    if (Attach_Repostry_SMS_Send_.AllSendSystemEOS())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة صرف نقدي" + "\n" + "لمشروع: ترميم المنازل" + "\n" + "ر/الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                }

                if (DLSupportType.SelectedValue == "10")
                {
                    DataTable dtRequestBena = new DataTable();
                    dtRequestBena = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And BenaManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
                    if (dtRequestBena.Rows.Count > 0)
                    {
                        if (CBFinish.Checked)
                        {
                            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                            conn.Open();
                            string sql = "UPDATE [dbo].[ReportAlZyarat] SET [BenaManzil] = @BenaManzil Where IDItem = @IDItem And NumberMostafeed = @IDMustafeed";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@IDItem", Convert.ToInt64(dtRequestBena.Rows[0]["IDItem"].ToString()));
                            cmd.Parameters.AddWithValue("@IDMustafeed", Convert.ToInt64(txtNumberMostafeed.Text.Trim()));
                            cmd.Parameters.AddWithValue("@BenaManzil", false);
                            cmd.ExecuteScalar();
                            conn.Close();
                        }
                    }
                }
                else if (DLSupportType.SelectedValue == "11")
                {
                    DataTable dtRequestTarmim = new DataTable();
                    dtRequestTarmim = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And TarmemManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
                    if (dtRequestTarmim.Rows.Count > 0)
                    {
                        if (CBFinish.Checked)
                        {
                            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                            conn.Open();
                            string sql = "UPDATE [dbo].[ReportAlZyarat] SET [TarmemManzil] = @TarmemManzil Where IDItem = @IDItem And NumberMostafeed = @IDMustafeed";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@IDItem", Convert.ToInt64(dtRequestTarmim.Rows[0]["IDItem"].ToString()));
                            cmd.Parameters.AddWithValue("@IDMustafeed", Convert.ToInt64(txtNumberMostafeed.Text.Trim()));
                            cmd.Parameters.AddWithValue("@TarmemManzil", false);
                            cmd.ExecuteScalar();
                            conn.Close();
                        }
                    }
                }

                FGetLastBill();
                txtNumberMostafeed.Text = string.Empty;
                DLName.SelectedValue = null;
                pnlDataMosTafeed.Visible = false;
                txtThe_Mony.Text = string.Empty;
                RBIsCash_Money.Checked = false; RBIsShayk_Bank.Checked = false; RBIsConvert_Bank.Checked = false;
                NumberShayk.Visible = false; Transfer_On_Account.Visible = false;

                //string XURL = "../In_Kind_Donation/PageView.aspx?IDUniq=" + MBAT.ID_FinancialYear.ToString() + "&IDX=" + MBAT.billNumber.ToString()
                //     + "&XID=" + MBAT.NumberMostafeed.ToString()
                //    + "&IsBena=" + MBAT.IsBena.ToString() + "&IsTarmem=" + MBAT.IsTarmem.ToString();
                //Response.Redirect(XURL);
            }

        }
        else if (Request.QueryString["id"] != null)
        {
            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_()
            {
                IDCheck = "Edit",
                IDUniq = new Guid(Request.QueryString["ID"]),
                ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                ID_Donor = new Guid(DLCompany.SelectedValue),
                NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
                billNumber = Convert.ToInt32(txtNumberOrder.Text.Trim()),
                The_Mony = Convert.ToDecimal(txtThe_Mony.Text.Trim()),
                IsCash_Money = RBIsCash_Money.Checked,
                IsShayk_Bank = RBIsShayk_Bank.Checked,
                Number_Shayk_Bank = txtNumber_Shayk_Bank.Text.Trim(),
                Date_Get = txtDate_Shayk.Text.Trim(),
                For_Bank = txtFor_Bank.Text.Trim(),
                Transfer_On_Account = RBIsConvert_Bank.Checked,
                Number_Account = txtNumber_Account.Text.Trim(),
                For_Bank_Transfer = txtFor_Bank_Transfer.Text.Trim(),
                Date_Bank_Transfer = txtDate_Bank_Transfer.Text.Trim(),
                IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                IsAllowModer = false,
                IDModer_Allow = 0,
                IDModer_Date_Allow = XDate,
                IDAmeenAlsondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                AllowState = false,
                AllowStateDetalis = string.Empty,
                NotAllowState = false,
                WhayNotAllow = string.Empty,
                ID_Allow_Ameen = 0,
                Date_AllowOrNotAllow = XDate,
                IDRaeesAlMagles = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                IsAllowRaeesAlMagles = false,
                IDRaees_Allow = 0,
                IDRaees_Date_Allow = XDate,
                IDAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue),
                ID_Project = Convert.ToInt64(DLSupportType.SelectedValue),
                More_Details = txtMoreDetails.Text.Trim(),
                IsTarmem = IsTarmem_,
                IsBena = IsBena_,
                ID_DLInitiatives = Convert.ToInt32(DLInitiatives.SelectedValue),
                Note_ = txt_Note.Text.Trim(),
                Finance_Account = DLAccount.SelectedValue,
                Is_Bank = XIS_Bank,
                ID_Bank = XID_Bank,
                ID_Account = XID_Account,
                CreatedBy = 0,
                CreatedDate = Convert.ToDateTime(txt_Add.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                ModifiedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedDate = XDate,
                DeleteBy = 0,
                DeleteDate = XDate,
                IsDelete = false,
                Al_Qaryah = ID_Qariah
            };
            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            Xresult = RBAT.FArn_BenaaAndTarmim_Add(MBAT);
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

                if (RBBenaCheck.Checked && RBTarmimCheck.Checked == false)
                {
                    if (Attach_Repostry_SMS_Send_.AllSendSystemEOS())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل صرف نقدي" + "\n" + "لمشروع: بناء المنازل" + "\n" + "ر/الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
                }
                else if (RBBenaCheck.Checked == false && RBTarmimCheck.Checked)
                {
                    if (Attach_Repostry_SMS_Send_.AllSendSystemEOS())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل صرف نقدي" + "\n" + "لمشروع: ترميم المنازل" + "\n" + "ر/الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
                }

                if (Request.QueryString["ID"] != null)
                    FGetData();
            }
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            txt_Add.Text = Convert.ToDateTime(txt_Add.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
            FGetLastBill();
            FGetMony();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetLastBill()
    {
        txtNumberOrder.Text = Repostry_BenaaAndTarmim_.FGetLastBill(new Guid(ddlYears.SelectedValue), Convert.ToInt64(DLSupportType.SelectedValue)).ToString();
    }

    protected void DLAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetMony();
    }

    private void FGetMony()
    {
        ClassSaddam.FGetMony(lblReceipt, lblCashing, DLAccount, ddlYears, HFSumReceipt,
             lblSumReceipt, HFSumCashing, lblSumCashing, lblSumTotal, lblSumWordReceipt, lblSumWordCashing);
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "Bank")
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

}