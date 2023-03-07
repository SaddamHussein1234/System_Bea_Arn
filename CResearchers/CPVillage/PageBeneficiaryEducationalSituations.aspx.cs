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

public partial class CResearchers_CPVillage_PageBeneficiaryEducationalSituations : System.Web.UI.Page
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
            FGetAlBaheth();
            FGetAlQariah();

            HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
            string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";
            DLAlQriah.SelectedValue = IDVillage;
            DLAlQriahByData.SelectedValue = DLAlQriah.SelectedValue;

            //FGetMostafeedByOrphans();
        }
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
                txtTitleByQariah.Text = " قائمة إحصائية المستفيدين حسب الحالات التعليمية لقرية " + DLAlQriah.SelectedItem.ToString();
                FGetMostafeedByTypeStudentByQriah();

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
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
            pnlByQariah.Visible = false;
            pnlData.Visible = true;
            GetCookie();
            DataTable dtcheck = new DataTable();
            dtcheck = ClassDataAccess.GetData("select Top(1) 8 from tbl_MultiQariah With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And tbl_MultiQariah.IsDelete = @2", IDUser, DLAlQriahByData.SelectedValue, Convert.ToString(false));
            if (dtcheck.Rows.Count > 0)
            {
                if (DLAlQriahByData.Text != string.Empty)
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
                System.Threading.Thread.Sleep(100);
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
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryEducationalSituations.aspx");
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

}