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

public partial class Shaerd_CPanelManageExchangeOrders_PageManageProductExchangeOrdersDetails : System.Web.UI.UserControl
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
            bool IsBaheth_, A59;
            IsBaheth_ = Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]);
            A59 = Convert.ToBoolean(dtViewProfil.Rows[0]["A59"]);
            if (A59 == false)
                Response.Redirect("PageNotAccess.aspx");
            //if (IsBaheth_ == false)
            //{
                FGetAlQariah();
                ClassMosTafeed.FGetName(DLName);
            //}
            //else if (IsBaheth_)
            //    FGetAlQariahByBaheth();
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

            GVExchangeOrders.Columns[0].Visible = false;
            //GVExchangeOrders.Columns[8].Visible = false;
        }
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "IDItem";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetAlQariahByBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "IDQariah";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetCategoryShop()
    {
        ClassQuaem.FGetSupportType(0, "'1','2','3','6'", CBCategory);
        FSelectCheck();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    private void FGetData()
    {
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[8].Visible = true;
        int XbillNumber = 0, XIDMosTafeed = 0;
        string XQuery = "", IDType = "", XFrom = "", XTo = "", XDelete = "0";
        IDType = "1";
        XFrom = "'" + txtDateFrom.Text.Trim() + "'";
        XTo = "'" + txtDateTo.Text.Trim() + "'";
        XQuery = "SELECT DISTINCT TOP 1000 [_IDMosTafeed],[NameMostafeed],[AlQaryah],[_IDType],[_IDCategory] ";
        XQuery += "FROM [dbo].[ProductShopWarehouse] With(NoLock) ";
        XQuery += "Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed ";
        if (DLName.SelectedValue != string.Empty)
            XQuery += "Where _billNumber <> " + XbillNumber + " And _IDMosTafeed <> " + XIDMosTafeed + " And _IDMosTafeed = " + DLName.SelectedValue + " And _IDType = " + IDType + " And ";
        else
            XQuery += "Where _billNumber <> " + XbillNumber + " And _IDMosTafeed <> " + XIDMosTafeed + " And _IDType = " + IDType + " And ";

        XQuery += "(";
        string XCategory = string.Empty;
        foreach (ListItem item in CBCategory.Items)
            XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
        XQuery += " _IDCategory In (" + XCategory.Substring(0, XCategory.Length - 1) + ")";
        XQuery += ")";

        XQuery += " And (";
        string XAlQariah = string.Empty;
        foreach (ListItem item in CBAlQariah.Items)
            XAlQariah += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
        XQuery += " AlQaryah In (" + XAlQariah.Substring(0, XAlQariah.Length - 1) + ")";
        XQuery += ")";

        XQuery += " And (convert(date, _DateAddProduct) Between " + XFrom + " And " + XTo + ") And _IsDelete = " + XDelete + " Order by AlQaryah, NameMostafeed";
        DataTable dt = new DataTable();
        try
        {
            dt = ClassDataAccess.GetData(XQuery);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة فرز أوامر الصرف من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
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
            lbmsg.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل";
            lbmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrders.Columns[8].Visible = false;

            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDFilter.Visible = true;
    }

    int Cou = 0;
    decimal sum = 0;
    int tempcounter = 0;
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPrice.Text = sum.ToString();
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";

                tempcounter = tempcounter + 1;
                if (tempcounter == 14)
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

    public string FGetCountCard(string XName, int XIDMostafeed, int XIDCategory)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        if (XName == "مشاريع لغير المستفيدين")
        {
            dt = ClassDataAccess.GetData("SELECT TOP 1 ISNULL(Sum([_Count_Cart_]),'0') As 'Count_Cart' FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join ProductShop on ProductShop.ProductID = ProductShopWarehouse._IDProduct Where _billNumber <> @0 And _IDMosTafeed <> @1 And _IDMosTafeed = @2 And _IDType = @3 And _IDCategory = @4 And (convert(date, _DateAddProduct) Between @5 And @6) And _IsDelete = @7",
            "0", "0", Convert.ToString(XIDMostafeed), "1", Convert.ToString(XIDCategory),
            Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                XResult = dt.Rows[0]["Count_Cart"].ToString();
        }
        else
        {
            dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_billNumber],[_IDMosTafeed],[_ProductionDate],[_DateAddProduct],[_IDType],[_IsActive],[_IDAdmin],[_IDRaeesMaglisAlEdarah],[_IsRaeesMaglisAlEdarah],[_IDAmmenAlSondoq],[_IsAmmenAlSondoq],[_IDModer],[_IsModer],[_IDStorekeeper],[_IsStorekeeper],[_A1],[_IsDelete],[_IDCategory],[_IsReceived],[_IsNotReceived],[_IDDelivery] FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join ProductShop on ProductShop.ProductID = ProductShopWarehouse._IDProduct Where _billNumber <> @0 And _IDMosTafeed <> @1 And _IDMosTafeed = @2 And _IDType = @3 And _IDCategory = @4 And (convert(date, _DateAddProduct) Between @5 And @6) And _IsDelete = @7",
                "0", "0", Convert.ToString(XIDMostafeed), "1", Convert.ToString(XIDCategory),
                Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(false));
            if (dt.Rows.Count > 0)
                XResult = dt.Rows.Count.ToString();
        }
        
        return XResult;
    }

    public string FPrice(int XIDMostafeed, int XIDCategory)
    {
        string Result = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([_TotalPrice]),'0') As 'TotalPrice' FROM [dbo].[ProductShopWarehouse] Where _IDMosTafeed = @0 And _IDCategory = @1 And (convert(date, _DateAddProduct) Between @2 And @3) And _IsDelete = @4", 
            Convert.ToString(XIDMostafeed), Convert.ToString(XIDCategory), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            Result = dt.Rows[0]["TotalPrice"].ToString();
        return Result;
    }

}