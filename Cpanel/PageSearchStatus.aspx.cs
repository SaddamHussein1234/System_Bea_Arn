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

public partial class Cpanel_PageSearchStatus : System.Web.UI.Page
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
            bool A41,A85;
            A41 = Convert.ToBoolean(dtViewProfil.Rows[0]["A41"]);
            A85 = Convert.ToBoolean(dtViewProfil.Rows[0]["A85"]);
            if (A41 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A85 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVConvertedCases.Columns[0].Visible = false;
                //Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            //FArnQararQobolMustafeedByAll();
            //FGetModerAlGmeiah();
            pnlSelect.Visible = true;
            txtSearch.Focus();
        }
    }

    private void FArnTahweelAlHalahByAll()
    {
        try
        {
            ClassBahthHalatMostafeed CBM = new ClassBahthHalatMostafeed();
            CBM._IDUniq = txtSearch.Text.Trim();
            CBM._IsDelete = false;
            DataTable dtRecord = new DataTable();
            dtRecord = CBM.BArnBahthHalatMostafeedByAll();
            if (dtRecord.Rows.Count > 0)
            {
                GVConvertedCases.DataSource = dtRecord;
                GVConvertedCases.DataBind();
                lblCount.Text = Convert.ToString(dtRecord.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Focus();
                
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
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVConvertedCases.Columns[0].Visible = false;
            GVConvertedCases.Columns[9].Visible = false;
            CheckAccountAdmin();

            GVConvertedCases.UseAccessibleHeader = true;
            GVConvertedCases.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSearchStatus.aspx");
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVConvertedCases.UseAccessibleHeader = false;
        GVConvertedCases.Columns[0].Visible = true;
        GVConvertedCases.Columns[9].Visible = true;
        CheckAccountAdmin();
        FArnTahweelAlHalahByAll();
        System.Threading.Thread.Sleep(200);
    }
    
    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVConvertedCases.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVConvertedCases.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[BahthHalatMostafeed] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FArnTahweelAlHalahByAll();
        }
        catch (Exception)
        {
            return;
        }
    }

}