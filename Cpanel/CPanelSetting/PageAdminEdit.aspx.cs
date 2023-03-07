using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelSetting_PageAdminEdit : System.Web.UI.Page
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
            bool A8;
            A8 = Convert.ToBoolean(dtViewProfil.Rows[0]["A8"]);
            if (A8 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetGroup();
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[tbl_Admin] With(NoLock) Where IDUniq = @0 And IsDelete = @1", Convert.ToString(Request.QueryString["ID"]), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                DLGroup.SelectedValue = dt.Rows[0]["IDGroup"].ToString();
                txtName.Text = dt.Rows[0]["FirstName"].ToString();
                txtUserName.Text = dt.Rows[0]["User_Name_"].ToString();
                Session["AdminUser"] = dt.Rows[0]["User_Name_"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                Session["AdminEmail"] = dt.Rows[0]["Email"].ToString();
                Session["AdminPass"] = dt.Rows[0]["__pass_"].ToString();
                txtPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                CBView.Checked = Convert.ToBoolean(dt.Rows[0]["IsBlock"]);
                Session["AdminImgOld"] = dt.Rows[0]["ImgAdmin"].ToString();
                ImgAdmin.ImageUrl = "/" + Session["AdminImgOld"].ToString();
                CBIsAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["IsAdmin"]);
                CBIsBaheth.Checked = Convert.ToBoolean(dt.Rows[0]["IsBaheth"]);
                CBIsRaeesLgnatAlBath.Checked = Convert.ToBoolean(dt.Rows[0]["IsRaeesLgnatAlBath"]);
                CBIsModer.Checked = Convert.ToBoolean(dt.Rows[0]["IsModer"]);
                if (Convert.ToBoolean(dt.Rows[0]["IsAdminInEdarah"]) == true)
                {
                    DLIsAdminInEdarah.SelectedValue = "true";
                    if (DLIsAdminInEdarah.SelectedValue == "true")
                    {
                        pnlAdminInEdarah.Visible = true;
                    }
                    else if (DLIsAdminInEdarah.SelectedValue == "false")
                    {
                        pnlAdminInEdarah.Visible = false;
                    }
                    txtIsOrderAdminInEdarah.Text = dt.Rows[0]["IsOrderAdminInEdarah"].ToString();
                    CBIsRaeesMaglisAlEdarah.Checked = Convert.ToBoolean(dt.Rows[0]["IsRaeesMaglisAlEdarah"]);
                    CBIsNaebMaglisAlEdarah.Checked = Convert.ToBoolean(dt.Rows[0]["IsNaebMaglisAlEdarah"]);
                    CBIsAmeenAlSondoq.Checked = Convert.ToBoolean(dt.Rows[0]["IsAmeenAlSondoq"]);
                    CBIsAmeenGeneral.Checked = Convert.ToBoolean(dt.Rows[0]["IsAmeenGeneral"]);
                }
                else if (Convert.ToBoolean(dt.Rows[0]["IsAdminInEdarah"]) == false)
                {
                    DLIsAdminInEdarah.SelectedValue = "false";
                    txtIsOrderAdminInEdarah.Text = dt.Rows[0]["IsOrderAdminInEdarah"].ToString();
                }
                CBIsStorekeeper.Checked = Convert.ToBoolean(dt.Rows[0]["IsStorekeeper"]);
                Session["AdminImgSignature"] = dt.Rows[0]["AddImgSignature"].ToString();
                ImgSignature.ImageUrl = "/" + Session["AdminImgSignature"].ToString();
                txtCommentAdmin.Text = dt.Rows[0]["CommentAdmin"].ToString();
                bool ISUperAdmin;
                ISUperAdmin = Convert.ToBoolean(dt.Rows[0]["IsSuperAdmin"]);
                if (ISUperAdmin)
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    lblMessageWarning.Text = "لا يمكن تعديل المدير العام ... ";
                    btnAdd.Visible = false;
                    DLGroup.Enabled = false;
                    txtName.Enabled = false;
                    txtUserName.Enabled = false;
                    txtEmail.Enabled = false;
                    txtPass.Enabled = false;
                    txtRePass.Enabled = false;
                    txtPhone.Enabled = false;
                    CBView.Checked = false;
                    FUImgAdmin.Enabled = false;
                    FAddImgSignature.Enabled = false;
                }
                Session["AdminEmpID"] = dt.Rows[0]["_IDEmpNumber"].ToString();
                txtEmpNumber.Text = Session["AdminEmpID"].ToString();
                CBIsAssmply.Checked = Convert.ToBoolean(dt.Rows[0]["A1"]);
                txtDateRigstr.Text = Convert.ToDateTime(dt.Rows[0]["A2"]).ToString("yyyy-MM-dd");
                txt_ID_Card.Text = dt.Rows[0]["A3"].ToString();
                CBIsRaeesShoaoon.Checked = Convert.ToBoolean(dt.Rows[0]["A4"]);
                CBIsModerEmp.Checked = Convert.ToBoolean(dt.Rows[0]["_Moder_Emp_"]);
            }
            else
                Response.Redirect("PageAdmin.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageAdmin.aspx");
        }
    }

    private void FGetGroup()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) [IDGroup],[IDUniqGroup],[NameGroup],[IsActiveGroup],[IsSuperAdminGroup],[IsDeleteGroup],[DateAddGroup] FROM [dbo].[tbl_Group_Arn] With(NoLock) Where IsActiveGroup = @0 And IsSuperAdminGroup = @1 And IsDeleteGroup = @2 Order By NameGroup", Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLGroup.Items.Clear();
            DLGroup.Items.Add("");
            DLGroup.AppendDataBoundItems = true;
            DLGroup.DataValueField = "IDGroup";
            DLGroup.DataTextField = "NameGroup";
            DLGroup.DataSource = dt;
            DLGroup.DataBind();
        }
    }

    protected void DLIsAdminInEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DLIsAdminInEdarah.SelectedValue == "true")
                pnlAdminInEdarah.Visible = true;
            else if (DLIsAdminInEdarah.SelectedValue == "false")
                pnlAdminInEdarah.Visible = false;
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
            FChackEmpID();
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

    private void FChackEmpID()
    {
        if (txtEmpNumber.Text.Trim() != Session["AdminEmpID"].ToString())
        {
            DataTable dtUser = new DataTable();
            dtUser = ClassDataAccess.GetData("Select Top(1) IsDelete,_IDEmpNumber from tbl_Admin With(NoLock) Where IsDelete = @0 And _IDEmpNumber = @1", Convert.ToString(false), txtEmpNumber.Text.Trim());
            if (dtUser.Rows.Count > 0)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "الرقم الوظيفي مستخدم ... ";
                return;
            }
            else
            {
                Session["AdminEmpID"] = txtEmpNumber.Text.Trim();
                FChackUserName();
            }
        }
        else
            FChackUserName();
    }

    private void FChackUserName()
    {
        if (txtUserName.Text.Trim() != Session["AdminUser"].ToString())
        {
            DataTable dtUser = new DataTable();
            dtUser = ClassDataAccess.GetData("Select Top(1) User_Name_,IsDelete from tbl_Admin With(NoLock) Where User_Name_ = @0 And IsDelete = @1", txtUserName.Text.Trim(), Convert.ToString(false));
            if (dtUser.Rows.Count > 0)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "إسم المستخدم مستخدم ... ";
                return;
            }
            else
            {
                Session["AdminUser"] = txtUserName.Text.Trim();
                FChackEmail();
            }
        }
        else
            FChackEmail();
    }

    private void FChackEmail()
    {
        if (txtEmail.Text.Trim() != Session["AdminEmail"].ToString())
        {
            DataTable dtEmail = new DataTable();
            dtEmail = ClassDataAccess.GetData("Select Top(1) Email,IsDelete from tbl_Admin With(NoLock) Where Email = @0 And IsDelete = @1", txtEmail.Text.Trim(), Convert.ToString(false));
            if (dtEmail.Rows.Count > 0)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "البريد الالكتروني مستخدم ...";
                return;
            }
            else
            {
                Session["AdminEmail"] = txtEmail.Text.Trim();
                FChackPass();
            }
        }
        else
            FChackPass();
    }

    private void FChackPass()
    {
        if (txtPass.Text.Trim() != string.Empty)
        {
            if (txtPass.Text.Trim() == txtRePass.Text.Trim())
            {
                Session["AdminPass"] = ClassEncryptPassword.Encrypt(txtPass.Text.Trim(), "www.ITFY-Edu.Net_For_Saddam");
                FChackImgF();
            }
            else
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "كلمات المرور التي ادخلتهما غير متطابقتان";
                return;
            }
        }
        else
            FChackImgF();
    }

    private void FChackImgF()
    {
        if (FUImgAdmin.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUImgAdmin.PostedFile.FileName);
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
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimg(FUImgAdmin);
        }
        else
            FUpimg(FUImgAdmin);
    }

    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = 256; // New Width of Image in Pixel
                int newHeight = 192; // New Height of Image in Pixel
                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgAdmin/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["AdminImgOld"] = "ImgSystem/ImgAdmin/" + upl.FileName.Remove(3) + XRandom + ".png";
                FChackImgSignatureF();
            }
        }
        else
            FChackImgSignatureF();
    }

    private void FChackImgSignatureF()
    {
        if (FAddImgSignature.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FAddImgSignature.PostedFile.FileName);
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
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimgSignature(FAddImgSignature);
        }
        else
            FUpimgSignature(FAddImgSignature);
    }

    protected void FUpimgSignature(FileUpload upl)
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
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgSignature/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["AdminImgSignature"] = "ImgSystem/ImgSignature/" + upl.FileName.Remove(3) + XRandom + ".png";
                FAdminEdit();
            }
        }
        else
            FAdminEdit();
    }

    private void FAdminEdit()
    {
        ClassAdmin_Arn CA = new ClassAdmin_Arn();
        CA._IDUniq = Convert.ToString(Request.QueryString["ID"]);
        CA._IDGroup = Convert.ToInt32(DLGroup.SelectedValue);
        CA._FirstName = txtName.Text.Trim();
        CA._User_Name_ = Session["AdminUser"].ToString();
        CA._Email = Session["AdminEmail"].ToString();
        CA.___pass_ = Session["AdminPass"].ToString();
        CA._PhoneNumber = txtPhone.Text.Trim();
        CA._IsBlock = Convert.ToBoolean(CBView.Checked);
        CA._IsSuperAdmin = false;
        CA._ImgAdmin = Session["AdminImgOld"].ToString();
        CA._IsAdmin = Convert.ToBoolean(CBIsAdmin.Checked);
        CA._IsBaheth = Convert.ToBoolean(CBIsBaheth.Checked);
        CA._IDQriah = 0;
        CA._IsRaeesLgnatAlBath = Convert.ToBoolean(CBIsRaeesLgnatAlBath.Checked);
        CA._IsModer = Convert.ToBoolean(CBIsModer.Checked);
        if (DLIsAdminInEdarah.SelectedValue == "true")
        {
            CA._IsAdminInEdarah = true;
            CA._IsOrderAdminInEdarah = Convert.ToInt32(txtIsOrderAdminInEdarah.Text.Trim());
            CA._IsRaeesMaglisAlEdarah = Convert.ToBoolean(CBIsRaeesMaglisAlEdarah.Checked);
            CA._IsNaebMaglisAlEdarah = Convert.ToBoolean(CBIsNaebMaglisAlEdarah.Checked);
            CA._IsAmeenAlSondoq = Convert.ToBoolean(CBIsAmeenAlSondoq.Checked);
            CA._IsAmeenGeneral = Convert.ToBoolean(CBIsAmeenGeneral.Checked);
        }
        else if (DLIsAdminInEdarah.SelectedValue == "false")
        {
            CA._IsAdminInEdarah = false;
            CA._IsOrderAdminInEdarah = Convert.ToInt32(txtIsOrderAdminInEdarah.Text.Trim());
            CA._IsRaeesMaglisAlEdarah = false;
            CA._IsNaebMaglisAlEdarah = false;
            CA._IsAmeenAlSondoq = false;
            CA._IsAmeenGeneral = false;
        }
        CA._IsStorekeeper = Convert.ToBoolean(CBIsStorekeeper.Checked);
        CA._AddImgSignature = Session["AdminImgSignature"].ToString();
        CA._CommentAdmin = txtCommentAdmin.Text.Trim();
        CA._A1 = Convert.ToBoolean(CBIsAssmply.Checked);
        CA._A2 = txtDateRigstr.Text.Trim();
        CA._A3 = txt_ID_Card.Text.Trim();
        CA._A4 = Convert.ToBoolean(CBIsRaeesShoaoon.Checked);
        CA._A5 = "0";
        CA.IDEmpNumber = Convert.ToInt32(txtEmpNumber.Text.Trim());
        CA._Moder_Emp = Convert.ToBoolean(CBIsModerEmp.Checked);
        CA.BArnAdmin_Edit();
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        lblMessage.Text = "تم التعديل بنجاح ";
        ImgAdmin.ImageUrl = "/" + Session["AdminImgOld"].ToString();
        ImgSignature.ImageUrl = "/" + Session["AdminImgSignature"].ToString();
        if (Attach_Repostry_SMS_Send_.AllSendSystemSetting())
            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل مستخدم للنظام \n بإسم :" + txtName.Text.Trim(), "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
    }
    
    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAdmin.aspx");
    }

}