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

public partial class Cpanel_PageRe_beneficiaryByRaees : System.Web.UI.Page
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
            bool A98, A100;
            A98 = Convert.ToBoolean(dtViewProfil.Rows[0]["A98"]);
            A100 = Convert.ToBoolean(dtViewProfil.Rows[0]["A100"]);
            if (A100 == false)
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
            FArnQararQobolMustafeedByRaeesAlmaglis();
        }
    }

    private void FArnQararQobolMustafeedByRaeesAlmaglis()
    {
        try
        {
            ClassEadatMostafeed CEM = new ClassEadatMostafeed();
            CEM._IDUniq = txtSearch.Text.Trim();
            CEM._IsAllowModer = true;
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
        Response.Redirect("PageRe_beneficiaryByRaees.aspx");
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

        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        FAllowRaees();
    }

    private void FAllowRaees()
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
                    string sql = "UPDATE [dbo].[EadatMostafeed] SET [IsAllowRaees] = @IsAllowRaees WHERE NumberAlMostafeed = @NAlMostafeed";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NAlMostafeed", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsAllowRaees", true);
                    cmd.ExecuteScalar();
                    conn.Close();

                    ClassMosTafeed CM = new ClassMosTafeed();
                    DataTable dt = new DataTable();
                    dt = ClassDataAccess.GetData("SELECT [IDUniq] FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0", Comp_ID);
                    if (dt.Rows.Count > 0)
                    {
                        CM._IDUniq = dt.Rows[0]["IDUniq"].ToString();
                        CM._TypeMostafeed = "دائم";
                        CM.BArnRasAlEstemarahUpdateTypeMostafeed();
                    }
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
        FArnQararQobolMustafeedByRaeesAlmaglis();
        System.Threading.Thread.Sleep(500);
    }

}