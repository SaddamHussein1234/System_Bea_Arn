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

public partial class Cpanel_PageBeneficiaryBySearch : System.Web.UI.Page
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
            bool A39, A75;
            A39 = Convert.ToBoolean(dtViewProfil.Rows[0]["A39"]);
            A75 = Convert.ToBoolean(dtViewProfil.Rows[0]["A75"]);
            if (A39 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A75 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVBeneficiaryAll.Columns[0].Visible = false;
                GVBeneficiaryAll.Columns[12].Visible = false;
            }
            if (A39 == false && A75 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAlQariah();
            txtSearch.Focus();
            pnlSelect.Visible = true;
        }
    }

    private void FGetMostafeedByTypeMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And TypeMostafeed = @1 And IsDelete = @2 Order By NameMostafeed", txtSearch.Text.Trim(), DLTypeMostafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    private void FGetMostafeedByAlQaryah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And AlQaryah = @1 And IsDelete = @2 Order By NameMostafeed", txtSearch.Text.Trim(), DLAlQriah.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLAlQriah.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    private void FGetMostafeedByGender()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And Gender = @1 And IsDelete = @2 Order By NameMostafeed", txtSearch.Text.Trim(), DLGender.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLGender.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    private void FGetMostafeedByHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And HalafAlMosTafeed = @1 And IsDelete = @2 Order By NameMostafeed", txtSearch.Text.Trim(), DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض نوع المستفيد والقرية
    private void FGetMostafeedByTypeMosTafeedAndAlQaryah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And TypeMostafeed = @1 And AlQaryah = @2 And IsDelete = @3 Order By NameMostafeed", txtSearch.Text.Trim(), DLTypeMostafeed.SelectedValue, DLAlQriah.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString() + " - قرية " + DLAlQriah.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض نوع المستفيد والقرية والجنس
    private void FGetMostafeedByTypeMosTafeedAndAlQaryahAndGender()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And TypeMostafeed = @1 And AlQaryah = @2 And Gender = @3 And IsDelete = @4 Order By NameMostafeed", txtSearch.Text.Trim(), DLTypeMostafeed.SelectedValue, DLAlQriah.SelectedValue, DLGender.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString() + " - قرية " + DLAlQriah.SelectedItem.ToString() + " - " + DLGender.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض نوع المستفيد والقرية والجنس وحالة المستفيد
    private void FGetMostafeedByTypeMosTafeedAndAlQaryahAndGenderAndHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And TypeMostafeed = @1 And AlQaryah = @2 And Gender = @3 And HalafAlMosTafeed = @4 And IsDelete = @5 Order By NameMostafeed", txtSearch.Text.Trim(), DLTypeMostafeed.SelectedValue, DLAlQriah.SelectedValue, DLGender.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString() + " - قرية " + DLAlQriah.SelectedItem.ToString() + " - " + DLGender.SelectedItem.ToString() + " - " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض نوع المستفيد والجنس
    private void FGetMostafeedByTypeMosTafeedAndGender()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And TypeMostafeed = @1 And Gender = @2 And IsDelete = @3 Order By NameMostafeed", txtSearch.Text.Trim(), DLTypeMostafeed.SelectedValue, DLGender.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString() + " - " + DLGender.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض نوع المستفيد والجنس وحالة المستفيد 
    private void FGetMostafeedByTypeMosTafeedAndGenderAndHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And TypeMostafeed = @1 And Gender = @2 And HalafAlMosTafeed = @3 And IsDelete = @4 Order By NameMostafeed", txtSearch.Text.Trim(), DLTypeMostafeed.SelectedValue, DLGender.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString() + " - " + DLGender.SelectedItem.ToString() + " - " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض القرية والجنس
    private void FGetMostafeedByAlQaryahAndGender()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And AlQaryah = @1 And Gender = @2 And IsDelete = @3 Order By NameMostafeed", txtSearch.Text.Trim(), DLAlQriah.SelectedValue, DLGender.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب قرية " + DLAlQriah.SelectedItem.ToString() + " - " + DLGender.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض القرية والجنس وحالة المستفيد
    private void FGetMostafeedByAlQaryahAndGenderAndHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And AlQaryah = @1 And Gender = @2 And HalafAlMosTafeed = @3 And IsDelete = @4 Order By NameMostafeed", txtSearch.Text.Trim(), DLAlQriah.SelectedValue, DLGender.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب قرية " + DLAlQriah.SelectedItem.ToString() + " - " + DLGender.SelectedItem.ToString() + " - " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض القرية وحالة المستفيد
    private void FGetMostafeedByAlQaryahAndHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And AlQaryah = @1 And HalafAlMosTafeed = @2 And IsDelete = @3 Order By NameMostafeed", txtSearch.Text.Trim(), DLAlQriah.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب قرية " + DLAlQriah.SelectedItem.ToString() + " - " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض الجنس وحالة المستفيد
    private void FGetMostafeedByGenderAndHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And Gender = @1 And HalafAlMosTafeed = @2 And IsDelete = @3 Order By NameMostafeed", txtSearch.Text.Trim(), DLGender.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب جنس " + DLGender.SelectedItem.ToString() + " - " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض نوع المستفيد وحالة المستفيد
    private void FGetMostafeedByTypeMosTafeedAndHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And TypeMostafeed = @1 And HalafAlMosTafeed = @2 And IsDelete = @3 Order By NameMostafeed", txtSearch.Text.Trim(), DLTypeMostafeed.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString() + " - " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    //عرض نوع المستفيد والقرية وحالة المستفيد
    private void FGetMostafeedByTypeMosTafeedAndQariahAndHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And AlQaryah = @1 And TypeMostafeed = @2 And HalafAlMosTafeed = @3 And IsDelete = @4 Order By NameMostafeed", txtSearch.Text.Trim(),DLAlQriah.SelectedValue, DLTypeMostafeed.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            txtTitle.Text = " قائمة المستفيدين حسب " + DLTypeMostafeed.SelectedItem.ToString() + " -  قرية " + DLAlQriah.SelectedItem.ToString() + " - " + DLHalafAlMosTafeed.SelectedItem.ToString();
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQriah.Items.Clear();
            DLAlQriah.Items.Add("");
            DLAlQriah.AppendDataBoundItems = true;
            DLAlQriah.DataValueField = "IDItem";
            DLAlQriah.DataTextField = "AlQriah";
            DLAlQriah.DataSource = dt;
            DLAlQriah.DataBind();
        }
        FGetGender();
    }

    private void FGetGender()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[Quaem] With(NoLock) Where Gender <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLGender.Items.Clear();
            DLGender.Items.Add("");
            DLGender.AppendDataBoundItems = true;
            DLGender.DataValueField = "IDItem";
            DLGender.DataTextField = "Gender";
            DLGender.DataSource = dt;
            DLGender.DataBind();
        }
        FGetHalafAlMosTafeed();
    }

    private void FGetHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(5000) * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDeleteHalatMostafeed = @1 Order by IDItem", string.Empty, Convert.ToString(false));
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

    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            GVBeneficiaryAll.UseAccessibleHeader = false;
            FCheckSelect();
            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetMostafeedAll()
    {
        ClassMosTafeed CMF = new ClassMosTafeed();
        CMF._NameMostafeed = txtSearch.Text.Trim();
        CMF._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CMF.BArnRasAlEstemarahGetAll();
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
        txtTitle.Focus();
    }

    private void FCheckSelect()
    {
        GVBeneficiaryAll.Columns[0].Visible = true;
        GVBeneficiaryAll.Columns[11].Visible = true;
        GVBeneficiaryAll.Columns[12].Visible = true;
        CheckAccountAdmin();
        if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedAll();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedByTypeMosTafeed();
        else if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedByAlQaryah();
        else if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedByGender();
        else if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByHalafAlMosTafeed();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedByTypeMosTafeedAndAlQaryah();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedByTypeMosTafeedAndAlQaryahAndGender();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByTypeMosTafeedAndAlQaryahAndGenderAndHalafAlMosTafeed();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedByTypeMosTafeedAndGender();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByTypeMosTafeedAndGenderAndHalafAlMosTafeed();
        else if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            FGetMostafeedByAlQaryahAndGender();
        else if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByAlQaryahAndGenderAndHalafAlMosTafeed();
        else if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByAlQaryahAndHalafAlMosTafeed();
        else if (DLTypeMostafeed.SelectedValue == string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByGenderAndHalafAlMosTafeed();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue == string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByTypeMosTafeedAndHalafAlMosTafeed();
        else if (DLTypeMostafeed.SelectedValue != string.Empty && DLAlQriah.SelectedValue != string.Empty && DLGender.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            FGetMostafeedByTypeMosTafeedAndQariahAndHalafAlMosTafeed();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FCheckSelect();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVBeneficiaryAll.Columns[11].Visible = false;
            GVBeneficiaryAll.Columns[12].Visible = false;

            GVBeneficiaryAll.UseAccessibleHeader = true;
            GVBeneficiaryAll.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;

            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=5000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryBySearch.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVBeneficiaryAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVBeneficiaryAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[RasAlEstemarah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FCheckSelect();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GVBeneficiaryAll.Rows)
        {
            if ((row.FindControl("chkSelect") as CheckBox).Checked)
            {
                string firstColumnValue = row.RowIndex.ToString();
                GVBeneficiaryAll.Rows[int.Parse(firstColumnValue)].Visible = false;
            }
        }
        lblCount.Text = GVBeneficiaryAll.Rows.Count.ToString();
    }

}