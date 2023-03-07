using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageZakat_PageDeedDonationInKind : System.Web.UI.Page
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
            bool A137, A140, A141, A142;
            A137 = Convert.ToBoolean(dtViewProfil.Rows[0]["A137"]);
            A141 = Convert.ToBoolean(dtViewProfil.Rows[0]["A141"]);
            A142 = Convert.ToBoolean(dtViewProfil.Rows[0]["A142"]);
            if (A137 == false)
                Response.Redirect("PageNotAccess.aspx");

            PnlAllow.Visible = true;
            //A142 = Convert.ToBoolean(dtViewProfil.Rows[0]["A142"]);
            A140 = Convert.ToBoolean(dtViewProfil.Rows[0]["A140"]);
            
            if (A142 == true) { IDAmeenAlMostodaa.Visible = true; }
            if (A140 == true) { IDAmeenAlsondoq.Visible = true; }
            if (A141 == true) { IDModer.Visible = true; }

            if (A137 == false && A141 == false)
                PnlAllow.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            lblSend.ForeColor = Color.Red;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtDateAdd.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetAmeenAlMostwdaa(DLIDStorekeeper);

            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);

            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);

            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            ClassCategory_Zakat.FGetCategory_Zakat(DLCategory);
            ClassQuaem.FGetProject(DL_Project);
            txtFromDonor.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_();
            MWZB.IDCheck = "GetByID";
            MWZB.IDUniq = new Guid(Request.QueryString["ID"]);
            MWZB.ID_FinancialYear = Guid.Empty;
            MWZB.bill_Number = 0;
            MWZB.ID_Project = 0;
            MWZB.Start_Date = string.Empty;
            MWZB.End_Date = string.Empty;
            MWZB.DateCheck = string.Empty;
            MWZB.DateCheck2 = string.Empty;
            MWZB.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
            dt = RWZB.BArn_Warehouse_Zakat_Bill_Manage(MWZB);

            if (dt.Rows.Count > 0)
            {
                ddlYears.Enabled = false; txtNumberBill.Enabled = false; DL_Project.Enabled = false;

                ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                txtNumberBill.Text = dt.Rows[0]["_bill_Number_"].ToString();
                txtFromDonor.Text = dt.Rows[0]["_Name_Donor_"].ToString();
                txtNumber.Text = dt.Rows[0]["_Phone_Donor_"].ToString();
                DL_Project.SelectedValue = dt.Rows[0]["_ID_Project_"].ToString();
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
                txt_Note.Text = dt.Rows[0]["_Note_Bill"].ToString();
                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["_IDModer_"].ToString();
                CBModer.Checked = Convert.ToBoolean(dt.Rows[0]["_IsModer_"]);
                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah_"].ToString();
                CBRaeesAlmaglis.Checked = Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah_"]);
                DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["_IDAmmenAlSondoq_"].ToString();
                CBAmeenAlsondoq.Checked = Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq_"]);
                DLIDStorekeeper.SelectedValue = dt.Rows[0]["_IDStorekeeper_"].ToString();
                CBAmeenAlMostodaa.Checked = Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper_"]);
                FGetByBill();
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

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;
    protected void GVProductShopWarehouseByID_RowDataBound(object sender, GridViewRowEventArgs e)
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
    
    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageDeedDonationInKind.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            System.Threading.Thread.Sleep(100);
            if (Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FArn_Warehouse_Zakat_Add();
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
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FArn_Warehouse_Zakat_Add()
    {
        Model_Warehouse_Zakat_ MWZ = new Model_Warehouse_Zakat_()
        {
            IDCheck = "Add",
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
            ID_Category = Convert.ToInt32(DLCategory.SelectedValue),
            CountProduct = Convert.ToInt32(txtCountProduct.Text.Trim()),
            One_Price = Convert.ToDecimal(txtPrice.Text.Trim()),
            Total_Price = Convert.ToDecimal(Convert.ToDecimal(txtCountProduct.Text.Trim()) * Convert.ToDecimal(txtPrice.Text.Trim())),
            ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
            DeleteBy = 0,
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsDelete = false
        };

        Repostry_Warehouse_Zakat_ RWZ = new Repostry_Warehouse_Zakat_();
        string Xresult = RWZ.FArn_Warehouse_Zakat_Add(MWZ);
        if (Xresult == "IsExistsAdd")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "تم إضافة البيانات سابقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccessAdd")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblMessage.Text = "تم إضافة البيانات بنجاح ... ";
            DLCategory.SelectedValue = null;
            txtCountProduct.Text = string.Empty;
            txtPrice.Text = string.Empty;
            FGetByBill();
            if (Request.QueryString["ID"] != null)
                FEdit();
        }
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FArn_Warehouse_Zakat_Bill_Add();
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

    private void FArn_Warehouse_Zakat_Bill_Add()
    {
        if (GVDeedDonationInKind.Rows.Count > 0)
        {
            if (Request.QueryString["id"] == null)
            {
                string Xresult = string.Empty;
                int XID = Test_Saddam.FGetIDUsiq();
                string XDate = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
                Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_()
                {
                    IDCheck = "Add",
                    IDUniq = Guid.NewGuid(),
                    ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                    bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
                    Name_Donor = txtFromDonor.Text.Trim(),
                    Phone_Donor = txtNumber.Text.Trim(),
                    ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
                    Note_Bill = txt_Note.Text.Trim(),
                    IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
                    IsRaeesMaglisAlEdarah = Convert.ToBoolean(CBRaeesAlmaglis.Checked),
                    IDRaees_Allow = XID,
                    IDRaees_Date_Allow = XDate,
                    IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
                    IsAmmenAlSondoq = Convert.ToBoolean(CBAmeenAlsondoq.Checked),
                    IDAmmen_Allow = XID,
                    IDAmmen_Date_Allow = XDate,
                    IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
                    IsModer = Convert.ToBoolean(CBModer.Checked),
                    IDModer_Allow = XID,
                    IDModer_Date_Allow = XDate,
                    IDStorekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
                    IsStorekeeper = Convert.ToBoolean(CBAmeenAlMostodaa.Checked),
                    IDStorekeeper_Allow = XID,
                    IDStorekeeper_Date_Allow = XDate,
                    CreatedBy = XID,
                    CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                    ModifiedBy = 0,
                    ModifiedDate = XDate,
                    DeleteBy = 0,
                    DeleteDate = XDate,
                    IsDelete = false
                };
                Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
                Xresult = RWZB.FArn_Warehouse_Zakat_Bill_Add(MWZB);
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
                    lblMessage.Text = "تم تحديث البيانات بنجاح ... ";
                    if (DLSend.SelectedValue == "Yes")
                    {
                        string XShort_URL = ClassRandomURL.GetURL();
                        string XLong_URL = ClassSetting.FGetNameServer() + "/ar/Zakat/PageView.aspx?IDYear=" + ddlYears.SelectedValue + "&ID=" + txtNumberBill.Text.Trim() + "&IDP=" + DL_Project.SelectedValue;
                        string XResult = Attach_Repostry_Short_URL_.FShort_URL_Add("Add", XShort_URL, XLong_URL, "Zakat", "Bill", XID, 0, XDate);
                        if (XResult == "IsSuccessAdd")
                            Attach_Repostry_SMS_Send_.FAddSMSMessage(txtNumber.Text.Trim(), DL_Project.SelectedItem.Text + "\n" + ClassSetting.FGetNameServer() + "/Check.aspx?ID=" + XShort_URL, "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                    }
                    string XURL = "PageDeedDonationInKindDetails.aspx?IDYear=" + MWZB.ID_FinancialYear.ToString() + "&ID=" + txtNumberBill.Text.Trim() + "&IDP=" + DL_Project.SelectedValue + "&IDUniq=" + MWZB.IDUniq.ToString();
                    Response.Redirect(XURL);
                }
            }
            else if (Request.QueryString["id"] != null)
                FEdit();

        }
        else
        {
            lblMessageWarning.Text = "لم يتم إضافة الأصناف بعد";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FEdit()
    {
        string Xresult = string.Empty;
        int XID = Test_Saddam.FGetIDUsiq();
        string XDate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
        Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_()
        {
            IDCheck = "Edit",
            IDUniq = new Guid(Request.QueryString["ID"]),
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim()),
            Name_Donor = txtFromDonor.Text.Trim(),
            Phone_Donor = txtNumber.Text.Trim(),
            ID_Project = Convert.ToInt32(DL_Project.SelectedValue),
            Note_Bill = txt_Note.Text.Trim(),
            IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            IsRaeesMaglisAlEdarah = false,
            IDRaees_Allow = XID,
            IDRaees_Date_Allow = XDate,
            IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue),
            IsAmmenAlSondoq = Convert.ToBoolean(CBAmeenAlsondoq.Checked),
            IDAmmen_Allow = XID,
            IDAmmen_Date_Allow = XDate,
            IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
            IsModer = Convert.ToBoolean(CBModer.Checked),
            IDModer_Allow = XID,
            IDModer_Date_Allow = XDate,
            IDStorekeeper = Convert.ToInt32(DLIDStorekeeper.SelectedValue),
            IsStorekeeper = Convert.ToBoolean(CBAmeenAlMostodaa.Checked),
            IDStorekeeper_Allow = XID,
            IDStorekeeper_Date_Allow = XDate,
            CreatedBy = 0,
            CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = XID,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            DeleteBy = 0,
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsDelete = false
        };
        Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
        Xresult = RWZB.FArn_Warehouse_Zakat_Bill_Add(MWZB);
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
            lblMessage.Text = "تم تحديث البيانات بنجاح ... ";
            if (DLSend.SelectedValue == "Yes")
            {
                string XShort_URL = ClassRandomURL.GetURL();
                string XLong_URL = ClassSetting.FGetNameServer() + "/ar/Zakat/PageView.aspx?IDYear=" + ddlYears.SelectedValue + "&ID=" + txtNumberBill.Text.Trim() + "&IDP=" + DL_Project.SelectedValue;
                string XResult = Attach_Repostry_Short_URL_.FShort_URL_Add("Add", XShort_URL, XLong_URL, "Zakat", "Bill", XID, 0, XDate);
                if (XResult == "IsSuccessAdd")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(txtNumber.Text.Trim(), DL_Project.SelectedItem.Text + "\n" + ClassSetting.FGetNameServer() + "/Check.aspx?ID=" + XShort_URL, "BerArn", "Add", Test_Saddam.FGetIDUsiq());
            }
            FGetData();
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
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            foreach (GridViewRow row in GVDeedDonationInKind.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVDeedDonationInKind.DataKeys[row.RowIndex].Value);
                    Model_Warehouse_Zakat_ MWZ = new Model_Warehouse_Zakat_()
                    {
                        IDCheck = "Delete",
                        ID_FinancialYear = Guid.Empty,
                        bill_Number = Convert.ToInt64(Comp_ID),
                        ID_Category = 0,
                        CountProduct = 0,
                        One_Price = 0,
                        Total_Price = 0,
                        ID_Project = 0,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsDelete = true
                    };

                    Repostry_Warehouse_Zakat_ RWZ = new Repostry_Warehouse_Zakat_();
                    string Xresult = RWZ.FArn_Warehouse_Zakat_Add(MWZ);
                    if (Xresult == "IsSuccessDelete")
                    {
                        lblMessage.Text = "تم حذف الصنف بنجاح ... ";
                        IDMessageSuccess.Visible = false;
                        IDMessageWarning.Visible = false;
                        FGetByBill();
                        if (Request.QueryString["id"] != null)
                            FEdit();
                    }
                }
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

    private void FGetByBill()
    {
        try
        {
            Model_Warehouse_Zakat_ MWZ = new Model_Warehouse_Zakat_();
            MWZ.IDCheck = "GetByBill";
            MWZ.ID_Item = 0;
            MWZ.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MWZ.bill_Number = Convert.ToInt32(txtNumberBill.Text.Trim());
            MWZ.ID_Project = Convert.ToInt32(DL_Project.SelectedValue);
            MWZ.Start_Date = string.Empty;
            MWZ.End_Date = string.Empty;
            MWZ.DateCheck = string.Empty;
            MWZ.IsDelete = false;
            Repostry_Warehouse_Zakat_ RWZ = new Repostry_Warehouse_Zakat_();
            DataTable dt = new DataTable();
            dt = RWZ.BArn_Warehouse_Zakat_Manage(MWZ);
            if (dt.Rows.Count > 0)
            {
                GVDeedDonationInKind.DataSource = dt;
                GVDeedDonationInKind.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                ProductByID.Visible = true;
                txtTitle.Text = "تقاصيل الفاتورة رقم " + MWZ.bill_Number.ToString();
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
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void DL_Project_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Note.Text = DL_Project.SelectedItem.ToString();
        FGetLastBill();
    }

    private void FGetLastBill()
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            txtNumberBill.Text = Repostry_Warehouse_Zakat_Bill_.FGetLastBill(new Guid(ddlYears.SelectedValue), Convert.ToInt32(DL_Project.SelectedValue)).ToString();
        }
        catch (Exception)
        {
            //IDMessageWarning.Visible = true;
            //IDMessageSuccess.Visible = false;
            //lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetLastBill();
        txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
    }

    protected void LBGetBill_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        if (DL_Project.SelectedValue != string.Empty && txtNumberBill.Text.Trim() != string.Empty)
            FGetByBill();
        else
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد المشروع ورقم الفاتورة ... ";
            return;
        }
    }

}