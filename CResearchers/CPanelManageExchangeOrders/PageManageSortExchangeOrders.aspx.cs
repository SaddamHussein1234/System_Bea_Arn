using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_CPanelManageExchangeOrders_PageManageSortExchangeOrders : System.Web.UI.Page
{
    string XID;
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
            Response.Redirect("PageNotAccess.aspx");
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
            bool A59;
            A59 = Convert.ToBoolean(dtViewProfil.Rows[0]["A59"]);
            if (A59 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageSortExchangeOrders.aspx");
    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathith.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = true;
            IDTarmem.Visible = false;
            IDTalef.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RPTarmem_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTarmem.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = true;
            IDTalef.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RPTalef_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTalef.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = false;
            IDTalef.Visible = true;
            IDPrisms.Visible = false;
        }
    }

    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        if (RPSupportForPrisms.Checked)
        {
            pnlSelect.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = false;
            IDTalef.Visible = false;
            IDPrisms.Visible = true;
        }
    }

}