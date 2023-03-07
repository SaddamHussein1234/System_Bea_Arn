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

public partial class Cpanel_PageAfieldVisitPendingApproval : System.Web.UI.Page
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
            bool A93;
            A93 = Convert.ToBoolean(dtViewProfil.Rows[0]["A93"]);
            if (A93 == false)
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
            FArnZeyarahMaydanyahPendingApprovalByModer();
        }
    }

    private void FArnZeyarahMaydanyahPendingApprovalByModer()
    {
        try
        {
            ClassZeyarahMaydanyah CZM = new ClassZeyarahMaydanyah();
            CZM._IDUniq = txtSearch.Text.Trim();
            CZM._IsModer = false;
            CZM._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CZM.BArnZeyarahMaydanyahPendingApprovalByModer();
            if (dt.Rows.Count > 0)
            {
                GVAfieldVisitPendingApproval.DataSource = dt;
                GVAfieldVisitPendingApproval.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAfieldVisitPendingApproval.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAfieldVisitPendingApproval.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAfieldVisitPendingApproval.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ZeyarahMaydanyah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FArnZeyarahMaydanyahPendingApprovalByModer();
    }
    
    protected void btnAllow_Click(object sender, EventArgs e)
    {
        FAllowManager();
    }

    private void FAllowManager()
    {
        try
        {
            GetCookie();
            foreach (GridViewRow row in GVAfieldVisitPendingApproval.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAfieldVisitPendingApproval.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ZeyarahMaydanyah] SET [AllowAlZeyarah] = @AllowAlZeyarah , [NotAllowAlZeyarah] = @NotAllowAlZeyarah , [AlAsBab] = @AlAsBab , [IDManage] = @IDManage , [StateView] = @StateView , [IsModer] = @IsModer , [IsRaeesMaglesAEdarah] = @IsRaeesMaglesAEdarah WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowAlZeyarah", true);
                    cmd.Parameters.AddWithValue("@NotAllowAlZeyarah", false);
                    cmd.Parameters.AddWithValue("@AlAsBab", string.Empty);
                    cmd.Parameters.AddWithValue("@IDManage", Convert.ToInt32(IDUser));
                    cmd.Parameters.AddWithValue("@StateView", true);
                    cmd.Parameters.AddWithValue("@IsModer", true);
                    cmd.Parameters.AddWithValue("@IsRaeesMaglesAEdarah", true);

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
    
    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        if (txtNotAllow.Text != string.Empty)
        {
            lblNotAllow.Visible = false;
            FNotAllowRaees();
        }
        else if (txtNotAllow.Text == string.Empty)
        {
            lblNotAllow.Visible = true;
            txtNotAllow.Focus();
        }
    }

    private void FNotAllowRaees()
    {
        try
        {
            GetCookie();
            foreach (GridViewRow row in GVAfieldVisitPendingApproval.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAfieldVisitPendingApproval.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ZeyarahMaydanyah] SET [AllowAlZeyarah] = @AllowAlZeyarah , [NotAllowAlZeyarah] = @NotAllowAlZeyarah , [AlAsBab] = @AlAsBab , [IDManage] = @IDManage , [StateView] = @StateView , [IsModer] = @IsModer , [IsRaeesMaglesAEdarah] = @IsRaeesMaglesAEdarah WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowAlZeyarah", false);
                    cmd.Parameters.AddWithValue("@NotAllowAlZeyarah", true);
                    cmd.Parameters.AddWithValue("@AlAsBab", txtNotAllow.Text.Trim());
                    cmd.Parameters.AddWithValue("@IDManage", Convert.ToInt32(IDUser));
                    cmd.Parameters.AddWithValue("@StateView", true);
                    cmd.Parameters.AddWithValue("@IsModer", true);
                    cmd.Parameters.AddWithValue("@IsRaeesMaglesAEdarah", true);
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