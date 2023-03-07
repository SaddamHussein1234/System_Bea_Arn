using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_CPanelManageWarehouse_PageManageProductShippingWarehouse : System.Web.UI.UserControl
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
            bool A120;
            A120 = Convert.ToBoolean(dtViewProfil.Rows[0]["A120"]);
            if (A120 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
            ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper2);

            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah2);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah3);

            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah2);

            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq2);

            Repostry_Company_.FCRM_Company_ManageView(DLCompany);

            txtDateCaming.Text = ClassSaddam.FGetDateTo();
            txt_Add.Text = txtDateCaming.Text;

            FGetAlBaheth();
            FGetStoragePlaces();
            FGetLastRecord();
            FGetLastRecordBill();
            FGetData();
        }
    }

    private void FGetLastRecordBill()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber = @0 And _IDMosTafeed = @0 And _IsDelete = @1 Order by _IDNaebRaees Desc", Convert.ToString(0), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblLastBill.Text = "آخر فاتورة : " + dt.Rows[0]["_IDNaebRaees"].ToString();
                txtNumberBill.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNaebRaees"].ToString()) + 1);
            }
            else
            {
                lblLastBill.Text = "آخر فاتورة : " + "1001";
                txtNumberBill.Text = "1001";
            }
        }
        catch (Exception)
        {
            return;
        }
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
        ImgIDStorekeeper.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLIDStorekeeper2.SelectedValue));
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah2.SelectedValue));
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq2.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah3.SelectedValue));
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            ClassProductShopWarehouse CPSW = new ClassProductShopWarehouse();
            CPSW.IDUniq = Convert.ToString(Request.QueryString["ID"]);
            CPSW.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPSW.BArnProductShopWarehouseByProductByID();
            if (dt.Rows.Count > 0)
            {
                DLCategory.SelectedValue = dt.Rows[0]["IDCategoryShop"].ToString();
                DLProduct.Items.Clear();
                DLProduct.Items.Add("");
                DLProduct.AppendDataBoundItems = true;
                FGetProductShopBtCategoryShop();
                DLProduct.SelectedValue = dt.Rows[0]["_IDProduct"].ToString();
                FArnProductShopWarehouseByProduct();
                Session["OldIDNumber"] = dt.Rows[0]["_IDNumberProduct"].ToString();
                txtIDNumberProduct.Text = Session["OldIDNumber"].ToString();
                txtCountProduct.Text = dt.Rows[0]["_CountProduct"].ToString();
                txtPriceOfTheGrain.Text = dt.Rows[0]["_PriceOfTheGrain"].ToString();
                txtTotalPrice.Text = dt.Rows[0]["_TotalPrice"].ToString();
                txtProductionDate.Text = Convert.ToDateTime(dt.Rows[0]["_ProductionDate"]).ToString("yyyy-MM-dd");
                txtExpiryDate.Text = Convert.ToDateTime(dt.Rows[0]["_ExpiryDate"]).ToString("yyyy-MM-dd");
                txtDateCaming.Text = Convert.ToDateTime(dt.Rows[0]["_DateCaming"]).ToString("dd-MM-yyyy");
                DLType.SelectedValue = dt.Rows[0]["_IDType"].ToString();
                Session["OldImg"] = dt.Rows[0]["_ImgProduct"].ToString();
                Img.ImageUrl = "/" + Session["OldImg"].ToString();
                DLStoragePlaces.SelectedValue = dt.Rows[0]["_IDProductStorage"].ToString();

                DLRaeesMaglesAlEdarah2.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah"].ToString();
                DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["_IDAmmenAlSondoq"].ToString();
                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["_IDModer"].ToString();
                DLIDStorekeeper.SelectedValue = dt.Rows[0]["_IDStorekeeper"].ToString();

                txtdescription.Text = dt.Rows[0]["_A1"].ToString();
                txtNumberBill.Text = dt.Rows[0]["_IDNaebRaees"].ToString();
                DLCompany.SelectedValue = dt.Rows[0]["_FromDonor"].ToString();
                txtThePurpose.Text = dt.Rows[0]["_The_Purpose"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل شحنة للمستودع";
            }
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _IsDelete = @0 Order by _IDNumberProduct Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["_IDNumberProduct"]) + 1);
        else
            txtIDNumberProduct.Text = "1001";
    }

    private void FGetStoragePlaces()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[StoragePlaces] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by StorageName", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLStoragePlaces.Items.Clear();
            DLStoragePlaces.Items.Add("");
            DLStoragePlaces.AppendDataBoundItems = true;
            DLStoragePlaces.DataValueField = "IDItem";
            DLStoragePlaces.DataTextField = "StorageName";
            DLStoragePlaces.DataSource = dt;
            DLStoragePlaces.DataBind();
        }
        FGetCategoryShop();
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
        Response.Redirect("PageManageProductShippingWarehouse.aspx");
    }

    protected void DLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DLProduct.Items.Clear();
            DLProduct.Items.Add("");
            DLProduct.AppendDataBoundItems = true;
            FGetProductShopBtCategoryShop();
            ProductByID.Visible = false;
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FArnProductShopWarehouseByProduct();
        FGetSumation();
    }

    protected void txtPriceOfTheGrain_TextChanged(object sender, EventArgs e)
    {
        if (txtCountProduct.Text != string.Empty)
        {
            try
            {
                txtTotalPrice.Text = Convert.ToString(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim()));
            }
            catch (Exception)
            {

            }
        }
        else
            lblCheckCountProduct.Text = "الكمية *";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "حفظ البيانات")
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
            dt = ClassDataAccess.GetData("Select Top(1) * from ProductShopWarehouse With(NoLock) Where _IDNumberProduct = @0 And _IsDelete = @1", txtIDNumberProduct.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lbmsg.Text = "لا يمكن تكرار رقم الشحنة قم بتغييره";
                lbmsg.ForeColor = Color.Red;
            }
            else
            {
                Session["OldIDNumber"] = txtIDNumberProduct.Text.Trim();
                FChackImgF();
            }
        }
        else
            FChackImgF();
    }

    private void FCheckNumber()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from ProductShopWarehouse With(NoLock) Where _IDNumberProduct = @0 And _IsDelete = @1", txtIDNumberProduct.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "لا يمكن تكرار رقم الشحنة قم بتغييره";
            lbmsg.ForeColor = Color.Red;
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from ProductShopWarehouse With(NoLock) Where _IDProduct = @0 And _IDNumberProduct = @1 And _IsDelete = @2", DLProduct.SelectedValue, txtIDNumberProduct.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة هذه الشحنة مسبقاً";
            lbmsg.ForeColor = Color.Red;
        }
        else
            FChackImgF();
    }

    private void FChackImgF()
    {
        if (FUArticle.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUArticle.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i <= validFileTypes.Length - 1; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (!isValidFile)
            {
                lbmsg.ForeColor = Color.Red;
                lbmsg.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimgArticle(FUArticle);
        }
        else
            FUpimgArticle(FUArticle);
    }

    protected void FUpimgArticle(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            System.Drawing.Image im = System.Drawing.Image.FromStream(strm);
            double h = im.PhysicalDimension.Height;
            double w = im.PhysicalDimension.Width;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = Convert.ToInt32(w); // 855; // New Width of Image in Pixel
                int newHeight = Convert.ToInt32(h); // 495; // New Height of Image in Pixel
                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgProductStorage/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["OldImg"] = "ImgSystem/ImgProductStorage/" + upl.FileName.Remove(3) + XRandom + ".png";
                FArnProductShopWarehouseAdd();
            }
        }
        else
        {
            if (btnAdd.Text == "حفظ البيانات")
            {
                Session["OldImg"] = "ImgSystem/ImgProductStorage/no-img.jpg";
                FArnProductShopWarehouseAdd();
            }
            else if (btnAdd.Text == "تعديل البيانات")
            {
                FArnProductShopWarehouseAdd();
            }

        }
    }

    private void FArnProductShopWarehouseAdd()
    {
        if (btnAdd.Text == "حفظ البيانات")
        {
            GetCookie();
            ClassProductShopWarehouse CPSW = new ClassProductShopWarehouse()
            {
                IDUniq = Convert.ToString(Guid.NewGuid()),
                billNumber = 0,
                IDMosTafeed = 0,
                IDProduct = Convert.ToInt64(DLProduct.SelectedValue),
                IDNumberProduct = Convert.ToInt64(txtIDNumberProduct.Text.Trim()),
                CountProduct = Convert.ToInt32(txtCountProduct.Text.Trim()),
                PriceOfTheGrain = Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim()),
                TotalPrice = Convert.ToDecimal(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim())),
                ProductionDate = txtProductionDate.Text.Trim(),
                ExpiryDate = txtExpiryDate.Text.Trim(),
                DateCaming = Convert.ToDateTime(txtDateCaming.Text.Trim()).ToString("yyyy/MM/dd"),
                IDType = DLType.SelectedValue,
                ImgProduct = Session["OldImg"].ToString(),
                IDProductStorage = Convert.ToInt32(DLStoragePlaces.SelectedValue),
                IsActive = true,
                DateAddProduct = txt_Add.Text.Trim(),
                IDAdmin = Convert.ToInt32(IDUser),
                IDUpdate = 0,
                DateUpDate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
                IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah2.SelectedValue),
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
                IDCategory = Convert.ToInt32(DLCategory.SelectedValue),
                IsDone = false,
                IsNotDone = false,
                IsReceived = false,
                IsNotReceived = false,
                IDDelivery = 0,
                IDNaebRaees = Convert.ToInt32(txtNumberBill.Text.Trim()),
                IsNaebRaees = false,
                FromDonor = DLCompany.SelectedValue,
                The_Purpose = txtThePurpose.Text.Trim(),
                Count_Cart = 0,
                Count_Families = 0
            };
            CPSW.BArnProductShopWarehouseAdd();
            lbmsg.Text = "تم إضافة الشحنة بنجاح";
            lbmsg.ForeColor = Color.MediumAquamarine;
            Img.ImageUrl = "/" + Session["OldImg"].ToString();
            FArnProductShopWarehouseByProduct();
            FGetSumation();
            FRefreshPage();
        }
        else if (btnAdd.Text == "تعديل البيانات")
        {
            GetCookie();
            ClassProductShopWarehouse CPSW = new ClassProductShopWarehouse()
            {
                IDUniq = Convert.ToString(Request.QueryString["ID"]),
                billNumber = 0,
                IDMosTafeed = 0,
                IDProduct = Convert.ToInt64(DLProduct.SelectedValue),
                IDNumberProduct = Convert.ToInt64(Session["OldIDNumber"]),
                CountProduct = Convert.ToInt32(txtCountProduct.Text.Trim()),
                PriceOfTheGrain = Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim()),
                TotalPrice = Convert.ToDecimal(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim())),
                ProductionDate = Convert.ToDateTime(txtProductionDate.Text.Trim()).ToString("yyyy/MM/dd"),
                ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text.Trim()).ToString("yyyy/MM/dd"),
                DateCaming = Convert.ToDateTime(txtDateCaming.Text.Trim()).ToString("yyyy/MM/dd"),
                IDType = DLType.SelectedValue,
                ImgProduct = Session["OldImg"].ToString(),
                IDProductStorage = Convert.ToInt32(DLStoragePlaces.SelectedValue),
                IDUpdate = Convert.ToInt32(IDUser),
                DateUpDate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
                IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah2.SelectedValue),
                IsRaeesMaglisAlEdarah = false,
                IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                IsAmmenAlSondoq = false,
                IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                IsModer = false,
                IDStorekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
                IsStorekeeper = false,
                A1 = txtdescription.Text.Trim(),
                A2_ = 1,
                A3 = txtThePurpose.Text.Trim(),
                A4 = "0",
                A5 = "0",
                IDCategory = Convert.ToInt32(DLCategory.SelectedValue),
                IsDone = false,
                IsNotDone = false,
                IsReceived = false,
                IsNotReceived = false,
                IDDelivery = 0,
                IDNaebRaees = Convert.ToInt32(txtNumberBill.Text.Trim()),
                FromDonor = DLCompany.SelectedValue,
                The_Purpose = txtThePurpose.Text.Trim()
            };
            CPSW.BArnProductShopWarehouseEdit();
            lbmsg.Text = "تم تعديل الشحنة بنجاح";
            lbmsg.ForeColor = Color.MediumAquamarine;
            Img.ImageUrl = "/" + Session["OldImg"].ToString();
            FGetData();
            FGetSumation();
        }
    }

    private void FRefreshPage()
    {
        DLCategory.SelectedValue = null;
        DLProduct.SelectedValue = null;
        txtIDNumberProduct.Text = Convert.ToString(Convert.ToInt64(txtIDNumberProduct.Text) + 1);
        txtCountProduct.Text = string.Empty;
        txtPriceOfTheGrain.Text = string.Empty;
        txtTotalPrice.Text = string.Empty;
        txtProductionDate.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;
        DLStoragePlaces.SelectedValue = null;
        //ProductByID.Visible = false;
    }

    private void FArnProductShopWarehouseByProduct()
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = 0;
            CPS.IDProduct = Convert.ToInt32(DLProduct.SelectedValue);
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopWarehouseByProduct();
            if (dt.Rows.Count > 0)
            {
                GVProductShopWarehouseByID.DataSource = dt;
                GVProductShopWarehouseByID.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                ProductByID.Visible = true;
                txtTitle.Text = "قائمة جميع الوارد للمستودع لـ " + DLCategory.SelectedItem.ToString() + " - " + DLProduct.SelectedItem.ToString() + " - إلى حينة ";
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                ProductByID.Visible = true;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductWarehouseCatchReceipt.aspx?ID=" + txtNumberBill.Text.Trim() + "&Type=Moder");
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        DLIDStorekeeper2.Visible = true;
        DLModerAlGmeiah2.Visible = true;
        DLAmeenAlSondoq2.Visible = true;
        DLRaeesMaglesAlEdarah3.Visible = true;
        lblIDStorekeeper.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblAmeenAlSondoq.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        GVProductShopWarehouseByID.Columns[0].Visible = true;
        GVProductShopWarehouseByID.Columns[13].Visible = true;
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

        GVProductShopWarehouseByID.Columns[0].Visible = false;
        GVProductShopWarehouseByID.Columns[13].Visible = false;

        lblIDStorekeeper.Text = DLIDStorekeeper2.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah2.SelectedItem.ToString();
        lblAmeenAlSondoq.Text = DLAmeenAlSondoq2.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah3.SelectedItem.ToString();
        Session["footable1"] = pnlData;
        //if (GVProductShopWarehouseByID.Rows.Count > 14)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        //}
        //else if (GVProductShopWarehouseByID.Rows.Count <= 14)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        //}
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVProductShopWarehouseByID.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVProductShopWarehouseByID.DataKeys[row.RowIndex].Value);
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
            FArnProductShopWarehouseByProduct();
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
    protected void GVProductShopWarehouseByID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
            Cou += int.Parse(Count.Text);
            lblSum.Text = Cou.ToString();

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            lblTotalPrice.Text = sum.ToString();
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