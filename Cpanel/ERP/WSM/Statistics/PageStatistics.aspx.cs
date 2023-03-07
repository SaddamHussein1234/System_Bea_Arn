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

public partial class Cpanel_ERP_WSM_Statistics_PageStatistics : System.Web.UI.Page
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
        Response.Redirect("PageStatistics.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = true;
            GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlDataPrint;
            if (GVFinancialStatistics.Rows.Count > 13)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVFinancialStatistics.Rows.Count <= 13)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
        FGetProducts();
    }

    private void FGetProducts()
    {
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = false;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategoryName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_();
            MIKDD.IDCheck = "GetStaticByProject";
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
                txtTitle.Text = " الإحصاء المالي لفواتير المستودع حسب المشاريع الموضحة , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();

                lbl_Years.Text = XYearsName;
                GVFinancialStatistics.DataSource = dt;
                GVFinancialStatistics.DataBind();

                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
                FGetProductsAll();
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

    private void FGetProductsAll()
    {
        WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_();
        MIKDD.IDCheck = "GetStaticByProjectAll";
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
            RPTProductAll.DataSource = dt;
            RPTProductAll.DataBind();
        }
    }

    int CoutAosar = 0, CoutCard = 0;
    decimal sumTotal = 0;
    
    protected void GVFinancialStatistics_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id

                sumTotal += decimal.Parse(Sum.Text);
                lblSum.Text = sumTotal.ToString();

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblSum.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();
                lblMony.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
    }

}