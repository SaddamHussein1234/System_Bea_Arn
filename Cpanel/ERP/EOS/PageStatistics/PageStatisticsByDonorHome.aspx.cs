using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PageStatistics_PageStatisticsByDonorHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A129");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            Repostry_Company_.FCRM_Company_ManageView(CBDonors);
            pnlSelect.Visible = true;
            ClassQuaem.FGetSupportType(0, "'4'", CBCategory);
            FSelectCheck(true);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
        }
    }

    private void FSelectCheck(bool XValue)
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBDonors.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsByDonorHome.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        GVFinancialStatistics.UseAccessibleHeader = false;
        FGetSupportType();
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

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumTotal += decimal.Parse(Sum.Text);
                lblSum.Text = sumTotal.ToString();

                lblMony.Text = ClassSaddam.FGetMonySa();
                lblMonyOperating_Expenses.Text = lblMony.Text;
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
            string XYears = string.Empty, XYearsName = string.Empty, XDonor = string.Empty, XCategory = string.Empty, XCategoryName = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBDonors.Items)
                XDonor += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategoryName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_();
            MBAT.IDCheck = "GetByPojectStaticByDonor";
            MBAT.IDUniq = Guid.Empty;
            MBAT.ID_FinancialYear = Guid.Empty;
            MBAT.ID_Donor = Guid.Empty;
            MBAT.NumberMostafeed = 1;
            MBAT.billNumber = 0;
            MBAT.ID_Project = 0;
            MBAT.Start_Date = txtDateFrom.Text.Trim();
            MBAT.End_Date = txtDateTo.Text.Trim();
            MBAT.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MBAT.DataCheck2 = XDonor.Substring(0, XDonor.Length - 1);
            MBAT.DataCheck3 = XCategory.Substring(0, XCategory.Length - 1);
            MBAT.IsTarmem = false;
            MBAT.IsBena = false;
            MBAT.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            dt = RBAT.BArn_BenaaAndTarmim_Manage(MBAT);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " الإحصاء المالي حسب الداعمين للمشروع ( " + XCategoryName + " ) من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " +
                       txtDateTo.Text.Trim();

                lbl_Years.Text = XYearsName;
                GVFinancialStatistics.DataSource = dt;
                GVFinancialStatistics.DataBind();

                lblSumOperating_Expenses.Text = WSM_Repostry_Operating_Expenses_.FGetBySumByStaticByProject("GetBySumByStaticByProjectMulti", Guid.Empty,
                    0, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), MBAT.DataCheck, MBAT.DataCheck3);

                List<CurrencyInfo> CurrencieOperating_Expenses = new List<CurrencyInfo>();
                CurrencieOperating_Expenses.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWordOperating_Expenses = new ToWord(Convert.ToDecimal(lblSumOperating_Expenses.Text), CurrencieOperating_Expenses[Convert.ToInt32(0)]);
                lblSumWordSumOperating_Expenses.Text = toWordOperating_Expenses.ConvertToArabic();

                lblSumAll.Text = Convert.ToString(decimal.Parse(lblSum.Text) + decimal.Parse(lblSumOperating_Expenses.Text));
                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblSumAll.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();

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