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

public partial class Cpanel_ERP_EOS_PageSheard_WUCSortingSupportForHomes : System.Web.UI.UserControl
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FGetAlQariah();
            ClassMosTafeed.FGetName(DLName);
            //GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "IDItem";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetCategoryShop()
    {
        ClassQuaem.FGetSupportType(0, "'4'", CBCategory);
        FSelectCheck();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = true;
        FGetData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = true;
        try
        {
            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVExchangeOrders.Columns[14].Visible = false;
            Session["foot"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = true;
        FGetData();
    }

    private void FGetData()
    {
        try
        {
            if (CB2.Checked) { GVExchangeOrders.Columns[2].Visible = true; } else { GVExchangeOrders.Columns[2].Visible = false; }
            if (CB3.Checked) { GVExchangeOrders.Columns[3].Visible = true; } else { GVExchangeOrders.Columns[3].Visible = false; }
            if (CB4.Checked) { GVExchangeOrders.Columns[4].Visible = true; } else { GVExchangeOrders.Columns[4].Visible = false; }
            if (CB6.Checked) { GVExchangeOrders.Columns[6].Visible = true; } else { GVExchangeOrders.Columns[6].Visible = false; }
            if (CB7.Checked) { GVExchangeOrders.Columns[7].Visible = true; } else { GVExchangeOrders.Columns[7].Visible = false; }
            if (CB8.Checked) { GVExchangeOrders.Columns[8].Visible = true; } else { GVExchangeOrders.Columns[8].Visible = false; }
            if (CB9.Checked) { GVExchangeOrders.Columns[9].Visible = true; } else { GVExchangeOrders.Columns[9].Visible = false; }
            if (CB10.Checked) { GVExchangeOrders.Columns[10].Visible = true; } else { GVExchangeOrders.Columns[10].Visible = false; }
            if (CB11.Checked) { GVExchangeOrders.Columns[11].Visible = true; } else { GVExchangeOrders.Columns[11].Visible = false; }
            if (CB12.Checked) { GVExchangeOrders.Columns[12].Visible = true; } else { GVExchangeOrders.Columns[12].Visible = false; }
            if (CB13.Checked) { GVExchangeOrders.Columns[13].Visible = true; } else { GVExchangeOrders.Columns[13].Visible = false; }

            GVExchangeOrders.UseAccessibleHeader = false;
            GVExchangeOrders.Columns[14].Visible = true;

            string XYears = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            string XCategory = string.Empty;
            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            string XAlQariah = string.Empty;
            foreach (ListItem item in CBAlQariah.Items)
                XAlQariah += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_();
            if (DLName.SelectedValue != string.Empty)
            {
                MBAT.IDCheck = "GetSortExchangeOrdersByID";
                MBAT.NumberMostafeed = Convert.ToInt32(DLName.SelectedValue);
            }
            else
            {
                MBAT.IDCheck = "GetSortExchangeOrders";
                MBAT.NumberMostafeed = 0;
            }
            MBAT.IDUniq = Guid.Empty;
            MBAT.ID_FinancialYear = Guid.Empty;
            MBAT.ID_Donor = Guid.Empty;
            MBAT.billNumber = 0;
            MBAT.ID_Project = 0;
            MBAT.Start_Date = txtDateFrom.Text.Trim();
            MBAT.End_Date = txtDateTo.Text.Trim();
            MBAT.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MBAT.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MBAT.DataCheck3 = XAlQariah.Substring(0, XAlQariah.Length - 1);
            MBAT.IsTarmem = false;
            MBAT.IsBena = false;
            MBAT.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            dt = RBAT.BArn_BenaaAndTarmim_Manage(MBAT);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة فرز أوامر الصرف من تاريخ " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("MM-dd-yyyy") +
                        " إلى تاريخ " + Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("MM-dd-yyyy");

                GVExchangeOrders.DataSource = dt;
                GVExchangeOrders.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
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
                IDFilter.Visible = true;
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
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        pnlNull.Visible = true;
        pnlData.Visible = false;
        pnlSelect.Visible = false;
        IDFilter.Visible = true;
    }

    public Int64 FGetCountCard(Guid XYear, int XIDMostafeed, int XIDProject)
    {
        return Repostry_BenaaAndTarmim_.FGetCountGetByProject(XYear, XIDMostafeed, XIDProject, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
    }

    public decimal FGetSumCard(Guid XYear, int XIDMostafeed, int XIDProject)
    {
        return Repostry_BenaaAndTarmim_.FGetSumGetByProject(XYear, XIDMostafeed, XIDProject, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
    }

}