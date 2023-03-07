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

public partial class Shaerd_CPanelManageWarehouse_PageManageStoragePlaces : System.Web.UI.UserControl
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
            bool A67, A115;
            A67 = Convert.ToBoolean(dtViewProfil.Rows[0]["A67"]);
            A115 = Convert.ToBoolean(dtViewProfil.Rows[0]["A115"]);
            if (A67 == false)
                View.Visible = false;
            if (A115 == false)
            {
                IDAdd.Visible = false;
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVStoragePlacesAll.Columns[0].Visible = false;
                GVStoragePlacesAll.Columns[8].Visible = false;
            }
            if (A67 == false && A115 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtName.Focus();
            lblDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd hh:mm");
            FGetData();
            CheckAccountAdmin();
        }
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select * from StoragePlaces Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["StorageName"].ToString();
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["DateAddStorage"]).ToString("yyyy/MM/dd");
                txtNote.Text = dt.Rows[0]["A1"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل مسمى للمستودع";
                FGetStoragePlacesAll();
            }
            else
                FGetStoragePlacesAll();
        }
        else
        {
            FGetStoragePlacesAll();
        }
    }

    private void FGetStoragePlacesAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[StoragePlaces] Where StorageName Like '%' + @0 + '%' And IsDelete = @1 Order by StorageName", txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVStoragePlacesAll.DataSource = dt;
                GVStoragePlacesAll.DataBind();
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
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageStoragePlaces.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVStoragePlacesAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVStoragePlacesAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[StoragePlaces] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "حفظ البيانات")
            {
                if (txtName.Text.Trim() == string.Empty)
                {
                    System.Threading.Thread.Sleep(100);
                    lblCheckName.Text = "* يرجى إضافة عنوان المسمى";
                }
                else
                {
                    lblCheckName.Text = string.Empty;
                    System.Threading.Thread.Sleep(500);
                    lblCheckName.Visible = false;
                    FCheckName();
                }
            }
            else if (btnAdd.Text == "تعديل البيانات")
            {
                System.Threading.Thread.Sleep(500);
                FArnStoragePlacesShopEdit();
            }

        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from StoragePlaces Where StorageName = @0 And IsDelete = @1", txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة هذا المسمى مسبقاً";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
            FArnStoragePlacesShopAdd();
    }

    private void FArnStoragePlacesShopAdd()
    {
        GetCookie();
        ClassStoragePlaces CSP = new ClassStoragePlaces()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _StorageName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IsCheck = false,
            _DateAddStorage = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = txtNote.Text.Trim(),
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CSP.BArnStoragePlacesShopAdd();
        txtName.Text = string.Empty;
        txtName.Focus();
        lbmsg.Text = "تم الإضافة بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetStoragePlacesAll();
    }

    private void FArnStoragePlacesShopEdit()
    {
        GetCookie();
        ClassStoragePlaces CSP = new ClassStoragePlaces()
        {
            _IDUniq = Convert.ToString(Request.QueryString["ID"]),
            _StorageName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = txtNote.Text.Trim(),
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0"
        };
        CSP.BArnStoragePlacesShopEdit();
        lbmsg.Text = "تم التعديل بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetStoragePlacesAll();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetStoragePlacesAll();
        CheckAccountAdmin();
        System.Threading.Thread.Sleep(200);
    }

}