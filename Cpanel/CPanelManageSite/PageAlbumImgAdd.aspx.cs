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

public partial class Cpanel_CPanelManageSite_PageAlbumImgAdd : System.Web.UI.Page
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
            bool A15,A16;
            A15 = Convert.ToBoolean(dtViewProfil.Rows[0]["A15"]);
            A16 = Convert.ToBoolean(dtViewProfil.Rows[0]["A16"]);
            if (A15 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A16 == false)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "لا تمتلك صلاحية التعديل ... ";
                FUImgTeacher.Enabled = false;
                CBActive.Disabled = true;
                txtOrder.Enabled = false;
                btnDelete.Visible = false;
                btnAdd.Visible = false;
                GVImgAlbum.Columns[0].Visible = false;
                return;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CheckAccountAdmin();
                FGetDetail();
            }
        }
        catch (Exception)
        {
            Response.Redirect("PageAlbum.aspx");
        }
    }

    private void FGetDetail()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Album] Where RandomUniq = @0", Convert.ToString(Request.QueryString["ID"]));
        if (dt.Rows.Count > 0)
        {
            lblTitleAR.Text = dt.Rows[0]["TitleAlbumAr"].ToString();
            lblTitleTR.Text = dt.Rows[0]["TitleAlbumTr"].ToString();
            lblTitleEn.Text = dt.Rows[0]["TitleAlbumEn"].ToString();
            lblAr.Text = ClassSaddam.FChangeStyleCheckbox2(Convert.ToBoolean(dt.Rows[0]["IsViewAr"]));
            //CBViewAR.Checked = Convert.ToBoolean(dt.Rows[0]["IsViewAr"]);
            CBViewTR.Checked = Convert.ToBoolean(dt.Rows[0]["IsViewTr"]);
            CBViewEN.Checked = Convert.ToBoolean(dt.Rows[0]["IsViewEn"]);
            FGetAlbum(Convert.ToInt64(dt.Rows[0]["IDItem"]));

            DataTable dtImg = new DataTable();
            dtImg = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_AlbumImg] Where IDAlbum = @0 And IsDelete = @1 Order by IDOrder Desc", Convert.ToString(dt.Rows[0]["IDItem"]), Convert.ToString(false));
            if (dtImg.Rows.Count > 0)
                txtOrder.Text = Convert.ToString(Convert.ToInt32(dtImg.Rows[0]["IDOrder"]) + 1);
        }
    }

    private void FGetAlbum(float XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_AlbumImg] With(NoLock) Where IDAlbum = @0 And IsDelete = @1 Order by IDOrder", Convert.ToString(XID), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVImgAlbum.DataSource = dt;
            GVImgAlbum.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);

            pnlNull.Visible = false;
            pnlData.Visible = true;
            //btnDelete.Visible = true;
        }
        else
        {
            btnDelete.Visible = false;
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
    }

    protected void LBEnd_Click(object sender, EventArgs e)
    {
        Session.Remove("ImgImgAlb");
        Response.Redirect("PageAlbum.aspx");
    }
    
    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            foreach (GridViewRow row in GVImgAlbum.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVImgAlbum.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "DELETE FROM [dbo].[tbl_AlbumImg] WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            FChackImgF();
        }
        catch
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FChackImgF()
    {
        if (FUImgTeacher.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUImgTeacher.PostedFile.FileName);
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
                lblWarning.Text = "المسموح فقط : " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimg(FUImgTeacher);
        }
        else
            FUpimg(FUImgTeacher);
    }

    protected void FUpimg(FileUpload upl)
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
                int newWidth = Convert.ToInt32(w);
                int newHeight = Convert.ToInt32(h);

                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgAlbumImg/"), upl.FileName.Remove(3) + XRandom + upl.FileName + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["ImgImgAlb"] = "ImgSystem/ImgAlbumImg/" + upl.FileName.Remove(3) + XRandom + upl.FileName + ".png";
                AddAlbum();
            }
        }
        else
            AddAlbum();
    }

    private void AddAlbum()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT top(1) * FROM [dbo].[tbl_Album] With(NoLock) Where RandomUniq = @0", Convert.ToString(Request.QueryString["ID"]));
        if (dt.Rows.Count > 0)
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "INSERT INTO [dbo].[tbl_AlbumImg]([IDAlbum],[IsView],[IDOrder],[PathImg],[IsDelete],[DataAddImg],[IDAdmin]) VALUES (@IDAlbum,@IsView,@IDOrder,@PathImg,@IsDelete,@DataAddImg,@IDAdmin)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDAlbum", Convert.ToInt64(dt.Rows[0]["IDItem"]));
            cmd.Parameters.AddWithValue("@IsView", Convert.ToBoolean(CBActive.Checked));
            cmd.Parameters.AddWithValue("@IDOrder", Convert.ToInt32(txtOrder.Text.Trim()));
            cmd.Parameters.AddWithValue("@PathImg", Session["ImgImgAlb"].ToString());
            cmd.Parameters.AddWithValue("@IsDelete", Convert.ToBoolean(false));
            cmd.Parameters.AddWithValue("@DataAddImg", ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@IDAdmin", Convert.ToInt32(IDUser));
            cmd.ExecuteScalar();
            conn.Close();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            GVImgAlbum.DataBind();
            FGetDetail();
            
        }
    }

}