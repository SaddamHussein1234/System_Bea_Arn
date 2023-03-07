using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageZakat_PageDeedDonationInKindAll : System.Web.UI.Page
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
            bool A136, A137;
            A136 = Convert.ToBoolean(dtViewProfil.Rows[0]["A136"]);
            A137 = Convert.ToBoolean(dtViewProfil.Rows[0]["A137"]);
            if (A136 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A137 == false)
                btnDelete.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            pnlSelect.Visible = true;
            ClassQuaem.FGetProject(DL_ProjectNew);
        }
    }

    private void FGetAllByYears()
    {
        try
        {
            GVWarehouseCashier.Columns[0].Visible = true;
            GVWarehouseCashier.Columns[9].Visible = true;

            Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_();
            if (txtSearch.Text.Trim() != string.Empty)
                MWZB.IDCheck = "GetAllByYearsBySearch";
            else if (txtSearch.Text.Trim() == string.Empty)
                MWZB.IDCheck = "GetAllByYears";
            MWZB.IDUniq = Guid.Empty;
            MWZB.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MWZB.bill_Number = 0;
            MWZB.ID_Project = Convert.ToInt32(DL_ProjectNew.SelectedValue);
            MWZB.Start_Date = txtDateFrom.Text.Trim();
            MWZB.End_Date = txtDateTo.Text.Trim();
            MWZB.DateCheck = txtSearch.Text.Trim();
            MWZB.DateCheck2 = string.Empty;
            MWZB.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
            dt = RWZB.BArn_Warehouse_Zakat_Bill_Manage(MWZB);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة فواتير الزكاة من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim() + " - " + DL_ProjectNew.SelectedItem.ToString() + ", " + txtSearch.Text.Trim();
                GVWarehouseCashier.DataSource = dt;
                GVWarehouseCashier.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageDeedDonationInKindAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
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

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        FGetAllByYears();       
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        try
        {  
            GVWarehouseCashier.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVWarehouseCashier.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Xresult = string.Empty;
                    string Comp_ID = Convert.ToString(GVWarehouseCashier.DataKeys[row.RowIndex].Value);
                    btnDelete.Text = Comp_ID;
                    Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_()
                    {
                        IDCheck = "Delete",
                        IDUniq = Guid.Empty,
                        ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                        bill_Number = Convert.ToInt64(Comp_ID),
                        Name_Donor = string.Empty,
                        Phone_Donor = string.Empty,
                        ID_Project = 0,
                        Note_Bill = string.Empty,
                        IDRaeesMaglisAlEdarah = 0,
                        IsRaeesMaglisAlEdarah = false,
                        IDRaees_Allow = 0,
                        IDRaees_Date_Allow = string.Empty,
                        IDAmmenAlSondoq = 0,
                        IsAmmenAlSondoq = false,
                        IDAmmen_Allow = 0,
                        IDAmmen_Date_Allow = string.Empty,
                        IDModer = 0,
                        IsModer = false,
                        IDModer_Allow = 0,
                        IDModer_Date_Allow = string.Empty,
                        IDStorekeeper = 0,
                        IsStorekeeper = false,
                        IDStorekeeper_Allow = 0,
                        IDStorekeeper_Date_Allow = string.Empty,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsDelete = true
                    };
                    Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
                    Xresult = RWZB.FArn_Warehouse_Zakat_Bill_Add(MWZB);
                    if (Xresult == "IsSuccessDelete")
                    {
                        Model_Warehouse_Zakat_ MWZ = new Model_Warehouse_Zakat_()
                        {
                            IDCheck = "DeleteBill",
                            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                            bill_Number = Convert.ToInt64(Comp_ID),
                            ID_Category = 0,
                            CountProduct = 0,
                            One_Price = 0,
                            Total_Price = 0,
                            ID_Project = 0,
                            CreatedBy = 0,
                            CreatedDate = string.Empty,
                            DeleteBy = Test_Saddam.FGetIDUsiq(),
                            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                            IsDelete = true
                        };
                        Repostry_Warehouse_Zakat_ RWZ = new Repostry_Warehouse_Zakat_();
                        string XresultDetails = RWZ.FArn_Warehouse_Zakat_Add(MWZ);
                        if (XresultDetails == "IsSuccessDeleteBill")
                        {
                            System.Threading.Thread.Sleep(100);
                            IDMessageSuccess.Visible = true;
                            FGetAllByYears();
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        pnlNull.Visible = false;
        pnlData.Visible = false;
        pnlSelect.Visible = true;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-12-31");
    }

}