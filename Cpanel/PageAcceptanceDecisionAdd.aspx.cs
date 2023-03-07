using Library_CLS_Arn.ClassEntity.Attach.Repostry;
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

public partial class Cpanel_PageAcceptanceDecisionAdd : System.Web.UI.Page
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
            bool A88;
            A88 = Convert.ToBoolean(dtViewProfil.Rows[0]["A88"]);
            if (A88 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetName();
            txtNumberMostafeed.Focus();
            FGetLastRecord();
            txtDateQarar.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            txtDateReport.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            pnlGetNull.Visible = true;
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetNaeebMaglesAlEdarah(DLNaeebMaglesAlEdarah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlsondoq);
            ClassAdmin_Arn.FGetAmeenGeneral(DLAmeenGeneral);
            ClassAdmin_Arn.FGetAdminInEdarah(DLAdminInEdarahFive);
            ClassAdmin_Arn.FGetAdminInEdarah(DLAdminInEdarahSix);
            ClassAdmin_Arn.FGetAdminInEdarah(DLAdminInEdarahSiven);
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where IsQobol = @0 And IsEstepaad = @1 And IsDelete = @2 Order by NumberQarar Desc", Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtNumberQarar.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberQarar"]) + 1);
        FGetLastRecord2();
    }

    private void FGetLastRecord2()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where IsQobol = @0 And IsEstepaad = @1 And IsDelete = @2 Order by NumberReport Desc", Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtNumberReport.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberReport"]) + 1);
    }

    private void FGetName()
    {
        DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT [IDItem],[NumberMostafeed],[NameMostafeed] FROM [dbo].[RasAlEstemarah] Where TypeMostafeed = @0 And IsDelete = @1 Order By NameMostafeed", "مستبعد", Convert.ToString(false));
        dt = ClassDataAccess.GetData("SELECT Top(1000) [IDItem],[NumberMostafeed],[NameMostafeed] FROM [dbo].[RasAlEstemarah] With(NoLock) Where IsDelete = @0 Order By NameMostafeed", Convert.ToString(false));
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

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And TypeMostafeed = @1 And IsDelete = @2", txtNumberMostafeed.Text.Trim(), "مستبعد", Convert.ToString(false));
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
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

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDay.Text = "لم يُضاف";
                lblAge.Text = "لم يُضاف";
            }
            pnlData.Visible = true;
            pnlGetNull.Visible = false;
            pnlGetData.Visible = true;
            Label1.Text = "بيانات المستفيد";
        }
        else
        {
            Label1.Text = "يبدو ان هذا المستفيد ليس موجود";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        FGetDataMostafed();
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetDataMostafedByName();
    }

    private void FGetDataMostafedByName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
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

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDay.Text = "لم يُضاف";
                lblAge.Text = "لم يُضاف";
            }
            pnlData.Visible = true;
            pnlGetNull.Visible = false;
            pnlGetData.Visible = true;
        }
        else
        {
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckNumberQarar();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNumberQarar()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where NumberQarar = @0 And IsQobol = @1 And IsEstepaad = @2 And IsDelete = @3", txtNumberQarar.Text.Trim(), Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "رقم القرار مستخدم لشخص آخر قم بتغييره";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
            FCheckNumberReport();
    }

    private void FCheckNumberReport()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where NumberReport = @0 And IsQobol = @1 And IsEstepaad = @2 And IsDelete = @3", txtNumberReport.Text.Trim(), Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "رقم التقرير مستخدم لشخص آخر قم بتغييره";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
            FCheckAdd();
    }

    private void FCheckAdd()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where NumberMostafeed = @0 And NumberReport = @1 And NumberQarar = @2 And IsQobol = @3 And IsEstepaad = @4 And IsDelete = @5", txtNumberMostafeed.Text.Trim(), txtNumberReport.Text.Trim(), txtNumberQarar.Text.Trim(), Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "تم إضافة هذا القرار مسبقاً";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
            FArnQararQobolMustafeedAdd();
    }

    private void FArnQararQobolMustafeedAdd()
    {
        GetCookie();
        ClassQararQobol CQQ = new ClassQararQobol()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            _DateQarar = Convert.ToDateTime(txtDateQarar.Text.Trim()).ToString("yyyy/MM/dd"),
            _DateReport = Convert.ToDateTime(txtDateReport.Text.Trim()).ToString("yyyy/MM/dd"),
            _NumberReport = Convert.ToInt32(txtNumberReport.Text.Trim()),
            _NumberQarar = Convert.ToInt32(txtNumberQarar.Text.Trim()),
            _DateAddQara = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = "0",
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsQobol = true,
            _IsEstepaad = false,
            _IsDelete = false
        };
        CQQ.BArnQararQobolMustafeedAdd();

        bool XCheck;
        XCheck = Convert.ToBoolean(CBAllow.Checked);

        FAdd(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue), lblOrderOne.Text, lblCommentOne.Text);
        FAdd(Convert.ToInt32(DLNaeebMaglesAlEdarah.SelectedValue), lblOrderTow.Text, lblCommentTow.Text);
        FAdd(Convert.ToInt32(DLAmeenAlsondoq.SelectedValue), lblOrderThree.Text, lblCommentThree.Text);
        FAdd(Convert.ToInt32(DLAmeenGeneral.SelectedValue), lblOrderFour.Text, lblCommentFour.Text);
        FAdd(Convert.ToInt32(DLAdminInEdarahFive.SelectedValue), lblOrderFive.Text, lblCommentFive.Text);
        FAdd(Convert.ToInt32(DLAdminInEdarahSix.SelectedValue), lblOrderSix.Text, lblCommentSix.Text);
        FAdd(Convert.ToInt32(DLAdminInEdarahSiven.SelectedValue), lblOrderSiven.Text, lblCommentSiven.Text);

        Label1.Text = "تم الإضافة بنجاح ";
        Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
        System.Threading.Thread.Sleep(100);
        if (Attach_Repostry_SMS_Send_.AllSendSystemSocialSearch() && XCheck == false)
            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة قرار إستبعاد" + "\n" + "رقم الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
    }

    private void FAdd(int XIDAdmin, string XOrder, string XComment)
    {
        bool XCheck = Convert.ToBoolean(CBAllow.Checked);
        ClassQararQobolAdmin CQQA = new ClassQararQobolAdmin()
        {
            _NumberMostafeed = Convert.ToInt64(txtNumberMostafeed.Text.Trim()),
            _NumberReport = Convert.ToInt64(txtNumberReport.Text.Trim()),
            _NumberQarar = Convert.ToInt64(txtNumberQarar.Text.Trim()),
            _IDAdmin = XIDAdmin,
            _AdminAllow = XCheck,
            _A1 = XOrder,
            _A2 = XComment,
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CQQA.BArnQararQobolMustafeedAdminAdd();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAcceptanceDecisionAdd.aspx");
    }

}