using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CHome_PageAuthenticatorWithSMS : System.Web.UI.Page
{
    ClassAdmin_Arn CA = new ClassAdmin_Arn();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetData();
    }

    private void GetData()
    {
        try
        {
            GetCookie();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CA.BArnAdminGetByIDUniq();
            if (dt.Rows.Count > 0)
            {
                lblCheck.Text = Wellcome();
                lblCheck2.Text = lblCheck.Text;
                HFIDUniq.Value = dt.Rows[0]["IDUniqUser"].ToString();
                HFUser_Name.Value = dt.Rows[0]["FirstName"].ToString();
                lblName.Text = HFUser_Name.Value;
                lblName2.Text = HFUser_Name.Value;
                lblUser.Text = dt.Rows[0]["User_Name_"].ToString();
                lblUser2.Text = lblUser.Text;
                HFPhone.Value = dt.Rows[0]["PhoneNumber"].ToString();
                lblPhone.Text = HFPhone.Value;
                lblPhone2.Text = lblPhone.Text;

                if (Convert.ToBoolean(dt.Rows[0]["_SMS_Enabled_"]))
                {
                    lbmsg.Text = "تم تفعيل المصادقة الثنائية , هل تريد الإلغاء (لا يُنصح بذلك)";
                    pnlActive.Visible = false;
                    pnlActived.Visible = true;
                    btnAdd.Visible = false;
                }
                else
                {
                    lbmsg.Text = "تفعيل المصادقة الثنائية";
                    pnlActive.Visible = true;
                    pnlActived.Visible = false;
                    btnAdd.Visible = false;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("PageMyAccount.aspx");
        }
    }

    private string Wellcome()
    {
        try
        {
            DateTime time = ClassDataAccess.GetCurrentTime();
            if ((time > Convert.ToDateTime("10:00:00 AM")) && (time < Convert.ToDateTime("11:59:50 AM")))
                return "صباح الخير";
            else if ((time > Convert.ToDateTime("12:00:00 PM")) && (time < Convert.ToDateTime("5:00:00 PM")))
                return "نهارك سعيد";
            else if ((time > Convert.ToDateTime("5:01:00 PM")) && (time < Convert.ToDateTime("11:59:50 PM")))
                return "مساء الخير";
            else if ((time > Convert.ToDateTime("12:00:00 AM")) && (time < Convert.ToDateTime("9:59:50 PM")))
                return "صباح الخير";
            else
                return "مرحباً بك";
        }
        catch (Exception)
        {

        }
        return "مرحباً بك";
    }

    protected void LBSend_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_OTP_"] = ClassSaddam.Generate_OTP_();

            string XResult = Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, " رمز التفعيل الخاص بك هو : " + Session["_OTP_"].ToString(), "BerArn", "Active_SMS", Convert.ToInt32(Test_Saddam.FGetIDUsiq()));
            if (XResult == "IsSuccessAdd")
            {
                IDCode.Visible = true;
                btnAdd.Visible = true;
                LBSend.Visible = false;
                txt_Code.Focus();
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;

        if (txt_Code.Text.Trim() == Session["_OTP_"].ToString())
        {
            FEnableAuth(true);
            lblSuccess.Text = "تم تفعيل المصادقة بنجاح , شكراً لك ... ";
            btnAdd.Visible = false;
        }
        else
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "رمز التحقق المدخل غير صحيح ... ";
            return;
        }
    }

    private void FEnableAuth(bool XValue)
    {
        try
        {
            GetCookie();
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[tbl_Admin] SET [_SMS_Enabled_] = @Two_Factor WHERE IDUniq = @IDUniq";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(IDUniq));
            cmd.Parameters.AddWithValue("@Two_Factor", XValue);
            cmd.ExecuteScalar();
            conn.Close();
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
        GetData();
    }

    protected void LBSendUnActive_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_OTP_"] = ClassSaddam.Generate_OTP_();
            string XResult = Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, " رمز التفعيل الخاص بك هو : " + Session["_OTP_"].ToString(), "BerArn", "Un_Active_SMS", Convert.ToInt32(Test_Saddam.FGetIDUsiq()));
            if (XResult == "IsSuccessAdd")
            {
                lbmsg.Text = " رمز التفعيل الخاص بك هو : " + Session["_OTP_"].ToString();
                IDCodeUnActive.Visible = true;
                LBSendUnActive.Visible = false;
                LBUnActive.Visible = true;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBUnActive_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;

        if (txt_CodeUnActive.Text.Trim() == Session["_OTP_"].ToString())
        {
            FEnableAuth(false);
            lblSuccess.Text = "تم إلغاء تفعيل المصادقة بنجاح , شكراً لك ... ";
            LBUnActive.Visible = false;
        }
        else
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "رمز التحقق المدخل غير صحيح ... ";
            return;
        }
    }

}