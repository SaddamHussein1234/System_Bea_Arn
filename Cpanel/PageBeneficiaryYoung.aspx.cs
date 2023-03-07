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

public partial class Cpanel_PageBeneficiaryYoung : System.Web.UI.Page
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

    // عدد اسر الايتام
    private void FGetMostafeedByHalafAlMosTafeed(string XCheck)
    {
        DataTable dt = new DataTable();
        if (XCheck == "All")
        {
            if (RBLFilter.SelectedValue == "_All")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3) or (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @4) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @5)) And AlQarabah = @6)) And (TarafEstemarah.IsDelete = @7)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Boy")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Gir")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
        }
        else
        {
            if (RBLFilter.SelectedValue == "_All")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3) or (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @4) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @5)) And AlQarabah = @6)) And (TarafEstemarah.IsDelete = @7) And (TarafEstemarah.[_Is_Print_Hide_] = @7)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Boy")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4) And (TarafEstemarah.[_Is_Print_Hide_] = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Gir")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4) And (TarafEstemarah.[_Is_Print_Hide_] = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
        }
        if (dt.Rows.Count > 0)
            lblCountAoser.Text = Convert.ToString(dt.Rows.Count);
    }

    private void FGetMostafeedByChildren(string IDCheck, int IDQariah, int XGenderMale, int XGenderFeMale)
    {
        int XAgeBoysmaller = 0, XAgeGirlsmaller = 0, XAgeBoyLarger = 0, XAgeGirlLarger = 0;
        XAgeBoysmaller = Int32.Parse(txtAgeBoysmaller.Text.Trim());
        XAgeGirlsmaller = Int32.Parse(txtAgeGirlssmaller.Text.Trim());

        XAgeBoyLarger = Int32.Parse(txtAgeBoyLarger.Text.Trim());
        XAgeGirlLarger = Int32.Parse(txtAgeGirlsLarger.Text.Trim());

        GVOrphansAll.UseAccessibleHeader = false;
        ClassTarafMostafeed._XIDCheck = IDCheck;
        ClassTarafMostafeed._TypeMostafeed = DLTypeMostafeed.SelectedValue;
        ClassTarafMostafeed._AlQaryah = IDQariah;
        ClassTarafMostafeed._AllowYearBoy = XAgeBoysmaller;
        ClassTarafMostafeed._AllowYearLargerBoy = XAgeBoyLarger;
        ClassTarafMostafeed._AllowYearGirl = XAgeGirlsmaller;
        ClassTarafMostafeed._AllowYearLargerGirl = XAgeGirlLarger;
        ClassTarafMostafeed._GenderMale = XGenderMale;//2;
        ClassTarafMostafeed._GenderFeMale = XGenderFeMale;//1;
        ClassTarafMostafeed._IsDelete2 = false;
        DataTable dt = new DataTable();
        dt = ClassTarafMostafeed.BArnTarafEstemarahGetBoysYoungAllAddvance();
        if (dt.Rows.Count > 0)
        {
            GVOrphansAll.DataSource = dt;
            GVOrphansAll.DataBind();
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

    private void Filter()
    {
        if (RBLFilter.SelectedValue == "_All")
        {
            FGetMostafeedByChildren("All", Convert.ToInt32(0), 2, 1);
            txtSearchOrphans.Text = "بيانات الشباب من (" + txtAgeBoysmaller.Text.Trim() + " إلى " + txtAgeBoyLarger.Text.Trim()
                + " ) سنة والفتيات من ( " + txtAgeGirlssmaller.Text.Trim() + " إلى " + txtAgeGirlsLarger.Text.Trim() + " ) سنة";
        }
        else if (RBLFilter.SelectedValue == "_Boy")
        {
            FGetMostafeedByChildren("Boy", Convert.ToInt32(0), 2, 0);
            txtSearchOrphans.Text = "بيانات الشباب من (" + txtAgeBoysmaller.Text.Trim() + " إلى " + txtAgeBoyLarger.Text.Trim() + " ) سنة";
        }
        else if (RBLFilter.SelectedValue == "_Gir")
        {
            FGetMostafeedByChildren("Boy", Convert.ToInt32(0), 1, 0);
            txtSearchOrphans.Text = "بيانات الفتيات من (" + txtAgeGirlssmaller.Text.Trim() + " إلى " + txtAgeGirlsLarger.Text.Trim() + " ) سنة";
        }
    }

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        try
        {
            FGetAitamByQriah("Part");
            FGetMostafeedByHalafAlMosTafeed("Part");

            if (RBLFilter.SelectedValue == "_All")
                FGetMostafeedByChildren("AllPrint", Convert.ToInt32(0), 2, 1);
            else if (RBLFilter.SelectedValue == "_Boy")
                FGetMostafeedByChildren("BoyPrint", Convert.ToInt32(0), 2, 0);
            else if (RBLFilter.SelectedValue == "_Gir")
                FGetMostafeedByChildren("BoyPrint", Convert.ToInt32(0), 1, 0);

            GVOrphansAll.Columns[0].Visible = false;

            GVOrphansAll.UseAccessibleHeader = true;
            GVOrphansAll.HeaderRow.TableSection = TableRowSection.TableHeader;

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
        Response.Redirect("PageBeneficiaryYoung.aspx");
    }

    private void FGetAitamByQriah(string XCheck)
    {
        DataTable dt = new DataTable();
        if (XCheck == "All")
        {
            if (RBLFilter.SelectedValue == "_All")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3) or (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @4) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @5)) And AlQarabah = @6)) And (TarafEstemarah.IsDelete = @7)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Boy")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Gir")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
        }
        else
        {
            if (RBLFilter.SelectedValue == "_All")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3) or (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @4) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @5)) And AlQarabah = @6)) And (TarafEstemarah.IsDelete = @7) And (TarafEstemarah.[_Is_Print_Hide_] = @7)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Boy")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4) And (TarafEstemarah.[_Is_Print_Hide_] = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeBoysmaller.Text.Trim(), txtAgeBoyLarger.Text.Trim(), "2", Convert.ToString(false));
            else if (RBLFilter.SelectedValue == "_Gir")
                dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And ((((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) > @1) And (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2)) And AlQarabah = @3)) And (TarafEstemarah.IsDelete = @4) And (TarafEstemarah.[_Is_Print_Hide_] = @4)",
                DLTypeMostafeed.SelectedValue, txtAgeGirlssmaller.Text.Trim(), txtAgeGirlsLarger.Text.Trim(), "1", Convert.ToString(false));
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
            GVOrphansAll.Columns[0].Visible = true;

            pnlData.Visible = true;
            FGetAitamByQriah("All");
            FGetMostafeedByHalafAlMosTafeed("All");
            Filter();
            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryYoung.aspx");
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVOrphansAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVOrphansAll.DataKeys[row.RowIndex].Value);
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
            foreach (GridViewRow row in GVOrphansAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVOrphansAll.DataKeys[row.RowIndex].Value);
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
