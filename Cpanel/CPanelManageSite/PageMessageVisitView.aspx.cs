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

public partial class Cpanel_CPanelManageSite_PageMessageVisitView : System.Web.UI.Page
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
            bool A21;
            A21 = Convert.ToBoolean(dtViewProfil.Rows[0]["A21"]);
            if (A21 == false)
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
            FContentView();
        }
    }

    private void FContentView()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[tbl_Content] with(NoLock) Where IDCont = @0 And IsDelete = @1", Convert.ToString(Request.QueryString["ID"]), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblTypeMessage.Text = dt.Rows[0]["TypeMessage"].ToString();
            lblTitle.Text = dt.Rows[0]["TitleMeassge"].ToString();
            lblNameUser.Text = dt.Rows[0]["NameUser"].ToString();
            lblCity.Text = dt.Rows[0]["CountryUser"].ToString();
            lblEmail.Text = dt.Rows[0]["EmailUser"].ToString();
            lblPhone.Text = dt.Rows[0]["PhoneUser"].ToString();
            lblDate.Text = Convert.ToDateTime(dt.Rows[0]["DateSend"]).ToString("yyyy/MM/dd");
            lblMessage.Text = dt.Rows[0]["DetailsMessega"].ToString();

            ClassContent CCon = new ClassContent();
            CCon.IDCont = Convert.ToInt64(Request.QueryString["ID"]);
            CCon.ViewRead = true;
            CCon.BContentView();

        }
        else
        {
            Response.Redirect("PageMassageVisit.aspx");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(100);
        Response.Redirect("PageMassageVisit.aspx");
    }

}