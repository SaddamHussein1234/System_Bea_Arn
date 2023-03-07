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

public partial class Cpanel_CPanelSetting_PageGroup : System.Web.UI.Page
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
            bool A5, A6;
            A5 = Convert.ToBoolean(dtViewProfil.Rows[0]["A5"]);
            A6 = Convert.ToBoolean(dtViewProfil.Rows[0]["A6"]);
            if (A5 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A6 == false)
            {
                IDAdd.Visible = false;
                btnActive.Visible = false;
                btnUnActive.Visible = false;
                GVGroupArn.Columns[0].Visible = false;
                GVGroupArn.Columns[6].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetGroup();
        }
    }

    private void FGetGroup()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Group_Arn] With(NoLock) Where NameGroup Like '%' + @0 + '%' And IsSuperAdminGroup = @1 And IsDeleteGroup = @2 Order by IDGroup Desc", txtSearch.Text.Trim(), Convert.ToString(false), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVGroupArn.DataSource = dt;
            GVGroupArn.DataBind();
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

    public float FCountAdmin(int X)
    {
        float XID = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT COUNT(*) As 'Count' FROM [dbo].[tbl_Admin] With(NoLock) Where IDGroup = @0 And IsDelete = @1", Convert.ToString(X), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XID = Convert.ToInt64(dt.Rows[0]["Count"]);
        }
        return XID;
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageGroup.aspx");
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVGroupArn.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVGroupArn.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Group_Arn] SET [IsActiveGroup] = @IsActiveGroup WHERE IDGroup = @IDGroup";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDGroup", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsActiveGroup", true);
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

    protected void btnUnActive_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVGroupArn.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVGroupArn.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Group_Arn] SET [IsActiveGroup] = @IsActiveGroup WHERE IDGroup = @IDGroup";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDGroup", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsActiveGroup", false);
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
        FGetGroup();
        System.Threading.Thread.Sleep(100);
    }

}