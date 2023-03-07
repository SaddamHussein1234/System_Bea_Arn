using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageSearchStatusEdit : System.Web.UI.Page
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
            bool A85, IsModer;
            A85 = Convert.ToBoolean(dtViewProfil.Rows[0]["A85"]);
            IsModer = Convert.ToBoolean(dtViewProfil.Rows[0]["IsModer"]);
            if (A85 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            //if (IsModer == false)
            //{
            //    PnlManager.Visible = false;
            //}
            //else if (IsModer == true)
            //{
            PnlManager.Visible = true;
            //}
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAlBaheth();
            FGetName();
            txtNumberMostafeed.Focus();
            pnlGetNull.Visible = true;
            txtAlAsBab.Visible = false;
            txtAlAsBabAllow.Visible = false;
            FGetData();
        }
    }

    private void FGetData()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1 * FROM [dbo].[BahthHalatMostafeed] Where IDUniq = @0 And IsDelete = @1",Convert.ToString(Request.QueryString["XID"]), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
            FGetDataMostafed();
            txtDateReport.Text = Convert.ToDateTime(dt.Rows[0]["DateOfReport"]).ToString("dd-MM-yyyy");
            CBAllow.Checked = Convert.ToBoolean(dt.Rows[0]["AllowState"]);
            txtAlAsBabAllow.Text = dt.Rows[0]["AllowStateDetalis"].ToString();
            if (CBAllow.Checked)
            {
                CBNotAllow.Checked = false;
                txtAlAsBab.Visible = false;
                txtAlAsBabAllow.Visible = true;
                txtAlAsBabAllow.Focus();
            }

            CBNotAllow.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowState"]);
            txtAlAsBab.Text = dt.Rows[0]["WhayNotAllow"].ToString();
            if (CBNotAllow.Checked)
            {
                CBAllow.Checked = false;
                txtAlAsBab.Visible = true;
                txtAlAsBabAllow.Visible = false;
                txtAlAsBab.Focus();
            }
            DLAlBaheth.SelectedValue = dt.Rows[0]["IDAlBaheth"].ToString();
            Session["OldNumber"] = dt.Rows[0]["IDNumberReport"].ToString();
            txtNumberQarar.Text = Session["OldNumber"].ToString();
            DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
            DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaeesLagnatAlBahth"].ToString();
        }
    }

    private void FGetName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [IDItem],[NumberMostafeed],[NameMostafeed] FROM [dbo].[RasAlEstemarah] Where IsDelete = @0 Order By NameMostafeed", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.Items.Clear();
            DLName.Items.Add("");
            DLName.AppendDataBoundItems = true;
            DLName.DataValueField = "NumberMostafeed";
            DLName.DataTextField = "NameMostafeed";
            DLName.DataSource = dt;
            DLName.DataBind();
        }
    }

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Session["OldNumber"] = dt.Rows[0]["NumberMostafeed"].ToString();
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
            {
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            }
            else
            {
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            }

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDay.Text = "لم يُضاف";
                lblAge.Text = "لم يُضاف";
            }
            pnlData.Visible = true;
            pnlGetNull.Visible = false;
            pnlGetData.Visible = true;
            Label1.Text = "بيانات المستفيد";
        }
        else
        {
            Label1.Text = "يبدو ان هذا المستفيد ليس موجود في النظام";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        FGetDataMostafed();
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetDataMostafedByName();
    }

    private void FGetDataMostafedByName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", DLName.SelectedValue, Convert.ToString(false));
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
            {
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            }
            else
            {
                lblAge.Text = dt.Rows[0]["Age"].ToString();
            }

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDay.Text = "لم يُضاف";
                lblAge.Text = "لم يُضاف";
            }
            pnlData.Visible = true;
            pnlGetNull.Visible = false;
            pnlGetData.Visible = true;
        }
        else
        {
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlBaheth.Items.Clear();
            DLAlBaheth.Items.Add("");
            DLAlBaheth.AppendDataBoundItems = true;
            DLAlBaheth.DataValueField = "ID_Item";
            DLAlBaheth.DataTextField = "FirstName";
            DLAlBaheth.DataSource = dt;
            DLAlBaheth.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckEdit();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckEdit()
    {
        if (txtNumberQarar.Text.Trim() != Session["OldNumber"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[BahthHalatMostafeed] With(NoLock) Where IDNumberReport = @0 And IsDelete = @1", txtNumberQarar.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                Label1.Text = "تم إضافة رقم القرار مسبقاً";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
            else
                FArnBahthHalatMostafeedeEdit();
        }
        else if (Session["OldNumber"].ToString() == txtNumberQarar.Text.Trim())
            FArnBahthHalatMostafeedeEdit();
    }

    private void FArnBahthHalatMostafeedeEdit()
    {
        ClassBahthHalatMostafeed CBHM = new ClassBahthHalatMostafeed();
        CBHM._IDUniq = Convert.ToString(Request.QueryString["XID"]);
        CBHM._NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
        CBHM._DateOfReport = Convert.ToDateTime(txtDateReport.Text.Trim()).ToString("yyyy/MM/dd");
        CBHM._AllowState = CBAllow.Checked;
        CBHM._AllowStateDetalis = txtAlAsBabAllow.Text.Trim();
        CBHM._NotAllowState = CBNotAllow.Checked;
        CBHM._WhayNotAllow = txtAlAsBab.Text.Trim();
        CBHM._IDAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
        CBHM._IDNumberReport = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
        CBHM._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
        CBHM._IsAllowModer = false;
        CBHM._IDRaeesLagnatAlBahth = Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue);
        CBHM._IsAllowRaeesLagnatAlBahth = false;
        CBHM._A1 = "0";
        CBHM._A2 = "0";
        CBHM._A3 = "0";
        CBHM._A4 = "0";
        CBHM._A5 = "0";
        CBHM.BArnBahthHalatMostafeedEdit();
        FGetData();
        Label1.Text = "تم التعديل بنجاح ";
        Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
        if (Attach_Repostry_SMS_Send_.AllSendSystemSocialSearch())
            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل بحث حالة" + "\n" + "رقم الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
    }

    protected void CBAllow_CheckedChanged(object sender, EventArgs e)
    {
        if (CBAllow.Checked)
        {
            CBNotAllow.Checked = false;
            txtAlAsBab.Visible = false;
            txtAlAsBabAllow.Visible = true;
            txtAlAsBabAllow.Focus();
        }
    }

    protected void CBNotAllow_CheckedChanged(object sender, EventArgs e)
    {
        if (CBNotAllow.Checked)
        {
            CBAllow.Checked = false;
            txtAlAsBab.Visible = true;
            txtAlAsBabAllow.Visible = false;
            txtAlAsBab.Focus();
        }
    }

}