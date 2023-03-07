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

public partial class Shaerd_CPanelManageExchangeOrders_PageManageProductExchangeSortExchangeOrders : System.Web.UI.UserControl
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
                FGetAlQariahPrisms();
                ClassMosTafeed.FGetName(DLName);
            //}
            //else if (IsBaheth_)
            //    FGetAlQariahPrismsByBaheth();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            txtDateFromPrisms.Text = ClassSaddam.FGetDateFrom();
            txtDateToPrisms.Text = ClassSaddam.FGetDateTo();

            GVExchangeOrdersPrisms.Columns[0].Visible = false;
            //GVExchangeOrders.Columns[8].Visible = false;
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

    private void FGetAlQariahPrismsByBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariahPrisms.DataValueField = "IDQariah";
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
        foreach (ListItem lst in CBAlQariahPrisms.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategoryPrisms.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrdersPrisms.UseAccessibleHeader = true;
            GVExchangeOrdersPrisms.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVExchangeOrdersPrisms.Columns[9].Visible = false;
            Session["footable1"] = pnlDataPrisms;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearchPrisms_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    private void FGetData()
    {
        GVExchangeOrdersPrisms.UseAccessibleHeader = false;
        GVExchangeOrdersPrisms.Columns[9].Visible = true;
        string XQuery = "", XFrom = "", XTo = "", XDelete = "0";
        XFrom = "'" + txtDateFromPrisms.Text.Trim() + "'";
        XTo = "'" + txtDateToPrisms.Text.Trim() + "'";
        XQuery = "SELECT DISTINCT TOP 1000 TSP.NumberMostafeed As 'ID',[NameMostafeed],[AlQaryah],[ID_Type] ";
        XQuery += "FROM [dbo].[tbl_SupportForPrisms] TSP With(NoLock) ";
        XQuery += "Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TSP.NumberMostafeed ";
        if (DLName.SelectedValue != string.Empty)
            XQuery += "Where TSP.[NumberMostafeed] = " + DLName.SelectedValue + " And ";
        else
            XQuery += "Where ";

        XQuery += "(";
        string XCategory = string.Empty;
        foreach (ListItem item in CBCategoryPrisms.Items)
            XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
        XQuery += " ID_Type In (" + XCategory.Substring(0, XCategory.Length - 1) + ")";
        XQuery += ")";

        XQuery += " And (";
        string XAlQariah = string.Empty;
        foreach (ListItem item in CBAlQariahPrisms.Items)
            XAlQariah += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
        XQuery += " AlQaryah In (" + XAlQariah.Substring(0, XAlQariah.Length - 1) + ")";
        XQuery += ")";
        XQuery += " And (convert(date, Date_Add_Report) Between " + XFrom + " And " + XTo + ") And TSP.IsDelete = " + XDelete + " Order by AlQaryah";

        DataTable dt = new DataTable();
        try
        {
            dt = ClassDataAccess.GetData(XQuery);
            if (dt.Rows.Count > 0)
            {
                txtTitlePrisms.Text = "قائمة فرز أوامر الصرف من تاريخ " + txtDateFromPrisms.Text.Trim() + " إلى تاريخ " + txtDateToPrisms.Text.Trim();
                GVExchangeOrdersPrisms.DataSource = dt;
                GVExchangeOrdersPrisms.DataBind();
                lblCountPrisms.Text = Convert.ToString(dt.Rows.Count);
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
            lbmsgPrisms.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل";
            lbmsgPrisms.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }

    int CouPrisms = 0;
    decimal sumPrisms = 0;
    int tempcounterPrisms = 0;
    protected void GVExchangeOrdersPrisms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumPrisms += decimal.Parse(salary.Text);
                if (sumPrisms != 0)
                    lblTotalPricePrisms.Text = sumPrisms.ToString();
                else
                    lblTotalPricePrisms.Text = "بإنتظار التسعير";

                tempcounterPrisms = tempcounterPrisms + 1;
                if (tempcounterPrisms == 14)
                {
                    e.Row.Attributes.Add("style", "page-break-after: always;");
                    tempcounterPrisms = 0;
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public string FGetCountCardPrisms(int XIDMostafeed, float XIDCategory)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [billNumber_],[NumberMostafeed] FROM [dbo].[tbl_SupportForPrisms] With(NoLock) Where NumberMostafeed = @0 And ID_Type = @1 And (convert(date, Date_Add_Report) Between @2 And @3) And IsDelete = @4",
            Convert.ToString(XIDMostafeed), Convert.ToString(XIDCategory),
            Convert.ToDateTime(txtDateFromPrisms.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateToPrisms.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            XResult = dt.Rows.Count.ToString();
        return XResult;
    }

    public string FPricePrisms(int XIDMostafeed, float XIDCategory)
    {
        string Result = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([The_Mony]),'0') As 'TotalPrice' FROM [dbo].[tbl_SupportForPrisms] With(NoLock) Where NumberMostafeed = @0 And ID_Type = @1 And (convert(date, Date_Add_Report) Between @2 And @3) And IsDelete = @4 ", 
            Convert.ToString(XIDMostafeed), Convert.ToString(XIDCategory), Convert.ToDateTime(txtDateFromPrisms.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateToPrisms.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            Result = dt.Rows[0]["TotalPrice"].ToString();
        return Result;
    }
    
    protected void LBGetFilterPrisms_Click(object sender, EventArgs e)
    {
        IDFilterPrisms.Visible = true;
    }

}