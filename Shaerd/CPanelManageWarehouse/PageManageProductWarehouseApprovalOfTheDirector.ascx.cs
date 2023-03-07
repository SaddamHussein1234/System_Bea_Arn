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

public partial class Shaerd_CPanelManageWarehouse_PageManageProductWarehouseApprovalOfTheDirector : System.Web.UI.UserControl
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
            bool A117;
            A117 = Convert.ToBoolean(dtViewProfil.Rows[0]["A117"]);
            if (A117 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnProductShopWarehouseByModer();
        }
    }

    private void FArnProductShopWarehouseByModer()
    {
        try
        {
            GVWarehouseApprovalOfTheDirector.Columns[0].Visible = true;
            GVWarehouseApprovalOfTheDirector.Columns[6].Visible = true;
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.IsModer = false;
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopWarehouseByModer();
            if (dt.Rows.Count > 0)
            {
                GVWarehouseApprovalOfTheDirector.DataSource = dt;
                GVWarehouseApprovalOfTheDirector.DataBind();
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
        Response.Redirect("PageManageProductWarehouseApprovalOfTheDirector.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVWarehouseApprovalOfTheDirector.Columns[0].Visible = false;
            GVWarehouseApprovalOfTheDirector.Columns[6].Visible = false;

            GVWarehouseApprovalOfTheDirector.UseAccessibleHeader = true;
            GVWarehouseApprovalOfTheDirector.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            GVWarehouseApprovalOfTheDirector.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVWarehouseApprovalOfTheDirector.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVWarehouseApprovalOfTheDirector.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsModer] = @IsModer WHERE _billNumber = @billNumber And _IDMosTafeed = @IDMosTafeed And _IDNaebRaees = @IDNaebRaees";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@billNumber", 0);
                    cmd.Parameters.AddWithValue("@IDMosTafeed", 0);
                    cmd.Parameters.AddWithValue("@IDNaebRaees", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsModer", true);
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

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;
    int tempcounter = 0;
    protected void GVWarehouseApprovalOfTheDirector_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
                lblTotalPrice.Text = sum.ToString();
            else
                lblTotalPrice.Text = "بإنتظار التسعير";
            tempcounter = tempcounter + 1;
            if (tempcounter == 5)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

}