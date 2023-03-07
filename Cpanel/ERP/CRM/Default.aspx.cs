using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_Default : System.Web.UI.Page
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
            Response.Redirect("~/Cpanel/LogOut.aspx");
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
            Response.Redirect("~/Cpanel/LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FCRM_Remamber_Manage();
            FCRM_Company_Manage();
        }
    }

    private void FCRM_Remamber_Manage()
    {
        Model_Remamber_ MR = new Model_Remamber_();
        MR.IDCheck = "GetByRemamber";
        MR.ID_Item = Guid.Empty;
        MR.ID_Company = Guid.Empty;
        MR.Start_Date = ClassSaddam.GetCurrentTime().AddDays(-1).ToString("yyyy-MM-dd");
        MR.End_Date = ClassSaddam.GetCurrentTime().AddDays(5).ToString("yyyy-MM-dd");
        MR.Remamber_Date = string.Empty;
        MR.Is_Active = true;
        MR.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_Remamber_ RR = new Repostry_Remamber_();
        dt = RR.BCRM_Remamber_Manage(MR);
        if (dt.Rows.Count > 0)
        {
            RPT_Remamber_.DataSource = dt;
            RPT_Remamber_.DataBind();
            pnlData.Visible = true;
        }
        else
            pnlData.Visible = false;
    }

    private void FCRM_Company_Manage()
    {
        Model_Company_ MC = new Model_Company_();
        MC.IDCheck = "GetCount";
        MC.ID_Item = Guid.Empty;
        MC.Company_Name = string.Empty;
        MC.Is_Active = false;
        MC.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_Company_ RC = new Repostry_Company_();
        dt = RC.BCRM_Company_Manage(MC);
        if (dt.Rows.Count > 0)
            lblCountCompany.Text = dt.Rows[0]["Count_Comp"].ToString();
    }

    protected void LBDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Guid IDItem = new Guid((((LinkButton)sender).CommandArgument));
            Model_Remamber_ MR = new Model_Remamber_()
            {
                IDCheck = "Delete",
                ID_Item = IDItem,
                ID_Company = Guid.Empty,
                Remamber_Date = string.Empty,
                Remamber_Desc = string.Empty,
                Is_Active = false,
                CreatedDate = string.Empty,
                CreatedBy = 0,
                ModifiedBy = 0,
                ModifiedDate = string.Empty,
                Is_Delete = true
            };

            Repostry_Remamber_ RR = new Repostry_Remamber_();
            string Xresult = RR.FCRM_Remamber_Add(MR);
            if (Xresult == "IsSuccessDelete")
                FCRM_Remamber_Manage();
        }
        catch (Exception)
        {
            return;
        }
    }

}