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

public partial class Cpanel_PageBeneficiaryOrphans : System.Web.UI.Page
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
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAitamByQriah("All");
            pnlWaiting.Visible = true;
            pnlSelect.Visible = true;
            string XAgeBoy = string.Empty, XAgeGirl = string.Empty;
            XAgeBoy = ClassSetting.FGetAgeBoy(); XAgeGirl = ClassSetting.FGetAgeGirl();
            txtAgeBoy.Text = XAgeBoy; txtAgeGirls.Text = XAgeGirl;
            txtTitle.Text = "قائمة إحصائية المستفيدين حسب الايتام دون سن (" + XAgeBoy + " سنة للذكور ) و ( " + XAgeGirl + " سنة للإناث )";
            FGetMostafeedByHalafAlMosTafeed("All");
            pnlPrint.Visible = true;
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
            DLAlQriah.Items.Clear();
            DLAlQriah.Items.Add("");
            DLAlQriah.AppendDataBoundItems = true;
            DLAlQriah.DataValueField = "IDItem";
            DLAlQriah.DataTextField = "AlQriah";
            DLAlQriah.DataSource = dt;
            DLAlQriah.DataBind();

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
            dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (HalafAlMosTafeed = @1) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2) And AlQarabah = @3) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @4) And AlQarabah = @5)) And (TarafEstemarah.IsDelete = @6)",
                DLTypeMostafeed.SelectedValue, "27", ClassSetting.FGetAgeBoy(), "2", ClassSetting.FGetAgeGirl(), "1", Convert.ToString(false));
        else
            dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (HalafAlMosTafeed = @1) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2) And AlQarabah = @3) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @4) And AlQarabah = @5)) And (TarafEstemarah.IsDelete = @6) And TarafEstemarah.[_Is_Print_Hide_] = @6",
                    DLTypeMostafeed.SelectedValue, "27", ClassSetting.FGetAgeBoy(), "2", ClassSetting.FGetAgeGirl(), "1", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberOser.Text = Convert.ToString(dt.Rows.Count);
            lblCountAoser.Text = lblNumberOser.Text;
        }
        lblNumberAitam.Text = ClassTarafMostafeed.FCheckAgeAll();
        lblNumberAitamByMale.Text = ClassTarafMostafeed.FCheckAgeByGender(2);
        lblNumberAitamByFeMale.Text = ClassTarafMostafeed.FCheckAgeByGender(1);
        FGetChart();
    }

    private void FGetChart()
    {
        var dataValuePair = new List<KeyValuePair<string, double>>();

        dataValuePair.Add(new KeyValuePair<string, double>("عدد الاُسر", Convert.ToInt32(lblNumberOser.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("عدد الايتام", Convert.ToInt32(lblNumberAitam.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("الايتام الذكور", Convert.ToInt32(lblNumberAitamByMale.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("الايتام الإناث", Convert.ToInt32(lblNumberAitamByFeMale.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("القرى", Convert.ToInt32(lblNumberQriahByFeMale.Text)));
        StringBuilder jsonData = new StringBuilder();
        StringBuilder data = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "رسم بياني حسب الايتام ");
        chartConfig.Add("subCaption", "");
        chartConfig.Add("xAxisName", "");
        chartConfig.Add("yAxisName", "");
        chartConfig.Add("numberSuffix", "");
        chartConfig.Add("theme", "fusion");

        // json data to use as chart data source
        jsonData.Append("{'chart':{");
        foreach (var config in chartConfig)
        {
            jsonData.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
        }
        jsonData.Replace(",", "},", jsonData.Length - 1, 1);

        // build  data object from label-value pair
        data.Append("'data':[");

        foreach (KeyValuePair<string, double> pair in dataValuePair)
        {
            data.AppendFormat("{{'label':'{0}','value':'{1}'}},", pair.Key, pair.Value);
        }
        data.Replace(",", "]", data.Length - 1, 1);

        jsonData.Append(data.ToString());
        jsonData.Append("}");
        //Create chart instance
        // charttype, chartID, width, height, data format, data

        Chart MyFirstChart = new Chart("column2d", "first_chart", "100%", "200", "json", jsonData.ToString());
        // render chart
        IDChart.Text = MyFirstChart.Render();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["footable1"] = pnlPrint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        pnlPrintAll.Visible = false;
        pnlByQariah.Visible = true;
        pnlData.Visible = false;
        if (DLAlQriah.SelectedItem.ToString() != string.Empty)
        {
            lblQriah.Visible = false;
            pnlPrintByQariah.Visible = true;
            pnlSelect.Visible = false;
            txtTitleByQariah.Text = "قائمة إحصائية المستفيدين حسب الايتام دون سن (" + ClassSetting.FGetAgeBoy() + " سنة للذكور ) و ( " + ClassSetting.FGetAgeGirl() + " سنة للإناث ) لقرية " + DLAlQriah.SelectedItem.ToString() + "";
            FGetMostafeedByHalafAlMosTafeedByQriah();

            System.Threading.Thread.Sleep(500);
        }
        else if (DLAlQriah.SelectedItem.ToString() == string.Empty)
        {
            lblQriah.Visible = true;
            pnlPrintByQariah.Visible = false;
            pnlSelect.Visible = true;
            System.Threading.Thread.Sleep(200);
        }
    }

    // عدد اسر الايتام حسب القرية
    private void FGetMostafeedByHalafAlMosTafeedByQriah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where AlQaryah = @0 And (TypeMostafeed = @1) And (HalafAlMosTafeed = @2) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @3) And AlQarabah = @4) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @5) And AlQarabah = @6)) And (TarafEstemarah.IsDelete = @7)",
            DLAlQriah.SelectedValue, DLTypeMostafeed.SelectedValue, "27", ClassSetting.FGetAgeBoy(), "2", ClassSetting.FGetAgeGirl(), "1", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            lblNumberOserByQariah.Text = Convert.ToString(dt.Rows.Count);
        lblNumberAitamByQariah.Text = ClassTarafMostafeed.FCheckAgeByQariah(Convert.ToInt32(DLAlQriah.SelectedValue));
        lblNumberAitamByMaleByQariah.Text = ClassTarafMostafeed.FCheckAgeByGender(Convert.ToInt32(DLAlQriah.SelectedValue),2);
        lblNumberAitamByFeMaleByQariah.Text = ClassTarafMostafeed.FCheckAgeByGender(Convert.ToInt32(DLAlQriah.SelectedValue),1);
        FGetChartByQariah();
    }

    private void FGetChartByQariah()
    {
        var dataValuePairByQariah = new List<KeyValuePair<string, double>>();

        dataValuePairByQariah.Add(new KeyValuePair<string, double>("عدد الاُسر", Convert.ToInt32(lblNumberOserByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("عدد الايتام", Convert.ToInt32(lblNumberAitamByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("الايتام الذكور", Convert.ToInt32(lblNumberAitamByMaleByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("الايتام الإناث", Convert.ToInt32(lblNumberAitamByFeMaleByQariah.Text)));

        StringBuilder jsonDataByQariah = new StringBuilder();
        StringBuilder dataByQariah = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "إحصائية حسب الايتام لقرية " + DLAlQriah.SelectedItem.ToString());
        chartConfig.Add("subCaption", "");
        chartConfig.Add("xAxisName", "");
        chartConfig.Add("yAxisName", "");
        chartConfig.Add("numberSuffix", "");
        chartConfig.Add("theme", "fusion");

        // json data to use as chart data source
        jsonDataByQariah.Append("{'chart':{");
        foreach (var config in chartConfig)
        {
            jsonDataByQariah.AppendFormat("'{0}':'{1}',", config.Key, config.Value);
        }
        jsonDataByQariah.Replace(",", "},", jsonDataByQariah.Length - 1, 1);

        // build  data object from label-value pair
        dataByQariah.Append("'data':[");

        foreach (KeyValuePair<string, double> pair in dataValuePairByQariah)
        {
            dataByQariah.AppendFormat("{{'label':'{0}','value':'{1}'}},", pair.Key, pair.Value);
        }
        dataByQariah.Replace(",", "]", dataByQariah.Length - 1, 1);

        jsonDataByQariah.Append(dataByQariah.ToString());
        jsonDataByQariah.Append("}");
        //Create chart instance
        // charttype, chartID, width, height, data format, data

        Chart MyFirstChart = new Chart("column2d", "first_chartByQariah", "100%", "170", "json", jsonDataByQariah.ToString());
        // render chart
        IDChartByQariah.Text = MyFirstChart.Render();
    }

    protected void lbPrintByQariah_Click(object sender, EventArgs e)
    {
        try
        {
            Session["footable1"] = pnlPrintByQariah;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryOrphans.aspx");
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void FGetMostafeedByOrphans(int IDCheck , int IDQariah)
    {
        int XAgeBoy = 0, XAgeGirl = 0;
        XAgeBoy = Int32.Parse(ClassSetting.FGetAgeBoy());
        XAgeGirl = Int32.Parse(ClassSetting.FGetAgeGirl());

        GVOrphansAll.UseAccessibleHeader = false;
        ClassTarafMostafeed._IDCheck = IDCheck;
        ClassTarafMostafeed._TypeMostafeed = DLTypeMostafeed.SelectedValue;
        ClassTarafMostafeed._HalafAlMosTafeed = 27;
        ClassTarafMostafeed._AlQaryah = IDQariah;
        ClassTarafMostafeed._AllowYearBoy = XAgeBoy;
        ClassTarafMostafeed._AllowYearGirl = XAgeGirl;
        ClassTarafMostafeed._GenderMale = 2;
        ClassTarafMostafeed._GenderFeMale = 1;
        ClassTarafMostafeed._IsDelete2 = false;
        DataTable dt = new DataTable();
        dt = ClassTarafMostafeed.BArnTarafEstemarahGetBoysAllAddvance();
        if (dt.Rows.Count > 0)
        {
            txtSearchOrphans.Text = "بيانات الأيتام  دون سن ("+ XAgeBoy.ToString() + " سنة للذكور ) و ( "+ XAgeGirl.ToString() + " سنة للإناث )";
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
                FGetMostafeedByOrphans(100, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByOrphans(10, Convert.ToInt32(0));

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
        Response.Redirect("PageBeneficiaryOrphans.aspx");
    }

    private void FGetAitamByQriah(string XCheck)
    {
        DataTable dt = new DataTable();
        if(XCheck == "All")
        dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (HalafAlMosTafeed = @1) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2) And AlQarabah = @3) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @4) And AlQarabah = @5)) And (TarafEstemarah.IsDelete = @6)",
            DLTypeMostafeed.SelectedValue, "27", ClassSetting.FGetAgeBoy(), "2", ClassSetting.FGetAgeGirl(), "1", Convert.ToString(false));
        else
            dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (HalafAlMosTafeed = @1) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2) And AlQarabah = @3) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @4) And AlQarabah = @5)) And (TarafEstemarah.IsDelete = @6) And TarafEstemarah.[_Is_Print_Hide_] = @6",
                DLTypeMostafeed.SelectedValue, "27", ClassSetting.FGetAgeBoy(), "2", ClassSetting.FGetAgeGirl(), "1", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblCountAitam.Text = dt.Rows.Count.ToString();
            lblNumberQriahByFeMale.Text = lblCountAitam.Text;
        }
        else
        {
            lblCountAitam.Text = "0";
            lblNumberQriahByFeMale.Text = lblCountAitam.Text;
        }
    }
    
    protected void btnGetByType_Click(object sender, EventArgs e)
    {
        try
        {
            GVOrphansAll.Columns[0].Visible = true;

            pnlPrintAll.Visible = false;
            pnlByQariah.Visible = false;
            pnlData.Visible = true;
            FGetAitamByQriah("All");
            FGetMostafeedByHalafAlMosTafeed("All");
            if (DLAlQriahByData.SelectedValue != string.Empty)
                FGetMostafeedByOrphans(1, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByOrphans(0, Convert.ToInt32(0));

            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryOrphans.aspx");
    }

    protected void LBEditAge_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[SettingTable] SET [_AgeBoy] = @AgeBoy, [_AgeGirl] = @AgeGirl WHERE IDSetting = @IDSetting";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@AgeBoy", txtAgeBoy.Text.Trim());
            cmd.Parameters.AddWithValue("@AgeGirl", txtAgeGirls.Text.Trim());
            cmd.Parameters.AddWithValue("@IDSetting", 964654);
            cmd.ExecuteScalar();
            conn.Close();
            Response.Redirect("PageBeneficiaryOrphans.aspx");
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
                FGetMostafeedByOrphans(1, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByOrphans(0, Convert.ToInt32(0));
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
                FGetMostafeedByOrphans(1, Convert.ToInt32(DLAlQriahByData.SelectedValue));
            else
                FGetMostafeedByOrphans(0, Convert.ToInt32(0));
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