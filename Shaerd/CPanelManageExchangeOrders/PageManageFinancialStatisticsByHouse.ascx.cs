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

public partial class Shaerd_CPanelManageExchangeOrders_PageManageFinancialStatisticsByHouse : System.Web.UI.UserControl
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
            FGetCategoryShop();
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
        }
    }

    private void FGetCategoryShop()
    {
        ClassQuaem.FGetSupportType(1, "'3'", DLCategory);
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [AlQaryah] FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _billNumber <> @0 And _IDMosTafeed <> @1 And _IDType = @2 And _IDCategory = @3 And (convert(date, [_DateAddProduct]) Between @4 And @5) And _IsRaeesMaglisAlEdarah = @6 And _IsAmmenAlSondoq = @6 And _IsModer = @6 And _IsStorekeeper = @6 And _IsDelete = @7",
           Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, DLCategory.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
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

    private void FGetAlQariahByBaheth()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) [IDItem],[IDAdminJoin],[IDQariah] As 'AlQaryah',[IsDelete] FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IsDelete = @1",
           IDUser, Convert.ToString(false));
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageFinancialStatisticsByHouse.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = true;
            GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlData;
            if (GVFinancialStatistics.Rows.Count > 14)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVFinancialStatistics.Rows.Count <= 14)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        GVFinancialStatistics.UseAccessibleHeader = false;

        txtTitle.Text = " الإحصاء المالي لمشروع ( " + DLCategory.SelectedItem.ToString() + " ) لسنة " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy") + "م - " + Convert.ToDateTime(ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(txtDateFrom.Text.Trim()))).ToString("yyyy") + "هـ";

        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool IsBaheth;
            IsBaheth = Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]);
            if (IsBaheth == false)
                FGetAlQariah();
            else if (IsBaheth)
                FGetAlQariahByBaheth();
        }
    }

    public string FGetCard(int XID,string XProgect,string XQariah)
    {
        string Xresult = "0";
        try
        {
            if (XProgect == "تاثيث منازل" && XQariah == "(مناطق_أخرى)")
                Xresult = "10";
            else
            {
                ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
                CPS.billNumber = Convert.ToInt32(0);
                CPS.IDMosTafeed = Convert.ToInt32(0);
                CPS.IDType = DLType.SelectedValue;
                CPS.IDCategory = Convert.ToInt32(DLCategory.SelectedValue);
                CPS.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
                CPS.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
                CPS.IsDelete = false;
                DataTable dt = new DataTable();
                dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_billNumber],[_IDMosTafeed] FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where RasAlEstemarah.AlQaryah = @0 And _billNumber <> @1 And _IDMosTafeed <> @2 And _IDType = @3 And _IDCategory = @4 And (convert(date, [_DateAddProduct]) Between @5 And @6) And _IsRaeesMaglisAlEdarah = @7 And _IsAmmenAlSondoq = @7 And _IsModer = @7 And _IsStorekeeper = @7 And _IsDelete = @8",
                   Convert.ToString(XID), Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, DLCategory.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
                if (dt.Rows.Count > 0)
                    Xresult = dt.Rows.Count.ToString();
            }
        }
        catch (Exception)
        {
            Xresult = "0";
        }
        return Xresult;
    }

    // عرض السعر
    public string FPrice(int Num)
    {
        string Result = "0";
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Sum([_TotalPrice]) As 'TotalPrice' FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where RasAlEstemarah.AlQaryah = @0 And _IDCategory = @1 And (convert(date, [_DateAddProduct]) Between @2 And @3) And _IsRaeesMaglisAlEdarah = @4 And _IsAmmenAlSondoq = @4 And _IsModer = @4 And _IsStorekeeper = @4 And _IsDelete = @5",
                Convert.ToString(Num), DLCategory.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["TotalPrice"].ToString() != string.Empty)
                {
                    Result = dt.Rows[0]["TotalPrice"].ToString();
                }
                else
                {
                    Result = "0";
                }
            }
        }
        catch (Exception)
        {
            Result = "0";
        }
        return Result;
    }

    int CoutAosar = 0;
    decimal sumTotal = 0;
    int tempcounter = 0;
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

                tempcounter = tempcounter + 1;
                if (tempcounter == 5)
                {
                    e.Row.Attributes.Add("style", "page-break-after: always;");
                    tempcounter = 0;
                }
            }
        }
        catch (Exception)
        {

        }
    }

}