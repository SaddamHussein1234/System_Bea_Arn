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

public partial class CResearchers_CPVillage_PageAfieldVisitEdit : System.Web.UI.Page
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
            bool A92;
            A92 = Convert.ToBoolean(dtViewProfil.Rows[0]["A92"]);
            if (A92 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            txtDay.Text = ClassDataAccess.GetCurrentTime().ToString("ddd");
            FGetName();
            txtNumberMostafeed.Focus();
            FGetAlBaheth();
            FGetData();
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
            DLModerAlGmeiah.Items.Clear();
            DLModerAlGmeiah.Items.Add("");
            DLModerAlGmeiah.AppendDataBoundItems = true;
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.Items.Clear();
            DLRaeesMaglesAlEdarah.Items.Add("");
            DLRaeesMaglesAlEdarah.AppendDataBoundItems = true;
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
    }

    private void FGetData()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from ZeyarahMaydanyah Where IDUniq = @0 And IsDelete = @1", Convert.ToString(Request.QueryString["ID"]), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblAge.Text = dt.Rows[0]["NameAlBaheth"].ToString();
            txtNumberVisit.Text = dt.Rows[0]["NumberAlZyarah"].ToString();
            txtNumberMostafeed.Text = dt.Rows[0]["NumberAlMosTafeed"].ToString();
            DLName.SelectedValue = txtNumberMostafeed.Text.Trim();
            CBBahthHalatMosTafeed.Checked = Convert.ToBoolean(dt.Rows[0]["BahthHalatMosTafeed"]);
            CBEadatAlBahthLestafeedHaly.Checked = Convert.ToBoolean(dt.Rows[0]["EadatAlBahthLestafeedHaly"]);
            CBEadatAlBahthLeMostafeedSabeq.Checked = Convert.ToBoolean(dt.Rows[0]["EadatAlBahthLeMostafeedSabeq"]);
            DLAlBaheth.SelectedValue = dt.Rows[0]["NameAlBaheth"].ToString();
            txtDateVisit.Text = Convert.ToDateTime(dt.Rows[0]["DayAlZeyarah"]).ToString("dd-MM-yyyy");
            txtDay.Text = dt.Rows[0]["ToDay"].ToString();
            CBAllow.Checked = Convert.ToBoolean(dt.Rows[0]["AllowAlZeyarah"]);
            CBNotAllow.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowAlZeyarah"]);
            txtAlAsBab.Text = dt.Rows[0]["AlAsBab"].ToString();
            DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
            DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["IDRaeesMaglesAEdarah"].ToString();
        }
        FGetDataMostafed();
    }

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
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
        }
        else
        {
            pnlData.Visible = false;
        }
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        FGetDataMostafedByName();
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
        }
        else
        {
            pnlData.Visible = false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            FZyaraEdit();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FZyaraEdit()
    {
        ClassZeyarahMaydanyah CZM = new ClassZeyarahMaydanyah();
        CZM._IDUniq = Convert.ToString(Request.QueryString["ID"]);
        CZM._NumberAlMosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
        CZM._BahthHalatMosTafeed = Convert.ToBoolean(CBBahthHalatMosTafeed.Checked);
        CZM._EadatAlBahthLestafeedHaly = Convert.ToBoolean(CBEadatAlBahthLestafeedHaly.Checked);
        CZM._EadatAlBahthLeMostafeedSabeq = Convert.ToBoolean(CBEadatAlBahthLeMostafeedSabeq.Checked);
        CZM._NameAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
        CZM._ToDay = txtDay.Text.Trim();
        CZM._DayAlZeyarah = Convert.ToDateTime(txtDateVisit.Text.Trim()).ToString("yyyy/MM/dd");
        CZM._AllowAlZeyarah = CBAllow.Checked;
        CZM._NotAllowAlZeyarah = CBNotAllow.Checked;
        CZM._AlAsBab = txtAlAsBab.Text.Trim();
        CZM._IDAdmin = 101;
        CZM._IDManage = 105;
        if (CBAllow.Checked == true || CBNotAllow.Checked == true)
        {
            CZM._StateView = true;
        }
        else if (CBAllow.Checked == false || CBNotAllow.Checked == false)
        {
            CZM._StateView = false;
        }
        CZM._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
        CZM._IDRaeesMaglesAEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue);
        CZM._A1 = "0";
        CZM._A2 = "0";
        CZM._A3 = "0";
        CZM._A4 = "0";
        CZM._A5 = "0";
        CZM.BArnZeyarahMaydanyahEdit();
        Label1.Text = "تم التعديل بنجاح ";
        Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
    }

    protected void txtDateVisit_TextChanged(object sender, EventArgs e)
    {
        txtDay.Text = Convert.ToDateTime(txtDateVisit.Text.Trim()).ToString("ddd");
    }
    
    protected void CBAllow_CheckedChanged(object sender, EventArgs e)
    {
        if (CBAllow.Checked)
        {
            CBNotAllow.Checked = false;
        }
    }

    protected void CBNotAllow_CheckedChanged(object sender, EventArgs e)
    {
        if (CBNotAllow.Checked)
        {
            CBAllow.Checked = false;
            txtAlAsBab.Focus();
        }
    }

}