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

public partial class CResearchers_CPanelManageExchangeOrders_PageManageProductMatterOfExchange : System.Web.UI.Page
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
            pnlStar.Visible = true;
            FGetName();
            txtNumberMostafeed.Focus();
            FGetLastRecordProssess();
            txtProductionDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");

            pnlDataMosTafeed.Visible = false;
            ProductByUser.Visible = false;
            pnlMostafeed.Visible = true;
            pnlStar.Visible = false;
            pnlAlDaam.Visible = false;
            txtNumberMostafeed.Focus();
            FGetCategoryShop();
            IDNaeeb.Visible = false;
            IDAmeenSondoq.Visible = true;
            IDModer.Visible = true;
            IDAlBaheth.Visible = true;
            FArnProductShopMatterOfExchangeByUser();
            pnlStarView.Visible = true;
        }
    }

    private void FGetName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And [TypeMostafeed] = @1 And RasAlEstemarah.IsDelete = @2 And tbl_MultiQariah.IsDelete = @2 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.Items.Clear();
            DLName.Items.Add("");
            DLName.AppendDataBoundItems = true;
            DLName.DataValueField = "NumberMostafeed";
            DLName.DataTextField = "NameMostafeed";
            DLName.DataSource = dt;
            DLName.DataBind();
        }
    }

    private void FGetLastRecordProssess()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 Order by _IDNumberProduct Desc", Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNumberProduct"]) + 1);
        }
        else
        {
            txtIDNumberProduct.Text = "1001";
        }
        FGetAmeenAlmostodaa();
    }

    private void FGetAmeenAlmostodaa()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsStorekeeper = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLIDStorekeeper.DataValueField = "ID_Item";
            DLIDStorekeeper.DataTextField = "FirstName";
            DLIDStorekeeper.DataSource = dt;
            DLIDStorekeeper.DataBind();

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
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();

            DLRaeesMaglesAlEdarah2.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah2.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah2.DataSource = dt;
            DLRaeesMaglesAlEdarah2.DataBind();
        }
        FGetNaeebRaeesMagles();
    }

    private void FGetNaeebRaeesMagles()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsNaebMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLNaeebRaeesMagles.DataValueField = "ID_Item";
            DLNaeebRaeesMagles.DataTextField = "FirstName";
            DLNaeebRaeesMagles.DataSource = dt;
            DLNaeebRaeesMagles.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
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
        FGetAlBaheth();
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlBaheth.Items.Clear();
            DLAlBaheth.Items.Add("");
            DLAlBaheth.AppendDataBoundItems = true;
            DLAlBaheth.DataValueField = "ID_Item";
            DLAlBaheth.DataTextField = "FirstName";
            DLAlBaheth.DataSource = dt;
            DLAlBaheth.DataBind();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductMatterOfExchange.aspx");
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        FGetDataMostafed();
    }

    private void FGetDataMostafed()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And [TypeMostafeed] = @1 And NumberMostafeed = @2 And RasAlEstemarah.IsDelete = @3 And tbl_MultiQariah.IsDelete = @3 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, "دائم", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
            {
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            }
            else
            {
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            }
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            pnlDataMosTafeed.Visible = true;
            pnlAlDaam.Visible = true;
            Label1.Text = "بيانات المستفيد";
            pnlStarView.Visible = false;
        }
        else
        {
            Label1.Text = "يبدو ان هذا المستفيد ليس موجود , أو خارج نطاق صلاحيتك";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
            pnlStarView.Visible = true;
        }
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetDataMostafedByName();
    }

    private void FGetDataMostafedByName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
            {
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            }
            else
            {
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            }
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            pnlDataMosTafeed.Visible = true;
            pnlAlDaam.Visible = true;
            pnlStarView.Visible = false;
        }
        else
        {
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
            pnlStarView.Visible = true;
        }
    }

    protected void DLSupportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberOrder.Enabled = true;
            txtNumberMostafeed.Focus();
            lblStar.Text = "يرجى تحديد بيانات المستفيد";
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) [_billNumber] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IDCategory = @1 And _IsDelete = @2 And _IsCart = @3 And _IsDevice = @4 And _IsTathith = @5 And _IsTalef = @6 Order by _billNumber Desc ",
                Convert.ToString(0), DLSupportType.SelectedValue, Convert.ToString(false), Convert.ToString(true), Convert.ToString(false), Convert.ToString(false), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_billNumber"]) + 1);
            }
            else
            {
                txtNumberOrder.Text = "1001";
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBGetBill_Click(object sender, EventArgs e)
    {
        FArnProductShopMatterOfExchangeByUser();
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
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Sum([CountProduct]) As 'Set' FROM [dbo].[ProductShop] Where ProductID = @0 And IsDelete = @1", DLProduct.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                lbmsg.Text = " إنشاء أمر صرف ";
                lbmsg.ForeColor = System.Drawing.Color.Black;
                lblCountProduct.Text = " - الكمية المتبقية لمنتج " + DLProduct.SelectedItem.ToString() + " ( " + dt.Rows[0]["Set"].ToString() + " منتج  ), ";
                DataTable dtPrice = new DataTable();
                dtPrice = ClassDataAccess.GetData("SELECT TOP 1 [_IDItem],[_IDNumberProduct],[_CountProduct],[_PriceOfTheGrain] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber = @0 And _IDMosTafeed = @0 And _IDProduct = @1 And _IsDelete = @2 Order by _IDItem Desc ", Convert.ToString(0), DLProduct.SelectedValue, Convert.ToString(false));
                lblCountProduct.Text += "السعر ( " + Convert.ToString(Convert.ToInt32((dtPrice.Rows[0]["_PriceOfTheGrain"]))) + " ريال )";
                lblCountProduct.ForeColor = System.Drawing.Color.Red;
                FGetSumation();
            }
            catch (Exception)
            {
                lblCountProduct.Text = "الكمية المتبقية لمنتج " + DLProduct.SelectedItem.ToString() + " " + "0";
                lblCountProduct.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void txtPriceOfTheGrain_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtCountProduct.Text != string.Empty)
            {
                txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim()));
                txtdescription.Focus();
            }
            else
            {
                lblCheckCountProduct.Text = "الكمية *";
            }
        }
        catch (Exception)
        {
            txtTotalPrice.Text = "0";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
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
                lbmsg.Text = "لا يمكن تكرار رقم الشحنة قم بتغييره";
                lbmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Session["OldIDNumber"] = txtIDNumberProduct.Text.Trim();
                FArnProductShopWarehouseAddForMostafeed();
            }
        }
        else
        {
            FArnProductShopWarehouseAddForMostafeed();
        }
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
            lbmsg.Text = "لا يمكن تكرار رقم الطلب قم بتغييره";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FCheckName();
        }
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from ProductShopWarehouse Where _billNumber <> @0 And _billNumber = @1 And _IDProduct = @2 And _IsDelete = @3", Convert.ToString(0), txtNumberOrder.Text.Trim(), DLProduct.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "لا يمكن تكرار الدعم بنفس الفاتورة";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {

            FArnProductShopWarehouseAddForMostafeed();
        }
    }

    private void FArnProductShopWarehouseAddForMostafeed()
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

                    // Write Code Hear
                    if (dtPrice.Rows.Count > 0)
                    {
                        decimal IDPriceOfTheGrain = 0;
                        IDPriceOfTheGrain = Convert.ToDecimal(dtPrice.Rows[0]["_PriceOfTheGrain"]);
                        GetCookie();
                        ClassProductShopWarehouse CPSW = new ClassProductShopWarehouse()
                        {
                            IDUniq = Convert.ToString(Guid.NewGuid()),
                            billNumber = Convert.ToInt32(txtNumberOrder.Text.Trim()),
                            IDMosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
                            IDProduct = Convert.ToInt64(DLProduct.SelectedValue),
                            IDNumberProduct = Convert.ToInt64(txtIDNumberProduct.Text.Trim()),
                            CountProduct = Convert.ToInt32(txtCountProduct.Text.Trim()),
                            PriceOfTheGrain = IDPriceOfTheGrain,
                            TotalPrice = IDPriceOfTheGrain * Convert.ToDecimal(txtCountProduct.Text.Trim()),
                            ProductionDate = Convert.ToDateTime(txtProductionDate.Text.Trim()).ToString("yyyy/MM/dd"),
                            ExpiryDate = Convert.ToDateTime(txtProductionDate.Text.Trim()).ToString("yyyy/MM/dd"),
                            DateCaming = Convert.ToDateTime(txtProductionDate.Text.Trim()).ToString("yyyy/MM/dd"),
                            IDType = "1",
                            ImgProduct = "0",
                            IDProductStorage = 0,
                            IsActive = true,
                            DateAddProduct = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
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
                            IDDelivery = Convert.ToInt32(DLAlBaheth.SelectedValue),
                            IDNaebRaees = Convert.ToInt32(DLNaeebRaeesMagles.SelectedValue),
                            IsNaebRaees = false,
                            FromDonor = "0",
                            The_Purpose = "0",
                            IsCart = true,
                            IsDevice = false,
                            IsTathith = false,
                            IsTalef = false,
                            Count_Cart = 0,
                            Count_Families = 0
                        };
                        CPSW.BArnProductShopWarehouseAdd();
                        DataTable dt = new DataTable();
                        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 Order by _IDNumberProduct Desc", Convert.ToString(0), Convert.ToString(false));
                        if (dt.Rows.Count > 0)
                            txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNumberProduct"]) + 1);
                        else
                            txtIDNumberProduct.Text = "1001";
                        FArnProductShopMatterOfExchangeByUser();
                        FGetSumation();
                        lbmsg.Text = "تم الإضافة للفاتورة بنجاح";
                        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
                        txtCountProduct.Text = string.Empty;
                    }

                }
                else if (Convert.ToInt64(txtCountProduct.Text.Trim()) > Convert.ToInt64(XSumation))
                {
                    lbmsg.Text = "الكمية المتبقية " + XSumation.ToString() + " لقد طلبت كمية أكثر من التي بالمستودع";
                    lbmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        else if (btnAdd.Text == "تعديل البيانات")
        {

        }
    }

    protected void LBR_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByID.Columns[0].Visible = true;
        FArnProductShopMatterOfExchangeByUser();
        pnllblPrint.Visible = false;
        pnlDlPrint.Visible = true;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByID.Columns[0].Visible = false;
        lblDateHide.Visible = true;
        pnllblPrint.Visible = true;
        pnlDlPrint.Visible = false;
        lblIDStorekeeper2.Text = DLIDStorekeeper2.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        Session["footable1"] = pnlDataSarf;
        if (GVMatterOfExchangeByID.Rows.Count > 15)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        else if (GVMatterOfExchangeByID.Rows.Count <= 15)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMatterOfExchangeByID.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMatterOfExchangeByID.DataKeys[row.RowIndex].Value);
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
            FArnProductShopMatterOfExchangeByUser();
            FGetSumation();
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;
    protected void GVMatterOfExchangeByID_RowDataBound(object sender, GridViewRowEventArgs e)
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

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IDStore = @0 And (CategoryName <> @1) And IsActive = @2 And IsDelete = @3 Order by IDNumberCategory", "1", "الأجهزة الكهربائية", Convert.ToString(true), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And TypeAlDam <> @1 And TypeAlDam <> @2 And TypeAlDam <> @3 And TypeAlDam <> @4 And TypeAlDam <> @5 And IsDeleteTypeAlDam = @6 Order by IDItem", string.Empty, "بناء منازل", "ترميم منازل", "تاثيث منازل", "الاجهزة الكهربائية", "التالف", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLSupportType.Items.Clear();
            DLSupportType.Items.Add("");
            DLSupportType.AppendDataBoundItems = true;
            DLSupportType.DataValueField = "IDItem";
            DLSupportType.DataTextField = "TypeAlDam";
            DLSupportType.DataSource = dt;
            DLSupportType.DataBind();
        }
    }

    private void FArnProductShopMatterOfExchangeByUser()
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(txtNumberOrder.Text.Trim());
            CPS.IDMosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
            CPS.IDCategory = Convert.ToInt64(DLSupportType.SelectedValue);
            CPS.IsDelete = false;
            CPS.IsCart = true;
            CPS.IsDevice = false;
            CPS.IsTathith = false;
            CPS.IsTalef = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopMatterOfExchangeByUserNew();
            if (dt.Rows.Count > 0)
            {
                IDBarcode.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                    "/Cpanel/PScanF.aspx?ID=" + txtNumberOrder.Text.Trim() + "&XID=" + txtNumberMostafeed.Text.Trim() + "&chs=75";

                GVMatterOfExchangeByID.DataSource = dt;
                GVMatterOfExchangeByID.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNullSarf.Visible = false;
                pnlDataSarf.Visible = true;
                //ProductByTalef.Visible = false;
                ProductByUser.Visible = true;
                txtNumberMostafeed2.Text = dt.Rows[0]["_IDMosTafeed"].ToString();
                lblAlQariah2.Text = ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]));
                lblPhone2.Text = ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]));
                lblAmeenAlmosTodaa.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]));
                lblNumber.Text = dt.Rows[0]["_billNumber"].ToString();
                lblNameEvint.Text = ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]), dt.Rows[0]["_IDType"].ToString());

                lblDateHide.Text = Convert.ToDateTime(dt.Rows[0]["_ProductionDate"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHide.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_ProductionDate"])) + "هـ";
                //DLIDStorekeeper2.SelectedValue = dt.Rows[0]["_IDStorekeeper"].ToString();

                lblDataEntry.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDAdmin"]));
                lblDateEntry.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateAddProduct"]));
                if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) != 0)
                {
                    IDUpdate.Visible = true;
                    lblDataEntryEdit.Text = ClassQuaem.FAlBahethByEdit(Convert.ToInt32(dt.Rows[0]["_IDUpdate"]));
                    lblDateEntryEdit.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateUpDate"]));
                }
                else if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) == 0)
                {
                    IDUpdate.Visible = false;
                }

                txtTitle.Text = ClassSaddam.FAlTypeEvint(Convert.ToInt32(dt.Rows[0]["_IDType"])) + " لمشروع (" + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["_IDCategory"])) + ")";
                //DLRaeesMaglesAlEdarah2.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah"].ToString();

                CBDone.Checked = Convert.ToBoolean(dt.Rows[0]["_IsDone"]);
                CBNotDone.Checked = Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]);
                CBReceived.Checked = Convert.ToBoolean(dt.Rows[0]["_IsReceived"]);
                CBNotReceived.Checked = Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]);
                if (CBNotReceived.Checked)
                {
                    IDNotReceived.Visible = true;
                    lblA2.Text = dt.Rows[0]["_A2"].ToString();
                }
                else
                {
                    IDNotReceived.Visible = false;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsDone"]) == false && Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]) == false)
                {
                    lblDateGo.Text = "بإنتظار الملاحظة";
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_IsDone"]) == true || Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]) == true)
                {
                    lblDateGo.Text = Convert.ToDateTime(dt.Rows[0]["_ExpiryDate"]).ToString("yyyy/MM/dd");
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]) == false && Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]) == false)
                {
                    lblDateRecived.Text = "بإنتظار التسليم";
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]) == true || Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]) == true)
                {
                    lblDateRecived.Text = Convert.ToDateTime(dt.Rows[0]["_DateCaming"]).ToString("yyyy/MM/dd");
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]))
                {
                    ImgAmeenAlsondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDAmmenAlSondoq"]), Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]));
                    ImgAmeenAlsondoq.Width = 100;
                    ImgAmeenAlsondoq.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoq.ImageUrl = "../loaderMin.gif";
                    ImgAmeenAlsondoq.Width = 30;
                    ImgAmeenAlsondoq.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]))
                {
                    ImgRaees.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah"]), Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]));
                    ImgRaees.Width = 100;
                    ImgRaees.Visible = true;
                }
                else
                {
                    ImgRaees.ImageUrl = "../loaderMin.gif";
                    ImgRaees.Width = 30;
                    ImgRaees.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsModer"]))
                {
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDModer"]), Convert.ToBoolean(dt.Rows[0]["_IsModer"]));
                    ImgModer.Width = 100;
                    ImgModer.Visible = true;
                }
                else
                {
                    ImgModer.ImageUrl = "../loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]))
                {
                    ImgAmeenAlmosTodaa.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]), Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]));
                    ImgAmeenAlmosTodaa.Width = 100;
                    ImgAmeenAlmosTodaa.Visible = true;
                }
                else
                {
                    ImgAmeenAlmosTodaa.ImageUrl = "../loaderMin.gif";
                    ImgAmeenAlmosTodaa.Width = 30;
                    ImgAmeenAlmosTodaa.Visible = true;
                }

                lblNameEvint2.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDDelivery"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]))
                {
                    ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDDelivery"]), Convert.ToBoolean(dt.Rows[0]["_IsReceived"]));
                    ImgAlBaheth.Width = 100;
                    ImgAlBaheth.Visible = true;
                }
                else
                {
                    ImgAlBaheth.ImageUrl = "../loaderMin.gif";
                    ImgAlBaheth.Width = 30;
                    ImgAlBaheth.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]) && Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]) && Convert.ToBoolean(dt.Rows[0]["_IsModer"]) && Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]) && Convert.ToBoolean(dt.Rows[0]["_IsReceived"]))
                {
                    IDKhatm.Visible = true;
                }
                lblSarf.Text = "بموجبه يتم الصرف للسيد / ";
                IDUserDetails.Visible = true;
                pnlStar.Visible = false;

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
                lblSumSaraf.Text = toWord.ConvertToArabic();
            }
            else
            {
                pnlNullSarf.Visible = true;
                pnlDataSarf.Visible = false;
                ProductByUser.Visible = true;
                pnlStar.Visible = false;
            }
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
                lbmsg.Text = " إنشاء أمر صرف ";
                lbmsg.ForeColor = System.Drawing.Color.Black;
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

}