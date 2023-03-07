using Library_CLS_Arn.ClassEntity.Attach.Repostry;
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

public partial class Cpanel_CPanelSetting_PageGroupAdd : System.Web.UI.Page
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
            bool A6;
            A6 = Convert.ToBoolean(dtViewProfil.Rows[0]["A6"]);
            if (A6 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            txtTitleGroup.Focus();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageGroupAdd.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitleGroup.Text != string.Empty)
            {
                lblTitleGroup.Visible = false;
                FChackName();
                System.Threading.Thread.Sleep(500);
            }
            else
            {
                lblTitleGroup.Visible = true;
                lblTitleGroup.Text = "* عنوان المجموعة";
            }

        }
        catch
        {
            lblMessageWarning.Text = "خطأ غير متوقع حاول لاحقاً";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FChackName()
    {
        DataTable dtUser = new DataTable();
        dtUser = ClassDataAccess.GetData("Select Top(1) NameGroup,IsDeleteGroup from tbl_Group_Arn With(NoLock) Where NameGroup =@0 And IsDeleteGroup = @1", txtTitleGroup.Text.Trim(), Convert.ToString(false));
        if (dtUser.Rows.Count > 0)
        {
            lblMessageWarning.Text = "تم إضافة المجموعة سابقاً ";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
        }
        else
            FGroupAdd();
    }

    private void FGroupAdd()
    {
        ClassGroup CG = new ClassGroup();
        CG.IDUniqGroup = Convert.ToString(Guid.NewGuid());
        CG.NameGroup = txtTitleGroup.Text.Trim();
        CG.IsActiveGroup = Convert.ToBoolean(CBActive.Checked);
        CG.IsSuperAdminGroup = false;
        CG.IsDeleteGroup = false;
        CG.DateAddGroup = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
        CG.A[0] = false;
        CG.A[1] = false;
        CG.A[2] = false;
        CG.A[3] = false;
        CG.A[4] = CBGroupView.Checked;
        CG.A[5] = CBGroupAdd.Checked;
        CG.A[6] = CBAdminView.Checked;
        CG.A[7] = CBAdminAdd.Checked;

        // نظام المؤسسة
        CG.A[8] = CBMenu.Checked;
        CG.A[9] = CBMenuAdd.Checked;
        CG.A[10] = CBArticle.Checked;
        CG.A[11] = CBArticleAdd.Checked;
        CG.A[12] = CBObjectivesFoundation.Checked;
        CG.A[13] = CBObjectivesFoundationAdd.Checked;
        CG.A[14] = CBAlbum.Checked;
        CG.A[15] = CBAlbumAdd.Checked;
        CG.A[16] = CBPartner.Checked;
        CG.A[17] = CBPartnerAdd.Checked;
        CG.A[18] = CBTwitter.Checked;
        CG.A[19] = CBTwitterAdd.Checked;
        CG.A[20] = CBMessage.Checked;
        CG.A[21] = CBMessageAdd.Checked;
        CG.A[22] = CBTrikerUser.Checked;
        CG.A[23] = CBTrikerAdmin.Checked;
        CG.A[24] = CBVideo.Checked;
        CG.A[25] = CBVideoAdd.Checked;
        CG.A[26] = false;
        CG.A[27] = false;
        CG.A[28] = false;
        CG.A[29] = false;

        // View
        CG.A[30] = CBVillageView.Checked;
        CG.A[31] = CBBeneficiaryStatusView.Checked;
        CG.A[32] = CBTypeOfDwellingView.Checked;
        CG.A[33] = CBMonthlyIncomeView.Checked;
        CG.A[34] = CBHousingStatusView.Checked;
        CG.A[35] = CBSupportTypeView.Checked;
        CG.A[36] = CBBeneficiaryFamilyView.Checked;
        CG.A[37] = CBBeneficiaryRelationshipView.Checked;
        CG.A[38] = CBBeneficiaryBySearchView.Checked;
        CG.A[39] = CBBeneficiaryByView.Checked;
        CG.A[40] = CBSearchStatusView.Checked;
        CG.A[41] = CBSearchStatusDetailsView.Checked;
        CG.A[42] = CBAcceptanceDecisionView.Checked;
        CG.A[43] = CBAcceptanceDecisionDetailsView.Checked;
        CG.A[44] = CBTecisionToExcludeView.Checked;
        CG.A[45] = CBTecisionToExcludeDetailsView.Checked;
        CG.A[46] = CVisitBApprovalView.Checked;
        CG.A[47] = CBVisitNotApprovedView.Checked;
        CG.A[48] = CBVisitDetailsView.Checked;
        CG.A[49] = CBVisitReportView.Checked;
        CG.A[50] = CBVisitReportDetailsView.Checked;
        CG.A[51] = CBRe_beneficiaryView.Checked;
        CG.A[52] = CBRe_beneficiaryDetailsView.Checked;
        CG.A[53] = CBExclusionOfTheBeneficiaryView.Checked;
        CG.A[54] = CBExclusionOfTheBeneficiaryDetailsView.Checked;
        CG.A[55] = CBConvertedCasesView.Checked;
        CG.A[56] = CBConvertedCasesWaitingForApprovalView.Checked;
        CG.A[57] = CBConvertedCasesDetailsView.Checked;
        CG.A[58] = CBExchangeOrdersView.Checked;
        CG.A[59] = CBSupportByBeneficiaryView.Checked;
        CG.A[60] = CBAddThePriceToOrderView.Checked;
        CG.A[61] = CBAffiliationView.Checked;
        CG.A[62] = CBCategoryView.Checked;
        CG.A[63] = CBProductView.Checked;
        CG.A[64] = CBProductByAffiliationView.Checked;
        CG.A[65] = CBProductByCategoryView.Checked;
        CG.A[66] = CBStoragePlacesView.Checked;
        CG.A[67] = CBWarehousebyContainedView.Checked;
        CG.A[68] = CBWarehousebyIssuedView.Checked;
        CG.A[69] = CBContainedAndIssuedView.Checked;

        // Add
        CG.A[70] = CBSettingMainAdd.Checked;
        CG.A[71] = CBSettingTitleAdd.Checked;
        CG.A[72] = CBSettingDataAdd.Checked;
        CG.A[73] = CBSettingAboutAdd.Checked;
        CG.A[74] = CBAddBeneficiaryAdd.Checked;
        CG.A[75] = CBBeneficiaryAddBoysAdd.Checked;
        CG.A[76] = CBVillageAdd.Checked;
        CG.A[77] = CBBeneficiaryStatusAdd.Checked;
        CG.A[78] = CBTypeOfDwellingAdd.Checked;
        CG.A[79] = CBMonthlyIncomeAdd.Checked;
        CG.A[80] = CBHousingStatusAdd.Checked;
        CG.A[81] = CBSupportTypeAdd.Checked;
        CG.A[82] = CBBeneficiaryFamilyAdd.Checked;
        CG.A[83] = CBBeneficiaryRelationshipAdd.Checked;
        CG.A[84] = CBSearchStatusAdd.Checked;
        CG.A[85] = CBSearchStatusManagerAdd.Checked;
        CG.A[86] = CBSearchStatusLagnatAdd.Checked;
        CG.A[87] = CBAcceptanceDecisionAdd.Checked;
        CG.A[88] = CBAcceptanceDecisionApprovedAdd.Checked;
        CG.A[89] = CBTecisionToExcludeAdd.Checked;
        CG.A[90] = CBTecisionToExcludeApprovedAdd.Checked;
        CG.A[91] = CBAfieldVisitAdd.Checked;
        CG.A[92] = CBAfieldVisitPendingApprovalAdd.Checked;
        CG.A[93] = CBAfieldVisitPendingApprovalByRaeesAdd.Checked;
        CG.A[94] = CBVisitReportAdd.Checked;
        CG.A[95] = CBVisitReportByModerAdd.Checked;
        CG.A[96] = CBVisitReportByRaeesAllagnahAdd.Checked;
        CG.A[97] = CBRe_beneficiaryAdd.Checked;
        CG.A[98] = CBRe_beneficiaryByModerAdd.Checked;
        CG.A[99] = CBRe_beneficiaryByRaeesAdd.Checked;
        CG.A[100] = CBExclusionOfTheBeneficiaryAdd.Checked;
        CG.A[101] = CBExclusionOfTheBeneficiaryByModerAdd.Checked;
        CG.A[102] = CBExclusionOfTheBeneficiaryByRaeesAdd.Checked;
        CG.A[103] = CBConvertedCasesAdd.Checked;
        CG.A[104] = CBConvertedCasesByModerAdd.Checked;
        CG.A[105] = CBProductMatterOfExchangeAdd.Checked;
        CG.A[106] = CBProductStorekeeperAdd.Checked;
        CG.A[107] = CBProductApprovalOfTheDirectorAdd.Checked;
        CG.A[108] = CBProductCashierAdd.Checked;
        CG.A[109] = CBProductChairmanOfTheBoardAdd.Checked;
        CG.A[110] = CBProductFileSearchersAdd.Checked;
        CG.A[111] = CBAffiliationAdd.Checked;
        CG.A[112] = CBCategoryAdd.Checked;
        CG.A[113] = CBProductAdd.Checked;
        CG.A[114] = CBStoragePlacesAdd.Checked;
        CG.A[115] = CBProductWarehouseStorekeeperAdd.Checked;
        CG.A[116] = CBProductWarehouseApprovalOfTheDirectorAdd.Checked;
        CG.A[117] = CBProductWarehouseCashierAdd.Checked;
        CG.A[118] = CBProductWarehouseChairmanOfTheBoardAdd.Checked;
        CG.A[119] = CBManageProductShippingWarehouseAdd.Checked;
        CG.A[120] = CBBeneficiaryByRaeesAlLagnah.Checked;
        CG.A[121] = CBBeneficiaryByModer.Checked;
        CG.A[122] = CBBeneficiaryByRaeesAlMaglis.Checked;

        CG.A[123] = CBTemporaryExclusionView.Checked;
        CG.A[124] = CBTemporaryExclusionDetails.Checked;
        CG.A[125] = CBTemporaryExclusionAdd.Checked;
        CG.A[126] = CBTemporaryExclusionByModer.Checked;
        CG.A[127] = CBTemporaryExclusionByRaees.Checked;
        CG.A[128] = CBFinancialStatisticsView.Checked;
        CG.A[129] = CBProductViceBoardAdd.Checked;
        CG.A[130] = CBIDBillView.Checked;
        CG.A[131] = CBInitiativesView.Checked;
        CG.A[132] = CBInitiativesAdd.Checked;

        CG.A[133] = CBCategoryZakatView.Checked;
        CG.A[134] = CBCategoryZakatAdd.Checked;
        CG.A[135] = CBZakatAlfiterBillView.Checked;
        CG.A[136] = CBZakatAlfiterBillAdd.Checked;
        CG.A[137] = CBZakatAlfiterBillInCome.Checked;
        CG.A[138] = false;
        CG.A[139] = CBZakatAlfiterBillAllowAmeenAlsondoq.Checked;
        CG.A[140] = CBZakatAlfiterBillAllowRaeesAlMajlis.Checked;
        CG.A[141] = CBZakatAlfiterBillAllowAmeenAlMostodaa.Checked;
        CG.A[142] = CBGeneralAssembly.Checked;
        CG.A[143] = CBGeneralAssemblyAdd.Checked;
        CG.A[144] = CBGeneralAssemblyAllow.Checked;
        CG.A[145] = CBGeneralAssemblyBill.Checked;
        CG.A[146] = CBGeneralAssemblyBillAdd.Checked;
        CG.A[147] = CBGeneralAssemblyBillAmeen.Checked;
        CG.A[148] = CBGeneralAssemblyBillRaees.Checked;

        CG.A[149] = CBHRMSettingAdd.Checked;
        CG.A[150] = CBHRMEmpDetialsView.Checked;
        CG.A[151] = CBHRMEmpDetialsAdd.Checked;
        CG.A[152] = CBHRMEmpSalaeryView.Checked;
        CG.A[153] = CBHRMEmpSalaeryAdd.Checked;
        CG.A[154] = CBHRMJobAssignmentView.Checked;
        CG.A[155] = CBHRMJobAssignmentAdd.Checked;
        CG.A[156] = CBHRMJobAssignmentModerAdd.Checked;
        CG.A[157] = CBHRMCompensatoryView.Checked;
        CG.A[158] = CBHRMLeaveCategoryView.Checked;
        CG.A[159] = CBHRMLeaveCategoryListView.Checked;
        CG.A[160] = CBHRMCompensatoryAdd.Checked;
        CG.A[161] = CBHRMLeaveCategoryAdd.Checked;
        CG.A[162] = CBHRMLeaveCategoryModerAdd.Checked;
        CG.A[163] = CBHRMLeaveCategoryRaeesAdd.Checked;
        CG.A[164] = CBHRMAccountableView.Checked;
        CG.A[165] = CBHRMAccountableAdd.Checked;
        CG.A[166] = CBHRMAccountableModerAdd.Checked;
        CG.A[167] = CBHRMResolvedView.Checked;
        CG.A[168] = CBHRMResolvedAdd.Checked;
        CG.A[169] = CBHRMResolvedModerAdd.Checked;
        CG.A[170] = CBHRMResolvedRaeesAdd.Checked;
        CG.A[171] = CBHRMLoanView.Checked;
        CG.A[172] = CBHRMLoanAdd.Checked;
        CG.A[173] = CBHRMLoanRepaymentAdd.Checked;
        CG.A[174] = CBHRMLoanModerAdd.Checked;
        CG.A[175] = CBHRMLoanRaeesAdd.Checked;
        CG.A[176] = CBHRMMandateView.Checked;
        CG.A[177] = CBHRMMandateListView.Checked;
        CG.A[178] = CBHRMMandateAdd.Checked;
        CG.A[179] = CBHRMMandateModerAdd.Checked;
        CG.A[180] = CBHRMOvertimeView.Checked;
        CG.A[181] = CBHRMOvertimeListView.Checked;
        CG.A[182] = CBHRMOvertimeAdd.Checked;
        CG.A[183] = CBHRMOvertimeModerAdd.Checked;
        CG.A[184] = CBHRMBonusesView.Checked;
        CG.A[185] = CBHRMBonusesListView.Checked;
        CG.A[186] = CBHRMBonusesAdd.Checked;
        CG.A[187] = CBHRMBonusesModerAdd.Checked;
        CG.A[188] = CBHRMAttendanceEntryView.Checked;
        CG.A[189] = CBHRMAttendanceEntryAllView.Checked;
        CG.A[190] = CBHRMAttendanceEntryAdd.Checked;
        CG.A[191] = CBHRMAddSalaryView.Checked;
        CG.A[192] = CBHRMAddSalaryListView.Checked;
        CG.A[193] = CBHRMAddSalaryAdd.Checked;
        CG.A[194] = CBHRMPermissionView.Checked;
        CG.A[195] = CBHRMPermissionAdd.Checked;
        CG.A[196] = CBHRMPermissionModerAdd.Checked;
        CG.A[197] = CBHRMPermissionRaeesAdd.Checked;
        CG.A[198] = CBHRMWarningView.Checked;
        CG.A[199] = CBHRMWarningAdd.Checked;
        CG.A[200] = CBHRMWarningModerAdd.Checked;
        CG.A[201] = CBHRMWarningRaeesAdd.Checked;
        CG.A[202] = false;
        CG.A[203] = false;
        CG.A[204] = false;
        CG.A[205] = false;
        CG.A[206] = false;
        CG.A[207] = false;
        CG.A[208] = false;
        CG.A[209] = false;
        CG.A[210] = false;
        CG.A[211] = false;
        CG.A[212] = false;
        CG.A[213] = false;
        CG.A[214] = false;
        CG.A[215] = false;
        CG.A[216] = false;
        CG.A[217] = false;
        CG.A[218] = false;
        CG.A[219] = false;
        CG.A[220] = false;
        CG.A[221] = false;
        CG.A[222] = false;
        CG.A[223] = false;
        CG.A[224] = false;
        CG.A[225] = false;
        CG.A[226] = false;
        CG.A[227] = false;
        CG.A[228] = false;
        CG.A[229] = false;
        CG.A[230] = false;
        CG.A[231] = false;
        CG.A[232] = false;
        CG.A[233] = false;
        CG.A[234] = false;
        CG.A[235] = false;
        CG.A[236] = false;
        CG.A[237] = false;
        CG.A[238] = false;
        CG.A[239] = false;
        CG.A[240] = false;
        CG.A[241] = false;
        CG.A[242] = false;
        CG.A[243] = false;
        CG.A[244] = false;
        CG.A[245] = false;
        CG.A[246] = false;
        CG.A[247] = false;
        CG.A[248] = false;
        CG.A[249] = false;
        CG.BArnGroup_Add();
        lblMessage.Text = "تم إضافة البيانات بنجاح";
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        if (Attach_Repostry_SMS_Send_.AllSendSystemSetting())
            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة صلاحيات للنظام \n بإسم :" + txtTitleGroup.Text.Trim(), "BerArn", "Add", Test_Saddam.FGetIDUsiq());
    }

}