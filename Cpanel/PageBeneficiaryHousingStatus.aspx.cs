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

public partial class Cpanel_PageBeneficiaryHousingStatus : System.Web.UI.Page
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
            txtTitle.Text = "قائمة إحصائية المستفيدين حسب حالة المسكن ";
            //FGetNumberZero();
            FGetAlBaheth();
            pnlSelectByCheck.Visible = true;
            FGetHousingStatus();
            pnlWaiting.Visible = true;
            FGetAlQariah();
            foreach (ListItem lst in CBLType.Items) { lst.Selected = true; }
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
        }
    }

    private void FGetHousingStatus()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatAlMaskan <> @0 And IsDeleteHalatAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBLType.DataValueField = "IDItem";
            CBLType.DataTextField = "HalatAlMaskan";
            CBLType.DataSource = dt;
            CBLType.DataBind();

            DLMasderAlDkhal.Items.Clear();
            DLMasderAlDkhal.Items.Add("");
            DLMasderAlDkhal.AppendDataBoundItems = true;
            DLMasderAlDkhal.DataValueField = "IDItem";
            DLMasderAlDkhal.DataTextField = "HalatAlMaskan";
            DLMasderAlDkhal.DataSource = dt;
            DLMasderAlDkhal.DataBind();

        }
    }

    private void FGetCharByQariah()
    {
        var dataValuePairByQariah = new List<KeyValuePair<string, double>>();

        foreach (ListItem lst in CBLType.Items)
        {
            if (lst.Selected == true)
            {
                dataValuePairByQariah.Add(new KeyValuePair<string, double>(lst.Text, Convert.ToInt32(FGetCountByQariah(Convert.ToInt32(lst.Value)))));
            }
        }

        StringBuilder jsonDataByQariah = new StringBuilder();
        StringBuilder dataByQariah = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "إحصائية حسب حالة المسكن لقرية   " + DLAlQriah.SelectedItem.ToString());
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

        FusionCharts.Charts.Chart MyFirstChart = new FusionCharts.Charts.Chart("column2d", "first_chartByQariah", "100%", "315", "json", jsonDataByQariah.ToString());
        // render chart
        IDChartByQariah.Text = MyFirstChart.Render();
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
            txtTitleByQariah.Text = " قائمة إحصائية المستفيدين حسب حالة المسكن لقرية " + DLAlQriah.SelectedItem.ToString();
            int XSum = 0;
            lblvaluesTypeByQriah.Text = string.Empty;
            foreach (ListItem lst in CBLType.Items)
            {
                if (lst.Selected == true)
                {

                    lblvaluesTypeByQriah.Text += "<tr><td class='StyleTD'>" + lst.Text + "</td><td class='StyleTD'>" + FGetCountByQariah(Convert.ToInt32(lst.Value)) + "</td>";
                    XSum += Convert.ToInt32(FGetCountByQariah(Convert.ToInt32(lst.Value)));
                }
            }
            lblvaluesTypeByQriah.Text += "<tr><td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'> الإجمالي </td>";
            lblvaluesTypeByQriah.Text += "<td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'>";
            lblvaluesTypeByQriah.Text += XSum.ToString();
            lblvaluesTypeByQriah.Text += "</td></tr>";
            FGetCharByQariah();
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

    public string FGetCount(int XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountHaletAlMasken' FROM [dbo].[RasAlEstemarah] With(NoLock) Where HaletAlMasken = @0 And TypeMostafeed = @1 And IsDelete = @2", Convert.ToString(XID), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows[0]["CountHaletAlMasken"].ToString();
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    public string FGetCountByQariah(int XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountHaletAlMasken' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And HaletAlMasken = @1 And TypeMostafeed = @2 And IsDelete = @3", DLAlQriah.SelectedValue, Convert.ToString(XID), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows[0]["CountHaletAlMasken"].ToString();
        }
        else
        {
            XResult = "0";
        }
        return XResult;
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryHousingStatus.aspx");
    }


    protected void btnGetByType_Click(object sender, EventArgs e)
    {
        int XSum = 0;
        lblvaluesType.Text = string.Empty;
        foreach (ListItem lst in CBLType.Items)
        {
            if (lst.Selected == true)
            {

                lblvaluesType.Text += "<tr><td class='StyleTD'>" + lst.Text + "</td><td class='StyleTD'>" + FGetCount(Convert.ToInt32(lst.Value)) + "</td>";
                XSum += Convert.ToInt32(FGetCount(Convert.ToInt32(lst.Value)));
            }
        }
        lblvaluesType.Text += "<tr><td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'> الإجمالي </td>";
        lblvaluesType.Text += "<td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'>";
        lblvaluesType.Text += XSum.ToString();
        lblvaluesType.Text += "</td></tr>";
        FGetChartBySelect();
        pnlPrint.Visible = true;
        pnlSelectByCheck.Visible = false;
        System.Threading.Thread.Sleep(500);
    }

    private void FGetChartBySelect()
    {
        var dataValuePair = new List<KeyValuePair<string, double>>();

        foreach (ListItem lst in CBLType.Items)
        {
            if (lst.Selected == true)
            {
                dataValuePair.Add(new KeyValuePair<string, double>(lst.Text, Convert.ToInt32(FGetCount(Convert.ToInt32(lst.Value)))));
            }
        }

        StringBuilder jsonData = new StringBuilder();
        StringBuilder data = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfigAll = new Dictionary<string, string>();
        chartConfigAll.Add("caption", "إحصائية حسب حالة المسكن");
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

        FusionCharts.Charts.Chart MyFirstChart = new FusionCharts.Charts.Chart("column2d", "first_chart", "100%", "315", "json", jsonData.ToString());
        // render chart
        IDChart.Text = MyFirstChart.Render();
    }

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryHousingStatus.aspx");
    }

    protected void btnGetByAlMasder_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPrintAll.Visible = false;
            pnlByQariah.Visible = false;
            pnlData.Visible = true;
            GVMostafeedByDakhl.UseAccessibleHeader = false;
            GVMostafeedByDakhl.Columns[11].Visible = true;
            FGetMostafeedByMasderAlDhal(Convert.ToInt32(DLMasderAlDkhal.SelectedValue));
            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {

        }
    }

    private void FGetMostafeedByMasderAlDhal(int XID)
    {
        GVMostafeedByDakhl.Columns[11].Visible = true;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 * FROM [dbo].[RasAlEstemarah] With(NoLock) inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where HaletAlMasken = @0 And TypeMostafeed = @1 And RasAlEstemarah.IsDelete = @2 And Quaem.AlQriah <> @3 Order By AlQaryah", Convert.ToString(XID), "دائم", Convert.ToString(false), "مناطق_أخرى");
        if (dt.Rows.Count > 0)
        {
            txtSearchMostafeed.Text = "قائمة بيانات المستفيدين حسب حالة المسكن ( " + DLMasderAlDkhal.SelectedItem.ToString() + " ) ";
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
        FGetCountByMasder();
    }

    private void FGetCountByMasder()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where HaletAlMasken = @0 And TypeMostafeed = @1 And RasAlEstemarah.IsDelete = @2 And Quaem.AlQriah <> @3", DLMasderAlDkhal.SelectedValue, "دائم", Convert.ToString(false), "مناطق_أخرى");
        if (dt.Rows.Count > 0)
        {
            lblCountQriah.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblCountQriah.Text = "0";
        }
    }

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        GVMostafeedByDakhl.Columns[11].Visible = false;
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
        Response.Redirect("PageBeneficiaryHousingStatus.aspx");
    }

}