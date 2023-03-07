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

public partial class CResearchers_CPVillage_PageVisitReportByDevice : System.Web.UI.Page
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
            bool A50;
            A50 = Convert.ToBoolean(dtViewProfil.Rows[0]["A50"]);
            if (A50 == false)
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
            FGetAlBaheth();
            FGetAlQariah();
            txtDateFrom.Text = "01-01-2019";
            txtDateTo.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
            FGetDevice();
            pnlSelectByCheck.Visible = true;
            foreach (ListItem lst in CBLType.Items) { lst.Selected = true; }
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
        FGetHalafAlMosTafeed();
    }

    private void FGetHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDeleteHalatMostafeed = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLHalafAlMosTafeed.Items.Clear();
            DLHalafAlMosTafeed.Items.Add("");
            DLHalafAlMosTafeed.AppendDataBoundItems = true;
            DLHalafAlMosTafeed.DataValueField = "IDItem";
            DLHalafAlMosTafeed.DataTextField = "HalatMostafeed";
            DLHalafAlMosTafeed.DataSource = dt;
            DLHalafAlMosTafeed.DataBind();
        }
    }

    protected void btnGetByType_Click(object sender, EventArgs e)
    {
        txtSearchStatistic.Text = "قائمة إحتياجات المستفيدين من أجهزة كهربائية";
        int XSum = 0;
        //int XDevice = 0;
        lblvaluesType.Text = string.Empty;
        foreach (ListItem lst in CBLType.Items)
        {
            if (lst.Selected == true)
            {
                if (lst.Text != "ايتام")
                {
                    lblvaluesType.Text += "<tr><td class='StyleTD'>" + lst.Text + "</td><td class='StyleTD'>" + FGetCountDeviceAll(Convert.ToInt64(lst.Value)) + " جهاز</td>";
                    XSum += Convert.ToInt32(FGetCountDeviceAll(Convert.ToInt32(lst.Value)));
                    //XDevice += Convert.ToInt32(FGetCountDeviceAllByAosrah(Convert.ToInt32(lst.Value)));
                }
            }
        }
        lblvaluesType.Text += "<tr><td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'> إجمالي عدد الاجهزة </td>";
        lblvaluesType.Text += "<td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'>";
        lblvaluesType.Text += XSum.ToString();
        lblvaluesType.Text += " جهاز </td></tr>";
        lblvaluesType.Text += "<tr><td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'> إجمالي عدد الأسر </td>";
        lblvaluesType.Text += "<td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'>";
        lblvaluesType.Text += FGetCountDeviceAllByAosrah();
        lblvaluesType.Text += " أسره </td></tr>";
        FGetChartBySelect();
        pnlPrint.Visible = true;
        pnlSelectByCheck.Visible = false;
        System.Threading.Thread.Sleep(200);
    }

    public string FGetCountDeviceAllByAosrah()
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [IDMustafeed],[IDReport],DateReport,ReportAlZyarat.IDAdmin,RZEA.A1,RZEA.A2,RZEA.A3,RZEA.A4,RZEA.A5,RZEA.IsDelete FROM [dbo].[ReportAlZyaratElectricalAppliances] RZEA With(noLock) Inner join ReportAlZyarat on ReportAlZyarat.NumberReport = RZEA.IDReport Where ReportAlZyarat.IsDelete = @0 And ReportAlZyarat.A1 = @1 And (convert(date, DateReport) Between @2 And @3) And ((IDNumberCount <> @4)) And RZEA.IsDelete = @0", Convert.ToString(false), DLPercint.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), "0");
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows.Count.ToString();
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    private void FGetChartBySelect()
    {
        var dataValuePair = new List<KeyValuePair<string, double>>();


        foreach (ListItem lst in CBLType.Items)
        {
            if (lst.Selected == true)
            {
                dataValuePair.Add(new KeyValuePair<string, double>(lst.Text, Convert.ToInt32(FGetCountDeviceAll(Convert.ToInt32(lst.Value)))));
            }
        }

        StringBuilder jsonData = new StringBuilder();
        StringBuilder data = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfigAll = new Dictionary<string, string>();
        chartConfigAll.Add("caption", "إحصائية إحتياجات المستفيدين من أجهزة كهربائية ");
        chartConfigAll.Add("subCaption", "");
        chartConfigAll.Add("xAxisName", "");
        chartConfigAll.Add("yAxisName", "");
        chartConfigAll.Add("numberSuffix", "");
        chartConfigAll.Add("theme", "fusion");

        // json data to use as chart data source
        jsonData.Append("{'chart':{");
        foreach (var config in chartConfigAll)
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

        FusionCharts.Charts.Chart MyFirstChart = new FusionCharts.Charts.Chart("column2d", "first_chart", "100%", "300", "json", jsonData.ToString());
        // render chart
        IDChart.Text = MyFirstChart.Render();
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageVisitReportByDevice.aspx");
    }

    protected void LBPrint_Click(object sender, EventArgs e)
    {
        lblModerByStatistic.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarahByStatistic.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahathByStatistic.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();

        DLModerByStatistic.Visible = false;
        DLRaeesLagnatAlBahathByStatistic.Visible = false;
        DLRaeesMaglesAlEdarahByStatistic.Visible = false;
        lblModerByStatistic.Visible = true;
        lblRaeesLagnatAlBahathByStatistic.Visible = true;
        lblRaeesMaglesAlEdarahByStatistic.Visible = true;

        Session["footable1"] = pnlPrint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void DLModerByStatistic_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DLRaeesLagnatAlBahathByStatistic_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DLRaeesMaglesAlEdarahByStatistic_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageVisitReportByDevice.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GVVisitReport.Columns[0].Visible = false;
        GVVisitReport.Columns[11].Visible = false;
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

        GVVisitReport.UseAccessibleHeader = true;
        GVVisitReport.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVVisitReport.Columns[0].Visible = true;
        GVVisitReport.Columns[11].Visible = true;
        DLAlBaheth.Visible = true;
        DLModerAlGmeiah.Visible = true;
        DLRaeesMaglesAlEdarah.Visible = true;
        DLRaeesLagnatAlBahath.Visible = true;
        lblAlBaheth.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        lblRaeesLagnatAlBahath.Visible = false;
        GVVisitReport.UseAccessibleHeader = false;

        GetCookie();
        DataTable dtcheck = new DataTable();
        dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriah.SelectedValue, Convert.ToString(false));
        if (dtcheck.Rows.Count > 0)
        {
            FCheckSelect();
        }
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void FCheckSelect()
    {
        try
        {
            //  حسب الكل
            if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && DlVevice.SelectedValue == string.Empty)
            {
                FArnReportAlZyaratForDeviceByDate();
                FGetDeviceByAll();
            }
            // حسب القرية
            else if (DLAlQriah.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && DlVevice.SelectedValue == string.Empty)
            {
                FArnReportAlZyaratForDeviceByDateByQariah();
                FGetDeviceByQariah();
            }
            // حسب الحالة
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty && DlVevice.SelectedValue == string.Empty)
            {
                FArnReportAlZyaratForDeviceByDateByHalatMostafeed();
                FGetDeviceByHalatMostafeed();
            }
            // حسب الجهاز
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && DlVevice.SelectedValue != string.Empty)
            {
                FArnReportAlZyaratForDeviceByDateByDevice();
                FGetDeviceByQDevice();
            }
            // حسب القرية والحالة
            else if (DLAlQriah.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty && DlVevice.SelectedValue == string.Empty)
            {
                FArnReportAlZyaratForDeviceByDateByQariahAndHalatMostafeed();
                FGetDeviceByQariahAndHalatMostafeed();
            }
            // حسب القرية والجهاز
            else if (DLAlQriah.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && DlVevice.SelectedValue != string.Empty)
            {
                FArnReportAlZyaratForDeviceByDateByQariahAndDevice();
                FGetDeviceByQariahAndDevice();
            }
            // حسب القرية والجهاز
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty && DlVevice.SelectedValue != string.Empty)
            {
                FArnReportAlZyaratForDeviceByDateByHalatMostafeedAndDevice();
                FGetDeviceByHalatMostafeedAndDevice();
            }

            // حسب القرية وحالة المستفيد والجهاز
            else if (DLAlQriah.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty && DlVevice.SelectedValue != string.Empty)
            {
                FArnReportAlZyaratForDeviceByDateByQariahAndHalatMostafeedAndDevice();
                FGetDeviceByAlQariahAndHalatMostafeedAndDevice();
            }
            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {

        }
    }

    private void FArnReportAlZyaratForDeviceByDate()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDate();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة إحتياجات المستفيدين من أجهزة كهربائية حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByAll()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = false;
            RPTDeviceByDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = false;
            RPTDeviceByQariah.Visible = false;
            RPTDeviceAll.Visible = true;
            RPTDeviceAll.DataSource = dt;
            RPTDeviceAll.DataBind();
        }
    }

    public string FGetCountDeviceAll(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Where  (convert(date, DateAddDevice) Between @0 And @1) And (IDDevice = @2) And (IDNumberCount <> @3) And RZEA.IsDelete = @4", Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    public string FGetCountAosrahAll(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select DISTINCT ReportAlZyarat.NumberMostafeed, IDNumberCount As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join ReportAlZyarat on ReportAlZyarat.NumberReport = RZEA.IDReport Where ReportAlZyarat.IsDelete = @0 And ReportAlZyarat.A1 = @1 And (convert(date, DateReport) Between @2 And @3) And (IDDevice = @4) And (IDNumberCount <> @5) And RZEA.IsDelete = @5", Convert.ToString(false), DLPercint.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0");
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows.Count.ToString();
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    //حسب القرية 
    private void FArnReportAlZyaratForDeviceByDateByQariah()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDateByQariah();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = " قائمة إحتياجات المستفيدين من أجهزة كهربائية لقرية " + DLAlQriah.SelectedItem.ToString() + " حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = false;
            RPTDeviceByDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = false;
            RPTDeviceAll.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = false;
            RPTDeviceByQariah.Visible = true;
            RPTDeviceByQariah.DataSource = dt;
            RPTDeviceByQariah.DataBind();
        }
    }

    public string FGetCountDeviceByQariah(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = RZEA.IDMustafeed Where RasAlEstemarah.AlQaryah = @0 And (convert(date, DateAddDevice) Between @1 And @2) And (IDDevice = @3) And (IDNumberCount <> @4) And RZEA.IsDelete = @5", DLAlQriah.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    //حسب حالة المستفيد
    private void FArnReportAlZyaratForDeviceByDateByHalatMostafeed()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDateByHalatMostafeed();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "إحتياجات المستفيدين من أجهزة كهربائية لحالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByHalatMostafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = false;
            RPTDeviceByDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = false;
            RPTDeviceAll.Visible = false;
            RPTDeviceByQariah.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = true;
            RPTDeviceByHalatMostafeed.DataSource = dt;
            RPTDeviceByHalatMostafeed.DataBind();
        }
    }

    public string FGetCountDeviceByHalatMostafeed(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = RZEA.IDMustafeed Where RasAlEstemarah.HalafAlMosTafeed = @0 And (convert(date, DateAddDevice) Between @1 And @2) And (IDDevice = @3) And (IDNumberCount <> @4) And RZEA.IsDelete = @5", DLHalafAlMosTafeed.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    //حسب القرية وحالة المستفيد
    private void FArnReportAlZyaratForDeviceByDateByQariahAndHalatMostafeed()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
        CRAEA._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDateByQariahAndHalatMostafeed();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "إحتياجات المستفيدين من أجهزة كهربائية لقرية " + DLAlQriah.SelectedItem.ToString() + " - حالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByQariahAndHalatMostafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = false;
            RPTDeviceByDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = true;
            RPTDeviceAll.Visible = false;
            RPTDeviceByQariah.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.DataSource = dt;
            RPTDeviceByQariahAndHalatMostafeed.DataBind();
        }
    }

    public string FGetCountDeviceByQariahAndHalatMostafeed(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = RZEA.IDMustafeed Where RasAlEstemarah.AlQaryah = @0 And RasAlEstemarah.HalafAlMosTafeed = @1 And (convert(date, DateAddDevice) Between @2 And @3) And (IDDevice = @4) And (IDNumberCount <> @5) And RZEA.IsDelete = @6", DLAlQriah.SelectedValue, DLHalafAlMosTafeed.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    //حسب الجهاز 
    private void FArnReportAlZyaratForDeviceByDateByDevice()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._IDDevice = Convert.ToInt32(DlVevice.SelectedValue);
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDateByDevice();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "إحتياجات المستفيدين من أجهزة كهربائية لـ " + DlVevice.SelectedItem.ToString() + " حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByQDevice()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = false;
            RPTDeviceAll.Visible = false;
            RPTDeviceByQariah.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = false;
            RPTDeviceByDevice.Visible = true;
            RPTDeviceByDevice.DataSource = dt;
            RPTDeviceByDevice.DataBind();
        }
    }

    public string FGetCountDeviceByDevice(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA Where IDDevice = @0 And (convert(date, DateAddDevice) Between @1 And @2) And (IDDevice = @3) And (IDNumberCount <> @4) And RZEA.IsDelete = @5", DlVevice.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    //حسب القرية والجهاز
    private void FArnReportAlZyaratForDeviceByDateByQariahAndDevice()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
        CRAEA._IDDevice = Convert.ToInt32(DlVevice.SelectedValue);
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDateByQariahAndDevice();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "إحتياجات المستفيدين من أجهزة كهربائية لقرية " + DLAlQriah.SelectedItem.ToString() + " - الإحتياج ( " + DlVevice.SelectedItem.ToString() + " ) حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByQariahAndDevice()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = false;
            RPTDeviceAll.Visible = false;
            RPTDeviceByQariah.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = true;
            RPTDeviceByQariahAndDevice.DataSource = dt;
            RPTDeviceByQariahAndDevice.DataBind();
        }
    }

    public string FGetCountDeviceByQariahAndDevice(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = RZEA.IDMustafeed Where RasAlEstemarah.AlQaryah = @0 And IDDevice = @1 And (convert(date, DateAddDevice) Between @2 And @3) And (IDDevice = @4) And (IDNumberCount <> @5) And RZEA.IsDelete = @6", DLAlQriah.SelectedValue, DlVevice.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    //حسب حالة المستفيد والجهاز
    private void FArnReportAlZyaratForDeviceByDateByHalatMostafeedAndDevice()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
        CRAEA._IDDevice = Convert.ToInt32(DlVevice.SelectedValue);
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDateByHalatMostafeedAndDevice();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "إحتياجات المستفيدين من أجهزة كهربائية لحالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " - الإحتياج ( " + DlVevice.SelectedItem.ToString() + " ) حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByHalatMostafeedAndDevice()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = false;
            RPTDeviceAll.Visible = false;
            RPTDeviceByQariah.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = true;
            RPTDeviceByHalatMostafeedAndDevice.DataSource = dt;
            RPTDeviceByHalatMostafeedAndDevice.DataBind();
        }
    }

    public string FGetCountDeviceByHalatMostafeedAndDevice(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = RZEA.IDMustafeed Where RasAlEstemarah.HalafAlMosTafeed = @0 And IDDevice = @1 And (convert(date, DateAddDevice) Between @2 And @3) And (IDDevice = @4) And (IDNumberCount <> @5) And RZEA.IsDelete = @6", DLHalafAlMosTafeed.SelectedValue, DlVevice.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    //حسب القرية و حالة المستفيد والجهاز
    private void FArnReportAlZyaratForDeviceByDateByQariahAndHalatMostafeedAndDevice()
    {
        ClassReportAlZyaratElectricalAppliances CRAEA = new ClassReportAlZyaratElectricalAppliances();
        CRAEA._IDUniq = txtNameMostafeed.Text.Trim();
        CRAEA._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
        CRAEA._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
        CRAEA._IDDevice = Convert.ToInt32(DlVevice.SelectedValue);
        CRAEA._IsDelete = false;
        CRAEA._A1 = DLPercint.SelectedValue;
        CRAEA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRAEA._Null = 0;
        CRAEA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRAEA.BArnReportAlZyaratForDeviceByDateByAlQaryahAndHalafAlMosTafeedAndDevice();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "إحتياجات المستفيدين من أجهزة كهربائية لقرية " + DLAlQriah.SelectedItem.ToString() + " - حالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " - الإحتياج ( " + DlVevice.SelectedItem.ToString() + " ) حتى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FGetDeviceByAlQariahAndHalatMostafeedAndDevice()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByDevice.Visible = false;
            RPTDeviceByQariahAndHalatMostafeed.Visible = false;
            RPTDeviceAll.Visible = false;
            RPTDeviceByQariah.Visible = false;
            RPTDeviceByHalatMostafeed.Visible = false;
            RPTDeviceByQariahAndDevice.Visible = false;
            RPTDeviceByHalatMostafeedAndDevice.Visible = false;
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.Visible = true;
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.DataSource = dt;
            RPTDeviceByAlQariahAndHalatMostafeedAndDevice.DataBind();

        }
    }

    public string FGetCountDeviceByAlQariahAndHalatMostafeedAndDevice(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = RZEA.IDMustafeed Where AlQaryah = @0 And RasAlEstemarah.HalafAlMosTafeed = @1 And IDDevice = @2 And (convert(date, DateAddDevice) Between @3 And @4) And (IDDevice = @5) And (IDNumberCount <> @6) And RZEA.IsDelete = @7", DLAlQriah.SelectedValue, DLHalafAlMosTafeed.SelectedValue, DlVevice.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
            {
                XResult = dt.Rows[0]["CountItem"].ToString();
            }
            else
            {
                XResult = "0";
            }
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //DLAlBaheth.Items.Clear();
            //DLAlBaheth.Items.Add("");
            //DLAlBaheth.AppendDataBoundItems = true;
            DLAlBaheth.DataValueField = "ID_Item";
            DLAlBaheth.DataTextField = "FirstName";
            DLAlBaheth.DataSource = dt;
            DLAlBaheth.DataBind();
        }
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
        ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAlBaheth.SelectedValue));
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));

        ImgModerByStatistic.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahathByStatistic.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarahByStatistic.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    public string FGetDevice(int IDMostafeed, int IDReport, string XCheck)
    {
        string XResult = "";
        DataTable dt = new DataTable();

        if (XCheck != string.Empty)
        {
            dt = ClassDataAccess.GetData("SELECT TOP 1000 [IDItam],[IDDevice],ProductShop.ProductName,[IDNumberCount],[IDMustafeed],[IDReport],[DateAddDevice],TblRZEA.A1,TblRZEA.A2,TblRZEA.A3,TblRZEA.A4,TblRZEA.A5,TblRZEA.IsDelete FROM [dbo].[ReportAlZyaratElectricalAppliances] TblRZEA With(noLock) inner join ProductShop on ProductShop.ProductID = TblRZEA.IDDevice Where IDDevice = @0 And IDMustafeed = @1 And IDReport = @2 And IDNumberCount <> @3 And TblRZEA.IsDelete = @4", XCheck, Convert.ToString(IDMostafeed), Convert.ToString(IDReport), Convert.ToString(0), Convert.ToString(false));
        }
        else if (XCheck == string.Empty)
        {
            dt = ClassDataAccess.GetData("SELECT TOP 1000 [IDItam],[IDDevice],ProductShop.ProductName,[IDNumberCount],[IDMustafeed],[IDReport],[DateAddDevice],TblRZEA.A1,TblRZEA.A2,TblRZEA.A3,TblRZEA.A4,TblRZEA.A5,TblRZEA.IsDelete FROM [dbo].[ReportAlZyaratElectricalAppliances] TblRZEA With(noLock) inner join ProductShop on ProductShop.ProductID = TblRZEA.IDDevice Where IDMustafeed = @0 And IDReport = @1 And IDNumberCount <> @2 And TblRZEA.IsDelete = @3", Convert.ToString(IDMostafeed), Convert.ToString(IDReport), Convert.ToString(0), Convert.ToString(false));
        }

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                XResult += "<span style='font-size:11px'>" + dt.Rows[i]["ProductName"].ToString() + ":" + dt.Rows[i]["IDNumberCount"].ToString() + "</span>,";
            }

        }
        return XResult;
    }

    private void FGetDevice()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBLType.DataValueField = "ProductID";
            CBLType.DataTextField = "ProductName";
            CBLType.DataSource = dt;
            CBLType.DataBind();

            DlVevice.Items.Clear();
            DlVevice.Items.Add("");
            DlVevice.AppendDataBoundItems = true;
            DlVevice.DataValueField = "ProductID";
            DlVevice.DataTextField = "ProductName";
            DlVevice.DataSource = dt;
            DlVevice.DataBind();
        }
    }

    int tempcounter = 0;
    protected void GVVisitReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    protected void DLAlBaheth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAlBaheth.SelectedValue));
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

}