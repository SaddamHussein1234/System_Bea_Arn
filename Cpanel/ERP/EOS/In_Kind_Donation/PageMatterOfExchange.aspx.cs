using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Models;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_In_Kind_Donation_PageMatterOfExchange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A106");
            HFXID.Value = Guid.NewGuid().ToString();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FCheckYears();
            pnlStar.Visible = true;
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            ClassQuaem.FGetSupportType(1, "'1','2','3'", DLSupportType);
            ClassMosTafeed.FGetName(DLName);
            txtNumberMostafeed.Focus();
            FGetAmeenAlmostodaa();
            txt_Add.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
            pnlDataMosTafeed.Visible = false;
            pnlMostafeed.Visible = true;
            pnlStar.Visible = false;
            pnlAlDaam.Visible = false;
            txtNumberMostafeed.Focus();
            FGetCategoryShop();
            pnlStarView.Visible = true;
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
        MEOB.IDCheck = "GetByID";
        MEOB.ID_Item = new Guid(Request.QueryString["ID"]);
        MEOB.ID_FinancialYear = Guid.Empty;
        MEOB.ID_Donor = Guid.Empty;
        MEOB.bill_Number = 0;
        MEOB.ID_MosTafeed = 0;
        MEOB.Start_Date = string.Empty;
        MEOB.End_Date = string.Empty;
        MEOB.DataCheck = string.Empty;
        MEOB.DataCheck2 = string.Empty;
        MEOB.DataCheck3 = string.Empty;
        MEOB.Is_Cart = false;
        MEOB.Is_Device = false;
        MEOB.Is_Tathith = false;
        MEOB.Is_Talef = false;
        MEOB.IsActive = true;
        DataTable dt = new DataTable();
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        dt = REOB.BWSM_Exchange_Order_Bill_Manage(MEOB);
        if (dt.Rows.Count > 0)
        {
            IDSelectDoner.Visible = false; DLCategory.Enabled = true; DLProduct.Enabled = true;
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberBill.Enabled = true;
            txtNumberMostafeed.Focus();
            lblStar.Text = "يرجى تحديد بيانات المستفيد";

            HFXID.Value = dt.Rows[0]["_ID_Item_"].ToString();
            ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
            DLCompany.SelectedValue = dt.Rows[0]["_ID_Donor_"].ToString();
            txtNumberBill.Text = dt.Rows[0]["_bill_Number_"].ToString();
            txtNumberMostafeed.Text = dt.Rows[0]["_ID_MosTafeed_"].ToString();
            DLName.SelectedValue = txtNumberMostafeed.Text;
            FGetDataMostafedByName();
            FCheckMostafeed();
            DLInitiatives.SelectedValue = dt.Rows[0]["_The_Initiative_"].ToString();
            DLSupportType.SelectedValue = dt.Rows[0]["_ID_Project_"].ToString();

            DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["_ID_Raees_Maglis_AlEdarah_"].ToString();
            DLNaeebRaeesMagles.SelectedValue = dt.Rows[0]["_ID_Naeb_Raees_"].ToString();
            DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["_ID_Ammen_AlSondoq_"].ToString();
            DLModerAlGmeiah.SelectedValue = dt.Rows[0]["_ID_Moder_"].ToString();
            DLIDStorekeeper.SelectedValue = dt.Rows[0]["_ID_Storekeeper_"].ToString();
            txtdescription.Text = dt.Rows[0]["_Note_"].ToString();
            DLAlBaheth.SelectedValue = dt.Rows[0]["_ID_Delivery_"].ToString();
            txtCount_Cart.Text = dt.Rows[0]["_Count_Cart_"].ToString();
            txtCount_Families.Text = dt.Rows[0]["_Count_Families_"].ToString();
            txtCount_Qariah.Text = dt.Rows[0]["_Count_Qariah_"].ToString();
            txt_Add.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
            FCheckYears();
            FGetByBill();
        }
    }

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT Top(1000) * FROM [dbo].[CategoryShop] With(NoLock) Where IDStore = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberCategory", "1", Convert.ToString(true), Convert.ToString(false));
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

    private void FGetAmeenAlmostodaa()
    {
        ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);

        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);

        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsNaebMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLNaeebRaeesMagles.DataValueField = "ID_Item";
            DLNaeebRaeesMagles.DataTextField = "FirstName";
            DLNaeebRaeesMagles.DataSource = dt;
            DLNaeebRaeesMagles.DataBind();
        }
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
        ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);

        ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageMatterOfExchange.aspx");
    }

    protected void DLSupportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            DLCompany.Enabled = true;
            txtNumberMostafeed.Enabled = true;
            DLName.Enabled = true;
            txtNumberBill.Enabled = true;
            txtNumberMostafeed.Focus();
            lblStar.Text = "يرجى تحديد بيانات المستفيد";
            FGetLastBill();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetLastBill()
    {
        txtNumberBill.Text = WSM_Repostry_Exchange_Order_Bill_.FGetLastBill(new Guid(ddlYears.SelectedValue), Convert.ToInt32(DLSupportType.SelectedValue)).ToString();
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
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
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
            if (DLName.SelectedItem.ToString() != "مشاريع لغير المستفيدين")
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

        }
    }

    private void FGetDataMostafedByName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
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
            if (DLName.SelectedItem.ToString() != "مشاريع لغير المستفيدين")
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

    private void FCheckMostafeed()
    {
        if (DLName.SelectedItem.Text == "مشاريع لغير المستفيدين")
        {
            ID504.Visible = true; txtCount_Qariah.Visible = true; RFCount_Qariah.Visible = true; txtCount_Families.Visible = true; 
            RFCount_Families.Visible = true; txtCount_Cart.Visible = true; RFCount_Cart.Visible = true;
            txtCount_Qariah.Focus();
        }
        else
        {
            ID504.Visible = false; txtCount_Qariah.Visible = false; RFCount_Qariah.Visible = false; txtCount_Families.Visible = false;
            RFCount_Families.Visible = false; txtCount_Cart.Visible = false; RFCount_Cart.Visible = false;
        }
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
            return;
        }
    }

    private void FGetProductShopBtCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT Top(5000) * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", DLCategory.SelectedValue, Convert.ToString(true), Convert.ToString(false));
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
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;

            if (ClassSaddam.XCheckYear(ddlYears.SelectedItem.Text))
                lblMessageWarning.Text = " - مجموع الشحنات لمنتج " + DLProduct.SelectedItem.Text + " ( " + FGetSumationAll().ToString() + " منتج  ), لسنة " + ddlYears.SelectedItem.Text;
            else
                lblMessageWarning.Text = "لا يتم حساب الكمية في السنين السابقة ,,,";
            WSM_Repostry_In_Kind_Donation_Details_.FGet_Price("GetSumForProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt64(DLProduct.SelectedValue), DL_Price, new Guid(ddlYears.SelectedValue));
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "الكمية المتبقية لمنتج " + DLProduct.SelectedItem.ToString() + " " + "0";
        }
    }

    int Cou = 0;
    decimal sum = 0;
    private Int64 FGetSumationAll()
    {
        Int64 XResult = 0;
        Int64 Getsum = 0, Setsum = 0;
        Setsum = WSM_Repostry_In_Kind_Donation_Details_.FGetCountProduct("GetCountProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt64(DLProduct.SelectedValue), string.Empty, new Guid(ddlYears.SelectedValue));
        Getsum = WSM_Repostry_Exchange_Order_Details_.FGetCountProduct("GetCountProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt32(DLProduct.SelectedValue), string.Empty, new Guid(ddlYears.SelectedValue));
        XResult = Setsum - Getsum;
        HFCountProduct.Value = XResult.ToString();
        return XResult;
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {

    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            lblStar.Text = "يرجى تحديد بيانات المستفيد";
            txt_Add.Text = Convert.ToDateTime(txt_Add.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
            FCheckYears();
            FGetLastBill();
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

    private void FCheckYears()
    {
        if (ClassSaddam.XCheckYear(ddlYears.SelectedItem.Text))
        { PnlSelectPrice.Visible = true; PnlInputPrice.Visible = false; }
        else
        { PnlSelectPrice.Visible = false; PnlInputPrice.Visible = true; }
    }

    protected void DL_Price_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;

        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            if (ClassSaddam.XCheckYear(ddlYears.SelectedItem.Text))
                lblMessageWarning.Text = "- المتبقي من هذة التسعيرة " + DLProduct.SelectedItem.Text + " ( " +
                FGetSumationByProject().ToString() + " منتج  ), ";
            else
                lblMessageWarning.Text = "لا يتم حساب الكمية في السنين السابقة ,,,";
            txtCountProduct.Focus();
        }
        catch (Exception)
        {

        }
    }

    private Int64 FGetSumationByProject()
    {
        Int64 XResult = 0;
        Int64 Getsum = 0, Setsum = 0;
        string XPrice = DL_Price.SelectedItem.Text.Split(new char[] { '[', ']' })[1];
        Setsum = WSM_Repostry_In_Kind_Donation_Details_.FGetCountProduct("GetCountSelectProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt64(DLProduct.SelectedValue), XPrice, new Guid(ddlYears.SelectedValue));
        Getsum = WSM_Repostry_Exchange_Order_Details_.FGetCountProduct("GetCountSelectProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt32(DLProduct.SelectedValue), XPrice, new Guid(ddlYears.SelectedValue));
        XResult = Setsum - Getsum;
        return XResult;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
            {
                if (ClassSaddam.XCheckYear(ddlYears.SelectedItem.Text))
                {
                    Int64 XSumation = FGetSumationByProject();
                    if (Convert.ToInt64(txtCountProduct.Text.Trim()) <= Convert.ToInt64(XSumation))
                        FWSM_In_Kind_Donation_Details_Add();
                    else if (Convert.ToInt64(txtCountProduct.Text.Trim()) > Convert.ToInt64(XSumation))
                    {
                        IDMessageSuccess.Visible = false;
                        IDMessageWarning.Visible = true;
                        lblMessageWarning.Text = "الكمية المتبقية " + XSumation.ToString() + " لقد طلبت كمية أكثر من التي بالمستودع";
                    }
                }
                else
                    FWSM_In_Kind_Donation_Details_Add();
            }
            else
            {
                lblMessageWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }
    
    private void FWSM_In_Kind_Donation_Details_Add()
    {
        string XPrice = string.Empty , XCount_Partition = string.Empty;
        if (ClassSaddam.XCheckYear(ddlYears.SelectedItem.Text))
        {
            XPrice = DL_Price.SelectedItem.Text.Split(new char[] { '[', ']' })[1];
            XCount_Partition = DL_Price.SelectedItem.Text.Split(new char[] { '[', ']' })[3];
        }
        else
        {
            XPrice = txtPrice.Text.Trim();
            XCount_Partition = txtCount_Partition.Text.Trim();
        }
        bool XIs_There_Partition = false;
        if (XCount_Partition != "0" && XCount_Partition != "1" && XCount_Partition != string.Empty)
            XIs_There_Partition = true;
        else XIs_There_Partition = false;
        string Xresult = string.Empty;
        WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_()
        {
            IDCheck = "Add",
            IDItem = Guid.NewGuid(),
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            ID_Bill = new Guid(HFXID.Value),
            ID_Donor = new Guid(DLCompany.SelectedValue),
            bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
            ID_MosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            ID_Product = Convert.ToInt32(DLProduct.SelectedValue),
            Count_Product = Convert.ToInt32(txtCountProduct.Text.Trim()),
            One_Price = Convert.ToDecimal(XPrice),
            Total_Price = Convert.ToDecimal(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(XPrice)),
            ID_Project = Convert.ToInt32(DLSupportType.SelectedValue),
            Is_There_Partition = XIs_There_Partition,
            Count_Partition = Convert.ToInt32(XCount_Partition),
            Sum_Partition = Convert.ToInt32(txtCountProduct.Text.Trim()) * Convert.ToInt32(XCount_Partition),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            CreatedDate = Convert.ToDateTime(txt_Add.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            DeleteBy = 0,
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true
        };
        WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
        Xresult = REOD.FWSM_Exchange_Order_Details_Add(MEOD);
        if (Xresult == "IsExistsAdd")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "تم إضافة البيانات سابقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccessAdd")
        {
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int ID_Qariah = ClassMosTafeed.FGetMosTafeedIDQariah(Convert.ToInt32(txtNumberMostafeed.Text.Trim()));

            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblMessage.Text = "تم إضافة البيانات بنجاح ... ";
            DLProduct.SelectedValue = null;
            txtCountProduct.Text = string.Empty;
            DL_Price.SelectedValue = null;
            FGetByBill();
            if (Request.QueryString["id"] != null)
                FEdit(XDate, ID_Qariah);
        }
    }

    private void FGetByBill()
    {
        try
        {
            GVDeedDonationInKind.DataBind();
            WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_();
            MEOD.IDCheck = "GetByBill";
            MEOD.IDItem = new Guid(HFXID.Value);
            MEOD.ID_FinancialYear = Guid.Empty;
            MEOD.ID_Donor = Guid.Empty;
            MEOD.bill_Number = 0;
            MEOD.ID_MosTafeed = 0;
            MEOD.Start_Date = string.Empty;
            MEOD.End_Date = string.Empty;
            MEOD.DataCheck = string.Empty;
            MEOD.DataCheck2 = string.Empty;
            MEOD.DataCheck3 = string.Empty;
            MEOD.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
            dt = REOD.BWSM_Exchange_Order_Details_Manage(MEOD);
            if (dt.Rows.Count > 0)
            {
                GVDeedDonationInKind.DataSource = dt;
                GVDeedDonationInKind.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                ProductByID.Visible = true;
                txtTitle.Text = "تقاصيل الفاتورة برقم " + txtNumberBill.Text.Trim() + ", للمستفيد " + DLName.SelectedItem.ToString();
                txtTitle.Focus();
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                ProductByID.Visible = true;
                //IDMessageSuccess.Visible = false;
                //IDMessageWarning.Visible = true;
                //lblMessageWarning.Text = "لا توجد نتائج ... ";
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (DLSupportType.SelectedValue != null && DLCompany.SelectedValue != null && txtNumberMostafeed.Text.Trim() != string.Empty && txtNumberBill.Text.Trim() != string.Empty)
            {
                if (Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                    FWSM_Exchange_Order_Bill_Add();
                else
                {
                    lblMessageWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
            }
            else
            {
                lblMessageWarning.Text = "يُرجى إدخال بيانات الفاتورة ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FWSM_Exchange_Order_Bill_Add()
    {
        if (GVDeedDonationInKind.Rows.Count > 0)
        {
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            string Xresult = string.Empty;
            int XCount_Qariah = 0, XCountFamilies = 0, XCount_Cart = 0, ID_Qariah = ClassMosTafeed.FGetMosTafeedIDQariah(Convert.ToInt32(txtNumberMostafeed.Text.Trim()));
            if (ID504.Visible)
            {
                if (txtCount_Qariah.Text.Trim() == string.Empty || txtCount_Families.Text.Trim() == string.Empty || txtCount_Cart.Text.Trim() == string.Empty)
                {
                    lblMessageWarning.Text = "يرجى إدخال عدد الُقرى وعدد الأسر والعدد الموزع ... ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    txtCount_Families.Focus();
                    return;
                }
                else
                {
                    XCount_Qariah = Convert.ToInt32(txtCount_Qariah.Text.Trim());
                    XCountFamilies = Convert.ToInt32(txtCount_Families.Text.Trim());
                    XCount_Cart = Convert.ToInt32(txtCount_Cart.Text.Trim());
                }
            }
            if (Request.QueryString["id"] == null)
            {
                WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_()
                {
                    IDCheck = "Add",
                    ID_Item = new Guid(HFXID.Value),
                    ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                    ID_Donor = new Guid(DLCompany.SelectedValue),
                    bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
                    ID_MosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
                    The_Initiative = Convert.ToInt32(DLInitiatives.SelectedValue),
                    ID_Project = Convert.ToInt32(DLSupportType.SelectedValue),
                    ID_Type_Shipment = "1",
                    Img_Product = "0",
                    ID_Raees_Maglis_AlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                    Is_Raees_Maglis_AlEdarah = false,
                    Date_Raees_Allow = XDate,
                    ID_Raees_Allow = 0,
                    ID_Naeb_Raees = Convert.ToInt32(DLNaeebRaeesMagles.SelectedValue),
                    Is_Naeb_Raees = false,
                    Date_Naeb_Raees_Allow = XDate,
                    ID_Naeb_Raees_Allow = 0,
                    ID_Ammen_AlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                    Is_Ammen_AlSondoq = false,
                    Date_Ammen_AlSondoq_Allow = XDate,
                    ID_Ammen_AlSondoq_Allow = 0,
                    ID_Moder = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                    Is_Moder = false,
                    Date_Moder_Allow = XDate,
                    ID_Moder_Allow = 0,
                    ID_Storekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
                    Is_Storekeeper = false,
                    Date_Storekeeper_Allow = XDate,
                    ID_Storekeeper_Allow = 0,
                    Note = txtdescription.Text.Trim(),
                    Is_Done = false,
                    Is_Not_Done = false,
                    Is_Received = false,
                    Is_Not_Received = false,
                    Note_Not_Received = string.Empty,
                    ID_Delivery = Convert.ToInt32(DLAlBaheth.SelectedValue),
                    ID_Delivery_Allow = 0,
                    The_Purpose = XDate,
                    Is_Cart = true,
                    Is_Device = false,
                    Is_Tathith = false,
                    Is_Talef = false,
                    Count_Cart = XCount_Cart,
                    Count_Families = XCountFamilies,
                    Count_Qariah = XCount_Qariah,
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    CreatedDate = Convert.ToDateTime(txt_Add.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifiedBy = 0,
                    ModifiedDate = XDate,
                    DeleteBy = 0,
                    DeleteDate = XDate,
                    IsActive = true,
                    AlQaryah = ID_Qariah
                };
                WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
                Xresult = REOB.FWSM_Exchange_Order_Bill_Add(MEOB);
                if (Xresult == "IsExistsNumberAdd")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblMessageWarning.Text = "لا يمكن تكرار رقم الفاتورة ... ";
                    return;
                }
                else if (Xresult == "IsExistsAdd")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblMessageWarning.Text = "تم إضافة هذه الفاتورة مسبقاً , قم يتغير رقم الفاتورة ... ";
                    return;
                }
                else if (Xresult == "IsSuccessAdd")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblMessage.Text = "تم إضافة البيانات بنجاح ... ";
                    HFXID.Value = Guid.NewGuid().ToString();
                    if (Attach_Repostry_SMS_Send_.AllSendSystemEOS())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة صرف عيني" + "\n" + DLSupportType.SelectedItem.ToString() + "\n" + "ر/الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());

                    FGetLastBill(); 
                    FGetByBill();
                    txtNumberMostafeed.Text = string.Empty;
                    DLName.SelectedValue = null;
                    pnlDataMosTafeed.Visible = false;
                    txtCountProduct.Text = string.Empty;
                    ProductByID.Visible = false;
                    //string XURL = "PageView.aspx?IDUniq=" + MEOB.ID_FinancialYear.ToString() + "&ID=" + MEOB.bill_Number.ToString() + "&XID=" +
                    //    MEOB.ID_MosTafeed.ToString() + "&XIDCate=" + MEOB.ID_Project.ToString() + "&IsCart=" + MEOB.Is_Cart.ToString() +
                    //    "&IsDevice=" + MEOB.Is_Device.ToString() + "&IsTathith=" + MEOB.Is_Tathith.ToString() + "&IsTalef=" + MEOB.Is_Talef.ToString();
                    //Response.Redirect(XURL);
                }
            }
            else if (Request.QueryString["id"] != null)
                FEdit(XDate, ID_Qariah);
        }
        else
        {
            lblMessageWarning.Text = "لم يتم إضافة الأصناف بعد";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FEdit(string XDate, int ID_Qariah)
    {
        int XCount_Qariah = 0, XCountFamilies = 0, XCount_Cart = 0;
        if (ID504.Visible)
        {
            if (txtCount_Qariah.Text.Trim() == string.Empty || txtCount_Families.Text.Trim() == string.Empty || txtCount_Cart.Text.Trim() == string.Empty)
            {
                lblMessageWarning.Text = "يرجى إدخال عدد الُقرى وعدد الأسر والعدد الموزع ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                txtCount_Families.Focus();
                return;
            }
            else
            {
                XCount_Qariah = Convert.ToInt32(txtCount_Qariah.Text.Trim());
                XCountFamilies = Convert.ToInt32(txtCount_Families.Text.Trim());
                XCount_Cart = Convert.ToInt32(txtCount_Cart.Text.Trim());
            }
        }
        string Xresult = string.Empty;
        WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_()
        {
            IDCheck = "Edit",
            ID_Item = new Guid(Request.QueryString["ID"]),
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            ID_Donor = new Guid(DLCompany.SelectedValue),
            bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
            ID_MosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            The_Initiative = Convert.ToInt32(DLInitiatives.SelectedValue),
            ID_Project = Convert.ToInt32(DLSupportType.SelectedValue),
            ID_Type_Shipment = "1",
            Img_Product = "0",
            ID_Raees_Maglis_AlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            Is_Raees_Maglis_AlEdarah = false,
            Date_Raees_Allow = XDate,
            ID_Raees_Allow = 0,
            ID_Naeb_Raees = Convert.ToInt32(DLNaeebRaeesMagles.SelectedValue),
            Is_Naeb_Raees = false,
            Date_Naeb_Raees_Allow = XDate,
            ID_Naeb_Raees_Allow = 0,
            ID_Ammen_AlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
            Is_Ammen_AlSondoq = false,
            Date_Ammen_AlSondoq_Allow = XDate,
            ID_Ammen_AlSondoq_Allow = 0,
            ID_Moder = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
            Is_Moder = false,
            Date_Moder_Allow = XDate,
            ID_Moder_Allow = 0,
            ID_Storekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
            Is_Storekeeper = false,
            Date_Storekeeper_Allow = XDate,
            ID_Storekeeper_Allow = 0,
            Note = txtdescription.Text.Trim(),
            Is_Done = false,
            Is_Not_Done = false,
            Is_Received = false,
            Is_Not_Received = false,
            Note_Not_Received = string.Empty,
            ID_Delivery = Convert.ToInt32(DLAlBaheth.SelectedValue),
            ID_Delivery_Allow = 0,
            The_Purpose = XDate,
            Is_Cart = true,
            Is_Device = false,
            Is_Tathith = false,
            Is_Talef = false,
            Count_Cart = XCount_Cart,
            Count_Families = XCountFamilies,
            Count_Qariah = XCount_Qariah,
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            CreatedDate = Convert.ToDateTime(txt_Add.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = 0,
            ModifiedDate = XDate,
            DeleteBy = 0,
            DeleteDate = XDate,
            IsActive = true,
            AlQaryah = ID_Qariah
        };
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        Xresult = REOB.FWSM_Exchange_Order_Bill_Add(MEOB);
        if (Xresult == "IsExistsNumberAdd")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "لا يمكن تكرار رقم الفاتورة ... ";
            return;
        }
        else if (Xresult == "IsExistsEdit")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "تم إضافة هذه الفاتورة مسبقاً , قم يتغير رقم الفاتورة ... ";
            return;
        }
        else if (Xresult == "IsSuccessEdit")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblMessage.Text = "تمت العملية بنجاح ... ";
            if (Attach_Repostry_SMS_Send_.AllSendSystemEOS())
                Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل صرف عيني" + "\n" + DLSupportType.SelectedItem.ToString() + "\n" + "ر/الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
            FGetData();
        }
    }

    protected void LBRefresh2_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete1_Click1(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int ID_Qariah = ClassMosTafeed.FGetMosTafeedIDQariah(Convert.ToInt32(txtNumberMostafeed.Text.Trim()));
            foreach (GridViewRow row in GVDeedDonationInKind.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid Comp_ID = new Guid(GVDeedDonationInKind.DataKeys[row.RowIndex].Value.ToString());

                    WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_()
                    {
                        IDCheck = "Delete",
                        IDItem = Comp_ID,
                        ID_FinancialYear = Guid.Empty,
                        ID_Bill = Guid.Empty,
                        ID_Donor = Guid.Empty,
                        bill_Number = 1,
                        ID_MosTafeed = 1,
                        ID_Project = 1,
                        Count_Product = 1,
                        One_Price = 1,
                        Total_Price = 1,
                        ID_Product = 1,
                        Is_There_Partition = false,
                        Count_Partition = 0,
                        Sum_Partition = 0,
                        CreatedBy = 1,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
                    Xresult = REOD.FWSM_Exchange_Order_Details_Add(MEOD);
                }
            }
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblMessage.Text = "تم حذف الصنف بنجاح ... ";
                FGetByBill();
                if (Request.QueryString["id"] != null)
                    FEdit(XDate, ID_Qariah);
            }
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "حدث خطأ غير متوقع , حاول لاحقاً ... ";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    protected void GVDeedDonationInKind_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
        catch (Exception)
        {

        }
    }

    protected void LBGetBill_Click(object sender, EventArgs e)
    {
        if (txtNumberMostafeed.Text.Trim() != string.Empty)
            FGetByBill();
        else
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد المستفيد ... ";
            return;
        }
    }

    protected void DLCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            IDSelectDoner.Visible = false; DLCategory.Enabled = true; DLProduct.Enabled = true;
            if (DLProduct.SelectedValue != string.Empty)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = " - مجموع الشحنات لمنتج " + DLProduct.SelectedItem.ToString() + " ( " + FGetSumationAll().ToString() + " منتج  ), ";
                WSM_Repostry_In_Kind_Donation_Details_.FGet_Price("GetSumForProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt64(DLProduct.SelectedValue), DL_Price, new Guid(ddlYears.SelectedValue));
            }
        }
        catch (Exception)
        {
            return;
        }
    }

}