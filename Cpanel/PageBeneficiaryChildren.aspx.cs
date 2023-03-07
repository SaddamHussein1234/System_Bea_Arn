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

public partial class Cpanel_PageBeneficiaryChildren : System.Web.UI.Page
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
            FGetAitamByQriah("All");
            pnlWaiting.Visible = true;
            string XAgeBoy = string.Empty, XAgeGirl = string.Empty;
            XAgeBoy = ClassSetting.FGetChildrenAgeBoy(); XAgeGirl = ClassSetting.FGetChildrenAgeGirl();
            txtAgeBoy.Text = XAgeBoy; txtAgeGirls.Text = XAgeGirl;
            //txtTitle.Text = "قائمة إحصائية المستفيدين حسب الأطفال دون سن (" + XAgeBoy + " سنة للذكور ) و ( " + XAgeGirl + " سنة للإناث )";
            FGetMostafeedByHalafAlMosTafeed("All");
            FGetAlQariah();
            //FGetMostafeedByOrphans();
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
            dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @1) And AlQarabah = @2) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @3) And AlQarabah = @4)) And (TarafEstemarah.IsDelete = @5)",
                DLTypeMostafeed.SelectedValue, ClassSetting.FGetChildrenAgeGirl(), "2", ClassSetting.FGetChildrenAgeBoy(), "1", Convert.ToString(false));
        else
            dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @1) And AlQarabah = @2) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @3) And AlQarabah = @4)) And (TarafEstemarah.IsDelete = @5) And TarafEstemarah.[_Is_Print_Hide_] = @5",
                    DLTypeMostafeed.SelectedValue, ClassSetting.FGetChildrenAgeGirl(), "2", ClassSetting.FGetChildrenAgeBoy(), "1", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            lblCountAoser.Text = Convert.ToString(dt.Rows.Count);
    }

    private void FGetMostafeedByChildren(int IDCheck, int IDQariah)
    {
        int XAgeBoy = 0, XAgeGirl = 0;
        XAgeBoy = Int32.Parse(ClassSetting.FGetChildrenAgeGirl());
        XAgeGirl = Int32.Parse(ClassSetting.FGetChildrenAgeBoy());

        GVOrphansAll.UseAccessibleHeader = false;
        ClassTarafMostafeed._IDCheck = IDCheck;
        ClassTarafMostafeed._TypeMostafeed = DLTypeMostafeed.SelectedValue;
        ClassTarafMostafeed._AlQaryah = IDQariah;
        ClassTarafMostafeed._AllowYearBoy = XAgeBoy;
        ClassTarafMostafeed._AllowYearGirl = XAgeGirl;
        ClassTarafMostafeed._GenderMale = 2;
        ClassTarafMostafeed._GenderFeMale = 1;
        ClassTarafMostafeed._IsDelete2 = false;
        DataTable dt = new DataTable();
        dt = ClassTarafMostafeed.BArnTarafEstemarahGetBoysChildrenAllAddvance();
        if (dt.Rows.Count > 0)
        {
            txtSearchOrphans.Text = "بيانات الأطفال دون سن (" + XAgeBoy.ToString() + " سنة للذكور ) و ( " + XAgeGirl.ToString() + " سنة للإناث )";
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

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        try
        {
            FGetAitamByQriah("Part");
            FGetMostafeedByHalafAlMosTafeed("Part");
            if (DLAlQriahByData.SelectedValue != string.Empty)
                FGetMostafeedByChildren(100, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByChildren(10, Convert.ToInt32(0));

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
        Response.Redirect("PageBeneficiaryChildren.aspx");
    }

    private void FGetAitamByQriah(string XCheck)
    {
        DataTable dt = new DataTable();
        if (XCheck == "All")
            dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @1) And AlQarabah = @2) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @3) And AlQarabah = @4)) And (TarafEstemarah.IsDelete = @5)",
                DLTypeMostafeed.SelectedValue, ClassSetting.FGetChildrenAgeGirl(), "2", ClassSetting.FGetChildrenAgeBoy(), "1", Convert.ToString(false));
        else
            dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @1) And AlQarabah = @2) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @3) And AlQarabah = @4)) And (TarafEstemarah.IsDelete = @5) And TarafEstemarah.[_Is_Print_Hide_] = @5",
                DLTypeMostafeed.SelectedValue, ClassSetting.FGetChildrenAgeGirl(), "2", ClassSetting.FGetChildrenAgeBoy(), "1", Convert.ToString(false));
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
            if (DLAlQriahByData.SelectedValue != string.Empty)
                FGetMostafeedByChildren(1, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByChildren(0, Convert.ToInt32(0));

            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryChildren.aspx");
    }

    protected void LBEditAge_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[SettingTable] SET [_ChildrenAgeBoy] = @ChildrenAgeBoy, [_ChildrenAgeGirl] = @ChildrenAgeGirl WHERE IDSetting = @IDSetting";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ChildrenAgeBoy", txtAgeBoy.Text.Trim());
            cmd.Parameters.AddWithValue("@ChildrenAgeGirl", txtAgeGirls.Text.Trim());
            cmd.Parameters.AddWithValue("@IDSetting", 964654);
            cmd.ExecuteScalar();
            conn.Close();
            Response.Redirect("PageBeneficiaryChildren.aspx");
        }
        catch (Exception)
        {
            return;
        }
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
            if (DLAlQriahByData.SelectedValue != string.Empty)
                FGetMostafeedByChildren(1, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByChildren(0, Convert.ToInt32(0));
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
            if (DLAlQriahByData.SelectedValue != string.Empty)
                FGetMostafeedByChildren(1, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByChildren(0, Convert.ToInt32(0));
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