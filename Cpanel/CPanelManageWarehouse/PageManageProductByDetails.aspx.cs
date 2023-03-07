using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageWarehouse_PageManageProductByDetails : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        HttpCookie Cooke;  // رقم المستخدم
        Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
        IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

        HttpCookie CookeUniq;  // رقم يونيك
        CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
        IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
    }

    private void CheckAccountAdmin()
    {
        try
        {
            GetCookie();
            ClassAdmin_Arn CAA = new ClassAdmin_Arn();
            CAA._IDUniq = IDUniq;
            CAA._IsDelete = false;
            DataTable dtViewProfil = new DataTable();
            dtViewProfil = CAA.BArnAdminGetByIDUniq();
            if (dtViewProfil.Rows.Count > 0)
            {
                bool A61, A70, A120;
                A61 = Convert.ToBoolean(dtViewProfil.Rows[0]["A61"]);
                A70 = Convert.ToBoolean(dtViewProfil.Rows[0]["A70"]);
                A120 = Convert.ToBoolean(dtViewProfil.Rows[0]["A120"]);
                if (A70 == false)
                {
                    Response.Redirect("PageNotAccess.aspx");
                }
                if (A120 == false)
                {
                    IDAdd.Visible = false;
                }
                if (A61 == false)
                {
                    GVProductSet.Columns[8].Visible = false;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("LogOut.aspx");
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnProductShopWarehouseByProductGet();
        }
    }

    private void FArnProductShopWarehouseByProductGet()
    {
        ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
        CPS.billNumber = Convert.ToInt32(0);
        CPS.IDMosTafeed = Convert.ToInt32(0);
        CPS.IDProduct = Convert.ToInt64(Request.QueryString["XID"]);
        CPS.IDUniq = txtSearch.Text.Trim();
        CPS.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CPS.BArnProductShopWarehouseByProductGet();
        if (dt.Rows.Count > 0)
        {
            GVProductGet.DataSource = dt;
            GVProductGet.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            FArnProductShopWarehouseByProductSet();
            txtTitle.Text = " قائمة تفاصيل العمليات التي حصلت لـ " + dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["ProductName"].ToString() + "," + " الكمية المتبقية " + Convert.ToString(Convert.ToInt64(lblSum.Text) - Convert.ToInt64(lblSum2.Text));
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }

    }

    private void FArnProductShopWarehouseByProductSet()
    {
        ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
        CPS.billNumber = Convert.ToInt32(0);
        CPS.IDMosTafeed = Convert.ToInt32(0);
        CPS.IDProduct = Convert.ToInt64(Request.QueryString["XID"]);
        CPS.IDUniq = txtSearch.Text.Trim();
        CPS.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CPS.BArnProductShopWarehouseByProductSet();
        if (dt.Rows.Count > 0)
        {
            GVProductSet.DataSource = dt;
            GVProductSet.DataBind();
            lblCount2.Text = Convert.ToString(dt.Rows.Count);
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        GVProductSet.Columns[8].Visible = true;
        CheckAccountAdmin();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVProductSet.Columns[8].Visible = false;
            Session["footable"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable_.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVProductGet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
            Cou += int.Parse(Count.Text);
            lblSum.Text = Cou.ToString();

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
                lblTotalPrice.Text = sum.ToString();
            else
                lblTotalPrice.Text = "بإنتظار التسعير";
        }
    }

    int Cou2 = 0;
    decimal sum2 = 0;
    protected void GVProductSet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
            Cou2 += int.Parse(Count.Text);
            lblSum2.Text = Cou2.ToString();

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum2 += decimal.Parse(salary.Text);
            if (sum2 != 0)
                lblTotalPrice2.Text = sum2.ToString();
            else
                lblTotalPrice2.Text = "بإنتظار التسعير";
        }
    }

}