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

public partial class Cpanel_CPanelManageSite_PageAlbum : System.Web.UI.Page
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
            bool A15, A16;
            A15 = Convert.ToBoolean(dtViewProfil.Rows[0]["A15"]);
            A16 = Convert.ToBoolean(dtViewProfil.Rows[0]["A16"]);
            if (A15 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A16 == false)
            {
                IDAdd.Visible = false;
                btnDelete.Visible = false;
                GVAlbum.Columns[0].Visible = false;
                GVAlbum.Columns[10].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            //lblName.Text = ClassSetting.FGetNameSiteAR();
            //lblDate.Text = ClassKhwarism.GetCurrentTime().ToString("yyyy/MM/dd");
            FGetAlbum();
        }
    }

    private void FGetAlbum()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Album] With(NoLock) Where TitleAlbumAr Like '%' + @0 + '%' And IsDelete = @1 Order by IsOrder", txtSearch.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة البومات الصور";
            GVAlbum.DataSource = dt;
            GVAlbum.DataBind();
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

    public float FCountImg(float X)
    {
        float VaManage = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT COUNT(*) As 'CountImg' FROM [dbo].[tbl_AlbumImg] Where IDAlbum = @0 And IsDelete = @1", Convert.ToString(X), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            VaManage = Convert.ToInt64(dt.Rows[0]["CountImg"]);
        return VaManage;
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAlbum.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAlbum.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Album] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
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
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetAlbum();
        System.Threading.Thread.Sleep(300);
    }

}