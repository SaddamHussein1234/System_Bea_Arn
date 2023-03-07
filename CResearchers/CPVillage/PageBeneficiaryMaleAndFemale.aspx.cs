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

public partial class CResearchers_CPVillage_PageBeneficiaryMaleAndFemale : System.Web.UI.Page
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
            FGetAlBaheth();
            FGetAlQariah();

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
            ImgModerByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblModerAlGmeiahbyQariah.Text = dt.Rows[0]["FirstName"].ToString();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            ImgRaeesMaglesAlEdarahByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblRaeesMaglesAlEdarahByQariah.Text = dt.Rows[0]["FirstName"].ToString();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            ImgRaeesLagnatAlBahathByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblRaeesLagnatAlBahathByQariah.Text = dt.Rows[0]["FirstName"].ToString();
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        pnlByQariah.Visible = true;
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
                txtTitleByQariah.Text = " قائمة إحصائية المستفيدين حسب الذكور والإناث لقرية " + DLAlQriah.SelectedItem.ToString();
                FGetMostafeedByTypeStudentByQriah();

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
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    // حسب القرية
    private void FGetMostafeedByTypeStudentByQriah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(RasAlEstemarah.NumberMostafeed) As 'NumberOser' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner Join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where AlQaryah = @0 And Quaem.AlQriah <> @1 And TypeMostafeed = @2 And RasAlEstemarah.IsDelete = @3 ", DLAlQriah.SelectedValue, "مناطق_أخرى", "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberOserByQariah.Text = dt.Rows[0]["NumberOser"].ToString();
            lblNumberStudentMaleByQariah.Text = Convert.ToString(Convert.ToInt64(FGetCountByGenderByQriah(2)) + Convert.ToInt64(FGetCountByGenderOsrahByQriah(1)) - FGetCountByQriah());
            lblNumberStudentFeMaleQariah.Text = Convert.ToString(Convert.ToInt64(FGetCountByGenderByQriah(1)) + Convert.ToInt64(FGetCountByGenderByQriah(8)) + Convert.ToInt64(FGetCountByGenderOsrahByQriah(2)));
            lblSumByQriah.Text = Convert.ToString(Convert.ToInt64(lblNumberStudentMaleByQariah.Text) + Convert.ToInt64(lblNumberStudentFeMaleQariah.Text));
        }
        FGetChartByQariah();
    }

    // جلب عدد أسر الايتام
    public int FGetCountByQriah()
    {
        int XResult = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountTypeMostafeed' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And HalafAlMosTafeed = @1 And TypeMostafeed = @2 And IsDelete = @3", DLAlQriah.SelectedValue, Convert.ToString(27), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = Convert.ToInt32(dt.Rows[0]["CountTypeMostafeed"]);
        }
        else
        {
            XResult = 0;
        }
        return XResult;
    }

    // حسب القرية
    private string FGetCountByGenderByQriah(int XGender)
    {
        string XType = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(NumberMostafeed) As 'CountStudents' FROM [dbo].[TarafEstemarah] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where RasAlEstemarah.AlQaryah = @0 And RasAlEstemarah.TypeMostafeed = @1 And RasAlEstemarah.IsDelete = @2 And AlQarabah = @3 And TarafEstemarah.IsDelete = @4 ", DLAlQriah.SelectedValue, "دائم", Convert.ToString(false), Convert.ToString(XGender), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XType = dt.Rows[0]["CountStudents"].ToString();
        }
        return XType;
    }

    // حسب القرية
    private string FGetCountByGenderOsrahByQriah(int XGender)
    {
        string XType = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count(*) As 'CountGender' FROM [dbo].[RasAlEstemarah] With(NoLock) Where AlQaryah = @0 And Gender = @1 And TypeMostafeed = @2 And IsDelete = @3 ", DLAlQriah.SelectedValue, Convert.ToString(XGender), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XType = dt.Rows[0]["CountGender"].ToString();
        }
        return XType;
    }

    private void FGetChartByQariah()
    {
        var dataValuePairByQariah = new List<KeyValuePair<string, double>>();

        dataValuePairByQariah.Add(new KeyValuePair<string, double>("عدد الاُسر", Convert.ToInt32(lblNumberOserByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("عدد الذكور", Convert.ToInt32(lblNumberStudentMaleByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("الايتام الإناث", Convert.ToInt32(lblNumberStudentFeMaleQariah.Text)));

        StringBuilder jsonDataByQariah = new StringBuilder();
        StringBuilder dataByQariah = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "إحصائية حسب الذكور والإناث لقرية " + DLAlQriah.SelectedItem.ToString());
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
        Response.Redirect("PageBeneficiaryMaleAndFemale.aspx");
    }

}