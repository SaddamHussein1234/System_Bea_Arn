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

public partial class Cpanel_PageSearchStatusManager : System.Web.UI.Page
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
            bool A86;
            A86 = Convert.ToBoolean(dtViewProfil.Rows[0]["A86"]);
            if (A86 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnTahweelAlHalahByAllManager();
            //FGetModerAlGmeiah();
            //pnlSelect.Visible = true;
        }
    }

    private void FArnTahweelAlHalahByAllManager()
    {
        try
        {
            ClassBahthHalatMostafeed CBM = new ClassBahthHalatMostafeed();
            CBM._IDUniq = txtSearch.Text.Trim();
            CBM._IsAllowModer = false;
            CBM._IsAllowRaeesLagnatAlBahth = false;
            CBM._IsDelete = false;
            DataTable dtRecord = new DataTable();
            dtRecord = CBM.BArnBahthHalatMostafeedByAllManager();
            if (dtRecord.Rows.Count > 0)
            {
                GVSearch.DataSource = dtRecord;
                GVSearch.DataBind();
                lblCount.Text = Convert.ToString(dtRecord.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Focus();
                GVSearch.Columns[1].Visible = true;
                GVSearch.Columns[9].Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVSearch.UseAccessibleHeader = false;
        FArnTahweelAlHalahByAllManager();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSearchStatusManager.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVSearch.Columns[1].Visible = false;
            GVSearch.Columns[9].Visible = false;
            GVSearch.UseAccessibleHeader = true;
            GVSearch.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FAllowManager()
    {
        try
        {
            foreach (GridViewRow row in GVSearch.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVSearch.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[BahthHalatMostafeed] SET [AllowState] = @AllowState , [IsAllowModer] = @IsAllowModer WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowState", true);
                    cmd.Parameters.AddWithValue("@IsAllowModer", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                    //DataTable dt = new DataTable();
                    //dt = ClassDataAccess.GetData("SELECT [IDUniq] FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0", Comp_ID);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    ClassMosTafeed CM = new ClassMosTafeed();
                    //    CM._IDUniq = dt.Rows[0]["IDUniq"].ToString();
                    //    CM._IsAllowModer_ = true;
                    //    CM.BArnRasAlEstemarahUpdateModerAllow();
                    //}
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
        FAllowManager();
    }

}