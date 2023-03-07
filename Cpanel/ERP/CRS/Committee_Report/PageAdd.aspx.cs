using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRS.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
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

public partial class Cpanel_ERP_CRS_Committee_Report_PageAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            HFID.Value = Guid.NewGuid().ToString();
            Repostry_CRS_Type_.FGetDropList(1, "_Ar", string.Empty, DLType);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetAdminAllByItem("ByID", DLCommittee_Members);
            FGetLastBill();
            FGetLastBillCommittee_Members();
            if (Request.QueryString["ID"] != null)
                FGetData();
            System.Threading.Thread.Sleep(100);
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_CRS_Committee_Report_.FGetDataInDataTable("GetByID", 1, new Guid(Request.QueryString["ID"]), 
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                HFID.Value = dt.Rows[0]["_ID_Item_"].ToString();
                ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                DLType.SelectedValue = dt.Rows[0]["_ID_Type_"].ToString();
                txtNumberBill.Text = dt.Rows[0]["_Nmber_"].ToString();
                txtTitle.Text = dt.Rows[0]["_Title_"].ToString();
                txtMeeting_Venue.Text = dt.Rows[0]["_Meeting_Venue_"].ToString();
                txtObjective_Of_the_Report.Text = dt.Rows[0]["_Objective_Of_the_Report_"].ToString().Replace("<br />", Environment.NewLine);
                txtReport_Recommendations.Text = dt.Rows[0]["_Report_Recommendations_"].ToString().Replace("<br />", Environment.NewLine);
                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["_IDRaees_"].ToString();
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
                FGetImages(); FGetCommittee_Members();
                FGetLastBillCommittee_Members();
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FGetLastBill()
    {
        txtNumberBill.Text = Convert.ToString(Repostry_CRS_Committee_Report_.FGetCount("GetLast", 1, new Guid(ddlYears.SelectedValue), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1);
    }
    
    private void FGetLastBillCommittee_Members()
    {
        txtOrder.Text = Convert.ToString(Repostry_CRS_Committee_Members_.FGetCount("GetLast", 1, new Guid(HFID.Value), string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1);
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAdd.aspx");
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (txtNumberBill.Text.Trim() != string.Empty)
            {
                if (RPTCommittee_Members.Items.Count > 0)
                    FAdd();
                else
                {
                    lblMessageWarning.Text = "لم يتم إضافة الأعضاء بعد";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
            }
            else
            {
                lblMessageWarning.Text = "يُرجى إدخال رقم التقرير ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FAdd()
    {
        string Xresult = string.Empty, XCheck = string.Empty;
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        Guid XID = Guid.Empty;
        int XIDAdd = 0, XUpdate = 0;
        if (Request.QueryString["ID"] == null)
        {
            XCheck = "Add"; XID = new Guid(HFID.Value); XIDAdd = Test_Saddam.FGetIDUsiq();
        }
        if (Request.QueryString["ID"] != null)
        {
            XCheck = "Edit"; XID = new Guid(Request.QueryString["ID"]); XUpdate = Test_Saddam.FGetIDUsiq();
        }

        Xresult = Repostry_CRS_Committee_Report_.FAPP(XCheck, XID, new Guid(ddlYears.SelectedValue), new Guid(DLType.SelectedValue), txtNumberBill.Text.Trim(), 
            txtTitle.Text.Trim(), txtMeeting_Venue.Text.Trim(), txtObjective_Of_the_Report.Text.Trim().Replace(Environment.NewLine, "<br />"),
            txtReport_Recommendations.Text.Trim().Replace(Environment.NewLine, "<br />"), "", Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            false, 0, XDate, XIDAdd, XUpdate, 0,
           Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"), XDate, true);
        if (Xresult == "IsExistsNumber")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "رقم التقرير مستخدم بالفعل !!! ";
            txtNumberBill.Focus();
            return;
        }
        else if (Xresult == "IsExistsName")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "هذا التقرير تم إضافتة مسبقاً ... ";
            txtTitle.Focus();
            return;
        }
        else if (Xresult == "IsSuccess")
            Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue);
    }

    protected void LB_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void LBUplode_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FUpludeImg();
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    string ImgArt;
    private void FUpludeImg()
    {
        if (FUImages.HasFiles)
        {
            string XResult = string.Empty; int XID_Order = 0;
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            DateTime XDateGetCurrentTime = ClassSaddam.GetCurrentTime();
            foreach (HttpPostedFile uploadedFile in FUImages.PostedFiles)
            {
                
                string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
                string ext = Path.GetExtension(FUImages.PostedFile.FileName);
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
                    lblMessageWarning.Text = "المسموح فقط ملفات من نوع ( " + string.Join(",", validFileTypes) + " ) ";
                    return;
                }
                else
                {
                    FAddFolder_ToDay("Committee", XDateGetCurrentTime.ToString("yyyy_MM_dd"), XDateGetCurrentTime.ToString("MM"), XDateGetCurrentTime.ToString("yyyy"));
                    // ReSize Img
                    Stream strm = uploadedFile.InputStream;
                    System.Drawing.Image im = System.Drawing.Image.FromStream(uploadedFile.InputStream);
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

                        string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
                        string XDate = "BerArn" + "/" + XDateGetCurrentTime.ToString("yyyy") + "/" + XDateGetCurrentTime.ToString("MM") + "/" + XDateGetCurrentTime.ToString("yyyy_MM_dd");
                        string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/CRS/Committee/" + XDate + "/"), XRandom + ".png");
                        thumbImg.Save(theFileName, image.RawFormat);
                        ImgArt = "ImgSystem/CRS/Committee/" + XDate + "/" + XRandom + ".png";

                        XID_Order = Repostry_CRS_Images_.FGetCount("GetLast", 1, new Guid(HFID.Value), string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1;
                        XResult = Repostry_CRS_Images_.FAPP("Add", Guid.NewGuid(), new Guid(HFID.Value), ImgArt, XID_Order, XIDAdd, 0, XDateGetCurrentTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
                    }
                }
            }
            if (XResult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم رفع الصور بنجاح ... ";
                FGetImages();
            }
        }
    }

    private void FGetImages()
    {
        DataTable dt = new DataTable();
        dt = Repostry_CRS_Images_.FGetDataInDataTable("GetAllByReport", 50, new Guid(HFID.Value), string.Empty, string.Empty, string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTImages.DataSource = dt;
            RPTImages.DataBind();
            RPTImages.Visible = true;
            pnlNullImages.Visible = false;
        }
        else
        {
            RPTImages.Visible = false;
            pnlNullImages.Visible = true;
        }
    }

    private void FAddFolder_ToDay(string XFolder, string Xdate, string XMounth, string XYears)
    {
        string XNameServer = "BerArn";
        string XHost = Server.MapPath("~/ImgSystem/CRS/" + XFolder + "/" + XNameServer);
        if (!Directory.Exists(XHost))
            Directory.CreateDirectory(XHost);
        string folderYears = Server.MapPath("~/ImgSystem/CRS/" + XFolder + "/" + XNameServer + "/" + XYears);
        string folderMounth = Server.MapPath("~/ImgSystem/CRS/" + XFolder + "/" + XNameServer + "/" + XYears + "/" + XMounth);
        string folderDay = Server.MapPath("~/ImgSystem/CRS/" + XFolder + "/" + XNameServer + "/" + XYears + "/" + XMounth + "/" + Xdate);
        if (!Directory.Exists(folderYears))
        {
            Directory.CreateDirectory(folderYears);
            Directory.CreateDirectory(folderMounth);
            Directory.CreateDirectory(folderDay);
        }
        else if (Directory.Exists(folderYears))
        {
            if (!Directory.Exists(folderMounth))
            {
                Directory.CreateDirectory(folderMounth);
                Directory.CreateDirectory(folderDay);
            }
            else if (Directory.Exists(folderMounth))
            {
                if (!Directory.Exists(folderDay))
                    Directory.CreateDirectory(folderDay);
            }
        }
    }

    protected void LBDeleteImage_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Guid _XID = new Guid(Convert.ToString((((LinkButton)sender).CommandArgument)).ToString());
            string XResult = Repostry_CRS_Images_.FAPP("Delete", _XID, Guid.Empty, string.Empty, 0, 0, Test_Saddam.FGetIDUsiq(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
            if (XResult == "IsSuccess")
            {
                DeleteFile("/" + Convert.ToString((((LinkButton)sender).CommandName)).ToString());
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGetImages();
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

    private void DeleteFile(string XPath)
    {
        FileInfo fileInfo = new FileInfo(MapPath(XPath));
        fileInfo.Delete();
    }

    protected void LBCommittee_Members_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            string XResult = string.Empty;
            XResult = Repostry_CRS_Committee_Members_.FAPP("Add", Guid.NewGuid(), new Guid(HFID.Value),
                Convert.ToInt32(DLCommittee_Members.SelectedValue), CBViewAdmin_Allow.Checked, XIDAdd,
                XDate, txtAdjective.Text.Trim(), Convert.ToInt32(txtOrder.Text.Trim()), XIDAdd, 0, 0, XDate, true);
            if (XResult == "IsExists")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblMessageWarning.Text = "تم إضافة العضو مسبقاً ... ";
                return;
            }
            else if (XResult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                DLCommittee_Members.SelectedValue = null;
                txtAdjective.Text = string.Empty;
                FGetLastBillCommittee_Members();
                FGetCommittee_Members();
            }
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FGetCommittee_Members()
    {
        RPTCommittee_Members.DataBind();
        DataTable dt = new DataTable();
        dt = Repostry_CRS_Committee_Members_.FGetDataInDataTable("GetAllByReport", 50, new Guid(HFID.Value), string.Empty, string.Empty, string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTCommittee_Members.DataSource = dt;
            RPTCommittee_Members.DataBind();
            lblCount.Text = dt.Rows.Count.ToString();
            pnlDataCommittee_Members.Visible = true;
            pnlNullCommittee_Members.Visible = false;
        }
        else
        {
            pnlDataCommittee_Members.Visible = false;
            pnlNullCommittee_Members.Visible = true;
        }
    }

    protected void LBDeleteCommittee_Members_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Guid _XID = new Guid(Convert.ToString((((LinkButton)sender).CommandArgument)).ToString());
            string XResult = Repostry_CRS_Committee_Members_.FAPP("Delete", _XID, Guid.Empty, 0, false, 0,
                string.Empty, string.Empty, 0, 0, 0, Test_Saddam.FGetIDUsiq(),
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
            if (XResult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGetCommittee_Members();
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetLastBill();
    }

}