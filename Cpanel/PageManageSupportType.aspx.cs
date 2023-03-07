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

public partial class Cpanel_PageManageSupportType : System.Web.UI.Page
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
            bool A36, A82;
            A36 = Convert.ToBoolean(dtViewProfil.Rows[0]["A36"]);
            A82 = Convert.ToBoolean(dtViewProfil.Rows[0]["A82"]);
            if (A36 == false)
            {
                view.Visible = false;
            }
            if (A82 == false)
            {
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVSupportTypeAll.Columns[0].Visible = false;
                GVSupportTypeAll.Columns[9].Visible = false;
            }
            if (A36 == false && A82 == false)
            {
                Response.Redirect("LogOut.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            ClassQuaem.FGetAffiliation_Progect(DLAffiliation_Progect);
            FGetData();
            txtName.Focus();
        }
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select Top(1) [TypeAlDam],[ID_Affiliation_Progect],[TypeCart],[TypeCartPart] from Quaem With(NoLock) Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["TypeAlDam"].ToString();
                DLAffiliation_Progect.SelectedValue = dt.Rows[0]["ID_Affiliation_Progect"].ToString();
                txtTypeCart.Text = dt.Rows[0]["TypeCart"].ToString();
                txtTypeCartPart.Text = dt.Rows[0]["TypeCartPart"].ToString();
                btnAdd.Text = "تعديل البيانات";
                FGetSupportTypeAll();
            }
            else
                FGetSupportTypeAll();
        }
        else
            FGetSupportTypeAll();
    }

    private void FGetSupportTypeAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And TypeAlDam Like '%' + @1 + '%' And IsDeleteTypeAlDam = @2 Order by IDItem Desc", string.Empty, txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVSupportTypeAll.DataSource = dt;
                GVSupportTypeAll.DataBind();
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
            foreach (GridViewRow row in GVSupportTypeAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVSupportTypeAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[Quaem] SET [IsDeleteTypeAlDam] = @IsDeleteTypeAlDam WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDeleteTypeAlDam", true);
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageSupportType.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text != string.Empty)
            {
                if (btnAdd.Text == "حفظ البيانات")
                    FCheckName();
                else if (btnAdd.Text == "تعديل البيانات")
                    FQariahEdit();
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
        dt = ClassDataAccess.GetData("Select Top(1000) * from Quaem With(NoLock) Where TypeAlDam = @0 And IsDeleteTypeAlDam = @1", txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة نوع الدعم مسبقاً";
            return;
        }
        else
            FQariahAdd();
    }

    private void FQariahAdd()
    {
        GetCookie();
        ClassQuaem CQ = new ClassQuaem()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _AlQriah = string.Empty,
            _HalatMostafeed = string.Empty,
            _TypeAlMaskan = string.Empty,
            _AlDakhlAlShahryWaMasdarah = string.Empty,
            _Gender = string.Empty,
            _HalatAlMaskan = string.Empty,
            _AlBaheth = string.Empty,
            _TypeAlDam = txtName.Text.Trim(),
            _AlQarabah = string.Empty,
            _AmeenAlSondoq = string.Empty,
            _RaeesMaglesAlEdarah = string.Empty,
            _AlModer = string.Empty,
            _AlAmeenAlAam = string.Empty,
            _AlMohaseb = string.Empty,
            _RaeesAlShoonAlMaliah = string.Empty,
            _SelatAlQarabah = string.Empty,
            _DateAdd = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IsDelete = false,
            _IDAdmin = Convert.ToInt32(IDUser),
            ID_Affiliation_Progect = Convert.ToInt32(DLAffiliation_Progect.SelectedValue),
            TypeCart = txtTypeCart.Text.Trim(),
            TypeCartPart = txtTypeCartPart.Text.Trim()
        };
        CQ.BArnQuaemAdd();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم إضافة المشروع بنجاح";
        FGetSupportTypeAll();
    }

    private void FQariahEdit()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[Quaem] SET [TypeAlDam] = @TypeAlDam , [ID_Affiliation_Progect] = @ID_Affiliation_Progect , [TypeCart] = @TypeCart , [TypeCartPart] = @TypeCartPart WHERE IDUniq = @IDUniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(Request.QueryString["ID"]));
        cmd.Parameters.AddWithValue("@TypeAlDam", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@ID_Affiliation_Progect", DLAffiliation_Progect.SelectedValue);
        cmd.Parameters.AddWithValue("@TypeCart", txtTypeCart.Text.Trim());
        cmd.Parameters.AddWithValue("@TypeCartPart", txtTypeCartPart.Text.Trim());
        cmd.ExecuteScalar();
        conn.Close();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم تعديل المشروع بنجاح";
        FGetSupportTypeAll();
        System.Threading.Thread.Sleep(100);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetSupportTypeAll();
    }

}