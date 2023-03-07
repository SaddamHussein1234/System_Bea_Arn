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

public partial class Cpanel_PageAcceptanceDecision : System.Web.UI.Page
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
            bool A43, A88;
            A43 = Convert.ToBoolean(dtViewProfil.Rows[0]["A43"]);
            A88 = Convert.ToBoolean(dtViewProfil.Rows[0]["A88"]);
            if (A43 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A88 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVAcceptanceDecision.Columns[0].Visible = false;
                //Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
        }
    }

    private void FArnQararQobolMustafeedByAll()
    {
        try
        {
            GVAcceptanceDecision.UseAccessibleHeader = false;
            ClassQararQobol CQQ = new ClassQararQobol();
            CQQ._IDCheck = 0;
            CQQ._AlQaryah = 0;
            CQQ._IDUniq = txtSearch.Text.Trim();
            CQQ._IsQobol = true;
            CQQ._IsEstepaad = false;
            CQQ._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CQQ.BArnQararQobolMustafeedByAll();
            if (dt.Rows.Count > 0)
            {
                GVAcceptanceDecision.DataSource = dt;
                GVAcceptanceDecision.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Focus();
                GVAcceptanceDecision.Columns[0].Visible = true;
                GVAcceptanceDecision.Columns[12].Visible = true;
                CheckAccountAdmin();
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVAcceptanceDecision.Columns[0].Visible = false;
            GVAcceptanceDecision.Columns[12].Visible = false;
            GVAcceptanceDecision.UseAccessibleHeader = true;
            GVAcceptanceDecision.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FArnQararQobolMustafeedByAll();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAcceptanceDecision.aspx");
    }
    
    protected void DLAlBaheth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAcceptanceDecision.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAcceptanceDecision.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();

                    DataTable dtGetData = new DataTable();
                    dtGetData = ClassDataAccess.GetData("SELECT * FROM [dbo].[QararQobolMustafeed] With(NoLock) Where IDItem = @0", Comp_ID);
                    if (dtGetData.Rows.Count > 0)
                    {
                        string sql = "UPDATE [dbo].[QararQobolMustafeed] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                        cmd.Parameters.AddWithValue("@IsDelete", true);
                        cmd.ExecuteScalar();

                        string sql_ = "UPDATE [dbo].[QararQobolMustafeedAdmin] SET [IsDelete] = @IsDelete WHERE NumberMostafeed = @NumberMostafeed And NumberQarsr = @NumberQarar And NumberReport = @NumberReport";
                        SqlCommand cmd_ = new SqlCommand(sql_, conn);
                        cmd_.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(dtGetData.Rows[0]["NumberMostafeed"]));
                        cmd_.Parameters.AddWithValue("@NumberQarar", Convert.ToInt32(dtGetData.Rows[0]["NumberQarar"]));
                        cmd_.Parameters.AddWithValue("@NumberReport", Convert.ToInt32(dtGetData.Rows[0]["NumberReport"]));
                        cmd_.Parameters.AddWithValue("@IsDelete", true);
                        cmd_.ExecuteScalar();
                    }

                    conn.Close();
                }
            }
            FArnQararQobolMustafeedByAll();
        }
        catch (Exception)
        {
            return;
        }
    }

}