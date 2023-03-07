using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using FusionCharts.Charts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_CPVillage_PageBeneficiaryOrphans : System.Web.UI.Page
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
            FGetAitamByQriah();
            pnlWaiting.Visible = true;
            pnlSelect.Visible = true;
            FGetAlBaheth();
            FGetAlQariah();

            HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
            string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";
            DLAlQriah.SelectedValue = IDVillage;
            DLAlQriahByData.SelectedValue = DLAlQriah.SelectedValue;

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

            DLAlQriahByData.Items.Clear();
            DLAlQriahByData.Items.Add("");
            DLAlQriahByData.AppendDataBoundItems = true;
            DLAlQriahByData.DataValueField = "IDQariah";
            DLAlQriahByData.DataTextField = "AlQriah";
            DLAlQriahByData.DataSource = dt;
            DLAlQriahByData.DataBind();
        }
    }

    // عدد اسر الايتام
    //private void FGetMostafeedByHalafAlMosTafeed()
    //{
    //    DataTable dt = new DataTable();
    //    dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] Where HalafAlMosTafeed = @0 and TypeMostafeed = @1 And IsDelete = @2 Order By NameMostafeed", "27", "دائم", Convert.ToString(false));
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblNumberOser.Text = Convert.ToString(dt.Rows.Count);
    //    }
    //    lblNumberAitam.Text = ClassTarafMostafeed.FCheckAgeAll();
    //    lblNumberAitamByMale.Text = ClassTarafMostafeed.FCheckAgeByGender(2);
    //    lblNumberAitamByFeMale.Text = ClassTarafMostafeed.FCheckAgeByGender(1);
    //    FGetChart();
    //}

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
            ImgModerByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgModerByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblModerAlGmeiahbyAll.Text = dt.Rows[0]["FirstName"].ToString();
            lblModerAlGmeiahbyQariah.Text = lblModerAlGmeiahbyAll.Text;
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            ImgRaeesMaglesAlEdarahByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgRaeesMaglesAlEdarahByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblRaeesMaglesAlEdarahByAll.Text = dt.Rows[0]["FirstName"].ToString();
            lblRaeesMaglesAlEdarahByQariah.Text = lblRaeesMaglesAlEdarahByAll.Text;
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            ImgRaeesLagnatAlBahathByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgRaeesLagnatAlBahathByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblRaeesLagnatAlBahathByAll.Text = dt.Rows[0]["FirstName"].ToString();
            lblRaeesLagnatAlBahathByQariah.Text = lblRaeesLagnatAlBahathByAll.Text;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        pnlByQariah.Visible = true;
        pnlData.Visible = false;

        GetCookie();
        DataTable dtcheck = new DataTable();
        dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriah.SelectedValue, Convert.ToString(false));
        if (dtcheck.Rows.Count > 0)
        {
            if (DLAlQriah.SelectedItem.ToString() != string.Empty)
            {
                lblQriah.Visible = false;
                pnlPrintByQariah.Visible = true;
                pnlSelect.Visible = false;
                txtTitleByQariah.Text = " قائمة إحصائية المستفيدين حسب الايتام لقرية " + DLAlQriah.SelectedItem.ToString();
                FGetMostafeedByHalafAlMosTafeedByQriah();

                System.Threading.Thread.Sleep(100);
            }
            else if (DLAlQriah.SelectedItem.ToString() == string.Empty)
            {
                lblQriah.Visible = true;
                pnlPrintByQariah.Visible = false;
                pnlSelect.Visible = true;
                System.Threading.Thread.Sleep(100);
            }
        }
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    // عدد اسر الايتام حسب القرية
    private void FGetMostafeedByHalafAlMosTafeedByQriah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] Where AlQaryah = @0 And HalafAlMosTafeed = @1 and TypeMostafeed = @2 And IsDelete = @3 Order By NameMostafeed", DLAlQriah.SelectedValue, "27", "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberOserByQariah.Text = Convert.ToString(dt.Rows.Count);
        }
        lblNumberAitamByQariah.Text = ClassTarafMostafeed.FCheckAgeByQariah(Convert.ToInt32(DLAlQriah.SelectedValue));
        lblNumberAitamByMaleByQariah.Text = ClassTarafMostafeed.FCheckAgeByGender(Convert.ToInt32(DLAlQriah.SelectedValue), 2);
        lblNumberAitamByFeMaleByQariah.Text = ClassTarafMostafeed.FCheckAgeByGender(Convert.ToInt32(DLAlQriah.SelectedValue), 1);
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
        Session["footable1"] = pnlPrintByQariah;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryOrphans.aspx");
    }

    private void FGetMostafeedByOrphans(int IDCheck, int IDQariah)
    {
        GVOrphansAll.UseAccessibleHeader = false;
        ClassTarafMostafeed._IDCheck = IDCheck;
        ClassTarafMostafeed._TypeMostafeed = "دائم";
        ClassTarafMostafeed._HalafAlMosTafeed = 27;
        ClassTarafMostafeed._AlQaryah = IDQariah;
        ClassTarafMostafeed._AllowYearBoy = 16;
        ClassTarafMostafeed._AllowYearGirl = 19;
        ClassTarafMostafeed._GenderMale = 2;
        ClassTarafMostafeed._GenderFeMale = 1;
        ClassTarafMostafeed._IsDelete2 = false;
        DataTable dt = new DataTable();
        dt = ClassTarafMostafeed.BArnTarafEstemarahGetBoysAllAddvance();
        if (dt.Rows.Count > 0)
        {
            txtSearchOrphans.Text = "بيانات الايتام  دون سن (16 سنة) لقرية " + DLAlQriahByData.SelectedItem.ToString();
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

    protected void btnGetByType_Click(object sender, EventArgs e)
    {
        try
        {
            pnlByQariah.Visible = false;
            pnlData.Visible = true;

            GetCookie();
            DataTable dtcheck = new DataTable();
            dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriahByData.SelectedValue, Convert.ToString(false));
            if (dtcheck.Rows.Count > 0)
            {
                if (DLAlQriahByData.SelectedValue != string.Empty)
                {
                    FGetMostafeedByOrphans(1, Convert.ToInt32(DLAlQriahByData.SelectedValue));
                }
                //else
                //{
                //    FGetMostafeedByOrphans(0, Convert.ToInt32(0));
                //}
            }
            else
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            System.Threading.Thread.Sleep(100);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        GVOrphansAll.UseAccessibleHeader = true;
        GVOrphansAll.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlPrintAllData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryOrphans.aspx");
    }

    int tempcounter = 0;
    protected void GVOrphansAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 5)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    private void FGetAitamByQriah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.AlQaryah FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (HalafAlMosTafeed = @1) And  (((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2) And (AlQarabah = @3 or AlQarabah = @4) And (TarafEstemarah.IsDelete = @5)", "دائم", "27", "16", "1", "2", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblCountAitam.Text = dt.Rows.Count.ToString();
        }
        else
        {
            lblCountAitam.Text = "0";
        }
    }

}