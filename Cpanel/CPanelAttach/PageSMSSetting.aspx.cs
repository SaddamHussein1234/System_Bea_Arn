using Library_CLS_Arn.ClassEntity.Attach.Models;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelAttach_PageSMSSetting : System.Web.UI.Page
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
            //bool A72;
            //A72 = Convert.ToBoolean(dtViewProfil.Rows[0]["A72"]);
            //if (A72 == false)
            //    Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Attach_Model_SMS_Setting_ AMSS = new Attach_Model_SMS_Setting_();
            AMSS.IDCheck = "GetByID";
            AMSS.ID_Item = new Guid("fc340a50-41ff-4c33-bdd7-4dfa1fdd1752");
            AMSS.Is_Active = true;
            AMSS.Is_Delete = false;
            DataTable dt = new DataTable();
            Attach_Repostry_SMS_Setting_ ARSS = new Attach_Repostry_SMS_Setting_();
            dt = ARSS.BAttach_SMS_Setting_Manage(AMSS);
            if (dt.Rows.Count > 0)
            {
                txt_Url.Text = ClassEncryptPassword.Decrypt(dt.Rows[0]["_Url_"].ToString(), CLS_Key.FGetKeyUrl());
                txt_User_Name.Text = ClassEncryptPassword.Decrypt(dt.Rows[0]["_UserName_"].ToString(), CLS_Key.FGetKeyUser());
                txt_Pass.Text = ClassEncryptPassword.Decrypt(dt.Rows[0]["_Pass_Word_"].ToString(), CLS_Key.FGetKeyPass());
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Active_"]);

                CBActiveSetting.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_Setting_"]);
                CBActiveSite.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_Site_"]);
                CBActiveSocialSearch.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_SocialSearch_"]);
                CBActiveWSM.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_WSM_"]);
                CBActiveEOS.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_EOS_"]);
                CBActiveZakat.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_Zakat_"]);
                CBActiveGeneralAssembly.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_GeneralAssembly_"]);
                CBActiveHR.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_HR_"]);
                CBActiveCRM.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_CRM_"]);
                CBActiveOM.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Send_OM_"]);
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
            FAttach_SMS_Setting_Add();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FAttach_SMS_Setting_Add()
    {
        Attach_Model_SMS_Setting_ AMSS = new Attach_Model_SMS_Setting_()
        {
            IDCheck = "Edit",
            ID_Item = new Guid("fc340a50-41ff-4c33-bdd7-4dfa1fdd1752"),
            Url_ = ClassEncryptPassword.Encrypt(txt_Url.Text.Trim(), CLS_Key.FGetKeyUrl()),
            UserName = ClassEncryptPassword.Encrypt(txt_User_Name.Text.Trim(), CLS_Key.FGetKeyUser()),
            Pass_Word = ClassEncryptPassword.Encrypt(txt_Pass.Text.Trim(), CLS_Key.FGetKeyPass()),

            Is_Send_Setting = Convert.ToBoolean(CBActiveSetting.Checked),
            Is_Send_Site = Convert.ToBoolean(CBActiveSite.Checked),
            Is_Send_SocialSearch = Convert.ToBoolean(CBActiveSocialSearch.Checked),
            Is_Send_WSM = Convert.ToBoolean(CBActiveWSM.Checked),
            Is_Send_EOS = Convert.ToBoolean(CBActiveEOS.Checked),
            Is_Send_Zakat = Convert.ToBoolean(CBActiveZakat.Checked),
            Is_Send_GeneralAssembly = Convert.ToBoolean(CBActiveGeneralAssembly.Checked),
            Is_Send_HR = Convert.ToBoolean(CBActiveHR.Checked),
            Is_Send_CRM = Convert.ToBoolean(CBActiveCRM.Checked),
            Is_Send_OM = Convert.ToBoolean(CBActiveOM.Checked),
            Is_Active = Convert.ToBoolean(CBActive.Checked),

            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = Test_Saddam.FGetIDUsiq(),
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            DeleteBy = 0,
            Is_Delete = false
        };

        Attach_Repostry_SMS_Setting_ ARSS = new Attach_Repostry_SMS_Setting_();
        string Xresult = ARSS.FAttach_SMS_Setting_Add(AMSS);
        if (Xresult == "IsExistsEdit")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccessEdit")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            FGetData();
        }
    }

}