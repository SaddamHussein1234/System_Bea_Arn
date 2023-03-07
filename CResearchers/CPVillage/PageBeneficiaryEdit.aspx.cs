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

public partial class CResearchers_CPVillage_PageBeneficiaryEdit : System.Web.UI.Page
{
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
            txtNumberMostafeed.Focus();
            FGetLastRecord();
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("Year", typeof(int));
            for (int i = 1360; i <= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")); i++)
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
            FGetData();
            //Session["XIDRandom"] = Convert.ToInt32(ClassDataAccess.RandomGenerator().ToString().Replace("-", ""));
            FGetImgMosTafeed();
        }
    }

    private void FGetData()
    {
        try
        {
            GetCookie();
            DataTable dtGetQariah = new DataTable();
            dtGetQariah = ClassDataAccess.GetData("SELECT Top(1) [NumberMostafeed],[AlQaryah],[IsDelete] FROM [dbo].[RasAlEstemarah] With(NoLock) Where IDUniq = @0", Convert.ToString(Request.QueryString["XID"]));
            if (dtGetQariah.Rows.Count > 0)
            {
                DataTable dtCheck = new DataTable();
                dtCheck = ClassDataAccess.GetData("SELECT Top(1) [IDItem],[IDAdminJoin],[IDQariah],[IsDelete] FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And IsDelete = @2"
                    , IDUser, dtGetQariah.Rows[0]["AlQaryah"].ToString(), Convert.ToString(false));
                if (dtCheck.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where IDUniq = @0 And IsDelete = @1", Convert.ToString(Request.QueryString["XID"]), Convert.ToString(false));
                    if (dt.Rows.Count > 0)
                    {
                        Session["XIDNumberMostafeed"] = dt.Rows[0]["NumberMostafeed"].ToString();
                        txtNumberMostafeed.Text = Session["XIDNumberMostafeed"].ToString();
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
                        DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
                        DLTypeMostafeed.SelectedValue = dt.Rows[0]["TypeMostafeed"].ToString();
                        txtDateRegistry.Text = Convert.ToDateTime(dt.Rows[0]["DateRegistry"]).ToString("dd-MM-yyyy");
                        txtMehnahAlAAbKablAlWafah.Text = dt.Rows[0]["MehnahAlAAbKablAlWafah"].ToString();
                        txtCellPhoneTow.Text = dt.Rows[0]["CellPhone2"].ToString();
                        txtPhoneOne.Text = dt.Rows[0]["Phone1"].ToString();
                        txtPhoneTow.Text = dt.Rows[0]["Phone2"].ToString();
                        txtNumberQareb.Text = dt.Rows[0]["NumberAlQareb"].ToString();
                        txtNameQareb.Text = dt.Rows[0]["NameAlQareb"].ToString();
                        DLSelatAlQarabah.SelectedValue = dt.Rows[0]["SelatAlQarabah"].ToString();
                        txtNote.Text = dt.Rows[0]["NoteMosTafed"].ToString();
                        Session["UserNameOld_"] = dt.Rows[0]["User_Name_"].ToString();
                        txtUserName.Text = Session["UserNameOld_"].ToString();
                        Session["EmailNameOld_"] = dt.Rows[0]["Email_User_"].ToString();
                        txtEmail.Text = Session["EmailNameOld_"].ToString();
                        txtPassword.Text = ClassEncryptPassword.Decrypt(dt.Rows[0]["Password_User_"].ToString(), "لا اله الا انت سبحانك اني كنت من الظالمين");
                        txtRePassword.Text = txtPassword.Text.Trim();
                        Session["UserOldPass"] = txtPassword.Text.Trim();
                        CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                        DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaeesLagnatAlBahth_"].ToString();
                        DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer_"].ToString();
                        DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["IDRaeesMaglisAlEdarah_"].ToString();
                        lblIDImg.Text = dt.Rows[0]["_IDImg"].ToString();
                    }
                }
                else
                {
                    Response.Redirect("PageNotAccess.aspx");
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
            DLModerAlGmeiah.Items.Clear();
            DLModerAlGmeiah.Items.Add("");
            DLModerAlGmeiah.AppendDataBoundItems = true;
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
            DLRaeesMaglesAlEdarah.Items.Clear();
            DLRaeesMaglesAlEdarah.Items.Add("");
            DLRaeesMaglesAlEdarah.AppendDataBoundItems = true;
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
            DLRaeesLagnatAlBahath.Items.Clear();
            DLRaeesLagnatAlBahath.Items.Add("");
            DLRaeesLagnatAlBahath.AppendDataBoundItems = true;
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {

    }

    protected void ddlDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
            txtAge.Text = years.ToString() + " سنة " + " و " + months.ToString() + " شهر ";
            txtNumberQareb.Focus();
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
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarahImages] Where TitleImg = @0 And _IDMustafeed = @1 And IDUniqInt = @2 And _IsDelete = @3", txtTitleImg.Text.Trim(), txtNumberMostafeed.Text.Trim(), lblIDImg.Text, Convert.ToString(false));
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
        lbmsg.Text = "تم رفع الصوره بنجاح ";
        lbmsg.ForeColor = Color.Green;
        FGetImgMosTafeed();
        System.Threading.Thread.Sleep(500);
        txtTitleImg.Text = string.Empty;
        txtTitleImg.Focus();
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('تم رفع الصورة بنجاح');", true);
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
        if (txtNumberMostafeed.Text.Trim() != Session["XIDNumberMostafeed"].ToString())
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
                Session["XIDNumberMostafeed"] = txtNumberMostafeed.Text.Trim();
                FCheckNameMosTafeed();
            }
        }
        else if (txtNumberMostafeed.Text.Trim() == Session["XIDNumberMostafeed"].ToString())
        {
            FCheckNameMosTafeed();
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
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtNumberAlSegelAlMadany.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                //lbmsg.Text = "إسم الدخول للنظام مستخدم لشخص آخر قم بتغييره";
                lbmsg.Text = "رقم السجل المدني مستخدم لشخص آخر قم بتغييره";
                lbmsg.ForeColor = Color.Red;
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
        FChackPass();
    }

    private void FChackPass()
    {
        if (txtPassword.Text.Trim() != string.Empty)
        {
            if (txtPassword.Text.Trim() == txtRePassword.Text.Trim())
            {
                Session["UserOldPass"] = ClassEncryptPassword.Encrypt(txtPassword.Text.Trim(), "لا اله الا انت سبحانك اني كنت من الظالمين");
                FMostafeedEdit();
            }
            else
            {
                lbmsg.Text = "كلمات المرور التي ادخلتهما غير متطابقتان";
                lbmsg.ForeColor = Color.Red;
            }
        }
        else
        {
            FMostafeedEdit();
        }
    }

    private void FMostafeedEdit()
    {
        GetCookie();
        ClassMosTafeed CMF = new ClassMosTafeed();
        //{
        CMF._IDUniq = Convert.ToString(Request.QueryString["XID"]);
        CMF._NumberMostafeed = Convert.ToInt32(Session["XIDNumberMostafeed"]);
        CMF._NameMostafeed = txtNameMostafeed.Text.Trim();
        CMF._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
        CMF._Gender = Convert.ToInt32(DLGender.SelectedValue);
        CMF._PhoneNumber = Convert.ToInt64(txtCellPhoneOne.Text.Trim());
        CMF._NumberAlSegelAlMadany = Convert.ToInt64(Session["XIDNumberAlSegelAlMadany"]);
        CMF._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.Text.Trim());
        CMF._dateBrith = ddlYears.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlDates.SelectedValue;
        CMF._Age = txtAge.Text.Trim();
        CMF._AlMehnahAlHaliyahllmostafeed = txtAlMehnahAlHaliyahllmostafeed.Text.Trim();
        CMF._AlHalahAlTaelimiahllmostafeed = DLAlHalahAlTaelimiahllmostafeed.SelectedValue;
        CMF._Saleem = Convert.ToBoolean(CBSaleem.Checked);
        CMF._Moalek = Convert.ToBoolean(CBMoalek.Checked);
        CMF._TypeAleakah = txtTypeAleakah.Text.Trim();
        CMF._Mareedh = Convert.ToBoolean(CBMareedh.Checked);
        CMF._TypeAlmaradh = txtTypeAlmaradh.Text.Trim();
        CMF._AlDakhlAlShahryllMostafeed = Convert.ToInt32(txtAlDakhlAlShahryllMostafeed.Text.Trim());
        CMF._MasderAlDakhl = Convert.ToInt32(DLMasderAlDakhl.SelectedValue);
        CMF._TypeAlMasken = Convert.ToInt32(DLTypeAlMasken.SelectedValue);
        CMF._HaletAlMasken = Convert.ToInt32(DLHaletAlMasken.SelectedValue);
        CMF._AlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
        CMF._TypeMostafeed = DLTypeMostafeed.SelectedValue;
        CMF._DateRegistry = txtDateRegistry.Text.Trim();
        CMF._MehnahAlAAbKablAlWafah = txtMehnahAlAAbKablAlWafah.Text.Trim();
        CMF._CellPhone2 = txtCellPhoneTow.Text.Trim();
        CMF._Phone1 = txtPhoneOne.Text.Trim();
        CMF._Phone2 = txtPhoneTow.Text.Trim();
        CMF._NumberAlQareb = Convert.ToInt64(txtNumberQareb.Text.Trim());
        CMF._NameAlQareb = txtNameQareb.Text.Trim();
        CMF._SelatAlQarabah = Convert.ToInt32(DLSelatAlQarabah.SelectedValue);
        CMF._NoteMosTafed = txtNote.Text.Trim();
        CMF._User_Name_ = Session["UserNameOld_"].ToString();
        CMF._Email_User_ = Session["EmailNameOld_"].ToString();
        CMF._Password_User_ = Session["UserOldPass"].ToString();
        CMF._IsActive = Convert.ToBoolean(CBActive.Checked);
        CMF._A2 = "0";
        CMF._A3 = "0";
        CMF._A4 = "0";
        CMF._A5 = "0";
        CMF._IDRaeesLagnatAlBahth = Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue);
        CMF._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
        CMF._IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue);
        CMF.IDUpdate = Convert.ToInt32(IDUser);
        CMF.DateUpdate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
        //};
        CMF.BArnRasAlEstemarahEdit();
        lbmsg.Text = "تم التعديل بنجاح ";
        lbmsg.ForeColor = Color.MediumAquamarine;
        FGetData();
        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('تم تعديل بيانات المستفيد بنجاح');", true);
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

}