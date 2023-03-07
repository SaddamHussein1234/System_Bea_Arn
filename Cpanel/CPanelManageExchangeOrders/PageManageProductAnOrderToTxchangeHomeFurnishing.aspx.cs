using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
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

public partial class Cpanel_CPanelManageExchangeOrders_PageManageProductAnOrderToTxchangeHomeFurnishing : System.Web.UI.Page
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
            pnlStar.Visible = true;
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            FGetName();
            txtNumberMostafeed.Focus();
            FGetLastRecord();
            FGetCategoryShop();
            FGetLastRecordProssess();
            txtProductionDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            txt_Add.Text = txtProductionDate.Text;
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
        }
    }

    private void FGetLastRecordProssess()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 Order by _IDNumberProduct Desc", Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNumberProduct"]) + 1);
        else
            txtIDNumberProduct.Text = ClassSaddam.FGetNumberBillStart().ToString();
        FGetAmeenAlmostodaa();
    }

    private void FGetAmeenAlmostodaa()
    {
        ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
        ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper2);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah2);
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
        ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
        ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsNaebMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLNaeebRaeesMagles.DataValueField = "ID_Item";
            DLNaeebRaeesMagles.DataTextField = "FirstName";
            DLNaeebRaeesMagles.DataSource = dt;
            DLNaeebRaeesMagles.DataBind();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) [_billNumber] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 And _IsCart = @2 And _IsDevice = @3 And _IsTathith = @4 And _IsTalef = @5 Order by _billNumber Desc ",
            Convert.ToString(0), Convert.ToString(false), Convert.ToString(false), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_billNumber"]) + 1);
        else
            txtNumberOrder.Text = ClassSaddam.FGetNumberBillStart().ToString();
    }

    private void FGetName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) [IDItem],[NumberMostafeed],[NameMostafeed] FROM [dbo].[RasAlEstemarah] With(NoLock) Where TypeMostafeed = @0 And IsDelete = @1 Order By NameMostafeed", "دائم", Convert.ToString(false));
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductAnOrderToTxchangeHomeFurnishing.aspx");
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FGetDataMostafed();
            FCheckMostafeed();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And TypeMostafeed = @1 And IsDelete = @2", txtNumberMostafeed.Text.Trim(), "دائم", Convert.ToString(false));
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
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            pnlDataMosTafeed.Visible = true;
            pnlAlDaam.Visible = true;
            pnlStar.Visible = false;
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            //if (GVMatterOfExchangeByID.Rows.Count > 0)
            //{
            //    FArnProductShopMatterOfExchangeByUser();
            //}
            //else
            //{
            //    Response.Redirect("PageManageProductMatterOfExchange.aspx");
            //}
        }
        else
        {
            lblMessageWarning.Text = "يبدو ان هذا المستفيد مستبعد";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
            pnlStar.Visible = false;
        }
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FGetDataMostafedByName();
            FCheckMostafeed();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetDataMostafedByName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
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
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            pnlDataMosTafeed.Visible = true;
            pnlAlDaam.Visible = true;
            pnlStar.Visible = false;
        }
        else
        {
            pnlDataMosTafeed.Visible = false;
            pnlAlDaam.Visible = false;
            pnlStar.Visible = false;
        }
    }

    private void FCheckMostafeed()
    {
        if (DLName.SelectedItem.ToString() == "مشاريع لغير المستفيدين")
        {
            PnlOther.Visible = true;
            txtCount_Families.Focus();
        }
        else
            PnlOther.Visible = false;
    }

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[CategoryShop] With(NoLock) Where IDStore = @0 And (CategoryName <> @1) And IsActive = @2 And IsDelete = @3 Order by IDNumberCategory", "1", "الأجهزة الكهربائية", Convert.ToString(true), Convert.ToString(false));
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
        ClassQuaem.FGetSupportType(0, "'3'", DLSupportType);
    }

    private void FGetProductShopBtCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", DLCategory.SelectedValue, Convert.ToString(true), Convert.ToString(false));
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
    
    protected void DLProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Sum([CountProduct]) As 'Set' FROM [dbo].[ProductShop] With(NoLock) Where ProductID = @0 And IsDelete = @1", DLProduct.SelectedValue, Convert.ToString(false));
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
                lblMessageWarning.Text = "الكمية المتبقية لمنتج " + DLProduct.SelectedItem.ToString() + " " + "0";
            }
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
            dt = ClassDataAccess.GetData("Select Top(100) * from ProductShopWarehouse With(NoLock) Where _IDNumberProduct = @0 And _IsDelete = @1", txtIDNumberProduct.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblMessageWarning.Text = "لا يمكن تكرار رقم الشحنة قم بتغييره";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
            }
            else
            {
                Session["OldIDNumber"] = txtIDNumberProduct.Text.Trim();
                FArnProductShopWarehouseAddForMostafeed();
            }
        }
        else
            FArnProductShopWarehouseAddForMostafeed();
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
        dt = ClassDataAccess.GetData("Select Top(10) * from ProductShopWarehouse With(NoLock) Where _billNumber <> @0 And _IDNumberProduct = @1 And _IsDelete = @2", Convert.ToString(0), txtIDNumberProduct.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblMessageWarning.Text = "لا يمكن تكرار رقم الطلب قم بتغييره";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(100) * from ProductShopWarehouse With(NoLock) Where _billNumber <> @0 And _billNumber = @1 And _IDProduct = @2 And _IsDelete = @3", Convert.ToString(0), txtNumberOrder.Text.Trim(), DLProduct.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblMessageWarning.Text = "لا يمكن تكرار الدعم بنفس الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
        }
        else
            FArnProductShopWarehouseAddForMostafeed();
    }

    private void FArnProductShopWarehouseAddForMostafeed()
    {
        if (btnAdd.Text == "إضافة للفاتورة")
        {
            int XCountFamilies = 0, XCount_Cart = 0;
            if (PnlOther.Visible)
            {
                if (txtCount_Families.Text.Trim() == string.Empty || txtCount_Cart.Text.Trim() == string.Empty)
                {
                    lblMessageWarning.Text = "يرجى إدخال عدد الأسر ومرات الدعم ... ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    txtCount_Families.Focus();
                    return;
                }
                else
                {
                    XCountFamilies = Convert.ToInt32(txtCount_Families.Text.Trim());
                    XCount_Cart = Convert.ToInt32(txtCount_Cart.Text.Trim());
                }
            }

            DataTable dtProduct = new DataTable();
            dtProduct = ClassDataAccess.GetData("Select Top(100) ProductID,CountProduct from ProductShop With(NoLock) Where ProductID = @0 ", DLProduct.SelectedValue);
            if (dtProduct.Rows.Count > 0)
            {
                float XSumation = 0;
                XSumation = Convert.ToInt64(dtProduct.Rows[0]["CountProduct"]);
                // جلب الوارد إلى المستودع
                DataTable dtPrice = new DataTable();
                dtPrice = ClassDataAccess.GetData("SELECT TOP 1 [_IDItem],[_IDNumberProduct],[_CountProduct],[_PriceOfTheGrain] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber = @0 And _IDMosTafeed = @0 And _IDProduct = @1 And _IsDelete = @2 Order by _IDItem Desc ", Convert.ToString(0), DLProduct.SelectedValue, Convert.ToString(false));

                // Write Code Hear
                DataTable dtRequestOfThrBeneficiary = new DataTable();
                dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And TathithManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
                if (dtRequestOfThrBeneficiary.Rows.Count > 0)
                {
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
                            ProductionDate = Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy/MM/dd"),
                            ExpiryDate = Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy/MM/dd"),
                            DateCaming = Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy/MM/dd"),
                            IDType = "1",
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
                            A2_ = Convert.ToInt32(DLInitiatives.SelectedValue),
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
                            FromDonor = DLCompany.SelectedValue,
                            The_Purpose = "0",
                            IsCart = false,
                            IsDevice = false,
                            IsTathith = true,
                            IsTalef = false,
                            Count_Cart = XCount_Cart,
                            Count_Families = XCountFamilies
                        };
                        CPSW.BArnProductShopWarehouseAdd();
                        DataTable dt = new DataTable();
                        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IsDelete = @1 Order by _IDNumberProduct Desc", Convert.ToString(0), Convert.ToString(false));
                        if (dt.Rows.Count > 0)
                            txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNumberProduct"]) + 1);
                        else
                            txtIDNumberProduct.Text = ClassSaddam.FGetNumberBillStart().ToString();
                        FArnProductShopMatterOfExchangeByUser();
                        FGetSumation();
                        lblMessage.Text = "تم الإضافة للفاتورة بنجاح ... ";
                        IDMessageSuccess.Visible = true;
                        IDMessageWarning.Visible = false;
                        txtCountProduct.Text = string.Empty;
                    }
                }
                else
                {
                    lblMessageWarning.Text = " (المستفيد لا يحتاج إلى تأثيث منزل) ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
            }
        }
    }
    
    protected void CBFinish_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtRequestOfThrBeneficiary = new DataTable();
            dtRequestOfThrBeneficiary = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[ReportAlZyarat] With(NoLock) Where NumberMostafeed = @0 And TathithManzil = @1 And IsDelete = @2 Order by IDItem Desc", txtNumberMostafeed.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
            if (dtRequestOfThrBeneficiary.Rows.Count > 0)
            {
                if (CBFinish.Checked)
                {
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ReportAlZyarat] SET [TathithManzil] = @TathithManzil Where IDItem = @IDItem And NumberMostafeed = @IDMustafeed";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Convert.ToInt64(dtRequestOfThrBeneficiary.Rows[0]["IDItem"].ToString()));
                    cmd.Parameters.AddWithValue("@IDMustafeed", Convert.ToInt64(txtNumberMostafeed.Text.Trim()));
                    cmd.Parameters.AddWithValue("@TathithManzil", false);
                    cmd.ExecuteScalar();
                    conn.Close();
                    lblMessage.Text = " تمت العملية بنجاح ";
                    IDMessageSuccess.Visible = true;
                    IDMessageWarning.Visible = false;
                }
                else if (CBFinish.Checked == false)
                {
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ReportAlZyarat] SET [TathithManzil] = @TathithManzil Where IDItem = @IDItem And NumberMostafeed = @IDMustafeed";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Convert.ToInt64(dtRequestOfThrBeneficiary.Rows[0]["IDItem"].ToString()));
                    cmd.Parameters.AddWithValue("@IDMustafeed", Convert.ToInt64(txtNumberMostafeed.Text.Trim()));
                    cmd.Parameters.AddWithValue("@TathithManzil", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            else
            {
                lblMessageWarning.Text = " (المستفيد لا يحتاج إلى تأثيث منزل) ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }            
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void LBR_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByID.Columns[0].Visible = true;
        FArnProductShopMatterOfExchangeByUser();
        pnllblPrint.Visible = false;
        pnlDlPrint.Visible = true;
        //lblDateShow.Visible = true;
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
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        else if (GVMatterOfExchangeByID.Rows.Count <= 15)
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
    private void FArnProductShopMatterOfExchangeByUser()
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(txtNumberOrder.Text.Trim());
            CPS.IDMosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
            CPS.IsDelete = false;
            CPS.IsDelete = false;
            CPS.IsCart = false;
            CPS.IsDevice = false;
            CPS.IsTathith = true;
            CPS.IsTalef = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopMatterOfExchangeByUser();
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
                    IDUpdate.Visible = false;

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
                    IDNotReceived.Visible = false;
                if (Convert.ToBoolean(dt.Rows[0]["_IsDone"]) == false && Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]) == false)
                    lblDateGo.Text = "بإنتظار الملاحظة";
                else if (Convert.ToBoolean(dt.Rows[0]["_IsDone"]) == true || Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]) == true)
                    lblDateGo.Text = Convert.ToDateTime(dt.Rows[0]["_ExpiryDate"]).ToString("yyyy/MM/dd");
                if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]) == false && Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]) == false)
                    lblDateRecived.Text = "بإنتظار التسليم";
                else if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]) == true || Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]) == true)
                    lblDateRecived.Text = Convert.ToDateTime(dt.Rows[0]["_DateCaming"]).ToString("yyyy/MM/dd");
                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]))
                {
                    ImgAmeenAlsondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDAmmenAlSondoq"]), Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]));
                    ImgAmeenAlsondoq.Width = 100;
                    ImgAmeenAlsondoq.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoq.ImageUrl = "/Cpanel/loaderMin.gif";
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
                    ImgRaees.ImageUrl = "/Cpanel/loaderMin.gif";
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
                    ImgModer.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                }
                lblAmeenAlmosTodaa2.Text = DLIDStorekeeper2.SelectedItem.ToString();
                if (Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]))
                {
                    ImgAmeenAlmosTodaa.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]), Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]));
                    ImgAmeenAlmosTodaa.Width = 100;
                    ImgAmeenAlmosTodaa.Visible = true;
                }
                else
                {
                    ImgAmeenAlmosTodaa.ImageUrl = "/Cpanel/loaderMin.gif";
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
                    ImgAlBaheth.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAlBaheth.Width = 30;
                    ImgAlBaheth.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]) && Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]) && Convert.ToBoolean(dt.Rows[0]["_IsModer"]) && Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]) && Convert.ToBoolean(dt.Rows[0]["_IsReceived"]))
                    IDKhatm.Visible = true;
                if (dt.Rows[0]["Name_Initiatives_"].ToString() != "بدون مبادرة")
                    lbl_Initiatives.Text = dt.Rows[0]["Name_Initiatives_"].ToString();
                else
                    lbl_Initiatives.Text = string.Empty;

                if (dt.Rows[0]["_A1"].ToString() != string.Empty)
                {
                    DivNote.Visible = true;
                    lblNote.Text = dt.Rows[0]["_A1"].ToString();
                }
                else
                    DivNote.Visible = false;
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
                lblTotalPrice.Text = sum.ToString();
            else
                lblTotalPrice.Text = "بإنتظار التسعير";
        }
    }

    protected void LBGetBill_Click(object sender, EventArgs e)
    {
        FArnProductShopMatterOfExchangeByUser();
    }

    protected void DLCategory_SelectedIndexChanged1(object sender, EventArgs e)
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

}