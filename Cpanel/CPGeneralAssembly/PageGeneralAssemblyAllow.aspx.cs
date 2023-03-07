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

public partial class Cpanel_CPGeneralAssembly_PageGeneralAssemblyAllow : System.Web.UI.Page
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
            Response.Redirect("LogOut.aspx");
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
            bool A144,A145;
            A144 = Convert.ToBoolean(dtViewProfil.Rows[0]["A144"]);
            A145 = Convert.ToBoolean(dtViewProfil.Rows[0]["A145"]);
            if (A144 == false)
                btnDelete1.Visible = false;
            if (A145 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetGeneral_Assmply();
        }
    }

    private void FGetGeneral_Assmply()
    {
        try
        {
            GVAdmin.Columns[0].Visible = true;
            GVAdmin.Columns[11].Visible = true;
            GVAdmin.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT TOP (1000) TGA.*,TA.FirstName,TA.FirstName,[Email],[PhoneNumber],[A3] FROM [dbo].[tbl_General_Assmply] TGA With(NoLock) Inner Join tbl_Admin TA on TA.ID_Item = TGA.ID_Admin_Account_ Where ([FirstName] Like '%' + @0 + '%') And [Is_Allow_] = @1 And [Is_Delete_] = @1 Order by [Number_Rigstry_]",
                txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVAdmin.DataSource = dt;
                GVAdmin.DataBind();
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
            return;
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAdmin.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAdmin.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_General_Assmply] SET [Is_Delete_] = @Is_Delete WHERE ID_Item_ = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@Is_Delete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetGeneral_Assmply();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetGeneral_Assmply();
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAdmin.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAdmin.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_General_Assmply] SET [Is_Allow_] = @Is_Allow , [Is_Active_] = @Is_Active WHERE ID_Item_ = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@Is_Allow", true);
                    cmd.Parameters.AddWithValue("@Is_Active", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetGeneral_Assmply();
        }
        catch (Exception)
        {
            return;
        }
    }

}