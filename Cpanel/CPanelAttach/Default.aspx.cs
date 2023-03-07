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

public partial class Cpanel_CPanelAttach_Default : System.Web.UI.Page
{
    string IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        try
        {
            ClassAdmin_Arn CA = new ClassAdmin_Arn();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dtViewProfil = new DataTable();
            dtViewProfil = CA.BArnAdminGetByIDUniq();
            if (dtViewProfil.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                    IDMessageWarning.Visible = false;
                else
                    IDMessageWarning.Visible = true;
            }
        }
        catch (Exception)
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Threading.Thread.Sleep(100);
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
                string XURL = ClassEncryptPassword.Decrypt(dt.Rows[0]["_Url_"].ToString(), CLS_Key.FGetKeyUrl());
                string XUser = ClassEncryptPassword.Decrypt(dt.Rows[0]["_UserName_"].ToString(), CLS_Key.FGetKeyUser());
                string XPass = ClassEncryptPassword.Decrypt(dt.Rows[0]["_Pass_Word_"].ToString(), CLS_Key.FGetKeyPass());

                ClassAPI_SMS api = new ClassAPI_SMS();
                lblAuthentication.Text = api._Authentication_(XURL, XUser, XPass);
                lblBlance.Text = api._Get_Balance_(XURL, XUser, XPass);
                lblUser.Text = api._Get_Sender_Names_(XURL, XUser, XPass);
            }
        }
        catch (Exception)
        {
            return;
        }
    }

}