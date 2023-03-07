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

public partial class Cpanel_PageBeneficiaryBySearchComprehensive : System.Web.UI.Page
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
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAlQariah();
            pnlWaiting.Visible = true;
            txtSearchByFilter.Focus();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBLMasderAlDakhl.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBFamliyCases.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAccommodationType.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBHousingStatus.Items) { lst.Selected = true; }
        foreach (ListItem lst in CMMostafeed.Items) { lst.Selected = true; }
    }

    private void FGetMostafeedByMasderAlDhal()
    {
        if (CB3.Checked) {GVMostafeedByDakhl.Columns[3].Visible = true; } else { GVMostafeedByDakhl.Columns[3].Visible = false; }
        if (CB4.Checked) { GVMostafeedByDakhl.Columns[4].Visible = true; } else { GVMostafeedByDakhl.Columns[4].Visible = false; }
        if (CB6.Checked) { GVMostafeedByDakhl.Columns[6].Visible = true; } else { GVMostafeedByDakhl.Columns[6].Visible = false; }
        if (CB7.Checked) { GVMostafeedByDakhl.Columns[7].Visible = true; } else { GVMostafeedByDakhl.Columns[7].Visible = false; }
        if (CB8.Checked) { GVMostafeedByDakhl.Columns[8].Visible = true; } else { GVMostafeedByDakhl.Columns[8].Visible = false; }
        if (CB9.Checked) { GVMostafeedByDakhl.Columns[9].Visible = true; } else { GVMostafeedByDakhl.Columns[9].Visible = false; }
        if (CB10.Checked) { GVMostafeedByDakhl.Columns[10].Visible = true; } else { GVMostafeedByDakhl.Columns[10].Visible = false; }
        if (CB11.Checked) { GVMostafeedByDakhl.Columns[11].Visible = true; } else { GVMostafeedByDakhl.Columns[11].Visible = false; }
        if (CB12.Checked) { GVMostafeedByDakhl.Columns[12].Visible = true; } else { GVMostafeedByDakhl.Columns[12].Visible = false; }
        if (CB13.Checked) { GVMostafeedByDakhl.Columns[13].Visible = true; } else { GVMostafeedByDakhl.Columns[13].Visible = false; }
        if (CB14.Checked) { GVMostafeedByDakhl.Columns[14].Visible = true; } else { GVMostafeedByDakhl.Columns[14].Visible = false; }
        if (CB15.Checked) { GVMostafeedByDakhl.Columns[15].Visible = true; } else { GVMostafeedByDakhl.Columns[15].Visible = false; }
        if (CB16.Checked) { GVMostafeedByDakhl.Columns[16].Visible = true; } else { GVMostafeedByDakhl.Columns[16].Visible = false; }
        if (CB17.Checked) { GVMostafeedByDakhl.Columns[17].Visible = true; } else { GVMostafeedByDakhl.Columns[17].Visible = false; }
        if (CB18.Checked) { GVMostafeedByDakhl.Columns[18].Visible = true; } else { GVMostafeedByDakhl.Columns[18].Visible = false; }
        GVMostafeedByDakhl.Columns[0].Visible = true;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData(Convert.ToString(txtSearchByFilter.Text.Trim()));
        if (dt.Rows.Count > 0)
        {
            //txtSearchMostafeed.Text = "قائمة بيانات المستفيدين حسب الدخل الشهري الذي يتراوح مابين " + txtMasderAlDkhalMinimum.Text.Trim() + "ريال إلى " + txtMasderAlDkhalMaxiMam.Text.Trim() + " ريال ";
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
    }

    protected void btnGetByAlMasder_Click(object sender, EventArgs e)
    {
        try
        {
            lbmsg.Text = "بحث شامل عن بيانات المستفيدين";
            lbmsg.ForeColor = System.Drawing.Color.Black;
            pnlHideFilter.Visible = false;
            FGetData(string.Empty);
        }
        catch (Exception)
        {
            lbmsg.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل";
            lbmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }

    private void FGetData(string XData)
    {
        GVMostafeedByDakhl.Columns[0].Visible = true;
        GVMostafeedByDakhl.Columns[19].Visible = true;
        txtSearchByFilter.Text = "SELECT TOP 1000 * FROM [dbo].[RasAlEstemarah] With(NoLock) ";
        txtSearchByFilter.Text += "Where TypeMostafeed = N'" + Convert.ToString(DLTypeMostafeed.SelectedValue) + "' And IsDelete = 0 And ";
        txtSearchByFilter.Text += "(AlDakhlAlShahryllMostafeed between '" + Convert.ToString(txtMasderAlDkhalMinimum.Text.Trim()) + "' And '" + Convert.ToString(txtMasderAlDkhalMaxiMam.Text.Trim()) + "') And ";
        SelectByQriah();
        txtSearchByFilter.Text += " " + XData + " Order By AlQaryah , NameMostafeed";
        GVMostafeedByDakhl.UseAccessibleHeader = false;
        FGetMostafeedByMasderAlDhal();
        System.Threading.Thread.Sleep(100);
    }

    private void SelectByQriah()
    {
        txtSearchByFilter.Text += "(";
        foreach (ListItem lst in CBAlQariah.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " AlQaryah = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByMostafeed();
    }

    private void SelectByMostafeed()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CMMostafeed.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " IDItem = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByMasderAlDakhl();
    }

    private void SelectByMasderAlDakhl()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBLMasderAlDakhl.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " MasderAlDakhl = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByFamliyCases();
    }

    private void SelectByFamliyCases()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBFamliyCases.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " HalafAlMosTafeed = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByAccommodationType();
    }

    private void SelectByAccommodationType()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBAccommodationType.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " TypeAlMasken = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByHousingStatus();
    }

    private void SelectByHousingStatus()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBHousingStatus.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " HaletAlMasken = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }
        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 AlQaryah , Quaem.AlQriah FROM [dbo].[RasAlEstemarah] With(NoLock)  Inner Join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah");
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "AlQaryah";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetAlDakhlAlShahryWaMasdarah();
    }

    private void FGetAlDakhlAlShahryWaMasdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlDakhlAlShahryWaMasdarah <> @0 And IsDeleteAlDakhlAlShahryWaMasdarah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBLMasderAlDakhl.DataValueField = "IDItem";
            CBLMasderAlDakhl.DataTextField = "AlDakhlAlShahryWaMasdarah";
            CBLMasderAlDakhl.DataSource = dt;
            CBLMasderAlDakhl.DataBind();
        }
        FGetHalafAlMosTafeed();
    }

    private void FGetHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDeleteHalatMostafeed = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBFamliyCases.DataValueField = "IDItem";
            CBFamliyCases.DataTextField = "HalatMostafeed";
            CBFamliyCases.DataSource = dt;
            CBFamliyCases.DataBind();
        }
        FGetAccommodationType();
    }

    private void FGetAccommodationType()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where TypeAlMaskan <> @0 And IsDeleteTypeAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAccommodationType.DataValueField = "IDItem";
            CBAccommodationType.DataTextField = "TypeAlMaskan";
            CBAccommodationType.DataSource = dt;
            CBAccommodationType.DataBind();
        }
        FGetHousingStatus();
    }

    private void FGetHousingStatus()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatAlMaskan <> @0 And IsDeleteHalatAlMaskan = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBHousingStatus.DataValueField = "IDItem";
            CBHousingStatus.DataTextField = "HalatAlMaskan";
            CBHousingStatus.DataSource = dt;
            CBHousingStatus.DataBind();
        }
        FGetMostafeed();
    }

    private void FGetMostafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 RasAlEstemarah.IDItem,(+'( '+CAST(NumberMostafeed as varchar(10)) + ' ) ' + NameMostafeed + ' ( ' + Quaem.AlQriah +' ) ') As 'Name' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner Join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where TypeMostafeed = @0 And RasAlEstemarah.IsDelete = @1 Order by AlQaryah , NameMostafeed", DLTypeMostafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CMMostafeed.DataValueField = "IDItem";
            CMMostafeed.DataTextField = "Name";
            CMMostafeed.DataSource = dt;
            CMMostafeed.DataBind();
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMostafeedByDakhl.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMostafeedByDakhl.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[RasAlEstemarah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetData(string.Empty);
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        try
        {
            FGetData(" And ([_Is_Print_Hide_] = 0) ");

            GVMostafeedByDakhl.Columns[0].Visible = false;
            GVMostafeedByDakhl.Columns[19].Visible = false;
            GVMostafeedByDakhl.UseAccessibleHeader = true;
            GVMostafeedByDakhl.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlPrintAllData;

            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryBySearchComprehensive.aspx");
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        pnlHideFilter.Visible = true;
    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        //lblCount.Text = "0";
        //foreach (GridViewRow row in GVMostafeedByDakhl.Rows)
        //{
        //    if ((row.FindControl("chkSelect") as CheckBox).Checked)
        //    {
        //        string firstColumnValue = row.RowIndex.ToString();
        //        GVMostafeedByDakhl.Rows[int.Parse(firstColumnValue)].Visible = false;
        //    }
        //}
        //lblCount.Text = GVMostafeedByDakhl.Rows.Count.ToString();
        try
        {
            foreach (GridViewRow row in GVMostafeedByDakhl.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMostafeedByDakhl.DataKeys[row.RowIndex].Value);
                    FHideOrView(Convert.ToInt64(Comp_ID), true);
                }
            }
            FGetData(string.Empty);
        }
        catch (Exception)
        {
            return;
        }
    }

    public string FCheckHide(bool XValue)
    {
        string XResult = string.Empty;
        if (XValue)
            XResult = "<br /> <br /> <span style='background:#ba0404; padding:3px; border-radius:3px; color:#F0F0F0;'><small><i class='fa fa-eye-slash'></i> مخفي أثناء الطباعة </small></span>";
        return XResult;
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMostafeedByDakhl.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMostafeedByDakhl.DataKeys[row.RowIndex].Value);
                    FHideOrView(Convert.ToInt64(Comp_ID), false);
                }
            }
            FGetData(string.Empty);
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FHideOrView(Int64 XID, bool XValue)
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[RasAlEstemarah] SET [_Is_Print_Hide_] = @Is_Print_Hide WHERE IDItem = @IDItem";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDItem", XID);
        cmd.Parameters.AddWithValue("@Is_Print_Hide", XValue);
        cmd.ExecuteScalar();
        conn.Close();
    }

}