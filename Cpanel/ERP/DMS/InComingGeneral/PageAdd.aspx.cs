using Library_CLS_Arn.DMS.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_DMS_InComingGeneral_PageAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HFID.Value = Guid.NewGuid().ToString();
            HFIDStore.Value = Guid.Empty.ToString();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtNumber.Text = (Repostry_DMS_InComing_General_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
            Repostry_DMS_Category_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), "In_General", DLCategory);
            Repostry_DMS_Party_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), DLParty);
            Repostry_DMS_Party_Send_.FGetDropList(0, "_Ar", new Guid(HFIDStore.Value), DLParty_Send);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDateAddSend.Text = txtDateAdd.Text;
            Repostry_DMS_Nature_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), DL_Nature);
            Repostry_DMS_Importance_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), DL_Importance);
            Repostry_DMS_Replay_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), "", DL_Replay);
            Repostry_DMS_Achievement_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), "", DL_Achievement);
            txtNumber.Text = (Repostry_DMS_InComing_General_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                 string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
            Repostry_Country_.FErp_Country_Manage(ddlCountry);
            ddlCountry.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            Repostry_Country_.FErp_Country_Manage(ddlCountrySend);
            ddlCountrySend.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            Session[Request.QueryString["ID"] + "PDF"] = "---";
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
            dt = Repostry_DMS_InComing_General_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(Request.QueryString["ID"]), new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                HFID.Value = dt.Rows[0]["_ID_Item_"].ToString();
                ddlYears.SelectedValue = dt.Rows[0]["_ID_Year_"].ToString();
                DLCategory.SelectedValue = dt.Rows[0]["_ID_Category_"].ToString();
                txtNumber.Text = dt.Rows[0]["_Number_File_"].ToString();
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Transaction_"]).ToString("yyyy-MM-dd");
                txtDateAddSend.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Cuff_Transaction_"]).ToString("yyyy-MM-dd");
                DLParty.SelectedValue = dt.Rows[0]["_ID_Party_"].ToString();
                DL_Nature.SelectedValue = dt.Rows[0]["_ID_Nature_"].ToString();
                DL_Importance.SelectedValue = dt.Rows[0]["_ID_Importance_"].ToString();
                DL_Replay.SelectedValue = dt.Rows[0]["_ID_Replay_"].ToString();
                DL_Achievement.SelectedValue = dt.Rows[0]["_ID_Achievement_"].ToString();
                txt_Title.Text = dt.Rows[0]["_The_Title_"].ToString();
                txt_Title_Attachments.Text = dt.Rows[0]["_The_Title_Attachments_"].ToString();
                txt_Note.Text = dt.Rows[0]["_The_Details_"].ToString().Replace("<br />", Environment.NewLine);
                Session[Request.QueryString["ID"] + "PDF"] = dt.Rows[0]["_Src_"].ToString();
                RFVUpload.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void LBSave2_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FAdd();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FAdd()
    {
        Guid XID = Guid.NewGuid();
        string Xresult = Repostry_DMS_Party_.FAPP("Add", XID, new Guid(HFIDStore.Value), DLType_Customer.SelectedValue, txtCompanyName.Text.Trim(),
                "-", Convert.ToInt64(Repostry_DMS_Party_.FGetLastRecord(new Guid(HFIDStore.Value)).ToString()), string.Empty, new Guid(ddlCountry.SelectedValue),
                string.Empty, string.Empty, ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com", 0, string.Empty, txtPhone_Number1.Text.Trim(),
                string.Empty, string.Empty, string.Empty, true, true, Test_Saddam.FGetIDUsiq(), 0, 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsSuccess")
        {
            Repostry_DMS_Party_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), DLParty);
            DLParty.SelectedValue = XID.ToString();
            HFPhone.Value = txtPhone_Number1.Text.Trim();
            lblPhone.InnerHtml = txtPhone_Number1.Text.Trim();
        }
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FChackFile();
            else
            {
                lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            lblWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
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
        else
            FAPP("EditOutFile", string.Empty, string.Empty);
    }

    protected void FUpimg(FileUpload upl)
    {
        string fileExtension = "0";
        if (upl.HasFile)
        {
            FAddFolder_ToDay("In_General", ClassSaddam.GetCurrentTime().ToString("yyyy_MM_dd"), ClassSaddam.GetCurrentTime().ToString("MM"), ClassSaddam.GetCurrentTime().ToString("yyyy"));

            string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
            fileExtension = Path.GetExtension(upl.PostedFile.FileName);
            string XDate = "BerArn" + "/" + ClassSaddam.GetCurrentTime().ToString("yyyy") + "/" + ClassSaddam.GetCurrentTime().ToString("MM") + "/" + ClassSaddam.GetCurrentTime().ToString("yyyy_MM_dd");
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FilesDMS/In_General/" + XDate + "/"), XRandom + fileExtension);
            upl.SaveAs(theFileName);
            if (Request.QueryString["ID"] != null)
                DeleteFile("/" + Session[Request.QueryString["ID"] + "PDF"].ToString());
            Session[Request.QueryString["ID"] + "PDF"] = "ImgSystem/FilesDMS/In_General/" + XDate + "/" + XRandom + fileExtension;
            FAPP("Edit", fileExtension, FUFiles.PostedFile.ContentLength.ToString());
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
    
    private void DeleteFile(string XPath)
    {
        FileInfo fileInfo = new FileInfo(MapPath(XPath));
        fileInfo.Delete();
    }

    private void FAPP(string XEditType, string XType_File, string XSize_File)
    {
        string XCheck = string.Empty, Xresult = string.Empty;
        Guid XID = Guid.Empty; Guid XID_Marketed = Guid.Empty;
        int XIDAdd = 0, XUpdate = 0;
        if (Request.QueryString["ID"] == null)
        {
            XCheck = "Add"; XID = new Guid(HFID.Value); XIDAdd = Test_Saddam.FGetIDUsiq();
        }
        if (Request.QueryString["ID"] != null)
        {
            XCheck = XEditType; XID = new Guid(Request.QueryString["ID"]); XUpdate = Test_Saddam.FGetIDUsiq();
        }
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        Xresult = Repostry_DMS_InComing_General_.FAdd(XCheck, XID, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue), new Guid(DLCategory.SelectedValue),
            Convert.ToInt64(txtNumber.Text.Trim()), txtDateAdd.Text.Trim(), txtDateAddSend.Text.Trim(), new Guid(DLParty.SelectedValue), new Guid(DLParty_Send.SelectedValue),
            new Guid(DL_Nature.SelectedValue), new Guid(DL_Importance.SelectedValue), new Guid(DL_Replay.SelectedValue), new Guid(DL_Achievement.SelectedValue),
            txt_Title.Text.Trim(), txt_Title_Attachments.Text.Trim(), txt_Note.Text.Trim().Replace(Environment.NewLine, "<br />"), Session[Request.QueryString["ID"] + "PDF"].ToString(),
            XType_File, XSize_File, 0, XIDAdd, XUpdate, 0, XDate, true);
        if (Xresult == "IsExistsNumber")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "رقم الوارد مستخدم بالفعل !!! ";
            txtNumber.Focus();
            return;
        }
        else if (Xresult == "IsExistsName")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "هذا الوارد تم إضافة مسبقاً ... ";
            txt_Title.Focus();
            return;
        }
        else if (Xresult == "IsSuccess")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
        }
    }

    protected void LB_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
        txtNumber.Text = (Repostry_DMS_InComing_General_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                 string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
    }

    protected void LBVGCategory_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FAddCategory();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FAddCategory()
    {
        Guid XID = Guid.NewGuid();
        string Xresult = Repostry_DMS_Category_.FAPP("Add", XID, new Guid(HFIDStore.Value), "In_General", txt_Category_Ar.Text.Trim(), txt_Category_En.Text.Trim(),
                Convert.ToInt32(Repostry_DMS_Category_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(HFIDStore.Value), "In_General",
                string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1), true, true, Test_Saddam.FGetIDUsiq(), 0, 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsSuccess")
        {
            Repostry_DMS_Category_.FGetDropList(1, "_Ar", new Guid(HFIDStore.Value), "In_General", DLCategory);
            DLCategory.SelectedValue = XID.ToString();
        }
    }

    protected void LBSend_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FAddSend();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FAddSend()
    {
        Guid XID = Guid.NewGuid();
        string Xresult = Repostry_DMS_Party_Send_.FAPP("Add", XID, new Guid(HFIDStore.Value), DLType_Customer_Send.SelectedValue, txtCompanyNameSend.Text.Trim(),
                "-", Convert.ToInt64(Repostry_DMS_Party_Send_.FGetLastRecord(new Guid(HFIDStore.Value)).ToString()), string.Empty, new Guid(ddlCountrySend.SelectedValue),
                string.Empty, string.Empty, ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com", 0, string.Empty, txtPhone_Number1_Send.Text.Trim(),
                string.Empty, string.Empty, string.Empty, true, true, Test_Saddam.FGetIDUsiq(), 0, 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsSuccess")
        {
            Repostry_DMS_Party_Send_.FGetDropList(0, "_Ar", new Guid(HFIDStore.Value), DLParty_Send);
            DLParty_Send.SelectedValue = XID.ToString();
            HFPhoneSend.Value = txtPhone_Number1_Send.Text.Trim();
            lblPhoneSend.InnerHtml = txtPhone_Number1_Send.Text.Trim();
        }
    }

}