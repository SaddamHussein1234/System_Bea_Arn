using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PageSheard_WUCSortingOutFinancialSupport : System.Web.UI.UserControl
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            XMony = ClassSaddam.FGetMonySa();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            txtDateFromPrisms.Text = ClassSaddam.FGetDateFrom();
            txtDateToPrisms.Text = ClassSaddam.FGetDateTo();

            GVExchangeOrdersPrisms.Columns[0].Visible = false;
            //GVExchangeOrders.Columns[8].Visible = false;
            FGetAlQariahPrisms();
            ClassMosTafeed.FGetName(DLName);
        }
    }

    private void FGetAlQariahPrisms()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariahPrisms.DataValueField = "IDItem";
            CBAlQariahPrisms.DataTextField = "AlQriah";
            CBAlQariahPrisms.DataSource = dt;
            CBAlQariahPrisms.DataBind();
        }
        FGetCategoryShopPrisms();
    }

    private void FGetCategoryShopPrisms()
    {
        ClassQuaem.FGetSupportType(0, "'5'", CBCategoryPrisms);
        FSelectCheck();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBAlQariahPrisms.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategoryPrisms.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVExchangeOrdersPrisms.UseAccessibleHeader = true;
            GVExchangeOrdersPrisms.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVExchangeOrdersPrisms.Columns[14].Visible = false;
            Session["foot"] = pnlDataPrisms;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearchPrisms_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    private void FGetData()
    {
        try
        {
            if (CB2.Checked) { GVExchangeOrdersPrisms.Columns[2].Visible = true; } else { GVExchangeOrdersPrisms.Columns[2].Visible = false; }
            if (CB3.Checked) { GVExchangeOrdersPrisms.Columns[3].Visible = true; } else { GVExchangeOrdersPrisms.Columns[3].Visible = false; }
            if (CB4.Checked) { GVExchangeOrdersPrisms.Columns[4].Visible = true; } else { GVExchangeOrdersPrisms.Columns[4].Visible = false; }
            if (CB6.Checked) { GVExchangeOrdersPrisms.Columns[6].Visible = true; } else { GVExchangeOrdersPrisms.Columns[6].Visible = false; }
            if (CB7.Checked) { GVExchangeOrdersPrisms.Columns[7].Visible = true; } else { GVExchangeOrdersPrisms.Columns[7].Visible = false; }
            if (CB8.Checked) { GVExchangeOrdersPrisms.Columns[8].Visible = true; } else { GVExchangeOrdersPrisms.Columns[8].Visible = false; }
            if (CB9.Checked) { GVExchangeOrdersPrisms.Columns[9].Visible = true; } else { GVExchangeOrdersPrisms.Columns[9].Visible = false; }
            if (CB10.Checked) { GVExchangeOrdersPrisms.Columns[10].Visible = true; } else { GVExchangeOrdersPrisms.Columns[10].Visible = false; }
            if (CB11.Checked) { GVExchangeOrdersPrisms.Columns[11].Visible = true; } else { GVExchangeOrdersPrisms.Columns[11].Visible = false; }
            if (CB12.Checked) { GVExchangeOrdersPrisms.Columns[12].Visible = true; } else { GVExchangeOrdersPrisms.Columns[12].Visible = false; }
            if (CB13.Checked) { GVExchangeOrdersPrisms.Columns[13].Visible = true; } else { GVExchangeOrdersPrisms.Columns[13].Visible = false; }

            GVExchangeOrdersPrisms.UseAccessibleHeader = false;
            GVExchangeOrdersPrisms.Columns[14].Visible = true;

            string XYears = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            string XCategory = string.Empty;
            foreach (ListItem item in CBCategoryPrisms.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            string XAlQariah = string.Empty;
            foreach (ListItem item in CBAlQariahPrisms.Items)
                XAlQariah += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_();
            if (DLName.SelectedValue != string.Empty)
            {
                MSFP.IDCheck = "GetSortExchangeOrdersByID";
                MSFP.NumberMostafeed = Convert.ToInt32(DLName.SelectedValue);
            }
            else
            {
                MSFP.IDCheck = "GetSortExchangeOrders";
                MSFP.NumberMostafeed = 0;
            }
            MSFP.IDUniq = Guid.Empty;
            MSFP.ID_FinancialYear = Guid.Empty;
            MSFP.ID_Donor = Guid.Empty;
            MSFP.billNumber = 0;
            MSFP.ID_Project = 0;
            MSFP.Start_Date = txtDateFromPrisms.Text.Trim();
            MSFP.End_Date = txtDateToPrisms.Text.Trim();
            MSFP.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MSFP.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MSFP.DataCheck3 = XAlQariah.Substring(0, XAlQariah.Length - 1);
            MSFP.IsTarmem = false;
            DataTable dt = new DataTable();
            Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
            dt = RSFP.BArn_SupportForPrisms_Manage(MSFP);

            if (dt.Rows.Count > 0)
            {
                txtTitlePrisms.Text = "قائمة فرز أوامر الصرف من تاريخ " + Convert.ToDateTime(txtDateFromPrisms.Text.Trim()).ToString("MM-dd-yyyy") +
                        " إلى تاريخ " + Convert.ToDateTime(txtDateToPrisms.Text.Trim()).ToString("MM-dd-yyyy");

                GVExchangeOrdersPrisms.DataSource = dt;
                GVExchangeOrdersPrisms.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNullPrisms.Visible = false;
                pnlDataPrisms.Visible = true;
                pnlSelectPrisms.Visible = false;
                IDFilterPrisms.Visible = false;
            }
            else
            {
                pnlNullPrisms.Visible = true;
                pnlDataPrisms.Visible = false;
                pnlSelectPrisms.Visible = false;
                IDFilterPrisms.Visible = true;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل";
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVExchangeOrdersPrisms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCount = (Label)e.Row.FindControl("lblCountGet");//take lable id
                Cou += int.Parse(lblCount.Text);
                if (Cou != 0)
                    lbl_CountGet.Text = Cou.ToString();

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblSum.Text = sum.ToString();
                else
                    lblSum.Text = "0";

                lblMony.Text = XMony;
            }

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(lblSum.Text), currencies[Convert.ToInt32(0)]);
            lblSumWord.Text = toWord.ConvertToArabic();
        }
        catch (Exception)
        {

        }
    }

    public Int64 FGetCountCard(Guid XYear, int XIDMostafeed, int XIDProject)
    {
        return Repostry_SupportForPrisms_.FGetCountGetByProject(XYear, XIDMostafeed, XIDProject, txtDateFromPrisms.Text.Trim(), txtDateToPrisms.Text.Trim());
    }

    public decimal FGetSumCard(Guid XYear, int XIDMostafeed, int XIDProject)
    {
        return Repostry_SupportForPrisms_.FGetSumGetByProject(XYear, XIDMostafeed, XIDProject, txtDateFromPrisms.Text.Trim(), txtDateToPrisms.Text.Trim());
    }

    protected void LBGetFilterPrisms_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        pnlNullPrisms.Visible = true;
        pnlDataPrisms.Visible = false;
        pnlSelectPrisms.Visible = false;
        IDFilterPrisms.Visible = true;
    }

}