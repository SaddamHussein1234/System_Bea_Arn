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

public partial class Cpanel_CPanelManageExchangeOrders_PageManageProductExchangeOrdersDetails : System.Web.UI.Page
{
    string XID;
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
            bool A59;
            A59 = Convert.ToBoolean(dtViewProfil.Rows[0]["A59"]);
            if (A59 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAlQariah();
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FGetAlBaheth();
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
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And IsDeleteTypeAlDam = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBCategory.DataValueField = "IDItem";
            CBCategory.DataTextField = "TypeAlDam";
            CBCategory.DataSource = dt;
            CBCategory.DataBind();
        }
        FSelectCheck();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductExchangeOrdersDetails.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GVExchangeOrders.Columns[0].Visible = false;
        GVExchangeOrders.Columns[8].Visible = false;
        lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblAmeenAlSondoq.Text = DLAmeenAlSondoq.SelectedItem.ToString();
        DLAlBaheth.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        DLAmeenAlSondoq.Visible = false;
        lblAlBaheth.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;
        lblAmeenAlSondoq.Visible = true;

        GVExchangeOrders.UseAccessibleHeader = true;
        GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
                lblTotalPrice.Text = sum.ToString();
            else
                lblTotalPrice.Text = "بإنتظار التسعير";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[8].Visible = true;
        DLAlBaheth.Visible = true;
        DLModerAlGmeiah.Visible = true;
        DLRaeesMaglesAlEdarah.Visible = true;
        DLAmeenAlSondoq.Visible = true;
        lblAlBaheth.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        lblAmeenAlSondoq.Visible = false;

        int XbillNumber = 0, XIDMosTafeed = 0;
        string XQuery = "", IDType = "", XFrom = "", XTo = "", XDelete = "0";
        IDType = DLType.SelectedValue;
        XFrom = "'" + txtDateFrom.Text.Trim() + "'";
        XTo = "'" + txtDateTo.Text.Trim() + "'";
        XQuery = "SELECT DISTINCT TOP 1000 [_IDMosTafeed],[NameMostafeed],[AlQaryah],[_IDType],[_IDCategory] ";
        XQuery += "FROM [dbo].[ProductShopWarehouse] With(NoLock) ";
        XQuery += "Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ProductShopWarehouse._IDMosTafeed ";
        XQuery += "Where _billNumber <> " + XbillNumber + " And _IDMosTafeed <> " + XIDMosTafeed + " And _IDType = " + IDType + " And ";

        XQuery += "(";
        foreach (ListItem lst in CBCategory.Items)
        {
            if (lst.Selected == true)
            {

                XQuery += " _IDCategory = " + lst.Value;
                XQuery += " Or ";
            }

        }
        XQuery = XQuery.Remove(XQuery.Length - 3, 3);
        XQuery += ")";

        XQuery += " And (";
        foreach (ListItem lstQ in CBAlQariah.Items)
        {
            if (lstQ.Selected == true)
            {

                XQuery += " AlQaryah = " + lstQ.Value;
                XQuery += " Or ";
            }

        }
        XQuery = XQuery.Remove(XQuery.Length - 3, 3);
        XQuery += ")";

        XQuery += " And (convert(date, _DateCaming) Between " + XFrom + " And " + XTo + ") And _IsDelete = " + XDelete + " Order by AlQaryah, NameMostafeed";
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
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
        }
        catch (Exception)
        {
            lbmsg.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل";
            lbmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }
    
    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLAmeenAlSondoq_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    public string FGetCountCard(int XIDMostafeed, int XIDCategory)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_billNumber],[_IDMosTafeed],AffiliationShop.AffiliationName,[_ProductionDate],[_DateCaming],[_IDType],[_IsActive],[_IDAdmin],[_IDRaeesMaglisAlEdarah],[_IsRaeesMaglisAlEdarah],[_IDAmmenAlSondoq],[_IsAmmenAlSondoq],[_IDModer],[_IsModer],[_IDStorekeeper],[_IsStorekeeper],[_A1],[_IsDelete],[_IDCategory],[_IsReceived],[_IsNotReceived],[_IDDelivery] FROM [dbo].[ProductShopWarehouse] With(NoLock) Inner join ProductShop on ProductShop.ProductID = ProductShopWarehouse._IDProduct Inner join AffiliationShop on AffiliationShop.AffiliationID = ProductShop.IDAffiliationShop Where _billNumber <> @0 And _IDMosTafeed <> @1 And _IDMosTafeed = @2 And _IDType = @3 And _IDCategory = @4 And (convert(date, _DateCaming) Between @5 And @6) And _IsDelete = @7",
            "0", "0", Convert.ToString(XIDMostafeed), DLType.SelectedValue, Convert.ToString(XIDCategory),
            Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows.Count.ToString();
        }
        return XResult;
    }

    private void FGetAlBaheth()
    {
        //DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        //if (dt.Rows.Count > 0)
        //{
        //    //DLAlBaheth.Items.Clear();
        //    //DLAlBaheth.Items.Add("");
        //    //DLAlBaheth.AppendDataBoundItems = true;
        //    DLAlBaheth.DataValueField = "ID_Item";
        //    DLAlBaheth.DataTextField = "FirstName";
        //    DLAlBaheth.DataSource = dt;
        //    DLAlBaheth.DataBind();
        //}
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsAmeenAlSondoq = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAmeenAlSondoq.DataValueField = "ID_Item";
            DLAmeenAlSondoq.DataTextField = "FirstName";
            DLAmeenAlSondoq.DataSource = dt;
            DLAmeenAlSondoq.DataBind();
        }
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    public static string FPrice(int XIDMostafeed, int XIDCategory)
    {
        string Result = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([_TotalPrice]),'0') As 'TotalPrice' FROM [dbo].[ProductShopWarehouse] Where _IDMosTafeed = @0 And _IDCategory = @1 And _IsDelete = @2", Convert.ToString(XIDMostafeed), Convert.ToString(XIDCategory), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Result = dt.Rows[0]["TotalPrice"].ToString();
        }
        return Result;
    }

}