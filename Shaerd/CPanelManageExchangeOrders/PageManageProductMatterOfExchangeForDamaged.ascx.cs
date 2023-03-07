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

public partial class Shaerd_CPanelManageExchangeOrders_PageManageProductMatterOfExchangeForDamaged : System.Web.UI.UserControl
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
            bool A106;
            A106 = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            if (A106 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetLastRecordProssess();
            txtProductionDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            txt_Add.Text = txtProductionDate.Text;
            FGetCategoryShopTallef();
            FGetLastRecord();

            pnlTTaleef.Visible = true;
            pnlAlDaam.Visible = true;
            IDNaeeb.Visible = true;
            IDAmeenSondoq.Visible = false;
            IDModer.Visible = false;
            IDAlBaheth.Visible = false;
            FGetCategoryShopTallef();
            FArnProductShopMatterOfExchangeByTaleef();
            Label2.Text = "يرجى تحديد المنتجات التالفة";
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) [_billNumber] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 And _IsCart = @2 And _IsDevice = @3 And _IsTathith = @4 And _IsTalef = @5 Order by _billNumber Desc ",
            Convert.ToString(0), Convert.ToString(false), Convert.ToString(false), Convert.ToString(false), Convert.ToString(false), Convert.ToString(true));
        if (dt.Rows.Count > 0)
            txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_billNumber"]) + 1);
        else
            txtNumberOrder.Text = ClassSaddam.FGetNumberBillStart().ToString();
    }

    private void FGetLastRecordProssess()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 Order by _IDNumberProduct Desc", Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNumberProduct"]) + 1);
        else
            txtIDNumberProduct.Text = ClassSaddam.FGetNumberBillStart().ToString();
        FGetAmeenAlmostodaa();
    }

    private void FGetAmeenAlmostodaa()
    {
        ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
        ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
        ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);

        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsNaebMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLNaeebRaeesMagles.DataValueField = "ID_Item";
            DLNaeebRaeesMagles.DataTextField = "FirstName";
            DLNaeebRaeesMagles.DataSource = dt;
            DLNaeebRaeesMagles.DataBind();
        }
    }

    protected void LBGetBill_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FArnProductShopMatterOfExchangeByTaleef();
    }

    protected void DLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            DLProduct.Items.Clear();
            DLProduct.Items.Add("");
            DLProduct.AppendDataBoundItems = true;
            FGetProductShopBtCategoryShop();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
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

    protected void DLProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Sum([CountProduct]) As 'Set' FROM [dbo].[ProductShop] Where ProductID = @0 And IsDelete = @1", DLProduct.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = " - الكمية المتبقية لمنتج " + DLProduct.SelectedItem.ToString() + " ( " + dt.Rows[0]["Set"].ToString() + " منتج  ), ";
                DataTable dtPrice = new DataTable();
                dtPrice = ClassDataAccess.GetData("SELECT TOP 1 [_IDItem],[_IDNumberProduct],[_CountProduct],[_PriceOfTheGrain] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber = @0 And _IDMosTafeed = @0 And _IDProduct = @1 And _IsDelete = @2 Order by _IDItem Desc ", Convert.ToString(0), DLProduct.SelectedValue, Convert.ToString(false));
                lblMessageWarning.Text += "السعر ( " + Convert.ToString(Convert.ToInt32((dtPrice.Rows[0]["_PriceOfTheGrain"]))) + " ريال )";
                FGetSumation();
            }
            catch (Exception)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text  = "الكمية المتبقية لمنتج " + DLProduct.SelectedItem.ToString() + " " + "0";
                return;
            }
        }
    }

    private void FGetCategoryShopTallef()
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
        FGetSupportType();
    }

    private void FGetSupportType()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And TypeAlDam <> @1 And TypeAlDam = @2 And IsDeleteTypeAlDam = @3 Order by IDItem", string.Empty, "الاجهزة الكهربائية", "التالف", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLSupportType.DataValueField = "IDItem";
            DLSupportType.DataTextField = "TypeAlDam";
            DLSupportType.DataSource = dt;
            DLSupportType.DataBind();
        }
    }

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (btnAdd.Text == "إضافة للفاتورة")
            {
                System.Threading.Thread.Sleep(100);
                FCheckNumber();
            }
            else if (btnAdd.Text == "تعديل البيانات")
            {
                System.Threading.Thread.Sleep(100);
                FCheckNumberEdit();
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FCheckNumberEdit()
    {
        if (txtIDNumberProduct.Text != Session["OldIDNumber"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select * from ProductShopWarehouse Where _IDNumberProduct = @0 And _IsDelete = @1", txtIDNumberProduct.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "لا يمكن تكرار رقم الشحنة قم بتغييره";
                return;
            }
            else
            {
                Session["OldIDNumber"] = txtIDNumberProduct.Text.Trim();
                FArnProductShopWarehouseAddTaleef();
            }
        }
        else
            FArnProductShopWarehouseAddTaleef();
    }

    private void FCheckNumber()
    {
        //DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("Select * from ProductShopWarehouse Where _billNumber <> @0 And _billNumber = @1 And _IsDelete = @2", Convert.ToString(0), txtNumberOrder.Text.Trim(), Convert.ToString(false));
        //if (dt.Rows.Count > 0)
        //{
        //    lbmsg.Text = "لا يمكن تكرار رقم الامر قم بتغييره";
        //    lbmsg.ForeColor = System.Drawing.Color.Red;
        //}
        //else
        //{
        FCheckNumberOrder();
        //}
    }

    private void FCheckNumberOrder()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from ProductShopWarehouse Where _billNumber <> @0 And _IDNumberProduct = @1 And _IsDelete = @2", Convert.ToString(0), txtIDNumberProduct.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "لا يمكن تكرار رقم الطلب قم بتغييره";
            return;
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from ProductShopWarehouse Where _billNumber <> @0 And _billNumber = @1 And _IDProduct = @2 And _IsDelete = @3", Convert.ToString(0), txtNumberOrder.Text.Trim(), DLProduct.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "لا يمكن تكرار الدعم بنفس الفاتورة";
            return;
        }
        else
            FArnProductShopWarehouseAddTaleef();
    }

    private void FArnProductShopWarehouseAddTaleef()
    {
        if (btnAdd.Text == "إضافة للفاتورة")
        {
            DataTable dtProduct = new DataTable();
            dtProduct = ClassDataAccess.GetData("Select ProductID,CountProduct from ProductShop Where ProductID = @0 ", DLProduct.SelectedValue);
            if (dtProduct.Rows.Count > 0)
            {
                float XSumation = 0;
                XSumation = Convert.ToInt64(dtProduct.Rows[0]["CountProduct"]);
                if (Convert.ToInt64(txtCountProduct.Text.Trim()) <= Convert.ToInt64(XSumation))
                {
                    // جلب الوارد إلى المستودع
                    DataTable dtPrice = new DataTable();
                    dtPrice = ClassDataAccess.GetData("SELECT TOP 1 [_IDItem],[_IDNumberProduct],[_CountProduct],[_PriceOfTheGrain] FROM [dbo].[ProductShopWarehouse] Where _billNumber = @0 And _IDMosTafeed = @0 And _IDProduct = @1 And _IsDelete = @2 Order by _IDItem Desc ", Convert.ToString(0), DLProduct.SelectedValue, Convert.ToString(false));
                    if (dtPrice.Rows.Count > 0)
                    {
                        decimal IDPriceOfTheGrain = 0;
                        IDPriceOfTheGrain = Convert.ToDecimal(dtPrice.Rows[0]["_PriceOfTheGrain"]);
                        GetCookie();
                        ClassProductShopWarehouse CPSW = new ClassProductShopWarehouse()
                        {
                            IDUniq = Convert.ToString(Guid.NewGuid()),
                            billNumber = Convert.ToInt32(txtNumberOrder.Text.Trim()),
                            IDMosTafeed = 999999999,
                            IDProduct = Convert.ToInt64(DLProduct.SelectedValue),
                            IDNumberProduct = Convert.ToInt64(txtIDNumberProduct.Text.Trim()),
                            CountProduct = Convert.ToInt32(txtCountProduct.Text.Trim()),
                            PriceOfTheGrain = IDPriceOfTheGrain,
                            TotalPrice = IDPriceOfTheGrain * Convert.ToDecimal(txtCountProduct.Text.Trim()),
                            ProductionDate = Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy/MM/dd"),
                            ExpiryDate = Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy/MM/dd"),
                            DateCaming = Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy/MM/dd"),
                            IDType = "3",
                            ImgProduct = "0",
                            IDProductStorage = 0,
                            IsActive = true,
                            DateAddProduct = txt_Add.Text.Trim(),
                            IDAdmin = Convert.ToInt32(IDUser),
                            IDUpdate = 0,
                            DateUpDate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
                            IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                            IsRaeesMaglisAlEdarah = false,
                            IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                            IsAmmenAlSondoq = false,
                            IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                            IsModer = false,
                            IDStorekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
                            IsStorekeeper = false,
                            A1 = txtdescription.Text.Trim(),
                            A2_ = 1,
                            A3 = "0",
                            A4 = "0",
                            A5 = "0",
                            IsDelete = false,
                            IDCategory = Convert.ToInt32(DLSupportType.SelectedValue),
                            IsDone = false,
                            IsNotDone = false,
                            IsReceived = false,
                            IsNotReceived = false,
                            IDDelivery = 0,
                            IDNaebRaees = Convert.ToInt32(DLNaeebRaeesMagles.SelectedValue),
                            IsNaebRaees = false,
                            FromDonor = "0",
                            The_Purpose = "0",
                            IsCart = false,
                            IsDevice = false,
                            IsTathith = false,
                            IsTalef = true,
                            Count_Cart = 0,
                            Count_Families = 0
                        };
                        CPSW.BArnProductShopWarehouseAdd();
                        DataTable dt = new DataTable();
                        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 Order by _IDNumberProduct Desc", Convert.ToString(0), Convert.ToString(false));
                        if (dt.Rows.Count > 0)
                            txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNumberProduct"]) + 1);
                        else
                            txtIDNumberProduct.Text = ClassSaddam.FGetNumberBillStart().ToString();
                        FArnProductShopMatterOfExchangeByTaleef();
                        FGetSumation();
                        IDMessageSuccess.Visible = true;
                        IDMessageWarning.Visible = false;
                        lblMessage.Text = "تم إضافة البيانات بنجاح ... ";
                    }
                }
                else if (Convert.ToInt64(txtCountProduct.Text.Trim()) > Convert.ToInt64(XSumation))
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    lblMessageWarning.Text = "الكمية المتبقية " + XSumation.ToString() + " لقد طلبت كمية أكثر من التي بالمستودع";
                    return;
                }
            }
        }
        else if (btnAdd.Text == "تعديل البيانات")
        {

        }
    }

    protected void LBRefresh_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        GVMatterOfExchangeByIDTaleef.Columns[0].Visible = true;
        FArnProductShopMatterOfExchangeByTaleef();
        lblDateHideTaleef.Visible = false;
    }

    protected void LbPrintTaleef_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            GVMatterOfExchangeByIDTaleef.Columns[0].Visible = false;
            lblDateHideTaleef.Visible = true;
            Session["footable1"] = pnlDataTalef;
            if (GVMatterOfExchangeByIDTaleef.Rows.Count > 15)
                Page.ClientScript.RegisterStartupScript(GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVMatterOfExchangeByIDTaleef.Rows.Count <= 15)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDeleteTaleef_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVMatterOfExchangeByIDTaleef.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMatterOfExchangeByIDTaleef.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsDelete] = @_IsDelete WHERE _IDItem = @_IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@_IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@_IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FArnProductShopMatterOfExchangeByTaleef();
            FGetSumation();
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            lblMessage.Text = "تم حذف البيانات بنجاح ... ";
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    int CouTaleef = 0;
    decimal sumTaleef = 0;
    float GetsumTaleef, SetsumTaleef = 0;
    protected void GVMatterOfExchangeByIDTaleef_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Count = (Label)e.Row.FindControl("lblCountTaleef");//take lable id
                CouTaleef += int.Parse(Count.Text);
                lblSumTaleef.Text = CouTaleef.ToString();

                Label salary = (Label)e.Row.FindControl("lblCountTotalPriceTaleef");//take lable id
                sumTaleef += decimal.Parse(salary.Text);
                if (sumTaleef != 0)
                    lblTotalPriceTaleef.Text = sumTaleef.ToString();
                else
                    lblTotalPriceTaleef.Text = "بإنتظار التسعير";
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
        }
    }

    private void FArnProductShopMatterOfExchangeByTaleef()
    {
        try
        {
            IDBarcodeTalef.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                    "/Cpanel/PScanF.aspx?ID=" + txtNumberOrder.Text.Trim() + "&XID=" + 999999999 + "&chs=75";
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(txtNumberOrder.Text.Trim());
            CPS.IsDelete = false;
            CPS.IsCart = false;
            CPS.IsDevice = false;
            CPS.IsTathith = false;
            CPS.IsTalef = true;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopMatterOfExchangeByTaleef();
            if (dt.Rows.Count > 0)
            {
                lblToday.Text = ClassDataAccess.GetCurrentTime().ToString("ddd");
                lblDateToDay.Text = ClassDataAccess.GetCurrentTime().ToString("dd/MM/yyyy");
                GVMatterOfExchangeByIDTaleef.DataSource = dt;
                GVMatterOfExchangeByIDTaleef.DataBind();
                lblCountTaleef.Text = Convert.ToString(dt.Rows.Count);
                pnlNullTalef.Visible = false;
                pnlDataTalef.Visible = true;
                ProductByTalef.Visible = true;
                lblNumberTaleef.Text = " " + dt.Rows[0]["_billNumber"].ToString();

                lblDataEntryTaleef.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDAdmin"]));
                lblDateEntryTaleef.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateAddProduct"]));
                if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) != 0)
                {
                    IDUpdateTaleef.Visible = true;
                    lblDataEntryEditTaleef.Text = ClassQuaem.FAlBahethByEdit(Convert.ToInt32(dt.Rows[0]["_IDUpdate"]));
                    lblDateEntryEditTaleef.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateUpDate"]));
                }
                else if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) == 0)
                    IDUpdateTaleef.Visible = false;
                lblDateHideTaleef.Text = Convert.ToDateTime(dt.Rows[0]["_ProductionDate"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHideTaleef.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_ProductionDate"])) + "هـ";
                //txtTitleTalef.Text = ClassSaddam.FAlTypeEvint(Convert.ToInt32(dt.Rows[0]["_IDType"])) + "(المنتج " + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["_IDCategory"])) + ")";
                txtTitleTalef.Text = "عقد حصر وإتلاف";
                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]) && Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]) && Convert.ToBoolean(dt.Rows[0]["_IsModer"]) && Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]) && Convert.ToBoolean(dt.Rows[0]["_IsReceived"]))
                    IDKhatmTaleef.Visible = true;

                lblRaees.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah"]));
                lblNaeeb.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDNaebRaees"]));
                lblAmeen.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]))
                {
                    IDRaees.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah"]), Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]));
                    IDRaees.Visible = true;
                }
                else
                    IDRaees.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]))
                    IDKhatmTaleef.Visible = true;

                if (Convert.ToBoolean(dt.Rows[0]["_IsNaebRaees"]))
                {
                    IDNeeb.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDNaebRaees"]), Convert.ToBoolean(dt.Rows[0]["_IsNaebRaees"]));
                    IDNeeb.Visible = true;
                }
                else
                    IDNeeb.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]))
                {
                    IDAmeen.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]), Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]));
                    IDAmeen.Visible = true;
                }
                else
                    IDAmeen.Visible = false;

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPriceTaleef.Text), currencies[Convert.ToInt32(0)]);
                lblSumTalef.Text = toWord.ConvertToArabic();

            }
            else
            {
                pnlNullTalef.Visible = true;
                pnlDataTalef.Visible = false;
                ProductByTalef.Visible = true;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }
    
    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductMatterOfExchangeForDamaged.aspx");
    }

}