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

public partial class CResearchers_CPanelManageWarehouse_PageManageProductWarehousebyContainedAndIssued : System.Web.UI.Page
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
            bool A70, A120;
            A70 = Convert.ToBoolean(dtViewProfil.Rows[0]["A70"]);
            A120 = Convert.ToBoolean(dtViewProfil.Rows[0]["A120"]);
            if (A70 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A120 == false)
            {
                IDAdd.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            FGetCategoryShop();
            FGetAmeenAlMostwdaa();
        }
    }

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberCategory", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLCategory.Items.Clear();
            DLCategory.Items.Add("");
            DLCategory.AppendDataBoundItems = true;
            DLCategory.DataValueField = "CategoryID";
            DLCategory.DataTextField = "CategoryName";
            DLCategory.DataSource = dt;
            DLCategory.DataBind();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductWarehousebyContainedAndIssued.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        lblIDStorekeeper.Text = DLIDStorekeeper.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblAmeenAlSondoq.Text = DLAmeenAlSondoq.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();

        DLIDStorekeeper.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLAmeenAlSondoq.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        lblIDStorekeeper.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblAmeenAlSondoq.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;

        GVByContainedAndIssued.UseAccessibleHeader = true;
        GVByContainedAndIssued.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DLIDStorekeeper.Visible = true;
        DLModerAlGmeiah.Visible = true;
        DLAmeenAlSondoq.Visible = true;
        DLRaeesMaglesAlEdarah.Visible = true;
        lblIDStorekeeper.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblAmeenAlSondoq.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        GVByContainedAndIssued.UseAccessibleHeader = false;
        if (DLCategory.Text != string.Empty)
        {
            lblCategory.Visible = false;
            FGetProductShopByCategory();
            System.Threading.Thread.Sleep(500);
        }
        else if (DLCategory.Text == string.Empty)
        {
            lblCategory.Visible = true;
        }

    }

    private void FGetProductShopByCategory()
    {
        try
        {
            ClassProduct CP = new ClassProduct();
            CP._IDCategoryShop = Convert.ToInt64(DLCategory.SelectedValue);
            CP._IDUniq = txtSearch.Text.Trim();
            CP._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CP.BArnProductShopByCategory();
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " إجمالي الوارد والصادر والموجود في المستودع لـ " + DLCategory.SelectedItem.ToString() + " - إلى حينة ";
                GVByContainedAndIssued.DataSource = dt;
                GVByContainedAndIssued.DataBind();
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

    public string FGetSum(Int64 XID)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([_CountProduct]), 0 ) As 'Get' FROM [dbo].[ProductShopWarehouse] Where _IDProduct = @0 And _billNumber <> @1 And _IsDelete = @2", Convert.ToString(XID), Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows[0]["Get"].ToString();
        }
        return XResult;
    }

    public string FSetSum(Int64 XID)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT ISNULL(Sum([_CountProduct]), 0 ) As 'Set' FROM [dbo].[ProductShopWarehouse] Where _IDProduct = @0 And _billNumber = @1 And _IsDelete = @2", Convert.ToString(XID), Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows[0]["Set"].ToString();
        }
        return XResult;
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVByContainedAndIssued_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblSumSet");//take lable id
            Cou += int.Parse(Count.Text);
            lblSumSet.Text = Cou.ToString();

            Label salary = (Label)e.Row.FindControl("lblSumGet");//take lable id
            sum += decimal.Parse(salary.Text);
            lblSumGet.Text = sum.ToString();
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