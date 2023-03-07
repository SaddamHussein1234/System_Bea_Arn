using Library_CLS_Arn.DMS;
using Library_CLS_Arn.DMS.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_DMS_OutComingGeneral_PageAttachments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Type"] == "Association")
                HFIDStore.Value = Class_Identity_.FGetIdentityAssociation();
            else if (Request.QueryString["Type"] == "Institute")
                HFIDStore.Value = Class_Identity_.FGetIdentityInstitute();
            else
                Response.Redirect("../");
            Repostry_DMS_OutComing_General_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), Guid.Empty, DLOutComing_General);
            if (Request.QueryString["ID"] != null)
            {
                DLOutComing_General.SelectedValue = Request.QueryString["ID"];
                FGetByIDOutComing_General();
                FGetDate();
            }
        }
    }

    private void FGetDate()
    {
        DataTable dt = new DataTable();
        dt = Repostry_DMS_OutComing_General_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(DLOutComing_General.SelectedValue), new Guid(HFIDStore.Value),
            Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            ID_Edit_.HRef = "PageAdd.aspx?ID=" + dt.Rows[0]["_ID_Item_"].ToString(); ID_Edit_.Visible = true;
            HFIDYears.Value = dt.Rows[0]["_ID_Year_"].ToString();
            HFIDNumber.Value = dt.Rows[0]["_Number_File_"].ToString();
        }
    }

    private void FGetByIDOutComing_General()
    {
        DataTable dt = new DataTable();
        dt = Repostry_DMS_OutComing_General_Attachments_.FGetDataInDataTable("GetByIDOutComing_General", 15, new Guid(DLOutComing_General.SelectedValue), new Guid(HFIDStore.Value), string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTFiles.DataSource = dt;
            RPTFiles.DataBind();
            lblCount.Text = dt.Rows.Count.ToString();
            IDTable.Visible = true;
            pnlNull.Visible = false;
        }
        else
        {
            IDTable.Visible = false;
            pnlNull.Visible = true;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void DLOutComing_General_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetByIDOutComing_General(); FGetDate();
    }

    protected void LBImage_Click(object sender, EventArgs e)
    {
        try
        {
            FChackFile();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FChackFile()
    {
        if (FUFiles.HasFile)//xlsx
        {
            int fileSize = FUFiles.PostedFile.ContentLength;
            if (fileSize < 10485760)
            {
                string[] validFileTypes = { "pdf", "PDF" };
                string ext = Path.GetExtension(FUFiles.PostedFile.FileName);
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
                    lblWarning.Text = "المسموح فقط ملفات من نوع ( " + string.Join(",", validFileTypes) + " ) ";
                    return;
                }
                else
                    FUpimg(FUFiles);
            }
            else
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = " يجب أن يكون حجم الملف أقل من 10 m";
                return;
            }
        }
    }

    string _TheFile = string.Empty;
    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            DateTime XDateGetCurrentTime = ClassSaddam.GetCurrentTime();
            FAddFolder_ToDay("Out_General", XDateGetCurrentTime.ToString("yyyy_MM_dd"), XDateGetCurrentTime.ToString("MM"), XDateGetCurrentTime.ToString("yyyy"));

            string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
            string fileExtension = Path.GetExtension(upl.PostedFile.FileName);
            string XDate = "BerArn" + "/" + XDateGetCurrentTime.ToString("yyyy") + "/" + XDateGetCurrentTime.ToString("MM") + "/" + XDateGetCurrentTime.ToString("yyyy_MM_dd");
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FilesDMS/Out_General/" + XDate + "/"), XRandom + fileExtension);
            upl.SaveAs(theFileName);
            _TheFile = "ImgSystem/FilesDMS/Out_General/" + XDate + "/" + XRandom + fileExtension;
            FAPP(fileExtension, FUFiles.PostedFile.ContentLength.ToString());
        }
    }

    private void FAddFolder_ToDay(string XFolder, string Xdate, string XMounth, string XYears)
    {
        string XNameServer = "BerArn";
        string XHost = Server.MapPath("~/ImgSystem/FilesDMS/" + XFolder + "/" + XNameServer);
        if (!Directory.Exists(XHost))
            Directory.CreateDirectory(XHost);
        string folderYears = Server.MapPath("~/ImgSystem/FilesDMS/" + XFolder + "/" + XNameServer + "/" + XYears);
        string folderMounth = Server.MapPath("~/ImgSystem/FilesDMS/" + XFolder + "/" + XNameServer + "/" + XYears + "/" + XMounth);
        string folderDay = Server.MapPath("~/ImgSystem/FilesDMS/" + XFolder + "/" + XNameServer + "/" + XYears + "/" + XMounth + "/" + Xdate);
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

    private void FAPP(string XType_File, string XSize_File)
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        string Xresult = Repostry_DMS_OutComing_General_Attachments_.FAPP("Add", Guid.NewGuid(), new Guid(HFIDStore.Value),
            new Guid(DLOutComing_General.SelectedValue), txt_Title.Text.Trim(), _TheFile, XType_File, XSize_File, Test_Saddam.FGetIDUsiq(), XDate, true);
        if (Xresult == "IsExists")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة الملف مسبقاً ... ";
            txt_Title.Focus();
            return;
        }
        else if (Xresult == "IsSuccess")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.InnerText = "تم رفع الملف بنجاح ... ";           
            FGetByIDOutComing_General();
        }
    }

    protected void LBDelete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Comp_ID = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            DeleteFile("/" + Repostry_DMS_OutComing_General_Attachments_.FGetByColum("GetByIDUniq", 1, new Guid(Comp_ID), Guid.Empty, string.Empty, true, "_Src_"));
            string Xresult = Repostry_DMS_OutComing_General_Attachments_.FAPP("Delete", new Guid(Comp_ID), new Guid(HFIDStore.Value),
                Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty, false);
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.InnerHtml = "تم حذف الملف بنجاح ,,, ";
                FGetByIDOutComing_General();
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void DeleteFile(string XPath)
    {
        FileInfo fileInfo = new FileInfo(MapPath(XPath));
        fileInfo.Delete();
    }

    protected void LB_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void LBView_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageView.aspx?ID=" + HFIDNumber.Value + "&IDYears=" + HFIDYears.Value + "&Type=" + Request.QueryString["Type"]);
    }

}