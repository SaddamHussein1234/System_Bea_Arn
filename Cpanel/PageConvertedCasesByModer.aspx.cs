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

public partial class Cpanel_PageConvertedCasesByModer : System.Web.UI.Page
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
            bool A104 , A105;
            A104 = Convert.ToBoolean(dtViewProfil.Rows[0]["A104"]);
            A105 = Convert.ToBoolean(dtViewProfil.Rows[0]["A105"]);
            if (A104 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVConvertedCases.Columns[0].Visible = false;
            }
            if (A105 == false)
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
            FArnTahweelAlHalahByModer();
        }
    }

    private void FArnTahweelAlHalahByModer()
    {
        try
        {
            ClassTahweelAlHalah CEM = new ClassTahweelAlHalah();
            CEM._IDUniq = txtSearch.Text.Trim();
            CEM._IsAllowModer = false;
            CEM._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CEM.BArnTahweelAlHalahByModer();
            if (dt.Rows.Count > 0)
            {
                GVConvertedCases.DataSource = dt;
                GVConvertedCases.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                txtTitle.Focus();
                GVConvertedCases.Columns[10].Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageConvertedCasesByModer.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVConvertedCases.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVConvertedCases.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[TahweelAlHalah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        FAllowModer();
    }

    private void FAllowModer()
    {
        try
        {
            foreach (GridViewRow row in GVConvertedCases.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVConvertedCases.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[TahweelAlHalah] SET [AllowAlhalah] = @AllowAlhalah , [BlockAlhalah] = @BlockAlhalah , [AlAsbaab] = @AlAsbaab , [IsAllowModer] = @IsAllowModer WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowAlhalah", true);
                    cmd.Parameters.AddWithValue("@BlockAlhalah", false);
                    cmd.Parameters.AddWithValue("@IsAllowModer", true);
                    cmd.Parameters.AddWithValue("@AlAsbaab", string.Empty);
                    cmd.ExecuteScalar();
                    conn.Close();

                    ClassMosTafeed CM = new ClassMosTafeed();
                    DataTable dt = new DataTable();
                    dt = ClassDataAccess.GetData("SELECT Top(1) [IDItem],[NumberMostafeed],[HalatAlmostafeedAfter] FROM [dbo].[TahweelAlHalah] Where IDItem = @0", Convert.ToString(GVConvertedCases.DataKeys[row.RowIndex].Value));
                    if (dt.Rows.Count > 0)
                    {
                        SqlConnection conn2 = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                        conn2.Open();
                        string sql2 = "UPDATE [dbo].[RasAlEstemarah] SET [HalafAlMosTafeed] = @HalafAlMosTafeed WHERE NumberMostafeed = @NumberMostafeed";
                        SqlCommand cmd2 = new SqlCommand(sql2, conn2);
                        cmd2.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                        cmd2.Parameters.AddWithValue("@HalafAlMosTafeed", Convert.ToInt32(dt.Rows[0]["HalatAlmostafeedAfter"]));
                        cmd2.ExecuteScalar();
                        conn2.Close();
                    }
                }
            }
            System.Threading.Thread.Sleep(200);
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FArnTahweelAlHalahByModer();
        System.Threading.Thread.Sleep(500);
    }
    
    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        if (txtNotAllow.Text != string.Empty)
        {
            lblNotAllow.Visible = false;
            FNotAllowModer();
        }
        else if (txtNotAllow.Text == string.Empty)
        {
            lblNotAllow.Visible = true;
            txtNotAllow.Focus();
        }
    }

    private void FNotAllowModer()
    {
        try
        {
            foreach (GridViewRow row in GVConvertedCases.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVConvertedCases.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[TahweelAlHalah] SET [AllowAlhalah] = @AllowAlhalah , [BlockAlhalah] = @BlockAlhalah , [AlAsbaab] = @AlAsbaab , [IsAllowModer] = @IsAllowModer WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowAlhalah", false);
                    cmd.Parameters.AddWithValue("@BlockAlhalah", true);
                    cmd.Parameters.AddWithValue("@IsAllowModer", true);
                    cmd.Parameters.AddWithValue("@AlAsbaab", txtNotAllow.Text.Trim());
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

}