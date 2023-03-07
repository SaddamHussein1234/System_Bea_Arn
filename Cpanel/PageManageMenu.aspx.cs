using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageMenu : System.Web.UI.Page
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

            // القُرى
            A[31] = Convert.ToBoolean(dtViewProfil.Rows[0]["A31"]);
            A[77] = Convert.ToBoolean(dtViewProfil.Rows[0]["A77"]);
            if (A[31] == false && A[77] == false)
            {
                IDManageVillage.Visible = false;
            }
            else if (A[31] == true || A[77] == true)
            {
                IDManageVillage.Visible = true;
            }

            // حالة المستفيد
            A[32] = Convert.ToBoolean(dtViewProfil.Rows[0]["A32"]);
            A[78] = Convert.ToBoolean(dtViewProfil.Rows[0]["A78"]);
            if (A[32] == false && A[78] == false)
            {
                IDManageBeneficiaryStatus.Visible = false;
            }
            else if (A[32] == true || A[78] == true)
            {
                IDManageBeneficiaryStatus.Visible = true;
            }

            // نوع المسكن
            A[33] = Convert.ToBoolean(dtViewProfil.Rows[0]["A33"]);
            A[79] = Convert.ToBoolean(dtViewProfil.Rows[0]["A79"]);
            if (A[33] == false && A[79] == false)
            {
                IDManageTypeOfDwelling.Visible = false;
            }
            else if (A[33] == true || A[79] == true)
            {
                IDManageTypeOfDwelling.Visible = true;
            }

            // الدخل الشهري
            A[34] = Convert.ToBoolean(dtViewProfil.Rows[0]["A34"]);
            A[80] = Convert.ToBoolean(dtViewProfil.Rows[0]["A80"]);
            if (A[34] == false && A[80] == false)
            {
                IDManageMonthlyIncome.Visible = false;
            }
            else if (A[34] == true || A[80] == true)
            {
                IDManageMonthlyIncome.Visible = true;
            }

            // حالة المسكن
            A[35] = Convert.ToBoolean(dtViewProfil.Rows[0]["A35"]);
            A[81] = Convert.ToBoolean(dtViewProfil.Rows[0]["A81"]);
            if (A[35] == false && A[81] == false)
            {
                IDManageHousingStatus.Visible = false;
            }
            else if (A[35] == true || A[81] == true)
            {
                IDManageHousingStatus.Visible = true;
            }

            // نوع الدعم
            A[36] = Convert.ToBoolean(dtViewProfil.Rows[0]["A36"]);
            A[82] = Convert.ToBoolean(dtViewProfil.Rows[0]["A82"]);
            if (A[36] == false && A[82] == false)
            {
                IDManageSupportType.Visible = false;
            }
            else if (A[36] == true || A[82] == true)
            {
                IDManageSupportType.Visible = true;
            }

            // قرابة عائلة المستفيد
            A[37] = Convert.ToBoolean(dtViewProfil.Rows[0]["A37"]);
            A[83] = Convert.ToBoolean(dtViewProfil.Rows[0]["A83"]);
            if (A[37] == false && A[83] == false)
            {
                IDManageBeneficiaryFamily.Visible = false;
            }
            else if (A[37] == true || A[83] == true)
            {
                IDManageBeneficiaryFamily.Visible = true;
            }

            // صلة قرابة المستفيد
            A[38] = Convert.ToBoolean(dtViewProfil.Rows[0]["A38"]);
            A[84] = Convert.ToBoolean(dtViewProfil.Rows[0]["A84"]);
            if (A[38] == false && A[84] == false)
            {
                IDManageBeneficiaryRelationship.Visible = false;
            }
            else if (A[38] == true || A[84] == true)
            {
                IDManageBeneficiaryRelationship.Visible = true;
            }

            // المبادرات والداعمين
            A[132] = Convert.ToBoolean(dtViewProfil.Rows[0]["A132"]);
            A[133] = Convert.ToBoolean(dtViewProfil.Rows[0]["A133"]);
            if (A[132] == false && A[133] == false)
            {
                IDInitiatives.Visible = false;
            }
            else if (A[132] == true || A[133] == true)
            {
                IDInitiatives.Visible = true;
            }

            if (A[31] == false && A[32] == false && A[33] == false && A[34] == false && A[35] == false && A[36] == false && A[37] == false && A[38] == false && A[77] == false && A[78] == false && A[79] == false && A[80] == false && A[81] == false && A[82] == false && A[83] == false && A[84] == false && A[132] == false && A[133] == false)
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