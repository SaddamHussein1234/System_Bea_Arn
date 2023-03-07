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

public partial class Shaerd_CPanelManageExchangeOrders_PageManageProductExchangeOrdersDetailsHouse : System.Web.UI.UserControl
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

    private void FGetAlQariahByBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
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
        ClassQuaem.FGetSupportType(0, "'4'", CBCategory);
        FSelectCheck();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVExchangeOrders.Columns[8].Visible = false;
            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    private void FGetData()
    {
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[8].Visible = true;

        string XQuery = "", XFrom = "", XTo = "", XDelete = "0";
        XFrom = "'" + txtDateFrom.Text.Trim() + "'";
        XTo = "'" + txtDateTo.Text.Trim() + "'";
        XQuery = "SELECT DISTINCT TOP 1000 BAT.NumberMostafeed As 'ID',[NameMostafeed],[AlQaryah],[ID_Type] ";
        XQuery += "FROM [dbo].[BenaaAndTarmim] BAT With(NoLock) ";
        XQuery += "Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = BAT.NumberMostafeed ";

        if (DLName.SelectedValue != string.Empty)
            XQuery += "Where BAT.NumberMostafeed = " + DLName.SelectedValue + " And ";
        else
            XQuery += "Where ";

        XQuery += "(";
        string XCategory = string.Empty;
        foreach (ListItem item in CBCategory.Items)
            XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
        XQuery += " ID_Type In (" + XCategory.Substring(0, XCategory.Length - 1) + ")";
        XQuery += ")";

        XQuery += " And (";
        string XAlQariah = string.Empty;
        foreach (ListItem item in CBAlQariah.Items)
            XAlQariah += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
        XQuery += " AlQaryah In (" + XAlQariah.Substring(0, XAlQariah.Length - 1) + ")";
        XQuery += ")";


        XQuery += " And (convert(date, Date_Add_Report) Between " + XFrom + " And " + XTo + ") And BAT.IsDelete = " + XDelete + " Order by AlQaryah, NameMostafeed";
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
    
    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDFilter.Visible = true;
    }

    public string FGetCountCard(int XIDMostafeed, float XIDCategory)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [billNumber_],[NumberMostafeed] FROM [dbo].[BenaaAndTarmim] With(NoLock) Where NumberMostafeed = @0 And ID_Type = @1 And (convert(date, Date_Add_Report) Between @2 And @3) And IsDelete = @4",
            Convert.ToString(XIDMostafeed), Convert.ToString(XIDCategory),
            Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            XResult = dt.Rows.Count.ToString();
        return XResult;
    }

    public static string FPrice(int XIDMostafeed, float XIDCategory)
    {
        string Result = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([The_Mony]),'0') As 'TotalPrice' FROM [dbo].[BenaaAndTarmim] With(NoLock) Where NumberMostafeed = @0 And ID_Type = @1 And IsDelete = @2", Convert.ToString(XIDMostafeed), Convert.ToString(XIDCategory), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            Result = dt.Rows[0]["TotalPrice"].ToString();
        return Result;
    }

}