using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.GAM;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_ERP_FMS_GeneralAssembly_PageAdd : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    string IDUser = string.Empty, IDUniq = string.Empty;
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
            bool A147, A148, A149;
            A147 = Convert.ToBoolean(dtViewProfil.Rows[0]["A147"]);
            if (A147 == false)
                Response.Redirect("/Cpanel/CHome/PageNotAccess.aspx");
            PnlAllow.Visible = true;
            A148 = Convert.ToBoolean(dtViewProfil.Rows[0]["A148"]);
            A149 = Convert.ToBoolean(dtViewProfil.Rows[0]["A149"]);
            if (A148 == true) { IDAmeenAlsondoq.Visible = true; }
            if (A149 == true) { IDRaeesAlmaglis.Visible = true; }

            if (A148 == false && A149 == false)
                PnlAllow.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "GA")
                CheckAccountAdmin();
            DLNumber_Admin.Focus();
            Repostry_Bank_.FGetDropList("WithNull", "Ar", DL_Bank);
            txtDate_Get.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            FGetLastRecord();
            ClassGeneral_Assmply.FGetGeneral_Assmply(DLNumber_Admin);
            ClassAdmin_Arn.FGetAmeenAlsondoq(DLAmeenAlsondoq);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);

            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")); i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 10; i--)
            {
                dtYear.Rows.Add(i);
            }
            DLYears.Items.Clear();
            DLYears.Items.Add("");
            DLYears.AppendDataBoundItems = true;
            DLYears.DataTextField = "Year";
            DLYears.DataValueField = "Year";
            DLYears.DataSource = dtYear;
            DLYears.DataBind();
            ClassQuaem.FGetSupportType(1, DLProject);
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select Top(1) bill_Number_ from tbl_General_Assmply_Bill With(NoLock) Where [Is_Delete_] = @0 Order by bill_Number_ Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtbill_Number.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["bill_Number_"]) + 1);
        else
            txtbill_Number.Text = "1";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
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
            FCheckNumber();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غيرمتوقع حاول لاحقاً ! ";
            return;
        }
    }

    private void FCheckNumber()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) [bill_Number_] FROM [dbo].[tbl_General_Assmply_Bill] With(NoLock) Where [bill_Number_] = @0 And [Is_Delete_] = @1", txtbill_Number.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "رقم الفاتورة مستخدم , يرجى تغيير رقم الفاتورة";
            return;
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) [Number_Admin_],[_Years_] FROM [dbo].[tbl_General_Assmply_Bill] With(NoLock) Where [Number_Admin_] = @0 And [_Years_] = @1 And [Is_Delete_] = @2",
            DLNumber_Admin.SelectedValue, DLYears.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "قام هذا الشخص بالتسديد مسبقاً ! ";
        }
        else
            FChackAdwiah();
    }

    public void FChackAdwiah()
    {
        if (RBCash_Money.Checked == false && RBShayk_Bank.Checked == false && RBEdaa_Bank.Checked == false)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "يجب تحديد نوع الدفع ! ";
        }
        else
            FArnGeneral_Assmply_Bill_Add();
    }

    private void FArnGeneral_Assmply_Bill_Add()
    {
        GetCookie();
        bool XIS_Bank = false;
        string XID_Bank = string.Empty, XID_Account = string.Empty;
        if (DLAccount.SelectedValue == "البنك")
        {
            XIS_Bank = true;
            XID_Bank = DL_Bank.SelectedValue;
            XID_Account = DL_Account.SelectedValue;
        }
        else if (DLAccount.SelectedValue != "البنك")
        {
            XIS_Bank = false; XID_Bank = Guid.Empty.ToString(); XID_Account = Guid.Empty.ToString();
        }
        ClassGeneral_Assmply_Bill CGAB = new ClassGeneral_Assmply_Bill()
        {
            ID_Uniq = Convert.ToString(Guid.NewGuid()),
            Number_Admin = Convert.ToInt32(DLNumber_Admin.SelectedValue),
            bill_Number = Convert.ToInt32(txtbill_Number.Text.Trim()),
            The_Mony = Convert.ToInt32(txtThe_Mony.Text.Trim()),
            IsCash_Money = RBCash_Money.Checked,
            IsShayk_Bank = RBShayk_Bank.Checked,
            Number_Shayk = txtNumber_Shayk.Text.Trim(),
            Date_Shayk_Bank = txtDate_Shayk_Bank.Text.Trim(),
            For_Bank = txtFor_Bank.Text.Trim(),
            Edaa_Bank = RBEdaa_Bank.Checked,
            For_Edaa_Bank = txtFor_Edaa_Bank.Text.Trim(),
            Number_Edaa = txtNumber_Edaa.Text.Trim(),
            Date_Edaa_Bank = txtDate_Edaa_Bank.Text.Trim(),
            _Date_Get = txtDate_Get.Text.Trim(),
            _Years = Convert.ToInt32(DLYears.SelectedValue),
            ID_Ameen_Alsondoq = Convert.ToInt32(DLAmeenAlsondoq.SelectedValue),
            IsAllow_Ameen_Alsondoq = CBAmeenAlsondoq.Checked,
            ID_Raees_AlMagles = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            IsAllow_Raees_AlMagles = CBRaeesAlmaglis.Checked,
            ID_Admin = Convert.ToInt32(IDUser),
            Date_Add = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            More_Details = txtMore_Details.Text.Trim(),
            Finance_Account = DLAccount.SelectedValue,
            Is_Bank = XIS_Bank,
            ID_Bank = XID_Bank,
            ID_Account = XID_Account,
            The_Project = Convert.ToInt32(DLProject.SelectedValue),
            Is_Delete = false
        };
        CGAB.BArnGeneral_Assmply_Bill_Add();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم إضافة البيانات بنجاح";
        FGetLastRecord();
    }

    protected void LB_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void RBCash_Money_CheckedChanged(object sender, EventArgs e)
    {
        if (RBCash_Money.Checked)
        {
            IDSheyk.Visible = false;
            IDEdaa.Visible = false;
        }
    }

    protected void RBShayk_Bank_CheckedChanged(object sender, EventArgs e)
    {
        if (RBShayk_Bank.Visible)
        {
            txtNumber_Shayk.Focus();
            IDSheyk.Visible = true;
            IDEdaa.Visible = false;
        }
    }

    protected void RBEdaa_Bank_CheckedChanged(object sender, EventArgs e)
    {
        if (RBEdaa_Bank.Visible)
        {
            txtNumber_Edaa.Focus();
            IDSheyk.Visible = false;
            IDEdaa.Visible = true;
        }
    }

    protected void LB_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAdd.aspx");
    }

    protected void DLAccount_Load(object sender, EventArgs e)
    {
        DLAccount.Attributes["onchange"] = "Validate();";
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

    protected void DL_Account_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        RBCash_Money.Checked = false; RBShayk_Bank.Checked = false; RBEdaa_Bank.Checked = true;
        IDSheyk.Visible = false;
        IDEdaa.Visible = true;
        txtFor_Edaa_Bank.Text = DL_Bank.SelectedItem.Text;
        txtDate_Edaa_Bank.Text = txtDate_Get.Text.Trim();
        string XAccount = DL_Account.SelectedItem.Text.Split(new char[] { '[', ']' })[1];
        txtNumber_Edaa.Text = XAccount.Trim();
        txtThe_Mony.Focus();
    }

}