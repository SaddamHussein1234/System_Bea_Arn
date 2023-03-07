using Library_CLS_Arn.ClassEntity.Warehouse.Models;
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

public partial class Cpanel_CPanelManageZakat_PageManageProductWarehouseStorekeeper : System.Web.UI.Page
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
            bool A142;
            A142 = Convert.ToBoolean(dtViewProfil.Rows[0]["A142"]);
            if (A142 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetByStorekeeper();
        }
    }

    private void FGetByStorekeeper()
    {
        try
        {
            GVWarehouseCashier.Columns[0].Visible = true;
            GVWarehouseCashier.Columns[9].Visible = true;

            Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_();
            MWZB.IDCheck = "GetByStorekeeper";
            MWZB.IDUniq = Guid.Empty;
            MWZB.ID_FinancialYear = Guid.Empty;
            MWZB.bill_Number = 0;
            MWZB.ID_Project = 0;
            MWZB.Start_Date = string.Empty;
            MWZB.End_Date = string.Empty;
            MWZB.DateCheck = "1";
            MWZB.DateCheck2 = string.Empty;
            MWZB.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
            dt = RWZB.BArn_Warehouse_Zakat_Bill_Manage(MWZB);

            if (dt.Rows.Count > 0)
            {
                GVWarehouseCashier.DataSource = dt;
                GVWarehouseCashier.DataBind();
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
        Response.Redirect("PageManageProductWarehouseStorekeeper.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVWarehouseCashier.Columns[0].Visible = false;
            GVWarehouseCashier.Columns[9].Visible = false;

            GVWarehouseCashier.UseAccessibleHeader = true;
            GVWarehouseCashier.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            int XID = Test_Saddam.FGetIDUsiq();
            string XDate = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
            foreach (GridViewRow row in GVWarehouseCashier.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVWarehouseCashier.DataKeys[row.RowIndex].Value);
                    Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_()
                    {
                        IDCheck = "ByStorekeeper",
                        IDUniq = Guid.Empty,
                        ID_FinancialYear = Guid.Empty,
                        bill_Number = Convert.ToInt32(Comp_ID),
                        Name_Donor = string.Empty,
                        Phone_Donor = string.Empty,
                        ID_Project = 0,
                        Note_Bill = string.Empty,
                        IDRaeesMaglisAlEdarah = 0,
                        IsRaeesMaglisAlEdarah = true,
                        IDRaees_Allow = XID,
                        IDRaees_Date_Allow = string.Empty,
                        IDAmmenAlSondoq = 0,
                        IsAmmenAlSondoq = false,
                        IDAmmen_Allow = XID,
                        IDAmmen_Date_Allow = string.Empty,
                        IDModer = 0,
                        IsModer = false,
                        IDModer_Allow = XID,
                        IDModer_Date_Allow = string.Empty,
                        IDStorekeeper = 0,
                        IsStorekeeper = true,
                        IDStorekeeper_Allow = XID,
                        IDStorekeeper_Date_Allow = XDate,
                        CreatedBy = XID,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        DeleteBy = 0,
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsDelete = false
                    };
                    Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
                    string Xresult = RWZB.FArn_Warehouse_Zakat_Bill_Add(MWZB);
                    if (Xresult == "IsSuccessStorekeeper")
                    {
                        System.Threading.Thread.Sleep(100);
                        IDMessageSuccess.Visible = true;
                        FGetByStorekeeper();
                    }
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;

    protected void GVWarehouseCashier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPrice.Text = sum.ToString();
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";
                lblMonyType.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

}