using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_MPCPanel : System.Web.UI.MasterPage
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
                    lblNotifications.Text = Repostry_JobAssignment_Map_.FGetCount("GetByAdminActiveCountMain", 0, Guid.Empty, Guid.Empty,
                        new Guid(IDUniq), Guid.Empty, string.Empty, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), string.Empty,
                        false, true, "_Count").ToString();

                    //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]) && Convert.ToBoolean(dtViewProfil.Rows[0]["IsModer"]) == false)
                    //    Response.Redirect("LogOut.aspx");
                    //else
                    //{
                    lblFirstName.Text = dtViewProfil.Rows[0]["FirstName"].ToString();
                    //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsSuperAdmin"]) == false)
                        IDHRM.Visible = true;
                    bool[] A = new bool[150];

                    pnlMenuArn.Visible = true;
                    // القُرى
                    A[31] = Convert.ToBoolean(dtViewProfil.Rows[0]["A31"]);
                    A[77] = Convert.ToBoolean(dtViewProfil.Rows[0]["A77"]);
                    if (A[31] == false && A[77] == false)
                        IDManageVillage.Visible = false;
                    else if (A[31] == true || A[77] == true)
                        IDManageVillage.Visible = true;

                    // حالة المستفيد
                    A[32] = Convert.ToBoolean(dtViewProfil.Rows[0]["A32"]);
                    A[78] = Convert.ToBoolean(dtViewProfil.Rows[0]["A78"]);
                    if (A[32] == false && A[78] == false)
                        IDManageBeneficiaryStatus.Visible = false;
                    else if (A[32] == true || A[78] == true)
                        IDManageBeneficiaryStatus.Visible = true;

                    // نوع المسكن
                    A[33] = Convert.ToBoolean(dtViewProfil.Rows[0]["A33"]);
                    A[79] = Convert.ToBoolean(dtViewProfil.Rows[0]["A79"]);
                    if (A[33] == false && A[79] == false)
                        IDManageTypeOfDwelling.Visible = false;
                    else if (A[33] == true || A[79] == true)
                        IDManageTypeOfDwelling.Visible = true;

                    // الدخل الشهري
                    A[34] = Convert.ToBoolean(dtViewProfil.Rows[0]["A34"]);
                    A[80] = Convert.ToBoolean(dtViewProfil.Rows[0]["A80"]);
                    if (A[34] == false && A[80] == false)
                        IDManageMonthlyIncome.Visible = false;
                    else if (A[34] == true || A[80] == true)
                        IDManageMonthlyIncome.Visible = true;

                    // حالة المسكن
                    A[35] = Convert.ToBoolean(dtViewProfil.Rows[0]["A35"]);
                    A[81] = Convert.ToBoolean(dtViewProfil.Rows[0]["A81"]);
                    if (A[35] == false && A[81] == false)
                        IDManageHousingStatus.Visible = false;
                    else if (A[35] == true || A[81] == true)
                        IDManageHousingStatus.Visible = true;

                    // نوع الدعم
                    A[36] = Convert.ToBoolean(dtViewProfil.Rows[0]["A36"]);
                    A[82] = Convert.ToBoolean(dtViewProfil.Rows[0]["A82"]);
                    if (A[36] == false && A[82] == false)
                        IDManageSupportType.Visible = false;
                    else if (A[36] == true || A[82] == true)
                        IDManageSupportType.Visible = true;

                    // قرابة عائلة المستفيد
                    A[37] = Convert.ToBoolean(dtViewProfil.Rows[0]["A37"]);
                    A[83] = Convert.ToBoolean(dtViewProfil.Rows[0]["A83"]);
                    if (A[37] == false && A[83] == false)
                        IDManageBeneficiaryFamily.Visible = false;
                    else if (A[37] == true || A[83] == true)
                        IDManageBeneficiaryFamily.Visible = true;

                    // صلة قرابة المستفيد
                    A[38] = Convert.ToBoolean(dtViewProfil.Rows[0]["A38"]);
                    A[84] = Convert.ToBoolean(dtViewProfil.Rows[0]["A84"]);
                    if (A[38] == false && A[84] == false)
                        IDManageBeneficiaryRelationship.Visible = false;
                    else if (A[38] == true || A[84] == true)
                        IDManageBeneficiaryRelationship.Visible = true;

                    // المبادرات والداعمين
                    A[132] = Convert.ToBoolean(dtViewProfil.Rows[0]["A132"]);
                    A[133] = Convert.ToBoolean(dtViewProfil.Rows[0]["A133"]);
                    if (A[132] == false && A[133] == false)
                        IDInitiatives.Visible = false;
                    else if (A[132] == true || A[133] == true)
                        IDInitiatives.Visible = true;

                    if (A[31] == false && A[32] == false && A[33] == false && A[34] == false && A[35] == false && A[36] == false && A[37] == false && A[38] == false && A[77] == false && A[78] == false && A[79] == false && A[80] == false && A[81] == false && A[82] == false && A[83] == false && A[84] == false && A[132] == false && A[133] == false)
                        pnlMenuArn.Visible = false;

                    A[39] = Convert.ToBoolean(dtViewProfil.Rows[0]["A39"]); A[40] = Convert.ToBoolean(dtViewProfil.Rows[0]["A40"]);
                    A[75] = Convert.ToBoolean(dtViewProfil.Rows[0]["A75"]); A[76] = Convert.ToBoolean(dtViewProfil.Rows[0]["A76"]);
                    A[121] = Convert.ToBoolean(dtViewProfil.Rows[0]["A121"]); A[122] = Convert.ToBoolean(dtViewProfil.Rows[0]["A122"]); A[123] = Convert.ToBoolean(dtViewProfil.Rows[0]["A123"]);
                    pnlMostafeed.Visible = true;
                    pnlMostafeedStatistic.Visible = true;

                    if (A[39])
                    {
                        IDBeneficiaryBySearch.Visible = true;
                        IDBeneficiaryBySearchComprehensive.Visible = true;
                        IDBeneficiaryBySearchBoysComprehensive.Visible = true;
                        IDBeneficiaryStatistic.Visible = true;
                        IDBeneficiarySourceOfIncome.Visible = true;
                        IDBeneficiaryFamliyCases.Visible = true;
                        IDBeneficiaryAccommodationType.Visible = true;
                        IDBeneficiaryHousingStatus.Visible = true;
                        IDBeneficiaryOrphans.Visible = true;
                        IDBeneficiaryChildrens.Visible = true;
                        IDEducationalSituations.Visible = true;
                        IDMaleAndFemale.Visible = true; IDBeneficiaryTheElderly.Visible = true; IDBeneficiaryYoung.Visible = true;
                    }

                    if (A[40]) { IDBeneficiaryByView.Visible = true; }
                    if (A[75]) { IDAddBeneficiary.Visible = true; }
                    if (A[76]) { IDBeneficiaryAddBoys.Visible = true; }
                    if (A[121]) { IDBeneficiaryByRaeesAlLagnah.Visible = true; }
                    if (A[122]) { IDBeneficiaryByModer.Visible = true; }
                    if (A[123]) { IDBeneficiaryByRaeesAlMaglis.Visible = true; }
                    if (A[39] == false && A[40] == false && A[75] == false && A[76] == false && A[121] == false && A[122] == false && A[123] == false)
                    {
                        pnlMostafeed.Visible = false;
                    }

                    A[85] = Convert.ToBoolean(dtViewProfil.Rows[0]["A85"]); A[41] = Convert.ToBoolean(dtViewProfil.Rows[0]["A41"]);
                    A[86] = Convert.ToBoolean(dtViewProfil.Rows[0]["A86"]); A[87] = Convert.ToBoolean(dtViewProfil.Rows[0]["A87"]);
                    A[42] = Convert.ToBoolean(dtViewProfil.Rows[0]["A42"]);
                    pnlSearchStatus.Visible = true;
                    if (A[85]) { IDSearchStatusAdd.Visible = true; }
                    if (A[41]) { IDSearchStatus.Visible = true; }
                    if (A[86]) { IDSearchStatusManager.Visible = true; }
                    if (A[87]) { IDSearchStatusLagnat.Visible = true; }
                    if (A[42]) { IDSearchStatusDetails.Visible = true; }
                    if (A[85] == false && A[41] == false && A[86] == false && A[87] == false && A[42] == false)
                    {
                        pnlSearchStatus.Visible = false;
                    }

                    A[88] = Convert.ToBoolean(dtViewProfil.Rows[0]["A88"]); A[89] = Convert.ToBoolean(dtViewProfil.Rows[0]["A89"]);
                    A[43] = Convert.ToBoolean(dtViewProfil.Rows[0]["A43"]); A[44] = Convert.ToBoolean(dtViewProfil.Rows[0]["A44"]);
                    pnlAcceptanceDecision.Visible = true;
                    if (A[88]) { IDAcceptanceDecisionAdd.Visible = true; }
                    if (A[89]) { IDAcceptanceDecisionAllow.Visible = true; }
                    if (A[43]) { IDAcceptanceDecisionView.Visible = true; }
                    if (A[44]) { IDAcceptanceDecisionDetailsView.Visible = true; }
                    if (A[88] == false && A[89] == false && A[43] == false && A[44] == false)
                    {
                        pnlAcceptanceDecision.Visible = false;
                    }

                    A[90] = Convert.ToBoolean(dtViewProfil.Rows[0]["A90"]); A[91] = Convert.ToBoolean(dtViewProfil.Rows[0]["A91"]);
                    A[45] = Convert.ToBoolean(dtViewProfil.Rows[0]["A45"]); A[46] = Convert.ToBoolean(dtViewProfil.Rows[0]["A46"]);
                    pnlTecisionToExclude.Visible = true;
                    //pnlTecisionToExclude_Timer.Visible = true;
                    if (A[90]) { IDTecisionToExcludeAdd.Visible = true; }
                    if (A[91]) { IDTecisionToExcludeAllow.Visible = true; }
                    if (A[45]) { IDTecisionToExcludeView.Visible = true; }
                    if (A[46]) { IDTecisionToExcludeDetailsView.Visible = true; }
                    if (A[90] == false && A[91] == false && A[45] == false && A[46] == false)
                    {
                        pnlTecisionToExclude.Visible = false;
                    }

                    A[92] = Convert.ToBoolean(dtViewProfil.Rows[0]["A92"]);
                    A[93] = Convert.ToBoolean(dtViewProfil.Rows[0]["A93"]);
                    A[94] = Convert.ToBoolean(dtViewProfil.Rows[0]["A94"]);
                    A[47] = Convert.ToBoolean(dtViewProfil.Rows[0]["A47"]);
                    A[48] = Convert.ToBoolean(dtViewProfil.Rows[0]["A48"]);
                    A[49] = Convert.ToBoolean(dtViewProfil.Rows[0]["A49"]);

                    pnlAfieldVisi.Visible = true;
                    if (A[92]) { IDAfieldVisitAdd.Visible = true; }
                    if (A[93])
                    {
                        IDAfieldVisitPendingApprovalAdd.Visible = true;
                        ClassZeyarahMaydanyah CZM = new ClassZeyarahMaydanyah();
                        CZM._IsModer = false;
                        CZM._IsDelete = false;
                        DataTable dt = new DataTable();
                        dt = CZM.BArnZeyarahMaydanyahPendingApprovalByModerNotice();
                        if (dt.Rows.Count > 0)
                        {
                            lblNoticeAfieldVisit.Text = dt.Rows[0]["CountNotice"].ToString();
                            lblAfieldVisit.Text = lblNoticeAfieldVisit.Text;
                        }
                        IDNoticeAfieldVisit.Visible = true;
                    }
                    if (A[94]) { IDAfieldVisitPendingApprovalByRaeesAdd.Visible = true; }
                    if (A[47]) { IDAfieldVisitApprovalView.Visible = true; }
                    if (A[48]) { IDAfieldVisitNotApprovedView.Visible = true; }
                    if (A[49]) { IDAfieldVisitDetailsView.Visible = true; }
                    if (A[92] == false && A[93] == false && A[94] == false && A[47] == false && A[48] == false && A[49] == false)
                        pnlAfieldVisi.Visible = false;
                    IDAfieldVisitPendingApprovalByRaeesAdd.Visible = false;

                    A[95] = Convert.ToBoolean(dtViewProfil.Rows[0]["A95"]);
                    A[96] = Convert.ToBoolean(dtViewProfil.Rows[0]["A96"]);
                    A[97] = Convert.ToBoolean(dtViewProfil.Rows[0]["A97"]);
                    A[50] = Convert.ToBoolean(dtViewProfil.Rows[0]["A50"]);
                    A[51] = Convert.ToBoolean(dtViewProfil.Rows[0]["A51"]);
                    pnlVisitReport.Visible = true;
                    if (A[95]) { IDVisitReportAdd.Visible = true; }
                    if (A[96])
                    {
                        IDVisitReportByModerAdd.Visible = true;
                        DataTable dtrm = new DataTable();
                        dtrm = ClassDataAccess.GetData("SELECT Count(*) As 'CountNotice' FROM [dbo].[ReportAlZyarat] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ReportAlZyarat.NumberMostafeed Where IsModerAllow = @0 And IsRaesLagnatAlBahthAllow = @0 And ReportAlZyarat.IsDelete = @0", Convert.ToString(false));
                        if (dtrm.Rows.Count > 0)
                            lblVisitReportByModer.Text = dtrm.Rows[0]["CountNotice"].ToString();
                    }
                    if (A[97])
                    {
                        IDVisitReportByRaeesAllagnahAdd.Visible = true;
                        DataTable dtrr = new DataTable();
                        dtrr = ClassDataAccess.GetData("SELECT Count(*) As 'CountNotice' FROM [dbo].[ReportAlZyarat] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ReportAlZyarat.NumberMostafeed Where IsModerAllow = @0 And IsRaesLagnatAlBahthAllow = @1 And ReportAlZyarat.IsDelete = @1", Convert.ToString(true), Convert.ToString(false));
                        if (dtrr.Rows.Count > 0)
                            lblVisitReportByRaeesAllagnah.Text = dtrr.Rows[0]["CountNotice"].ToString();
                    }
                    lblNoticeVisitReport.Text = Convert.ToString(Convert.ToInt64(lblVisitReportByModer.Text) + Convert.ToInt64(lblVisitReportByRaeesAllagnah.Text));
                    if (A[96] == false && A[97] == false) { IDNoticeVisitReport.Visible = false; }

                    if (A[50]) { IDVisitReportView.Visible = true; IDVisitReportByDevice.Visible = true; IDVisitReportByDeviceMedical.Visible = true; IDVisitReportByHouse.Visible = true; }
                    if (A[51]) { IDVisitReportDetailsView.Visible = true; }
                    if (A[95] == false && A[96] == false && A[97] == false && A[50] == false && A[51] == false)
                        pnlVisitReport.Visible = false;

                    A[98] = Convert.ToBoolean(dtViewProfil.Rows[0]["A98"]);
                    A[99] = Convert.ToBoolean(dtViewProfil.Rows[0]["A99"]);
                    A[100] = Convert.ToBoolean(dtViewProfil.Rows[0]["A100"]);
                    A[52] = Convert.ToBoolean(dtViewProfil.Rows[0]["A52"]);
                    A[53] = Convert.ToBoolean(dtViewProfil.Rows[0]["A53"]);
                    pnlRe_beneficiary.Visible = true;
                    if (A[98]) { IDRe_beneficiaryAdd.Visible = true; }
                    if (A[99])
                    {
                        IDRe_beneficiaryByModerAdd.Visible = true;
                        DataTable dtrm = new DataTable();
                        dtrm = ClassDataAccess.GetData("SELECT Count(NumberOrder) As 'CountNotice' FROM [dbo].[EadatMostafeed] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = EadatMostafeed.NumberAlMostafeed Where IsAllowModer = @0 And IsAllowRaees = @1 And IsEaadat = @2 And IsEstbaad = @3 And EadatMostafeed.IsDelete = @4 And RasAlEstemarah.IsDelete = @4", Convert.ToString(false), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
                        if (dtrm.Rows.Count > 0)
                            lblRe_beneficiaryByModer.Text = dtrm.Rows[0]["CountNotice"].ToString();
                    }
                    if (A[100])
                    {
                        IDRe_beneficiaryByRaeesAdd.Visible = true;
                        DataTable dtrm = new DataTable();
                        dtrm = ClassDataAccess.GetData("SELECT Count(NumberOrder) As 'CountNotice' FROM [dbo].[EadatMostafeed] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = EadatMostafeed.NumberAlMostafeed Where IsAllowModer = @0 And IsAllowRaees = @1 And IsEaadat = @2 And IsEstbaad = @3 And EadatMostafeed.IsDelete = @4 And RasAlEstemarah.IsDelete = @4", Convert.ToString(true), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
                        if (dtrm.Rows.Count > 0)
                            lblRe_beneficiaryByRaees.Text = dtrm.Rows[0]["CountNotice"].ToString();
                    }
                    lblNoticeRe_beneficiary.Text = Convert.ToString(Convert.ToInt64(lblRe_beneficiaryByModer.Text) + Convert.ToInt64(lblRe_beneficiaryByRaees.Text));
                    if (A[99] == false && A[100] == false) { IDNoticeRe_beneficiary.Visible = false; }

                    if (A[52]) { IDRe_beneficiaryView.Visible = true; }
                    if (A[53]) { IDRe_beneficiaryDetailsView.Visible = true; }
                    if (A[98] == false && A[99] == false && A[100] == false && A[52] == false && A[53] == false)
                        pnlRe_beneficiary.Visible = false;

                    A[101] = Convert.ToBoolean(dtViewProfil.Rows[0]["A101"]);
                    A[102] = Convert.ToBoolean(dtViewProfil.Rows[0]["A102"]);
                    A[103] = Convert.ToBoolean(dtViewProfil.Rows[0]["A103"]);
                    A[54] = Convert.ToBoolean(dtViewProfil.Rows[0]["A54"]);
                    A[55] = Convert.ToBoolean(dtViewProfil.Rows[0]["A55"]);
                    pnlExclusionOfTheBeneficiary.Visible = true;
                    if (A[101]) { IDExclusionOfTheBeneficiaryAdd.Visible = true; }
                    if (A[102])
                    {
                        IDExclusionOfTheBeneficiaryByModerViewAdd.Visible = true;
                        DataTable dtEMM = new DataTable();
                        dtEMM = ClassDataAccess.GetData("SELECT Count(NumberOrder) As 'CountNotice' FROM [dbo].[EadatMostafeed] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = EadatMostafeed.NumberAlMostafeed Where IsAllowModer = @0 And IsAllowRaees = @1 And IsEaadat = @2 And IsEstbaad = @3 And EadatMostafeed.IsDelete = @4 And RasAlEstemarah.IsDelete = @4", Convert.ToString(false), Convert.ToString(false), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
                        if (dtEMM.Rows.Count > 0)
                            lblExclusionOfTheBeneficiaryByModer.Text = dtEMM.Rows[0]["CountNotice"].ToString();
                    }
                    if (A[103])
                    {
                        IDExclusionOfTheBeneficiaryByRaeesAdd.Visible = true;
                        DataTable dtEMR = new DataTable();
                        dtEMR = ClassDataAccess.GetData("SELECT Count(NumberOrder) As 'CountNotice' FROM [dbo].[EadatMostafeed] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = EadatMostafeed.NumberAlMostafeed Where IsAllowModer = @0 And IsAllowRaees = @1 And IsEaadat = @2 And IsEstbaad = @3 And EadatMostafeed.IsDelete = @4 And RasAlEstemarah.IsDelete = @4", Convert.ToString(true), Convert.ToString(false), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
                        if (dtEMR.Rows.Count > 0)
                            lblExclusionOfTheBeneficiaryByRaees.Text = dtEMR.Rows[0]["CountNotice"].ToString();
                    }
                    lblNoticeExclusionOfTheBeneficiary.Text = Convert.ToString(Convert.ToInt64(lblExclusionOfTheBeneficiaryByModer.Text) + Convert.ToInt64(lblExclusionOfTheBeneficiaryByRaees.Text));
                    if (A[102] == false && A[103] == false) { IDNoticeExclusionOfTheBeneficiary.Visible = false; }

                    if (A[54]) { IDExclusionOfTheBeneficiaryView.Visible = true; }
                    if (A[55]) { IDExclusionOfTheBeneficiaryDetailsView.Visible = true; }
                    if (A[101] == false && A[102] == false && A[103] == false && A[54] == false && A[55] == false)
                        pnlExclusionOfTheBeneficiary.Visible = false;

                    A[104] = Convert.ToBoolean(dtViewProfil.Rows[0]["A104"]);
                    A[105] = Convert.ToBoolean(dtViewProfil.Rows[0]["A105"]);
                    A[56] = Convert.ToBoolean(dtViewProfil.Rows[0]["A56"]);
                    A[57] = Convert.ToBoolean(dtViewProfil.Rows[0]["A57"]);
                    A[58] = Convert.ToBoolean(dtViewProfil.Rows[0]["A58"]);
                    pnlConvertedCases.Visible = true;
                    if (A[104]) { IDConvertedCasesAdd.Visible = true; }
                    if (A[105])
                    {
                        IDConvertedCasesByModerAdd.Visible = true;
                        DataTable dtTHM = new DataTable();
                        dtTHM = ClassDataAccess.GetData("SELECT Count(TahweelAlHalah.NumberMostafeed)  As 'CountNotice' FROM [dbo].[TahweelAlHalah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TahweelAlHalah.NumberMostafeed Where  IsAllowModer = @0 And TahweelAlHalah.IsDelete = @0 And RasAlEstemarah.IsDelete = @0", Convert.ToString(false));
                        if (dtTHM.Rows.Count > 0)
                        {
                            lblNoticeConvertedCases.Text = dtTHM.Rows[0]["CountNotice"].ToString();
                            lblConvertedCasesByModer.Text = lblNoticeConvertedCases.Text;
                        }
                        IDNoticeConvertedCases.Visible = true;
                    }
                    if (A[56]) { IDConvertedCasesView.Visible = true; }
                    if (A[57]) { IDConvertedCasesWaitingForApprovalView.Visible = true; }
                    if (A[58]) { IDConvertedCasesDetailsView.Visible = true; }
                    if (A[104] == false && A[105] == false && A[56] == false && A[57] == false && A[58] == false)
                        pnlConvertedCases.Visible = false;

                    pnlManageProductWarehouse.Visible = true;

                    if (A[62] == false && A[63] == false && A[64] == false && A[65] == false && A[66] == false && A[67] == false && A[68] == false && A[69] == false && A[70] == false && A[112] == false && A[113] == false && A[114] == false && A[115] == false)
                        pnlManageProductWarehouse.Visible = false;

                    if (A[31] == false && A[32] == false && A[33] == false && A[34] == false && A[35] == false && A[36] == false && A[37] == false && A[38] == false &&
                        A[39] == false && A[40] == false && A[41] == false && A[42] == false && A[43] == false && A[44] == false && A[45] == false && A[46] == false &&
                        A[47] == false && A[48] == false && A[49] == false && A[50] == false && A[51] == false && A[52] == false && A[53] == false && A[56] == false &&
                        A[57] == false && A[58] == false)
                        Response.Redirect("~/Cpanel/CHome/PageNotAccess.aspx");
                    //}
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
            FGetSearchStatusByModer();
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
        FGetMostafeedByRaeesAllagnah();
    }

    private void FGetMostafeedByRaeesAllagnah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(*) As 'CountRaeesAllagnah' FROM [dbo].[RasAlEstemarah] With(NoLock) Where IsDelete = @0 And IsAllowRaeesLagnatAlBahth_ = @0 And IsAllowModer_ = @1", Convert.ToString(false), Convert.ToString(true));
        if (dt.Rows.Count > 0)
            lblMostafeedByRaeesAllagnah.Text = dt.Rows[0]["CountRaeesAllagnah"].ToString();
        FGetMostafeedByModer();
    }

    private void FGetMostafeedByModer()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(*) As 'CountModer' FROM [dbo].[RasAlEstemarah] With(NoLock) Where IsDelete = @0 And IsAllowModer_ = @0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            lblMostafeedByModer.Text = dt.Rows[0]["CountModer"].ToString();
        FGetMostafeedByRaeesMaglisAlEdarah();
    }

    private void FGetMostafeedByRaeesMaglisAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(*) As 'CountRaeesMaglisAlEdara' FROM [dbo].[RasAlEstemarah] With(NoLock) Where IsDelete = @0 And IsAllowRaeesLagnatAlBahth_ = @1 And IsAllowModer_ = @1 And IsRaeesMaglisAlEdarah_ = @0", Convert.ToString(false), Convert.ToString(true));
        if (dt.Rows.Count > 0)
            lblMostafeedByRaeesMaglisAlEdara.Text = dt.Rows[0]["CountRaeesMaglisAlEdara"].ToString();
        if (IDBeneficiaryByRaeesAlLagnah.Visible == false)
            lblNoticeMostafeed.Text = Convert.ToString(Convert.ToInt64(lblMostafeedByModer.Text) + Convert.ToInt64(lblMostafeedByRaeesMaglisAlEdara.Text));
        else if (IDBeneficiaryByModer.Visible == false)
            lblNoticeMostafeed.Text = Convert.ToString(Convert.ToInt64(lblMostafeedByRaeesAllagnah.Text) + Convert.ToInt64(lblMostafeedByRaeesMaglisAlEdara.Text));
        else if (IDBeneficiaryByRaeesAlLagnah.Visible == false && IDBeneficiaryByModer.Visible == false)
            lblNoticeMostafeed.Text = Convert.ToString(Convert.ToInt64(lblMostafeedByRaeesMaglisAlEdara.Text));
        else if (IDBeneficiaryByRaeesAlMaglis.Visible == false && IDBeneficiaryByModer.Visible == false)
            lblNoticeMostafeed.Text = Convert.ToString(Convert.ToInt64(lblMostafeedByRaeesAllagnah.Text));
        else if (IDBeneficiaryByRaeesAlLagnah.Visible == false && IDBeneficiaryByRaeesAlMaglis.Visible == false)
            lblNoticeMostafeed.Text = Convert.ToString(Convert.ToInt64(lblMostafeedByModer.Text));
        else if (IDBeneficiaryByRaeesAlMaglis.Visible == false)
            lblNoticeMostafeed.Text = Convert.ToString(Convert.ToInt64(lblMostafeedByRaeesAllagnah.Text) + Convert.ToInt64(lblMostafeedByModer.Text));
        else if (IDBeneficiaryByRaeesAlLagnah.Visible && IDBeneficiaryByModer.Visible && IDBeneficiaryByRaeesAlMaglis.Visible)
            lblNoticeMostafeed.Text = Convert.ToString(Convert.ToInt64(lblMostafeedByRaeesAllagnah.Text) + Convert.ToInt64(lblMostafeedByModer.Text) + Convert.ToInt64(lblMostafeedByRaeesMaglisAlEdara.Text));
        FGetAllMostafeed();
    }

    private void FGetSearchStatusByModer()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(*) As 'CountSearchByModer' FROM [dbo].[BahthHalatMostafeed] With(NoLock) Where IsAllowModer = @0 And IsDelete = @1", Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            lblAllowSearchStatusByModer.Text = dt.Rows[0]["CountSearchByModer"].ToString();
        FGetSearchStatusByRaeesLagnatAlBahth();
    }

    private void FGetSearchStatusByRaeesLagnatAlBahth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(*) As 'CountSearchByModer' FROM [dbo].[BahthHalatMostafeed] With(NoLock) Where IsAllowModer = @0 And IsAllowRaeesLagnatAlBahth = @1 And IsDelete = @2", Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            lblAllowSearchStatusByLagnat.Text = dt.Rows[0]["CountSearchByModer"].ToString();

        if (IDSearchStatusManager.Visible == false && IDSearchStatusLagnat.Visible)
            lblNoticeSearchStatus.Text = lblAllowSearchStatusByLagnat.Text;
        else if(IDSearchStatusLagnat.Visible == false && IDSearchStatusManager.Visible)
            lblNoticeSearchStatus.Text = lblAllowSearchStatusByModer.Text;
        else if (IDSearchStatusManager.Visible && IDSearchStatusLagnat.Visible)
            lblNoticeSearchStatus.Text = Convert.ToString(Convert.ToInt64(lblAllowSearchStatusByModer.Text) + Convert.ToInt64(lblAllowSearchStatusByLagnat.Text));
        FGetAcceptanceDecisionAllow();
    }

    private void FGetAcceptanceDecisionAllow()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 Count(*) As 'CountQarar' FROM [dbo].[QararQobolMustafeedAdmin] TBLAdmin , QararQobolMustafeed TBL Where (TBLAdmin.NumberQarsr = TBL.NumberQarar) And (TBLAdmin.NumberMosTafeed = TBL.NumberMostafeed) And TBLAdmin.IDAdmin = @0 And AdminAllow = @1 And IsQobol = @2 And IsEstepaad = @3 And TBL.IsDelete = @4", IDUser, Convert.ToString(false), Convert.ToString(true), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            lblAcceptanceDecisionAllow.Text = dt.Rows[0]["CountQarar"].ToString();
        if (IDAcceptanceDecisionAllow.Visible)
            lblNoticeAcceptanceDecisionAllow.Text = lblAcceptanceDecisionAllow.Text;
        FGetTecisionToExcludeAllow();
    }

    private void FGetTecisionToExcludeAllow()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 Count(*) As 'CountQarar' FROM [dbo].[QararQobolMustafeedAdmin] TBLAdmin , QararQobolMustafeed TBL Where (TBLAdmin.NumberQarsr = TBL.NumberQarar) And (TBLAdmin.NumberMosTafeed = TBL.NumberMostafeed) And TBLAdmin.IDAdmin = @0 And AdminAllow = @1 And IsQobol = @2 And IsEstepaad = @3 And TBL.IsDelete = @4", IDUser, Convert.ToString(false), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            lblTecisionToExcludeAllow.Text = dt.Rows[0]["CountQarar"].ToString();
        if (IDTecisionToExcludeAllow.Visible)
            lblNoticeTecisionToExcludeAllow.Text = lblTecisionToExcludeAllow.Text;
    }

    private void FGetAllMostafeed()
    {
        try
        {
            DataTable dtToAll = new DataTable();
            dtToAll = ClassDataAccess.GetData("SELECT Count(*) As 'CountAll' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where RasAlEstemarah.IsDelete = @0 And Quaem.AlQriah <> @1", Convert.ToString(false), "مناطق_أخرى");
            if (dtToAll.Rows.Count > 0)
                lblCountAllMostafeed.Text = dtToAll.Rows[0]["CountAll"].ToString();
            FGetMostafeedByDaaem();
        }
        catch (Exception)
        {

        }
    }

    public string GetPercintAllMostafeed()
    {
        string XResult = "";
        try
        {
            double PerSent;
            //PerSent = 100 / 5000;
            PerSent = 0.1;
            XResult = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(lblCountAllMostafeed.Text) * PerSent, 1)));
        }
        catch (Exception)
        {

        }
        return XResult;
    }

    private void FGetMostafeedByDaaem()
    {
        DataTable dtToDaaem = new DataTable();
        dtToDaaem = ClassDataAccess.GetData("SELECT Count(*) As 'CountByDaaem' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where TypeMostafeed = @0 And RasAlEstemarah.IsDelete = @1 And Quaem.AlQriah <> @2 ", Convert.ToString("دائم"), Convert.ToString(false), "مناطق_أخرى");
        if (dtToDaaem.Rows.Count > 0)
            lblCountByDaaem.Text = dtToDaaem.Rows[0]["CountByDaaem"].ToString();
        FGetMostafeedByMostabaad();
    }

    public string GetPercintByDaaem()
    {
        string XResult = "";
        try
        {
            double PerSent;
            PerSent = 100 / Convert.ToDouble(lblCountAllMostafeed.Text);
            XResult = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(lblCountByDaaem.Text) * PerSent, 1)));
        }
        catch (Exception)
        {

        }
        return XResult;
    }

    private void FGetMostafeedByMostabaad()
    {
        DataTable dtToDaaem = new DataTable();
        dtToDaaem = ClassDataAccess.GetData("SELECT Count(*) As 'CountByMostabaad' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where TypeMostafeed = @0 And RasAlEstemarah.IsDelete = @1 And Quaem.AlQriah <> @2", Convert.ToString("مستبعد"), Convert.ToString(false), "مناطق_أخرى");
        if (dtToDaaem.Rows.Count > 0)
            lblCountByMostabaad.Text = dtToDaaem.Rows[0]["CountByMostabaad"].ToString();
    }

    public string GetPercintByMostabaad()
    {
        string XResult = "";
        try
        {
            double PerSent;
            PerSent = 100 / Convert.ToDouble(lblCountAllMostafeed.Text);
            XResult = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(lblCountByMostabaad.Text) * PerSent, 1)));
        }
        catch (Exception)
        {

        }
        return XResult;
    }

}

