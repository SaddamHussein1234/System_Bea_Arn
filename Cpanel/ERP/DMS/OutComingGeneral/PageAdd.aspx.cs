using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.DMS;
using Library_CLS_Arn.DMS.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_DMS_OutComingGeneral_PageAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HFID.Value = Guid.NewGuid().ToString();
            if (Request.QueryString["Type"] == "Association")
                HFIDStore.Value = Class_Identity_.FGetIdentityAssociation();
            else if (Request.QueryString["Type"] == "Institute")
                HFIDStore.Value = Class_Identity_.FGetIdentityInstitute();
            else
                Response.Redirect("../");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtNumber.Text = (Repostry_DMS_OutComing_General_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
            Repostry_DMS_Category_.FGetDropList(1, "_Ar", Guid.Empty, "Out_General", DLCategory);
            Repostry_DMS_Party_.FGetDropList(1, "_Ar", Guid.Empty, DLParty);
            Repostry_DMS_Party_Send_.FGetDropList(0, "_Ar", Guid.Empty, DLParty_Send);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDateAddSend.Text = txtDateAdd.Text;
            Repostry_DMS_Nature_.FGetDropList(1, "_Ar", Guid.Empty, DL_Nature);
            Repostry_DMS_Importance_.FGetDropList(1, "_Ar", Guid.Empty, DL_Importance);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLGeneral_Director);
            ClassAdmin_Arn.FGetModerEmp(DLDirector_Of_Personnel);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLCashier);
            ClassAdmin_Arn.FGetAmeenGeneral(DLSecretary_General);
            ClassAdmin_Arn.FGetNaeebMaglesAlEdarah(DLDeputy_Chairman_Of_The_Board);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLChairman_Of_Board_Of_Directors);
            txtNumber.Text = (Repostry_DMS_OutComing_General_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                 string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
            Repostry_Country_.FErp_Country_Manage(ddlCountry);
            ddlCountry.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            Repostry_Country_.FErp_Country_Manage(ddlCountrySend);
            ddlCountrySend.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            txt_Note.Text = "<p dir='rtl'></p>";
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
            dt = Repostry_DMS_OutComing_General_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(Request.QueryString["ID"]), new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                HFID.Value = dt.Rows[0]["_ID_Item_"].ToString();
                ddlYears.SelectedValue = dt.Rows[0]["_ID_Year_"].ToString();
                DLCategory.SelectedValue = dt.Rows[0]["_ID_Category_"].ToString();
                txtNumber.Text = dt.Rows[0]["_Number_File_"].ToString();
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Transaction_"]).ToString("yyyy-MM-dd");
                DLParty.SelectedValue = dt.Rows[0]["_ID_Party_"].ToString();
                DL_Nature.SelectedValue = dt.Rows[0]["_ID_Nature_"].ToString();
                DL_Importance.SelectedValue = dt.Rows[0]["_ID_Importance_"].ToString();
                txt_Title.Text = dt.Rows[0]["_The_Title_"].ToString();
                txt_Title_Attachments.Text = dt.Rows[0]["_The_Title_Attachments_"].ToString();
                txt_Note.Text = dt.Rows[0]["_The_Details_"].ToString();
                CBGeneral_Director.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_General_Director_"]);
                DLGeneral_Director.SelectedValue = dt.Rows[0]["_ID_General_Director_"].ToString();
                CBDirector_Of_Personnel.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Director_Of_Personnel_"]);
                DLDirector_Of_Personnel.SelectedValue = dt.Rows[0]["_ID_Director_Of_Personnel_"].ToString();
                CBCashier.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Cashier_"]);
                DLCashier.SelectedValue = dt.Rows[0]["_ID_Cashier_"].ToString();
                CBSecretary_General.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Secretary_General_"]);
                DLSecretary_General.SelectedValue = dt.Rows[0]["_ID_Secretary_General_"].ToString();
                CBDeputy_Chairman_Of_The_Board.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Deputy_Chairman_Of_The_Board_"]);
                DLDeputy_Chairman_Of_The_Board.SelectedValue = dt.Rows[0]["_ID_Deputy_Chairman_Of_The_Board_"].ToString();
                CBChairman_Of_Board_Of_Directors.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Chairman_Of_Board_Of_Directors_"]);
                DLChairman_Of_Board_Of_Directors.SelectedValue = dt.Rows[0]["_ID_Chairman_Of_Board_Of_Directors_"].ToString();
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
        string Xresult = Repostry_DMS_Party_.FAPP("Add", XID, Guid.Empty, DLType_Customer.SelectedValue, txtCompanyName.Text.Trim(),
                "-", Convert.ToInt64(Repostry_DMS_Party_.FGetLastRecord(Guid.Empty).ToString()), string.Empty, new Guid(ddlCountry.SelectedValue),
                string.Empty, string.Empty, ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com", 0, string.Empty, txtPhone_Number1.Text.Trim(),
                string.Empty, string.Empty, string.Empty, true, true, Test_Saddam.FGetIDUsiq(), 0, 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsSuccess")
        {
            Repostry_DMS_Party_.FGetDropList(1, "_Ar", Guid.Empty, DLParty);
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
                FAPP();
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

    private void FAPP()
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
            XCheck = "Edit"; XID = new Guid(Request.QueryString["ID"]); XUpdate = Test_Saddam.FGetIDUsiq();
        }
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        Xresult = Repostry_DMS_OutComing_General_.FAdd(XCheck, XID, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue), new Guid(DLCategory.SelectedValue), 
            Convert.ToInt64(txtNumber.Text.Trim()), txtDateAdd.Text.Trim(), txtDateAddSend.Text.Trim(), new Guid(DLParty.SelectedValue), new Guid(DLParty_Send.SelectedValue), 
            new Guid(DL_Nature.SelectedValue), new Guid(DL_Importance.SelectedValue), txt_Title.Text.Trim(), txt_Title_Attachments.Text.Trim(), txt_Note.Text.Trim(), 
            CBGeneral_Director.Checked, Convert.ToInt32(DLGeneral_Director.SelectedValue), CBDirector_Of_Personnel.Checked, Convert.ToInt32(DLDirector_Of_Personnel.SelectedValue), 
            CBCashier.Checked, Convert.ToInt32(DLCashier.SelectedValue), CBSecretary_General.Checked, Convert.ToInt32(DLSecretary_General.SelectedValue), 
            CBDeputy_Chairman_Of_The_Board.Checked, Convert.ToInt32(DLDeputy_Chairman_Of_The_Board.SelectedValue), CBChairman_Of_Board_Of_Directors.Checked, 
            Convert.ToInt32(DLChairman_Of_Board_Of_Directors.SelectedValue), 0, XIDAdd, XUpdate, 0, XDate, true);
        if (Xresult == "IsExistsNumber")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "رقم الخطاب مستخدم بالفعل !!! ";
            txtNumber.Focus();
            return;
        }
        else if (Xresult == "IsExistsName")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "هذا الخطاب تم إضافة مسبقاً ... ";
            txt_Title.Focus();
            return;
        }
        else if (Xresult == "IsSuccess")
            Response.Redirect("PageAttachments.aspx?ID=" + HFID.Value + "&Type=" + Request.QueryString["Type"]);
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
        txtNumber.Text = (Repostry_DMS_OutComing_General_.FGetCount("GetLastRecord", 1, Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                 string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1).ToString();
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "General_Director")
        {
            if (CBGeneral_Director.Checked)
                XResult = "display:block;";
            else if (CBGeneral_Director.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Director_Of_Personnel")
        {
            if (CBDirector_Of_Personnel.Checked)
                XResult = "display:block;";
            else if (CBDirector_Of_Personnel.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Cashier")
        {
            if (CBCashier.Checked)
                XResult = "display:block;";
            else if (CBCashier.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Secretary_General")
        {
            if (CBSecretary_General.Checked)
                XResult = "display:block;";
            else if (CBSecretary_General.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Deputy_Chairman_Of_The_Board")
        {
            if (CBDeputy_Chairman_Of_The_Board.Checked)
                XResult = "display:block;";
            else if (CBDeputy_Chairman_Of_The_Board.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Chairman_Of_Board_Of_Directors")
        {
            if (CBChairman_Of_Board_Of_Directors.Checked)
                XResult = "display:block;";
            else if (CBChairman_Of_Board_Of_Directors.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        return XResult;
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
        string Xresult = Repostry_DMS_Category_.FAPP("Add", XID, Guid.Empty, "Out_General", txt_Category_Ar.Text.Trim(), txt_Category_En.Text.Trim(),
                Convert.ToInt32(Repostry_DMS_Category_.FGetCount("GetLastRecord", 1, Guid.Empty, Guid.Empty, "Out_General",
                string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1), true, true, Test_Saddam.FGetIDUsiq(), 0, 0, 
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsSuccess")
        {
            Repostry_DMS_Category_.FGetDropList(1, "_Ar", Guid.Empty, "Out_General", DLCategory);
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
        string Xresult = Repostry_DMS_Party_Send_.FAPP("Add", XID, Guid.Empty, DLType_Customer_Send.SelectedValue, txtCompanyNameSend.Text.Trim(),
                "-", Convert.ToInt64(Repostry_DMS_Party_Send_.FGetLastRecord(Guid.Empty).ToString()), string.Empty, new Guid(ddlCountrySend.SelectedValue),
                string.Empty, string.Empty, ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com", 0, string.Empty, txtPhone_Number1_Send.Text.Trim(),
                string.Empty, string.Empty, string.Empty, true, true, Test_Saddam.FGetIDUsiq(), 0, 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsSuccess")
        {
            Repostry_DMS_Party_Send_.FGetDropList(0, "_Ar", Guid.Empty, DLParty_Send);
            DLParty_Send.SelectedValue = XID.ToString();
            HFPhoneSend.Value = txtPhone_Number1_Send.Text.Trim();
            lblPhoneSend.InnerHtml = txtPhone_Number1_Send.Text.Trim();
        }
    }

}