using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
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

public partial class Shaerd_ERP_FMS_Operating_Expenses_PageAdd : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_Bank_.FGetDropList("WithNull", "Ar", DL_Bank);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Company_.FCRM_Company_ManageView(DLCompany);
            DLSupportType.Focus();
            txtDateAdd.Text = ClassSaddam.FGetDateTo();
            pnlMostafeed.Visible = true;
            pnlAlDaam.Visible = false;
            ClassQuaem.FGetSupportType(1, DLSupportType);
            pnlStarView.Visible = true;
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlSondoq);
            if (Request.QueryString["ID"] != null)
                FGetData();
            else if (Request.QueryString["IDYears"] != null)
            {
                ddlYears.SelectedValue = Request.QueryString["IDYears"];
                txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
                DLSupportType.SelectedValue = Request.QueryString["IDP"];
                txtNumberOrder.Enabled = true; DLCompany.Enabled = true;
                pnlAlDaam.Visible = true;
                txtThe_Mony.Focus();
                pnlStarView.Visible = false;
                FGetLastBill();
            }
        }
    }

    private void FGetData()
    {
        try
        {
            WSM_Model_Operating_Expenses_ MEOE = new WSM_Model_Operating_Expenses_();
            MEOE.IDCheck = "GetByIDUniq";
            MEOE.Top = 1;
            MEOE.ID_Item = new Guid(Request.QueryString["ID"]);
            MEOE.ID_FinancialYear = Guid.Empty;
            MEOE.ID_Donor = Guid.Empty;
            MEOE.ID_Project = 0;
            MEOE.Start_Date = string.Empty;
            MEOE.End_Date = string.Empty;
            MEOE.DataCheck = string.Empty;
            MEOE.DataCheck2 = string.Empty;
            MEOE.DataCheck3 = string.Empty;
            MEOE.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_Operating_Expenses_ WROE = new WSM_Repostry_Operating_Expenses_();
            dt = WROE.BWSM_Operating_Expenses_Manage(MEOE);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                DLCompany.SelectedValue = dt.Rows[0]["_ID_Donor_"].ToString();
                DLSupportType.SelectedValue = dt.Rows[0]["_ID_Project_"].ToString();
                txtNumberOrder.Text = dt.Rows[0]["_ID_Order_"].ToString();
                txtThe_Mony.Text = dt.Rows[0]["_The_Mony_"].ToString();

                RBIsCash_Money.Checked = Convert.ToBoolean(dt.Rows[0]["_IsCash_Money_"]);
                RBIsShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["_IsShayk_Bank_"]);
                RBIsConvert_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["_Transfer_On_Account_"]);
                FCheck();
                txtNumber_Shayk_Bank.Text = dt.Rows[0]["_Number_Shayk_Bank_"].ToString();
                txtDate_Shayk.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy-MM-dd");
                txtFor_Bank.Text = dt.Rows[0]["_For_Bank_"].ToString();
                txtNumber_Account.Text = dt.Rows[0]["_Number_Account_"].ToString();
                txtFor_Bank_Transfer.Text = dt.Rows[0]["_For_Bank_Transfer_"].ToString();
                txtDate_Bank_Transfer.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Bank_Transfer_"]).ToString("yyyy-MM-dd");
                txtDetails.Text = dt.Rows[0]["_What_Get_"].ToString();
                txt_Note.Text = dt.Rows[0]["_Note_Get_"].ToString();
                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah_"].ToString();
                DLAmeenAlSondoq.SelectedValue = dt.Rows[0]["_IDAmmenAlSondoq_"].ToString();
                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["_IDModer_"].ToString();
                DLAccount.SelectedValue = dt.Rows[0]["_Finance_Account_"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Bank_"]))
                {
                    DL_Bank.SelectedValue = dt.Rows[0]["_ID_Bank_"].ToString();
                    Repostry_Account_.FGetDropList(1, "_ID", "_Ar", new Guid(DL_Bank.SelectedValue), DL_Account);
                    DL_Account.SelectedValue = dt.Rows[0]["_ID_Account_"].ToString();
                }
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
                pnlAlDaam.Visible = true;
                pnlStarView.Visible = false;
            }
            else
                Response.Redirect("PageAll.aspx");
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
            FGetLastBill();
            FGetMony();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void DLSupportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            txtNumberOrder.Enabled = true; DLCompany.Enabled = true;
            pnlAlDaam.Visible = true;
            txtThe_Mony.Focus();
            pnlStarView.Visible = false;
            FGetLastBill();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void RBIsCash_Money_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FCheck();
    }

    protected void RBIsShayk_Bank_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FCheck();
    }

    protected void RBIsConvert_Bank_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FCheck();
    }

    private void FCheck()
    {
        if (RBIsCash_Money.Checked)
        {
            NumberShayk.Visible = false;
            Transfer_On_Account.Visible = false;
            FEmptyData();
        }
        else if (RBIsShayk_Bank.Checked)
        {
            NumberShayk.Visible = true;
            Transfer_On_Account.Visible = false;
            txtNumber_Shayk_Bank.Focus();
            FEmptyData();
        }
        else if (RBIsConvert_Bank.Checked)
        {
            NumberShayk.Visible = false;
            Transfer_On_Account.Visible = true;
            txtNumber_Account.Focus();
            FEmptyData();
        }
    }

    private void FEmptyData()
    {
        txtNumber_Shayk_Bank.Text = string.Empty;
        txtDate_Shayk.Text = string.Empty;
        txtFor_Bank.Text = string.Empty;
        txtNumber_Account.Text = string.Empty;
        txtDate_Bank_Transfer.Text = string.Empty;
        txtFor_Bank_Transfer.Text = string.Empty;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (DLAccount.SelectedValue == "البنك")
            {
                if (DL_Bank.SelectedValue == string.Empty || DL_Account.SelectedValue == string.Empty)
                {
                    lblWarning.Text = "يُرجى تحديد الحساب البنكي ,,, ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
            }
            if (txtNumberOrder.Text.Trim() != string.Empty)
            {
                if (RBIsCash_Money.Checked == false && RBIsShayk_Bank.Checked == false && RBIsConvert_Bank.Checked == false)
                {
                    lblWarning.Text = "الرجاء تحديد نوع الدفع ... ";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
                else
                {
                    if (Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                        FWSM_Operating_Expenses_Add(ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
                    else
                    {
                        lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                        IDMessageSuccess.Visible = false;
                        IDMessageWarning.Visible = true;
                        return;
                    }
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

    private void FWSM_Operating_Expenses_Add(string XDate)
    {
        bool XIS_Bank = false;
        Guid XID_Bank = Guid.Empty, XID_Account = Guid.Empty;
        if (DLAccount.SelectedValue == "البنك")
        {
            XIS_Bank = true;
            XID_Bank = new Guid(DL_Bank.SelectedValue);
            XID_Account = new Guid(DL_Account.SelectedValue);
        }
        else if (DLAccount.SelectedValue != "البنك")
        {
            XIS_Bank = false; XID_Bank = Guid.Empty; XID_Account = Guid.Empty;
        }
        WSM_Model_Operating_Expenses_ WMPE = new WSM_Model_Operating_Expenses_();
        if (Request.QueryString["ID"] == null)
        {
            WMPE.IDCheck = "Add";
            WMPE.ID_Item = Guid.NewGuid();
        }
        if (Request.QueryString["ID"] != null)
        {
            WMPE.IDCheck = "Edit";
            WMPE.ID_Item = new Guid(Request.QueryString["ID"]);
        }
        WMPE.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
        WMPE.ID_Donor = new Guid(DLCompany.SelectedValue);
        WMPE.ID_Project = Convert.ToInt32(DLSupportType.SelectedValue);
        WMPE.ID_Order = Convert.ToInt64(txtNumberOrder.Text.Trim());
        WMPE.The_Mony = Convert.ToDecimal(txtThe_Mony.Text.Trim());
        WMPE.IsCash_Money = Convert.ToBoolean(RBIsCash_Money.Checked);
        WMPE.IsShayk_Bank = Convert.ToBoolean(RBIsShayk_Bank.Checked);
        WMPE.Number_Shayk_Bank = txtNumber_Shayk_Bank.Text.Trim();
        WMPE.Date_Get = txtDate_Shayk.Text.Trim();
        WMPE.For_Bank = txtFor_Bank.Text.Trim();
        WMPE.Transfer_On_Account = Convert.ToBoolean(RBIsConvert_Bank.Checked);
        WMPE.Number_Account = txtNumber_Account.Text.Trim();
        WMPE.For_Bank_Transfer = txtFor_Bank_Transfer.Text.Trim();
        WMPE.Date_Bank_Transfer = txtDate_Bank_Transfer.Text.Trim();
        WMPE.What_Get = txtDetails.Text.Trim();
        WMPE.Note_Get = txt_Note.Text.Trim();
        WMPE.IDRaeesMaglisAlEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue);
        WMPE.IsRaeesMaglisAlEdarah = false;
        WMPE.IDRaees_Allow = 0;
        WMPE.IDRaees_Date_Allow = XDate;
        WMPE.IDAmmenAlSondoq = Convert.ToInt32(DLAmeenAlSondoq.SelectedValue);
        WMPE.IsAmmenAlSondoq = false;
        WMPE.IDAmmen_Allow = 0;
        WMPE.IDAmmen_Date_Allow = XDate;
        WMPE.IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
        WMPE.IsModer = false;
        WMPE.IDModer_Allow = 0;
        WMPE.IDModer_Date_Allow = XDate;
        WMPE.Finance_Account = DLAccount.SelectedValue;
        WMPE.Is_Bank = XIS_Bank;
        WMPE.ID_Bank = XID_Bank;
        WMPE.ID_Account = XID_Account;
        WMPE.CreatedBy = Test_Saddam.FGetIDUsiq();
        WMPE.CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss");
        if (Request.QueryString["ID"] == null)
            WMPE.ModifiedBy = 0;
        if (Request.QueryString["ID"] != null)
            WMPE.ModifiedBy = Test_Saddam.FGetIDUsiq();
        WMPE.ModifiedDate = XDate;
        WMPE.DeleteBy = 0;
        WMPE.DeleteDate = XDate;
        WMPE.IsActive = true;
        WSM_Repostry_Operating_Expenses_ WRPE = new WSM_Repostry_Operating_Expenses_();
        string Xresult = WRPE.FWSM_Operating_Expenses_Add(WMPE);
        if (Xresult == "IsExistsNumberAdd" || Xresult == "IsExistsNumberEdit")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة رقم الفاتورة سابقاً ... ";
            txtNumberOrder.Focus();
            return;
        }
        if (Xresult == "IsExistsAdd" || Xresult == "IsExistsEdit")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
            txtNumberOrder.Focus();
            return;
        }
        else if (Xresult == "IsSuccessAdd" || Xresult == "IsSuccessEdit")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
            if (Request.QueryString["ID"] != null)
                FGetData();
            else
                FGetLastBill();
        }
    }

    private void FGetLastBill()
    {
        txtNumberOrder.Text = WSM_Repostry_Operating_Expenses_.FGetLastRecord(new Guid(ddlYears.SelectedValue), Convert.ToInt32(DLSupportType.SelectedValue)).ToString();
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void DLAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetMony();
    }

    private void FGetMony()
    {
        ClassSaddam.FGetMony(lblReceipt, lblCashing, DLAccount, ddlYears, HFSumReceipt,
             lblSumReceipt, HFSumCashing, lblSumCashing, lblSumTotal, lblSumWordReceipt, lblSumWordCashing);
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "Bank")
        {
            if (DLAccount.SelectedValue == "البنك")
                XResult = "display:block;";
            else
                XResult = "display:none;";
        }
        return XResult;
    }

    protected void DLAccount_Load(object sender, EventArgs e)
    {
        DLAccount.Attributes["onchange"] = "Validate();";
    }

    protected void DL_Bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Repostry_Account_.FGetDropList(1, "_ID", "_Ar", new Guid(DL_Bank.SelectedValue), DL_Account);
        }
        catch (Exception)
        {

        }
    }

}