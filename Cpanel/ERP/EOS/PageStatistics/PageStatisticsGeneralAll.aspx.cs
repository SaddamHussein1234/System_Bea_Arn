using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Models;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PageStatistics_PageStatisticsGeneralAll : System.Web.UI.Page
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A129");
            ClassQuaem.FGetSupportType(0, CBCategory);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            pnlSelect.Visible = true;
            txtType_Cart.Text = ClassSetting.FGetType_Cart();
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = false; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsGeneralAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            
            if (GVFinancialStatistics.Rows.Count > 0)
            {
                GVFinancialStatistics.UseAccessibleHeader = true;
                GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (GVFinancialStatisticsHome.Rows.Count > 0)
            {
                GVFinancialStatisticsHome.UseAccessibleHeader = true;
                GVFinancialStatisticsHome.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (GVFinancialStatisticsMony.Rows.Count > 0)
            {
                GVFinancialStatisticsMony.UseAccessibleHeader = true;
                GVFinancialStatisticsMony.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Session["foot"] = pnlDataPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
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

        string XYears = string.Empty, XCategory = string.Empty, XYearsName = string.Empty;

        txtTitle.Text = " الإحصاء المالي العام , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
        //foreach (ListItem item in CBYears.Items)
        //    XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

        //foreach (ListItem item in CBYears.Items)
        //    XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

        foreach (ListItem item in CBCategory.Items)
            XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

        //lbl_Years.Text = XYearsName;
        FGetData(string.Empty, XCategory.Substring(0, XCategory.Length - 1));
        lblTotal.Text = SumTotal.ToString() + " " + XMony; lblTotalHome.Text = SumTotalHome.ToString() + " " + XMony;
        lblTotalMony.Text = SumTotalMony.ToString() + " " + XMony;
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
        ToWord toWord = new ToWord(SumTotal + SumTotalHome + SumTotalMony,
            currencies[Convert.ToInt32(0)]);
        lblSumWord.Text = toWord.ConvertToArabic();
        lblSum.Text = (SumTotal + SumTotalHome + SumTotalMony).ToString();
        lblMony.Text = XMony;
    }

    private void FGetData(string XYears,string XCategory)
    {
        GVFinancialStatistics.UseAccessibleHeader = false;
        GVFinancialStatisticsHome.UseAccessibleHeader = false;
        GVFinancialStatisticsMony.UseAccessibleHeader = false;
        FGetSupportType(XYears, XCategory); FGetSupportTypeHome(XYears, XCategory); FGetSupportTypeMony(XYears, XCategory);
        pnlNull.Visible = false;
        pnlData.Visible = true;
        pnlSelect.Visible = false;
        IDFilter.Visible = false;
    }

    decimal SumTotal = 0, SumTotalHome = 0, SumTotalMony = 0;
    protected void GVFinancialStatistics_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Sum = (Label)e.Row.FindControl("lblSumTotal");//take lable id
                Label lbl_SumOperating = (Label)e.Row.FindControl("lblSumOperating_Expenses");//take lable id

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                Sum.Text = Convert.ToString(decimal.Parse(lbl_Sum.Text) + decimal.Parse(lbl_SumOperating.Text));

                SumTotal += decimal.Parse(Sum.Text);

            }
        }
        catch (Exception)
        {

        }
    }

    private void FGetSupportType(string XYears, string XCategory)
    {
        try
        {
            

            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            MEOB.IDCheck = "GetByPojectStaticGeneralAll";
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = Guid.Empty;
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = 0;
            MEOB.ID_MosTafeed = 0;
            MEOB.Start_Date = txtDateFrom.Text.Trim();
            MEOB.End_Date = txtDateTo.Text.Trim();
            MEOB.DataCheck = DLType.SelectedValue;
            MEOB.DataCheck2 = XYears;
            MEOB.DataCheck3 = XCategory;
            MEOB.Is_Cart = false;
            MEOB.Is_Device = false;
            MEOB.Is_Tathith = false;
            MEOB.Is_Talef = false;
            MEOB.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
            dt = REOB.BWSM_Exchange_Order_Bill_Manage(MEOB);
            if (dt.Rows.Count > 0)
            {
                GVFinancialStatistics.DataSource = dt;
                GVFinancialStatistics.DataBind();
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

    private void FGetSupportTypeHome(string XYears, string XCategory)
    {
        try
        {
            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_();
            MBAT.IDCheck = "GetByPojectStaticGeneralAll";
            MBAT.IDUniq = Guid.Empty;
            MBAT.ID_FinancialYear = Guid.Empty;
            MBAT.ID_Donor = Guid.Empty;
            MBAT.NumberMostafeed = 1;
            MBAT.billNumber = 0;
            MBAT.ID_Project = 0;
            MBAT.Start_Date = txtDateFrom.Text.Trim();
            MBAT.End_Date = txtDateTo.Text.Trim();
            MBAT.DataCheck = XYears;
            MBAT.DataCheck2 = XCategory;
            MBAT.DataCheck3 = string.Empty;
            MBAT.IsTarmem = false;
            MBAT.IsBena = false;
            MBAT.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            dt = RBAT.BArn_BenaaAndTarmim_Manage(MBAT);

            if (dt.Rows.Count > 0)
            {
                GVFinancialStatisticsHome.DataSource = dt;
                GVFinancialStatisticsHome.DataBind();
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

    private void FGetSupportTypeMony(string XYears, string XCategory)
    {
        try
        {
            Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_();
            MSFP.IDCheck = "GetByPojectStaticGeneralAll";
            MSFP.IDUniq = Guid.Empty;
            MSFP.ID_FinancialYear = Guid.Empty;
            MSFP.ID_Donor = Guid.Empty;
            MSFP.NumberMostafeed = 1;
            MSFP.billNumber = 0;
            MSFP.ID_Project = 0;
            MSFP.Start_Date = txtDateFrom.Text.Trim();
            MSFP.End_Date = txtDateTo.Text.Trim();
            MSFP.DataCheck = XYears;
            MSFP.DataCheck2 = XCategory;
            MSFP.DataCheck3 = string.Empty;
            MSFP.IsTarmem = false;
            MSFP.IsBena = false;
            MSFP.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
            dt = RSFP.BArn_SupportForPrisms_Manage(MSFP);
            if (dt.Rows.Count > 0)
            {
                GVFinancialStatisticsMony.DataSource = dt;
                GVFinancialStatisticsMony.DataBind();
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

    protected void LBEditAge_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[SettingTable] SET [_Type_Cart] = @Type_Cart WHERE IDSetting = @IDSetting";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Type_Cart", txtType_Cart.Text.Trim());
            cmd.Parameters.AddWithValue("@IDSetting", 964654);
            cmd.ExecuteScalar();
            conn.Close();

            string XYears = string.Empty, XCategory = string.Empty, XYearsName = string.Empty;

            txtTitle.Text = " الإحصاء المالي العام , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            lbl_Years.Text = XYearsName;
            FGetData(XYears.Substring(0, XYears.Length - 1), XCategory.Substring(0, XCategory.Length - 1));
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
    }

    protected void GVFinancialStatisticsHome_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Sum = (Label)e.Row.FindControl("lblSumTotal");//take lable id
                Label lbl_SumOperating = (Label)e.Row.FindControl("lblSumOperating_Expenses");//take lable id

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                Sum.Text = Convert.ToString(decimal.Parse(lbl_Sum.Text) + decimal.Parse(lbl_SumOperating.Text));

                SumTotalHome += decimal.Parse(Sum.Text);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void GVFinancialStatisticsMony_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Sum = (Label)e.Row.FindControl("lblSumTotal");//take lable id
                Label lbl_SumOperating = (Label)e.Row.FindControl("lblSumOperating_Expenses");//take lable id

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                Sum.Text = Convert.ToString(decimal.Parse(lbl_Sum.Text) + decimal.Parse(lbl_SumOperating.Text));

                SumTotalMony += decimal.Parse(Sum.Text);

            }
        }
        catch (Exception)
        {

        }
    }

}