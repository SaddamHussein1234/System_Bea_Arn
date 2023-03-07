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

public partial class Cpanel_CPanelManageSite_PageMassageVisit : System.Web.UI.Page
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
            bool A21, A22;
            A21 = Convert.ToBoolean(dtViewProfil.Rows[0]["A21"]);
            A22 = Convert.ToBoolean(dtViewProfil.Rows[0]["A22"]);
            if (A21 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A22 == false)
            {
                btnDelete.Visible = false;
                RPTMessage.Columns[0].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetMessage();
        }
    }

    private void FGetMessage()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Content] Where TitleMeassge + NameUser + CountryUser Like '%' + @0 + '%' And IsDelete = @1 Order by DateSend desc", txtSearch.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة رسائل الزوار";
            RPTMessage.DataSource = dt;
            RPTMessage.DataBind();
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageMassageVisit.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in RPTMessage.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(RPTMessage.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Content] SET [IsDelete] = @IsDelete WHERE IDCont = @IDCont";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDCont", Comp_ID);
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
    
    protected void RPTMessage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RPTMessage.PageIndex = e.NewPageIndex;
        this.FGetMessage();
    }

    public static string FCheckNewF(bool IDP)
    {
        string VaComp = "";
        if (IDP == false)
        {
            VaComp = "<span style='border-radius:6px; font-size:10px; background-color:#B2D430; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> جديد </span>";
        }
        return VaComp.ToString();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(500);
        FGetMessage();
    }

}