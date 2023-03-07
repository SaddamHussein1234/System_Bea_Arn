using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_WSM_PageCategory_PageAll : System.Web.UI.Page
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
            Response.Redirect("~/Cpanel/LogOut.aspx");
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
            bool A63, A113;
            A63 = Convert.ToBoolean(dtViewProfil.Rows[0]["A63"]);
            A113 = Convert.ToBoolean(dtViewProfil.Rows[0]["A113"]);
            if (A63 == false)
                View.Visible = false;
            if (A113 == false)
            {
                IDAdd.Visible = false;
                Add.Visible = false;
                btnDelete1.Visible = false;
                GVCategoryAll.Columns[0].Visible = false;
                GVCategoryAll.Columns[10].Visible = false;
            }
            if (A63 == false && A113 == false)
                Response.Redirect("../PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetLastRecord();
            txtName.Focus();
            lblDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd hh:mm");
            FGetData();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IsDelete = @0 Order by IDNumberCategory Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtNumber.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDNumberCategory"]) + 1);
        else
            txtNumber.Text = "1";
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = WSM_Data_Access_Layer.GetData("Select * from CategoryShop Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                DLStore.SelectedValue = dt.Rows[0]["IDStore"].ToString();
                txtName.Text = dt.Rows[0]["CategoryName"].ToString();
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["DateAddCategory"]).ToString("yyyy/MM/dd");
                txtNote.Text = dt.Rows[0]["A1"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل صنف للمستودع";
                FGetCategoryAll();
            }
            else
                FGetCategoryAll();
        }
        else
            FGetCategoryAll();
    }

    private void FGetCategoryAll()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[CategoryShop] Where CategoryName Like '%' + @0 + '%' And IsDelete = @1 Order by IDNumberCategory", txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                GVCategoryAll.DataSource = dt;
                GVCategoryAll.DataBind();
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
            foreach (GridViewRow row in GVCategoryAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVCategoryAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_WSM_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[CategoryShop] SET [IsDelete] = @IsDelete WHERE CategoryID = @CategoryID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CategoryID", Comp_ID);
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
                    lblNumber.Text = "* يرجى إضافة رقم الصنف";
                }
                else
                {
                    lblNumber.Text = string.Empty;
                    if (txtName.Text.Trim() == string.Empty)
                    {
                        System.Threading.Thread.Sleep(100);
                        lblCheckName.Text = "* يرجى إضافة إسم الصنف";
                    }
                    else
                    {
                        lblCheckName.Text = string.Empty;
                        //string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                        //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                        System.Threading.Thread.Sleep(500);
                        lblCheckName.Visible = false;
                        FCheckNumber();
                    }
                }
            }
            else if (btnAdd.Text == "تعديل البيانات")
            {
                System.Threading.Thread.Sleep(500);
                FQariahEdit();
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
        dt = WSM_Data_Access_Layer.GetData("Select * from CategoryShop Where IDNumberCategory = @0 And IsDelete = @1", txtNumber.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "لا يمكن تكرار رقم الصنف قم بتغييره";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("Select * from CategoryShop Where IDStore = @0 And CategoryName = @1 And IsDelete = @2", DLStore.SelectedValue, txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة هذا الصنف مسبقاً";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
            FQariahAdd();
    }

    private void FQariahAdd()
    {
        GetCookie();
        WSM_ClassCategory CC = new WSM_ClassCategory()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _IDStore = Convert.ToInt32(DLStore.SelectedValue),
            _IDNumberCategory = Convert.ToInt64(txtNumber.Text.Trim()),
            _CategoryName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _DateAddCategory = ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = txtNote.Text.Trim(),
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CC.BArnCategoryShopAdd();
        lbmsg.Text = "تم إضافة الصنف بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetCategoryAll();
        FGetLastRecord();
    }

    private void FQariahEdit()
    {
        GetCookie();
        WSM_ClassCategory CC = new WSM_ClassCategory()
        {
            _IDUniq = Convert.ToString(Request.QueryString["ID"]),
            _IDStore = Convert.ToInt32(DLStore.SelectedValue),
            _CategoryName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = txtNote.Text.Trim(),
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0"
        };
        CC.BArnCategoryShopEdit();
        lbmsg.Text = "تم تعديل الصنف بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetCategoryAll();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetCategoryAll();
        System.Threading.Thread.Sleep(500);
    }

    public string FGetCount(float ID)
    {
        string XResult = "";
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT Count(*) As 'CountProductByCategory' FROM [dbo].[ProductShop] Where IDCategoryShop = @0 And IsDelete = @1", Convert.ToString(ID), Convert.ToString(false));
        if (dt.Rows.Count > 0)
            XResult = dt.Rows[0]["CountProductByCategory"].ToString();
        return XResult;
    }

}