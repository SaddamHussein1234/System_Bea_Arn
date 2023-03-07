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

public partial class Cpanel_PageBeneficiaryEducationalSituations : System.Web.UI.Page
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
            //FGetAitamByQriah();
            pnlWaiting.Visible = true;
            pnlSelect.Visible = true;
            txtTitle.Text = " قائمة إحصائية المستفيدين حسب الحالات التعليمية ";
            FGetMostafeedByHalafAlMosTafeed();
            FGetAlBaheth();
            pnlPrint.Visible = true;
            FGetAlQariah();
            //FGetMostafeedByOrphans();
        }
    }

    // عدد جميع الاُسر
    private void FGetMostafeedByHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(RasAlEstemarah.NumberMostafeed) As 'NumberOser' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner Join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where Quaem.AlQriah <> @0 And TypeMostafeed = @1 And RasAlEstemarah.IsDelete = @2 ", "مناطق_أخرى", "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberOser.Text = dt.Rows[0]["NumberOser"].ToString();
            lblNumberStudentMale.Text = FGetCountStudentByGender("طالب");
            lblNumberStudentFeMale.Text = FGetCountStudentByGender("طالبة");
            lblSum.Text = Convert.ToString(Convert.ToInt64(lblNumberStudentMale.Text) + Convert.ToInt64(lblNumberStudentFeMale.Text));
        }

        FGetChart();
    }

    private string FGetCountStudentByGender(string XGender)
    {
        string XType = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(NumberMostafeed) As 'CountStudents' FROM [dbo].[TarafEstemarah] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where RasAlEstemarah.TypeMostafeed = @0 And RasAlEstemarah.IsDelete = @1 And AlMehnahAlHaliah = @2 And TarafEstemarah.IsDelete = @3 ", "دائم", Convert.ToString(false), XGender, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XType = dt.Rows[0]["CountStudents"].ToString();
        }
        return XType;
    }

    private void FGetChart()
    {
        var dataValuePair = new List<KeyValuePair<string, double>>();

        dataValuePair.Add(new KeyValuePair<string, double>("عدد الاُسر", Convert.ToInt32(lblNumberOser.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("عدد الطلاب", Convert.ToInt32(lblNumberStudentMale.Text)));
        dataValuePair.Add(new KeyValuePair<string, double>("عدد الطالبات", Convert.ToInt32(lblNumberStudentFeMale.Text)));
        StringBuilder jsonData = new StringBuilder();
        StringBuilder data = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "رسم بياني حسب الحالات التعليمية ");
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

        Chart MyFirstChart = new Chart("column2d", "first_chart", "100%", "170", "json", jsonData.ToString());
        // render chart
        IDChart.Text = MyFirstChart.Render();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["footable1"] = pnlPrint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryEducationalSituations.aspx");
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
            ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgModerByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgModerByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblModerAlGmeiah.Text = dt.Rows[0]["FirstName"].ToString();
            lblModerAlGmeiahbyAll.Text = lblModerAlGmeiah.Text;
            lblModerAlGmeiahbyQariah.Text = lblModerAlGmeiah.Text;
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgRaeesMaglesAlEdarahByAll.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgRaeesMaglesAlEdarahByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblRaeesMaglesAlEdarah.Text = dt.Rows[0]["FirstName"].ToString();
            lblRaeesMaglesAlEdarahByAll.Text = lblRaeesMaglesAlEdarah.Text;
            lblRaeesMaglesAlEdarahByQariah.Text = lblRaeesMaglesAlEdarah.Text;
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
            ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            ImgRaeesLagnatAlBahathByQariah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Item"]));
            lblRaeesLagnatAlBahath.Text = dt.Rows[0]["FirstName"].ToString();
            lblRaeesLagnatAlBahathByAll.Text = lblRaeesLagnatAlBahath.Text;
            lblRaeesLagnatAlBahathByQariah.Text = lblRaeesLagnatAlBahath.Text;
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
            txtTitleByQariah.Text = " قائمة إحصائية المستفيدين حسب الحالات التعليمية لقرية " + DLAlQriah.SelectedItem.ToString();
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

    // عدد اسر حسب القرية
    private void FGetMostafeedByTypeStudentByQriah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(RasAlEstemarah.NumberMostafeed) As 'NumberOser' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner Join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where Quaem.AlQriah <> @0 And AlQaryah = @1 And TypeMostafeed = @2 And RasAlEstemarah.IsDelete = @3", "مناطق_أخرى", DLAlQriah.SelectedValue, "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberOserByQariah.Text = dt.Rows[0]["NumberOser"].ToString();
        }
        lblNumberStudentMaleByQariah.Text = FGetCountStudentByGenderByQariah("طالب");
        lblNumberStudentFeMaleQariah.Text = FGetCountStudentByGenderByQariah("طالبة");
        lblSumByQriah.Text = Convert.ToString(Convert.ToInt64(lblNumberStudentMaleByQariah.Text) + Convert.ToInt64(lblNumberStudentFeMaleQariah.Text));
        FGetChartByQariah();
    }

    private void FGetChartByQariah()
    {
        var dataValuePairByQariah = new List<KeyValuePair<string, double>>();

        dataValuePairByQariah.Add(new KeyValuePair<string, double>("عدد الاُسر", Convert.ToInt32(lblNumberOserByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("عدد الطلاب", Convert.ToInt32(lblNumberStudentMaleByQariah.Text)));
        dataValuePairByQariah.Add(new KeyValuePair<string, double>("الايتام الطالبات", Convert.ToInt32(lblNumberStudentFeMaleQariah.Text)));

        StringBuilder jsonDataByQariah = new StringBuilder();
        StringBuilder dataByQariah = new StringBuilder();
        // store chart config name-config value pair

        Dictionary<string, string> chartConfig = new Dictionary<string, string>();
        chartConfig.Add("caption", "إحصائية حسب الحالات التعليمية لقرية " + DLAlQriah.SelectedItem.ToString());
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
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryEducationalSituations.aspx");
    }

    private string FGetCountStudentByGenderByQariah(string XGender)
    {
        string XType = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(NumberMostafeed) As 'CountStudents' FROM [dbo].[TarafEstemarah] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where RasAlEstemarah.TypeMostafeed = @0 And RasAlEstemarah.AlQaryah = @1 And RasAlEstemarah.IsDelete = @2 And AlMehnahAlHaliah = @3 And TarafEstemarah.IsDelete = @4 ", "دائم", DLAlQriah.SelectedValue, Convert.ToString(false), XGender, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XType = dt.Rows[0]["CountStudents"].ToString();
        }
        return XType;
    }

    protected void btnGetByType_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPrintAll.Visible = false;
            pnlByQariah.Visible = false;
            pnlData.Visible = true;
            if (DLAlQriahByData.Text == string.Empty)
            {
                if (RBMale.Checked == true && RBFeMale.Checked == false)
                {
                    FGetMostafeedByOrphans(0, 0, "طالب");
                    txtSearchOrphans.Text = "قائمة بيانات جميع الطلاب";
                }
                else if (RBMale.Checked == false && RBFeMale.Checked == true)
                {
                    FGetMostafeedByOrphans(0, 0, "طالبة");
                    txtSearchOrphans.Text = "قائمة بيانات جميع الطالبات";
                }
            }
            else if (DLAlQriahByData.Text != string.Empty)
            {
                if (RBMale.Checked == true && RBFeMale.Checked == false)
                {
                    FGetMostafeedByOrphans(1, Convert.ToInt32(DLAlQriahByData.SelectedValue), "طالب");
                    txtSearchOrphans.Text = "قائمة بيانات جميع الطلاب في قرية " + DLAlQriahByData.SelectedItem.ToString();
                }
                else if (RBMale.Checked == false && RBFeMale.Checked == true)
                {
                    FGetMostafeedByOrphans(1, Convert.ToInt32(DLAlQriahByData.SelectedValue), "طالبة");
                    txtSearchOrphans.Text = "قائمة بيانات جميع الطالبات في قرية " + DLAlQriahByData.SelectedItem.ToString();
                }
            }

            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetMostafeedByOrphans(int XIDCheck, int XIDQariah, string XIDGender)
    {
        GVOrphansAll.UseAccessibleHeader = false;
        ClassTarafMostafeed._IDCheck = XIDCheck;
        ClassTarafMostafeed._TypeMostafeed = "دائم";
        ClassTarafMostafeed._AlQaryah = XIDQariah;
        ClassTarafMostafeed.AlMehnahAlHaliah = XIDGender;
        ClassTarafMostafeed._IsDelete2 = false;
        DataTable dt = new DataTable();
        dt = ClassTarafMostafeed.BArnTarafEstemarahGetBoysAllByQariahAndGender();
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

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        GVOrphansAll.UseAccessibleHeader = true;
        GVOrphansAll.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlPrintAllData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryEducationalSituations.aspx");
    }

}