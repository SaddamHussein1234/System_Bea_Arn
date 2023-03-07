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

public partial class Shaerd_CPVillage_PageConvertedCasesEdit : System.Web.UI.UserControl
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
            bool A104, IsModer;
            A104 = Convert.ToBoolean(dtViewProfil.Rows[0]["A104"]);
            IsModer = Convert.ToBoolean(dtViewProfil.Rows[0]["IsModer"]);
            if (A104 == false || IsModer == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (IsModer == false)
            {
                PnlManager.Visible = false;
            }
            else if (IsModer == true)
            {
                PnlManager.Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetName();
            txtNumberMostafeed.Focus();
            pnlGetNull.Visible = true;
            FGetModerAlGmeiah();
            FGetHalafAlMosTafeed();
            FGetAlBaheth();
            FArnTahweelAlHalahByIDUniq();
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

    private void FGetHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDeleteHalatMostafeed = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLHalafAlMosTafeed.Items.Clear();
            DLHalafAlMosTafeed.Items.Add("");
            DLHalafAlMosTafeed.AppendDataBoundItems = true;
            DLHalafAlMosTafeed.DataValueField = "IDItem";
            DLHalafAlMosTafeed.DataTextField = "HalatMostafeed";
            DLHalafAlMosTafeed.DataSource = dt;
            DLHalafAlMosTafeed.DataBind();
        }
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
            DLName.SelectedValue = dt.Rows[0]["NumberMostafeed"].ToString();
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            lblOldHalah.Text = lblHalatAlmostafeed.Text;
            Session["XID"] = Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]);
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
            Label1.Text = "يبدو ان هذا المستفيد ليس مستبعد";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
    }

    private void FArnTahweelAlHalahByIDUniq()
    {
        ClassTahweelAlHalah CTA = new ClassTahweelAlHalah();
        CTA._IDUniq = Convert.ToString(Request.QueryString["XID"]);
        CTA._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CTA.BArnTahweelAlHalahByIDUniq();
        if (dt.Rows.Count > 0)
        {
            txtAlAsBab.Visible = false;
            txtNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
            FGetDataMostafed();
            DLName.SelectedValue = txtNumberMostafeed.Text;
            txtNumberOrder.Text = dt.Rows[0]["NumberOrder"].ToString();
            DLHalafAlMosTafeed.SelectedValue = dt.Rows[0]["HalatAlmostafeedAfter"].ToString();
            DLAlBaheth.SelectedValue = dt.Rows[0]["IDAlbaheth"].ToString();
            txtDateOrder.Text = Convert.ToDateTime(dt.Rows[0]["DateOrder"]).ToString("dd-MM-yyyy");
            txtSabbAlTahweel.Text = dt.Rows[0]["SabbAlTahweel"].ToString();
            DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
            CBAllow.Checked = Convert.ToBoolean(dt.Rows[0]["AllowAlhalah"]);
            CBNotAllow.Checked = Convert.ToBoolean(dt.Rows[0]["BlockAlhalah"]);
            if (CBNotAllow.Checked)
            {
                txtAlAsBab.Visible = true;
            }
            txtAlAsBab.Text = dt.Rows[0]["AlAsbaab"].ToString();
        }
    }

    private void FRestDefault()
    {
        DLHalafAlMosTafeed.SelectedValue = null;
        txtDateOrder.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
        txtSabbAlTahweel.Text = string.Empty;
        CBAllow.Checked = false;
        CBNotAllow.Checked = false;
        txtAlAsBab.Text = string.Empty;
    }

    protected void txtNumberMostafeed_TextChanged(object sender, EventArgs e)
    {
        FGetDataMostafed();
        FRestDefault();
    }

    protected void DLName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetDataMostafedByName();
        FRestDefault();
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
            lblOldHalah.Text = lblHalatAlmostafeed.Text;
            Session["XID"] = Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]);
            lblDateBrithDay.Text = dt.Rows[0]["dateBrith"].ToString();
            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
            try
            {
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            }
            catch (Exception)
            {
                lblAge.Text = dt.Rows[0]["Age"].ToString();
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            GetCookie();
            if (pnlManage.Visible == false)
            {
                FArnEadatMostafeedEdit(Convert.ToInt32(IDUser));
                Label1.Text = "تم التعديل بنجاح ";
                Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
            }
            else if (pnlManage.Visible == true)
            {
                if (CBAllow.Checked == false && CBNotAllow.Checked == false)
                {
                    Label1.Text = " يجب إعتماد أو إلغاء إعتماد الحالة ";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
                else if (CBAllow.Checked == true || CBNotAllow.Checked == true)
                {
                    FArnEadatMostafeedEdit(0);
                    Label1.Text = "تم التعديل والإطلاع بنجاح ";
                    Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FArnEadatMostafeedEdit(int IDUpdate)
    {
        ClassTahweelAlHalah CTA = new ClassTahweelAlHalah();
        CTA._IDUniq = Convert.ToString(Request.QueryString["XID"]);
        CTA._NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
        CTA._NumberOrder = Convert.ToInt32(txtNumberOrder.Text.Trim());
        CTA._DateOrder = Convert.ToDateTime(txtDateOrder.Text.Trim()).ToString("yyyy/MM/dd");
        CTA._HalatAlmostafeedBefor = Convert.ToInt32(Session["XID"]);
        CTA._SabbAlTahweel = txtSabbAlTahweel.Text.Trim();
        CTA._HalatAlmostafeedAfter = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
        CTA._AllowAlhalah = CBAllow.Checked;

        CTA._BlockAlhalah = CBNotAllow.Checked;
        CTA._AlAsbaab = txtAlAsBab.Text.Trim();
        CTA._IDAlbaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
        CTA._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
        if (CBAllow.Checked == true || CBNotAllow.Checked == true)
        {
            CTA._IsAllowModer = true;
        }
        else
        {
            CTA._IsAllowModer = false;
        }
        if (IDUpdate != 0)
        {
            CTA._IDUpdate = IDUpdate;
        }
        CTA._DateLastUpdate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd hh:mm:ss");
        CTA._A1 = "0";
        CTA._A2 = "0";
        CTA._A3 = "0";
        CTA._A4 = "0";
        CTA._A5 = "0";
        CTA.BArnTahweelAlHalahEdit();
        if (CBAllow.Checked == true)
        {
            FArnRasAlEstemarahUpdateHalafAlMosTafeed();
        }
        else
        {
            if (Attach_Repostry_SMS_Send_.AllSendSystemSocialSearch())
                Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل طلب تحويل الحالة" + "\n" + "رقم الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
        }
        FGetDataMostafed();
    }

    private void FArnRasAlEstemarahUpdateHalafAlMosTafeed()
    {
        ClassMosTafeed CM = new ClassMosTafeed();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [IDUniq] FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0", txtNumberMostafeed.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            CM._IDUniq = dt.Rows[0]["IDUniq"].ToString();
            CM._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
            CM.BArnRasAlEstemarahUpdateHalafAlMosTafeed();
        }
    }

    protected void CBAllow_CheckedChanged(object sender, EventArgs e)
    {
        if (CBAllow.Checked)
        {
            CBNotAllow.Checked = false;
            txtAlAsBab.Visible = false;
        }
    }

    protected void CBNotAllow_CheckedChanged(object sender, EventArgs e)
    {
        if (CBNotAllow.Checked)
        {
            CBAllow.Checked = false;
            txtAlAsBab.Focus();
            txtAlAsBab.Visible = true;
        }
    }
    
}