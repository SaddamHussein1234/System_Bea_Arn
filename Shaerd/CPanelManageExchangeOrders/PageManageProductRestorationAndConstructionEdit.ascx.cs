using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_CPanelManageExchangeOrders_PageManageProductRestorationAndConstructionEdit : System.Web.UI.UserControl
{
    string XID;
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A106;
            A106 = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            if (A106 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlStar.Visible = true;
            FGetName();
            txtNumberMostafeed.Focus();
            FGetAmeenAlmostodaa();
            //txtProductionDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            pnlDataMosTafeed.Visible = false;
            pnlMostafeed.Visible = true;
            pnlAlDaam.Visible = false;
            txtNumberMostafeed.Focus();
            ClassQuaem.FGetSupportType(1, "'4'", DLSupportType);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[BenaaAndTarmim] With(NoLock) Where IDUniq = @0 and IsDelete = @1", Convert.ToString(Request.QueryString["IDX"]), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                RBTarmimCheck.Checked = Convert.ToBoolean(dt.Rows[0]["IsTarmem"]);
                RBBenaCheck.Checked = Convert.ToBoolean(dt.Rows[0]["IsBena"]);

                DataTable dtTar = new DataTable();
                if (RBTarmimCheck.Checked)
                {
                    dtTar = ClassDataAccess.GetData("SELECT * FROM [dbo].[BenaaAndTarmim] With(NoLock) Where IsTarmem = @0 And IsDelete = @1 Order by billNumber_ Desc", Convert.ToString(true), Convert.ToString(false));
                    if (dtTar.Rows.Count > 0)
                    {
                        txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dtTar.Rows[0]["billNumber_"]) + 1);
                        DLSupportType.SelectedValue = "11"; txtNumberMostafeed.Focus();
                        pnlStar.Visible = false;
                        txtNumberMostafeed.Enabled = true;
                        DLName.Enabled = true;
                        DLInitiatives.Enabled = true;
                        txtNumberOrder.Enabled = true;
                    }
                    else
                    {
                        txtNumberOrder.Text = ClassSaddam.FGetNumberBillStart().ToString();
                        DLSupportType.SelectedValue = "11"; txtNumberMostafeed.Focus();
                        pnlStar.Visible = false;
                        txtNumberMostafeed.Enabled = true;
                        DLName.Enabled = true;
                        DLInitiatives.Enabled = true;
                        txtNumberOrder.Enabled = true;
                    }
                }
                else
                {
                    dtTar = ClassDataAccess.GetData("SELECT * FROM [dbo].[BenaaAndTarmim] With(NoLock) Where IsBena = @0 And IsDelete = @1 Order by billNumber_ Desc", Convert.ToString(true), Convert.ToString(false));
                    if (dtTar.Rows.Count > 0)
                    {
                        txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dtTar.Rows[0]["billNumber_"]) + 1);
                        DLSupportType.SelectedValue = "10"; txtNumberMostafeed.Focus();
                        pnlStar.Visible = false;
                        txtNumberMostafeed.Enabled = true;
                        DLName.Enabled = true;
                        DLInitiatives.Enabled = true;
                        txtNumberOrder.Enabled = true;
                    }
                    else
                    {
                        txtNumberOrder.Text = ClassSaddam.FGetNumberBillStart().ToString();
                        DLSupportType.SelectedValue = "10"; txtNumberMostafeed.Focus();
                        pnlStar.Visible = false;
                        txtNumberMostafeed.Enabled = true;
                        DLName.Enabled = true;
                        DLInitiatives.Enabled = true;
                        txtNumberOrder.Enabled = true;
                    }
                }
                Session["OldNumberMostafeed"] = dt.Rows[0]["NumberMostafeed"].ToString();
                txtNumberMostafeed.Text = Session["OldNumberMostafeed"].ToString();
                DLName.SelectedValue = txtNumberMostafeed.Text;
                Session["OldNumberBill"] = dt.Rows[0]["billNumber_"].ToString();
                txtNumberOrder.Text = Session["OldNumberBill"].ToString();
                pnlStar.Visible = false;
                txtNumberMostafeed.Enabled = true;
                DLName.Enabled = true;
                DLInitiatives.Enabled = true;
                txtNumberOrder.Enabled = true;
                FGetDataMostafed();
                txtThe_Mony.Text = dt.Rows[0]["The_Mony"].ToString();
                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(txtThe_Mony.Text), currencies[Convert.ToInt32(0)]);
                txtThe_Mony_Word.Text = toWord.ConvertToArabic();
                RBIsCash_Money.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                RBIsShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                txtNumber_Shayk_Bank.Text = dt.Rows[0]["Number_Shayk_Bank"].ToString();
                if (RBIsShayk_Bank.Checked)
                {
                    NumberShayk.Visible = true;
                    txtNumber_Shayk_Bank.Focus();
                }
                else
                    NumberShayk.Visible = false;
                txtProductionDate.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy-MM-dd");
                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
                DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["IDAmeenAlsondoq"].ToString();
                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["IDRaeesAlMagles"].ToString();
                DLSupportType.SelectedValue = dt.Rows[0]["ID_Type"].ToString();
                txt_Add.Text = Convert.ToDateTime(dt.Rows[0]["Date_Add_Report"]).ToString("yyyy-MM-dd");
                txtMoreDetails.Text = dt.Rows[0]["More_Details"].ToString();
                DLInitiatives.SelectedValue = dt.Rows[0]["A1"].ToString();
                txt_Note.Text = dt.Rows[0]["A2"].ToString();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [IDItem],[NumberMostafeed],[NameMostafeed] FROM [dbo].[RasAlEstemarah] Where TypeMostafeed = @0 And IsDelete = @1 Order By NameMostafeed", "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.Items.Clear();
            DLName.Items.Add("");
            DLName.AppendDataBoundItems = true;
            DLName.DataValueField = "NumberMostafeed";
            DLName.DataTextField = "NameMostafeed";
            DLName.DataSource = dt;
            DLName.DataBind();
        }
    }

    private void FGetAmeenAlmostodaa()
    {
        ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
        ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLAmeenAlSondoq);
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

    protected void RBBenaCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[BenaaAndTarmim] With(NoLock) Where IsBena = @0 And IsDelete = @1 Order by billNumber_ Desc", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["billNumber_"]) + 1);
            DLSupportType.SelectedValue = "10"; txtNumberMostafeed.Focus();
            pnlStar.Visible = false;
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberOrder.Enabled = true;
        }
        else
        {
            txtNumberOrder.Text = ClassSaddam.FGetNumberBillStart().ToString();
            DLSupportType.SelectedValue = "10"; txtNumberMostafeed.Focus();
            pnlStar.Visible = false;
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberOrder.Enabled = true;
        }
    }

    protected void RBTarmimCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[BenaaAndTarmim] With(NoLock) Where IsTarmem = @0 And IsDelete = @1 Order by billNumber_ Desc", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["billNumber_"]) + 1);
            DLSupportType.SelectedValue = "11"; txtNumberMostafeed.Focus();
            pnlStar.Visible = false;
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberOrder.Enabled = true;
        }
        else
        {
            txtNumberOrder.Text = ClassSaddam.FGetNumberBillStart().ToString();
            DLSupportType.SelectedValue = "11"; txtNumberMostafeed.Focus();
            pnlStar.Visible = false;
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberOrder.Enabled = true;
        }
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        FGetDataMostafed();
    }

    private void FGetDataMostafed()
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
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
            lblMessageWarning.Text = "يبدو ان هذا المستفيد مستبعد";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
        }
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetDataMostafedByName();
    }

    private void FGetDataMostafedByName()
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
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
        if (RBIsCash_Money.Checked)
        {
            NumberShayk.Visible = false;
            txtNumber_Shayk_Bank.Text = string.Empty;
        }
    }

    protected void RBIsShayk_Bank_CheckedChanged(object sender, EventArgs e)
    {
        if (RBIsShayk_Bank.Checked)
        {
            NumberShayk.Visible = true;
            txtNumber_Shayk_Bank.Focus();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            System.Threading.Thread.Sleep(100);
            FCheckNumberOrder();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNumberOrder()
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (txtNumberOrder.Text.Trim() != Session["OldNumberBill"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select top(10) * from BenaaAndTarmim with(NoLock) Where billNumber_ = @0 And ID_Type = @1 And IsDelete = @2", txtNumberOrder.Text.Trim(), DLSupportType.SelectedValue, Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblMessageWarning.Text = "لا يمكن تكرار رقم الامر قم بتغييره";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
            }
            else
            {
                if (txtNumberMostafeed.Text.Trim() == Session["OldNumberMostafeed"].ToString())
                    FArnProductShopWarehouseAddForMostafeed();
                else
                {
                    if (DLSupportType.SelectedItem.ToString() == "بناء منازل")
                    {
                        DataTable dtRequestOfThrBeneficiary = new DataTable();
                        dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And BenaManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
                        if (dtRequestOfThrBeneficiary.Rows.Count > 0)
                        {
                            FArnProductShopWarehouseAddForMostafeed();
                        }
                        else
                        {
                            lblMessageWarning.Text = "المستفيد لا يحتاج إلى بناء منزل";
                            IDMessageSuccess.Visible = false;
                            IDMessageWarning.Visible = true;
                        }
                    }
                    else if (DLSupportType.SelectedItem.ToString() == "ترميم منازل")
                    {
                        DataTable dtRequestOfThrBeneficiary = new DataTable();
                        dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And TarmemManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
                        if (dtRequestOfThrBeneficiary.Rows.Count > 0)
                        {
                            FArnProductShopWarehouseAddForMostafeed();
                        }
                        else
                        {
                            lbmsg.Text = "المستفيد لا يحتاج إلى ترميم منزل";
                            lbmsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
        else
        {
            if (txtNumberMostafeed.Text.Trim() == Session["OldNumberMostafeed"].ToString())
                FArnProductShopWarehouseAddForMostafeed();
            else
            {
                if (DLSupportType.SelectedItem.ToString() == "بناء منازل")
                {
                    DataTable dtRequestOfThrBeneficiary = new DataTable();
                    dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And BenaManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
                    if (dtRequestOfThrBeneficiary.Rows.Count > 0)
                        FArnProductShopWarehouseAddForMostafeed();
                    else
                    {
                        lbmsg.Text = "المستفيد لا يحتاج إلى بناء منزل";
                        lbmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (DLSupportType.SelectedItem.ToString() == "ترميم منازل")
                {
                    DataTable dtRequestOfThrBeneficiary = new DataTable();
                    dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And TarmemManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
                    if (dtRequestOfThrBeneficiary.Rows.Count > 0)
                        FArnProductShopWarehouseAddForMostafeed();
                    else
                    {
                        lblMessageWarning.Text = "المستفيد لا يحتاج إلى ترميم منزل";
                        IDMessageSuccess.Visible = false;
                        IDMessageWarning.Visible = true;
                    }
                }
            }
        }

    }

    private void FArnProductShopWarehouseAddForMostafeed()
    {
        if (btnEdit.Text == "تعديل البانات")
        {
            GetCookie();
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            ClassBenaaAndTarmim CBAT = new ClassBenaaAndTarmim();
            CBAT._IDUniq = Convert.ToString(Request.QueryString["IDX"]);
            CBAT._NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
            CBAT._billNumber_ = Convert.ToInt32(txtNumberOrder.Text.Trim());
            CBAT._The_Mony = Convert.ToInt32(txtThe_Mony.Text.Trim());
            CBAT._IsCash_Money_ = Convert.ToBoolean(RBIsCash_Money.Checked);
            CBAT._IsShayk_Bank = Convert.ToBoolean(RBIsShayk_Bank.Checked);
            if (txtNumber_Shayk_Bank.Text != string.Empty)
                CBAT._Number_Shayk_Bank = Convert.ToInt32(txtNumber_Shayk_Bank.Text.Trim());
            else
                CBAT._Number_Shayk_Bank = Convert.ToInt32(0);

            CBAT.__Date_Get = txtProductionDate.Text.Trim();
            CBAT._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
            CBAT._IsAllowModer = false;
            CBAT._IDAmeenAlsondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue);
            CBAT._AllowState = false;
            CBAT._AllowStateDetalis = string.Empty;
            CBAT._NotAllowState = false;
            CBAT._WhayNotAllow = string.Empty;
            CBAT._Date_AllowOrNotAllow = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
            CBAT._IDRaeesAlMagles = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue);
            CBAT._IsAllowRaeesAlMagles = false;
            CBAT._IDAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
            CBAT._IDAdmin = Convert.ToInt32(IDUser);
            CBAT._Date_Add_Report = txt_Add.Text.Trim();
            CBAT._ID_Type = Convert.ToInt64(DLSupportType.SelectedValue);
            CBAT._More_Details = txtMoreDetails.Text.Trim();
            if (DLSupportType.SelectedItem.ToString() == "بناء منازل")
            {
                CBAT._IsTarmem = false;
                CBAT._IsBena = true;
            }
            else if (DLSupportType.SelectedItem.ToString() == "ترميم منازل")
            {
                CBAT._IsTarmem = true;
                CBAT._IsBena = false;
            }
            CBAT._A1_ = Convert.ToInt32(DLInitiatives.SelectedValue);
            CBAT._A2 = txt_Note.Text.Trim();
            CBAT._A3 = "0";
            CBAT._A4 = "0";
            CBAT._A5 = "0";
            CBAT.BArnBenaaAndTarmimEdit();

            if (DLSupportType.SelectedItem.ToString() == "بناء منازل")
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
            else if (DLSupportType.SelectedItem.ToString() == "ترميم منازل")
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
            lblMessage.Text = "تم تعديل أمر الصرف بنجاح";
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
        }
    }

}