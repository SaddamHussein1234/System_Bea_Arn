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

public partial class Cpanel_CPanelManageSite_PageMenuAdd : System.Web.UI.Page
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
            bool A10;
            A10 = Convert.ToBoolean(dtViewProfil.Rows[0]["A10"]);
            if (A10 == false)
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
            FGetMenu();
            FGetLastRecord();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT top(1) [IDOrder] As 'LastRecord' FROM [dbo].[tbl_MenuSite] Where IsDelete = @0 Order by IDOrder desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                txtOrder.Text = Convert.ToString(Convert.ToInt32(dt.Rows[0]["LastRecord"]) + 1);
            }
            catch (Exception)
            {
                txtOrder.Text = "1";
            }
        }
    }

    protected void rblCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
        {
            IDMenu.Visible = false;
            CBFork.Disabled = false;
            CBFork.Checked = true;
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
        {
            IDMenu.Visible = true;
            CBFork.Disabled = true;
            CBFork.Checked = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_MenuSite] Where TitleManageAr = @0", txtAr.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة القائمة سابقاً";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
            {
                AddMenu();
            }
            else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
            {
                if (DLMenu.SelectedItem.ToString() == string.Empty)
                {
                    lbmsg.Text = "الرجاء تحديد قائمة رئيسية للقائمة الفرعية";
                    lbmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    AddMenu();
                }
            }
        }
        FGetMenu();
    }

    private void AddMenu()
    {
        GetCookie();
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "INSERT INTO [dbo].[tbl_MenuSite]([TitleManageAr],[TitleManageTr],[TitleManageEn],[IDOrder],[TypeSection],[IDPartSection],[ViewMenu],[IsFork],[DescriptionManage],[DateAdd],[IsDelete],[IDAdmin])VALUES(@TitleManageAr,@TitleManageTr,@TitleManageEn,@IDOrder,@TypeSection,@IDPartSection,@ViewMenu,@IsFork,@DescriptionManage,@DateAdd,@IsDelete,@IDAdmin)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TitleManageAr", txtAr.Text.Trim());
        cmd.Parameters.AddWithValue("@TitleManageTr", txtTr.Text.Trim());
        cmd.Parameters.AddWithValue("@TitleManageEn", txtEn.Text.Trim());
        cmd.Parameters.AddWithValue("@IDOrder", Convert.ToInt32(txtOrder.Text.Trim()));
        if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
        {
            cmd.Parameters.AddWithValue("@TypeSection", 1);
            cmd.Parameters.AddWithValue("@IDPartSection", 0);
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
        {
            cmd.Parameters.AddWithValue("@TypeSection", 2);
            cmd.Parameters.AddWithValue("@IDPartSection", DLMenu.SelectedValue);
        }
        cmd.Parameters.AddWithValue("@ViewMenu", Convert.ToBoolean(CBView.Checked));
        cmd.Parameters.AddWithValue("@IsFork", Convert.ToBoolean(CBFork.Checked));
        cmd.Parameters.AddWithValue("@DescriptionManage", txtDetails.Text.Trim());
        cmd.Parameters.AddWithValue("@DateAdd", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
        cmd.Parameters.AddWithValue("@IsDelete", Convert.ToBoolean(false));
        cmd.Parameters.AddWithValue("@IDAdmin", Convert.ToInt32(IDUser));
        cmd.ExecuteScalar();
        conn.Close();
        lbmsg.Text = "تم إضافة القائمة بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        //txtAr.Text = string.Empty;
        //txtTr.Text = string.Empty;
        //txtEn.Text = string.Empty;
        txtOrder.Text = Convert.ToString(Convert.ToInt32(txtOrder.Text.Trim()) + 1);
        //txtDetails.Text = string.Empty;

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageMenu.aspx");
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageMenuAdd.aspx");
    }

    private void FGetMenu()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_MenuSite] Where TypeSection = @0 Order By IDOrder", Convert.ToString(1));
        if (dt.Rows.Count > 0)
        {
            DLMenu.Items.Clear();
            DLMenu.Items.Add("");
            DLMenu.AppendDataBoundItems = true;
            DLMenu.DataValueField = "IDItem";
            DLMenu.DataTextField = "TitleManageAr";
            DLMenu.DataSource = dt;
            DLMenu.DataBind();
        }
    }

}