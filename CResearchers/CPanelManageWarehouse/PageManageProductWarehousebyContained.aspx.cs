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

public partial class CResearchers_CPanelManageWarehouse_PageManageProductWarehousebyContained : System.Web.UI.Page
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
            bool A68, A120;
            A68 = Convert.ToBoolean(dtViewProfil.Rows[0]["A68"]);
            A120 = Convert.ToBoolean(dtViewProfil.Rows[0]["A120"]);
            if (A68 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A120 == false)
            {
                IDAdd.Visible = false;
                GVByContained.Columns[9].Visible = false;
            }
            else if (A120 == true)
            {
                GVByContained.Columns[9].Visible = true;
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

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IDStore = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberCategory", DLType.SelectedValue, Convert.ToString(true), Convert.ToString(false));
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

    private void FGetProductShopBtCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", DLCategory.SelectedValue, Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLProduct.Items.Clear();
            DLProduct.Items.Add("");
            DLProduct.AppendDataBoundItems = true;
            DLProduct.DataValueField = "ProductID";
            DLProduct.DataTextField = "ProductName";
            DLProduct.DataSource = dt;
            DLProduct.DataBind();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductWarehousebyContained.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        lblIDStorekeeper.Text = DLIDStorekeeper2.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah2.SelectedItem.ToString();
        lblAmeenAlSondoq.Text = DLAmeenAlSondoq2.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah3.SelectedItem.ToString();
        DLIDStorekeeper2.Visible = false;
        DLModerAlGmeiah2.Visible = false;
        DLAmeenAlSondoq2.Visible = false;
        DLRaeesMaglesAlEdarah3.Visible = false;
        lblIDStorekeeper.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblAmeenAlSondoq.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;

        GVByContained.Columns[9].Visible = false;

        GVByContained.UseAccessibleHeader = true;
        GVByContained.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintFootable_.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void DLType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLCategory.Items.Clear();
            DLCategory.Items.Add("");
            DLCategory.AppendDataBoundItems = true;
            FGetCategoryShop();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLProduct.Items.Clear();
            DLProduct.Items.Add("");
            DLProduct.AppendDataBoundItems = true;
            FGetProductShopBtCategoryShop();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetSum()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Sum([_CountProduct]) As 'Get' FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _IDProduct = @0 And _billNumber <> @1 And _IsDelete = @2", DLProduct.SelectedValue, Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                Getsum = Convert.ToInt64(dt.Rows[0]["Get"]);
            }
            catch (Exception)
            {
                Getsum = 0;
            }

        }
    }

    private void FSetSum()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Sum([_CountProduct]) As 'Set' FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _IDProduct = @0 And _billNumber = @1 And _IsDelete = @2", DLProduct.SelectedValue, Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                Setsum = Convert.ToInt64(dt.Rows[0]["Set"]);
            }
            catch (Exception)
            {
                Setsum = 0;
            }
        }
    }

    private void FGetSumation()
    {
        float XSumation = 0;
        FSetSum();
        FGetSum();
        XSumation = Setsum - Getsum;

        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[ProductShop] SET [CountProduct] = @CountProduct WHERE ProductID = @ProductID";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt64(DLProduct.SelectedValue));
        cmd.Parameters.AddWithValue("@CountProduct", Convert.ToInt64(XSumation));
        cmd.ExecuteScalar();
        conn.Close();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DLIDStorekeeper2.Visible = true;
        DLModerAlGmeiah2.Visible = true;
        DLAmeenAlSondoq2.Visible = true;
        DLRaeesMaglesAlEdarah3.Visible = true;
        lblIDStorekeeper.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblAmeenAlSondoq.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        if (DLType.Text != string.Empty)
        {
            lblType.Visible = false;
            if (DLCategory.Text != string.Empty)
            {
                lblCategory.Visible = false;
                if (DLProduct.Text != string.Empty)
                {
                    lblProduct.Visible = false;
                    if (txtDateFrom.Text != string.Empty)
                    {
                        lblDateFrom.Visible = false;
                        if (txtDateTo.Text != string.Empty)
                        {
                            lblDateTo.Visible = false;
                            // Write Code Hear
                            CheckAccountAdmin();
                            FGetSumation();
                            FArnProductShopWarehouseByProduct();
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
                else if (DLProduct.Text == string.Empty)
                {
                    lblProduct.Visible = true;
                }
            }
            else if (DLCategory.Text == string.Empty)
            {
                lblCategory.Visible = true;
            }
        }
        else if (DLType.Text == string.Empty)
        {
            lblType.Visible = true;
        }
    }

    private void FArnProductShopWarehouseByProduct()
    {
        try
        {
            GVByContained.UseAccessibleHeader = false;
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = 0;
            CPS.IDMosTafeed = 0;
            CPS.IDProduct = Convert.ToInt32(DLProduct.SelectedValue);
            CPS.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopWarehouseByProductAndDate();
            if (dt.Rows.Count > 0)
            {
                GVByContained.DataSource = dt;
                GVByContained.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Text = "قائمة جميع الوارد للمستودع لـ " + DLCategory.SelectedItem.ToString() + " - " + DLProduct.SelectedItem.ToString() + " - من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                FGetSumation();
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
    float Getsum, Setsum = 0;
    int tempcounter = 0;
    protected void GVByContained_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
            Cou += int.Parse(Count.Text);
            lblSum.Text = Cou.ToString();

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            lblTotalPrice.Text = sum.ToString();

            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
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