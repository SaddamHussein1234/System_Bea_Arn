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

public partial class Shaerd_CPanelManageExchangeOrders_PageSupportForPrismsAdd : System.Web.UI.UserControl
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
            bool IsBaheth, A106;
            IsBaheth = Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]);
            A106 = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            if (A106 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (IsBaheth == false)
                FGetName();
            else if (IsBaheth)
                FGetNameByBaheth();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlStar.Visible = true;
            txtNumberMostafeed.Focus();
            ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
            txtProductionDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            txt_Add.Text = txtProductionDate.Text;
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            pnlDataMosTafeed.Visible = false;
            pnlMostafeed.Visible = true;
            pnlStar.Visible = false;
            pnlAlDaam.Visible = false;
            txtNumberMostafeed.Focus();
            ClassQuaem.FGetSupportType(1, "'5'", DLSupportType);
            pnlStarView.Visible = true;
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

    private void FGetNameByBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And RasAlEstemarah.IsDelete = @1 And tbl_MultiQariah.IsDelete = @1 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, Convert.ToString(false));
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductRestorationAndConstruction.aspx");
    }

    protected void DLSupportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            DLInitiatives.Enabled = true;
            txtNumberOrder.Enabled = true;
            txtNumberMostafeed.Focus();
            lblStar.Text = "يرجى تحديد بيانات المستفيد";
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
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool IsBaheth;
            IsBaheth = Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]);
            if (IsBaheth)
                FGetDataMostafedByBaheth();
            else if (IsBaheth == false)
                FGetDataMostafed();
        }
    }

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(10000) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
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
            txtThe_Mony.Focus();
            pnlStarView.Visible = false;
            Label1.Text = " بيانات المستفيد";
            Label1.ForeColor = System.Drawing.Color.Black;
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

    private void FGetDataMostafedByBaheth()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And NumberMostafeed = @1 And RasAlEstemarah.IsDelete = @2 And tbl_MultiQariah.IsDelete = @2 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
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
            Label1.Text = " بيانات المستفيد";
            Label1.ForeColor = System.Drawing.Color.Black;
            txtThe_Mony.Focus();
            pnlStarView.Visible = false;
        }
        else
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يبدو ان هذا المستفيد ليس موجود , أو خارج نطاق صلاحيتك";
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
            pnlStarView.Visible = true;
        }
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetDataMostafedByName();
    }

    private void FGetDataMostafedByName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(10000) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (RBIsCash_Money.Checked || RBIsShayk_Bank.Checked || RBIsConvert_Bank.Checked)
            {
                if (btnAdd.Text == "إضافة للفاتورة")
                {
                    System.Threading.Thread.Sleep(100);
                    FCheckNumberOrder();
                }
            }
            else
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text =  " يرجى تحديد نوع الدفع ";
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

    private void FCheckNumberOrder()
    {
        lbmsg.Text = "إنشاء أمر صرف نقدي لمستفيد";
        lbmsg.ForeColor = System.Drawing.Color.Black;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1000) * from tbl_SupportForPrisms With(NoLock) Where billNumber_ = @0 And ID_Type = @1 And IsDelete = @2", txtNumberOrder.Text.Trim(), DLSupportType.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblMessageWarning.Text = "تم إضافة أمر الصرف بنجاح";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
        }
        else
            FArnSupportForPrismsAdd();
    }

    private void FArnSupportForPrismsAdd()
    {
        if (btnAdd.Text == "إضافة للفاتورة")
        {
            GetCookie();
            ClassSupportForPrisms CBAT = new ClassSupportForPrisms();
            CBAT._IDUniq = Convert.ToString(Guid.NewGuid());
            CBAT._NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
            CBAT._billNumber_ = Convert.ToInt32(txtNumberOrder.Text.Trim());
            CBAT.The_Mony_ = Convert.ToDecimal(txtThe_Mony.Text.Trim());
            CBAT._IsCash_Money_ = Convert.ToBoolean(RBIsCash_Money.Checked);
            CBAT._IsShayk_Bank = Convert.ToBoolean(RBIsShayk_Bank.Checked);
            if (txtNumber_Shayk_Bank.Text != string.Empty)
                CBAT._Number_Shayk_Bank = Convert.ToInt32(txtNumber_Shayk_Bank.Text.Trim());
            else
                CBAT._Number_Shayk_Bank = Convert.ToInt32(0);

            CBAT.__Date_Get = txt_Add.Text.Trim();
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
            CBAT._A1 = Convert.ToString(RBIsConvert_Bank.Checked);
            CBAT._A2_ = Convert.ToInt32(DLInitiatives.SelectedValue);
            CBAT._A3 = txt_Note.Text.Trim();
            CBAT._A4 = "0";
            CBAT._A5 = "0";
            CBAT._IsDelete = false;
            CBAT.BArnSupportForPrismsAdd();
            lblMessage.Text = "تم إضافة أمر الصرف بنجاح";
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(txtNumberOrder.Text.Trim()) + 1);
        }
    }
    
    protected void RBIsConvert_Bank_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (RBIsConvert_Bank.Checked)
        {
            NumberShayk.Visible = false;
            txtNumber_Shayk_Bank.Text = string.Empty;
        }
    }

}