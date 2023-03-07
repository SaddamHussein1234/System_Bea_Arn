using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Models;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_WSM_Statistics_PageStatisticsMony : System.Web.UI.Page
{
    public string XYears = string.Empty, XCategory = string.Empty, XYearsName = string.Empty, XCategoryName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClassQuaem.FGetSupportType(0, "'1','2','3'", CBCategory);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsMony.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Session["footable1"] = pnlDataPrint;
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
        FGetStaticByProjectMony();
    }

    private void FGetStaticByProjectMony()
    {
        try
        {
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategoryName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_();
            MIKDD.IDCheck = "GetStaticByProjectMony";
            MIKDD.ID_Item = Guid.Empty;
            MIKDD.bill_Number = 0;
            MIKDD.Start_Date = txtDateFrom.Text.Trim();
            MIKDD.End_Date = txtDateTo.Text.Trim();
            MIKDD.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MIKDD.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MIKDD.DataCheck3 = string.Empty;
            MIKDD.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_In_Kind_Donation_Details_ RIKDD = new WSM_Repostry_In_Kind_Donation_Details_();
            dt = RIKDD.BWSM_In_Kind_Donation_Details_Manage(MIKDD);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " الإحصاء المالي لفواتير المستودع وما تم صرفة , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                lblSumIn_Kind_Donation.Text = dt.Rows[0]["GetCount"].ToString() + " " + ClassSaddam.FGetMonySa();
                lblSumIn_Kind_Donation2.Text = lblSumIn_Kind_Donation.Text;
                FGetBySumByProjectMony(XYears.Substring(0, XYears.Length - 1), XCategory.Substring(0, XCategory.Length - 1));
                lbl_Years.Text = XYearsName;

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(dt.Rows[0]["GetCount"]), currencies[Convert.ToInt32(0)]);
                lblSumWordIn_Kind_Donation.Text = toWord.ConvertToArabic();

                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    private void FGetBySumByProjectMony(string XYear, string XCategory)
    {
        WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_();
        MEOD.IDCheck = "GetBySumByProjectMony";
        MEOD.ID_Item = Guid.Empty;
        MEOD.ID_FinancialYear = Guid.Empty;
        MEOD.ID_Donor = Guid.Empty;
        MEOD.bill_Number = 0;
        MEOD.ID_MosTafeed = 0;
        MEOD.Start_Date = txtDateFrom.Text.Trim();
        MEOD.End_Date = txtDateTo.Text.Trim();
        MEOD.DataCheck = DLType.SelectedValue;
        MEOD.DataCheck2 = XYear;
        MEOD.DataCheck3 = XCategory;
        MEOD.IsActive = true;
        DataTable dt = new DataTable();
        WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
        dt = REOD.BWSM_Exchange_Order_Details_Manage(MEOD);
        if (dt.Rows.Count > 0)
        {
            lblSumExchange_Order.Text = dt.Rows[0]["GetCount"].ToString() + " " + ClassSaddam.FGetMonySa();
            lblSumExchange_Order2.Text = lblSumExchange_Order.Text;

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(dt.Rows[0]["GetCount"]), currencies[Convert.ToInt32(0)]);
            lblSumWordExchange_Order.Text = toWord.ConvertToArabic();

        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
    }

}