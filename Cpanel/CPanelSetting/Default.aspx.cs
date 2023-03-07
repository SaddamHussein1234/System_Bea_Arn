using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelSetting_Default : System.Web.UI.Page
{
    string IDUser, IDUniq, UserN, UserCard, IDAdmin2;
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

            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_"];
            UserN = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUser;  // رقم المستخدم
            CookeUser = Request.Cookies["__UserUniqAdmin_True_"];
            IDAdmin2 = ClassSaddam.UnprotectPassword(CookeUser["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            CheckAccountAdmin();
        }
        catch
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        try
        {
            ClassAdmin_Arn CA = new ClassAdmin_Arn();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dtViewProfil = new DataTable();
            dtViewProfil = CA.BArnAdminGetByIDUniq();
            if ((IDUser == IDAdmin2) && (IDUser == dtViewProfil.Rows[0]["ID_Item"].ToString()) && (IDUniq == dtViewProfil.Rows[0]["IDUniqUser"].ToString()) && (UserN == dtViewProfil.Rows[0]["User_Name_"].ToString()))
            {
                if (dtViewProfil.Rows.Count > 0)
                {
                    bool[] A = new bool[150];

                    // الإعدادات الرئيسية
                    A[71] = Convert.ToBoolean(dtViewProfil.Rows[0]["A71"]);
                    if (A[71])
                        IDSetting.Visible = true;
                    else
                    {
                        IDSetting.Visible = true;
                        IDSetting.Attributes.Add("class", "isDisabled");
                        IDSetting.Title = "ليس لديك صلاحية ";
                        IDSetting.HRef = string.Empty;

                        faSetting.Attributes.Add("class", "fa fa-warning");
                    }

                    // المجموعات
                    A[5] = Convert.ToBoolean(dtViewProfil.Rows[0]["A5"]);
                    A[6] = Convert.ToBoolean(dtViewProfil.Rows[0]["A6"]);
                    if (A[5] && A[6]) { IDGroup.Visible = true; IDGroupAdmin.Visible = true; IDGroup.HRef = "PageGroup.aspx"; IDGroupAdmin.HRef = "PageGroupAdmin.aspx"; }
                    else if (A[5] && A[6] == false)
                    { IDGroup.Visible = true; IDGroup.HRef = "PageGroup.aspx"; IDGroupAdmin.Visible = true; IDGroupAdmin.HRef = "PageGroupAdmin.aspx"; }
                    else if (A[5] == false && A[6] == true)
                    { IDGroup.Visible = true; IDGroup.HRef = "PageGroupAdd.aspx"; }
                    else if (A[5] == false && A[6] == false)
                    {
                        IDGroup.Visible = true; IDGroup.Attributes.Add("class", "isDisabled"); IDGroup.Title = "ليس لديك صلاحية ";
                        IDGroup.HRef = string.Empty; faGroup.Attributes.Add("class", "fa fa-warning");

                        IDGroupAdmin.Visible = true; IDGroupAdmin.Attributes.Add("class", "isDisabled"); IDGroupAdmin.Title = "ليس لديك صلاحية ";
                        IDGroupAdmin.HRef = string.Empty; faGroup.Attributes.Add("class", "fa fa-warning");
                    }

                    //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsHide"]))
                    //{
                    //    IDGroup.Visible = true; IDGroupAdmin.Visible = true;
                    //}

                    // المستخدمين
                    A[7] = Convert.ToBoolean(dtViewProfil.Rows[0]["A7"]);
                    A[8] = Convert.ToBoolean(dtViewProfil.Rows[0]["A8"]);
                    if (A[7] && A[8]) { IDAdmin.Visible = true; IDAdmin.HRef = "PageAdmin.aspx"; }
                    else if (A[7] && A[8] == false)
                    { IDAdmin.Visible = true; IDAdmin.HRef = "PageAdmin.aspx"; }
                    else if (A[7] == false && A[8] == true)
                    { IDAdmin.Visible = true; IDAdmin.HRef = "PageAdminAdd.aspx"; }
                    else if (A[7] == false && A[8] == false)
                    {
                        IDAdmin.Visible = true; IDAdmin.Attributes.Add("class", "isDisabled"); IDAdmin.Title = "ليس لديك صلاحية ";
                        IDAdmin.HRef = string.Empty; faAdmin.Attributes.Add("class", "fa fa-warning");
                    }

                    if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                        IDMessageWarning.Visible = false;
                    else
                        IDMessageWarning.Visible = true;

                }
            }
            else
                Response.Redirect("/Cpanel/LogOut.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetCookie();
    }

}