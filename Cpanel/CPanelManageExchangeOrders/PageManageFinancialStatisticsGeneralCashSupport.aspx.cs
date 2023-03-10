using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageExchangeOrders_PageManageFinancialStatisticsGeneralCashSupport : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A129;
            A129 = Convert.ToBoolean(dtViewProfil.Rows[0]["A129"]);
            if (A129 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageFinancialStatisticsGeneralCashSupport.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = true;
            GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = false;
            txtTitle.Text = " الإحصاء المالي العام للدعم النقدي لسنة " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy") + "م - " + Convert.ToDateTime(ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(txtDateFrom.Text.Trim()))).ToString("yyyy") + "هـ";
            FGetSupportType();
        }
        catch (Exception)
        {
            return;
        }
    }

    int CoutAosar = 0;
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

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumTotal += decimal.Parse(Sum.Text);
                lblSum.Text = sumTotal.ToString();

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblSum.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();

            }
        }
        catch (Exception)
        {

        }
    }

    private void FGetSupportType()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) [IDItem],[TypeAlDam] FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And IsDeleteTypeAlDam = @1 And ([ID_Affiliation_Progect] = @2) Order by IDItem",
           string.Empty, Convert.ToString(false), "5");
        if (dt.Rows.Count > 0)
        {
            GVFinancialStatistics.DataSource = dt;
            GVFinancialStatistics.DataBind();
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
    }

    public int FGetCountAlQariah(int XID)
    {
        int Xresult = 0;
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [AlQaryah] FROM [dbo].[tbl_SupportForPrisms] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = tbl_SupportForPrisms.NumberMostafeed Where ID_Type = @0 And (convert(date, [Date_Add_Report]) Between @1 And @2) And IsAllowModer = @3 And AllowState = @3 And IsAllowRaeesAlMagles = @3 And tbl_SupportForPrisms.IsDelete = @4",
            XID.ToString(), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                Xresult = dt.Rows.Count;
        }
        catch (Exception)
        {
            Xresult = 0;
        }
        return Xresult;
    }

    public int FGetCountFamily(int XID)
    {
        int Xresult = 0;
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 TSP.NumberMostafeed FROM [dbo].[tbl_SupportForPrisms] TSP With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TSP.NumberMostafeed Where ID_Type = @0 And (convert(date, [Date_Add_Report]) Between @1 And @2) And IsAllowModer = @3 And AllowState = @3 And IsAllowRaeesAlMagles = @3 And TSP.IsDelete = @4",
               Convert.ToString(XID), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                Xresult = dt.Rows.Count;
        }
        catch (Exception)
        {
            Xresult = 0;
        }
        return Xresult;
    }

    public int FGetCard(int XID)
    {
        int Xresult = 0;
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [billNumber_],tbl_SupportForPrisms.NumberMostafeed FROM [dbo].[tbl_SupportForPrisms] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = tbl_SupportForPrisms.NumberMostafeed Where ID_Type = @0 And (convert(date, [Date_Add_Report]) Between @1 And @2) And IsAllowModer = @3 And AllowState = @3 And IsAllowRaeesAlMagles = @3 And tbl_SupportForPrisms.IsDelete = @4",
               Convert.ToString(XID), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                Xresult = dt.Rows.Count;
        }
        catch (Exception)
        {
            Xresult = 0;
        }
        return Xresult;
    }

    public decimal FPrice(int XID)
    {
        decimal Result = 0;
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([The_Mony]),0) As 'TotalPrice' FROM [dbo].[tbl_SupportForPrisms] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = tbl_SupportForPrisms.NumberMostafeed Where ID_Type = @0 And (convert(date, [Date_Add_Report]) Between @1 And @2) And IsAllowModer = @3 And AllowState = @3 And IsAllowRaeesAlMagles = @3 And tbl_SupportForPrisms.IsDelete = @4",
                Convert.ToString(XID), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                Result = Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows[0]["TotalPrice"].ToString()));
        }
        catch (Exception)
        {
            Result = 0;
        }
        return Result;
    }

}