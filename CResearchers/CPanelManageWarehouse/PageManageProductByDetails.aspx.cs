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

public partial class CResearchers_CPanelManageWarehouse_PageManageProductByDetails : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        HttpCookie Cooke;  // رقم المستخدم
        Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
        IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

        HttpCookie CookeUniq;  // رقم يونيك
        CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
        IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
    }

    private void CheckAccountAdmin()
    {
        try
        {
            DLIDStorekeeper2.Visible = true;
            DLModerAlGmeiah2.Visible = true;
            DLAmeenAlSondoq2.Visible = true;
            DLRaeesMaglesAlEdarah3.Visible = true;
            lblIDStorekeeper.Visible = false;
            lblModerAlGmeiah.Visible = false;
            lblAmeenAlSondoq.Visible = false;
            lblRaeesMaglesAlEdarah.Visible = false;
            GetCookie();
            ClassAdmin_Arn CAA = new ClassAdmin_Arn();
            CAA._IDUniq = IDUniq;
            CAA._IsDelete = false;
            DataTable dtViewProfil = new DataTable();
            dtViewProfil = CAA.BArnAdminGetByIDUniq();
            if (dtViewProfil.Rows.Count > 0)
            {
                bool A61, A70, A120;
                A61 = Convert.ToBoolean(dtViewProfil.Rows[0]["A61"]);
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
                if (A61 == false)
                {
                    GVProductSet.Columns[8].Visible = false;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("LogOut.aspx");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnProductShopWarehouseByProductGet();
            FGetAmeenAlMostwdaa();
        }
    }

    private void FArnProductShopWarehouseByProductGet()
    {
        ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
        CPS.billNumber = Convert.ToInt32(0);
        CPS.IDMosTafeed = Convert.ToInt32(0);
        CPS.IDProduct = Convert.ToInt64(Request.QueryString["XID"]);
        CPS.IDUniq = txtSearch.Text.Trim();
        CPS.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CPS.BArnProductShopWarehouseByProductGet();
        if (dt.Rows.Count > 0)
        {
            GVProductGet.DataSource = dt;
            GVProductGet.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            FArnProductShopWarehouseByProductSet();
            txtTitle.Text = " قائمة تفاصيل العمليات التي حصلت لـ " + dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["ProductName"].ToString() + "," + " الكمية المتبقية " + Convert.ToString(Convert.ToInt64(lblSum.Text) - Convert.ToInt64(lblSum2.Text));
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }

    }

    private void FArnProductShopWarehouseByProductSet()
    {
        ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
        CPS.billNumber = Convert.ToInt32(0);
        CPS.IDMosTafeed = Convert.ToInt32(0);
        CPS.IDProduct = Convert.ToInt64(Request.QueryString["XID"]);
        CPS.IDUniq = txtSearch.Text.Trim();
        CPS.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CPS.BArnProductShopWarehouseByProductSet();
        if (dt.Rows.Count > 0)
        {
            GVProductSet.DataSource = dt;
            GVProductSet.DataBind();
            lblCount2.Text = Convert.ToString(dt.Rows.Count);
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        DLIDStorekeeper2.Visible = true;
        DLModerAlGmeiah2.Visible = true;
        DLAmeenAlSondoq2.Visible = true;
        DLRaeesMaglesAlEdarah3.Visible = true;
        lblIDStorekeeper.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblAmeenAlSondoq.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        GVProductSet.Columns[8].Visible = true;
        CheckAccountAdmin();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DLIDStorekeeper2.Visible = false;
        DLModerAlGmeiah2.Visible = false;
        DLAmeenAlSondoq2.Visible = false;
        DLRaeesMaglesAlEdarah3.Visible = false;
        lblIDStorekeeper.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblAmeenAlSondoq.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;

        lblIDStorekeeper.Text = DLIDStorekeeper2.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah2.SelectedItem.ToString();
        lblAmeenAlSondoq.Text = DLAmeenAlSondoq2.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah3.SelectedItem.ToString();

        GVProductSet.Columns[8].Visible = false;
        Session["footable"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintFootable_.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVProductGet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
            Cou += int.Parse(Count.Text);
            lblSum.Text = Cou.ToString();

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
        }
    }

    int Cou2 = 0;
    decimal sum2 = 0;
    protected void GVProductSet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
            Cou2 += int.Parse(Count.Text);
            lblSum2.Text = Cou2.ToString();

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum2 += decimal.Parse(salary.Text);
            if (sum2 != 0)
            {
                lblTotalPrice2.Text = sum2.ToString();
            }
            else
            {
                lblTotalPrice2.Text = "بإنتظار التسعير";
            }
        }
    }

    private void FGetAmeenAlMostwdaa()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsStorekeeper = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLIDStorekeeper2.DataValueField = "ID_Item";
            DLIDStorekeeper2.DataTextField = "FirstName";
            DLIDStorekeeper2.DataSource = dt;
            DLIDStorekeeper2.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah3.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah3.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah3.DataSource = dt;
            DLRaeesMaglesAlEdarah3.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah2.DataValueField = "ID_Item";
            DLModerAlGmeiah2.DataTextField = "FirstName";
            DLModerAlGmeiah2.DataSource = dt;
            DLModerAlGmeiah2.DataBind();
        }
        FGetAmeenAlsondoq();
    }

    private void FGetAmeenAlsondoq()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsAmeenAlSondoq = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAmeenAlSondoq2.DataValueField = "ID_Item";
            DLAmeenAlSondoq2.DataTextField = "FirstName";
            DLAmeenAlSondoq2.DataSource = dt;
            DLAmeenAlSondoq2.DataBind();
        }
        ImgIDStorekeeper.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLIDStorekeeper2.SelectedValue));
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah2.SelectedValue));
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq2.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah3.SelectedValue));
    }

    protected void DLIDStorekeeper2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgIDStorekeeper.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLIDStorekeeper2.SelectedValue));
    }

    protected void DLModerAlGmeiah2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah2.SelectedValue));
    }

    protected void DLAmeenAlSondoq2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq2.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah3_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah3.SelectedValue));
    }
    
}