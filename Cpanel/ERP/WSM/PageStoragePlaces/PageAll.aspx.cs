using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_WSM_PageStoragePlaces_PageAll : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A67", "A115", btnDelete1, GVStoragePlacesAll, 0, 8);
            txtName.Focus();
            lblDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd hh:mm");
            FGetData();
        }
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = WSM_Data_Access_Layer.GetData("Select * from StoragePlaces Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
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
            dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[StoragePlaces] Where StorageName Like '%' + @0 + '%' And IsDelete = @1 Order by StorageName", txtSearch.Text.Trim(), Convert.ToString(false));
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
        Response.Redirect("PageAll.aspx");
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
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_WSM_);
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
        dt = WSM_Data_Access_Layer.GetData("Select Top(1) * from StoragePlaces Where StorageName = @0 And IsDelete = @1", txtName.Text.Trim(), Convert.ToString(false));
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
        ClassStoragePlaces CSP = new ClassStoragePlaces()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _StorageName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IsCheck = false,
            _DateAddStorage = ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Test_Saddam.FGetIDUsiq(),
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
        ClassStoragePlaces CSP = new ClassStoragePlaces()
        {
            _IDUniq = Convert.ToString(Request.QueryString["ID"]),
            _StorageName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IDAdmin = Test_Saddam.FGetIDUsiq(),
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
        System.Threading.Thread.Sleep(200);
    }

}