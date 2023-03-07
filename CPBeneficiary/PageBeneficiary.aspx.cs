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

public partial class CPBeneficiary_PageBeneficiary : System.Web.UI.Page
{
    string UserERasAlEstemarah;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_User"];
            UserERasAlEstemarah = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if (dt.Rows.Count > 0)
        {
            bool A25;
            A25 = Convert.ToBoolean(dt.Rows[0]["A25"]);
            if (A25)
                FGetData(dt.Rows[0]["NumberMostafeed"].ToString());
            else
                Response.Redirect("PageNotAccess.aspx");
        }
        else
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNumberMostafeed.Focus();
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

            CheckAccountAdmin();
            FGetImgMosTafeed();
        }
    }

    private void FGetData(string XID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", XID, Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                txtNumberMostafeed.Text = XID;
                txtNameMostafeed.Text = dt.Rows[0]["NameMostafeed"].ToString();
                DLAlQriah.SelectedValue = dt.Rows[0]["AlQaryah"].ToString();
                DLGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtCellPhoneOne.Text = dt.Rows[0]["PhoneNumber"].ToString();
                Session["XIDNumberAlSegelAlMadany"] = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                txtNumberAlSegelAlMadany.Text = Session["XIDNumberAlSegelAlMadany"].ToString();
                DLHalafAlMosTafeed.SelectedValue = dt.Rows[0]["HalafAlMosTafeed"].ToString();
                ddlYears.SelectedValue = Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy");
                ddlMonths.SelectedValue = Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM");
                ddlDates.SelectedValue = Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd");

                string DateHijri;
                DateHijri = Convert.ToDateTime(ClassSaddam.ConvertDateCalendar(Convert.ToDateTime(ddlDates.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlYears.SelectedValue), "Hijri", "en-US")).ToString("dd/MM/yyyy");
                ddlDatesH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("dd");
                ddlMonthsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("MM");
                ddlYearsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("yyyy");

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
                txtAge.Text = years.ToString() + " سنة " + " و " + months.ToString() + " شهر ";

                txtAlMehnahAlHaliyahllmostafeed.Text = dt.Rows[0]["AlMehnahAlHaliyahllmostafeed"].ToString();
                DLAlHalahAlTaelimiahllmostafeed.SelectedValue = dt.Rows[0]["AlHalahAlTaelimiahllmostafeed"].ToString();
                CBSaleem.Checked = Convert.ToBoolean(dt.Rows[0]["Saleem"]);
                CBMoalek.Checked = Convert.ToBoolean(dt.Rows[0]["Moalek"]);
                txtTypeAleakah.Text = dt.Rows[0]["TypeAleakah"].ToString();
                CBMareedh.Checked = Convert.ToBoolean(dt.Rows[0]["Mareedh"]);
                txtTypeAlmaradh.Text = dt.Rows[0]["TypeAlmaradh"].ToString();
                txtAlDakhlAlShahryllMostafeed.Text = dt.Rows[0]["AlDakhlAlShahryllMostafeed"].ToString();
                DLMasderAlDakhl.SelectedValue = dt.Rows[0]["MasderAlDakhl"].ToString();
                DLTypeAlMasken.SelectedValue = dt.Rows[0]["TypeAlMasken"].ToString();
                DLHaletAlMasken.SelectedValue = dt.Rows[0]["HaletAlMasken"].ToString();
                txtMehnahAlAAbKablAlWafah.Text = dt.Rows[0]["MehnahAlAAbKablAlWafah"].ToString();
                txtCellPhoneTow.Text = dt.Rows[0]["CellPhone2"].ToString();
                txtPhoneOne.Text = dt.Rows[0]["Phone1"].ToString();
                txtPhoneTow.Text = dt.Rows[0]["Phone2"].ToString();
                txtNumberQareb.Text = dt.Rows[0]["NumberAlQareb"].ToString();
                txtNameQareb.Text = dt.Rows[0]["NameAlQareb"].ToString();
                DLSelatAlQarabah.SelectedValue = dt.Rows[0]["SelatAlQarabah"].ToString();
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                lblIDImg.Text = dt.Rows[0]["_IDImg"].ToString();
                if (DLHalafAlMosTafeed.SelectedItem.ToString() == "ايتام")
                {
                    PnlMehnah.Visible = true;
                }
                else
                {
                    PnlMehnah.Visible = false;
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetLastRecord()
    {
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
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void ddlDatesH_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
        }
        catch (Exception)
        {
            return;
        }
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

    private void FCheckNumber()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarahImages] With(NoLock) Where TitleImg = @0 And _IDMustafeed = @1 And IDUniqInt = @2 And _IsDelete = @3", txtTitleImg.Text.Trim(), txtNumberMostafeed.Text.Trim(), lblIDImg.Text, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لقد قمت بإضافة الصورة سابقاً";
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
            string[] validFileTypes = { "bmp", "BMP", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
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
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
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
                FArticleAdd(Convert.ToInt32(lblIDImg.Text));
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
        IDMessageWarning.Visible = true;
        IDMessageSuccess.Visible = false;
        lblWarning.Text = "تم رفع الصوره بنجاح ";
        FGetImgMosTafeed();
        System.Threading.Thread.Sleep(500);
        txtTitleImg.Text = string.Empty;
        txtTitleImg.Focus();
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('تم رفع الصورة بنجاح');", true);
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

    private void FGetImgMosTafeed()
    {
        try
        {
            ClassRasAlEstemarahImages CREI = new ClassRasAlEstemarahImages();
            CREI._Top = 15;
            CREI.IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
            CREI._IDUniqInt = Convert.ToInt32(lblIDImg.Text);
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
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckNameMosTafeed();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNameMosTafeed()
    {
        FCheckUserName();
    }

    private void FCheckUserName()
    {
        if (txtNumberAlSegelAlMadany.Text.Trim() != Session["XIDNumberAlSegelAlMadany"].ToString())
        {
            DataTable dt = new DataTable();
            //dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtUserName.Text.Trim(), Convert.ToString(false));
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtNumberAlSegelAlMadany.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                //lbmsg.Text = "إسم الدخول للنظام مستخدم لشخص آخر قم بتغييره";
                lblWarning.Text = "رقم السجل المدني مستخدم لشخص آخر قم بتغييره";
            }
            else
            {
                Session["XIDNumberAlSegelAlMadany"] = txtNumberAlSegelAlMadany.Text.Trim();
                FCheckEmail();
            }
        }
        else if (txtNumberAlSegelAlMadany.Text.Trim() == Session["XIDNumberAlSegelAlMadany"].ToString())
        {
            FCheckEmail();
        }
    }

    private void FCheckEmail()
    {
        //DataTable dt = new DataTable();
        ////dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where Email_User_ = @0 And IsDelete = @1", txtEmail.Text.Trim(), Convert.ToString(false));
        //dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtNumberAlSegelAlMadany.Text.Trim(), Convert.ToString(false));
        //if (dt.Rows.Count > 0)
        //{
        //    //lbmsg.Text = "البريد الالكتروني مستخدم بالفعل";
        //    lbmsg.Text = "رقم السجل المدني مستخدم لشخص آخر قم بتغييره";
        //    lbmsg.ForeColor = Color.Red;
        //}
        //else
        //{
        //    FChackPass();
        //}
        FMostafeedEdit();
    }

    private void FMostafeedEdit()
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if (dt.Rows.Count > 0)
        {
            ClassMosTafeed CMF = new ClassMosTafeed()
            {
                _NumberMostafeed = Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]),
                _NameMostafeed = txtNameMostafeed.Text.Trim(),
                _AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue),
                _Gender = Convert.ToInt32(DLGender.SelectedValue),
                _PhoneNumber = Convert.ToInt64(txtCellPhoneOne.Text.Trim()),
                _NumberAlSegelAlMadany = Convert.ToInt64(Session["XIDNumberAlSegelAlMadany"]),
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
                _MehnahAlAAbKablAlWafah = txtMehnahAlAAbKablAlWafah.Text.Trim(),
                _CellPhone2 = txtCellPhoneTow.Text.Trim(),
                _Phone1 = txtPhoneOne.Text.Trim(),
                _Phone2 = txtPhoneTow.Text.Trim(),
                _NumberAlQareb = Convert.ToInt64(txtNumberQareb.Text.Trim()),
                _NameAlQareb = txtNameQareb.Text.Trim(),
                _SelatAlQarabah = Convert.ToInt32(DLSelatAlQarabah.SelectedValue),
                DateUpdate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            };
            CMF.BArnRasAlEstemarahEditByMostafeed();
        }
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم التعديل بنجاح ";
        CheckAccountAdmin();
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

}