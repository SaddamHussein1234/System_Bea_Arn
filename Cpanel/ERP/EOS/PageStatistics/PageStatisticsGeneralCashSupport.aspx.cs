using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PageStatistics_PageStatisticsGeneralCashSupport : System.Web.UI.Page
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A129");
            XMony = ClassSaddam.FGetMonySa();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            ClassQuaem.FGetSupportType(0, "'5'", CBCategory);
            FSelectCheck(true);
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
        }
    }

    private void FSelectCheck(bool XValue)
    {
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = XValue; }
    }
    
    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsGeneralCashSupport.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = true;
            GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = false;
            FGetSupportType();
        }
        catch (Exception)
        {
            return;
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
                Label Count = (Label)e.Row.FindControl("lblCountAsrah");//take lable id
                CoutAosar += int.Parse(Count.Text);
                lblCountAosar.Text = CoutAosar.ToString();

                Label CountCard = (Label)e.Row.FindControl("lblCountGet");//take lable id
                CoutCard += int.Parse(CountCard.Text);
                lblCountCard.Text = CoutCard.ToString();

                Label lbl_Sum = (Label)e.Row.FindControl("lblSumTotal");//take lable id
                Label lbl_SumOperating = (Label)e.Row.FindControl("lblSumOperating_Expenses");//take lable id

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                Sum.Text = Convert.ToString(decimal.Parse(lbl_Sum.Text) + decimal.Parse(lbl_SumOperating.Text));

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

    private void FGetSupportType()
    {
        try
        {
            string XYears = string.Empty, XYearsName = string.Empty, XCategory = string.Empty, XCategoryName = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategoryName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_();
            MSFP.IDCheck = "GetByPojectStaticGeneral";
            MSFP.IDUniq = Guid.Empty;
            MSFP.ID_FinancialYear = Guid.Empty;
            MSFP.ID_Donor = Guid.Empty;
            MSFP.NumberMostafeed = 1;
            MSFP.billNumber = 0;
            MSFP.ID_Project = 0;
            MSFP.Start_Date = txtDateFrom.Text.Trim();
            MSFP.End_Date = txtDateTo.Text.Trim();
            MSFP.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MSFP.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MSFP.DataCheck3 = string.Empty;
            MSFP.IsTarmem = false;
            MSFP.IsBena = false;
            MSFP.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
            dt = RSFP.BArn_SupportForPrisms_Manage(MSFP);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " الإحصاء المالي العام لمشروع ( " + XCategoryName + " ) من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " +
                       txtDateTo.Text.Trim();

                lbl_Years.Text = XYearsName;
                GVFinancialStatistics.DataSource = dt;
                GVFinancialStatistics.DataBind();
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

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
    }

}