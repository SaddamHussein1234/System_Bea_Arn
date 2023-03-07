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

public partial class CPBeneficiary_PageBeneficiaryFamily : System.Web.UI.Page
{
    string UserERasAlEstemarah;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_User"];
            UserERasAlEstemarah = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if (dt.Rows.Count > 0)
        {
            bool A26;
            A26 = Convert.ToBoolean(dt.Rows[0]["A26"]);
            if (A26)
            {
                txtSearch.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                FGetDataMostafed(txtSearch.Text.Trim());
            }
            else
                Response.Redirect("PageNotAccess.aspx");
        }
        else
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetAlQarabah();           
            CheckAccountAdmin();

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
                    Label4.Text = "تعديل بيانات أفراد الإسرة";
                }
            }
        }
        catch (Exception)
        {
            return;
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

    private void FGetData(string XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[TarafEstemarah] Where NumberMostafed = @0 And IsDelete = @1", XID, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVMenu.DataSource = dt;
            GVMenu.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
        txtSearch.Focus();
    }

    private void FGetDataMostafed(string XID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", XID, Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblTypeMostafeed.Text = dt.Rows[0]["TypeMostafeed"].ToString();
                lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                txtCountBoys.Text = dt.Rows[0]["AfradAlOsrah"].ToString();
                pnlPrint.Visible = true;
                pnlSelect.Visible = false;
            }
            else
            {
                pnlPrint.Visible = false;
                pnlSelect.Visible = true;
            }
            FGetData(XID);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            FUpdateAfradAlOsrah();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم تحديث عدد أفراد الاسرة بنجاح";
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void ddlDatesH_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void ddlDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FGetDate();
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

    private void FBoyEdit(int years)
    {
        string XDate;
        XDate = ddlYears.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlDates.SelectedValue;
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[TarafEstemarah] SET [Name] = @Name,[AlQarabah] = @AlQarabah,[DateBrith] = @DateBrith,[Age] = @Age,[AlMehnahAlHaliah] = @AlMehnahAlHaliah,[AlmostawaAlDerasy] = @AlmostawaAlDerasy,[AlHalahAlseHeyah] = @AlHalahAlseHeyah,[NumberMostafed] = @NumberMostafed,[A1] = @A1,[A2] = @A2,[A3] = @A3 WHERE [IDUniq] = @IDUniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(Request.QueryString["XID"]));
        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@AlQarabah", Convert.ToInt32(DLAlQarabah.SelectedValue));
        cmd.Parameters.AddWithValue("@DateBrith", XDate);
        cmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
        cmd.Parameters.AddWithValue("@AlMehnahAlHaliah", DLAlMehnah.SelectedValue);
        cmd.Parameters.AddWithValue("@AlmostawaAlDerasy", txtAlmostawaAlDerasy.Text.Trim());
        cmd.Parameters.AddWithValue("@AlHalahAlseHeyah", txtAlHalahAlSehe.Text.Trim());
        cmd.Parameters.AddWithValue("@NumberMostafed", Convert.ToInt32(txtSearch.Text.Trim()));
        cmd.Parameters.AddWithValue("@A1", years);
        cmd.Parameters.AddWithValue("@A2", Convert.ToInt32(txtNumberSigal.Text.Trim()));
        cmd.Parameters.AddWithValue("@A3", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
        cmd.ExecuteScalar();
        conn.Close();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم تعديل البيانات بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FUpdateAfradAlOsrah();
        CheckAccountAdmin();
        FGetDataboy();
    }

    private void FCheckNameBoyMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[TarafEstemarah] With(NoLock) Where [Name] = @0 And [NumberMostafed] = @1 And [IsDelete] = @2", txtName.Text.Trim(), txtSearch.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لقد قمت بإضافة الإسم مسبقاً";
        }
        else
        {
            FAdd(Convert.ToInt32(DLYearStudy.SelectedItem.ToString()));
        }
    }

    private void FAdd(int years)
    {
        string XDate;
        XDate = ddlYears.SelectedValue + "/" + ddlMonths.SelectedValue + "/" + ddlDates.SelectedValue;
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "INSERT INTO [dbo].[TarafEstemarah]([IDUniq],[Name],[AlQarabah],[DateBrith],[Age],[AlMehnahAlHaliah],[AlmostawaAlDerasy],[AlHalahAlseHeyah],[NumberMostafed],[IsDelete],[A1],[A2],[A3])VALUES(@IDUniq,@Name,@AlQarabah,@DateBrith,@Age,@AlMehnahAlHaliah,@AlmostawaAlDerasy,@AlHalahAlseHeyah,@NumberMostafed,@IsDelete,@A1,@A2,@A3)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(Guid.NewGuid()));
        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
        cmd.Parameters.AddWithValue("@AlQarabah", Convert.ToInt32(DLAlQarabah.SelectedValue));
        cmd.Parameters.AddWithValue("@DateBrith", XDate);
        cmd.Parameters.AddWithValue("@Age", txtAge.Text.Trim());
        cmd.Parameters.AddWithValue("@AlMehnahAlHaliah", DLAlMehnah.SelectedValue);
        cmd.Parameters.AddWithValue("@AlmostawaAlDerasy", txtAlmostawaAlDerasy.Text.Trim());
        cmd.Parameters.AddWithValue("@AlHalahAlseHeyah", txtAlHalahAlSehe.Text.Trim());
        cmd.Parameters.AddWithValue("@NumberMostafed", Convert.ToInt32(txtSearch.Text.Trim()));
        cmd.Parameters.AddWithValue("@IsDelete", false);
        cmd.Parameters.AddWithValue("@A1", years);
        cmd.Parameters.AddWithValue("@A2", Convert.ToInt32(txtNumberSigal.Text.Trim()));
        cmd.Parameters.AddWithValue("@A3", "1990-01-01 00:00:00.000");
        cmd.ExecuteScalar();
        conn.Close();

        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم إضافة البيانات بنجاح";
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
        txtName.Focus();
        FUpdateAfradAlOsrah();
        CheckAccountAdmin();
    }

    private void FUpdateAfradAlOsrah()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[RasAlEstemarah] SET [AfradAlOsrah] = @AfradAlOsrah WHERE NumberMostafeed = @NumberMostafeed";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@AfradAlOsrah", Convert.ToInt32(txtCountBoys.Text.Trim()));
        cmd.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(txtSearch.Text.Trim()));
        cmd.ExecuteScalar();
        conn.Close();
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMenu.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMenu.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[TarafEstemarah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            CheckAccountAdmin();
        }
        catch (Exception)
        {
            return;
        }
    }

}