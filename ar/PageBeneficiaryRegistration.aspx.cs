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

public partial class ar_PageBeneficiaryRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                HFLink.Value = Request.Url.Authority;
                FGetSetting();
                this.Page.Header.Title = "تسجيل مستفيد جديد" + " - " + HFNameSite.Value;
                FGetLastRecord();
                txtDateRegistry.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");

                DataTable dtYear = new DataTable();
                dtYear.Columns.Add("Year", typeof(int));
                for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 100; i <= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")); i++)
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
                for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 578; i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 700; i--)
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

            }
            catch (Exception)
            {
                return;
            }
        }
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            HFNameSite.Value = dt.Rows[0]["NameSiteAR"].ToString();

            HFTitle.Value = "تسجيل مستفيد جديد";
            HFDescrption.Value = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            HFKeyWord.Value = dt.Rows[0]["KeyWordAR"].ToString();
            HFImage.Value = dt.Rows[0]["ImgSystem"].ToString();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select max(NumberMostafeed) As 'NumberMostafeed' from RasAlEstemarah With(NoLock) Where IsActive = @0 And IsDelete = @1", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberMostafeed.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberMostafeed"]) + 1);
        }
        else
        {
            txtNumberMostafeed.Text = "1001";
        }
        FGetAlQariah();
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQriah.Items.Clear();
            DLAlQriah.Items.Add("");
            DLAlQriah.AppendDataBoundItems = true;
            DLAlQriah.DataValueField = "IDItem";
            DLAlQriah.DataTextField = "AlQriah";
            DLAlQriah.DataSource = dt;
            DLAlQriah.DataBind();
        }
        FGetGender();
    }

    private void FGetGender()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where Gender <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDeleteHalatMostafeed = @1 Order by IDItem", string.Empty, Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlDakhlAlShahryWaMasdarah <> @0 And IsDeleteAlDakhlAlShahryWaMasdarah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where TypeAlMaskan <> @0 And IsDeleteTypeAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatAlMaskan <> @0 And IsDeleteHalatAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where SelatAlQarabah <> @0 And IsDeleteSelatAlQarabah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
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
        ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
        ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
    }

    protected void DLHalafAlMosTafeed_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            //txtAlMehnahAlHaliyahllmostafeed.Focus();
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void txtdateBrith_TextChanged(object sender, EventArgs e)
    {

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

    private void FCheckNumber()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarahImages] With(NoLock) Where TitleImg = @0 And _IDMustafeed = @1 And IDUniqInt = @2 And _IsDelete = @3", txtTitleImg.Text.Trim(), txtNumberMostafeed.Text.Trim(), Session["XIDRandom"].ToString(), Convert.ToString(false));
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
            _DateAddimg = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"),
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
            txtTitleImg.Focus();
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('تم حذف الصورة بنجاح');", true);
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

    private void FCheckNumberMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "رقم المستفيد مستخدم لشخص آخر قم بتغييره";
            lbmsg.ForeColor = Color.Red;
        }
        else
            FCheckNameMosTafeed();
    }

    private void FCheckNameMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NameMostafeed = @0 And IsDelete = @1", txtNameMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "إسم المستفيد مستخدم لشخص آخر قم بتغييره";
            lbmsg.ForeColor = Color.Red;
        }
        else
            FCheckUserName();
    }

    private void FCheckUserName()
    {
        DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtUserName.Text.Trim(), Convert.ToString(false));
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtNumberAlSegelAlMadany.Text.Trim(), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtNumberAlSegelAlMadany.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //lbmsg.Text = "البريد الالكتروني مستخدم بالفعل";
            lbmsg.Text = "رقم السجل المدني مستخدم , يبدو أن لديك حساب بالفعل";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FChackPass();
        }
    }

    private void FChackPass()
    {
        //if (txtPassword.Text.Trim() == txtRePassword.Text.Trim())
        //{

            FMostafeedAdd();
        //}
        //else
        //{
        //    lbmsg.Text = "كلمات المرور التي ادخلتهما غير متطابقتان";
        //    lbmsg.ForeColor = Color.Red;
        //}
    }

    private void FMostafeedAdd()
    {
        DateTime today = ClassDataAccess.GetCurrentTime();
        Dates dateConvert = new Dates();

        string DateGen = dateConvert.HijriToGreg(Convert.ToDateTime(ddlYearsH.SelectedValue + "/" + ddlMonthsH.SelectedValue + "/" + ddlDatesH.SelectedValue).ToString("yyyy/MM/dd"));

        string yearH = ddlYearsH.SelectedValue;
        string monthH = ddlMonthsH.SelectedValue;
        string dateH = ddlDatesH.SelectedValue;

        ddlDates.SelectedValue = Convert.ToDateTime(DateGen).ToString("dd");
        ddlMonths.SelectedValue = Convert.ToDateTime(DateGen).ToString("MM");
        ddlYears.SelectedValue = Convert.ToDateTime(DateGen).ToString("yyyy");

        string year = ddlYears.SelectedValue;
        string month = ddlMonths.SelectedValue;
        string date = ddlDates.SelectedValue;
        DateTime dob = Convert.ToDateTime(date + "/" + month + "/" + year);

        TimeSpan ts = today - dob;
        DateTime age = DateTime.MinValue + ts;
        int years = age.Year - 1;
        int months = age.Month - 1;
        int days = age.Day - 1;
        txtAge.Text = years.ToString() + " سنة " + " و " + months.ToString() + " شهر ";
        txtNumberQareb.Focus();

        int IDBahth = 0;
        DataTable dtbahth = new DataTable();
        dtbahth = ClassDataAccess.GetData("Select Top(1) IDItem,IDAdminJoin from tbl_MultiQariah With(NoLock) Where IDQariah = @0", DLAlQriah.SelectedValue);
        if (dtbahth.Rows.Count > 0 )
        {
            IDBahth = Convert.ToInt32(dtbahth.Rows[0]["IDAdminJoin"]);
        }
        Session["IDUniq"] = Convert.ToString(Guid.NewGuid());
        ClassMosTafeed CMF = new ClassMosTafeed()
        {
            _IDUniq = Session["IDUniq"].ToString(),
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
            _EsmAlMostakhdem = 1124,
            _dateEntry = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _HoursEntry = ClassDataAccess.GetCurrentTime().ToString("HH:mm:ss ttt"),
            _AlBaheth = IDBahth,
            _TypeMostafeed = "مستبعد",
            _DateRegistry = txtDateRegistry.Text.Trim(),
            _MehnahAlAAbKablAlWafah = txtMehnahAlAAbKablAlWafah.Text.Trim(),
            _AfradAlOsrah = 0,
            _CellPhone2 = txtCellPhoneTow.Text.Trim(),
            _Phone1 = txtPhoneOne.Text.Trim(),
            _Phone2 = txtPhoneTow.Text.Trim(),
            _NumberAlQareb = Convert.ToInt64(txtNumberQareb.Text.Trim()),
            _NameAlQareb = txtNameQareb.Text.Trim(),
            _SelatAlQarabah = Convert.ToInt32(DLSelatAlQarabah.SelectedValue),
            _NoteMosTafed = "-",
            _User_Name_ = txtNumberAlSegelAlMadany.Text.Trim(),
            _Email_User_ = txtNumberAlSegelAlMadany.Text.Trim() + "@gmail.com",
            _Password_User_ = ClassEncryptPassword.Encrypt(ClassDataAccess.RandomGenerator().ToString().Replace("-", ""), "23ABC6587685DE4654A325BC456"),
            _IsActive = true,
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
            ID_Group = 1,
            Bank_Name = DLBank.SelectedValue,
            Bank_Account = txtBank_Account.Text.Trim(),
            Iban_Account = txtIBAN_Account.Text.Trim()
        };
        CMF.BArnRasAlEstemarahAdd();

        IDInfo.Visible = false;
        IDFile.Visible = true;
    }

    protected void btnBuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryRegistrationAddBuy.aspx?ID=" + Session["IDUniq"].ToString() + "&XID=" + txtNameMostafeed.Text.Trim());
    }

}