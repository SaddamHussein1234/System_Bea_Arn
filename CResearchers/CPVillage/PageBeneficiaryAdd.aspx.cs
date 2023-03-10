using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_CPVillage_PageBeneficiaryAdd : System.Web.UI.Page
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
            Response.Redirect("LogOut.aspx");
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
            DLAlBaheth.SelectedValue = IDUser;
            bool A75;
            A75 = Convert.ToBoolean(dtViewProfil.Rows[0]["A75"]);
            if (A75 == false)
            {
                Response.Redirect("LogOut.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            LBAddBoys.Enabled = false;
            txtNumberMostafeed.Focus();
            txtDateRegistry.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            FGetLastRecord();

            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")); i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 100; i--)
            {
                dtYear.Rows.Add(i);
            }

            ddlYears.Items.Clear();
            ddlYears.Items.Add("");
            ddlYears.AppendDataBoundItems = true;
            ddlYears.DataTextField = "Year";
            ddlYears.DataValueField = "Year";
            ddlYears.DataSource = dtYear;
            ddlYears.DataBind();

            DataTable dtYearH = new DataTable();
            dtYearH.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 700; i <= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 579; i++)
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

            //DataTable dtDate = new DataTable();
            //dtDate.Columns.Add("Date", typeof(int));
            //for (int i = 1; i <= 31; i++)
            //{
            //    dtDate.Rows.Add(i);
            //}

            //ddlDates.Items.Clear();
            //ddlDates.Items.Add("");
            //ddlDates.AppendDataBoundItems = true;
            //ddlDates.DataTextField = "Date";
            //ddlDates.DataValueField = "Date";
            //ddlDates.DataSource = dtDate;
            //ddlDates.DataBind();

            Session["XIDRandom"] = Convert.ToInt32(ClassDataAccess.RandomGenerator().ToString().Replace("-", ""));
            FGetImgMosTafeed();

            HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
            string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";
            DLAlQriah.SelectedValue = IDVillage;
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by NumberMostafeed Desc", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberMostafeed.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberMostafeed"]) + 1);
        }
        FGetAlQariah();
    }

    private void FGetAlQariah()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQriah.Items.Clear();
            DLAlQriah.Items.Add("");
            DLAlQriah.AppendDataBoundItems = true;
            DLAlQriah.DataValueField = "IDQariah";
            DLAlQriah.DataTextField = "AlQriah";
            DLAlQriah.DataSource = dt;
            DLAlQriah.DataBind();
        }
        FGetGender();
    }

    private void FGetGender()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where Gender <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLGender.Items.Clear();
            DLGender.Items.Add("");
            DLGender.AppendDataBoundItems = true;
            DLGender.DataValueField = "IDItem";
            DLGender.DataTextField = "Gender";
            DLGender.DataSource = dt;
            DLGender.DataBind();
        }
        FGetHalafAlMosTafeed();
    }

    private void FGetHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDeleteHalatMostafeed = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLHalafAlMosTafeed.Items.Clear();
            DLHalafAlMosTafeed.Items.Add("");
            DLHalafAlMosTafeed.AppendDataBoundItems = true;
            DLHalafAlMosTafeed.DataValueField = "IDItem";
            DLHalafAlMosTafeed.DataTextField = "HalatMostafeed";
            DLHalafAlMosTafeed.DataSource = dt;
            DLHalafAlMosTafeed.DataBind();
        }
        FGetMasderAlDakhl();
    }

    private void FGetMasderAlDakhl()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where AlDakhlAlShahryWaMasdarah <> @0 And IsDeleteAlDakhlAlShahryWaMasdarah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLMasderAlDakhl.Items.Clear();
            DLMasderAlDakhl.Items.Add("");
            DLMasderAlDakhl.AppendDataBoundItems = true;
            DLMasderAlDakhl.DataValueField = "IDItem";
            DLMasderAlDakhl.DataTextField = "AlDakhlAlShahryWaMasdarah";
            DLMasderAlDakhl.DataSource = dt;
            DLMasderAlDakhl.DataBind();
        }
        FGetTypeAlMasken();
    }

    private void FGetTypeAlMasken()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where TypeAlMaskan <> @0 And IsDeleteTypeAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLTypeAlMasken.Items.Clear();
            DLTypeAlMasken.Items.Add("");
            DLTypeAlMasken.AppendDataBoundItems = true;
            DLTypeAlMasken.DataValueField = "IDItem";
            DLTypeAlMasken.DataTextField = "TypeAlMaskan";
            DLTypeAlMasken.DataSource = dt;
            DLTypeAlMasken.DataBind();
        }
        FGetHaletAlMasken();
    }

    private void FGetHaletAlMasken()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where HalatAlMaskan <> @0 And IsDeleteHalatAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLHaletAlMasken.Items.Clear();
            DLHaletAlMasken.Items.Add("");
            DLHaletAlMasken.AppendDataBoundItems = true;
            DLHaletAlMasken.DataValueField = "IDItem";
            DLHaletAlMasken.DataTextField = "HalatAlMaskan";
            DLHaletAlMasken.DataSource = dt;
            DLHaletAlMasken.DataBind();
        }
        FGetSelatAlQarabah();
    }

    private void FGetSelatAlQarabah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where SelatAlQarabah <> @0 And IsDeleteSelatAlQarabah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLSelatAlQarabah.Items.Clear();
            DLSelatAlQarabah.Items.Add("");
            DLSelatAlQarabah.AppendDataBoundItems = true;
            DLSelatAlQarabah.DataValueField = "IDItem";
            DLSelatAlQarabah.DataTextField = "SelatAlQarabah";
            DLSelatAlQarabah.DataSource = dt;
            DLSelatAlQarabah.DataBind();
        }
        FGetAlBaheth();
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where ID_Item = @0 And IsBaheth = @1 And IsDelete = @2 Order by IsOrderAdminInEdarah ", IDUser, Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlBaheth.Items.Clear();
            DLAlBaheth.Items.Add("");
            DLAlBaheth.AppendDataBoundItems = true;
            DLAlBaheth.DataValueField = "ID_Item";
            DLAlBaheth.DataTextField = "FirstName";
            DLAlBaheth.DataSource = dt;
            DLAlBaheth.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
    }

    protected void DLHalafAlMosTafeed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DLHalafAlMosTafeed.SelectedItem.ToString() == "ايتام")
        {
            PnlMehnah.Visible = true;
        }
        else
        {
            PnlMehnah.Visible = false;
        }
    }

    protected void ddlDatesH_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string DateHijri;
            DateTime today = ClassDataAccess.GetCurrentTime();
            string year = ddlYears.SelectedValue;
            string month = ddlMonths.SelectedValue;
            string date = ddlDates.SelectedValue;
            DateTime dob = Convert.ToDateTime(date + "/" + month + "/" + year);

            DateHijri = Convert.ToDateTime(ClassSaddam.ConvertDateCalendar(Convert.ToDateTime(date + "/" + month + "/" + year), "Hijri", "en-US")).ToString("dd/MM/yyyy");
            ddlDatesH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("dd");
            ddlMonthsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("MM");
            ddlYearsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("yyyy");

            TimeSpan ts = today - dob;
            DateTime age = DateTime.MinValue + ts;
            int years = age.Year - 1;
            int months = age.Month - 1;
            int days = age.Day - 1;
            txtAge.Text = years.ToString() + " سنة " + " و " + months.ToString() + " شهر ";
            txtAlMehnahAlHaliyahllmostafeed.Focus();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNumberMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "رقم المستفيد مستخدم لشخص آخر قم بتغييره";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FCheckNameMosTafeed();
        }
    }

    private void FCheckNameMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NameMostafeed = @0 And IsDelete = @1", txtNameMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "إسم المستفيد مستخدم لشخص آخر قم بتغييره";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FCheckUserName();
        }
    }

    private void FCheckUserName()
    {
        DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtUserName.Text.Trim(), Convert.ToString(false));
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtNumberAlSegelAlMadany.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //lbmsg.Text = "إسم الدخول للنظام مستخدم لشخص آخر قم بتغييره";
            lbmsg.Text = "رقم السجل المدني مستخدم لشخص آخر قم بتغييره";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FCheckEmail();
        }
    }

    private void FCheckEmail()
    {
        DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where Email_User_ = @0 And IsDelete = @1", txtEmail.Text.Trim(), Convert.ToString(false));
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtNumberAlSegelAlMadany.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //lbmsg.Text = "البريد الالكتروني مستخدم بالفعل";
            lbmsg.Text = "رقم السجل المدني مستخدم لشخص آخر قم بتغييره";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FChackPass();
        }
    }

    private void FChackPass()
    {
        if (txtPassword.Text.Trim() == txtRePassword.Text.Trim())
        {

            FMostafeedAdd();
        }
        else
        {
            lbmsg.Text = "كلمات المرور التي ادخلتهما غير متطابقتان";
            lbmsg.ForeColor = Color.Red;
        }
    }

    //private void FMostafeedAdd()
    //{
    //    XID = Convert.ToString(Guid.NewGuid());
    //    ClassMosTafeed CMF = new ClassMosTafeed()
    //    {
    //        _IDUniq = XID,
    //        _NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
    //        _NameMostafeed = txtNameMostafeed.Text.Trim(),
    //        _AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue),
    //        _Gender = Convert.ToInt32(DLGender.SelectedValue),
    //        _PhoneNumber = Convert.ToInt64(txtCellPhoneOne.Text.Trim()),
    //        _NumberAlSegelAlMadany = Convert.ToInt64(txtNumberAlSegelAlMadany.Text.Trim()),
    //        _HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.Text.Trim()),
    //        _dateBrith = ddlDates.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlYears.SelectedValue,
    //        _Age = txtAge.Text.Trim(),
    //        _AlMehnahAlHaliyahllmostafeed = txtAlMehnahAlHaliyahllmostafeed.Text.Trim(),
    //        _AlHalahAlTaelimiahllmostafeed = DLAlHalahAlTaelimiahllmostafeed.SelectedValue,
    //        _Saleem = Convert.ToBoolean(CBSaleem.Checked),
    //        _Moalek = Convert.ToBoolean(CBMoalek.Checked),
    //        _TypeAleakah = txtTypeAleakah.Text.Trim(),
    //        _Mareedh = Convert.ToBoolean(CBMareedh.Checked),
    //        _TypeAlmaradh = txtTypeAlmaradh.Text.Trim(),
    //        _AlDakhlAlShahryllMostafeed = txtAlDakhlAlShahryllMostafeed.Text.Trim(),
    //        _MasderAlDakhl = Convert.ToInt32(DLMasderAlDakhl.SelectedValue),
    //        _TypeAlMasken = Convert.ToInt32(DLTypeAlMasken.SelectedValue),
    //        _HaletAlMasken = Convert.ToInt32(DLHaletAlMasken.SelectedValue),
    //        _EsmAlMostakhdem = "1",
    //        _dateEntry = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
    //        _HoursEntry = ClassDataAccess.GetCurrentTime().ToString("hh:mm:ss ttt"),
    //        _AlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue),
    //        _TypeMostafeed = DLTypeMostafeed.SelectedValue,
    //        _DateRegistry = txtDateRegistry.Text.Trim(),
    //        _MehnahAlAAbKablAlWafah = txtMehnahAlAAbKablAlWafah.Text.Trim(),
    //        _AfradAlOsrah = 0,
    //        _CellPhone2 = Convert.ToInt64(txtCellPhoneTow.Text.Trim()),
    //        _Phone1 = Convert.ToInt64(txtPhoneOne.Text.Trim()),
    //        _Phone2 = Convert.ToInt64(txtPhoneTow.Text.Trim()),
    //        _NumberAlQareb = Convert.ToInt64(txtNumberQareb.Text.Trim()),
    //        _NameAlQareb = txtNameQareb.Text.Trim(),
    //        _SelatAlQarabah = Convert.ToInt32(DLSelatAlQarabah.SelectedValue),
    //        _NoteMosTafed = txtNote.Text.Trim(),
    //        _User_Name_ = txtUserName.Text.Trim(),
    //        _Email_User_ = txtEmail.Text.Trim(),
    //        _Password_User_ = ClassEncryptPassword.Encrypt(txtPassword.Text.Trim(), "لا اله الا انت سبحانك اني كنت من الظالمين"),
    //        _IsActive = Convert.ToBoolean(CBActive.Checked),
    //        _IsDelete = false,
    //        _A1 = "0",
    //        _A2 = "0",
    //        _A3 = "0",
    //        _A4 = "0",
    //        _A5 = "0"
    //    };
    //    CMF.BArnRasAlEstemarahAdd();
    //    lbmsg.Text = "تم الإضافة بنجاح ";
    //    lbmsg.ForeColor = System.Drawing.Color.Red;
    //    LBAddBoys.Enabled = true;
    //}

    private void FMostafeedAdd()
    {
        GetCookie();
        XID = Convert.ToString(Guid.NewGuid());
        ClassMosTafeed CMF = new ClassMosTafeed()
        {
            _IDUniq = XID,
            _NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            _NameMostafeed = txtNameMostafeed.Text.Trim(),
            _AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue),
            _Gender = Convert.ToInt32(DLGender.SelectedValue),
            _PhoneNumber = Convert.ToInt64(txtCellPhoneOne.Text.Trim()),
            _NumberAlSegelAlMadany = Convert.ToInt64(txtNumberAlSegelAlMadany.Text.Trim()),
            _HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.Text.Trim()),
            _dateBrith = ddlYears.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlDates.SelectedValue,
            _Age = txtAge.Text.Trim(),
            _AlMehnahAlHaliyahllmostafeed = txtAlMehnahAlHaliyahllmostafeed.Text.Trim(),
            _AlHalahAlTaelimiahllmostafeed = DLAlHalahAlTaelimiahllmostafeed.SelectedValue,
            _Saleem = Convert.ToBoolean(CBSaleem.Checked),
            _Moalek = Convert.ToBoolean(CBMoalek.Checked),
            _TypeAleakah = txtTypeAleakah.Text.Trim(),
            _Mareedh = Convert.ToBoolean(CBMareedh.Checked),
            _TypeAlmaradh = txtTypeAlmaradh.Text.Trim(),
            _AlDakhlAlShahryllMostafeed = Convert.ToInt32(txtAlDakhlAlShahryllMostafeed.Text.Trim()),
            _MasderAlDakhl = Convert.ToInt32(DLMasderAlDakhl.SelectedValue),
            _TypeAlMasken = Convert.ToInt32(DLTypeAlMasken.SelectedValue),
            _HaletAlMasken = Convert.ToInt32(DLHaletAlMasken.SelectedValue),
            _EsmAlMostakhdem = Convert.ToInt32(IDUser),
            _dateEntry = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _HoursEntry = ClassDataAccess.GetCurrentTime().ToString("hh:mm:ss ttt"),
            _AlBaheth = Convert.ToInt32(IDUser),
            _TypeMostafeed = DLTypeMostafeed.SelectedValue,
            _DateRegistry = txtDateRegistry.Text.Trim(),
            _MehnahAlAAbKablAlWafah = txtMehnahAlAAbKablAlWafah.Text.Trim(),
            _AfradAlOsrah = 0,
            _CellPhone2 = txtCellPhoneTow.Text.Trim(),
            _Phone1 = txtPhoneOne.Text.Trim(),
            _Phone2 = txtPhoneTow.Text.Trim(),
            _NumberAlQareb = Convert.ToInt64(txtNumberQareb.Text.Trim()),
            _NameAlQareb = txtNameQareb.Text.Trim(),
            _SelatAlQarabah = Convert.ToInt32(DLSelatAlQarabah.SelectedValue),
            _NoteMosTafed = txtNote.Text.Trim(),
            _User_Name_ = txtNumberAlSegelAlMadany.Text.Trim(),
            _Email_User_ = txtNumberAlSegelAlMadany.Text.Trim() + "@gmail.com",
            _Password_User_ = ClassEncryptPassword.Encrypt(ClassDataAccess.RandomGenerator().ToString().Replace("-", ""), "لا اله الا انت سبحانك اني كنت من الظالمين"),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IsDelete = false,
            _A1 = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IDRaeesLagnatAlBahth = Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue),
            _IsAllowRaeesLagnatAlBahth = false,
            _IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
            _IsAllowModer = false,
            _IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            _IsRaeesMaglisAlEdarah = false,
            IDImg = Convert.ToInt32(Session["XIDRandom"]),
            IDUpdate = 0,
            DateUpdate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            Bank_Name = string.Empty,
            Bank_Account = string.Empty,
            Iban_Account = string.Empty
        };
        CMF.BArnRasAlEstemarahAdd();
        lbmsg.Text = "تم الإضافة بنجاح ";
        lbmsg.ForeColor = Color.MediumAquamarine;
        LBAddBoys.Enabled = true;
        btnAdd.Enabled = false;
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('تم إضافة بيانات المستفيد بنجاح');", true);
    }

    protected void txtdateBrith_TextChanged(object sender, EventArgs e)
    {
        DateTime today = ClassDataAccess.GetCurrentTime();
        string year = ddlYears.SelectedValue;
        string month = ddlMonths.SelectedValue;
        string date = ddlDates.SelectedValue;
        DateTime dob = Convert.ToDateTime(date + "/" + month + "/" + year);
        TimeSpan ts = today - dob;
        DateTime age = DateTime.MinValue + ts;
        int years = age.Year - 1;
        int months = age.Month - 1;
        int days = age.Day - 1;
        txtAge.Text = years.ToString();
        txtNumberQareb.Focus();
    }

    protected void LBBenaaHome_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitleImg.Text != string.Empty)
            {
                lblTitleImg.Visible = false;
                if (FBenaaHome.HasFile)
                {
                    lblBenaaHome.Visible = false;
                    System.Threading.Thread.Sleep(100);
                    FCheckNumber();
                }
                else if (FBenaaHome.HasFile == false)
                {
                    lblBenaaHome.Visible = true;
                    FBenaaHome.Focus();
                }

            }
            else if (txtTitleImg.Text == string.Empty)
            {
                lblTitleImg.Visible = true;
                txtTitleImg.Focus();
            }

        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBDeleteBenaaHome_Click(object sender, EventArgs e)
    {
        try
        {
            string Comp_ID = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[RasAlEstemarahImages] SET [_IsDelete] = @IsDelete WHERE _IDItam = @IDItam";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDItam", Convert.ToInt32(Comp_ID));
            cmd.Parameters.AddWithValue("@IsDelete", true);
            cmd.ExecuteScalar();
            conn.Close();
            FGetImgMosTafeed();
            lbmsg.Text = "تم حذف الصورة بنجاح";
            lbmsg.ForeColor = Color.Green;
            txtTitleImg.Focus();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('تم حذف الصورة بنجاح');", true);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckNumberMosTafeed();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBAddBoys_Click(object sender, EventArgs e)
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A76;
            A76 = Convert.ToBoolean(dtViewProfil.Rows[0]["A76"]);
            if (A76 == true)
            {
                Response.Redirect("PageBeneficiaryAddBoys.aspx?ID=" + txtNumberMostafeed.Text.Trim() + "&XID=" + txtNameMostafeed.Text.Trim());
            }
            else if (A76 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    private void FCheckNumber()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarahImages] Where TitleImg = @0 And _IDMustafeed = @1 And IDUniqInt = @2 And _IsDelete = @3", txtTitleImg.Text.Trim(), txtNumberMostafeed.Text.Trim(), Session["XIDRandom"].ToString(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "لقد قمت بإضافة الصورة سابقاً";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FChackImgF();
        }
    }

    private void FChackImgF()
    {
        if (FBenaaHome.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FBenaaHome.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i <= validFileTypes.Length - 1; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (!isValidFile)
            {
                lbmsg.ForeColor = Color.Red;
                lbmsg.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
            {
                FUpimgArticle(FBenaaHome);
            }
        }
        else
        {
            FUpimgArticle(FBenaaHome);
        }
    }
    string XXImg;
    protected void FUpimgArticle(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            System.Drawing.Image im = System.Drawing.Image.FromStream(strm);
            double h = im.PhysicalDimension.Height;
            double w = im.PhysicalDimension.Width;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = Convert.ToInt32(w); // 855; // New Width of Image in Pixel
                int newHeight = Convert.ToInt32(h); // 495; // New Height of Image in Pixel
                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgMostafeed/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                XXImg = "ImgSystem/ImgMostafeed/" + upl.FileName.Remove(3) + XRandom + ".png";
                FArticleAdd(Convert.ToInt32(Session["XIDRandom"]));
            }
        }
    }

    private void FArticleAdd(int IDImg)
    {
        ClassRasAlEstemarahImages CREI = new ClassRasAlEstemarahImages()
        {
            IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            _IDUniqInt = IDImg,
            _TitleImg = txtTitleImg.Text.Trim(),
            _IDType = 1,
            _ImgMosTafeed = XXImg,
            _DateAddimg = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd hh:mm:ss"),
            _IsDelete = false
        };
        CREI.BArnRasAlEstemarahImagesAdd();
        lbmsg.Text = "تم رفع الصوره بنجاح ";
        lbmsg.ForeColor = Color.Green;
        FGetImgMosTafeed();
        System.Threading.Thread.Sleep(500);
        txtTitleImg.Text = string.Empty;
        txtTitleImg.Focus();
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('تم رفع الصورة بنجاح');", true);
    }

    private void FGetImgMosTafeed()
    {
        ClassRasAlEstemarahImages CREI = new ClassRasAlEstemarahImages();
        CREI._Top = 15;
        CREI.IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
        CREI._IDUniqInt = Convert.ToInt32(Session["XIDRandom"]);
        CREI._IDType = 1;
        CREI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CREI.BArnRasAlEstemarahImagesImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTImgMosTafeed.DataSource = dt;
            RPTImgMosTafeed.DataBind();
        }
    }

}