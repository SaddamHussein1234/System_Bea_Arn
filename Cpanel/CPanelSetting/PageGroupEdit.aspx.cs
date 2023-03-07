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

public partial class Cpanel_CPanelSetting_PageGroupEdit : System.Web.UI.Page
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
            FGetData();
        }
    }

    private void FGetData()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * From tbl_Group_Arn With(NoLock) Where IDUniqGroup = @0", Convert.ToString(Request.QueryString["ID"]));
        if (dt.Rows.Count > 0)
        {
            txtTitleGroup.Text = dt.Rows[0]["NameGroup"].ToString();
            Session["OldName"] = txtTitleGroup.Text.Trim();
            CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActiveGroup"]);
            //نظام المؤسسة 
            CBGroupView.Checked = Convert.ToBoolean(dt.Rows[0]["A5"]);
            CBGroupAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A6"]);
            CBAdminView.Checked = Convert.ToBoolean(dt.Rows[0]["A7"]);
            CBAdminAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A8"]);
            CBMenu.Checked = Convert.ToBoolean(dt.Rows[0]["A9"]);
            CBMenuAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A10"]);
            CBArticle.Checked = Convert.ToBoolean(dt.Rows[0]["A11"]);
            CBArticleAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A12"]);
            CBObjectivesFoundation.Checked = Convert.ToBoolean(dt.Rows[0]["A13"]);
            CBObjectivesFoundationAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A14"]);
            CBAlbum.Checked = Convert.ToBoolean(dt.Rows[0]["A15"]);
            CBAlbumAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A16"]);
            CBPartner.Checked = Convert.ToBoolean(dt.Rows[0]["A17"]);
            CBPartnerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A18"]);
            CBTwitter.Checked = Convert.ToBoolean(dt.Rows[0]["A19"]);
            CBTwitterAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A20"]);
            CBMessage.Checked = Convert.ToBoolean(dt.Rows[0]["A21"]);
            CBMessageAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A22"]);
            CBTrikerUser.Checked = Convert.ToBoolean(dt.Rows[0]["A23"]);
            CBTrikerAdmin.Checked = Convert.ToBoolean(dt.Rows[0]["A24"]);
            CBVideo.Checked = Convert.ToBoolean(dt.Rows[0]["A25"]);
            CBVideoAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A26"]);
            //View
            CBVillageView.Checked = Convert.ToBoolean(dt.Rows[0]["A31"]);
            CBBeneficiaryStatusView.Checked = Convert.ToBoolean(dt.Rows[0]["A32"]);
            CBTypeOfDwellingView.Checked = Convert.ToBoolean(dt.Rows[0]["A33"]);
            CBMonthlyIncomeView.Checked = Convert.ToBoolean(dt.Rows[0]["A34"]);
            CBHousingStatusView.Checked = Convert.ToBoolean(dt.Rows[0]["A35"]);
            CBSupportTypeView.Checked = Convert.ToBoolean(dt.Rows[0]["A36"]);
            CBBeneficiaryFamilyView.Checked = Convert.ToBoolean(dt.Rows[0]["A37"]);
            CBBeneficiaryRelationshipView.Checked = Convert.ToBoolean(dt.Rows[0]["A38"]);
            CBBeneficiaryBySearchView.Checked = Convert.ToBoolean(dt.Rows[0]["A39"]);
            CBBeneficiaryByView.Checked = Convert.ToBoolean(dt.Rows[0]["A40"]);
            CBSearchStatusView.Checked = Convert.ToBoolean(dt.Rows[0]["A41"]);
            CBSearchStatusDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A42"]);
            CBAcceptanceDecisionView.Checked = Convert.ToBoolean(dt.Rows[0]["A43"]);
            CBAcceptanceDecisionDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A44"]);
            CBTecisionToExcludeView.Checked = Convert.ToBoolean(dt.Rows[0]["A45"]);
            CBTecisionToExcludeDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A46"]);
            CVisitBApprovalView.Checked = Convert.ToBoolean(dt.Rows[0]["A47"]);
            CBVisitNotApprovedView.Checked = Convert.ToBoolean(dt.Rows[0]["A48"]);
            CBVisitDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A49"]);
            CBVisitReportView.Checked = Convert.ToBoolean(dt.Rows[0]["A50"]);
            CBVisitReportDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A51"]);
            CBRe_beneficiaryView.Checked = Convert.ToBoolean(dt.Rows[0]["A52"]);
            CBRe_beneficiaryDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A53"]);
            CBExclusionOfTheBeneficiaryView.Checked = Convert.ToBoolean(dt.Rows[0]["A54"]);
            CBExclusionOfTheBeneficiaryDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A55"]);
            CBConvertedCasesView.Checked = Convert.ToBoolean(dt.Rows[0]["A56"]);
            CBConvertedCasesWaitingForApprovalView.Checked = Convert.ToBoolean(dt.Rows[0]["A57"]);
            CBConvertedCasesDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A58"]);
            CBExchangeOrdersView.Checked = Convert.ToBoolean(dt.Rows[0]["A59"]);
            CBSupportByBeneficiaryView.Checked = Convert.ToBoolean(dt.Rows[0]["A60"]);
            CBAddThePriceToOrderView.Checked = Convert.ToBoolean(dt.Rows[0]["A61"]);
            CBAffiliationView.Checked = Convert.ToBoolean(dt.Rows[0]["A62"]);
            CBCategoryView.Checked = Convert.ToBoolean(dt.Rows[0]["A63"]);
            CBProductView.Checked = Convert.ToBoolean(dt.Rows[0]["A64"]);
            CBProductByAffiliationView.Checked = Convert.ToBoolean(dt.Rows[0]["A65"]);
            CBProductByCategoryView.Checked = Convert.ToBoolean(dt.Rows[0]["A66"]);
            CBStoragePlacesView.Checked = Convert.ToBoolean(dt.Rows[0]["A67"]);
            CBWarehousebyContainedView.Checked = Convert.ToBoolean(dt.Rows[0]["A68"]);
            CBWarehousebyIssuedView.Checked = Convert.ToBoolean(dt.Rows[0]["A69"]);
            CBContainedAndIssuedView.Checked = Convert.ToBoolean(dt.Rows[0]["A70"]);
            //Add
            CBSettingAboutAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A4"]);
            CBSettingMainAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A71"]);
            CBSettingTitleAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A72"]);
            CBSettingDataAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A73"]);
            CBSettingAboutAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A74"]);
            CBAddBeneficiaryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A75"]);
            CBBeneficiaryAddBoysAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A76"]);
            CBVillageAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A77"]);
            CBBeneficiaryStatusAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A78"]);
            CBTypeOfDwellingAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A79"]);
            CBMonthlyIncomeAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A80"]);
            CBHousingStatusAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A81"]);
            CBSupportTypeAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A82"]);
            CBBeneficiaryFamilyAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A83"]);
            CBBeneficiaryRelationshipAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A84"]);
            CBSearchStatusAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A85"]);
            CBSearchStatusManagerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A86"]);
            CBSearchStatusLagnatAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A87"]);
            CBAcceptanceDecisionAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A88"]);
            CBAcceptanceDecisionApprovedAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A89"]);
            CBTecisionToExcludeAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A90"]);
            CBTecisionToExcludeApprovedAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A91"]);
            CBAfieldVisitAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A92"]);
            CBAfieldVisitPendingApprovalAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A93"]);
            CBAfieldVisitPendingApprovalByRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A94"]);
            CBVisitReportAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A95"]);
            CBVisitReportByModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A96"]);
            CBVisitReportByRaeesAllagnahAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A97"]);
            CBRe_beneficiaryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A98"]);
            CBRe_beneficiaryByModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A99"]);
            CBRe_beneficiaryByRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A100"]);
            CBExclusionOfTheBeneficiaryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A101"]);
            CBExclusionOfTheBeneficiaryByModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A102"]);
            CBExclusionOfTheBeneficiaryByRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A103"]);
            CBConvertedCasesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A104"]);
            CBConvertedCasesByModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A105"]);
            CBProductMatterOfExchangeAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A106"]);
            CBProductStorekeeperAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A107"]);
            CBProductApprovalOfTheDirectorAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A108"]);
            CBProductCashierAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A109"]);
            CBProductChairmanOfTheBoardAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A110"]);
            CBProductFileSearchersAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A111"]);
            CBAffiliationAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A112"]);
            CBCategoryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A113"]);
            CBProductAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A114"]);
            CBStoragePlacesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A115"]);
            CBProductWarehouseStorekeeperAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A116"]);
            CBProductWarehouseApprovalOfTheDirectorAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A117"]);
            CBProductWarehouseCashierAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A118"]);
            CBProductWarehouseChairmanOfTheBoardAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A119"]);
            CBManageProductShippingWarehouseAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A120"]);
            CBBeneficiaryByRaeesAlLagnah.Checked = Convert.ToBoolean(dt.Rows[0]["A121"]);
            CBBeneficiaryByModer.Checked = Convert.ToBoolean(dt.Rows[0]["A122"]);
            CBBeneficiaryByRaeesAlMaglis.Checked = Convert.ToBoolean(dt.Rows[0]["A123"]);
            CBTemporaryExclusionView.Checked = Convert.ToBoolean(dt.Rows[0]["A124"]);
            CBTemporaryExclusionDetails.Checked = Convert.ToBoolean(dt.Rows[0]["A125"]);
            CBTemporaryExclusionAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A126"]);
            CBTemporaryExclusionByModer.Checked = Convert.ToBoolean(dt.Rows[0]["A127"]);
            CBTemporaryExclusionByRaees.Checked = Convert.ToBoolean(dt.Rows[0]["A128"]);
            CBFinancialStatisticsView.Checked = Convert.ToBoolean(dt.Rows[0]["A129"]);
            CBProductViceBoardAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A130"]);
            CBIDBillView.Checked = Convert.ToBoolean(dt.Rows[0]["A131"]);
            CBInitiativesView.Checked = Convert.ToBoolean(dt.Rows[0]["A132"]);
            CBInitiativesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A133"]);
            CBCategoryZakatView.Checked = Convert.ToBoolean(dt.Rows[0]["A134"]);
            CBCategoryZakatAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A135"]);
            CBZakatAlfiterBillView.Checked = Convert.ToBoolean(dt.Rows[0]["A136"]);
            CBZakatAlfiterBillAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A137"]);
            CBZakatAlfiterBillInCome.Checked = Convert.ToBoolean(dt.Rows[0]["A138"]);
            //CG.A[138] = false;
            CBZakatAlfiterBillAllowAmeenAlsondoq.Checked = Convert.ToBoolean(dt.Rows[0]["A140"]);
            CBZakatAlfiterBillAllowRaeesAlMajlis.Checked = Convert.ToBoolean(dt.Rows[0]["A141"]);
            CBZakatAlfiterBillAllowAmeenAlMostodaa.Checked = Convert.ToBoolean(dt.Rows[0]["A142"]);
            CBGeneralAssembly.Checked = Convert.ToBoolean(dt.Rows[0]["A143"]);
            CBGeneralAssemblyAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A144"]);
            CBGeneralAssemblyAllow.Checked = Convert.ToBoolean(dt.Rows[0]["A145"]);
            CBGeneralAssemblyBill.Checked = Convert.ToBoolean(dt.Rows[0]["A146"]);
            CBGeneralAssemblyBillAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A147"]);
            CBGeneralAssemblyBillAmeen.Checked = Convert.ToBoolean(dt.Rows[0]["A148"]);
            CBGeneralAssemblyBillRaees.Checked = Convert.ToBoolean(dt.Rows[0]["A149"]);
            CBHRMSettingAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A150"]);
            CBHRMEmpDetialsView.Checked = Convert.ToBoolean(dt.Rows[0]["A151"]);
            CBHRMEmpDetialsAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A152"]);
            CBHRMEmpSalaeryView.Checked = Convert.ToBoolean(dt.Rows[0]["A153"]);
            CBHRMEmpSalaeryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A154"]);
            CBHRMJobAssignmentView.Checked = Convert.ToBoolean(dt.Rows[0]["A155"]);
            CBHRMJobAssignmentAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A156"]);
            CBHRMJobAssignmentModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A157"]);
            CBHRMCompensatoryView.Checked = Convert.ToBoolean(dt.Rows[0]["A158"]);
            CBHRMLeaveCategoryView.Checked = Convert.ToBoolean(dt.Rows[0]["A159"]);
            CBHRMLeaveCategoryListView.Checked = Convert.ToBoolean(dt.Rows[0]["A160"]);
            CBHRMCompensatoryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A161"]);
            CBHRMLeaveCategoryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A162"]);
            CBHRMLeaveCategoryModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A163"]);
            CBHRMLeaveCategoryRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A164"]);
            CBHRMAccountableView.Checked = Convert.ToBoolean(dt.Rows[0]["A165"]);
            CBHRMAccountableAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A166"]);
            CBHRMAccountableModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A167"]);
            CBHRMResolvedView.Checked = Convert.ToBoolean(dt.Rows[0]["A168"]);
            CBHRMResolvedAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A169"]);
            CBHRMResolvedModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A170"]);
            CBHRMResolvedRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A171"]);
            CBHRMLoanView.Checked = Convert.ToBoolean(dt.Rows[0]["A172"]);
            CBHRMLoanAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A173"]);
            CBHRMLoanRepaymentAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A174"]);
            CBHRMLoanModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A175"]);
            CBHRMLoanRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A176"]);
            CBHRMMandateView.Checked = Convert.ToBoolean(dt.Rows[0]["A177"]);
            CBHRMMandateListView.Checked = Convert.ToBoolean(dt.Rows[0]["A178"]);
            CBHRMMandateAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A179"]);
            CBHRMMandateModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A180"]);
            CBHRMOvertimeView.Checked = Convert.ToBoolean(dt.Rows[0]["A181"]);
            CBHRMOvertimeListView.Checked = Convert.ToBoolean(dt.Rows[0]["A182"]);
            CBHRMOvertimeAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A183"]);
            CBHRMOvertimeModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A184"]);
            CBHRMBonusesView.Checked = Convert.ToBoolean(dt.Rows[0]["A185"]);
            CBHRMBonusesListView.Checked = Convert.ToBoolean(dt.Rows[0]["A186"]);
            CBHRMBonusesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A187"]);
            CBHRMBonusesModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A188"]);
            CBHRMAttendanceEntryView.Checked = Convert.ToBoolean(dt.Rows[0]["A189"]);
            CBHRMAttendanceEntryAllView.Checked = Convert.ToBoolean(dt.Rows[0]["A190"]);
            CBHRMAttendanceEntryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A191"]);
            CBHRMAddSalaryView.Checked = Convert.ToBoolean(dt.Rows[0]["A192"]);
            CBHRMAddSalaryListView.Checked = Convert.ToBoolean(dt.Rows[0]["A193"]);
            CBHRMAddSalaryAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A194"]);
            CBHRMPermissionView.Checked = Convert.ToBoolean(dt.Rows[0]["A195"]);
            CBHRMPermissionAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A196"]);
            CBHRMPermissionModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A197"]);
            CBHRMPermissionRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A198"]);
            CBHRMWarningView.Checked = Convert.ToBoolean(dt.Rows[0]["A199"]);
            CBHRMWarningAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A200"]);
            CBHRMWarningModerAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A201"]);
            CBHRMWarningRaeesAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A202"]);
        }
        else
            Response.Redirect("PageGroup.aspx");
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FChackName();
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
        if (txtTitleGroup.Text.Trim() != Session["OldName"].ToString())
        {
            DataTable dtUser = new DataTable();
            dtUser = ClassDataAccess.GetData("Select Top(1) NameGroup,IsDeleteGroup from tbl_Group_Arn With(NoLock) Where NameGroup = @0 And IsDeleteGroup = @1", txtTitleGroup.Text.Trim(), Convert.ToString(false));
            if (dtUser.Rows.Count > 0)
            {
                lblMessageWarning.Text = "تم إضافة المجموعة سابقاً ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
            else
            {
                Session["OldName"] = txtTitleGroup.Text.Trim();
                FGroupEdit();
            }
        }
        else
            FGroupEdit();
    }

    private void FGroupEdit()
    {
        ClassGroup CG = new ClassGroup();
        CG.IDUniqGroup = Convert.ToString(Request.QueryString["ID"]);
        CG.NameGroup = txtTitleGroup.Text.Trim();
        CG.IsActiveGroup = CBActive.Checked;
        CG.A[0] = false;
        CG.A[1] = false;
        CG.A[2] = false;
        CG.A[3] = false;
        CG.A[4] = CBGroupView.Checked;
        CG.A[5] = CBGroupAdd.Checked;
        CG.A[6] = CBAdminView.Checked;
        CG.A[7] = CBAdminAdd.Checked;
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

        CG.BGroupEdit();
        lblMessage.Text = "تم التعديل بنجاح ";
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        if (Attach_Repostry_SMS_Send_.AllSendSystemSetting())
            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "صلاحية للنظام \n بإسم :" + txtTitleGroup.Text.Trim(), "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
        FGetData();
        System.Threading.Thread.Sleep(100);
    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageGroup.aspx");
    }

}