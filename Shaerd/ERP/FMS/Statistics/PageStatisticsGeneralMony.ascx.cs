using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.GAM;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_ERP_FMS_Statistics_PageStatisticsGeneralMony : System.Web.UI.UserControl
{
    public string XYears = string.Empty, XAccount = string.Empty, XYearsName = string.Empty, XAccountName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAccount.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsGeneralMony.aspx");
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
            string XMony = ClassSaddam.FGetMonySa();
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBAccount.Items)
                XAccount += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBAccount.Items)
                XAccountName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            txtTitleReceipt.Text = " الإحصاء المالي للأموال الواردة , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
            txtTitleCashing.Text = " الإحصاء المالي للأموال المصروفة , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
            lbl_Years.Text = XYearsName;
            lblboxReceipt.Text = ClassSaddam.FAccountbox(); lblbankReceipt.Text = ClassSaddam.FAccountbank(); lblDonate_PublicReceipt.Text = ClassSaddam.FAccountDonate_PublicReceipt();
            lblboxCashing.Text = lblboxReceipt.Text; lblbankCashing.Text = lblbankReceipt.Text; lblDonate_PublicCashing.Text = lblDonate_PublicReceipt.Text;

            decimal XSumBoxReceipt = 0, XSumBankReceipt = 0, XSumDonate_PublicReceipt = 0;
            decimal XSumBoxCashing = 0, XSumBankCashing = 0, XSumDonate_PublicCashing = 0;
            if (XAccount.Substring(0, XAccount.Length - 1).Contains(ClassSaddam.FAccountbox()))
            {
                XSumBoxReceipt = Repostry_Receipt_.FGetSumReceipt("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbox()) +
                Repostry_Cash_Donation_.FGetSumCash_Donation("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbox()) +
                ClassGeneral_Assmply_Bill.FGetSumGeneral_Assmply_Bill(XYearsName.Substring(0, XYearsName.Length - 1), ClassSaddam.FAccountbox(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

                XSumBoxCashing = Repostry_Cashing_.FGetSumCashing("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbox()) +
                WSM_Repostry_Operating_Expenses_.FGetSumOperating_Expenses("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbox()) +
                Repostry_BenaaAndTarmim_.FGetSumBenaaAndTarmim("GetSumStaticGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbox(), string.Empty) +
                Repostry_SupportForPrisms_.FGetSumSupportForPrisms("GetSumStaticGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbox(), string.Empty);
            }
            lblSumboxReceipt.Text = XSumBoxReceipt.ToString() + " " + XMony; lblSumboxCashing.Text = XSumBoxCashing.ToString() + " " + XMony;

            if (XAccount.Substring(0, XAccount.Length - 1).Contains(ClassSaddam.FAccountbank()))
            {
                XSumBankReceipt = Repostry_Receipt_.FGetSumReceipt("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbank()) +
                    Repostry_Cash_Donation_.FGetSumCash_Donation("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbank()) +
                    ClassGeneral_Assmply_Bill.FGetSumGeneral_Assmply_Bill(XYearsName.Substring(0, XYearsName.Length - 1), ClassSaddam.FAccountbank(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

                XSumBankCashing = Repostry_Cashing_.FGetSumCashing("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbank()) +
                WSM_Repostry_Operating_Expenses_.FGetSumOperating_Expenses("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbank()) +
                Repostry_BenaaAndTarmim_.FGetSumBenaaAndTarmim("GetSumStaticGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbank(), string.Empty) +
                Repostry_SupportForPrisms_.FGetSumSupportForPrisms("GetSumStaticGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountbank(), string.Empty);
            }
            lblSumbankReceipt.Text = XSumBankReceipt.ToString() + " " + XMony; lblSumbankCashing.Text = XSumBankCashing.ToString() + " " + XMony;

            if (XAccount.Substring(0, XAccount.Length - 1).Contains(ClassSaddam.FAccountDonate_PublicReceipt()))
            {
                XSumDonate_PublicReceipt = Repostry_Receipt_.FGetSumReceipt("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountDonate_PublicReceipt()) +
                Repostry_Cash_Donation_.FGetSumCash_Donation("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountDonate_PublicReceipt()) +
                ClassGeneral_Assmply_Bill.FGetSumGeneral_Assmply_Bill(XYearsName.Substring(0, XYearsName.Length - 1), ClassSaddam.FAccountDonate_PublicReceipt(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

                XSumDonate_PublicCashing = Repostry_Cashing_.FGetSumCashing("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountDonate_PublicReceipt()) +
               WSM_Repostry_Operating_Expenses_.FGetSumOperating_Expenses("GetSumStaticGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountDonate_PublicReceipt()) +
               Repostry_BenaaAndTarmim_.FGetSumBenaaAndTarmim("GetSumStaticGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountDonate_PublicReceipt(), string.Empty) +
               Repostry_SupportForPrisms_.FGetSumSupportForPrisms("GetSumStaticGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), ClassSaddam.FAccountDonate_PublicReceipt(), string.Empty);
            }
            lblSumDonate_PublicReceipt.Text = XSumDonate_PublicReceipt.ToString() + " " + XMony; lblSumDonate_PublicCashing.Text = XSumDonate_PublicCashing.ToString() + " " + XMony;

            lblSumReceiptAll.Text = Repostry_Receipt_.FGetSumReceipt("GetSumStaticY_AGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), XAccount.Substring(0, XAccount.Length - 1)).ToString() + " " + XMony;
            lblSumCash_DonationAll.Text = Repostry_Cash_Donation_.FGetSumCash_Donation("GetSumStaticY_AGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), XAccount.Substring(0, XAccount.Length - 1)).ToString() + " " + XMony;

            lblSumCashingAll.Text = Repostry_Cashing_.FGetSumCashing("GetSumStaticY_AGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), XAccount.Substring(0, XAccount.Length - 1)).ToString() + " " + XMony;
            lblSumRestorationAndConstructionAll.Text = Repostry_BenaaAndTarmim_.FGetSumBenaaAndTarmim("GetSumStaticY_AGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), XAccount.Substring(0, XAccount.Length - 1), string.Empty).ToString() + " " + XMony;
            lblSumSupportForPrismAll.Text = Repostry_SupportForPrisms_.FGetSumSupportForPrisms("GetSumStaticY_AGeneral", Guid.Empty, 0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), XAccount.Substring(0, XAccount.Length - 1), string.Empty).ToString() + " " + XMony;
            lblSumOperating_ExpensesAll.Text = WSM_Repostry_Operating_Expenses_.FGetSumOperating_Expenses("GetSumStaticY_AGeneral", Guid.Empty, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), XAccount.Substring(0, XAccount.Length - 1)).ToString() + " " + XMony;

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(XSumBoxReceipt + XSumBankReceipt + XSumDonate_PublicReceipt, currencies[Convert.ToInt32(0)]);
            lblSumWordReceipt.Text = toWord.ConvertToArabic();
            lbl_SumReceipt.Text = (XSumBoxReceipt + XSumBankReceipt + XSumDonate_PublicReceipt).ToString() + " " + XMony;

            ToWord toWordCashing = new ToWord(XSumBoxCashing + XSumBankCashing + XSumDonate_PublicCashing, currencies[Convert.ToInt32(0)]);
            lblSumWordCashing.Text = toWordCashing.ConvertToArabic();
            lbl_SumCashing.Text = (XSumBoxCashing + XSumBankCashing + XSumDonate_PublicCashing).ToString() + " " + XMony;
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