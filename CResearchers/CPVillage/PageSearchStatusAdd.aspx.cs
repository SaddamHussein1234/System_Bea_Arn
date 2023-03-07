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

public partial class CResearchers_CPVillage_PageSearchStatusAdd : System.Web.UI.Page
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
            bool A85;
            A85 = Convert.ToBoolean(dtViewProfil.Rows[0]["A85"]);
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
            //PnlManager.Visible = true;
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
            txtDateReport.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            pnlGetNull.Visible = true;
            txtAlAsBab.Visible = false;
            txtAlAsBabAllow.Visible = false;
            FGetLastRecord();
            DLAlBaheth.SelectedValue = IDUser;
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) IDNumberReport FROM [dbo].[BahthHalatMostafeed] With(NoLock) Where IsDelete = @0 Order by IDNumberReport Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberQarar.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDNumberReport"]) + 1);
        }
    }

    private void FGetName()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And [TypeMostafeed] = @1 And RasAlEstemarah.IsDelete = @2 And tbl_MultiQariah.IsDelete = @2 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, "دائم", Convert.ToString(false));
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
    
    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where ID_Item = @0 And IsBaheth = @1 And IsDelete = @2 Order by IsOrderAdminInEdarah ", IDUser, Convert.ToString(true), Convert.ToString(false));
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

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        FGetDataMostafed();
    }

    private void FGetDataMostafed()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And [TypeMostafeed] = @1 And NumberMostafeed = @2 And RasAlEstemarah.IsDelete = @3 And tbl_MultiQariah.IsDelete = @3 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, "دائم", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
            //txtNumberQarar.Text = txtNumberMostafeed.Text.Trim();
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            //DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
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
            Label1.Text = "يبدو ان هذا المستفيد ليس موجود في النظام , أو ليس لديك صلاحية الوصول إلية";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
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
            //txtNumberQarar.Text = txtNumberMostafeed.Text.Trim();
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSearchStatusAdd.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckAdd();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckAdd()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[BahthHalatMostafeed] With(NoLock) Where IDNumberReport = @0 And IsDelete = @1", txtNumberQarar.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "تم إضافة رقم القرار مسبقاً";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FArnBahthHalatMostafeedAdd();
        }
    }

    private void FArnBahthHalatMostafeedAdd()
    {
        GetCookie();
        ClassBahthHalatMostafeed CBHM = new ClassBahthHalatMostafeed();
        CBHM._IDUniq = Convert.ToString(Guid.NewGuid());
        CBHM._NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
        CBHM._DateOfReport = Convert.ToDateTime(txtDateReport.Text.Trim()).ToString("yyyy/MM/dd");
        CBHM._DateAddReport = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
        CBHM._AllowState = CBAllow.Checked;
        CBHM._AllowStateDetalis = txtAlAsBabAllow.Text.Trim();
        CBHM._NotAllowState = CBNotAllow.Checked;
        CBHM._WhayNotAllow = txtAlAsBab.Text.Trim();
        CBHM._IDAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
        CBHM._IDNumberReport = Convert.ToInt32(txtNumberQarar.Text.Trim());
        CBHM._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
        //if (CBAllow.Checked == true || CBNotAllow.Checked == true)
        //{
        CBHM._IsAllowModer = false;
        CBHM._IDRaeesLagnatAlBahth = Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue);
        CBHM._IsAllowRaeesLagnatAlBahth = false;
        //}
        CBHM._IDAdmin = Convert.ToInt32(IDUser);
        CBHM._A1 = "0";
        CBHM._A2 = "0";
        CBHM._A3 = "0";
        CBHM._A4 = "0";
        CBHM._A5 = "0";
        CBHM._IsDelete = false;
        CBHM.BArnBahthHalatMostafeedAdd();
        FGetDataMostafed();
        if (CBAllow.Checked == true)
        {
            Label1.Text = "تم الإضافة بنجاح ";
            Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
        }
        else
        {
            Label1.Text = "تم الإضافة بنجاح , بإنتظار الموافقه ";
            Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
        }
        txtNumberQarar.Text = Convert.ToString(Convert.ToInt32(txtNumberQarar.Text) + 1);
    }

}
