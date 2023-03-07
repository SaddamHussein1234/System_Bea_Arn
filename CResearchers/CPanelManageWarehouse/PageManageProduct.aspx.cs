using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
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

public partial class CResearchers_CPanelManageWarehouse_PageManageProduct : System.Web.UI.Page
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
            bool A64, A114;
            A64 = Convert.ToBoolean(dtViewProfil.Rows[0]["A64"]);
            A114 = Convert.ToBoolean(dtViewProfil.Rows[0]["A114"]);
            if (A64 == false)
            {
                View.Visible = false;
                ProductAdd.Visible = true;
            }
            if (A114 == false)
            {
                btnNew.Visible = false;
                ProductAdd.Visible = false;
                btnDelete1.Visible = false;
                GVProductAll.Columns[0].Visible = false;
                GVProductAll.Columns[11].Visible = false;
            }
            if (A64 == false && A114 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetLastRecord();
            txtName.Focus();
            lblDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd hh:mm");
            Repostry_Units_.FErp_Units_Manage(DLUnits);
            FGetData();
            CheckAccountAdmin();
            FGetAmeenAlMostwdaa();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[ProductShop] With(NoLock) Where IsDelete = @0 Order by IDNumberProduct Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtNumber.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IDNumberProduct"]) + 1);
        }
        FGetAffiliationShop();
    }

    private void FGetAffiliationShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[AffiliationShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberAffiliation", Convert.ToString(true), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberCategory", Convert.ToString(true), Convert.ToString(false));
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
            dt = ClassDataAccess.GetData("Select * from ProductShop Where IDUniq = @0", Convert.ToString(Request.QueryString["ID"]));
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
                CheckData();
            }
            else
            {
                CheckData();
            }
        }
        else
        {
            CheckData();
        }
    }

    private void CheckData()
    {
        DLIDStorekeeper.Visible = true;
        DLModerAlGmeiah.Visible = true;
        DLAmeenAlSondoq.Visible = true;
        DLRaeesMaglesAlEdarah.Visible = true;
        lblIDStorekeeper.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblAmeenAlSondoq.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
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
            ClassProduct CP = new ClassProduct();
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
        Response.Redirect("PageManageProduct.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DLIDStorekeeper.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLAmeenAlSondoq.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        lblIDStorekeeper.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblAmeenAlSondoq.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;
        GVProductAll.Columns[0].Visible = false;
        GVProductAll.Columns[11].Visible = false;
        lblIDStorekeeper.Text = DLIDStorekeeper.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblAmeenAlSondoq.Text = DLAmeenAlSondoq.SelectedItem.ToString();

        GVProductAll.UseAccessibleHeader = true;
        GVProductAll.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
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

    private void FGetAmeenAlMostwdaa()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsStorekeeper = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLIDStorekeeper.DataValueField = "ID_Item";
            DLIDStorekeeper.DataTextField = "FirstName";
            DLIDStorekeeper.DataSource = dt;
            DLIDStorekeeper.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetAmeenAlsondoq();
    }

    private void FGetAmeenAlsondoq()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsAmeenAlSondoq = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAmeenAlSondoq.DataValueField = "ID_Item";
            DLAmeenAlSondoq.DataTextField = "FirstName";
            DLAmeenAlSondoq.DataSource = dt;
            DLAmeenAlSondoq.DataBind();
        }
        ImgIDStorekeeper.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLIDStorekeeper.SelectedValue));
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    int tempcounter = 0;
    protected void GVProductAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    protected void DLIDStorekeeper_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgIDStorekeeper.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLIDStorekeeper.SelectedValue));
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLAmeenAlSondoq_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgAmeenAlSondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoq.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
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
                                System.Threading.Thread.Sleep(500);
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
        dt = ClassDataAccess.GetData("Select * from ProductShop Where IDNumberProduct = @0 And IsDelete = @1", txtNumber.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "لا يمكن تكرار رقم المنتج قم بتغييره";
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
        dt = ClassDataAccess.GetData("Select * from ProductShop Where IDAffiliationShop = @0 And IDCategoryShop = @1 And ProductName = @2 And IsDelete = @3", DLAffiliation.SelectedValue, DLCategory.SelectedValue, txtName.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lbmsg.Text = "تم إضافة هذا المنتج مسبقاً";
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
        ClassProduct CP = new ClassProduct()
        {
            _IDUniq = Convert.ToString(Guid.NewGuid()),
            _IDNumberProduct = Convert.ToInt64(txtNumber.Text.Trim()),
            _IDAffiliationShop = Convert.ToInt64(DLAffiliation.SelectedValue),
            _IDCategoryShop = Convert.ToInt64(DLCategory.SelectedValue),
            _ProductName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _DateAddProduct = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"),
            _IDAdmin = Convert.ToInt32(IDUser),
            _CountProduct = 0,
            _A1 = txtNote.Text.Trim(),
            _A2 = Convert.ToInt64(DLUnits.SelectedValue),
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CP.BArnProductShopAdd();
        lbmsg.Text = "تم إضافة المنتج بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        CheckData();
    }

    private void FAffiliationEdit()
    {
        GetCookie();
        ClassProduct CP = new ClassProduct()
        {
            _IDUniq = Convert.ToString(Request.QueryString["ID"]),
            _IDAffiliationShop = Convert.ToInt64(DLAffiliation.SelectedValue),
            _IDCategoryShop = Convert.ToInt64(DLCategory.SelectedValue),
            _ProductName = txtName.Text.Trim(),
            _IsActive = Convert.ToBoolean(CBActive.Checked),
            _IDAdmin = Convert.ToInt32(IDUser),
            _A1 = txtNote.Text.Trim(),
            _A2 = Convert.ToInt64(DLUnits.SelectedValue),
            _A3 = "0",
            _A4 = "0",
            _A5 = "0"
        };
        CP.BArnProductShopEdit();
        lbmsg.Text = "تم تعديل المنتج بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        CheckData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVProductAll.Columns[0].Visible = true;
        GVProductAll.Columns[11].Visible = true;
        CheckData();
        System.Threading.Thread.Sleep(200);
    }

}