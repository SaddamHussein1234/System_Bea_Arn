using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Models;
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_ERP_WSM_PageShipping_PageAdd : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A120");
            HFXID.Value = Guid.NewGuid().ToString();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            ClassInitiatives.FGetInitiatives(DLInitiatives);
            DLInitiatives.SelectedValue = "1";
            FGetCategoryShop();
            ClassQuaem.FGetSupportType(1, DL_Project);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            WSM_Repostry_Shipment_Type_.FCRM_Company_Type_Manage(DLType_Shipment);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);
            FGetLastBill();
            FGetStoragePlaces();
            if (Request.QueryString["ID"] != null)
                FGetData();

            Repostry_Company_Type_.FCRM_Company_Type_Manage(ddlCompanyType);
            Repostry_Country_.FErp_Country_Manage(ddlCountry);
            ddlCountry.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            System.Threading.Thread.Sleep(100);
        }
    }

    private void FGetStoragePlaces()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[StoragePlaces] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by StorageName", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLProduct_Storage.Items.Clear();
            DLProduct_Storage.Items.Add("");
            DLProduct_Storage.AppendDataBoundItems = true;
            DLProduct_Storage.DataValueField = "IDItem";
            DLProduct_Storage.DataTextField = "StorageName";
            DLProduct_Storage.DataSource = dt;
            DLProduct_Storage.DataBind();
        }
    }

    private void FGetData()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetByID";
            MIKDB.ID_Item = new Guid(Request.QueryString["ID"]);
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
            {
                //ddlYears.Enabled = false;
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
                FChackImgF();
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
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimgArticle(FUArticle);
        }
        else
            FUpimgArticle(FUArticle);
    }

    string _Image_Icon = string.Empty;
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
                string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgProductStorage/"), XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                _Image_Icon = "ImgSystem/ImgProductStorage/" + XRandom + ".png";
                FWSM_In_Kind_Donation_Details_Add();
            }
        }
        else
        {
            _Image_Icon = "ImgSystem/ImgProductStorage/no-img.jpg";
            FWSM_In_Kind_Donation_Details_Add();
        }
    }

    private void FWSM_In_Kind_Donation_Details_Add()
    {
        string Xresult = string.Empty;
        if (txtProduct_Weight.Text.Trim() == "0" || txtProduct_Weight.Text.Trim() == string.Empty)
            txtProduct_Weight.Text = "1";
        WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_()
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
            Expiry_Date = txtExpiry_Date.Text.Trim(),
            ID_Type_Shipment = new Guid(DLType_Shipment.SelectedValue),
            ID_Product_Storage = new Guid(DLProduct_Storage.SelectedValue),
            Image_Icon = _Image_Icon,
            Is_There_Partition = Convert.ToBoolean(CBIs_There_Partition.Checked),
            Count_Partition = Convert.ToInt32(txtProduct_Weight.Text.Trim()),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" hh:mm:ss")).ToString("yyyy-MM-dd hh:mm:ss"),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
            DeleteBy = 0,
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
            IsActive = true
        };

        WSM_Repostry_In_Kind_Donation_Details_ RICS = new WSM_Repostry_In_Kind_Donation_Details_();
        Xresult = RICS.FWSM_In_Kind_Donation_Details_Add(MIKDD);
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
            DLProduct_Storage.SelectedValue = null;
            txtProduct_Weight.Text = "1";
            FGetByBill();
            txtTitle.Focus();
            if (Request.QueryString["id"] != null)
                FEdit();
        }
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
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
            lblWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
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
                WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_()
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
                    Type_Bill = "WSM",
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" hh:mm:ss")).ToString("yyyy-MM-dd hh:mm:ss"),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
                    DeleteBy = 0,
                    DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
                    IsActive = true
                };
                WSM_Repostry_In_Kind_Donation_Bill_ RIKKB = new WSM_Repostry_In_Kind_Donation_Bill_();
                string Xresult = RIKKB.FWSM_In_Kind_Donation_Bill_Add(MIKDB);
                if (Xresult == "IsExistsNumberAdd")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "لا يمكن تكرار رقم الفاتورة ... ";
                    return;
                }
                else if (Xresult == "IsExistsAdd")
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
                    if (Attach_Repostry_SMS_Send_.AllSendSystemWSM())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة شحنة للمستودع" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                    Response.Redirect("PageView.aspx?ID=" + txtNumberBill.Text.Trim() + "&IDUniq=" + MIKDB.ID_FinancialYear + "");
                }
            }
            else if (Request.QueryString["id"] != null)
                FEdit();
        }
        else
        {
            lblWarning.Text = "لم يتم إضافة الأصناف بعد";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
        }
    }

    private void FEdit()
    {
        WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_()
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
            Type_Bill = string.Empty,
            CreatedBy = 0,
            CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" hh:mm:ss")).ToString("yyyy-MM-dd hh:mm:ss"),
            ModifiedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
            DeleteBy = 0,
            DeleteDate = string.Empty,
            IsActive = true
        };
        WSM_Repostry_In_Kind_Donation_Bill_ RIKKB = new WSM_Repostry_In_Kind_Donation_Bill_();
        string Xresult = RIKKB.FWSM_In_Kind_Donation_Bill_Add(MIKDB);
        if (Xresult == "IsExistsNumberEdit")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لا يمكن تكرار رقم الفاتورة ... ";
            return;
        }
        else if (Xresult == "IsExistsEdit")
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
            lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
            if (Attach_Repostry_SMS_Send_.AllSendSystemWSM())
                Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل شحنة للمستودع" + "\n" + "رقم الفاتورة :" + txtNumberBill.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
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
                    WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_()
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
                        Expiry_Date = string.Empty,
                        ID_Type_Shipment = Guid.Empty,
                        ID_Product_Storage = Guid.Empty,
                        Image_Icon = _Image_Icon,
                        Is_There_Partition = false,
                        Count_Partition = 0,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
                        IsActive = false
                    };
                    WSM_Repostry_In_Kind_Donation_Details_ RICS = new WSM_Repostry_In_Kind_Donation_Details_();
                    Xresult = RICS.FWSM_In_Kind_Donation_Details_Add(MIKDD);
                }
                if (Xresult == "IsSuccessDelete")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم حذف الصنف بنجاح ... ";
                }
            }
            FGetByBill();
            if (Request.QueryString["id"] != null)
                FEdit();
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
            WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_();
            MIKDD.IDCheck = "GetByBillByIDBill";
            MIKDD.ID_Item = new Guid(HFXID.Value);
            MIKDD.bill_Number = 0;
            MIKDD.Start_Date = string.Empty;
            MIKDD.End_Date = string.Empty;
            MIKDD.DataCheck = string.Empty;
            MIKDD.DataCheck2 = string.Empty;
            MIKDD.DataCheck3 = string.Empty;
            MIKDD.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_In_Kind_Donation_Details_ RIKDD = new WSM_Repostry_In_Kind_Donation_Details_();
            dt = RIKDD.BWSM_In_Kind_Donation_Details_Manage(MIKDD);
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

    protected void LBGetBill_Click(object sender, EventArgs e)
    {
        FGetByBill();
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
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
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

}