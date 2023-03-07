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

public partial class Cpanel_CPanelManageZakat_PageCategory : System.Web.UI.Page
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
            bool A134, A135;
            A134 = Convert.ToBoolean(dtViewProfil.Rows[0]["A134"]);
            A135 = Convert.ToBoolean(dtViewProfil.Rows[0]["A135"]);
            if (A134 == false)
            {
                View.Visible = false;
            }
            if (A135 == false)
            {
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVInitiativesAll.Columns[0].Visible = false;
                GVInitiativesAll.Columns[6].Visible = false;
            }
            if (A134 == false && A135 == false)
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
            dt = ClassDataAccess.GetData("Select Top(1) * from tbl_Category_Zakat Where ID_Uniq_ = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name_Category_"].ToString();
                DLQuantity.SelectedValue = dt.Rows[0]["_Quantity_"].ToString();
                txt_Price.Text = Convert.ToInt32(dt.Rows[0]["Total_Amount_"]).ToString();
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Active_"]);
                txtDetails.Text = dt.Rows[0]["Details_Category_"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل صنف للنظام";
                FGetCategoryAll();
            }
            else
                FGetCategoryAll();
        }
        else
            FGetCategoryAll();
    }

    private void FGetCategoryAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Category_Zakat] With(NoLock) Where [Name_Category_] Like '%' + @0 + '%' And [Is_Delete] = @1 Order by [_Quantity_]", txtSearch.Text.Trim(), Convert.ToString(false));
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

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVInitiativesAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVInitiativesAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Category_Zakat] SET [Is_Delete] = @Is_Delete WHERE [ID_Category] = @ID_Category";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID_Category", Comp_ID);
                    cmd.Parameters.AddWithValue("@Is_Delete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            lblMessage.Text = "تم حذف البيانات بنجاح";
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            FGetCategoryAll();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageCategory.aspx");
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
        dt = ClassDataAccess.GetData("Select Top(1) * from tbl_Category_Zakat With(NoLock) Where [Name_Category_] = @0 And [_Quantity_] = @1 And Is_Delete = @2", txtName.Text.Trim(), DLQuantity.SelectedValue, Convert.ToString(false));
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
        string sql = "INSERT INTO [dbo].[tbl_Category_Zakat] ([ID_Uniq_],[Name_Category_],[_Quantity_],[Total_Amount_],[Is_Active_],[Details_Category_],[DateAdd_Category_],[ID_Admin_],[Is_Delete]) VALUES (@ID_Uniq_,@Name_Category_,@Quantity,@Total_Amount_,@Is_Active_,@Details_Category_,@DateAdd_Category_,@ID_Admin_, @Is_Delete )";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ID_Uniq_", Convert.ToString(Guid.NewGuid()));
        cmd.Parameters.AddWithValue("@Name_Category_", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@Quantity", DLQuantity.SelectedValue);
        cmd.Parameters.AddWithValue("@Total_Amount_", txt_Price.Text.Trim());
        cmd.Parameters.AddWithValue("@Is_Active_", CBActive.Checked);
        cmd.Parameters.AddWithValue("@Details_Category_", txtDetails.Text.Trim());
        cmd.Parameters.AddWithValue("@DateAdd_Category_", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
        cmd.Parameters.AddWithValue("@ID_Admin_", Convert.ToInt32(IDUser));
        cmd.Parameters.AddWithValue("@Is_Delete", false);
        cmd.ExecuteScalar();
        conn.Close();
        lblMessage.Text = "تم إضافة القرية بنجاح";
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        FGetCategoryAll();
        txtName.Text = string.Empty;
        DLQuantity.SelectedValue = null;
        txt_Price.Text = string.Empty;
        txtDetails.Text = string.Empty;
        System.Threading.Thread.Sleep(100);
    }

    private void FEdit()
    {
        conn.Open();
        string sql = "UPDATE [dbo].[tbl_Category_Zakat] SET [Name_Category_] = @Name_Category_, [_Quantity_] = @Quantity,[Total_Amount_] = @Total_Amount_,[Is_Active_] = @Is_Active_,[Details_Category_] = @Details_Category_ WHERE [ID_Uniq_] = @ID_Uniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ID_Uniq", Convert.ToString(Request.QueryString["ID"]));
        cmd.Parameters.AddWithValue("@Name_Category_", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@Quantity", DLQuantity.SelectedValue);
        cmd.Parameters.AddWithValue("@Total_Amount_", txt_Price.Text.Trim());
        cmd.Parameters.AddWithValue("@Is_Active_", CBActive.Checked);
        cmd.Parameters.AddWithValue("@Details_Category_", txtDetails.Text.Trim());
        cmd.ExecuteScalar();
        conn.Close();
        lblMessage.Text = "تم تعديل القرية بنجاح";
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        FGetCategoryAll();
        System.Threading.Thread.Sleep(100);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetCategoryAll();
    }

}