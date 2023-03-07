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

public partial class CResearchers_CPVillage_PageRe_beneficiaryByModer : System.Web.UI.Page
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
            bool A98, A99;
            A98 = Convert.ToBoolean(dtViewProfil.Rows[0]["A98"]);
            A99 = Convert.ToBoolean(dtViewProfil.Rows[0]["A99"]);
            if (A99 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A98 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVRe_beneficiary.Columns[0].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnQararQobolMustafeedByModer();
        }
    }

    private void FArnQararQobolMustafeedByModer()
    {
        try
        {
            ClassEadatMostafeed CEM = new ClassEadatMostafeed();
            CEM._IDUniq = txtSearch.Text.Trim();
            CEM._IsAllowModer = false;
            CEM._IsAllowRaees = false;
            CEM._IsEaadat = true;
            CEM._IsEstbaad = false;
            CEM._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CEM.BArnEadatMostafeedByModer();
            if (dt.Rows.Count > 0)
            {
                GVRe_beneficiary.DataSource = dt;
                GVRe_beneficiary.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                txtTitle.Focus();
                GVRe_beneficiary.Columns[9].Visible = true;
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
        Response.Redirect("PageRe_beneficiaryByModer.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVRe_beneficiary.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVRe_beneficiary.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[EadatMostafeed] SET [IsDelete] = @IsDelete WHERE IDIteam = @IDIteam";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDIteam", Comp_ID);
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
            foreach (GridViewRow row in GVRe_beneficiary.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVRe_beneficiary.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[EadatMostafeed] SET [IsAllowModer] = @IsAllowModer WHERE IDIteam = @IDIteam";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDIteam", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsAllowModer", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FArnQararQobolMustafeedByModer();
        System.Threading.Thread.Sleep(500);
    }

}