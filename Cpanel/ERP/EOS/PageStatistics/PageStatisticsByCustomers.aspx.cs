using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
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

public partial class Cpanel_ERP_EOS_PageStatistics_PageStatisticsByCustomers : System.Web.UI.Page
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            ClassQuaem.FGetSupportType(0, CBCategory);
            ClassMosTafeed.FGetName(DLName);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBYears.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsByCustomers.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4OutHeader.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
        FGetExchange_Order_Details();
    }

    private void FGetExchange_Order_Details()
    {
        try
        {
            string XYears = string.Empty, XCategory = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
            decimal XSumExchange_Order = 0, XSumBenaaAndTarmim = 0, XSumSupportForPrisms = 0;
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblTitle.Text = "كشف حساب ماتم صرفة للمستفيد من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                lblNameMosTafeed.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblAlqariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblNumberAlSegelAlMadany.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblHalafAlMosTafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
                lblNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
            }

            WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_();
            MEOD.IDCheck = "GetBySumByCustomerAll";
            MEOD.IDItem = Guid.Empty;
            MEOD.ID_FinancialYear = Guid.Empty;
            MEOD.ID_Donor = Guid.Empty;
            MEOD.bill_Number = 1;
            MEOD.ID_MosTafeed = Convert.ToInt32(DLName.SelectedValue);
            MEOD.Start_Date = txtDateFrom.Text.Trim();
            MEOD.End_Date = txtDateTo.Text.Trim();
            MEOD.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MEOD.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MEOD.DataCheck3 = string.Empty;
            MEOD.IsActive = true;
            DataTable dtExchange = new DataTable();
            WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
            dtExchange = REOD.BWSM_Exchange_Order_Details_Manage(MEOD);
            if (dtExchange.Rows.Count > 0)
            {
                XSumExchange_Order = Convert.ToDecimal(dtExchange.Rows[0]["GetCount"]);
                lblSumExchange_Order.Text = dtExchange.Rows[0]["GetCount"].ToString();
            }

            XSumBenaaAndTarmim = Repostry_BenaaAndTarmim_.FGetSumBenaaAndTarmim("GetBySumByCustomerAll", Guid.Empty, Convert.ToInt32(DLName.SelectedValue), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(),
                XYears.Substring(0, XYears.Length - 1), XCategory.Substring(0, XCategory.Length - 1), string.Empty);
            lblSumBenaaAndTarmim.Text = XSumBenaaAndTarmim.ToString();

            XSumSupportForPrisms = Repostry_SupportForPrisms_.FGetSumSupportForPrisms("GetBySumByCustomerAll", Guid.Empty, Convert.ToInt32(DLName.SelectedValue), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(),
                XYears.Substring(0, XYears.Length - 1), XCategory.Substring(0, XCategory.Length - 1), string.Empty);
            lblSumSupportForPrisms.Text = XSumSupportForPrisms.ToString();

            lbl_Sum.Text = (XSumExchange_Order + XSumBenaaAndTarmim + XSumSupportForPrisms).ToString();
            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(XSumExchange_Order + XSumBenaaAndTarmim + XSumSupportForPrisms, currencies[Convert.ToInt32(0)]);
            lblSumWord.Text = toWord.ConvertToArabic();
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