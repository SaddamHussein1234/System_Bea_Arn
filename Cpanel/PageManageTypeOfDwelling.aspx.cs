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

public partial class Cpanel_PageManageTypeOfDwelling : System.Web.UI.Page
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
            Response.Redirect("LogOut.aspx");
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
            bool A33, A79;
            A33 = Convert.ToBoolean(dtViewProfil.Rows[0]["A33"]);
            A79 = Convert.ToBoolean(dtViewProfil.Rows[0]["A79"]);
            if (A33 == false)
            {
                view.Visible = false;
            }
            if (A79 == false)
            {
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVTypeOfDwellingAll.Columns[0].Visible = false;
                GVTypeOfDwellingAll.Columns[5].Visible = false;
            }
            if (A33 == false && A79 == false)
            {
                Response.Redirect("LogOut.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetData();
            txtName.Focus();
        }
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select * from Quaem Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["TypeAlMaskan"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل نوع مسكن";
                FGetTypeOfDwellinAll();
            }
            else
            {
                FGetTypeOfDwellinAll();
            }
        }
        else
        {
            FGetTypeOfDwellinAll();
        }

    }

    private void FGetTypeOfDwellinAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] Where TypeAlMaskan <> @0 And TypeAlMaskan Like '%' + @1 + '%' And IsDeleteTypeAlMaskan = @2 Order by IDItem Desc", string.Empty, txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVTypeOfDwellingAll.DataSource = dt;
                GVTypeOfDwellingAll.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageTypeOfDwelling.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVTypeOfDwellingAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVTypeOfDwellingAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[Quaem] SET [IsDeleteTypeAlMaskan] = @IsDeleteTypeAlMaskan WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDeleteTypeAlMaskan", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text != string.Empty)
            {
                lblName.Visible = false;
                if (btnAdd.Text == "حفظ البيانات")
                {
                    FCheckName();
                }
                else if (btnAdd.Text == "تعديل البيانات")
                {
                    FQariahEdit();
                }
            }
            else if (txtName.Text == string.Empty)
            {
                lblName.Visible = true;
            }
        }
        catch (Exception)
        {

        }
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from Quaem Where TypeAlMaskan = @0 And IsDeleteTypeAlMaskan = @1", txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة نوع المسكن مسبقاً";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FQariahAdd();
        }
    }

    private void FQariahAdd()
    {
        GetCookie();
        ClassQuaem CQ = new ClassQuaem()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _AlQriah = string.Empty,
            _HalatMostafeed = string.Empty,
            _TypeAlMaskan = txtName.Text.Trim(),
            _AlDakhlAlShahryWaMasdarah = string.Empty,
            _Gender = string.Empty,
            _HalatAlMaskan = string.Empty,
            _AlBaheth = string.Empty,
            _TypeAlDam = string.Empty,
            _AlQarabah = string.Empty,
            _AmeenAlSondoq = string.Empty,
            _RaeesMaglesAlEdarah = string.Empty,
            _AlModer = string.Empty,
            _AlAmeenAlAam = string.Empty,
            _AlMohaseb = string.Empty,
            _RaeesAlShoonAlMaliah = string.Empty,
            _SelatAlQarabah = string.Empty,
            _DateAdd = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IsDelete = false,
            _IDAdmin = Convert.ToInt32(IDUser),
            ID_Affiliation_Progect = 0,
            TypeCart = string.Empty,
            TypeCartPart = string.Empty
        };
        CQ.BArnQuaemAdd();
        lbmsg.Text = "تم إضافة نوع المسكن بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetTypeOfDwellinAll();
        System.Threading.Thread.Sleep(500);
    }

    private void FQariahEdit()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[Quaem] SET [TypeAlMaskan] = @TypeAlMaskan WHERE IDUniq = @IDUniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(Request.QueryString["ID"]));
        cmd.Parameters.AddWithValue("@TypeAlMaskan", txtName.Text.Trim());
        cmd.ExecuteScalar();
        conn.Close();
        lbmsg.Text = "تم تعديل نوع المسكن بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetTypeOfDwellinAll();
        System.Threading.Thread.Sleep(500);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetTypeOfDwellinAll();
    }

}