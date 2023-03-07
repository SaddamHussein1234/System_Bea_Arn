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

public partial class CResearchers_CPVillage_PageTecisionToExclude : System.Web.UI.Page
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
            bool A45, A90;
            A45 = Convert.ToBoolean(dtViewProfil.Rows[0]["A45"]);
            A90 = Convert.ToBoolean(dtViewProfil.Rows[0]["A90"]);
            if (A45 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A90 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVTecisionToExclude.Columns[0].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAlBaheth();
            pnlSelect.Visible = true;
            FGetAlQariah();
            HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
            string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";
            DLAlQriah.SelectedValue = IDVillage;
        }
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQriah.Items.Clear();
            DLAlQriah.Items.Add("");
            DLAlQriah.AppendDataBoundItems = true;
            DLAlQriah.DataValueField = "IDQariah";
            DLAlQriah.DataTextField = "AlQriah";
            DLAlQriah.DataSource = dt;
            DLAlQriah.DataBind();
        }
    }

    private void FArnQararQobolMustafeedByAll()
    {
        try
        {
            DLAlBaheth.Visible = true;
            DLModerAlGmeiah.Visible = true;
            DLRaeesMaglesAlEdarah.Visible = true;
            DLRaeesLagnatAlBahath.Visible = true;
            lblAlBaheth.Visible = false;
            lblModerAlGmeiah.Visible = false;
            lblRaeesMaglesAlEdarah.Visible = false;
            lblRaeesLagnatAlBahath.Visible = false;
            GVTecisionToExclude.UseAccessibleHeader = false;
            ClassQararQobol CQQ = new ClassQararQobol();
            CQQ._IDCheck = 1;
            CQQ._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
            CQQ._IDUniq = txtSearch.Text.Trim();
            CQQ._IsQobol = false;
            CQQ._IsEstepaad = true;
            CQQ._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CQQ.BArnQararQobolMustafeedByAll();
            if (dt.Rows.Count > 0)
            {
                GVTecisionToExclude.DataSource = dt;
                GVTecisionToExclude.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Focus();
                GVTecisionToExclude.Columns[0].Visible = true;
                GVTecisionToExclude.Columns[12].Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
            CheckAccountAdmin();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetAlBaheth()
    {
        //DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        //if (dt.Rows.Count > 0)
        //{
        //    //DLAlBaheth.Items.Clear();
        //    //DLAlBaheth.Items.Add("");
        //    //DLAlBaheth.AppendDataBoundItems = true;
        //    DLAlBaheth.DataValueField = "ID_Item";
        //    DLAlBaheth.DataTextField = "FirstName";
        //    DLAlBaheth.DataSource = dt;
        //    DLAlBaheth.DataBind();
        //}
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageTecisionToExclude.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
        DLAlBaheth.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        DLRaeesLagnatAlBahath.Visible = false;
        lblAlBaheth.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;
        lblRaeesLagnatAlBahath.Visible = true;
        GVTecisionToExclude.Columns[0].Visible = false;
        GVTecisionToExclude.Columns[12].Visible = false;
        GVTecisionToExclude.UseAccessibleHeader = true;
        GVTecisionToExclude.HeaderRow.TableSection = TableRowSection.TableHeader;
        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVTecisionToExclude.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVTecisionToExclude.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();

                    DataTable dtGetData = new DataTable();
                    dtGetData = ClassDataAccess.GetData("SELECT * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where IDItem = @0", Comp_ID);
                    if (dtGetData.Rows.Count > 0)
                    {
                        string sql = "UPDATE [dbo].[QararQobolMustafeed] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                        cmd.Parameters.AddWithValue("@IsDelete", true);
                        cmd.ExecuteScalar();

                        string sql_ = "UPDATE [dbo].[QararQobolMustafeedAdmin] SET [IsDelete] = @IsDelete WHERE NumberMostafeed = @NumberMostafeed And NumberQarsr = @NumberQarar And NumberReport = @NumberReport";
                        SqlCommand cmd_ = new SqlCommand(sql_, conn);
                        cmd_.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(dtGetData.Rows[0]["NumberMostafeed"]));
                        cmd_.Parameters.AddWithValue("@NumberQarar", Convert.ToInt32(dtGetData.Rows[0]["NumberQarar"]));
                        cmd_.Parameters.AddWithValue("@NumberReport", Convert.ToInt32(dtGetData.Rows[0]["NumberReport"]));
                        cmd_.Parameters.AddWithValue("@IsDelete", true);
                        cmd_.ExecuteScalar();
                    }

                    conn.Close();
                }
            }
            FArnQararQobolMustafeedByAll();
        }
        catch (Exception)
        {
            return;
        }
    }

    int tempcounter = 0;
    protected void GVTecisionToExclude_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    protected void DLAlBaheth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetCookie();
        DataTable dtcheck = new DataTable();
        dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriah.SelectedValue, Convert.ToString(false));
        if (dtcheck.Rows.Count > 0)
        {
            FArnQararQobolMustafeedByAll();
        }
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
        System.Threading.Thread.Sleep(200);
    }

}