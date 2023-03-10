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

public partial class CResearchers_CPVillage_PageTecisionToExcludeAdd : System.Web.UI.Page
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
            bool A90;
            A90 = Convert.ToBoolean(dtViewProfil.Rows[0]["A90"]);
            if (A90 == false)
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
            FGetAdminInManagement();
            txtDateQarar.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            txtDateReport.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            pnlGetNull.Visible = true;
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where IsQobol = @0 And IsEstepaad = @1 And IsDelete = @2 Order by NumberQarar Desc", Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberQarar.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberQarar"]) + 1);
        }
        FGetLastRecord2();
    }

    private void FGetLastRecord2()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where IsQobol = @0 And IsEstepaad = @1 And IsDelete = @2 Order by NumberReport Desc", Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumberReport.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["NumberReport"]) + 1);
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
            Label1.Text = "يبدو ان هذا المستفيد مستبعد بالفعل";
            Label1.ForeColor = System.Drawing.Color.Red;
            pnlData.Visible = false;
            pnlGetNull.Visible = true;
            pnlGetData.Visible = false;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageTecisionToExcludeAdd.aspx");
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

    private void FGetAdminInManagement()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from tbl_Admin With(noLock) Where IsAdminInEdarah = @0 And IsDelete = @1 And _IsOldMaglis = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTGetAdminInManagment.DataSource = dt;
            RPTGetAdminInManagment.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckNumberQarar();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNumberQarar()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where NumberQarar = @0 And IsQobol = @1 And IsEstepaad = @2 And IsDelete = @3", txtNumberQarar.Text.Trim(), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "رقم القرار مستخدم لشخص آخر قم بتغييره";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FCheckNumberReport();
        }
    }

    private void FCheckNumberReport()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where NumberReport = @0 And IsQobol = @1 And IsEstepaad = @2 And IsDelete = @3", txtNumberReport.Text.Trim(), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where NumberMostafeed = @0 And NumberReport = @1 And NumberQarar = @2 And IsQobol = @3 And IsEstepaad = @4 And IsDelete = @5", txtNumberMostafeed.Text.Trim(), txtNumberReport.Text.Trim(), txtNumberQarar.Text.Trim(), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "تم إضافة هذا القرار مسبقاً";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FArnQararQobolMustafeedAdd();
        }
    }

    private void FArnQararQobolMustafeedAdd()
    {
        GetCookie();
        ClassQararQobol CQQ = new ClassQararQobol()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _NumberMostafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            _DateQarar = Convert.ToDateTime(txtDateQarar.Text.Trim()).ToString("yyyy/MM/dd"),
            _DateReport = Convert.ToDateTime(txtDateReport.Text.Trim()).ToString("yyyy/MM/dd"),
            _NumberReport = Convert.ToInt32(txtNumberReport.Text.Trim()),
            _NumberQarar = Convert.ToInt32(txtNumberQarar.Text.Trim()),
            _DateAddQara = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = "0",
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsQobol = false,
            _IsEstepaad = true,
            _IsDelete = false
        };
        CQQ.BArnQararQobolMustafeedAdd();

        DataTable DTAdmin = new DataTable();
        DTAdmin = ClassDataAccess.GetData("Select * from tbl_Admin With(noLock) Where IsAdminInEdarah = @0 And IsDelete = @1 And _IsOldMaglis = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (DTAdmin.Rows.Count > 0)
        {
            for (int i = 0; i <= DTAdmin.Rows.Count - 1; i++)
            {
                ClassQararQobolAdmin CQQA = new ClassQararQobolAdmin()
                {
                    _NumberMostafeed = Convert.ToInt64(txtNumberMostafeed.Text.Trim()),
                    _NumberReport = Convert.ToInt64(txtNumberReport.Text.Trim()),
                    _NumberQarar = Convert.ToInt64(txtNumberQarar.Text.Trim()),
                    _IDAdmin = Convert.ToInt32(DTAdmin.Rows[i]["ID_Item"]),
                    _AdminAllow = false,
                    _A1 = "0",
                    _A2 = "0",
                    _A3 = "0",
                    _A4 = "0",
                    _A5 = "0",
                    _IsDelete = false
                };
                CQQA.BArnQararQobolMustafeedAdminAdd();
            }
        }
        //FArnRasAlEstemarahUpdateTypeMostafeed();
        Label1.Text = "تم الإضافة بنجاح ";
        Label1.ForeColor = System.Drawing.Color.MediumAquamarine;
        System.Threading.Thread.Sleep(500);
    }

    private void FArnRasAlEstemarahUpdateTypeMostafeed()
    {
        ClassMosTafeed CM = new ClassMosTafeed();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [IDUniq] FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0", txtNumberMostafeed.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            CM._IDUniq = dt.Rows[0]["IDUniq"].ToString();
            CM._TypeMostafeed = "مستبعد";
            CM.BArnRasAlEstemarahUpdateTypeMostafeed();
        }
    }

}