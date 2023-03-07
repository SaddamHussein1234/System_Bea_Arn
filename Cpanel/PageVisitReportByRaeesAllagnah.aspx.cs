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

public partial class Cpanel_PageVisitReportByRaeesAllagnah : System.Web.UI.Page
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
            bool A97, A95;
            A97 = Convert.ToBoolean(dtViewProfil.Rows[0]["A97"]);
            A95 = Convert.ToBoolean(dtViewProfil.Rows[0]["A95"]);
            if (A97 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A95 == false)
            {
                btnDelete1.Visible = false;
                GVVisitReport.Columns[0].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnReportAlZyaratByRaesLagnatAlBahth();
        }
    }

    private void FArnReportAlZyaratByRaesLagnatAlBahth()
    {
        try
        {
            ClassReportAlZyarat CRA = new ClassReportAlZyarat();
            CRA._IDUniq = txtSearch.Text.Trim();
            CRA._IsModerAllow = true;
            CRA._IsRaesLagnatAlBahthAllow = false;
            CRA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CRA.BArnReportAlZyaratByModer();
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة عرض تقارير الزيارات التي تحتاج إلى إطلاع رئيس اللجنة";
                GVVisitReport.DataSource = dt;
                GVVisitReport.DataBind();
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
        }
        catch (Exception)
        {

        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageVisitReportByRaeesAllagnah.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVVisitReport.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVVisitReport.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ReportAlZyarat] SET [IsDelete] = @IsDelete WHERE NumberReport = @NumberReport";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NumberReport", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    System.Threading.Thread.Sleep(200);
                    string sqlDevice = "UPDATE [dbo].[ReportAlZyaratElectricalAppliances] SET [IsDelete] = @IsDelete WHERE IDReport = @IDReport";
                    SqlCommand cmdDevice = new SqlCommand(sqlDevice, conn);
                    cmdDevice.Parameters.AddWithValue("@IDReport", Comp_ID);
                    cmdDevice.Parameters.AddWithValue("@IsDelete", true);
                    cmdDevice.ExecuteScalar();
                    conn.Close();
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {

        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        FAllowRaesLagnatAlBahth();
    }

    private void FAllowRaesLagnatAlBahth()
    {
        try
        {
            foreach (GridViewRow row in GVVisitReport.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVVisitReport.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ReportAlZyarat] SET [IsRaesLagnatAlBahthAllow] = @IsRaesLagnatAlBahthAllow WHERE NumberReport = @NumberReport";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NumberReport", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsRaesLagnatAlBahthAllow", true);
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
        FArnReportAlZyaratByRaesLagnatAlBahth();
    }


}