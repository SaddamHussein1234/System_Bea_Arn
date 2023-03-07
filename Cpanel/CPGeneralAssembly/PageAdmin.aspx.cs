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

public partial class Cpanel_CPGeneralAssembly_PageAdmin : System.Web.UI.Page
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
            bool A143, A144;
            A143 = Convert.ToBoolean(dtViewProfil.Rows[0]["A143"]);
            A144 = Convert.ToBoolean(dtViewProfil.Rows[0]["A143"]);
            if (A143 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A144 == false)
            {
                IDAdd.Visible = false;
                btnActive.Visible = false;
                btnUnActive.Visible = false;
                GVAdmin.Columns[0].Visible = false;
                GVAdmin.Columns[9].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAdmin(4, false, false, true);
            txtTitle.Text = "قائمة أعضاء الجمعية العمومية";
        }
    }

    private void FGetAdmin(int XID, bool IsAdminInEdarah, bool IsBaheth, bool IsA1)
    {
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._ID_Item = XID;
        CAA._FirstName = txtSearch.Text.Trim();
        CAA._IsDelete = false;
        CAA._IsAdminInEdarah = IsAdminInEdarah;
        CAA._IsBaheth = IsBaheth;
        CAA._A1 = IsA1;
        DataTable dt = new DataTable();
        dt = CAA.BArnAdminByAll();
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

    private void FCheckRPL()
    {
        if (Convert.ToInt32(rblCheck.SelectedValue) == 0)
        {
            FGetAdmin(0, false, false, false);
            txtTitle.Text = "قائمة جميع مستخدمين النظام";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
        {
            FGetAdmin(1, true, false, false);
            txtTitle.Text = "قائمة أعضاء مجلس الإدارة";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
        {
            FGetAdmin(2, false, true, false);
            txtTitle.Text = "قائمة الباحثين";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 3)
        {
            FGetAdmin(3, false, false, false);
            txtTitle.Text = "قائمة المستخدمين";
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 4)
        {
            FGetAdmin(4, false, false, true);
            txtTitle.Text = "قائمة أعضاء الجمعية العمومية";
        }
    }

    public static string FCheckManageF(bool IDP)
    {
        string VaComp = "";
        if (IDP)
            VaComp = "<br /><span style='border-radius:6px; font-size:11px; background-color:Green; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> مدير النظام </span>";
        return VaComp.ToString();
    }

    public static string FCheckOldMaglisF(bool IDP)
    {
        string VaComp = "";
        if (IDP)
            VaComp = "<br /><span style='border-radius:6px; font-size:11px; background-color:yellowgreen; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> عضو مجلس قديم </span>";
        return VaComp.ToString();
    }

    public static string FCheckIsBahethF(int ID, string IDUniq, bool IDP)
    {
        string VaComp = "";
        if (IDP)
            VaComp = "<br /><span style='border-radius:6px; font-size:11px; background-color:#0282e2; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> <a href='PageMultiLinking.aspx?ID=" + ID.ToString() + "&IDUniq=" + IDUniq + "' style='color:#FFF'>ربط القُرى بالباحث</a> </span>";
        return VaComp.ToString();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnActive_Click(object sender, EventArgs e)
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
                    string sql = "UPDATE [dbo].[tbl_Admin] SET [IsBlock] = @IsBlock WHERE ID_Item = @ID_Item And IsSuperAdmin = @IsSuper";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID_Item", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsBlock", true);
                    cmd.Parameters.AddWithValue("@IsSuper", false);
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

    protected void btnUnActive_Click(object sender, EventArgs e)
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
                    string sql = "UPDATE [dbo].[tbl_Admin] SET [IsBlock] = @IsBlock WHERE ID_Item = @ID_Item And IsSuperAdmin = @IsSuper";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID_Item", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsBlock", false);
                    cmd.Parameters.AddWithValue("@IsSuper", false);
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
        FCheckRPL();
        System.Threading.Thread.Sleep(500);
    }

    protected void rblCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        FCheckRPL();
    }

    public string FCheckGeneral_AssmplyF(string XID, bool XCheck)
    {
        string VaComp = "";
        try
        {
            if (XCheck)
            {
                DataTable dt = new DataTable();
                dt = ClassDataAccess.GetData("SELECT TOP (1) [ID_Admin_Account_],[Is_Delete_] FROM [dbo].[tbl_General_Assmply] With(NoLock) Where [ID_Admin_Account_] = @0 And [Is_Delete_] = @1",
                    XID, Convert.ToString(false));
                if (dt.Rows.Count == 0)
                {
                    VaComp = "<br /><a target='_blank' href='../CPGeneralAssembly/PageGeneralAssemblyAdd.aspx?ID=" +
                        XID + "&IDUniq=" + Convert.ToString(Guid.NewGuid()) + "'><span style='border-radius:6px; font-size:11px;" +
                        " background-color:#0282e2; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> إضافة ملف العضوية </span></a>";
                }
            }
        }
        catch (Exception)
        {

        }
        return VaComp.ToString();
    }


}