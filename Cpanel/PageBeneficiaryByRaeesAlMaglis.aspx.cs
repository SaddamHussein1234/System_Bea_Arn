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

public partial class Cpanel_PageBeneficiaryByRaeesAlMaglis : System.Web.UI.Page
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
            bool A123, A75;
            A123 = Convert.ToBoolean(dtViewProfil.Rows[0]["A123"]);
            A75 = Convert.ToBoolean(dtViewProfil.Rows[0]["A75"]);
            if (A123 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A75 == false)
            {

                btnDelete1.Visible = false;
                GVBeneficiaryAll.Columns[0].Visible = false;
                GVBeneficiaryAll.Columns[13].Visible = false;
            }
            if (A123 == false && A75 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetMostafeedByRaeesMaglisAlEdarah();
        }
    }

    private void FGetMostafeedByRaeesMaglisAlEdarah()
    {
        GVBeneficiaryAll.Columns[0].Visible = true;
        GVBeneficiaryAll.Columns[11].Visible = true;
        GVBeneficiaryAll.Columns[13].Visible = true;
        GVBeneficiaryAll.UseAccessibleHeader = false;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And IsDelete = @1 And IsAllowRaeesLagnatAlBahth_ = @2 And IsAllowModer_ = @2 And IsRaeesMaglisAlEdarah_ = @1", txtSearch.Text.Trim(), Convert.ToString(false), Convert.ToString(true));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            txtTitle.Text = " مستفيدين يحتاجون إلى موافقة رئيس مجلس الإدارة ";
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
        txtTitle.Focus();
        CheckAccountAdmin();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryByRaeesAlMaglis.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GVBeneficiaryAll.UseAccessibleHeader = true;
        GVBeneficiaryAll.HeaderRow.TableSection = TableRowSection.TableHeader;

        GVBeneficiaryAll.Columns[0].Visible = false;
        GVBeneficiaryAll.Columns[11].Visible = false;
        GVBeneficiaryAll.Columns[13].Visible = false;

        Session["footable1"] = pnlData;
        //if (GVBeneficiaryAll.Rows.Count > 12)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        //}
        //else if (GVBeneficiaryAll.Rows.Count <= 12)
        //{
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        //}
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        FAllowRaeesLagnatAlBahth();
    }

    private void FAllowRaeesLagnatAlBahth()
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
                    string sql = "UPDATE [dbo].[RasAlEstemarah] SET [TypeMostafeed] = @TypeMostafeed , [IsRaeesMaglisAlEdarah_] = @IsRaeesMaglisAlEdarah_ WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@TypeMostafeed", "دائم");
                    cmd.Parameters.AddWithValue("@IsRaeesMaglisAlEdarah_", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetMostafeedByRaeesMaglisAlEdarah();
    }

}