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

public partial class CResearchers_CPVillage_PageExclusionOfTheBeneficiaryAdd : System.Web.UI.Page
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
            bool A101;
            A101 = Convert.ToBoolean(dtViewProfil.Rows[0]["A101"]);
            if (A101 == false)
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
            FGetName();
            txtNumberMostafeed.Focus();
            FGetLastRecord();
            txtDateOrder.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            pnlGetNull.Visible = true;
            FGetModerAlGmeiah();
        }
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //DLModerAlGmeiah.Items.Clear();
            //DLModerAlGmeiah.Items.Add("");
            //DLModerAlGmeiah.AppendDataBoundItems = true;
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

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[EadatMostafeed] With(NoLock) Where IsEaadat =@0 And IsEstbaad = @1 And IsDelete = @2 Order by NumberOrder Desc", Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberOrder"]) + 1);
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
            Label1.Text = "يبدو ان هذا المستفيد ليس موجود , أو خارج نطاق صلاحيتك";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageExclusionOfTheBeneficiaryAdd.aspx");
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckNumberOrder();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNumberOrder()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[EadatMostafeed] With(NoLock) Where NumberOrder = @0 And IsEaadat = @1 And IsEstbaad = @2 And IsDelete = @3", txtNumberOrder.Text.Trim(), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "رقم الطلب مستخدم لشخص آخر قم بتغييره";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FCheckAdd();
        }
    }

    private void FCheckAdd()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[EadatMostafeed] With(NoLock) Where NumberOrder = @0 And NumberAlMostafeed = @1 And IsEaadat = @2 And IsEstbaad = @3 And IsDelete = @4", txtNumberOrder.Text.Trim(), txtNumberMostafeed.Text.Trim(), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "تم إضافة هذا الطلب مسبقاً";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FArnEadatMostafeedAdd();
        }
    }

    private void FArnEadatMostafeedAdd()
    {
        GetCookie();
        ClassEadatMostafeed CEM = new ClassEadatMostafeed()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _NumberOrder = Convert.ToInt32(txtNumberOrder.Text.Trim()),
            _DateOrder = Convert.ToDateTime(txtDateOrder.Text.Trim()).ToString("yyyy/MM/dd"),
            _NumberAlMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            _WhayErgaa = txtNote.Text.Trim(),
            _DateAddOrder = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue),
            _IDRaeesMaglisAledarah = Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = "0",
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsEaadat = false,
            _IsEstbaad = true,
            _IsDelete = false
        };
        CEM.BArnEadatMostafeedAdd();
        Label1.Text = " تم الإضافة بنجاح ";
        Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
    }

}