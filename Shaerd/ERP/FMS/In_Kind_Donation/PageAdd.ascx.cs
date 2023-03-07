using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Models;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Models;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_ERP_FMS_In_Kind_Donation_PageAdd : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HFXID.Value = Guid.NewGuid().ToString();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
            FGetCategoryShop();
            ClassQuaem.FGetSupportType(1, DL_Project);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
            FGetLastBill();
            if (Request.QueryString["ID"] != null)
                FGetData();
            Repostry_Company_Type_.FCRM_Company_Type_Manage(ddlCompanyType);
            Repostry_Country_.FErp_Country_Manage(ddlCountry);
            ddlCountry.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";

            WSM_Repostry_Shipment_Type_.FCRM_Company_Type_Manage(DLType_Shipment);

            System.Threading.Thread.Sleep(100);
        }
    }

    private void FGetData()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetByID";
            MIKDB.ID_Item = new Guid(Request.QueryString["ID"]);
            MIKDB.bill_Number = 0;
            MIKDB.ID_Donor = Guid.Empty;
            MIKDB.Start_Date = string.Empty;
            MIKDB.End_Date = string.Empty;
            MIKDB.DateCheck = string.Empty;
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_In_Kind_Donation_Bill_ RIKDB = new Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BOM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
            {
                HFXID.Value = dt.Rows[0]["_ID_Item_"].ToString();
                ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                txtNumberBill.Text = dt.Rows[0]["_bill_Number_"].ToString();
                DLInitiatives.SelectedValue = dt.Rows[0]["_The_Initiative_"].ToString();
                DLCompany.SelectedValue = dt.Rows[0]["_ID_Donor_"].ToString();
                DL_Project.SelectedValue = dt.Rows[0]["_ID_Project_"].ToString();
                txt_Note.Text = dt.Rows[0]["_Note_Bill"].ToString();
                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah_"].ToString();
                DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["_IDAmmenAlSondoq_"].ToString();
                DLIDStorekeeper.SelectedValue = dt.Rows[0]["_IDStorekeeper_"].ToString();
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
                FGetByBill();
            }

        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FGetLastBill()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetLastBill";
            MIKDB.ID_Item = new Guid(ddlYears.SelectedValue);
            MIKDB.bill_Number = 0;
            MIKDB.ID_Donor = Guid.Empty;
            MIKDB.Start_Date = string.Empty;
            MIKDB.End_Date = string.Empty;
            MIKDB.DateCheck = string.Empty;
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_In_Kind_Donation_Bill_ RIKDB = new Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BOM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
                txtNumberBill.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["bill_Number"]) + 1);
            else
                txtNumberBill.Text = ClassSaddam.FGetNumberBillStart().ToString();

        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FOM_In_Kind_Donation_Details_Add();
            else
            {
                lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FOM_In_Kind_Donation_Details_Add()
    {
        string Xresult = string.Empty;
        if (txtProduct_Weight.Text.Trim() == "0" || txtProduct_Weight.Text.Trim() == string.Empty)
            txtProduct_Weight.Text = "1";
        Model_In_Kind_Donation_Details_ MIKDD = new Model_In_Kind_Donation_Details_()
        {
            IDCheck = "Add",
            ID_Item = Guid.NewGuid(),
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            ID_Bill = new Guid(HFXID.Value),
            bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
            ID_Category = Convert.ToInt32(DLCategory.SelectedValue),
            ID_Project = Convert.ToInt32(DLProduct.SelectedValue),
            CountProduct = Convert.ToInt32(txtCountProduct.Text.Trim()),
            One_Price = Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim()),
            Total_Price = Convert.ToDecimal(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim())),
            Is_There_Partition = Convert.ToBoolean(CBIs_There_Partition.Checked),
            Count_Partition = Convert.ToInt32(txtProduct_Weight.Text.Trim()),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            DeleteBy = 0,
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true
        };

        Repostry_In_Kind_Donation_Details_ RICS = new Repostry_In_Kind_Donation_Details_();
        Xresult = RICS.FOM_In_Kind_Donation_Details_Add(MIKDD);
        if (Xresult == "IsExistsAdd")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccessAdd")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            DLProduct.SelectedValue = null;
            txtCountProduct.Text = string.Empty;
            txtPriceOfTheGrain.Text = string.Empty;
            txtTotalPrice.Text = string.Empty;
            FGetByBill();
        }
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (txtNumberBill.Text.Trim() != string.Empty)
            {
                if (Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                    FOM_In_Kind_Donation_Bill_Add();
                else
                {
                    lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
            }
            else
            {
                lblWarning.Text = "يُرجى إدخال رقم الفاتورة ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            lblWarning.Text = "حدث خطأ غير متوقع , يبدوا أنة تم الإضافة سابقاً";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FOM_In_Kind_Donation_Bill_Add()
    {
        if (GVDeedDonationInKind.Rows.Count > 0)
        {
            if (Request.QueryString["id"] == null)
            {
                Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_()
                {
                    IDCheck = "Add",
                    ID_Item = new Guid(HFXID.Value),
                    ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                    bill_Number = Convert.ToInt64(txtNumberBill.Text.Trim()),
                    The_Initiative = Convert.ToInt32(DLInitiatives.SelectedValue),
                    ID_Donor = new Guid(DLCompany.SelectedValue),
                    ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
                    Note_Bill = txt_Note.Text.Trim(),
                    IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                    IsRaeesMaglisAlEdarah = Convert.ToBoolean(CBRaeesAlmaglis.Checked),
                    IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                    IsAmmenAlSondoq = Convert.ToBoolean(CBAmeenAlsondoq.Checked),
                    IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                    IsModer = Convert.ToBoolean(CBModer.Checked),
                    IDStorekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
                    IsStorekeeper = Convert.ToBoolean(CBAmeenAlMostodaa.Checked),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    DeleteBy = 0,
                    DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };
                Repostry_In_Kind_Donation_Bill_ RIKKB = new Repostry_In_Kind_Donation_Bill_();
                string Xresult = RIKKB.FOM_In_Kind_Donation_Bill_Add(MIKDB);
                if (Xresult == "IsExistsAdd")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "تم إضافة هذه الفاتورة مسبقاً , قم يتغير رقم الفاتورة ... ";
                    return;
                }
                else if (Xresult == "IsSuccessAdd")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                    if (DLSend.SelectedValue == "No")
                    {
                        if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة سند تبرع عيني" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                        Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + MIKDB.ID_FinancialYear + "");
                    }
                    if (DLSend.SelectedValue == "Yes")
                        FAddToWSM(new Guid(HFXID.Value));
                }
            }
            else if (Request.QueryString["id"] != null)
            {
                Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_()
                {
                    IDCheck = "Edit",
                    ID_Item = new Guid(Request.QueryString["ID"]),
                    ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                    bill_Number = Convert.ToInt64(txtNumberBill.Text.Trim()),
                    The_Initiative = Convert.ToInt32(DLInitiatives.SelectedValue),
                    ID_Donor = new Guid(DLCompany.SelectedValue),
                    ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
                    Note_Bill = txt_Note.Text.Trim(),
                    IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                    IsRaeesMaglisAlEdarah = Convert.ToBoolean(CBRaeesAlmaglis.Checked),
                    IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                    IsAmmenAlSondoq = Convert.ToBoolean(CBAmeenAlsondoq.Checked),
                    IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                    IsModer = Convert.ToBoolean(CBModer.Checked),
                    IDStorekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
                    IsStorekeeper = Convert.ToBoolean(CBAmeenAlMostodaa.Checked),
                    CreatedBy = 0,
                    CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    DeleteBy = 0,
                    DeleteDate = string.Empty,
                    IsActive = true
                };
                Repostry_In_Kind_Donation_Bill_ RIKKB = new Repostry_In_Kind_Donation_Bill_();
                string Xresult = RIKKB.FOM_In_Kind_Donation_Bill_Add(MIKDB);
                if (Xresult == "IsExistsEdit")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "تم إضافة هذه الفاتورة مسبقاً , قم يتغير رقم الفاتورة ... ";
                    return;
                }
                else if (Xresult == "IsSuccessEdit")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                    if (DLSend.SelectedValue == "No")
                    {
                        if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل سند تبرع عيني" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
                        Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + MIKDB.ID_FinancialYear + "");
                    }
                    if (DLSend.SelectedValue == "Yes")
                        FAddToWSM(new Guid(Request.QueryString["ID"]));
                }
                lbmsg.Text = Xresult;
            }
        }
        else
        {
            lblWarning.Text = "لم يتم إضافة الأصناف بعد";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
        }
    }

    protected void LBRefresh2_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetByBill();
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVDeedDonationInKind.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVDeedDonationInKind.DataKeys[row.RowIndex].Value);
                    Model_In_Kind_Donation_Details_ MIKDD = new Model_In_Kind_Donation_Details_()
                    {
                        IDCheck = "Delete",
                        ID_Item = new Guid(Comp_ID),
                        ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                        ID_Bill = Guid.Empty,
                        bill_Number = 0,
                        ID_Category = 0,
                        ID_Project = 0,
                        CountProduct = 0,
                        One_Price = 0,
                        Total_Price = 0,
                        Is_There_Partition = false,
                        Count_Partition = 0,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    Repostry_In_Kind_Donation_Details_ RICS = new Repostry_In_Kind_Donation_Details_();
                    Xresult = RICS.FOM_In_Kind_Donation_Details_Add(MIKDD);
                }
            }
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم حذف الصنف بنجاح ... ";
                if (Request.QueryString["id"] != null)
                    FDelete_Details(new Guid(Request.QueryString["id"]));
                FGetByBill();
            }
        }
        catch (Exception)
        {
            lblWarning.Text = "حدث خطأ غير متوقع , حاول لاحقاً ... ";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;
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

    protected void txtPriceOfTheGrain_TextChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (txtCountProduct.Text != string.Empty)
            {
                HFTotalPrice.Value = Convert.ToString(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(txtPriceOfTheGrain.Text.Trim()));
                txtTotalPrice.Text = HFTotalPrice.Value;
                txtTotalPrice.Focus();
            }
            else
            {
                lblCheckCountProduct.Text = "الكمية *";
            }
        }
        catch (Exception)
        {
            HFTotalPrice.Value = "0";
            txtTotalPrice.Text = HFTotalPrice.Value;
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

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberCategory", Convert.ToString(true), Convert.ToString(false));
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", DLCategory.SelectedValue, Convert.ToString(true), Convert.ToString(false));
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

    private void FGetByBill()
    {
        try
        {
            Model_In_Kind_Donation_Details_ MIKDD = new Model_In_Kind_Donation_Details_();
            MIKDD.IDCheck = "GetByBill";
            MIKDD.ID_Item = new Guid(HFXID.Value);
            MIKDD.bill_Number = 0;
            MIKDD.Start_Date = string.Empty;
            MIKDD.End_Date = string.Empty;
            MIKDD.DataCheck = string.Empty;
            MIKDD.DataCheck2 = string.Empty;
            MIKDD.DataCheck3 = string.Empty;
            MIKDD.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_In_Kind_Donation_Details_ RIKDD = new Repostry_In_Kind_Donation_Details_();
            dt = RIKDD.BOM_In_Kind_Donation_Details_Manage(MIKDD);
            if (dt.Rows.Count > 0)
            {
                GVDeedDonationInKind.DataSource = dt;
                GVDeedDonationInKind.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                ProductByID.Visible = true;
                txtTitle.Text = "تقاصيل الفاتورة رقم " + txtNumberBill.Text.Trim();
                lblTotalPrice.Focus();
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
        FGetLastBill();
    }

    protected void LBSave_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            FAdd();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FAdd()
    {
        Guid XID = Guid.NewGuid();
        Model_Company_ MC = new Model_Company_()
        {
            IDCheck = "Add",
            ID_Item = XID,
            Type_Customer = DLType_Customer.SelectedValue,
            Company_Name = txtCompanyName.Text.Trim(),
            Company_Type = new Guid(ddlCompanyType.SelectedValue),
            Registration_No = Repostry_Company_.FCRM_Company_Manage() + 1,
            Address = string.Empty,
            Country = new Guid(ddlCountry.SelectedValue),
            City = string.Empty,
            Website = string.Empty,
            Email_Address = ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com",
            Established_Year = 0,
            Fax = string.Empty,
            Phone_Number1 = txtPhone_Number1.Text.Trim(),
            Mobile_Number1 = string.Empty,
            Phone_Number2 = string.Empty,
            Icon_Img = "ImgSystem/ImgCompany/no-img.jpg",
            Is_Active = true,
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            Is_Delete = false
        };

        Repostry_Company_ RC = new Repostry_Company_();
        string Xresult = RC.FCRM_Company_Add(MC);
        if (Xresult == "IsSuccessAdd")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            DLCompany.SelectedValue = XID.ToString(); ;
        }
    }

    private void FAddToWSM(Guid XID)
    {
        Int64 XIDBill = FGetLastBillSend();
        WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_()
        {
            IDCheck = "Add",
            ID_Item = XID,
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            bill_Number = XIDBill,
            The_Initiative = Convert.ToInt32(DLInitiatives.SelectedValue),
            ID_Donor = new Guid(DLCompany.SelectedValue),
            ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
            Note_Bill = txt_Note.Text.Trim(),
            IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            IsRaeesMaglisAlEdarah = Convert.ToBoolean(CBRaeesAlmaglis.Checked),
            IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
            IsAmmenAlSondoq = Convert.ToBoolean(CBAmeenAlsondoq.Checked),
            IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
            IsModer = Convert.ToBoolean(CBModer.Checked),
            IDStorekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
            IsStorekeeper = Convert.ToBoolean(CBAmeenAlMostodaa.Checked),
            Type_Bill = "OM",
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            DeleteBy = 0,
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true
        };
        WSM_Repostry_In_Kind_Donation_Bill_ RIKKB = new WSM_Repostry_In_Kind_Donation_Bill_();
        string Xresult = RIKKB.FWSM_In_Kind_Donation_Bill_Add(MIKDB);
        if (Xresult == "IsSuccessAdd")
            FAddDetailsToWSM(new Guid(HFXID.Value), XIDBill);
    }

    private void FAddDetailsToWSM(Guid XID, Int64 XIDBill)
    {
        Model_In_Kind_Donation_Details_ MIKDDBill = new Model_In_Kind_Donation_Details_();
        MIKDDBill.IDCheck = "GetByBill";
        MIKDDBill.ID_Item = XID;
        MIKDDBill.bill_Number = 0;
        MIKDDBill.Start_Date = string.Empty;
        MIKDDBill.End_Date = string.Empty;
        MIKDDBill.DataCheck = string.Empty;
        MIKDDBill.DataCheck2 = string.Empty;
        MIKDDBill.DataCheck3 = string.Empty;
        MIKDDBill.IsActive = true;
        DataTable dt = new DataTable();
        Repostry_In_Kind_Donation_Details_ RIKDD = new Repostry_In_Kind_Donation_Details_();
        dt = RIKDD.BOM_In_Kind_Donation_Details_Manage(MIKDDBill);
        if (dt.Rows.Count > 0)
        {
            string Xresult = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_()
                {
                    IDCheck = "Add",
                    ID_Item = Guid.NewGuid(),
                    ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                    ID_Bill = XID,
                    bill_Number = XIDBill,
                    ID_Category = Convert.ToInt32(dt.Rows[i]["_ID_Category_"]),
                    ID_Project = Convert.ToInt32(dt.Rows[i]["_ID_Project_"]),
                    CountProduct = Convert.ToInt32(dt.Rows[i]["_CountProduct"]),
                    One_Price = Convert.ToDecimal(dt.Rows[i]["_One_Price_"]),
                    Total_Price = Convert.ToDecimal(dt.Rows[i]["_Total_Price_"]),
                    Expiry_Date = string.Empty,
                    ID_Type_Shipment = new Guid(DLType_Shipment.SelectedValue),
                    ID_Product_Storage = new Guid("13A553EA-1638-42E9-940B-1827C5A3B8BA"),
                    Image_Icon = "ImgSystem/ImgProductStorage/no-img.jpg",
                    Is_There_Partition = Convert.ToBoolean(dt.Rows[i]["_Is_There_Partition_"]),
                    Count_Partition = Convert.ToInt32(dt.Rows[i]["_Count_Partition_"]),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    DeleteBy = 0,
                    DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                WSM_Repostry_In_Kind_Donation_Details_ RICS = new WSM_Repostry_In_Kind_Donation_Details_();
                Xresult = RICS.FWSM_In_Kind_Donation_Details_Add(MIKDD);
            }
            if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";

                if (XIDBill != 0)
                {
                    if (Request.QueryString["id"] == null)
                    {
                        if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة سند تبرع عيني" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                        Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue + "");
                    }
                    else if (Request.QueryString["id"] != null)
                    {
                        if (Attach_Repostry_SMS_Send_.AllSendSystemOM())
                            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل سند تبرع عيني" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
                        Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue + "");
                    }
                }
            }
        }
    }

    private Int64 FGetLastBillSend()
    {
        Int64 XResult = 0;
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetLastBill";
            MIKDB.ID_Item = new Guid(ddlYears.SelectedValue);
            MIKDB.bill_Number = 0;
            MIKDB.ID_Donor = Guid.Empty;
            MIKDB.Start_Date = string.Empty;
            MIKDB.End_Date = string.Empty;
            MIKDB.DateCheck = string.Empty;
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_In_Kind_Donation_Bill_ RIKDB = new WSM_Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BWSM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
                XResult = Convert.ToInt64(dt.Rows[0]["bill_Number"]) + 1;
            else
                XResult = ClassSaddam.FGetNumberBillStart();

        }
        catch (Exception)
        {

        }
        return XResult;
    }

    protected void LBGetBill_Click(object sender, EventArgs e)
    {
        FGetByBill();
    }

    public string FCheck()
    {
        string XResult = "display:none;";
        if (Convert.ToBoolean(CBIs_There_Partition.Checked))
            XResult = "display:block;";
        else if (Convert.ToBoolean(CBIs_There_Partition.Checked) == false)
            XResult = "display:none;";
        else
            XResult = "display:none;";
        return XResult;
    }

    private void FDelete_Details(Guid XID)
    {
        WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_()
        {
            IDCheck = "DeleteBill",
            ID_Item = Guid.Empty,
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            ID_Bill = XID,
            bill_Number = 0,
            ID_Category = 0,
            ID_Project = 0,
            CountProduct = 0,
            One_Price = 0,
            Total_Price = 0,
            Expiry_Date = string.Empty,
            ID_Type_Shipment = Guid.Empty,
            ID_Product_Storage = Guid.Empty,
            Image_Icon = string.Empty,
            Is_There_Partition = false,
            Count_Partition = 0,
            CreatedBy = 0,
            CreatedDate = string.Empty,
            ModifiedBy = 0,
            ModifiedDate = string.Empty,
            DeleteBy = Test_Saddam.FGetIDUsiq(),
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = false
        };
        WSM_Repostry_In_Kind_Donation_Details_ RICS = new WSM_Repostry_In_Kind_Donation_Details_();
        string XresultDetails = RICS.FWSM_In_Kind_Donation_Details_Add(MIKDD);
        if (XresultDetails == "IsSuccessDeleteBill")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
            FAddDetailsToWSM(XID, 0);
        }
    }

}