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

public partial class Shaerd_ERP_FMS_GeneralAssembly_PageAll : System.Web.UI.UserControl
{
    public string XType = string.Empty, XDisplay = string.Empty;
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
            bool A146, A147;
            A146 = Convert.ToBoolean(dtViewProfil.Rows[0]["A146"]);
            A147 = Convert.ToBoolean(dtViewProfil.Rows[0]["A147"]);
            if (A146 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A147 == false)
                btnDelete1.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (XType == "GA")
            XDisplay = "display:none;";
        if (!IsPostBack)
        {
            if (XType == "GA")
                CheckAccountAdmin();
            pnlSelect.Visible = true;
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")); i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 10; i--)
            {
                dtYear.Rows.Add(i);
            }
            DLYears.Items.Clear();
            DLYears.Items.Add("");
            DLYears.AppendDataBoundItems = true;
            DLYears.DataTextField = "Year";
            DLYears.DataValueField = "Year";
            DLYears.DataSource = dtYear;
            DLYears.DataBind();
        }
    }

    private void FGetGeneral_AssmplyBill()
    {
        try
        {
            GVGeneralAssemblyBill.Columns[0].Visible = true;
            GVGeneralAssemblyBill.Columns[13].Visible = true;
            GVGeneralAssemblyBill.UseAccessibleHeader = false;

            if (CB1.Checked) GVGeneralAssemblyBill.Columns[1].Visible = true; else GVGeneralAssemblyBill.Columns[1].Visible = false;
            if (CB2.Checked) GVGeneralAssemblyBill.Columns[2].Visible = true; else GVGeneralAssemblyBill.Columns[2].Visible = false;
            if (CB3.Checked) GVGeneralAssemblyBill.Columns[3].Visible = true; else GVGeneralAssemblyBill.Columns[3].Visible = false;
            if (CB4.Checked) GVGeneralAssemblyBill.Columns[4].Visible = true; else GVGeneralAssemblyBill.Columns[4].Visible = false;
            if (CB5.Checked) GVGeneralAssemblyBill.Columns[5].Visible = true; else GVGeneralAssemblyBill.Columns[5].Visible = false;
            if (CB6.Checked) GVGeneralAssemblyBill.Columns[6].Visible = true; else GVGeneralAssemblyBill.Columns[6].Visible = false;
            if (CB7.Checked) GVGeneralAssemblyBill.Columns[7].Visible = true; else GVGeneralAssemblyBill.Columns[7].Visible = false;
            if (CB8.Checked) GVGeneralAssemblyBill.Columns[8].Visible = true; else GVGeneralAssemblyBill.Columns[8].Visible = false;
            if (CB9.Checked) GVGeneralAssemblyBill.Columns[9].Visible = true; else GVGeneralAssemblyBill.Columns[9].Visible = false;
            if (CB10.Checked) GVGeneralAssemblyBill.Columns[10].Visible = true; else GVGeneralAssemblyBill.Columns[10].Visible = false;
            if (CB11.Checked) GVGeneralAssemblyBill.Columns[11].Visible = true; else GVGeneralAssemblyBill.Columns[11].Visible = false;
            if (CB12.Checked) GVGeneralAssemblyBill.Columns[12].Visible = true; else GVGeneralAssemblyBill.Columns[12].Visible = false;

            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT TOP (1000) TGAB.[ID_Item_] As 'ID',TGAB.[ID_Uniq_] As 'IDUniq',TGAB.*,TGAB.[ID_Admin_] As 'IDAdmin',TGAB.[Date_Add_] As 'DateAdd_',TGA.*,TA.FirstName,TA.FirstName,[Email],[PhoneNumber],[A3] FROM [dbo].[tbl_General_Assmply_Bill] TGAB With(NoLock) Inner Join tbl_General_Assmply TGA on TGA.[ID_Item_] = TGAB.[Number_Admin_] Inner Join tbl_Admin TA on TA.ID_Item = TGA.ID_Admin_Account_ Where ([FirstName] Like '%' + @0 + '%') And [_Years_] = @1 And TGAB.[Is_Delete_] = @2 And TGA.[Is_Delete_] = @2 Order by [bill_Number_]",
                txtSearch.Text.Trim(), DLYears.SelectedValue, Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "بيان إشتراكات أعضاء الجمعية العمومية لعام " + " ( " + DLYears.SelectedValue + " ) ";
                GVGeneralAssemblyBill.DataSource = dt;
                GVGeneralAssemblyBill.DataBind();
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
            return;
        }
    }

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        try
        {
            GVGeneralAssemblyBill.Columns[0].Visible = false;
            GVGeneralAssemblyBill.Columns[13].Visible = false;
            GVGeneralAssemblyBill.UseAccessibleHeader = true;
            GVGeneralAssemblyBill.HeaderRow.TableSection = TableRowSection.TableHeader;
            //Session["footable1"] = pnl2;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");

            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");

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
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetGeneral_AssmplyBill();
    }

    protected void DLYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetGeneral_AssmplyBill();
    }

}