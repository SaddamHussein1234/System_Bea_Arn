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

public partial class CResearchers_CPVillage_PageAfieldVisitAdd : System.Web.UI.Page
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
            txtDateVisit.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            txtDay.Text = ClassDataAccess.GetCurrentTime().ToString("ddd");
            FGetName();
            txtNumberMostafeed.Focus();
            FGetLastRecord();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ZeyarahMaydanyah] With(NoLock) Where IsDelete = @0 Order by NumberAlZyarah Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberVisit.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberAlZyarah"]) + 1);
        }
        FGetAlBaheth();
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where ID_Item = @0 And IsBaheth = @1 And IsDelete = @2 Order by IsOrderAdminInEdarah ", IDUser, Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //DLAlBaheth.Items.Clear();
            //DLAlBaheth.Items.Add("");
            //DLAlBaheth.AppendDataBoundItems = true;
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
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
    }

    private void FGetName()
    {
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

    private void FGetDataMostafed()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And [TypeMostafeed] = @1 And NumberMostafeed = @2 And RasAlEstemarah.IsDelete = @3 And tbl_MultiQariah.IsDelete = @3 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, "دائم", txtNumberMostafeed.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
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
        }
        else
        {
            pnlData.Visible = false;
            Label1.Text = "يبدو ان هذا المستفيد ليس موجود في النظام , أو ليس لديك صلاحية الوصول إلية";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAfieldVisitAdd.aspx");
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
        }
        else
        {
            pnlData.Visible = false;
        }
    }

    protected void txtDateVisit_TextChanged(object sender, EventArgs e)
    {
        txtDay.Text = Convert.ToDateTime(txtDateVisit.Text.Trim()).ToString("ddd");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckNumberZyara();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNumberZyara()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ZeyarahMaydanyah] With(NoLock) Where NumberAlZyarah = @0 And IsDelete = @1", txtNumberVisit.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "رقم الزيارة مستخدم لشخص آخر قم بتغييره";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FZyaraAdd();
        }
    }

    private void FZyaraAdd()
    {
        GetCookie();
        ClassZeyarahMaydanyah CZM = new ClassZeyarahMaydanyah()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _NumberAlZyarah = Convert.ToInt64(txtNumberVisit.Text.Trim()),
            _NumberAlMosTafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            _BahthHalatMosTafeed = Convert.ToBoolean(CBBahthHalatMosTafeed.Checked),
            _EadatAlBahthLestafeedHaly = Convert.ToBoolean(CBEadatAlBahthLestafeedHaly.Checked),
            _EadatAlBahthLeMostafeedSabeq = Convert.ToBoolean(CBEadatAlBahthLeMostafeedSabeq.Checked),
            _NameAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue),
            _ToDay = txtDay.Text.Trim(),
            _DayAlZeyarah = Convert.ToDateTime(txtDateVisit.Text.Trim()).ToString("yyyy/MM/dd"),
            _AllowAlZeyarah = false,
            _NotAllowAlZeyarah = false,
            _AlAsBab = string.Empty,
            _DataAddAlZeyarah = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Convert.ToInt32(IDUser),
            _IDManage = 0,
            _StateView = false,
            _IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
            _IDRaeesMaglesAEdarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            _A1 = "0",
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CZM.BArnZeyarahMaydanyahAdd();
        Label1.Text = "تم الإضافة بنجاح ";
        Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
    }

}