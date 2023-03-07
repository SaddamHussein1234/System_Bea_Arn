using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_Count_PageStatisticFiles : System.Web.UI.Page
{
    public string XYears = string.Empty, XAdmin = string.Empty, XAccount = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            ClassAdmin_Arn.FGetAdminActive(CBAdmin);
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBAdmin.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAccount.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticFiles.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Session["footable1"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        pnlDataPrint.Visible = true;
        pnlSelect.Visible = false;
        IDFilter.Visible = false;
        pnlDataPrint.Visible = true;
        FGetStaticByProjectMony();
    }

    private void FGetStaticByProjectMony()
    {
        try
        {
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBAdmin.Items)
                XAdmin += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBAccount.Items)
                XAccount += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            txtTitleReceipt.Text = " الإحصاء العام للمالفات المؤرشفة , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();

            decimal XSumSSM = 0, XSumWSM = 0, XSumEOS = 0, XSumZSM = 0, XSumGAM = 0, XSumHRM = 0, XSumCRM = 0, XSumOM = 0;

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("111"))
            {
                XSumSSM = ClassDataAccess.FGetCountFiles("RasAlEstemarah", "EsmAlMostakhdem", "_DateUpdate", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    ClassDataAccess.FGetCountFiles("BahthHalatMostafeed", "IDAdmin", "DateAddReport", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    ClassDataAccess.FGetCountFiles("QararQobolMustafeed", "IDAdmin", "DateAddQara", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    ClassDataAccess.FGetCountFiles("ZeyarahMaydanyah", "IDAdmin", "DataAddAlZeyarah", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    ClassDataAccess.FGetCountFiles("ReportAlZyaratMedicalEquipments", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    ClassDataAccess.FGetCountFiles("ReportAlZyarat", "IDAdmin", "DateAddReport", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    ClassDataAccess.FGetCountFiles("EadatMostafeed", "IDAdmin", "DateAddOrder", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    ClassDataAccess.FGetCountFiles("TahweelAlHalah", "IDAdmin", "DateAddOrder", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false);
            }
            lblSumSSM.Text = XSumSSM.ToString() + " " + "<small> ملف </small>";

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("222"))
            {
                XSumWSM = WSM_Data_Access_Layer.FGetCountFiles("AffiliationShop", "IDAdmin", "DateAddAffiliation", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    WSM_Data_Access_Layer.FGetCountFiles("CategoryShop", "IDAdmin", "DateAddCategory", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    WSM_Data_Access_Layer.FGetCountFiles("ProductShop", "IDAdmin", "DateAddProduct", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    WSM_Data_Access_Layer.FGetCountFiles("StoragePlaces", "IDAdmin", "DateAddStorage", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                    WSM_Data_Access_Layer.FGetCountFiles("tbl_In_Kind_Donation_Bill", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    WSM_Data_Access_Layer.FGetCountFiles("tbl_In_Kind_Donation_Details_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    ClassDataAccess.FGetCountFiles("tbl_SupportForPrisms", "_CreatedBy_", "_CreatedDate_", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false);
            }
            lblSumWSM.Text = XSumWSM.ToString() + " " + "<small> ملف </small>";

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("333"))
            {
                XSumEOS = WSM_Data_Access_Layer.FGetCountFiles("tbl_Exchange_Order_Bill_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                WSM_Data_Access_Layer.FGetCountFiles("tbl_Exchange_Order_Details_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                ClassDataAccess.FGetCountFiles("BenaaAndTarmim", "_CreatedBy_", "_CreatedDate_", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                ClassDataAccess.FGetCountFiles("tbl_SupportForPrisms", "_CreatedBy_", "_CreatedDate_", "IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                ClassDataAccess.FGetCountFiles("ProductShopWarehouse", "_IDAdmin", "_DateAddProduct", "_IsDelete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false);
            }
            lblSumEOS.Text = XSumEOS.ToString() + " " + "<small> ملف </small>";

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("444"))
            {
                XSumZSM = ClassDataAccess.FGetCountFiles("tbl_Category_Zakat", "ID_Admin_", "DateAdd_Category_", "Is_Delete", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                 ClassDataAccess.FGetCountFiles("tbl_Warehouse_Zakat", "_CreatedBy_", "_CreatedDate_", "_IsDelete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                 ClassDataAccess.FGetCountFiles("tbl_Warehouse_Zakat_Bill", "_CreatedBy_", "_CreatedDate_", "_IsDelete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false);
            }
            lblSumZSM.Text = XSumZSM.ToString() + " " + "<small> ملف </small>";

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("555"))
            {
                XSumGAM = ClassDataAccess.FGetCountFiles("tbl_General_Assmply", "ID_Admin_", "Date_Add_", "Is_Delete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                ClassDataAccess.FGetCountFiles("tbl_General_Assmply_Bill", "ID_Admin_", "Date_Add_", "Is_Delete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false);
            }
            lblSumGAM.Text = XSumGAM.ToString() + " " + "<small> ملف </small>";

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("666"))
            {
                XSumHRM = HRM_Data_Access_Layer.FGetCountFiles("AccountableTypeMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("AllowanceMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("DeductionMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("DepartmentMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("DesignationMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EducationMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeAccountable", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeAttachment", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeAttendance", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeBonuses", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeGradeMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeJobAssignment", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeLeaveCategory", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeLeaveCompensatory", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeLoan", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeMandate", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeOverTime", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeePaidLoan", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeePaidSalary", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeePermission", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeResolved", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeSalary", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeTypeMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeWorkingDay", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("FinancialYearMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("HolidayMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("InterviewAttachment", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("InterviewMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("InvoiceDetail", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("InvoiceMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("LeaveCategoryMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("ResolvedPeriodMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("ShiftMaster", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                    HRM_Data_Access_Layer.FGetCountFiles("EmployeeJobAssignment", "CreatedBy", "CreatedDate", "IsActive", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);

            }
            lblSumHRM.Text = XSumHRM.ToString() + " " + "<small> ملف </small>";

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("777"))
            {
                XSumCRM = CRM_Data_Access_Layer.FGetCountFiles("_tbl_Company_Type_", "CreatedBy", "CreatedDate", "_Is_Delete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) +
                CRM_Data_Access_Layer.FGetCountFiles("_tbl_Company_", "CreatedBy", "CreatedDate", "_Is_Delete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) + 
                CRM_Data_Access_Layer.FGetCountFiles("_tbl_Remamber_", "CreatedBy", "CreatedDate", "_Is_Delete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false) + 
                CRM_Data_Access_Layer.FGetCountFiles("_tbl_Tricker_", "CreatedBy", "CreatedDate", "_Is_Delete_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false);
            }
            lblSumCRM.Text = XSumCRM.ToString() + " " + "<small> ملف </small>";

            if (XAccount.Substring(0, XAccount.Length - 1).Contains("777"))
            {
                XSumOM = OM_Data_Access_Layer.FGetCountFiles("tbl_Cash_Donation_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) +
                OM_Data_Access_Layer.FGetCountFiles("tbl_Cashing_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true) + 
                OM_Data_Access_Layer.FGetCountFiles("tbl_Customers_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                OM_Data_Access_Layer.FGetCountFiles("tbl_In_Kind_Donation_Bill", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                OM_Data_Access_Layer.FGetCountFiles("tbl_In_Kind_Donation_Details_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                OM_Data_Access_Layer.FGetCountFiles("tbl_Main_Items_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                OM_Data_Access_Layer.FGetCountFiles("tbl_Receipt_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
                OM_Data_Access_Layer.FGetCountFiles("tbl_Customers_", "_CreatedBy_", "_CreatedDate_", "_IsActive_", XAdmin.Substring(0, XAdmin.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
            }
            lblSumOM.Text = XSumOM.ToString() + " " + "<small> ملف </small>";

            lbl_SumAll.Text = (XSumSSM + XSumWSM + XSumEOS + XSumZSM + XSumGAM + XSumHRM + XSumCRM + XSumOM).ToString() + " " + "<small> ملف </small>";

        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
        pnlDataPrint.Visible = false;
    }

}