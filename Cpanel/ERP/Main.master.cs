using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_Main : System.Web.UI.MasterPage
{
    string IDUser, IDUniq, UserN, IDAdmin2, IDCookie;
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

            HttpCookie CookeID;  // رقم المستخدم
            CookeID = Request.Cookies["__User_Screen_"];
            IDCookie = ClassSaddam.UnprotectPassword(CookeID["Url"], "www.ITFY-Edu.Net_ProtectByITFY");

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
            if ((IDUser == IDAdmin2) && (IDUser == dtViewProfil.Rows[0]["ID_Item"].ToString()) && (IDUniq == dtViewProfil.Rows[0]["IDUniqUser"].ToString()) && (UserN == dtViewProfil.Rows[0]["User_Name_"].ToString()) && (IDCookie == dtViewProfil.Rows[0]["_ID_Cookie_"].ToString()))
            {
                if (dtViewProfil.Rows.Count > 0)
                {
                    lblFirstName.Text = dtViewProfil.Rows[0]["FirstName"].ToString();
                    //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsSuperAdmin"]) == false)
                    IDHRM.Visible = true;
                    bool[] A = new bool[250];

                    A[150] = Convert.ToBoolean(dtViewProfil.Rows[0]["A150"]);
                    A[151] = Convert.ToBoolean(dtViewProfil.Rows[0]["A151"]);
                    A[152] = Convert.ToBoolean(dtViewProfil.Rows[0]["A152"]);
                    A[153] = Convert.ToBoolean(dtViewProfil.Rows[0]["A153"]);
                    A[154] = Convert.ToBoolean(dtViewProfil.Rows[0]["A154"]);
                    A[155] = Convert.ToBoolean(dtViewProfil.Rows[0]["A155"]);
                    A[156] = Convert.ToBoolean(dtViewProfil.Rows[0]["A156"]);
                    A[157] = Convert.ToBoolean(dtViewProfil.Rows[0]["A157"]);
                    A[158] = Convert.ToBoolean(dtViewProfil.Rows[0]["A158"]);
                    A[159] = Convert.ToBoolean(dtViewProfil.Rows[0]["A159"]);
                    A[160] = Convert.ToBoolean(dtViewProfil.Rows[0]["A160"]);
                    A[161] = Convert.ToBoolean(dtViewProfil.Rows[0]["A161"]);
                    A[162] = Convert.ToBoolean(dtViewProfil.Rows[0]["A162"]);
                    A[163] = Convert.ToBoolean(dtViewProfil.Rows[0]["A163"]);
                    A[164] = Convert.ToBoolean(dtViewProfil.Rows[0]["A164"]);
                    A[165] = Convert.ToBoolean(dtViewProfil.Rows[0]["A165"]);
                    A[166] = Convert.ToBoolean(dtViewProfil.Rows[0]["A166"]);
                    A[167] = Convert.ToBoolean(dtViewProfil.Rows[0]["A167"]);
                    A[168] = Convert.ToBoolean(dtViewProfil.Rows[0]["A168"]);
                    A[169] = Convert.ToBoolean(dtViewProfil.Rows[0]["A169"]);
                    A[170] = Convert.ToBoolean(dtViewProfil.Rows[0]["A170"]);
                    A[171] = Convert.ToBoolean(dtViewProfil.Rows[0]["A171"]);
                    A[172] = Convert.ToBoolean(dtViewProfil.Rows[0]["A172"]);
                    A[173] = Convert.ToBoolean(dtViewProfil.Rows[0]["A173"]);
                    A[174] = Convert.ToBoolean(dtViewProfil.Rows[0]["A174"]);
                    A[175] = Convert.ToBoolean(dtViewProfil.Rows[0]["A175"]);
                    A[176] = Convert.ToBoolean(dtViewProfil.Rows[0]["A176"]);
                    A[177] = Convert.ToBoolean(dtViewProfil.Rows[0]["A177"]);
                    A[178] = Convert.ToBoolean(dtViewProfil.Rows[0]["A178"]);
                    A[179] = Convert.ToBoolean(dtViewProfil.Rows[0]["A179"]);
                    A[180] = Convert.ToBoolean(dtViewProfil.Rows[0]["A180"]);
                    A[181] = Convert.ToBoolean(dtViewProfil.Rows[0]["A181"]);
                    A[182] = Convert.ToBoolean(dtViewProfil.Rows[0]["A182"]);
                    A[183] = Convert.ToBoolean(dtViewProfil.Rows[0]["A183"]);
                    A[184] = Convert.ToBoolean(dtViewProfil.Rows[0]["A184"]);
                    A[185] = Convert.ToBoolean(dtViewProfil.Rows[0]["A185"]);
                    A[186] = Convert.ToBoolean(dtViewProfil.Rows[0]["A186"]);
                    A[187] = Convert.ToBoolean(dtViewProfil.Rows[0]["A187"]);
                    A[188] = Convert.ToBoolean(dtViewProfil.Rows[0]["A188"]);
                    A[189] = Convert.ToBoolean(dtViewProfil.Rows[0]["A189"]);
                    A[190] = Convert.ToBoolean(dtViewProfil.Rows[0]["A190"]);
                    A[191] = Convert.ToBoolean(dtViewProfil.Rows[0]["A191"]);
                    A[192] = Convert.ToBoolean(dtViewProfil.Rows[0]["A192"]);
                    A[193] = Convert.ToBoolean(dtViewProfil.Rows[0]["A193"]);
                    A[194] = Convert.ToBoolean(dtViewProfil.Rows[0]["A194"]);
                    A[195] = Convert.ToBoolean(dtViewProfil.Rows[0]["A195"]);
                    A[196] = Convert.ToBoolean(dtViewProfil.Rows[0]["A196"]);
                    A[197] = Convert.ToBoolean(dtViewProfil.Rows[0]["A197"]);
                    A[198] = Convert.ToBoolean(dtViewProfil.Rows[0]["A198"]);
                    A[199] = Convert.ToBoolean(dtViewProfil.Rows[0]["A198"]);
                    A[200] = Convert.ToBoolean(dtViewProfil.Rows[0]["A199"]);
                    A[201] = Convert.ToBoolean(dtViewProfil.Rows[0]["A201"]);
                    A[202] = Convert.ToBoolean(dtViewProfil.Rows[0]["A202"]);

                    lblNotifications.Text = Repostry_JobAssignment_Map_.FGetCount("GetByAdminActiveCountMain", 0, Guid.Empty, Guid.Empty,
                        new Guid(IDUniq), Guid.Empty, string.Empty, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), string.Empty,
                        false, true, "_Count").ToString();

                    if (A[150] == false && A[151] == false && A[152] == false && A[153] == false && A[154] == false && A[155] == false && A[156] == false && A[157] == false && A[158] == false && A[159] == false && A[160] == false && A[161] == false && A[163] == false && A[165] == false && A[167] == false && A[169] == false && A[171] == false && A[173] == false && A[175] == false && A[177] == false && A[179] == false)
                        Response.Redirect("~/Cpanel/CHome/PageNotAccess.aspx");

                    pnlSetting.Visible = true;
                    if (A[150]) pnlSetting.Visible = true;
                    if (A[150] == false) pnlSetting.Visible = false;


                    pnlEmpDetails.Visible = true;
                    if (A[151]) pnlEmpDetailsView.Visible = true;
                    if (A[152]) pnlEmpDetailsAdd.Visible = true;
                    if (A[151] == false && A[152] == false) pnlEmpDetails.Visible = false;


                    pnlEmpSalaery.Visible = true;
                    if (A[153]) pnlEmpSalaeryView.Visible = true;
                    if (A[154]) pnlEmpSalaeryAdd.Visible = true;
                    if (A[153] == false && A[154] == false) pnlEmpSalaery.Visible = false;

                    pnlEmpJobAssignment.Visible = true;
                    if (A[155]) { pnlEmpJobAssignmentView.Visible = true; pnlEmpJobAssignmentDetailsView.Visible = true; pnlEmpJobAssignmentList.Visible = true; }
                    if (A[156]) pnlEmpJobAssignmentAdd.Visible = true;
                    if (A[157]) { pnlEmpJobAssignmentModerAdd.Visible = false; pnlEmpJobAssignmentRaeesAdd.Visible = false; }
                    if (A[155] == false && A[156] == false && A[157] == false) pnlEmpJobAssignment.Visible = false;

                    pnlEmpCompensatory.Visible = true;
                    if (A[158]) pnlEmpCompensatoryView.Visible = true;
                    if (A[161]) pnlEmpCompensatoryAdd.Visible = true;
                    if (A[158] == false && A[161] == false) pnlEmpCompensatory.Visible = false;

                    if (A[162]) pnlEmpLeaveCategoryAdd.Visible = true;

                    if (A[163]) pnlEmpLeaveCategoryModerAdd.Visible = true;

                    if (A[164]) pnlEmpLeaveCategoryRaeesAdd.Visible = false;

                    if (A[159]) { pnlEmpLeaveCategoryView.Visible = true; pnlEmpLeaveCategoryDetailsView.Visible = true; }

                    if (A[160]) pnlEmpLeaveCategoryListView.Visible = true;

                    pnlEmpAccountable.Visible = true;
                    if (A[165]) { pnlEmpAccountableView.Visible = true; pnlEmpAccountableDetailsView.Visible = true; }
                    if (A[166]) pnlEmpAccountableAdd.Visible = true;
                    if (A[167]) pnlEmpAccountableRaeesAdd.Visible = true;
                    if (A[165] == false && A[166] == false && A[167] == false) pnlEmpAccountable.Visible = false;

                    pnlEmpResolved.Visible = true;
                    if (A[168]) { pnlEmpResolvedView.Visible = true; pnlEmpResolvedDetailsView.Visible = true; }
                    if (A[169]) pnlEmpResolvedAdd.Visible = true;
                    if (A[170]) pnlEmpResolvedModerAdd.Visible = true;
                    if (A[171]) pnlEmpResolvedRaeesAdd.Visible = false;
                    if (A[168] == false && A[169] == false && A[170] == false && A[171] == false) pnlEmpResolved.Visible = false;

                    pnlEmpLoan.Visible = true;
                    if (A[172]) { pnlEmpLoanView.Visible = true; pnlEmpLoanDetailsView.Visible = true; }
                    if (A[173]) pnlEmpLoanAdd.Visible = true;
                    if (A[174]) pnlEmpLoanRepaymentAdd.Visible = true;
                    if (A[175]) pnlEmpLoanModerAdd.Visible = true;
                    if (A[176]) pnlEmpLoanRaeesAdd.Visible = false;
                    if (A[172] == false && A[173] == false && A[174] == false && A[175] == false && A[176] == false) pnlEmpLoan.Visible = false;

                    pnlEmpMandate.Visible = true;
                    if (A[177]) { pnlEmpMandateView.Visible = true; pnlEmpMandateDetailsView.Visible = true; }
                    if (A[178]) pnlEmpMandateListView.Visible = true;
                    if (A[179]) pnlEmpMandateAdd.Visible = true;
                    if (A[180]) pnlEmpMandateModerAdd.Visible = true;
                    if (A[177] == false && A[178] == false && A[179] == false && A[180] == false) pnlEmpMandate.Visible = false;

                    pnlEmpOvertime.Visible = true;
                    if (A[181]) { pnlEmpOvertimeView.Visible = true; pnlEmpOvertimeDetailsView.Visible = true; }
                    if (A[182]) pnlEmpOvertimeListView.Visible = true;
                    if (A[183]) pnlEmpOvertimeAdd.Visible = true;
                    if (A[184]) pnlEmpOvertimeModerAdd.Visible = true;
                    if (A[181] == false && A[182] == false && A[183] == false && A[184] == false) pnlEmpOvertime.Visible = false;

                    pnlEmpBonuses.Visible = true;
                    if (A[185]) { pnlEmpBonusesView.Visible = true; pnlEmpBonusesDetailsView.Visible = true; }
                    if (A[186]) pnlEmpBonusesListView.Visible = true;
                    if (A[187]) pnlEmpBonusesAdd.Visible = true;
                    if (A[188]) pnlEmpBonusesModerAdd.Visible = true;
                    if (A[185] == false && A[186] == false && A[187] == false && A[188] == false) pnlEmpBonuses.Visible = false;

                    pnlEmpAttendanceEntry.Visible = true;
                    if (A[189]) pnlEmpAttendanceEntryView.Visible = true;
                    if (A[190]) pnlEmpAttendanceEntryAllView.Visible = true;
                    if (A[191]) pnlEmpAttendanceEntryAdd.Visible = true;
                    if (A[189] == false && A[190] == false && A[191] == false) pnlEmpAttendanceEntry.Visible = false;

                    pnlEmpAddSalary.Visible = true;
                    if (A[192]) pnlEmpAddSalaryView.Visible = true;
                    if (A[193]) pnlEmpAddSalaryListView.Visible = true;
                    if (A[194]) pnlEmpAddSalaryAdd.Visible = true;
                    if (A[192] == false && A[193] == false && A[194] == false) pnlEmpAddSalary.Visible = false;

                    pnlEmpPermission.Visible = true;
                    if (A[195]) { pnlEmpPermissionView.Visible = true; pnlEmpPermissionAll.Visible = true; }
                    if (A[196]) pnlEmpPermissionAdd.Visible = true;
                    if (A[198]) pnlEmpPermissionByRaees.Visible = true;
                    if (A[195] == false && A[196] == false && A[198] == false) pnlEmpPermission.Visible = false;

                    pnlEmpWarning.Visible = true;
                    if (A[199]) { pnlEmpWarningView.Visible = true; pnlEmpWarningDetailsView.Visible = true; }
                    if (A[200]) pnlEmpWarningAdd.Visible = true;
                    if (A[202]) pnlEmpWarningRaeesAdd.Visible = true;
                    if (A[199] == false && A[200] == false && A[202] == false) pnlEmpWarning.Visible = false;
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
        {
            CheckAccountAdmin();
            Wellcome();
            lblYears.Text = ClassSaddam.GetCurrentTime().ToString("yyyy");
        }
    }

    private void Wellcome()
    {
        try
        {
            DateTime time = ClassSaddam.GetCurrentTime();
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

}
