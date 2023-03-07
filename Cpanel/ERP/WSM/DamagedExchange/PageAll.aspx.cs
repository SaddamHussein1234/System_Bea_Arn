using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Models;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_WSM_DamagedExchange_PageAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A59");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            txtDateFrom.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, 1, 1).ToString("yyyy-MM-dd");
            txtDateTo.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            FGetCategoryShop();
            GVExchangeOrders.Columns[0].Visible = false;
            //GVExchangeOrders.Columns[8].Visible = false;
        }
    }

    private void FGetCategoryShop()
    {
        ClassQuaem.FGetSupportType(0, "'6'", CBCategory);
        FSelectCheck();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBYears.Items) { lst.Selected = true; }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    private void FGetData()
    {
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[0].Visible = true;
        GVExchangeOrders.Columns[7].Visible = true;

        string XYears = string.Empty;
        foreach (ListItem item in CBYears.Items)
            XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

        string XCategory = string.Empty;
        foreach (ListItem item in CBCategory.Items)
            XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

        try
        {
            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            MEOB.IDCheck = "GetSortExchangeOrdersDamaged";
            MEOB.ID_MosTafeed = 0;
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = Guid.NewGuid();
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = 0;

            MEOB.Start_Date = txtDateFrom.Text.Trim();
            MEOB.End_Date = txtDateTo.Text.Trim();
            MEOB.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MEOB.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MEOB.DataCheck3 = "0";
            MEOB.Is_Cart = false;
            MEOB.Is_Device = false;
            MEOB.Is_Tathith = false;
            MEOB.Is_Talef = false;
            MEOB.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
            dt = REOB.BWSM_Exchange_Order_Bill_Manage(MEOB);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة فرز أوامر صرف التالف من تاريخ " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("MM-dd-yyyy") +
                    " إلى تاريخ " + Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("MM-dd-yyyy");
                GVExchangeOrders.DataSource = dt;
                GVExchangeOrders.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
                IDFilter.Visible = true;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrders.Columns[0].Visible = false;
            GVExchangeOrders.Columns[7].Visible = false;

            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = IDPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDFilter.Visible = true;
    }

    decimal sum = 0;
    
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                {
                    lblTotalPrice.Text = sum.ToString();
                    lblMony.Text = ClassSaddam.FGetMonySa();
                }
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";

            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVExchangeOrders.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label XID = (Label)row.FindControl("lblID");

                    FAllowModer(new Guid(XID.Text.ToString()));
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FAllowModer(Guid XID)
    {
        WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_()
        {
            IDCheck = "Delete",
            ID_Item = XID,
            ID_FinancialYear = Guid.Empty,
            ID_Donor = Guid.Empty,
            bill_Number = 0,
            ID_MosTafeed = 0,
            The_Initiative = 0,
            ID_Project = 0,
            ID_Type_Shipment = string.Empty,
            Img_Product = string.Empty,
            ID_Raees_Maglis_AlEdarah = 0,
            Is_Raees_Maglis_AlEdarah = false,
            Date_Raees_Allow = string.Empty,
            ID_Raees_Allow = 0,
            ID_Naeb_Raees = 0,
            Is_Naeb_Raees = false,
            Date_Naeb_Raees_Allow = string.Empty,
            ID_Naeb_Raees_Allow = 0,
            ID_Ammen_AlSondoq = 0,
            Is_Ammen_AlSondoq = false,
            Date_Ammen_AlSondoq_Allow = string.Empty,
            ID_Ammen_AlSondoq_Allow = 0,
            ID_Moder = 0,
            Is_Moder = true,
            Date_Moder_Allow = string.Empty,
            ID_Moder_Allow = 0,
            ID_Storekeeper = 0,
            Is_Storekeeper = false,
            Date_Storekeeper_Allow = string.Empty,
            ID_Storekeeper_Allow = 0,
            Note = string.Empty,
            Is_Done = false,
            Is_Not_Done = false,
            Is_Received = false,
            Is_Not_Received = false,
            Note_Not_Received = string.Empty,
            ID_Delivery = 0,
            ID_Delivery_Allow = 0,
            The_Purpose = string.Empty,
            Is_Cart = true,
            Is_Device = false,
            Is_Tathith = false,
            Is_Talef = false,
            Count_Cart = 0,
            Count_Families = 0,
            Count_Qariah = 0,
            CreatedBy = 0,
            CreatedDate = string.Empty,
            ModifiedBy = 0,
            ModifiedDate = string.Empty,
            DeleteBy = Test_Saddam.FGetIDUsiq(),
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = false,
            AlQaryah = 0
        };
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        string Xresult = REOB.FWSM_Exchange_Order_Bill_Add(MEOB);
        if (Xresult == "IsSuccessDelete")
            FDeleteCategory(XID);
    }

    private void FDeleteCategory(Guid XIDBill)
    {
        string Xresult = string.Empty;
        WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_()
        {
            IDCheck = "DeleteBill",
            IDItem = Guid.Empty,
            ID_FinancialYear = Guid.Empty,
            ID_Bill = XIDBill,
            ID_Donor = Guid.Empty,
            bill_Number = 0,
            ID_MosTafeed = 0,
            ID_Product = 0,
            Count_Product = 0,
            One_Price = 0,
            Total_Price = 0,
            ID_Project = 0,
            Is_There_Partition = false,
            Count_Partition = 0,
            Sum_Partition = 0,
            CreatedBy = 0,
            CreatedDate = string.Empty,
            ModifiedBy = 0,
            ModifiedDate = string.Empty,
            DeleteBy = Test_Saddam.FGetIDUsiq(),
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = false
        };
        WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
        Xresult = REOD.FWSM_Exchange_Order_Details_Add(MEOD);
        if (Xresult == "IsSuccessDeleteBill")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblMessage.Text = "تم حذف الفاتورة بنجاح ... ";
            FGetData();
        }
    }

}