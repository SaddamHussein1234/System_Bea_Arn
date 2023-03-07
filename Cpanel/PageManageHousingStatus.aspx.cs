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

public partial class Cpanel_PageManageHousingStatus : System.Web.UI.Page
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
            bool A35, A81;
            A35 = Convert.ToBoolean(dtViewProfil.Rows[0]["A35"]);
            A81 = Convert.ToBoolean(dtViewProfil.Rows[0]["A81"]);
            if (A35 == false)
            {
                view.Visible = false;
            }
            if (A81 == false)
            {
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVHousingStatusAll.Columns[0].Visible = false;
                GVHousingStatusAll.Columns[5].Visible = false;
            }
            if (A35 == false && A81 == false)
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
                txtName.Text = dt.Rows[0]["HalatAlMaskan"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل حالة المسكن";
                FGetHalatAlMaskanAll();
            }
            else
            {
                FGetHalatAlMaskanAll();
            }
        }
        else
        {
            FGetHalatAlMaskanAll();
        }

    }

    private void FGetHalatAlMaskanAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] Where HalatAlMaskan <> @0 And HalatAlMaskan Like '%' + @1 + '%' And IsDeleteHalatAlMaskan = @2 Order by IDItem Desc", string.Empty, txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVHousingStatusAll.DataSource = dt;
                GVHousingStatusAll.DataBind();
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
        Response.Redirect("PageManageHousingStatus.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVHousingStatusAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVHousingStatusAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[Quaem] SET [IsDeleteHalatAlMaskan] = @IsDeleteHalatAlMaskan WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDeleteHalatAlMaskan", true);
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
        dt = ClassDataAccess.GetData("Select * from Quaem Where HalatAlMaskan = @0 And IsDeleteHalatAlMaskan = @1", txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة حالة المسكن مسبقاً";
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
            _TypeAlMaskan = string.Empty,
            _AlDakhlAlShahryWaMasdarah = string.Empty,
            _Gender = string.Empty,
            _HalatAlMaskan = txtName.Text.Trim(),
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
        lbmsg.Text = "تم إضافة حالة المسكن بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetHalatAlMaskanAll();
        System.Threading.Thread.Sleep(500);
    }

    private void FQariahEdit()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[Quaem] SET [HalatAlMaskan] = @HalatAlMaskan WHERE IDUniq = @IDUniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(Request.QueryString["ID"]));
        cmd.Parameters.AddWithValue("@HalatAlMaskan", txtName.Text.Trim());
        cmd.ExecuteScalar();
        conn.Close();
        lbmsg.Text = "تم تعديل حالة المسكن بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetHalatAlMaskanAll();
        System.Threading.Thread.Sleep(500);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetHalatAlMaskanAll();
    }

}