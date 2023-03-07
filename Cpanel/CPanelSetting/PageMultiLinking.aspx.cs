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

public partial class Cpanel_CPanelSetting_PageMultiLinking : System.Web.UI.Page
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
            bool A8;
            A8 = Convert.ToBoolean(dtViewProfil.Rows[0]["A8"]);
            if (A8 == false)
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
            pnlSelect.Visible = true;
            FGetAdmin();
            if (Request.QueryString["ID"] != null)
            {
                try
                {
                    FGetDataAdmin(Convert.ToInt32(Request.QueryString["ID"]));
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }

    private void FGetDataAdmin(int XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select * from tbl_Admin Where ID_Item = @0", Convert.ToString(XID));
        if (dt.Rows.Count > 0)
        {
            DLAdmin.SelectedValue = dt.Rows[0]["ID_Item"].ToString();
            lblName.Text = dt.Rows[0]["FirstName"].ToString();
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblDateReg.Text = Convert.ToDateTime(dt.Rows[0]["DateReg"]).ToString("yyyy/MM/dd");

        }
        else
        {
            Response.Redirect("PageAdmin.aspx");
        }
        FGetMultiQariah();
    }
    
    protected void btnRefrish_Click(object sender, EventArgs e)
    {

    }

    protected void DLAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FGetDataAdmin(Convert.ToInt32(DLAdmin.SelectedValue));
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetAdmin()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [ID_Item],[FirstName]FROM [dbo].[tbl_Admin] with(NoLock) Where tbl_Admin.IsDelete = @0 And IsHide = @0 And IsBaheth = @1 Order by IsOrderAdminInEdarah", Convert.ToString(false), Convert.ToString(true));
        if (dt.Rows.Count > 0)
        {
            DLAdmin.Items.Clear();
            DLAdmin.Items.Add("");
            DLAdmin.AppendDataBoundItems = true;
            DLAdmin.DataValueField = "ID_Item";
            DLAdmin.DataTextField = "FirstName";
            DLAdmin.DataSource = dt;
            DLAdmin.DataBind();
        }
        FGetAlQariah();
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLQariah.Items.Clear();
            DLQariah.Items.Add("");
            DLQariah.AppendDataBoundItems = true;
            DLQariah.DataValueField = "IDItem";
            DLQariah.DataTextField = "AlQriah";
            DLQariah.DataSource = dt;
            DLQariah.DataBind();
        }
    }

    private void FGetMultiQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IsDelete = @1 Order by IDQariah Desc", DLAdmin.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVMultiQariah.DataSource = dt;
            GVMultiQariah.DataBind();
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
    
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            FChackName();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FChackName()
    {
        DataTable dtUser = new DataTable();
        dtUser = ClassDataAccess.GetData("Select IDAdminJoin,IDQariah from tbl_MultiQariah Where IDAdminJoin =@0 And IDQariah = @1 And IsDelete = @2", DLAdmin.SelectedValue, DLQariah.SelectedValue, Convert.ToString(false));
        if (dtUser.Rows.Count > 0)
        {
            lbmsg.Text = "تم ربط المستخدم سابقاً ";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FArnMultiQariahAdd();
        }
    }

    private void FArnMultiQariahAdd()
    {
        GetCookie();
        ClassMultiQariah CMQ = new ClassMultiQariah();
        CMQ._IDAdminJoin = Convert.ToInt32(DLAdmin.SelectedValue);
        CMQ._AlQaryah = Convert.ToInt32(DLQariah.SelectedValue);
        CMQ._dateEntry = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
        CMQ._IDAdminAdd = Convert.ToInt32(IDUser);
        CMQ._A1 = "0"; CMQ._A2 = "0"; CMQ._A3 = "0"; CMQ._A4 = "0"; CMQ._A5 = "0";
        CMQ._IsDelete = false;
        CMQ.BArnMultiQariahAdd();
        lbmsg.Text = "تم الإضافة بنجاح ";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetMultiQariah();
    }
    
    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMultiQariah.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMultiQariah.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_MultiQariah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetDataAdmin(Convert.ToInt32(DLAdmin.SelectedValue));
        }
        catch (Exception)
        {
            return;
        }
    }

}