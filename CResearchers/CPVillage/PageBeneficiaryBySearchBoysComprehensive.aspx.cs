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

public partial class CResearchers_CPVillage_PageBeneficiaryBySearchBoysComprehensive : System.Web.UI.Page
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
            FGetAlQariah();
            pnlWaiting.Visible = true;
            txtSearchByFilter.Focus();
            FGetModerAlGmeiah();
            DataTable dtYearStady = new DataTable();
            dtYearStady.Columns.Add("Year", typeof(int));
            for (int i = Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 579; i >= Convert.ToInt32(ClassDataAccess.GetCurrentTime().ToString("yyyy")) - 599; i--)
            {
                dtYearStady.Rows.Add(i);
            }
            CBYearStudy.DataTextField = "Year";
            CBYearStudy.DataValueField = "Year";
            CBYearStudy.DataSource = dtYearStady;
            CBYearStudy.DataBind();

            FSelectCheck();
        }
    }

    private void FGetAlQariah()
    {
        GetCookie();
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "IDQariah";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetAlQarabah();
    }

    private void FGetAlQarabah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlQarabah <> @0 And IsDeleteAlQarabah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBQarabah.DataValueField = "IDItem";
            CBQarabah.DataTextField = "AlQarabah";
            CBQarabah.DataSource = dt;
            CBQarabah.DataBind();
        }
        //FGetHalafAlMosTafeed();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBQarabah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAlmostawaAlDerasy.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBYearStudy.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAlMehnah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAlHalahAlSehe.Items) { lst.Selected = true; }
        foreach (ListItem lst in CMMostafeed.Items) { lst.Selected = true; }
    }

    protected void btnGetByAlMasder_Click(object sender, EventArgs e)
    {
        try
        {
            lbmsg.Text = "بحث شامل عن بيانات المستفيدين";
            lbmsg.ForeColor = System.Drawing.Color.Black;
            pnlHideFilter.Visible = false;
            FGetData();
        }
        catch (Exception)
        {
            lbmsg.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل";
            lbmsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }

    private void FGetData()
    {
        DLModerAlGmeiah.Visible = true;
        DLRaeesMaglesAlEdarah.Visible = true;
        DLRaeesLagnatAlBahath.Visible = true;
        lblModerAlGmeiah.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        lblRaeesLagnatAlBahath.Visible = false;
        GVMostafeedByDakhl.Columns[0].Visible = true;
        GVMostafeedByDakhl.Columns[16].Visible = true;
        txtSearchByFilter.Text = "SELECT TOP 1000 TarafEstemarah.IDItem,TarafEstemarah.IDUniq,[Name],[AlQarabah],TarafEstemarah.DateBrith,TarafEstemarah.Age,[AlMehnahAlHaliah],[AlmostawaAlDerasy],[AlHalahAlseHeyah],[NumberMostafed],NameMostafeed,RasAlEstemarah.AlQaryah,PhoneNumber,TarafEstemarah.IsDelete,TarafEstemarah.A1,TarafEstemarah.A2,TarafEstemarah.A3,TarafEstemarah.A4,TarafEstemarah.A5,[IDAdmin] FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed ";
        txtSearchByFilter.Text += "Where TypeMostafeed = N'دائم' And TarafEstemarah.IsDelete = 0 And RasAlEstemarah.IsDelete = 0 And ";
        SelectByQarabah();
        txtSearchByFilter.Text += " Order By AlQaryah , AlmostawaAlDerasy, Name";
        GVMostafeedByDakhl.UseAccessibleHeader = false;
        FGetMostafeedByMasderAlDhal();
        System.Threading.Thread.Sleep(500);
    }

    private void SelectByQarabah()
    {
        txtSearchByFilter.Text += "(";
        foreach (ListItem lst in CBQarabah.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " AlQarabah = " + lst.Value;
                txtSearchByFilter.Text += " Or ";
            }

        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByAlMehnah();
    }

    private void SelectByAlMehnah()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBAlMehnah.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " AlMehnahAlHaliah = N'" + lst.Value + "'";
                txtSearchByFilter.Text += " Or ";
            }

        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByAlmostawaAlDerasy();
    }

    private void SelectByAlmostawaAlDerasy()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBAlmostawaAlDerasy.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " AlmostawaAlDerasy = '" + lst.Value + "'";
                txtSearchByFilter.Text += " Or ";
            }

        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByAlHalahAlSehe();
    }

    private void SelectByAlHalahAlSehe()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBAlHalahAlSehe.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " AlHalahAlseHeyah = N'" + lst.Value + "'";
                txtSearchByFilter.Text += " Or ";
            }

        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        SelectByQriah();
    }

    private void SelectByQriah()
    {
        txtSearchByFilter.Text += " And (";
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
        SelectByYearStudy();
    }

    private void SelectByYearStudy()
    {
        txtSearchByFilter.Text += " And (";
        foreach (ListItem lst in CBYearStudy.Items)
        {
            if (lst.Selected == true)
            {

                txtSearchByFilter.Text += " TarafEstemarah.A1 = '" + lst.Value + "'";
                txtSearchByFilter.Text += " Or ";
            }

        }
        txtSearchByFilter.Text = txtSearchByFilter.Text.Remove(txtSearchByFilter.Text.Length - 3, 3);
        txtSearchByFilter.Text += ")";
        FGetMostafeedByMasderAlDhal();
    }

    private void FGetMostafeedByMasderAlDhal()
    {
        if (CB3.Checked) { GVMostafeedByDakhl.Columns[3].Visible = true; } else { GVMostafeedByDakhl.Columns[3].Visible = false; }
        if (CB4.Checked) { GVMostafeedByDakhl.Columns[4].Visible = true; GVMostafeedByDakhl.Columns[5].Visible = true; } else { GVMostafeedByDakhl.Columns[4].Visible = false; GVMostafeedByDakhl.Columns[5].Visible = false; }
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

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
        DLModerAlGmeiah.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        DLRaeesLagnatAlBahath.Visible = false;
        lblModerAlGmeiah.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;
        lblRaeesLagnatAlBahath.Visible = true;
        GVMostafeedByDakhl.Columns[0].Visible = false;
        GVMostafeedByDakhl.Columns[16].Visible = false;
        GVMostafeedByDakhl.UseAccessibleHeader = true;
        GVMostafeedByDakhl.HeaderRow.TableSection = TableRowSection.TableHeader;
        Session["footable1"] = pnlPrintAllData;

        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void LBReafrchAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryBySearchBoysComprehensive.aspx");
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
                    string sql = "UPDATE [dbo].[TarafEstemarah] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetData();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        pnlHideFilter.Visible = true;
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
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
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

}