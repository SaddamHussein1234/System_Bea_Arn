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

public partial class CResearchers_CPVillage_PageBeneficiarySourceOfIncome : System.Web.UI.Page
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
            //FGetNumberZero();
            FGetAlBaheth();
            FGetAlDakhlAlShahryWaMasdarah();
            pnlWaiting.Visible = true;
            FGetAlQariah();

            HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
            string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";
            DLAlQriah.SelectedValue = IDVillage;

            foreach (ListItem lst in CBLType.Items) { lst.Selected = true; }
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
    }

    private void FGetAlDakhlAlShahryWaMasdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlDakhlAlShahryWaMasdarah <> @0 And IsDeleteAlDakhlAlShahryWaMasdarah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBLType.DataValueField = "IDItem";
            CBLType.DataTextField = "AlDakhlAlShahryWaMasdarah";
            CBLType.DataSource = dt;
            CBLType.DataBind();

            DLMasderAlDkhal.Items.Clear();
            DLMasderAlDkhal.Items.Add("");
            DLMasderAlDkhal.AppendDataBoundItems = true;
            DLMasderAlDkhal.DataValueField = "IDItem";
            DLMasderAlDkhal.DataTextField = "AlDakhlAlShahryWaMasdarah";
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
        chartConfig.Add("caption", "إحصائية حسب مصدر حالات الاُسر  " + DLAlQriah.SelectedItem.ToString());
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

        FusionCharts.Charts.Chart MyFirstChart = new FusionCharts.Charts.Chart("column2d", "first_chartByQariah", "100%", "285", "json", jsonDataByQariah.ToString());
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
            lblModerAlGmeiahbyQariah.Text = dt.Rows[0]["ID_Item"].ToString();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblRaeesMaglesAlEdarahByQariah.Text = dt.Rows[0]["ID_Item"].ToString();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblRaeesLagnatAlBahathByQariah.Text = dt.Rows[0]["ID_Item"].ToString();
        }

        ImgModerByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(lblModerAlGmeiahbyQariah.Text));
        ImgRaeesLagnatAlBahathByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(lblRaeesLagnatAlBahathByQariah.Text));
        ImgRaeesMaglesAlEdarahByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(lblRaeesMaglesAlEdarahByQariah.Text));

        ImgModerByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(lblModerAlGmeiahbyQariah.Text));
        ImgRaeesLagnatAlBahathByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(lblRaeesLagnatAlBahathByQariah.Text));
        ImgRaeesMaglesAlEdarahByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(lblRaeesMaglesAlEdarahByQariah.Text));
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        pnlByQariah.Visible = true;
        pnlData.Visible = false;
        if (DLAlQriah.SelectedItem.ToString() != string.Empty)
        {
            GetCookie();
            DataTable dtcheck = new DataTable();
            dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriah.SelectedValue, Convert.ToString(false));
            if (dtcheck.Rows.Count > 0)
            {
                lblQriah.Visible = false;
                pnlPrintByQariah.Visible = true;
                pnlSelect.Visible = false;
                txtTitleByQariah.Text = " قائمة إحصائية المستفيدين حسب حالات الاُسر لقرية " + DLAlQriah.SelectedItem.ToString();
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
                System.Threading.Thread.Sleep(100);
            }
            else
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
        else if (DLAlQriah.SelectedItem.ToString() == string.Empty)
        {
            lblQriah.Visible = true;
            pnlPrintByQariah.Visible = false;
            pnlSelect.Visible = true;
            System.Threading.Thread.Sleep(100);
        }
    }

    public string FGetCount(int XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountMasderAlDakhl' FROM [dbo].[RasAlEstemarah] With(NoLock) inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where MasderAlDakhl = @0 And TypeMostafeed = @1 And RasAlEstemarah.IsDelete = @2 And Quaem.AlQriah <> @3", Convert.ToString(XID), "دائم", Convert.ToString(false), "مناطق_أخرى");
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows[0]["CountMasderAlDakhl"].ToString();
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
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountMasderAlDakhl' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And MasderAlDakhl = @1 And TypeMostafeed = @2 And IsDelete = @3", DLAlQriah.SelectedValue, Convert.ToString(XID), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows[0]["CountMasderAlDakhl"].ToString();
        }
        else
        {
            XResult = "0";
        }
        return XResult;
    }

    protected void lbPrintByQariah_Click(object sender, EventArgs e)
    {
        FGetAlBaheth();

        lblModerAlGmeiahbyQariah.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(lblModerAlGmeiahbyQariah.Text));
        lblRaeesLagnatAlBahathByQariah.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(lblRaeesLagnatAlBahathByQariah.Text));
        lblRaeesMaglesAlEdarahByQariah.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(lblRaeesMaglesAlEdarahByQariah.Text));
        

        Session["footable1"] = pnlPrintByQariah;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiarySourceOfIncome.aspx");
    }

    protected void btnGetByAlMasder_Click(object sender, EventArgs e)
    {
        try
        {
            GetCookie();
            DataTable dtcheck = new DataTable();
            dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriah.SelectedValue, Convert.ToString(false));
            if (dtcheck.Rows.Count > 0)
            {
                GVMostafeedByDakhl.Columns[11].Visible = true;
                pnlByQariah.Visible = false;
                pnlData.Visible = true;
                GVMostafeedByDakhl.UseAccessibleHeader = false;
                FGetMostafeedByMasderAlDhal(Convert.ToInt32(DLMasderAlDkhal.SelectedValue));
                System.Threading.Thread.Sleep(500);
            }
            else
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetMostafeedByMasderAlDhal(int XID)
    {
        GVMostafeedByDakhl.Columns[10].Visible = true;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 * FROM [dbo].[RasAlEstemarah] With(NoLock) inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where AlQaryah = @0 And MasderAlDakhl = @1 And TypeMostafeed = @2 And RasAlEstemarah.IsDelete = @3 And Quaem.AlQriah <> @4 Order By AlQaryah", DLAlQriah.SelectedValue, Convert.ToString(XID), "دائم", Convert.ToString(false), "مناطق_أخرى");
        if (dt.Rows.Count > 0)
        {
            txtSearchMostafeed.Text = "قائمة بيانات المستفيدين حسب مصدر دخل ( " + DLMasderAlDkhal.SelectedItem.ToString() + " ) لقرية " + DLAlQriah.SelectedItem.ToString();
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
        dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where MasderAlDakhl = @0 And TypeMostafeed = @1 And RasAlEstemarah.IsDelete = @2 And Quaem.AlQriah <> @3", DLMasderAlDkhal.SelectedValue, "دائم", Convert.ToString(false), "مناطق_أخرى");
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
        FGetAlBaheth();
        GVMostafeedByDakhl.Columns[11].Visible = false;

        GVMostafeedByDakhl.UseAccessibleHeader = true;
        GVMostafeedByDakhl.HeaderRow.TableSection = TableRowSection.TableHeader;

        lblModerAlGmeiahbyAll.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(lblModerAlGmeiahbyQariah.Text));
        lblRaeesMaglesAlEdarahByAll.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(lblRaeesMaglesAlEdarahByQariah.Text));
        lblRaeesLagnatAlBahathByAll.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(lblRaeesLagnatAlBahathByQariah.Text));

        Session["footable1"] = pnlPrintAllData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiarySourceOfIncome.aspx");
    }

}