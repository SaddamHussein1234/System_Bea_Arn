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

public partial class Shaerd_ERP_FMS_GeneralAssembly_PageRaees : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    string IDUniq = string.Empty;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
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
            bool A149, A147;
            A149 = Convert.ToBoolean(dtViewProfil.Rows[0]["A149"]);
            A147 = Convert.ToBoolean(dtViewProfil.Rows[0]["A147"]);
            if (A149 == false)
                Response.Redirect("/Cpanel/CHome/PageNotAccess.aspx");
            if (A147 == false)
                btnDelete1.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "GA")
                CheckAccountAdmin();
            FGetGeneral_AssmplyBill();
        }
    }

    private void FGetGeneral_AssmplyBill()
    {
        try
        {
            GVGeneralAssemblyBill.Columns[0].Visible = true;
            GVGeneralAssemblyBill.Columns[12].Visible = true;
            GVGeneralAssemblyBill.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT TOP (1000) TGAB.[ID_Item_] As 'ID',TGAB.[ID_Uniq_] As 'IDUniq',TGAB.*,TGAB.[ID_Admin_] As 'IDAdmin',TGAB.[Date_Add_] As 'DateAdd_',TGA.*,TA.FirstName,TA.FirstName,[Email],[PhoneNumber],[A3] FROM [dbo].[tbl_General_Assmply_Bill] TGAB With(NoLock) Inner Join tbl_General_Assmply TGA on TGA.[ID_Item_] = TGAB.[Number_Admin_] Inner Join tbl_Admin TA on TA.ID_Item = TGA.ID_Admin_Account_ Where ([FirstName] Like '%' + @0 + '%') And [IsAllow_Ameen_Alsondoq_] = @1 And [IsAllow_Raees_AlMagles_] = @2 And TGAB.[Is_Delete_] = @2 And TGA.[Is_Delete_] = @2 Order by [Number_Rigstry_]",
                txtSearch.Text.Trim(), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVGeneralAssemblyBill.DataSource = dt;
                GVGeneralAssemblyBill.DataBind();
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

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVGeneralAssemblyBill.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVGeneralAssemblyBill.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_General_Assmply_Bill] SET [IsAllow_Ameen_Alsondoq_] = @IsAllow_Ameen_Alsondoq , [IsAllow_Raees_AlMagles_] = @IsAllow_Raees_AlMagles WHERE ID_Item_ = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsAllow_Ameen_Alsondoq", true);
                    cmd.Parameters.AddWithValue("@IsAllow_Raees_AlMagles", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetGeneral_AssmplyBill();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVGeneralAssemblyBill.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVGeneralAssemblyBill.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_General_Assmply_Bill] SET [Is_Delete_] = @Is_Delete WHERE ID_Item_ = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@Is_Delete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetGeneral_AssmplyBill();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageRaees.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetGeneral_AssmplyBill();
    }

}