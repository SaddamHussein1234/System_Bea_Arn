using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageStatistic : System.Web.UI.Page
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

            CheckAccountAdmin();
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        ClassAdmin_Arn CA = new ClassAdmin_Arn();
        CA._IDUniq = IDUniq;
        CA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool[] A = new bool[150];

            A[39] = Convert.ToBoolean(dtViewProfil.Rows[0]["A39"]);
            if (A[39])
            {
                IDBeneficiaryBySearchComprehensive.Visible = true;
                IDBeneficiaryBySearchBoysComprehensive.Visible = true;
                IDBeneficiaryStatistic.Visible = true;
                IDBeneficiarySourceOfIncome.Visible = true;
                IDBeneficiaryFamliyCases.Visible = true;
                IDBeneficiaryAccommodationType.Visible = true;
                IDBeneficiaryHousingStatus.Visible = true;
                IDBeneficiaryOrphans.Visible = true;
                IDBeneficiaryChildren.Visible = true;
                IDEducationalSituations.Visible = true;
                IDMaleAndFemale.Visible = true; IDBeneficiaryTheElderly.Visible = true; IDBeneficiaryYoung.Visible = true;
            }
            if (A[39] == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCookie();
        }
    }

}