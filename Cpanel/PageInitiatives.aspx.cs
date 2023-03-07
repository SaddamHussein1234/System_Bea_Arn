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

public partial class Cpanel_PageInitiatives : System.Web.UI.Page
{
    string IDUser, IDUniq;
    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
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
            bool A132, A133;
            A132 = Convert.ToBoolean(dtViewProfil.Rows[0]["A132"]);
            A133 = Convert.ToBoolean(dtViewProfil.Rows[0]["A133"]);
            if (A132 == false)
            {
                View.Visible = false;
            }
            if (A133 == false)
            {
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVInitiativesAll.Columns[0].Visible = false;
                GVInitiativesAll.Columns[6].Visible = false;
            }
            if (A132 == false && A133 == false)
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
            FGetData();
            txtName.Focus();
        }
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select Top(1) * from tbl_Initiatives Where ID_Uniq_ = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name_Initiatives_"].ToString();
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Active_"]);
                txtDetails.Text = dt.Rows[0]["Details_Initiatives_"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل مبادرة أو داعم";
                FGetInitiativesAll();
            }
            else
            {
                FGetInitiativesAll();
            }
        }
        else
        {
            FGetInitiativesAll();
        }
    }

    private void FGetInitiativesAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Initiatives] With(NoLock) Where [Name_Initiatives_] Like '%' + @0 + '%' And [Is_Hide_] = @1 And [Is_Delete] = @1 Order by [ID_Initiatives] Desc", txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVInitiativesAll.DataSource = dt;
                GVInitiativesAll.DataBind();
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageInitiatives.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVInitiativesAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVInitiativesAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Initiatives] SET [Is_Delete] = @Is_Delete WHERE [ID_Initiatives] = @ID_Initiatives";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID_Initiatives", Comp_ID);
                    cmd.Parameters.AddWithValue("@Is_Delete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            lblMessage.Text = "تم حذف البيانات بنجاح";
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            FGetInitiativesAll();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetInitiativesAll();
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "حفظ البيانات")
            {
                FCheckName();
            }
            else if (btnAdd.Text == "تعديل البيانات")
            {
                FEdit();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from tbl_Initiatives With(NoLock) Where Name_Initiatives_ = @0 And Is_Delete = @1", txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
        }
        else
        {
            FAdd();
        }
    }

    private void FAdd()
    {
        GetCookie();

        conn.Open();
        string sql = "INSERT INTO [dbo].[tbl_Initiatives] ([ID_Uniq_],[Name_Initiatives_],[Is_Active_],[Details_Initiatives_],[DateAdd_Initiatives_],[ID_Admin_],[Is_Delete]) VALUES (@ID_Uniq_,@Name_Initiatives_,@Is_Active_,@Details_Initiatives_,@DateAdd_Initiatives_,@ID_Admin_,@Is_Delete)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ID_Uniq_", Convert.ToString(Guid.NewGuid()));
        cmd.Parameters.AddWithValue("@Name_Initiatives_", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@Is_Active_", CBActive.Checked);
        cmd.Parameters.AddWithValue("@Details_Initiatives_", txtDetails.Text.Trim());
        cmd.Parameters.AddWithValue("@DateAdd_Initiatives_", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
        cmd.Parameters.AddWithValue("@ID_Admin_", Convert.ToInt32(IDUser));
        cmd.Parameters.AddWithValue("@Is_Delete", false);
        cmd.ExecuteScalar();
        conn.Close();
        lblMessage.Text = "تم إضافة القرية بنجاح";
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        FGetInitiativesAll();
        System.Threading.Thread.Sleep(100);
    }

    private void FEdit()
    {       
        conn.Open();
        string sql = "UPDATE [dbo].[tbl_Initiatives] SET [Name_Initiatives_] = @Name_Initiatives_ ,[Is_Active_] = @Is_Active_ ,[Details_Initiatives_] = @Details_Initiatives_ WHERE [ID_Uniq_] = @ID_Uniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ID_Uniq", Convert.ToString(Request.QueryString["ID"]));
        cmd.Parameters.AddWithValue("@Name_Initiatives_", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@Is_Active_", CBActive.Checked);
        cmd.Parameters.AddWithValue("@Details_Initiatives_", txtDetails.Text.Trim());
        cmd.ExecuteScalar();
        conn.Close();
        lblMessage.Text = "تم تعديل القرية بنجاح";
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        FGetInitiativesAll();
        System.Threading.Thread.Sleep(100);
    }

}