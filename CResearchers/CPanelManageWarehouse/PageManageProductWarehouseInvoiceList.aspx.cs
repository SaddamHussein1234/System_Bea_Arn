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

public partial class CResearchers_CPanelManageWarehouse_PageManageProductWarehouseInvoiceList : System.Web.UI.Page
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
            Response.Redirect("LogOut.aspx");
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
            bool A131;
            A131 = Convert.ToBoolean(dtViewProfil.Rows[0]["A131"]);
            if (A131 == false)
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
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            pnlSelect.Visible = true;
            FGetAmeenAlMostwdaa();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductWarehouseInvoiceList.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        lblIDStorekeeper.Text = DLIDStorekeeper.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblAmeenAlSondoq.Text = DLAmeenAlSondoq.SelectedItem.ToString();

        DLIDStorekeeper.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLAmeenAlSondoq.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        lblIDStorekeeper.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblAmeenAlSondoq.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;

        GVWarehouseInvoiceList.UseAccessibleHeader = true;
        GVWarehouseInvoiceList.HeaderRow.TableSection = TableRowSection.TableHeader;

        GVWarehouseInvoiceList.Columns[0].Visible = false;
        GVWarehouseInvoiceList.Columns[6].Visible = false;
        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtDateFrom.Text != string.Empty)
        {
            lblDateFrom.Visible = false;
            if (txtDateTo.Text != string.Empty)
            {
                GVWarehouseInvoiceList.Columns[6].Visible = true;
                GVWarehouseInvoiceList.UseAccessibleHeader = false;
                lblDateTo.Visible = false;
                // Write Code Hear
                CheckAccountAdmin();
                FArnProductShopWarehouseByInvoiceList();
                System.Threading.Thread.Sleep(500);
            }
            else if (txtDateTo.Text == string.Empty)
            {
                lblDateTo.Visible = true;
            }
        }
        else if (txtDateFrom.Text == string.Empty)
        {
            lblDateFrom.Visible = true;
        }
    }

    private void FArnProductShopWarehouseByInvoiceList()
    {
        try
        {
            DLIDStorekeeper.Visible = true;
            DLModerAlGmeiah.Visible = true;
            DLAmeenAlSondoq.Visible = true;
            DLRaeesMaglesAlEdarah.Visible = true;
            lblIDStorekeeper.Visible = false;
            lblModerAlGmeiah.Visible = false;
            lblAmeenAlSondoq.Visible = false;
            lblRaeesMaglesAlEdarah.Visible = false;
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopWarehouseByInvoiceList();
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة فواتير الشحن من تاريخ " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("dd/MM/yyyy") + " إلى تاريخ " + Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("dd/MM/yyyy");
                GVWarehouseInvoiceList.DataSource = dt;
                GVWarehouseInvoiceList.DataBind();
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
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    int tempcounter = 0;
    protected void GVWarehouseInvoiceList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
            {
                lblTotalPrice.Text = sum.ToString();
            }
            else
            {
                lblTotalPrice.Text = "بإنتظار التسعير";
            }

            tempcounter = tempcounter + 1;
            if (tempcounter == 5)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    private void FGetAmeenAlMostwdaa()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsStorekeeper = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLIDStorekeeper.DataValueField = "ID_Item";
            DLIDStorekeeper.DataTextField = "FirstName";
            DLIDStorekeeper.DataSource = dt;
            DLIDStorekeeper.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //DLModerAlGmeiah.Items.Clear();
            //DLModerAlGmeiah.Items.Add("");
            //DLModerAlGmeiah.AppendDataBoundItems = true;
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetAmeenAlsondoq();
    }

    private void FGetAmeenAlsondoq()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsAmeenAlSondoq = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAmeenAlSondoq.DataValueField = "ID_Item";
            DLAmeenAlSondoq.DataTextField = "FirstName";
            DLAmeenAlSondoq.DataSource = dt;
            DLAmeenAlSondoq.DataBind();

        }
        ImgIDStorekeeper.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLIDStorekeeper.SelectedValue));
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    protected void DLIDStorekeeper_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgIDStorekeeper.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLIDStorekeeper.SelectedValue));
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

}