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

public partial class Cpanel_PageVisitReport : System.Web.UI.Page
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
            bool A50, A95;
            A50 = Convert.ToBoolean(dtViewProfil.Rows[0]["A50"]);
            A95 = Convert.ToBoolean(dtViewProfil.Rows[0]["A95"]);
            if (A50 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A95 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVVisitReport.Columns[0].Visible = false;
            }
            else
            {
                IDAdd.Visible = true;
                btnDelete1.Visible = true;
                GVVisitReport.Columns[0].Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
        }
    }

    private void FArnReportAlZyaratByAll()
    {
        try
        {
            ClassReportAlZyarat CRA = new ClassReportAlZyarat();
            CRA.IDCheck = "0";
            CRA._AlQaryah = 0;
            CRA._IDUniq = txtSearch.Text.Trim();
            CRA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CRA.BArnReportAlZyaratByAll();
            if (dt.Rows.Count > 0)
            {
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
            return;
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CheckAccountAdmin();
        GVVisitReport.Columns[7].Visible = true;
        GVVisitReport.UseAccessibleHeader = false;
        FArnReportAlZyaratByAll();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageVisitReport.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GVVisitReport.Columns[0].Visible = false;
        GVVisitReport.Columns[7].Visible = false;

        GVVisitReport.UseAccessibleHeader = true;
        GVVisitReport.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlData;
        //if (GVVisitReport.Rows.Count > 6)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        //}
        //else if (GVVisitReport.Rows.Count <= 6)
        //{
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        //}
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
            GVVisitReport.Columns[7].Visible = true;
            GVVisitReport.UseAccessibleHeader = false;
            FArnReportAlZyaratByAll();
            CheckAccountAdmin();
        }
        catch (Exception)
        {
            return;
        }
    }

}