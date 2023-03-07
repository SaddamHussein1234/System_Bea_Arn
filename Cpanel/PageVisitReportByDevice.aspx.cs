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

public partial class Cpanel_PageVisitReportByDevice : System.Web.UI.Page
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
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            FGetAlQariah();
            txtDateFrom.Text = "2019-01-01";
            txtDateTo.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            FGetDevice();
            pnlSelectByCheck.Visible = true;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVVisitReport.Columns[0].Visible = true;
        GVVisitReport.Columns[11].Visible = true;
        GVVisitReport.UseAccessibleHeader = false;
        PnlChart.Visible = false;
        FCheckSelect();
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
            txtTitle.Text = "قائمة إحتياجات المستفيدين حسب الأجهزة الكهربائية حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
                XResult = dt.Rows[0]["CountItem"].ToString();
            else
                XResult = "0";
        }
        else
            XResult = "0";
        return XResult;
    }

    public string FGetCountAosrahAll(Int64 XID)
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("select DISTINCT ReportAlZyarat.NumberMostafeed, IDNumberCount As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join ReportAlZyarat on ReportAlZyarat.NumberReport = RZEA.IDReport Where ReportAlZyarat.IsDelete = @0 And ReportAlZyarat.A1 = @1 And (convert(date, DateReport) Between @2 And @3) And (IDDevice = @4) And (IDNumberCount <> @5) And RZEA.IsDelete = @5", Convert.ToString(false), DLPercint.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0");
        if (dt.Rows.Count > 0)
            XResult = dt.Rows.Count.ToString();
        else
            XResult = "0";
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
            txtTitle.Text = " قائمة إحتياجات المستفيدين حسب الأجهزة الكهربائية لقرية " + DLAlQriah.SelectedItem.ToString() +  " حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
            txtTitle.Text = "إحتياجات المستفيدين حسب الأجهزة الكهربائية لحالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
                XResult = dt.Rows[0]["CountItem"].ToString();
            else
                XResult = "0";
        }
        else
            XResult = "0";
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
            txtTitle.Text = "إحتياجات المستفيدين حسب الأجهزة الكهربائية لقرية " + DLAlQriah.SelectedItem.ToString() + " - حالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
                XResult = dt.Rows[0]["CountItem"].ToString();
            else
                XResult = "0";
        }
        else
            XResult = "0";
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
            txtTitle.Text = "إحتياجات المستفيدين حسب الأجهزة الكهربائية لـ " + DlVevice.SelectedItem.ToString() + " حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
                XResult = dt.Rows[0]["CountItem"].ToString();
            else
                XResult = "0";
        }
        else
            XResult = "0";
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
            txtTitle.Text = "إحتياجات المستفيدين حسب الأجهزة الكهربائية لقرية " + DLAlQriah.SelectedItem.ToString() + " - الإحتياج ( " + DlVevice.SelectedItem.ToString() + " ) حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("select sum(IDNumberCount) As 'CountItem' from [ReportAlZyaratElectricalAppliances] RZEA with(noLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = RZEA.IDMustafeed Where RasAlEstemarah.AlQaryah = @0 And IDDevice = @1 And (convert(date, DateAddDevice) Between @2 And @3) And (IDDevice = @4) And (IDNumberCount <> @5) And RZEA.IsDelete = @6", DLAlQriah.SelectedValue,DlVevice.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XID.ToString(), "0", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CountItem"].ToString() != string.Empty)
                XResult = dt.Rows[0]["CountItem"].ToString();
            else
                XResult = "0";
        }
        else
            XResult = "0";
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
            txtTitle.Text = "إحتياجات المستفيدين حسب الأجهزة الكهربائية لحالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " - الإحتياج ( " + DlVevice.SelectedItem.ToString() + " ) حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
                XResult = dt.Rows[0]["CountItem"].ToString();
            else
                XResult = "0";
        }
        else
            XResult = "0";
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
            txtTitle.Text = "إحتياجات المستفيدين حسب الأجهزة الكهربائية لقرية " + DLAlQriah.SelectedItem.ToString() + " - حالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " - الإحتياج ( " + DlVevice.SelectedItem.ToString() + " ) حتى تاريخ " + txtDateTo.Text.Trim();
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
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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
                XResult = dt.Rows[0]["CountItem"].ToString();
            else
                XResult = "0";
        }
        else
            XResult = "0";
        return XResult;
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageVisitReportByDevice.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVVisitReport.Columns[0].Visible = false;
            GVVisitReport.Columns[11].Visible = false;
            GVVisitReport.UseAccessibleHeader = true;
            GVVisitReport.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GVVisitReport.Rows)
        {
            if ((row.FindControl("chkSelect") as CheckBox).Checked)
            {
                string firstColumnValue = row.RowIndex.ToString();
                GVVisitReport.Rows[int.Parse(firstColumnValue)].Visible = false;
            }
            lblCount.Text = GVVisitReport.Rows.Count.ToString();
        }
    }

    public string FGetDevice(int IDMostafeed, int IDReport , string XCheck)
    {
        string XResult = "";
        DataTable dt = new DataTable();

        if (XCheck != string.Empty)
        {
            dt = ClassDataAccess.GetData("SELECT TOP 1000 [IDItam],[IDDevice],[IDNumberCount],[IDMustafeed],[IDReport],[DateAddDevice],TblRZEA.A1,TblRZEA.A2,TblRZEA.A3,TblRZEA.A4,TblRZEA.A5,TblRZEA.IsDelete FROM [dbo].[ReportAlZyaratElectricalAppliances] TblRZEA With(noLock) Where IDDevice = @0 And IDMustafeed = @1 And IDReport = @2 And IDNumberCount <> @3 And TblRZEA.IsDelete = @4", 
                XCheck, Convert.ToString(IDMostafeed), Convert.ToString(IDReport), Convert.ToString(0), Convert.ToString(false));
        }
        else if (XCheck == string.Empty)
        {
            dt = ClassDataAccess.GetData("SELECT TOP 1000 [IDItam],[IDDevice],[IDNumberCount],[IDMustafeed],[IDReport],[DateAddDevice],TblRZEA.A1,TblRZEA.A2,TblRZEA.A3,TblRZEA.A4,TblRZEA.A5,TblRZEA.IsDelete FROM [dbo].[ReportAlZyaratElectricalAppliances] TblRZEA With(noLock) Where IDMustafeed = @0 And IDReport = @1 And IDNumberCount <> @2 And TblRZEA.IsDelete = @3", 
                Convert.ToString(IDMostafeed), Convert.ToString(IDReport), Convert.ToString(0), Convert.ToString(false));
        }
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                XResult += "<span style='font-size:11px'>" + Library_CLS_Arn.WSM.WSM_ClassProduct.FProductName(Convert.ToInt32(dt.Rows[i]["IDDevice"])) + ":" + dt.Rows[i]["IDNumberCount"].ToString() + "</span>,";
            }
        }
        return XResult;
    }

    private void FGetDevice()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "18", Convert.ToString(true), Convert.ToString(false));
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

    protected void btnGetByType_Click(object sender, EventArgs e)
    {
        txtSearchStatistic.Text = "قائمة إحتياجات المستفيدين حسب الأجهزة الكهربائية";
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
        lblvaluesType.Text += "<tr><td class='StyleTD' style='font-size: 15px; background-color: #4b4b4b; color: #bababa'> إجمالي عدد الأجهزة </td>";
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

    //public string FGetCountDeviceAllByAosrah(int XIDDevice)
    //{
    //    string XResult = "0";
    //    DataTable dt = new DataTable();
    //    dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [IDMustafeed],[IDReport],DateReport,ReportAlZyarat.IDAdmin,RZEA.A1,RZEA.A2,RZEA.A3,RZEA.A4,RZEA.A5,RZEA.IsDelete FROM [dbo].[ReportAlZyaratElectricalAppliances] RZEA With(noLock) Inner join ReportAlZyarat on ReportAlZyarat.NumberReport = RZEA.IDReport Where ReportAlZyarat.IsDelete = @0 And ReportAlZyarat.A1 = @1 And (convert(date, DateReport) Between @2 And @3) And (IDDevice = @4) And ((IDNumberCount <> @5))", Convert.ToString(false), DLPercint.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), XIDDevice.ToString(), "0");
    //    if (dt.Rows.Count > 0)
    //    {
    //        XResult = dt.Rows.Count.ToString();
    //    }
    //    else
    //    {
    //        XResult = "0";
    //    }
    //    return XResult;
    //}

    public string FGetCountDeviceAllByAosrah()
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [IDMustafeed],[IDReport],DateReport,ReportAlZyarat.IDAdmin,RZEA.A1,RZEA.A2,RZEA.A3,RZEA.A4,RZEA.A5,RZEA.IsDelete FROM [dbo].[ReportAlZyaratElectricalAppliances] RZEA With(noLock) Inner join ReportAlZyarat on ReportAlZyarat.NumberReport = RZEA.IDReport Where ReportAlZyarat.IsDelete = @0 And ReportAlZyarat.A1 = @1 And (convert(date, DateReport) Between @2 And @3) And ((IDNumberCount <> @4)) And RZEA.IsDelete = @0", Convert.ToString(false), DLPercint.SelectedValue, Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd"), "0");
        if (dt.Rows.Count > 0)
            XResult = dt.Rows.Count.ToString();
        else
            XResult = "0";
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
        chartConfigAll.Add("caption", "إحصائية إحتياجات المستفيدين حسب الأجهزة الكهربائية ");
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
        try
        {
            Session["footable1"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable10.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }

    }
    
}