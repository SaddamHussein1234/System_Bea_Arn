using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_CPanelManageExchangeOrders_PageSupportForPrismsEdit : System.Web.UI.UserControl
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
            ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            pnlDataMosTafeed.Visible = false;
            pnlMostafeed.Visible = true;
            pnlStar.Visible = false;
            pnlAlDaam.Visible = false;
            txtNumberMostafeed.Focus();
            ClassQuaem.FGetSupportType(1, "'5'", DLSupportType);
            pnlStarView.Visible = true;
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[tbl_SupportForPrisms] With(NoLock) Where IDUniq = @0 and IsDelete = @1", Convert.ToString(Request.QueryString["IDX"]), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                txtNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                DLName.SelectedValue = txtNumberMostafeed.Text;
                Session["OldNumberBillPrisms"] = dt.Rows[0]["billNumber_"].ToString();
                txtNumberOrder.Text = Session["OldNumberBillPrisms"].ToString();
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
                RBIsConvert_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["A1"]);
                DLInitiatives.SelectedValue= dt.Rows[0]["A2"].ToString();
                txt_Note.Text = dt.Rows[0]["A3"].ToString();
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FGetName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000)  [IDItem],[NumberMostafeed],[NameMostafeed] FROM [dbo].[RasAlEstemarah] With(NoLock) Where IsDelete = @0 Order By NameMostafeed", Convert.ToString(false));
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

    protected void DLSupportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberOrder.Enabled = true;
            txtNumberMostafeed.Focus();
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(10) [billNumber_] FROM [dbo].[tbl_SupportForPrisms] With(NoLock) Where ID_Type = @0 And IsDelete = @1 Order by billNumber_ Desc", DLSupportType.SelectedValue, Convert.ToString(false));
            if (dt.Rows.Count > 0)
                txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["billNumber_"]) + 1);
            else
                txtNumberOrder.Text = ClassSaddam.FGetNumberBillStart().ToString();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
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
            Label1.Text = "بيانات المستفيد";
            //if (GVMatterOfExchangeByID.Rows.Count > 0)
            //{
            //    FArnProductShopMatterOfExchangeByUser();
            //}
            //else
            //{
            //    Response.Redirect("PageManageProductMatterOfExchange.aspx");
            //}
            txtThe_Mony.Focus();
            pnlStarView.Visible = false;
        }
        else
        {
            Label1.Text = "يبدو ان هذا المستفيد مستبعد";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
            pnlStarView.Visible = true;
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
        dt = ClassDataAccess.GetData("SELECT Top(10) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
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
            pnlStarView.Visible = false;
        }
        else
        {
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
            pnlStarView.Visible = true;
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
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void RBIsCash_Money_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (RBIsCash_Money.Checked)
        {
            NumberShayk.Visible = false;
            txtNumber_Shayk_Bank.Text = string.Empty;
        }
    }

    protected void RBIsShayk_Bank_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (RBIsShayk_Bank.Checked)
        {
            NumberShayk.Visible = true;
            txtNumber_Shayk_Bank.Focus();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            System.Threading.Thread.Sleep(100);
            FCheckNumberOrder();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FCheckNumberOrder()
    {
        if (txtNumberOrder.Text.Trim() != Session["OldNumberBillPrisms"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select top(10) * from tbl_SupportForPrisms with(NoLock) Where billNumber_ = @0 And ID_Type = @1 And IsDelete = @2", txtNumberOrder.Text.Trim(), DLSupportType.SelectedValue, Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "لا يمكن تكرار رقم الامر قم بتغييره";
                return;
            }
            else
                FArnSupportForPrismsEdit();
        }
        else
            FArnSupportForPrismsEdit();
    }

    private void FArnSupportForPrismsEdit()
    {
        if (btnEdit.Text == "تعديل البانات")
        {
            GetCookie();
            ClassSupportForPrisms CSFP = new ClassSupportForPrisms();
            CSFP._IDUniq = Convert.ToString(Request.QueryString["IDX"]);
            CSFP._NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
            CSFP._billNumber_ = Convert.ToInt32(txtNumberOrder.Text.Trim());
            CSFP.The_Mony_ = Convert.ToDecimal(txtThe_Mony.Text.Trim());
            CSFP._IsCash_Money_ = Convert.ToBoolean(RBIsCash_Money.Checked);
            CSFP._IsShayk_Bank = Convert.ToBoolean(RBIsShayk_Bank.Checked);
            if (txtNumber_Shayk_Bank.Text != string.Empty)
                CSFP._Number_Shayk_Bank = Convert.ToInt32(txtNumber_Shayk_Bank.Text.Trim());
            else
                CSFP._Number_Shayk_Bank = Convert.ToInt32(0);

            CSFP.__Date_Get = txtProductionDate.Text.Trim();
            CSFP._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
            CSFP._IsAllowModer = false;
            CSFP._IDAmeenAlsondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue);
            CSFP._AllowState = false;
            CSFP._AllowStateDetalis = string.Empty;
            CSFP._NotAllowState = false;
            CSFP._WhayNotAllow = string.Empty;
            CSFP._Date_AllowOrNotAllow = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
            CSFP._IDRaeesAlMagles = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue);
            CSFP._IsAllowRaeesAlMagles = false;
            CSFP._IDAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
            CSFP._IDAdmin = Convert.ToInt32(IDUser);
            CSFP._Date_Add_Report = txt_Add.Text.Trim();
            CSFP._ID_Type = Convert.ToInt64(DLSupportType.SelectedValue);
            CSFP._More_Details = txtMoreDetails.Text.Trim();
            CSFP._A1 = Convert.ToString(RBIsConvert_Bank.Checked);
            CSFP._A2_ = Convert.ToInt32(DLInitiatives.SelectedValue);
            CSFP._A3 = txt_Note.Text.Trim();
            CSFP._A4 = "0";
            CSFP._A5 = "0";
            CSFP.BArnSupportForPrismsEdit();
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            lblMessage.Text  = "تم تعديل أمر الصرف بنجاح";
            FGetData();
        }
    }
    
    protected void RBIsConvert_Bank_CheckedChanged(object sender, EventArgs e)
    {
        if (RBIsConvert_Bank.Checked)
        {
            NumberShayk.Visible = false;
            txtNumber_Shayk_Bank.Text = string.Empty;
        }
    }

}