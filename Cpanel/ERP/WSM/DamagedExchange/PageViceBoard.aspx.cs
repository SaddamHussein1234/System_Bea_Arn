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

public partial class Cpanel_ERP_WSM_DamagedExchange_PageViceBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A130");
            Button1.Visible = false;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FArnProductShopByNaeeb(false, false, false, true);
        }
    }

    private void FArnProductShopByNaeeb(bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            GVApprovalOfTheDirector.UseAccessibleHeader = false;
            GVApprovalOfTheDirector.Columns[0].Visible = true;
            GVApprovalOfTheDirector.Columns[10].Visible = true;
            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            MEOB.IDCheck = "GetByApprovalOfTheNaeebDamaged";
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = 0;
            MEOB.ID_MosTafeed = 0;
            MEOB.Start_Date = string.Empty;
            MEOB.End_Date = string.Empty;
            MEOB.DataCheck = "0";
            MEOB.DataCheck2 = string.Empty;
            MEOB.DataCheck3 = string.Empty;
            MEOB.Is_Cart = Cart;
            MEOB.Is_Device = Device;
            MEOB.Is_Tathith = Tathith;
            MEOB.Is_Talef = Talef;
            MEOB.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
            dt = REOB.BWSM_Exchange_Order_Bill_Manage(MEOB);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف " + ddlYears.SelectedItem.ToString() + " , قائمة أوامر الصرف التي تحتاج إلى موافقة نائب الرئيس ( التالف ) ";
                GVApprovalOfTheDirector.DataSource = dt;
                GVApprovalOfTheDirector.DataBind();
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FArnProductShopByNaeeb(false, false, false, true);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVApprovalOfTheDirector.Columns[0].Visible = false;
            GVApprovalOfTheDirector.Columns[10].Visible = false;

            GVApprovalOfTheDirector.UseAccessibleHeader = true;
            GVApprovalOfTheDirector.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirector.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblID");
                    FAllowModer(new Guid(BillID.Text.ToString()));
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
            IDCheck = "ByNaeebBoard",
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
            Is_Naeb_Raees = true,
            Date_Naeb_Raees_Allow = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            ID_Naeb_Raees_Allow = Test_Saddam.FGetIDUsiq(),
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
            DeleteBy = 0,
            DeleteDate = string.Empty,
            IsActive = true,
            AlQaryah = 0
        };
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        string Xresult = REOB.FWSM_Exchange_Order_Bill_Add(MEOB);
        if (Xresult == "IsSuccessNaeeBoard")
        {
            System.Threading.Thread.Sleep(100);
            FArnProductShopByNaeeb(false, false, false, true);

            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            lblMessage.Text = "تم الموافقة على الملفات بنجاح ... ";
        }
    }

     decimal sum = 0;
    protected void GVApprovalOfTheDirector_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lblTotalPrice.Text = "0";
                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPrice.Text = sum.ToString();
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";
                lblMony.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FArnProductShopByNaeeb(false, false, false, true);
    }

    protected void LBRefrsh_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageViceBoard.aspx");
    }

}