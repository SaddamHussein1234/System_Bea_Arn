using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageExchangeOrders_PageManageFinancialStatisticsGeneral : System.Web.UI.Page
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
            txtType_Cart.Text = ClassSetting.FGetType_Cart();
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
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
        Response.Redirect("PageManageFinancialStatisticsGeneral.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            lbl_Title_Bottom.Text = txt_Title_Bottom.Text.Trim();

            txt_Title_Bottom.Visible = false;

            lbl_Title_Bottom.Visible = true;
            GVFinancialStatistics.UseAccessibleHeader = true;
            GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlDataPrint;
            if (GVFinancialStatistics.Rows.Count > 13)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVFinancialStatistics.Rows.Count <= 13)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    private void FGetData()
    {
        GVFinancialStatistics.UseAccessibleHeader = false;
        txt_Title_Bottom.Visible = true;

        lbl_Title_Bottom.Visible = false;

        txtTitle.Text = " الإحصاء المالي العام لسنة " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy") + "م - " + Convert.ToDateTime(ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(txtDateFrom.Text.Trim()))).ToString("yyyy") + "هـ";

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
                FGetSupportType();
            else if (IsBaheth)
                FGetAlQariahByBaheth();
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
                //Label Count = (Label)e.Row.FindControl("lblCountAsrah");//take lable id
                //CoutAosar += int.Parse(Count.Text);
                //lbl_Count.Text = CoutAosar.ToString();

                Label _Count_Cart = (Label)e.Row.FindControl("lbl_Count_Cart");//take lable id
                CoutCard += int.Parse(_Count_Cart.Text);
                txt_Title_Bottom.Text = ", إجمالي العدد الموزع : " + CoutCard.ToString() + " " + txtType_Cart.Text.Trim();

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
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_IDCategory] FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _billNumber <> @0 And _IDMosTafeed <> @1 And _IDType = @2 And (convert(date, [_DateAddProduct]) Between @3 And @4) And _IsRaeesMaglisAlEdarah = @5 And _IsAmmenAlSondoq = @5 And _IsModer = @5 And _IsStorekeeper = @5 And _IsDelete = @6",
           Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
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

    public int FGetAlQariah(int XID)
    {
        int XResult = 0;
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT DISTINCT [AlQaryah] As 'Count_Qariah' FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _billNumber <> @0 And _IDMosTafeed <> @1 And _IDType = @2 And _IDCategory = @3 And (convert(date, [_DateAddProduct]) Between @4 And @5) And _IsRaeesMaglisAlEdarah = @6 And _IsAmmenAlSondoq = @6 And _IsModer = @6 And _IsStorekeeper = @6 And _IsDelete = @7",
               Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, XID.ToString(), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                XResult = dt.Rows.Count;
        }
        catch (Exception)
        {

        }

        return XResult;
    }

    public Int64 FGetCountFamily(int XID)
    {
        Int64 Xresult = 0;
        try
        {
            Int64 XCountCart = 0, XCountCartOther = 0;
            DataTable dtCart = new DataTable();
            dtCart = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_IDMosTafeed] FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _billNumber <> @0 And (_IDMosTafeed <> @1) And (_IDMosTafeed <> 504) And _IDType = @2 And _IDCategory = @3 And (convert(date, [_DateAddProduct]) Between @4 And @5) And _IsRaeesMaglisAlEdarah = @6 And _IsAmmenAlSondoq = @6 And _IsModer = @6 And _IsStorekeeper = @6 And _IsDelete = @7",
               Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, XID.ToString(), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dtCart.Rows.Count > 0)
                XCountCart = dtCart.Rows.Count;

            dtCart = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000  Sum([_Count_Families_]) As '_IDMosTafeed' FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _billNumber <> @0 And (_IDMosTafeed <> @1) And (_IDMosTafeed = 504) And _IDType = @2 And _IDCategory = @3 And (convert(date, [_DateAddProduct]) Between @4 And @5) And _IsRaeesMaglisAlEdarah = @6 And _IsAmmenAlSondoq = @6 And _IsModer = @6 And _IsStorekeeper = @6 And _IsDelete = @7",
                        Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, XID.ToString(), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if ((dtCart.Rows.Count > 0) && (dtCart.Rows[0]["_IDMosTafeed"] != DBNull.Value))
                XCountCartOther = Convert.ToInt64(dtCart.Rows[0]["_IDMosTafeed"]);
            Xresult = XCountCart + XCountCartOther;
        }
        catch (Exception)
        {
            Xresult = 0;
        }
        return Xresult;
    }

    public Int64 FGetCard(int XID)
    {
        Int64 Xresult = 0;
        try
        {
            Int64 XCountCart = 0, XCountCartOther = 0;
            DataTable dtCart = new DataTable();
            dtCart = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_billNumber],[_IDMosTafeed] FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _billNumber <> @0 And _IDMosTafeed <> @1 And (_IDMosTafeed <> 504) And _IDType = @2 And _IDCategory = @3 And (convert(date, [_DateAddProduct]) Between @4 And @5) And _IsRaeesMaglisAlEdarah = @6 And _IsAmmenAlSondoq = @6 And _IsModer = @6 And _IsStorekeeper = @6 And _IsDelete = @7",
                Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, XID.ToString(), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dtCart.Rows.Count > 0)
                XCountCart = dtCart.Rows.Count;

            dtCart = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000  Sum([_Count_Cart_]) As '_IDMosTafeed' FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _billNumber <> @0 And (_IDMosTafeed <> @1) And (_IDMosTafeed = 504) And _IDType = @2 And _IDCategory = @3 And (convert(date, [_DateAddProduct]) Between @4 And @5) And _IsRaeesMaglisAlEdarah = @6 And _IsAmmenAlSondoq = @6 And _IsModer = @6 And _IsStorekeeper = @6 And _IsDelete = @7",
                            Convert.ToString(0), Convert.ToString(0), DLType.SelectedValue, XID.ToString(), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if ((dtCart.Rows.Count > 0) && (dtCart.Rows[0]["_IDMosTafeed"] != DBNull.Value))
                XCountCartOther = Convert.ToInt64(dtCart.Rows[0]["_IDMosTafeed"]);
            Xresult = XCountCart + XCountCartOther;
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
            dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([_TotalPrice]),0) As 'TotalPrice' FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed Where _IDCategory = @0 And (convert(date, [_DateAddProduct]) Between @1 And @2) And _IsRaeesMaglisAlEdarah = @3 And _IsAmmenAlSondoq = @3 And _IsModer = @3 And _IsStorekeeper = @3 And _IsDelete = @4",
                XID.ToString(), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                Result = Convert.ToDecimal(String.Format("{0:0.00}", dt.Rows[0]["TotalPrice"].ToString()));
        }
        catch (Exception)
        {
            Result = 0;
        }
        return Result;
    }

    protected void LBEditAge_Click(object sender, EventArgs e)
    {
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
            FGetData();
        }
        catch (Exception)
        {
            return;
        }
    }

}