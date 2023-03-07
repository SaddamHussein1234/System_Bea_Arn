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

public partial class Shaerd_CPanelManageWarehouse_PageManageAffiliation : System.Web.UI.UserControl
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
            bool A62, A112;
            A62 = Convert.ToBoolean(dtViewProfil.Rows[0]["A62"]);
            A112 = Convert.ToBoolean(dtViewProfil.Rows[0]["A112"]);
            if (A62 == false)
                View.Visible = false;
            if (A112 == false)
            {
                IDAdd.Visible = false;
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVAffiliationAll.Columns[0].Visible = false;
                GVAffiliationAll.Columns[10].Visible = false;
            }
            if (A62 == false && A112 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetLastRecord();
            txtName.Focus();
            lblDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd hh:mm");
            FGetData();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[AffiliationShop] With(NoLock) Where IsDelete = @0 Order by IDNumberAffiliation Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumber.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDNumberAffiliation"]) + 1);
        }
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select * from AffiliationShop Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                DLStore.SelectedValue = dt.Rows[0]["IDStoreffiliation"].ToString();
                txtName.Text = dt.Rows[0]["AffiliationName"].ToString();
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["DateAddAffiliation"]).ToString("yyyy/MM/dd");
                txtNote.Text = dt.Rows[0]["A1"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل إنتماء للمستودع";
                FGetAffiliationAll();
            }
            else
            {
                FGetAffiliationAll();
            }
        }
        else
        {
            FGetAffiliationAll();
        }
    }

    private void FGetAffiliationAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[AffiliationShop] Where AffiliationName Like '%' + @0 + '%' And IsDelete = @1 Order by IDNumberAffiliation", txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVAffiliationAll.DataSource = dt;
                GVAffiliationAll.DataBind();
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
        Response.Redirect("PageManageAffiliation.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAffiliationAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAffiliationAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[AffiliationShop] SET [IsDelete] = @IsDelete WHERE AffiliationID = @AffiliationID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@AffiliationID", Comp_ID);
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
                if (txtNumber.Text.Trim() == string.Empty)
                {
                    System.Threading.Thread.Sleep(100);
                    lblNumber.Text = "* يرجى إضافة رقم الإنتماء";
                }
                else
                {
                    if (txtName.Text.Trim() == string.Empty)
                    {
                        System.Threading.Thread.Sleep(100);
                        lblCheckName.Text = "* يرجى إضافة إسم الإنتماء";
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        lblCheckName.Visible = false;
                        FCheckNumber();
                    }
                }
            }
            else if (btnAdd.Text == "تعديل البيانات")
            {
                System.Threading.Thread.Sleep(500);
                FAffiliationEdit();
            }

        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckNumber()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from AffiliationShop Where IDNumberAffiliation = @0 And IsDelete = @1", txtNumber.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "لا يمكن تكرار رقم الإنتماء قم بتغييره";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FCheckName();
        }
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from AffiliationShop Where IDStoreffiliation = @0 And AffiliationName = @1 And IsDelete = @2", DLStore.SelectedValue, txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة هذا الإنتماء مسبقاً";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            FAffiliationAdd();
        }
    }

    private void FAffiliationAdd()
    {
        GetCookie();
        ClassAffiliation CA = new ClassAffiliation()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _IDStoreffiliation = Convert.ToInt32(DLStore.SelectedValue),
            _IDNumberAffiliation = Convert.ToInt64(txtNumber.Text.Trim()),
            _AffiliationName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _DateAddAffiliation = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = txtNote.Text.Trim(),
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CA.BArnAffiliationShopAdd();
        lbmsg.Text = "تم إضافة الإنتماء بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetAffiliationAll();
    }

    private void FAffiliationEdit()
    {
        GetCookie();
        ClassAffiliation CA = new ClassAffiliation()
        {
            _IDUniq = Convert.ToString(Request.QueryString["ID"]),
            _IDStoreffiliation = Convert.ToInt32(DLStore.SelectedValue),
            _AffiliationName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = txtNote.Text.Trim(),
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0"
        };
        CA.BArnAffiliationShopEdit();
        lbmsg.Text = "تم تعديل الإنتماء بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetAffiliationAll();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetAffiliationAll();
        System.Threading.Thread.Sleep(500);
    }

    public string FGetCount(float ID)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(*) As 'CountProductByAffiliation' FROM [dbo].[ProductShop] Where IDAffiliationShop = @0 And IsDelete = @1", Convert.ToString(ID), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows[0]["CountProductByAffiliation"].ToString();
        }
        return XResult;
    }

}