using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FusionCharts.Charts;

public partial class Cpanel_PageBeneficiaryStatistic : System.Web.UI.Page
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
            pnlSelect.Visible = true;
            txtTitle.Text = "قائمة إحصائية المستفيدين حسب الدخل الشهري ";
            FGetNumberZero();
            FGetAlBaheth();
            pnlPrint.Visible = true;
            pnlWaiting.Visible = true;
            FGetChart();
        }
    }

    private void FGetChart()
    {
        var dataValuePair = new List<KeyValuePair<string, double>>();

        dataValuePair.Add(new KeyValuePair<string, double>("= 0", Convert.ToInt32(lblNumberZero.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("1 - 1500", Convert.ToInt32(lblNumberbetween1To1500.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("1501-3000", Convert.ToInt32(lblNumberbetween1501To3000.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("3001-4000", Convert.ToInt32(lblNumberbetween3001To4000.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("4001>", Convert.ToInt32(lblNumberbetween4001To20000.Text)));

        StringBuilder jsonData = new StringBuilder();
        StringBuilder data = new StringBuilder();
        // store chart config name-config value pair
        
        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "إحصائية حسب الدخل الشهري ");
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

        Chart MyFirstChart = new Chart("column2d", "first_chart", "100%", "225", "json", jsonData.ToString());
        // render chart
        IDChart.Text = MyFirstChart.Render();
    }

    private void FGetAlBaheth()
    {
        //DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        //if (dt.Rows.Count > 0)
        //{
        //    //DLAlBaheth.Items.Clear();
        //    //DLAlBaheth.Items.Add("");
        //    //DLAlBaheth.AppendDataBoundItems = true;
        //    DLAlBaheth.DataValueField = "ID_Item";
        //    DLAlBaheth.DataTextField = "FirstName";
        //    DLAlBaheth.DataSource = dt;
        //    DLAlBaheth.DataBind();
        //}
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
        //ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAlBaheth.SelectedValue));
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));

        ImgModerByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahathByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarahByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    private void FGetNumberZero()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountZero' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlDakhlAlShahryllMostafeed = @0 And TypeMostafeed = @1 And IsDelete = @2", Convert.ToString(0), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberZero.Text = dt.Rows[0]["CountZero"].ToString();
        }
        else
        {
            lblNumberZero.Text = "0";
        }
        FGetNumberbetween1To1500();
    }

    private void FGetNumberbetween1To1500()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween1To1500' FROM [dbo].[RasAlEstemarah] With(NoLock) Where (AlDakhlAlShahryllMostafeed between @0 And @1) And TypeMostafeed = @2 And IsDelete = @3", Convert.ToString(1), Convert.ToString(1500), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween1To1500.Text = dt.Rows[0]["Countbetween1To1500"].ToString();
        }
        else
        {
            lblNumberbetween1To1500.Text = "0";
        }
        FGetNumberbetween1501To3000();
    }

    private void FGetNumberbetween1501To3000()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween1501To3000' FROM [dbo].[RasAlEstemarah] With(NoLock) Where (AlDakhlAlShahryllMostafeed between @0 And @1) And TypeMostafeed = @2 And IsDelete = @3", Convert.ToString(1501), Convert.ToString(3000), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween1501To3000.Text = dt.Rows[0]["Countbetween1501To3000"].ToString();
        }
        else
        {
            lblNumberbetween1501To3000.Text = "0";
        }
        FGetNumberbetween3001To4000();
    }

    private void FGetNumberbetween3001To4000()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween3001To4000' FROM [dbo].[RasAlEstemarah] With(NoLock) Where (AlDakhlAlShahryllMostafeed between @0 And @1) And TypeMostafeed = @2 And IsDelete = @3", Convert.ToString(3001), Convert.ToString(4000), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween3001To4000.Text = dt.Rows[0]["Countbetween3001To4000"].ToString();
        }
        else
        {
            lblNumberbetween3001To4000.Text = "0";
        }
        FGetNumberbetween4001To20000();
    }

    private void FGetNumberbetween4001To20000()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween4001To20000' FROM [dbo].[RasAlEstemarah] With(NoLock) Where (AlDakhlAlShahryllMostafeed between @0 And @1) And TypeMostafeed = @2 And IsDelete = @3", Convert.ToString(4001), Convert.ToString(20000), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween4001To20000.Text = dt.Rows[0]["Countbetween4001To20000"].ToString();
        }
        else
        {
            lblNumberbetween4001To20000.Text = "0";
        }
        lblSum.Text = Convert.ToString(Convert.ToInt64(lblNumberZero.Text) + Convert.ToInt64(lblNumberbetween1To1500.Text) + Convert.ToInt64(lblNumberbetween1501To3000.Text) + Convert.ToInt64(lblNumberbetween3001To4000.Text) + Convert.ToInt64(lblNumberbetween4001To20000.Text));
        FGetAlQariah();
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
        }
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
            txtTitleByQariah.Text = " قائمة إحصائية المستفيدين حسب مصدر الدخل لقرية " + DLAlQriah.SelectedItem.ToString();
            FGetNumberZeroByQariah();
            
            System.Threading.Thread.Sleep(500);
        }
        else if (DLAlQriah.SelectedItem.ToString() == string.Empty)
        {
            lblQriah.Visible = true;
            pnlPrintByQariah.Visible = false;
            pnlSelect.Visible = true;
            System.Threading.Thread.Sleep(500);
        }
    }

    private void FGetChartByQariah()
    {
        var dataValuePairByQariah = new List<KeyValuePair<string, double>>();

        dataValuePairByQariah.Add(new KeyValuePair<string, double>("= 0", Convert.ToInt32(lblNumberZeroByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("1 - 1500", Convert.ToInt32(lblNumberbetween1To1500ByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("1501-3000", Convert.ToInt32(lblNumberbetween1501To3000ByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("3001-4000", Convert.ToInt32(lblNumberbetween3001To4000ByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("4001>", Convert.ToInt32(lblNumberbetween4001To20000ByQariah.Text)));

        StringBuilder jsonDataByQariah = new StringBuilder();
        StringBuilder dataByQariah = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "إحصائية حسب الدخل الشهري لقرية " + DLAlQriah.SelectedItem.ToString());
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

        Chart MyFirstChart = new Chart("column2d", "first_chartByQariah", "100%", "225", "json", jsonDataByQariah.ToString());
        // render chart
        IDChartByQariah.Text = MyFirstChart.Render();
    }

    private void FGetNumberZeroByQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountZero' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And AlDakhlAlShahryllMostafeed = @1 And TypeMostafeed = @2 And IsDelete = @3",DLAlQriah.SelectedValue, Convert.ToString(0), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberZeroByQariah.Text = dt.Rows[0]["CountZero"].ToString();
        }
        else
        {
            lblNumberZeroByQariah.Text = "0";
        }
        FGetNumberbetween1To1500ByQariah();
    }

    private void FGetNumberbetween1To1500ByQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween1To1500' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And (AlDakhlAlShahryllMostafeed between @1 And @2) And TypeMostafeed = @3 And IsDelete = @4",DLAlQriah.SelectedValue, Convert.ToString(1), Convert.ToString(1500), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween1To1500ByQariah.Text = dt.Rows[0]["Countbetween1To1500"].ToString();
        }
        else
        {
            lblNumberbetween1To1500ByQariah.Text = "0";
        }
        FGetNumberbetween1501To3000ByQariah();
    }

    private void FGetNumberbetween1501To3000ByQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween1501To3000' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And (AlDakhlAlShahryllMostafeed between @1 And @2) And TypeMostafeed = @3 And IsDelete = @4", DLAlQriah.SelectedValue, Convert.ToString(1501), Convert.ToString(3000), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween1501To3000ByQariah.Text = dt.Rows[0]["Countbetween1501To3000"].ToString();
        }
        else
        {
            lblNumberbetween1501To3000ByQariah.Text = "0";
        }
        FGetNumberbetween3001To4000ByQariah();
    }

    private void FGetNumberbetween3001To4000ByQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween3001To4000' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And (AlDakhlAlShahryllMostafeed between @1 And @2) And TypeMostafeed = @3 And IsDelete = @4", DLAlQriah.SelectedValue, Convert.ToString(3001), Convert.ToString(4000), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween3001To4000ByQariah.Text = dt.Rows[0]["Countbetween3001To4000"].ToString();
        }
        else
        {
            lblNumberbetween3001To4000ByQariah.Text = "0";
        }
        FGetNumberbetween4001To20000ByQariah();
    }

    private void FGetNumberbetween4001To20000ByQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'Countbetween4001To20000' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And (AlDakhlAlShahryllMostafeed between @1 And @2) And TypeMostafeed = @3 And IsDelete = @4", DLAlQriah.SelectedValue, Convert.ToString(4001), Convert.ToString(20000), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberbetween4001To20000ByQariah.Text = dt.Rows[0]["Countbetween4001To20000"].ToString();
        }
        else
        {
            lblNumberbetween4001To20000ByQariah.Text = "0";
        }
        lblSumByQariah.Text = Convert.ToString(Convert.ToInt64(lblNumberZeroByQariah.Text) + Convert.ToInt64(lblNumberbetween1To1500ByQariah.Text) + Convert.ToInt64(lblNumberbetween1501To3000ByQariah.Text) + Convert.ToInt64(lblNumberbetween3001To4000ByQariah.Text) + Convert.ToInt64(lblNumberbetween4001To20000ByQariah.Text));
        FGetChartByQariah();
    }
    
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
        DLAlBaheth.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        DLRaeesLagnatAlBahath.Visible = false;
        lblAlBaheth.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;
        lblRaeesLagnatAlBahath.Visible = true;

        Session["footable1"] = pnlPrint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryStatistic.aspx");
    }
    
    protected void lbPrintByQariah_Click(object sender, EventArgs e)
    {
        lblModerAlGmeiahbyQariah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarahByQariah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahathByQariah.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
        ImgModerByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahathByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarahByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
        Session["footable1"] = pnlPrintByQariah;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    private void FGetMostafeedByMasderAlDhal(int XMin , int XMax)
    {
        GVMostafeedByDakhl.Columns[10].Visible = true;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 * FROM [dbo].[RasAlEstemarah] With(NoLock) inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where (AlDakhlAlShahryllMostafeed between @0 And @1) And TypeMostafeed = @2 And RasAlEstemarah.IsDelete = @3 And Quaem.AlQriah <> @4 Order by AlQaryah", Convert.ToString(XMin), Convert.ToString(XMax), "دائم", Convert.ToString(false), "مناطق_أخرى");
        if (dt.Rows.Count > 0)
        {
            txtSearchMostafeed.Text = "قائمة بيانات المستفيدين حسب الدخل الشهري الذي يتراوح مابين "+ txtMasderAlDkhalMinimum.Text.Trim() + "ريال إلى " + txtMasderAlDkhalMaxiMam.Text.Trim() + " ريال ";
            GVMostafeedByDakhl.DataSource = dt;
            GVMostafeedByDakhl.DataBind();
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
        FGetCountByMasder(XMin, XMax);
    }

    private void FGetCountByMasder(int XMin, int XMax)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where (AlDakhlAlShahryllMostafeed between @0 And @1) And TypeMostafeed = @2 And RasAlEstemarah.IsDelete = @3 And Quaem.AlQriah <> @4", Convert.ToString(XMin), Convert.ToString(XMax), "دائم", Convert.ToString(false), "مناطق_أخرى");
        if (dt.Rows.Count > 0)
        {
            lblCountQriah.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblCountQriah.Text = "0";
        }
    }

    protected void btnGetByAlMasder_Click(object sender, EventArgs e)
    {
        try
        {
            GVMostafeedByDakhl.Columns[13].Visible = true;
            pnlPrintAll.Visible = false;
            pnlByQariah.Visible = false;
            pnlData.Visible = true;
            GVMostafeedByDakhl.UseAccessibleHeader = false;
            FGetMostafeedByMasderAlDhal(Convert.ToInt32(txtMasderAlDkhalMinimum.Text.Trim()), Convert.ToInt32(txtMasderAlDkhalMaxiMam.Text.Trim()));
            System.Threading.Thread.Sleep(500);

        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        GVMostafeedByDakhl.Columns[13].Visible = false;
        lblModerAlGmeiahbyAll.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarahByAll.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahathByAll.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();

        GVMostafeedByDakhl.UseAccessibleHeader = true;
        GVMostafeedByDakhl.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlPrintAllData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryStatistic.aspx");
    }
    
    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryStatistic.aspx");
    }

}