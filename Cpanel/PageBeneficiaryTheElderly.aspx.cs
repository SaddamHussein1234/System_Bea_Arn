using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using FusionCharts.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageBeneficiaryTheElderly : System.Web.UI.Page
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
            bool A39;
            A39 = Convert.ToBoolean(dtViewProfil.Rows[0]["A39"]);
            if (A39 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlWaiting.Visible = true;
            FGetAlQariah();
        }
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQriahByData.Items.Clear();
            DLAlQriahByData.Items.Add("");
            DLAlQriahByData.AppendDataBoundItems = true;
            DLAlQriahByData.DataValueField = "IDItem";
            DLAlQriahByData.DataTextField = "AlQriah";
            DLAlQriahByData.DataSource = dt;
            DLAlQriahByData.DataBind();
        }
    }

    private void Filter()
    {
        if (RBLFilter.SelectedValue == "_All")
        {
            FArnRasAlEstemarahTheElderlyAddvance("All", Convert.ToInt32(0), 1, 2);
            txtSearchOrphans.Text = "بيانات كبار السن الذكور أكبر من (" + txtAgeBoy.Text.Trim()  
                + " ) سنة والإناث أكبر من ( " + txtAgeGirls.Text.Trim() + " ) سنة";
        }
        else if (RBLFilter.SelectedValue == "_Boy")
        {
            FArnRasAlEstemarahTheElderlyAddvance("Boy", Convert.ToInt32(0), 1, 0);
            txtSearchOrphans.Text = "بيانات الذكور من (" + txtAgeBoy.Text.Trim() + " إلى " + "" + " ) سنة";
        }
        else if (RBLFilter.SelectedValue == "_Gir")
        {
            FArnRasAlEstemarahTheElderlyAddvance("Boy", Convert.ToInt32(0), 2, 0);
            txtSearchOrphans.Text = "بيانات الإناث من (" + txtAgeGirls.Text.Trim() + " إلى " + "" + " ) سنة";
        }
    }

    private void FArnRasAlEstemarahTheElderlyAddvance(string XIDCheck, int IDQariah, int XGenderMale, int XGenderFeMale)
    {
        if (CB3.Checked) { GVMostafeed.Columns[3].Visible = true; } else { GVMostafeed.Columns[3].Visible = false; }
        if (CB4.Checked) { GVMostafeed.Columns[4].Visible = true; } else { GVMostafeed.Columns[4].Visible = false; }
        if (CB6.Checked) { GVMostafeed.Columns[6].Visible = true; } else { GVMostafeed.Columns[6].Visible = false; }
        if (CB7.Checked) { GVMostafeed.Columns[7].Visible = true; } else { GVMostafeed.Columns[7].Visible = false; }
        if (CB8.Checked) { GVMostafeed.Columns[8].Visible = true; } else { GVMostafeed.Columns[8].Visible = false; }
        if (CB9.Checked) { GVMostafeed.Columns[9].Visible = true; } else { GVMostafeed.Columns[9].Visible = false; }
        if (CB10.Checked) { GVMostafeed.Columns[10].Visible = true; } else { GVMostafeed.Columns[10].Visible = false; }
        if (CB11.Checked) { GVMostafeed.Columns[11].Visible = true; } else { GVMostafeed.Columns[11].Visible = false; }
        if (CB12.Checked) { GVMostafeed.Columns[12].Visible = true; } else { GVMostafeed.Columns[12].Visible = false; }
        if (CB13.Checked) { GVMostafeed.Columns[13].Visible = true; } else { GVMostafeed.Columns[13].Visible = false; }
        if (CB14.Checked) { GVMostafeed.Columns[14].Visible = true; } else { GVMostafeed.Columns[14].Visible = false; }
        if (CB15.Checked) { GVMostafeed.Columns[15].Visible = true; } else { GVMostafeed.Columns[15].Visible = false; }
        int XAgeBoy = 0, XAgeGirl = 0;
        XAgeBoy = Int32.Parse(txtAgeBoy.Text.Trim());
        XAgeGirl = Int32.Parse(txtAgeGirls.Text.Trim());

        GVMostafeed.UseAccessibleHeader = false;
        ClassMosTafeed CM = new ClassMosTafeed();
        CM.IDCheck = XIDCheck;
        CM._TypeMostafeed = DLTypeMostafeed.SelectedValue;
        CM._AlQaryah = IDQariah;
        CM._AllowYearsmallerBoy = XAgeBoy;
        CM._AllowYearLargerBoy = 110;
        CM._AllowYearsmallerGirl = XAgeGirl;
        CM._AllowYearLargerGirl = 110;
        CM._GenderMale = XGenderMale;
        CM._GenderFeMale = XGenderFeMale;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahTheElderlyAddvance();
        if (dt.Rows.Count > 0)
        {
            GVMostafeed.DataSource = dt;
            GVMostafeed.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlWaiting.Visible = false;
            pnlPrintAllData.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlWaiting.Visible = false;
            pnlPrintAllData.Visible = false;
        }
    }

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        try
        {
            FGetAitamByQriah("Part");
            if (RBLFilter.SelectedValue == "_All")
                FArnRasAlEstemarahTheElderlyAddvance("AllPrint", Convert.ToInt32(0), 1, 2);
            else if (RBLFilter.SelectedValue == "_Boy")
                FArnRasAlEstemarahTheElderlyAddvance("BoyPrint", Convert.ToInt32(0), 1, 0);
            else if (RBLFilter.SelectedValue == "_Gir")
                FArnRasAlEstemarahTheElderlyAddvance("BoyPrint", Convert.ToInt32(0), 2, 0);

            GVMostafeed.Columns[0].Visible = false;
            GVMostafeed.Columns[16].Visible = false;

            GVMostafeed.UseAccessibleHeader = true;
            GVMostafeed.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlPrintAllData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryTheElderly.aspx");
    }

    private void FGetAitamByQriah(string XCheck)
    {
        DataTable dt = new DataTable();
        if (XCheck == "All")
        {
            if (RBLFilter.SelectedValue == "_All")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @2)) And [Gender] = @3) or (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @4) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @5)) And [Gender] = @6)) And (IsDelete = @7)",
                DLTypeMostafeed.SelectedValue, txtAgeBoy.Text.Trim(), "110", "1", txtAgeGirls.Text.Trim(), "110", "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Boy")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @2)) And [Gender] = @3)) And (IsDelete = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeBoy.Text.Trim(), "110", "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Gir")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @2)) And [Gender] = @3)) And (IsDelete = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeGirls.Text.Trim(), "110", "2", Convert.ToString(false));
        }
        else
        {
            if (RBLFilter.SelectedValue == "_All")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @2)) And [Gender] = @3) or (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @4) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @5)) And [Gender] = @6)) And (IsDelete = @7) And ([_Is_Print_Hide_] = @7)",
                DLTypeMostafeed.SelectedValue, txtAgeBoy.Text.Trim(), "110", "1", txtAgeGirls.Text.Trim(), "110", "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Boy")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @2)) And [Gender] = @3)) And (IsDelete = @4) And ([_Is_Print_Hide_] = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeBoy.Text.Trim(), "110", "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Gir")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),DateBrith,112))/10000) < @2)) And [Gender] = @3)) And (IsDelete = @4) And ([_Is_Print_Hide_] = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeGirls.Text.Trim(), "110", "2", Convert.ToString(false));
        }
        if (dt.Rows.Count > 0)
            lblCountAitam.Text = dt.Rows.Count.ToString();
        else
            lblCountAitam.Text = "0";
    }

    protected void btnGetByType_Click(object sender, EventArgs e)
    {
        try
        {
            GVMostafeed.Columns[0].Visible = true;
            GVMostafeed.Columns[16].Visible = true;

            pnlData.Visible = true;
            FGetAitamByQriah("All");
            Filter();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryTheElderly.aspx");
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMostafeed.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMostafeed.DataKeys[row.RowIndex].Value);
                    FHideOrView(Convert.ToInt64(Comp_ID), true);
                }
            }
            Filter();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMostafeed.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMostafeed.DataKeys[row.RowIndex].Value);
                    FHideOrView(Convert.ToInt64(Comp_ID), false);
                }
            }
            Filter();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FHideOrView(Int64 XID, bool XValue)
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[TarafEstemarah] SET [_Is_Print_Hide_] = @Is_Print_Hide WHERE [IDItem] = @IDItem";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDItem", XID);
        cmd.Parameters.AddWithValue("@Is_Print_Hide", XValue);
        cmd.ExecuteScalar();
        conn.Close();
    }

    public string FCheckHide(bool XValue)
    {
        string XResult = string.Empty;
        if (XValue)
            XResult = "<br /> <br /> <span style='background:#ba0404; padding:3px; border-radius:3px; color:#F0F0F0;'><small><i class='fa fa-eye-slash'></i> مخفي أثناء الطباعة </small></span>";
        return XResult;
    }

}