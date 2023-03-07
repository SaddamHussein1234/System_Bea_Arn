using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.GAM;
using Library_CLS_Arn.Saddam;
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

public partial class Cpanel_CPGeneralAssembly_PageGeneralAssemblyAdd : System.Web.UI.Page
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
            bool A144, A145;
            A144 = Convert.ToBoolean(dtViewProfil.Rows[0]["A144"]);
            A145 = Convert.ToBoolean(dtViewProfil.Rows[0]["A145"]);
            if (A144 == false)
                Response.Redirect("PageNotAccess.aspx");
            PnlAllow.Visible = true;
            IDManager.Visible = true;

            if (A145 == false)
            {
                PnlAllow.Visible = false;
                IDManager.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            ClassAdmin_Arn.FGetGeneral_Assmply(DL_Admin);           
            txtDate_Rigstry.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            FGetLastRecord();
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            DL_Admin.SelectedValue = Request.QueryString["ID"];
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select Top(1) Number_Rigstry_ from tbl_General_Assmply With(NoLock) Where [Is_Active_] = @0 And [Is_Delete_] = @1 Order by Number_Rigstry_ Desc", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtNumber_Rigstry.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Rigstry_"]) + 1);
        else
            txtNumber_Rigstry.Text = "1";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
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
        dt = ClassDataAccess.GetData("SELECT Top(1) Number_Rigstry_ FROM [dbo].[tbl_General_Assmply] With(NoLock) Where [Number_Rigstry_] = @0 And [Is_Delete_] = @1", txtNumber_Rigstry.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "رقم العضوية مستخدم لشخص آخر قم بتغييره";
            return;
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) [ID_Admin_Account_] FROM [dbo].[tbl_General_Assmply] With(NoLock) Where [ID_Admin_Account_] = @0 And [Is_Delete_] = @1", DL_Admin.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة هذا الملف مسبقاً ! ";
        }
        else
            FChackAdwiah();
    }

    public void FChackAdwiah()
    {
        if (RBIs_Aamel.Checked == false && RBIs_Montaseeb.Checked == false)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "يجب تحديد العضوية ! ";
            RBIs_Aamel.Focus();
        }
        else
            FArnGeneral_Assmply_Add();
    }

    private void FArnGeneral_Assmply_Add()
    {
        GetCookie();
        ClassGeneral_Assmply CGA = new ClassGeneral_Assmply()
        {
            ID_Uniq = Convert.ToString(Guid.NewGuid()),
            Number_Rigstry = Convert.ToInt32(txtNumber_Rigstry.Text.Trim()),
            ID_Admin_Account = Convert.ToInt32(DL_Admin.SelectedValue),
            Date_Bridth = txtDate_Bridth.Text.Trim(),
            The_Job = txtThe_Job.Text.Trim(),
            Address_Job = txtAddress_Job.Text.Trim(),
            Date_Card = txtDate_Card.Text.Trim(),
            Card_Source = txtCard_Source.Text.Trim(),
            Phone_Home = txtPhone_Home.Text.Trim(),
            Phone_Work = txtPhone_Work.Text.Trim(),
            Phone_Other = txtPhone_Other.Text.Trim(),
            Address = txtAddress.Text.Trim(),
            Box_Email = txtBox_Email.Text.Trim(),
            Serial_Email = txtSerial_Email.Text.Trim(),
            Date_Rigstry = txtDate_Rigstry.Text.Trim(),
            Is_Aamel = Convert.ToBoolean(RBIs_Aamel.Checked),
            Is_Montaseeb = Convert.ToBoolean(RBIs_Montaseeb.Checked),
            Number_Qarar = Convert.ToInt32(txtNumber_Qarar.Text.Trim()),
            Date_Qarar = txtDate_Qarar.Text.Trim(),
            Date_Qobol = txtDate_Qobol.Text.Trim(),
            ID_Raees_AlMagles = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            Is_Allow = Convert.ToBoolean(CBRaeesAlmaglis.Checked),
            ID_Admin = Convert.ToInt32(IDUser),
            Date_Add = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            A1 = "0",
            A2 = "0",
            A3 = "0",
            Is_Active = Convert.ToBoolean(CBRaeesAlmaglis.Checked),
            Is_Delete = false
        };
        CGA.BArnGeneral_Assmply_Add();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم إضافة البيانات بنجاح";
    }

    protected void LB_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageGeneralAssembly.aspx");
    }

    protected void DL_Admin_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT TOP (1) [ID_Item],[Email],[PhoneNumber],[A3] FROM [dbo].[tbl_Admin] With(NoLock) Where [ID_Item] = @0",
                DL_Admin.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                lbl_Email.Text = dt.Rows[0]["Email"].ToString();
                lbl_Phone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lbl_IC_Card.Text = dt.Rows[0]["A3"].ToString();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LB_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageGeneralAssemblyAdd.aspx");
    }

}