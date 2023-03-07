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

public partial class Cpanel_ERP_EOS_In_Kind_Donation_PageMatterOfExchangeGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A106");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FCheckYears();
            pnlStar.Visible = true;
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            ClassQuaem.FGetSupportType(1, "'1','2','3'", DLSupportType);
            FGetAmeenAlmostodaa();
            txt_Add.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
            pnlMostafeed.Visible = true;
            pnlStar.Visible = true;
            pnlAlDaam.Visible = false;
            FGetCategoryShop();
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
        Response.Redirect("PageMatterOfExchangeGroup.aspx");
    }

    protected void DLSupportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            DLCompany.Enabled = true;
            txtNumberBill.Enabled = true;
            FGetLastBill();
            pnlStar.Visible = false;
            pnlAlDaam.Visible = true;
            FGetMostafeedAll("All");
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
                lblMessageWarning.Text = " - مجموع الشحنات لمنتج " + DLProduct.SelectedItem.Text + " ( " + FGetSumationAll().ToString() + " منتج  ), ";
            else
                lblMessageWarning.Text = "لا يتم حساب الكمية في السنين السابقة ,,,";
            WSM_Repostry_In_Kind_Donation_Details_.FGet_Price("GetSumForProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt64(DLProduct.SelectedValue), DL_Price, new Guid(ddlYears.SelectedValue));
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "الكمية المتبقية لمنتج " + DLProduct.SelectedItem.Text + " " + "0";
        }
    }

    int Cou = 0;
    decimal sum = 0;
    private Int64 FGetSumationAll()
    {
        Int64 XResult = 0;
        Int64 Getsum = 0, Setsum = 0;
        Setsum = WSM_Repostry_In_Kind_Donation_Details_.FGetCountProduct("GetCountProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt32(DLProduct.SelectedValue), string.Empty, new Guid(ddlYears.SelectedValue));
        Getsum = WSM_Repostry_Exchange_Order_Details_.FGetCountProduct("GetCountProductByDoner", new Guid(DLCompany.SelectedValue), Convert.ToInt32(DLProduct.SelectedValue), string.Empty, new Guid(ddlYears.SelectedValue));
        XResult = Setsum - Getsum;
        HFCountProduct.Value = XResult.ToString();
        return XResult;
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
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
            FAddSearchToGridView();
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
            int XIDGuest = 0;
            Int64 XSumation = FGetSumationByProject();
            //Int64 XSumation = Convert.ToInt64(HFCountProduct.Value);
            if (Convert.ToInt64(txtCountProduct.Text.Trim()) <= Convert.ToInt64(XSumation))
            {
                if (Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                    FWSM_In_Kind_Donation_Details_Add(XIDGuest);
                else
                {
                    lblMessageWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
            }
            else if (Convert.ToInt64(txtCountProduct.Text.Trim()) > Convert.ToInt64(XSumation))
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "الكمية المتبقية " + XSumation.ToString() + " لقد طلبت كمية أكثر من التي بالمستودع";
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

    private void FWSM_In_Kind_Donation_Details_Add(int XIDGuest)
    {
        string XPrice = string.Empty, XCount_Partition = string.Empty;
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
            ID_MosTafeed = XIDGuest,
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
        }
        else if (Xresult == "IsSuccessAdd")
        {
            //FGetLastBill();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblMessage.Text = "تم إضافة البيانات بنجاح ... ";
            txtNumberBill.Text = (Convert.ToInt32(txtNumberBill.Text.Trim()) + 1).ToString();
        }
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txt_Add.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
            {
                FAddSearchToGridView();
                int XIDBaheth = 0;
                foreach (GridViewRow row in GVBeneficiaryAll.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        Label BillID = (Label)row.FindControl("lblID");
                        XIDBaheth = Convert.ToInt32(ClassMosTafeed.FGetIDBaheth(Convert.ToInt32(BillID.Text)));
                        if (ClassSaddam.XCheckYear(ddlYears.SelectedItem.Text))
                        {
                            Int64 XSumation = FGetSumationByProject();
                            if (Convert.ToInt64(txtCountProduct.Text.Trim()) <= Convert.ToInt64(XSumation))
                                FWSM_Exchange_Order_Bill_Add(Convert.ToInt32(BillID.Text), XIDBaheth);
                            else if (Convert.ToInt64(txtCountProduct.Text.Trim()) > Convert.ToInt64(XSumation))
                            {
                                IDMessageSuccess.Visible = false;
                                IDMessageWarning.Visible = true;
                                lblMessageWarning.Text = XSumation.ToString() + " كمية الشحنات التي بالمستودع إنتهت";
                                return;
                            }
                        }
                        else
                            FWSM_Exchange_Order_Bill_Add(Convert.ToInt32(BillID.Text), XIDBaheth);
                    }
                }
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
            lblMessageWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FWSM_Exchange_Order_Bill_Add(int XIDGuest, int XIDBaheth)
    {
        HFXID.Value = Guid.NewGuid().ToString();
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XCountQariah = 0, XCountFamilies = 0, XCount_Cart = 0, ID_Qariah = ClassMosTafeed.FGetMosTafeedIDQariah(XIDGuest);
        if (Request.QueryString["id"] == null)
        {
            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_()
            {
                IDCheck = "Add",
                ID_Item = new Guid(HFXID.Value),
                ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                ID_Donor = new Guid(DLCompany.SelectedValue),
                bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
                ID_MosTafeed = XIDGuest,
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
                ID_Delivery = XIDBaheth,
                ID_Delivery_Allow = 0,
                The_Purpose = XDate,
                Is_Cart = true,
                Is_Device = false,
                Is_Tathith = false,
                Is_Talef = false,
                Count_Cart = XCount_Cart,
                Count_Families = XCountFamilies,
                Count_Qariah = XCountQariah,
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
            string Xresult = REOB.FWSM_Exchange_Order_Bill_Add(MEOB);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblMessageWarning.Text = "لا يمكن تكرار رقم الفاتورة ... ";
            }
            else if (Xresult == "IsExistsAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblMessageWarning.Text = "تم إضافة هذه الفاتورة مسبقاً , قم يتغير رقم الفاتورة ... ";
            }
            else if (Xresult == "IsSuccessAdd")
                FWSM_In_Kind_Donation_Details_Add(XIDGuest);
        }
    }

    private void FGetMostafeedAll(string XFilter)
    {
        //ClassMosTafeed CMF = new ClassMosTafeed();
        //CMF._NameMostafeed = string.Empty;
        //CMF._IsDelete = false;
        //DataTable dt = new DataTable();
        //dt = CMF.BArnRasAlEstemarahGetAll();

        DataTable dt = new DataTable();
        if (XFilter == "All")
            dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where ([NumberMostafeed] <> @0) And ([IsDelete] = @1) Order by [NumberMostafeed] ", "504", "0");
        else if (XFilter == "Daeem")
            dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where ([NumberMostafeed] <> @0) And ([TypeMostafeed] = @1) And ([IsDelete] = @2) Order by [NumberMostafeed]  ", "504", "دائم", "0");
        else if (XFilter == "Mostabaad")
            dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where ([NumberMostafeed] <> @0) And ([TypeMostafeed] = @1) And ([IsDelete] = @2) Order by [NumberMostafeed]  ", "504", "مستبعد", "0");
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
    }

    protected void GVBeneficiaryAll_DataBound(object sender, EventArgs e)
    {
        
    }

    private void FAddSearchToGridView()
    {
        //GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
        //for (int i = 0; i < GVBeneficiaryAll.Columns.Count; i++)
        //{
        //    TableHeaderCell cell = new TableHeaderCell();
        //    TextBox txtSearch = new TextBox();
        //    txtSearch.Width = GVBeneficiaryAll.Width;
        //    txtSearch.ForeColor = System.Drawing.Color.Black;
        //    txtSearch.Attributes["placeholder"] = "بحث عن:" + GVBeneficiaryAll.Columns[i].HeaderText; //"إبحث هنا ... ";//
        //    txtSearch.CssClass = "search_textbox";
        //    cell.Controls.Add(txtSearch);
        //    row.Controls.Add(cell);
        //}

        //GVBeneficiaryAll.HeaderRow.Parent.Controls.AddAt(1, row);
    }

    protected void FCheckFilter()
    {
        if (RBAll.Checked && RBDaeem.Checked == false && RBMostabeed.Checked == false)
            FGetMostafeedAll("All");
        else if (RBAll.Checked == false && RBDaeem.Checked && RBMostabeed.Checked == false)
            FGetMostafeedAll("Daeem");
        else if (RBAll.Checked == false && RBDaeem.Checked == false && RBMostabeed.Checked)
            FGetMostafeedAll("Mostabaad");
    }

    protected void RBAll_CheckedChanged(object sender, EventArgs e)
    {
        FCheckFilter();
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