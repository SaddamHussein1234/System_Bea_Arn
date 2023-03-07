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

public partial class Shaerd_CPanelManageExchangeOrders_PageManageFinancialStatisticsByDamaged : System.Web.UI.UserControl
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
        Response.Redirect("PageManageFinancialStatisticsByDamaged.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        txtTitle.Text = " الإحصاء المالي لمشروع ( " + DLType.SelectedItem.ToString() + " ) لسنة " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy") + "م - " + Convert.ToDateTime(ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(txtDateFrom.Text.Trim()))).ToString("yyyy") + "هـ";
        lblSumTalef.Text = FPrice(999999999);
        pnlData.Visible = true;
        pnlNull.Visible = false;
        pnlSelect.Visible = false;
        lblSum.Text = lblSumTalef.Text;

        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
        ToWord toWord = new ToWord(Convert.ToDecimal(lblSumTalef.Text), currencies[Convert.ToInt32(0)]);
        lblSumWord.Text = toWord.ConvertToArabic();
    }

    // عرض السعر
    public string FPrice(int Num)
    {
        string Result = "0";
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Sum([_TotalPrice]) As 'TotalPrice' FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _IDMosTafeed = @0 And (convert(date, [_DateAddProduct]) Between @1 And @2) And _IsRaeesMaglisAlEdarah = @3 And _IsStorekeeper = @3 And _IsNaebRaees = @3 And _IsDelete = @4",
                Convert.ToString(Num), Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["TotalPrice"].ToString() != string.Empty)
                    Result = dt.Rows[0]["TotalPrice"].ToString();
                else
                    Result = "0";
            }
        }
        catch (Exception)
        {
            Result = "0";
        }
        return Result;
    }

    protected void DLAlBaheth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}