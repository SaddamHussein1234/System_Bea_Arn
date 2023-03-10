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

public partial class Cpanel_PageTecisionToExcludeAllow : System.Web.UI.Page
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
            bool A91;
            A91 = Convert.ToBoolean(dtViewProfil.Rows[0]["A91"]);
            if (A91 == false)
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
            FArnQararQobolMustafeedByAllow();
        }
    }

    private void FArnQararQobolMustafeedByAllow()
    {
        //try
        //{
            GetCookie();
            ClassQararQobolAdmin CQQA = new ClassQararQobolAdmin();
            CQQA._IDUniq = txtSearch.Text.Trim();
            CQQA._IDAdmin = Convert.ToInt32(IDUser);
            CQQA._AdminAllow = false;
            CQQA._IsQobol = false;
            CQQA._IsEstepaad = true;
            CQQA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CQQA.BArnQararQobolMustafeedByAllow();
            if (dt.Rows.Count > 0)
            {
                GVTecisionToExcludeAllow.DataSource = dt;
                GVTecisionToExcludeAllow.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                txtTitle.Focus();
                //GVTecisionToExcludeAllow.Columns[11].Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        //}
        //catch (Exception)
        //{
        //
        //}
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageTecisionToExcludeAllow.aspx");
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        FAllowQarar();
    }

    private void FAllowQarar()
    {
        try
        {
            GetCookie();
            foreach (GridViewRow row in GVTecisionToExcludeAllow.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVTecisionToExcludeAllow.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[QararQobolMustafeedAdmin] SET [AdminAllow] = @AdminAllow WHERE IDIteam = @IDIteam And IDAdmin = @IDAdmin ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDIteam", Comp_ID);
                    cmd.Parameters.AddWithValue("@IDAdmin", Convert.ToInt32(IDUser));
                    cmd.Parameters.AddWithValue("@AdminAllow", true);
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
        FArnQararQobolMustafeedByAllow();
        System.Threading.Thread.Sleep(500);
    }

}