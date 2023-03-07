using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_WSM_PageProduct_PageAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A64", "A114", btnDelete1, GVProductAll, 0, 11);
            FGetLastRecord();
            txtName.Focus();
            lblDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd hh:mm");
            WSM_Repostry_Units_.FWSM_Units_Manage(DLUnits);
            FGetData();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT Top(1) * FROM [dbo].[ProductShop] With(NoLock) Where IsDelete = @0 Order by IDNumberProduct Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            txtNumber.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDNumberProduct"]) + 1);
        FGetAffiliationShop();
    }

    private void FGetAffiliationShop()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[AffiliationShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberAffiliation", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAffiliation.Items.Clear();
            DLAffiliation.Items.Add("");
            DLAffiliation.AppendDataBoundItems = true;
            DLAffiliation.DataValueField = "AffiliationID";
            DLAffiliation.DataTextField = "AffiliationName";
            DLAffiliation.DataSource = dt;
            DLAffiliation.DataBind();

            DLAffiliation2.Items.Clear();
            DLAffiliation2.Items.Add("");
            DLAffiliation2.AppendDataBoundItems = true;
            DLAffiliation2.DataValueField = "AffiliationID";
            DLAffiliation2.DataTextField = "AffiliationName";
            DLAffiliation2.DataSource = dt;
            DLAffiliation2.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberCategory", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLCategory.Items.Clear();
            DLCategory.Items.Add("");
            DLCategory.AppendDataBoundItems = true;
            DLCategory.DataValueField = "CategoryID";
            DLCategory.DataTextField = "CategoryName";
            DLCategory.DataSource = dt;
            DLCategory.DataBind();

            DLCategory2.Items.Clear();
            DLCategory2.Items.Add("");
            DLCategory2.AppendDataBoundItems = true;
            DLCategory2.DataValueField = "CategoryID";
            DLCategory2.DataTextField = "CategoryName";
            DLCategory2.DataSource = dt;
            DLCategory2.DataBind();
        }
    }

    private void FGetData()
    {
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = new DataTable();
            dt = WSM_Data_Access_Layer.GetData("Select Top(1) * from ProductShop With(NoLock) Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
            if (dt.Rows.Count > 0)
            {
                ProductAdd.Visible = true;
                DLAffiliation.SelectedValue = dt.Rows[0]["IDAffiliationShop"].ToString();
                DLCategory.SelectedValue = dt.Rows[0]["IDCategoryShop"].ToString();
                txtName.Text = dt.Rows[0]["ProductName"].ToString();
                CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["DateAddProduct"]).ToString("yyyy/MM/dd");
                txtNote.Text = dt.Rows[0]["A1"].ToString();
                DLUnits.SelectedValue = dt.Rows[0]["A2"].ToString();
                btnAdd.Text = "تعديل البيانات";
                lbmsg.Text = "تعديل منتج في المستودع";
                //CheckData();
            }
            //else
            //    CheckData();
        }
        //else
        //    CheckData();
    }

    private void CheckData()
    {
        GVProductAll.UseAccessibleHeader = false;
        if (DLAffiliation2.SelectedValue == string.Empty && DLCategory2.SelectedValue == string.Empty)
        {
            txtTitle.Text = "قائمة منتجات المستودع";
            FGetProductShopByAll(0, 0, 0);
        }
        else if (DLAffiliation2.SelectedValue != string.Empty && DLCategory2.SelectedValue == string.Empty)
        {
            txtTitle.Text = "قائمة منتجات المستودع" + " حسب إنتماء " + DLAffiliation2.SelectedItem.ToString();
            FGetProductShopByAll(1, Convert.ToInt64(DLAffiliation2.SelectedValue), 0);
        }
        else if (DLAffiliation2.SelectedValue == string.Empty && DLCategory2.SelectedValue != string.Empty)
        {
            txtTitle.Text = "قائمة منتجات المستودع" + " حسب صنف " + DLCategory2.SelectedItem.ToString();
            FGetProductShopByAll(2, 0, Convert.ToInt64(DLCategory2.SelectedValue));
        }
        else if (DLAffiliation2.SelectedValue != string.Empty && DLCategory2.SelectedValue != string.Empty)
        {
            txtTitle.Text = "قائمة منتجات المستودع" + " حسب إنتماء " + DLAffiliation2.SelectedItem.ToString() + " وصنف " + DLCategory2.SelectedItem.ToString();
            FGetProductShopByAll(3, Convert.ToInt64(DLAffiliation2.SelectedValue), Convert.ToInt64(DLCategory2.SelectedValue));
        }
    }

    private void FGetProductShopByAll(int IDCheck, Int64 IDAffiliationShop, Int64 IDCategoryShop)
    {
        try
        {
            WSM_ClassProduct CP = new WSM_ClassProduct();
            CP._IDCheck = IDCheck;
            CP._IDAffiliationShop = IDAffiliationShop;
            CP._IDCategoryShop = IDCategoryShop;
            CP._IDUniq = txtSearch.Text.Trim();
            CP._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CP.BArnProductShopByAll();
            if (dt.Rows.Count > 0)
            {
                GVProductAll.DataSource = dt;
                GVProductAll.DataBind();
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        ProductAdd.Visible = true;
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVProductAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVProductAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_WSM_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShop] SET [IsDelete] = @IsDelete WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ProductID", Comp_ID);
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
                    lblNumber.Text = "* يرجى إضافة رقم المنتج";
                    txtNumber.Focus();
                }
                else
                {
                    lblNumber.Text = string.Empty;
                    if (DLAffiliation.Text == string.Empty)
                    {
                        System.Threading.Thread.Sleep(100);
                        lblAffiliation.Text = "* يرجى تحديد إنتماء المنتج";
                        DLAffiliation.Focus();
                    }
                    else
                    {
                        lblAffiliation.Text = string.Empty;
                        if (DLCategory.Text == string.Empty)
                        {
                            System.Threading.Thread.Sleep(100);
                            lblCategory.Text = "* يرجى تحديد صنف المنتج";
                            DLCategory.Focus();
                        }
                        else
                        {
                            lblCategory.Text = string.Empty;
                            if (txtName.Text.Trim() == string.Empty)
                            {
                                System.Threading.Thread.Sleep(100);
                                lblCheckName.Text = "* يرجى إضافة إسم المنتج";
                                txtName.Focus();
                            }
                            else
                            {
                                lblCheckName.Text = string.Empty;
                                lblCheckName.Visible = false;
                                FCheckNumber();
                            }
                        }
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
        dt = WSM_Data_Access_Layer.GetData("Select Top(1) * from ProductShop With(NoLock) Where IDNumberProduct = @0 And IsDelete = @1", txtNumber.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لا يمكن تكرار رقم المنتج قم بتغييره";
            return;
        }
        else
            FCheckName();
    }

    private void FCheckName()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("Select Top(1) * from ProductShop With(NoLock) Where IDAffiliationShop = @0 And IDCategoryShop = @1 And ProductName = @2 And IsDelete = @3", DLAffiliation.SelectedValue, DLCategory.SelectedValue, txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة هذا المنتج مسبقاً";
            return;
        }
        else
            FAffiliationAdd();
    }

    private void FAffiliationAdd()
    {
        WSM_ClassProduct CP = new WSM_ClassProduct()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _IDNumberProduct = Convert.ToInt64(txtNumber.Text.Trim()),
            _IDAffiliationShop = Convert.ToInt64(DLAffiliation.SelectedValue),
            _IDCategoryShop = Convert.ToInt64(DLCategory.SelectedValue),
            _ProductName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _DateAddProduct = ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Test_Saddam.FGetIDUsiq(),
            _CountProduct = 0,
            _A1 = txtNote.Text.Trim(),
            _A2 = Convert.ToInt64(DLUnits.SelectedValue),
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CP.BArnProductShopAdd();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم إضافة المنتج بنجاح";
        txtNumber.Text = Convert.ToString(Convert.ToInt32(txtNumber.Text.Trim()) + 1);
        //CheckData();
    }

    private void FAffiliationEdit()
    {
        WSM_ClassProduct CP = new WSM_ClassProduct()
        {
            _IDUniq = Convert.ToString(Request.QueryString["ID"]),
            _IDAffiliationShop = Convert.ToInt64(DLAffiliation.SelectedValue),
            _IDCategoryShop = Convert.ToInt64(DLCategory.SelectedValue),
            _ProductName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IDAdmin = Test_Saddam.FGetIDUsiq(),
            _A1 = txtNote.Text.Trim(),
            _A2 = Convert.ToInt64(DLUnits.SelectedValue),
            _A3 = "0",
            _A4 = "0",
            _A5 = "0"
        };
        CP.BArnProductShopEdit();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم تعديل المنتج بنجاح";
        //CheckData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVProductAll.Columns[0].Visible = true;
        GVProductAll.Columns[12].Visible = true;
        CheckData();
        System.Threading.Thread.Sleep(200);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVProductAll.Columns[0].Visible = false;
            GVProductAll.Columns[12].Visible = false;

            GVProductAll.UseAccessibleHeader = true;
            GVProductAll.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}