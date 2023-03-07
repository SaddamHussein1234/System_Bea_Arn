using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageBeneficiaryRegistrationAddBuy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetAlQarabah();
            FGetDataMostafed();

            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")); i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 100; i--)
            {
                dtYear.Rows.Add(i);
            }

            ddlYears.Items.Clear();
            ddlYears.Items.Add("");
            ddlYears.AppendDataBoundItems = true;
            ddlYears.DataTextField = "Year";
            ddlYears.DataValueField = "Year";
            ddlYears.DataSource = dtYear;
            ddlYears.DataBind();

            DataTable dtYearH = new DataTable();
            dtYearH.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 578; i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 700; i--)
            {
                dtYearH.Rows.Add(i);
            }

            ddlYearsH.Items.Clear();
            ddlYearsH.Items.Add("");
            ddlYearsH.AppendDataBoundItems = true;
            ddlYearsH.DataTextField = "Year";
            ddlYearsH.DataValueField = "Year";
            ddlYearsH.DataSource = dtYearH;
            ddlYearsH.DataBind();

            DataTable dtYearStady = new DataTable();
            dtYearStady.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 578; i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 619; i--)
            {
                dtYearStady.Rows.Add(i);
            }
            //DLYearStudy.Items.Clear();
            //DLYearStudy.Items.Add("");
            //DLYearStudy.AppendDataBoundItems = true;
            DLYearStudy.DataTextField = "Year";
            DLYearStudy.DataValueField = "Year";
            DLYearStudy.DataSource = dtYearStady;
            DLYearStudy.DataBind();

            FGetDataboy();
        }
    }

    private void FGetData()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[TarafEstemarah] with(NoLock) Where NumberMostafed = @0 And IsDelete = @1", lblFile.Text, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPBuys.DataSource = dt;
            RPBuys.DataBind();

            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            FUpdateAfradAlOsrah(dt.Rows.Count + 1);
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
    }

    private void FGetAlQarabah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlQarabah <> @0 And IsDeleteAlQarabah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQarabah.Items.Clear();
            DLAlQarabah.Items.Add("");
            DLAlQarabah.AppendDataBoundItems = true;
            DLAlQarabah.DataValueField = "IDItem";
            DLAlQarabah.DataTextField = "AlQarabah";
            DLAlQarabah.DataSource = dt;
            DLAlQarabah.DataBind();
        }
    }

    private void FGetDataMostafed()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where IDUniq = @0 And IsDelete = @1", Convert.ToString(Request.QueryString["ID"]), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblFile.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblTypeMostafeed.Text = "سيتم الإعتماد من قبل الإدارة";// dt.Rows[0]["TypeMostafeed"].ToString();
                lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            }
            FGetData();
        }
        catch (Exception)
        {
            return;
        }
    }


    protected void LBUpdate_Click(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "حفظ البيانات")
            {
                FCheckNameBoyMosTafeed();
            }
            else if (btnAdd.Text == "تعديل البيانات")
            {
                if (DLYearStudy.SelectedValue != string.Empty)
                {
                    FBoyEdit(Convert.ToInt32(DLYearStudy.SelectedItem.ToString()));
                }
                else
                {
                    FBoyEdit(0);
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FBoyEdit(int yearsStudy)
    {
        DateTime today = ClassDataAccess.GetCurrentTime();
        Dates dateConvert = new Dates();

        string DateGen = dateConvert.HijriToGreg(Convert.ToDateTime(ddlYearsH.SelectedValue + "/" + ddlMonthsH.SelectedValue + "/" + ddlDatesH.SelectedValue).ToString("yyyy/MM/dd"));

        string yearH = ddlYearsH.SelectedValue;
        string monthH = ddlMonthsH.SelectedValue;
        string dateH = ddlDatesH.SelectedValue;

        ddlDates.SelectedValue = Convert.ToDateTime(DateGen).ToString("dd");
        ddlMonths.SelectedValue = Convert.ToDateTime(DateGen).ToString("MM");
        ddlYears.SelectedValue = Convert.ToDateTime(DateGen).ToString("yyyy");

        string year = ddlYears.SelectedValue;
        string month = ddlMonths.SelectedValue;
        string date = ddlDates.SelectedValue;
        DateTime dob = Convert.ToDateTime(date + "/" + month + "/" + year);

        TimeSpan ts = today - dob;
        DateTime age = DateTime.MinValue + ts;
        int years = age.Year - 1;
        int months = age.Month - 1;
        int days = age.Day - 1;
        txtAge.Text = years.ToString() + " سنة " + " و " + months.ToString() + " شهر ";


        string XDate;
        XDate = ddlYears.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlDates.SelectedValue;
        ClassTarafMostafeed CTM = new ClassTarafMostafeed()
        {
            _IDUniq = Convert.ToString(Request.QueryString["XID"]),
            _Name = txtName.Text.Trim(),
            _AlQarabah = Convert.ToInt32(DLAlQarabah.SelectedValue),
            _DateBrith = XDate,
            _Age = txtAge.Text.Trim(),
            _AlMehnahAlHaliah = DLAlMehnah.SelectedValue,
            _AlmostawaAlDerasy = txtAlmostawaAlDerasy.Text.Trim(),
            _AlHalahAlseHeyah = txtAlHalahAlSehe.Text.Trim(),
            _NumberMostafed = Convert.ToInt32(lblFile.Text),
            _A1 = yearsStudy,
            _A2 = Convert.ToInt32(txtNumberSigal.Text.Trim()),
            _A3 = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _A4 = "0",
            _A5 = "0",
            _IDAdmin = 2122
        };
        CTM.BArnTarafAlEstemarahEdit();
        lbmsg.Text = "تم تعديل البيانات بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetData();
        FGetDataboy();
    }

    private void FGetDataboy()
    {
        try
        {
            if (Request.QueryString["XID"] != null)
            {
                DataTable dt = new DataTable();
                dt = ClassDataAccess.GetData("Select Top(1) * from TarafEstemarah With(NoLock) Where IDUniq = @0", Convert.ToString(Request.QueryString["XID"]));
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    DLAlQarabah.SelectedValue = dt.Rows[0]["AlQarabah"].ToString();
                    ddlYears.SelectedValue = Convert.ToDateTime(dt.Rows[0]["DateBrith"]).ToString("yyyy");
                    ddlMonths.SelectedValue = Convert.ToDateTime(dt.Rows[0]["DateBrith"]).ToString("MM");
                    ddlDates.SelectedValue = Convert.ToDateTime(dt.Rows[0]["DateBrith"]).ToString("dd");

                    FGetDate();

                    string DateHijri;
                    DateTime today = ClassDataAccess.GetCurrentTime();
                    string year = ddlYears.SelectedValue;
                    string month = ddlMonths.SelectedValue;
                    string date = ddlDates.SelectedValue;
                    DateTime dob = Convert.ToDateTime(date + "/" + month + "/" + year);
                    DateHijri = Convert.ToDateTime(ClassSaddam.ConvertDateCalendar(Convert.ToDateTime(date + "/" + month + "/" + year), "Hijri", "en-US")).ToString("dd/MM/yyyy");
                    ddlDatesH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("dd");
                    ddlMonthsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("MM");
                    ddlYearsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("yyyy");

                    DLAlMehnah.SelectedValue = dt.Rows[0]["AlMehnahAlHaliah"].ToString();
                    txtAlmostawaAlDerasy.Text = dt.Rows[0]["AlmostawaAlDerasy"].ToString();
                    txtAlHalahAlSehe.Text = dt.Rows[0]["AlHalahAlseHeyah"].ToString();
                    DLYearStudy.Text = dt.Rows[0]["A1"].ToString();
                    txtNumberSigal.Text = dt.Rows[0]["A2"].ToString();
                    btnAdd.Text = "تعديل البيانات";
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetDate()
    {
        string DateHijri;
        DateTime today = ClassDataAccess.GetCurrentTime();
        string year = ddlYears.SelectedValue;
        string month = ddlMonths.SelectedValue;
        string date = ddlDates.SelectedValue;
        DateTime dob = Convert.ToDateTime(date + "/" + month + "/" + year);

        DateHijri = Convert.ToDateTime(ClassSaddam.ConvertDateCalendar(Convert.ToDateTime(date + "/" + month + "/" + year), "Hijri", "en-US")).ToString("dd/MM/yyyy");
        ddlDatesH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("dd");
        ddlMonthsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("MM");
        ddlYearsH.SelectedValue = Convert.ToDateTime(DateHijri).ToString("yyyy");

        TimeSpan ts = today - dob;
        DateTime age = DateTime.MinValue + ts;
        int years = age.Year - 1;
        int months = age.Month - 1;
        int days = age.Day - 1;
        txtAge.Text = years.ToString() + " سنة " + " و " + months.ToString() + " شهر ";
        DLAlMehnah.Focus();
    }

    private void FCheckNameBoyMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[TarafEstemarah] With(NoLock) Where Name = @0 And NumberMostafed = @1", txtName.Text.Trim(), lblFile.Text);
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "لقد قمت بإضافة الإسم مسبقاً";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
            FAdd(Convert.ToInt32(DLYearStudy.SelectedItem.ToString()));
    }

    private void FAdd(int yearsStudy)
    {

        DateTime today = ClassDataAccess.GetCurrentTime();
        Dates dateConvert = new Dates();

        string DateGen = dateConvert.HijriToGreg(Convert.ToDateTime(ddlYearsH.SelectedValue + "/" + ddlMonthsH.SelectedValue + "/" + ddlDatesH.SelectedValue).ToString("yyyy/MM/dd"));

        string yearH = ddlYearsH.SelectedValue;
        string monthH = ddlMonthsH.SelectedValue;
        string dateH = ddlDatesH.SelectedValue;

        ddlDates.SelectedValue = Convert.ToDateTime(DateGen).ToString("dd");
        ddlMonths.SelectedValue = Convert.ToDateTime(DateGen).ToString("MM");
        ddlYears.SelectedValue = Convert.ToDateTime(DateGen).ToString("yyyy");

        string year = ddlYears.SelectedValue;
        string month = ddlMonths.SelectedValue;
        string date = ddlDates.SelectedValue;
        DateTime dob = Convert.ToDateTime(date + "/" + month + "/" + year);

        TimeSpan ts = today - dob;
        DateTime age = DateTime.MinValue + ts;
        int years = age.Year - 1;
        int months = age.Month - 1;
        int days = age.Day - 1;
        txtAge.Text = years.ToString() + " سنة " + " و " + months.ToString() + " شهر ";

        string XDate;
        XDate = ddlYears.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlDates.SelectedValue;
        ClassTarafMostafeed CTM = new ClassTarafMostafeed()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _Name = txtName.Text.Trim(),
            _AlQarabah = Convert.ToInt32(DLAlQarabah.SelectedValue),
            _DateBrith = XDate,
            _Age = txtAge.Text.Trim(),
            _AlMehnahAlHaliah = DLAlMehnah.SelectedValue,
            _AlmostawaAlDerasy = txtAlmostawaAlDerasy.Text.Trim(),
            _AlHalahAlseHeyah = txtAlHalahAlSehe.Text.Trim(),
            _NumberMostafed = Convert.ToInt32(lblFile.Text),
            _IsDelete = false,
            _A1 = yearsStudy,
            _A2 = Convert.ToInt32(txtNumberSigal.Text.Trim()),
            _A3 = "1990-01-01 00:00:00.000",
            _A4 = "0",
            _A5 = "0",
            _IDAdmin = 2122
        };
        CTM.BArnTarafAlEstemarahAdd();
        lbmsg.Text = "تم إضافة البيانات بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;

        txtName.Text = string.Empty;
        DLAlQarabah.SelectedValue = null;
        ddlYearsH.SelectedValue = null;
        ddlMonthsH.SelectedValue = null;
        ddlDatesH.SelectedValue = null;
        ddlYears.SelectedValue = null;
        ddlMonths.SelectedValue = null;
        ddlDates.SelectedValue = null;
        txtAge.Text = string.Empty;
        DLAlMehnah.SelectedValue = null;
        txtAlmostawaAlDerasy.Text = string.Empty;
        txtAlHalahAlSehe.Text = string.Empty;
        txtNumberSigal.Text = string.Empty;
        txtName.Focus();
        FGetData();
    }

    private void FUpdateAfradAlOsrah(int XID)
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[RasAlEstemarah] SET [AfradAlOsrah] = @AfradAlOsrah WHERE NumberMostafeed = @NumberMostafeed";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@AfradAlOsrah", Convert.ToInt32(XID));
        cmd.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(lblFile.Text.Trim()));
        cmd.ExecuteScalar();
        conn.Close();
    }

    protected void ddlDatesH_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDates_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public string FGetLink(string XID)
    {
        string Xresult = "";
        if (XID != string.Empty)
        {
            Xresult = "<a href='PageBeneficiaryRegistrationAddBuy.aspx?ID=" + Convert.ToString(Request.QueryString["ID"]) +
                "&XID=" + XID + "' title='تعديل'><i class='fa fa-edit'></i></a>";
        }
        return Xresult;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("/ar/");
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        pnlMessage.Visible = false;
        pnlOK.Visible = true;
    }

}