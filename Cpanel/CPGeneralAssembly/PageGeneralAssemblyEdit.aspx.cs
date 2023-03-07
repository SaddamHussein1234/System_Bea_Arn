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

public partial class Cpanel_CPGeneralAssembly_PageGeneralAssemblyEdit : System.Web.UI.Page
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
            if (A144 == false)
                Response.Redirect("LogOut.aspx");
            PnlAllow.Visible = true;
            IDManager.Visible = true;
            A145 = Convert.ToBoolean(dtViewProfil.Rows[0]["A145"]);
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
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT TOP (1) * FROM [dbo].[tbl_General_Assmply] With(NoLock) Where [ID_Uniq_] = @0",
                Convert.ToString(Request.QueryString["XID"]));
            if (dt.Rows.Count > 0)
            {
                Session["_Old_Number_Rigstry"] = dt.Rows[0]["Number_Rigstry_"].ToString();
                txtNumber_Rigstry.Text = Session["_Old_Number_Rigstry"].ToString();
                Session["_Old_Full_Name"] = dt.Rows[0]["ID_Admin_Account_"].ToString();
                DL_Admin.SelectedValue = Session["_Old_Full_Name"].ToString();
                FGetDataAdmin();
                txtDate_Bridth.Text = Convert.ToDateTime(dt.Rows[0]["Date_Bridth_"]).ToString("yyyy-MM-dd");
                txtThe_Job.Text = dt.Rows[0]["The_Job_"].ToString();
                txtAddress_Job.Text = dt.Rows[0]["Address_Job_"].ToString();
                txtDate_Card.Text = Convert.ToDateTime(dt.Rows[0]["Date_Card_"]).ToString("yyyy-MM-dd");
                txtCard_Source.Text = dt.Rows[0]["Card_Source_"].ToString();
                txtPhone_Home.Text = dt.Rows[0]["Phone_Home_"].ToString();
                txtPhone_Work.Text = dt.Rows[0]["Phone_Work_"].ToString();
                txtPhone_Other.Text = dt.Rows[0]["Phone_Other_"].ToString();
                txtAddress.Text = dt.Rows[0]["Address_"].ToString();
                txtBox_Email.Text = dt.Rows[0]["Box_Email_"].ToString();
                txtSerial_Email.Text = dt.Rows[0]["Serial_Email_"].ToString();
                txtDate_Rigstry.Text = Convert.ToDateTime(dt.Rows[0]["Date_Rigstry_"]).ToString("yyyy-MM-dd");
                RBIs_Aamel.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Aamel_"]);
                RBIs_Montaseeb.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Montaseeb_"]);
                txtNumber_Qarar.Text = dt.Rows[0]["Number_Qarar_"].ToString();
                txtDate_Qarar.Text = Convert.ToDateTime(dt.Rows[0]["Date_Qarar_"]).ToString("yyyy-MM-dd");
                txtDate_Qobol.Text = Convert.ToDateTime(dt.Rows[0]["Date_Qobol_"]).ToString("yyyy-MM-dd");
                DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["ID_Raees_AlMagles_"].ToString();
                CBRaeesAlmaglis.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Allow_"]);
            }
            else
                Response.Redirect("PageGeneralAssembly.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageGeneralAssembly.aspx");
        }
    }

    protected void LB_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageGeneralAssembly.aspx");
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
        if (txtNumber_Rigstry.Text.Trim() != Session["_Old_Number_Rigstry"].ToString())
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
            {
                Session["_Old_Number_Rigstry"] = txtNumber_Rigstry.Text.Trim();
                FCheckName();
            }
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        if (DL_Admin.SelectedValue != Session["_Old_Full_Name"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) [Full_Name_] FROM [dbo].[tbl_General_Assmply] With(NoLock) Where [Full_Name_] = @0 And [Is_Delete_] = @1", DL_Admin.SelectedValue, Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة هذا الملف مسبقاً";
            }
            else
            {
                Session["_Old_Full_Name"] = DL_Admin.SelectedValue;
                FChackAdwiah();
            }
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
            FArnGeneral_Assmply_Edit();
    }

    private void FArnGeneral_Assmply_Edit()
    {
        GetCookie();
        ClassGeneral_Assmply CGA = new ClassGeneral_Assmply()
        {
            ID_Uniq = Convert.ToString(Request.QueryString["XID"]),
            Number_Rigstry = Convert.ToInt32(Session["_Old_Number_Rigstry"]),
            ID_Admin_Account = Convert.ToInt32(Session["_Old_Full_Name"]),
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
            A1 = "0",
            A2 = "0",
            A3 = "0",
            Is_Active = Convert.ToBoolean(CBRaeesAlmaglis.Checked)
        };
        CGA.BArnGeneral_Assmply_Edit();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم تعديل البيانات بنجاح";
        FGetData();
    }

    protected void DL_Admin_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetDataAdmin();
    }

    private void FGetDataAdmin()
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

}