using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CPBeneficiary_MPBeneficiary : System.Web.UI.MasterPage
{
    string IDUserRasAlEstemarah, IDUserRasAlEstemarah2, IDUniqRasAlEstemarah, UserERasAlEstemarah;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_335_")];
            IDUserRasAlEstemarah = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_User"];
            IDUniqRasAlEstemarah = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");

            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_User"];
            UserERasAlEstemarah = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");

            HttpCookie CookeUser;  // رقم المستخدم
            CookeUser = Request.Cookies["__UserUniqAdmin_True_User"];
            IDUserRasAlEstemarah2 = ClassSaddam.UnprotectPassword(CookeUser["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");

            CheckAccountAdmin();
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if ((IDUserRasAlEstemarah == IDUserRasAlEstemarah2) && (IDUserRasAlEstemarah == dt.Rows[0]["IDItem"].ToString()) && (IDUniqRasAlEstemarah == dt.Rows[0]["IDUniq"].ToString()) && (UserERasAlEstemarah == dt.Rows[0]["User_Name_"].ToString()))
        {
            if (dt.Rows.Count > 0)
            {
                FGetZyarahByMostafeed(dt.Rows[0]["NumberMostafeed"].ToString());
                lblFirstName.Text = dt.Rows[0]["NameMostafeed"].ToString();

                bool[] A = new bool[50];

                A[1] = Convert.ToBoolean(dt.Rows[0]["A1"]);
                if (A[1]) IDStatusDetails.Visible = true;

                A[2] = Convert.ToBoolean(dt.Rows[0]["A2"]);
                if (A[2]) IDAcceptanceDecision.Visible = true;

                A[2] = Convert.ToBoolean(dt.Rows[0]["A2"]);
                if (A[2]) IDAcceptanceDecision.Visible = true;

                A[3] = Convert.ToBoolean(dt.Rows[0]["A3"]);
                if (A[3]) IDFormData.Visible = true;

                A[4] = Convert.ToBoolean(dt.Rows[0]["A4"]);
                if (A[4])
                { IDfieldVisitApproval.Visible = true; GVA_fieldVisitApproval.Visible = true; }
                    
                A[5] = Convert.ToBoolean(dt.Rows[0]["A5"]);
                if (A[5]) RPTReportDevice.Visible = true;

                pnlSupport.Visible = true;
                A[6] = Convert.ToBoolean(dt.Rows[0]["A6"]);
                if (A[6]) IDSupport.Visible = true;
                A[7] = Convert.ToBoolean(dt.Rows[0]["A7"]);
                if (A[7]) IDSupportHome.Visible = true;
                if (A[6] == false && A[7] == false)
                {
                    pnlSupport.Visible = false;
                }

                A[8] = Convert.ToBoolean(dt.Rows[0]["A8"]);
                if (A[8]) pnlSupportMony.Visible = true;

                A[25] = Convert.ToBoolean(dt.Rows[0]["A25"]);
                if (A[25]) IDBeneficiary.Visible = true;

                A[26] = Convert.ToBoolean(dt.Rows[0]["A26"]);
                if (A[26]) IDBeneficiaryFamily.Visible = true;
            }
        }
        else
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCookie();
            Wellcome();
            lblYears.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy");
        }
    }

    private void Wellcome()
    {
        try
        {
            DateTime time = ClassDataAccess.GetCurrentTime();
            if ((time > Convert.ToDateTime("10:00:00 AM")) && (time < Convert.ToDateTime("11:59:50 AM")))
                lblLestName.Text = "صباح الخير";
            else if ((time > Convert.ToDateTime("12:00:00 PM")) && (time < Convert.ToDateTime("5:00:00 PM")))
                lblLestName.Text = "نهارك سعيد";
            else if ((time > Convert.ToDateTime("5:01:00 PM")) && (time < Convert.ToDateTime("11:59:50 PM")))
                lblLestName.Text = "مساء الخير";
            else if ((time > Convert.ToDateTime("12:00:00 AM")) && (time < Convert.ToDateTime("9:59:50 PM")))
                lblLestName.Text = "صباح الخير";
            else
                lblLestName.Text = "مرحباً بك";
            System.Threading.Thread.Sleep(50);
        }
        catch (Exception)
        {
            lblLestName.Text = "مرحباً بك";
        }
    }

    private void FGetZyarahByMostafeed(string XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) [IDUniq],[DataAddAlZeyarah] FROM [dbo].[ZeyarahMaydanyah] With(NoLock) Where NumberAlMosTafeed = @0 And StateView = @1 And AllowAlZeyarah = @2 And NotAllowAlZeyarah = @3 And IsRaeesMaglesAEdarah = @4 And IsDelete = @5",
           XID, Convert.ToString(true), Convert.ToString(true), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVA_fieldVisitApproval.DataSource = dt;
            GVA_fieldVisitApproval.DataBind();
            //pnlNull.Visible = false;
            //pnlData.Visible = true;
        }
        else
        {
            //pnlNull.Visible = true;
            //pnlData.Visible = false;
        }
        FGetRepoetDevice(XID);
    }

    private void FGetRepoetDevice(string XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP (1) [IDUniq]  FROM [dbo].[ReportAlZyarat] RA With(noLock) Where [NumberMostafeed] = @0 And IsDelete = @1 Order By IDItem Desc",
           XID, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTReportDevice.DataSource = dt;
            RPTReportDevice.DataBind();
        }
    }

}
