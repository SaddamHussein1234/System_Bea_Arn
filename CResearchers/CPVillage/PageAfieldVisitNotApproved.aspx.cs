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

public partial class CResearchers_CPVillage_PageAfieldVisitNotApproved : System.Web.UI.Page
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
            bool A48, A92;
            A48 = Convert.ToBoolean(dtViewProfil.Rows[0]["A48"]);
            A92 = Convert.ToBoolean(dtViewProfil.Rows[0]["A92"]);
            if (A48 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A92 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVAfieldVisitApproval.Columns[0].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            FGetAlQariah();
            HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
            string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";
            DLAlQriah.SelectedValue = IDVillage;
        }
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQriah.Items.Clear();
            DLAlQriah.Items.Add("");
            DLAlQriah.AppendDataBoundItems = true;
            DLAlQriah.DataValueField = "IDQariah";
            DLAlQriah.DataTextField = "AlQriah";
            DLAlQriah.DataSource = dt;
            DLAlQriah.DataBind();
        }
    }

    private void FArnZeyarahMaydanyahNotApproval()
    {
        try
        {
            ClassZeyarahMaydanyah CZM = new ClassZeyarahMaydanyah();
            CZM._IDCheck = 1;
            CZM._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
            CZM._IDUniq = txtSearch.Text.Trim();
            CZM._StateView = true;
            CZM._AllowAlZeyarah = false;
            CZM._NotAllowAlZeyarah = true;
            CZM._IsRaeesMaglesAEdarah = true;
            CZM._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CZM.BArnZeyarahMaydanyahPendingApproval();
            if (dt.Rows.Count > 0)
            {
                GVAfieldVisitApproval.DataSource = dt;
                GVAfieldVisitApproval.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAfieldVisitNotApproved.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAfieldVisitApproval.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAfieldVisitApproval.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ZeyarahMaydanyah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FArnZeyarahMaydanyahNotApproval();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetCookie();
        DataTable dtcheck = new DataTable();
        dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriah.SelectedValue, Convert.ToString(false));
        if (dtcheck.Rows.Count > 0)
        {
            FArnZeyarahMaydanyahNotApproval();
        }
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
        System.Threading.Thread.Sleep(200);
    }

}