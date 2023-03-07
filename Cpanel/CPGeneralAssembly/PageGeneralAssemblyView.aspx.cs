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

public partial class Cpanel_CPGeneralAssembly_PageGeneralAssemblyView : System.Web.UI.Page
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
            bool A143;
            A143 = Convert.ToBoolean(dtViewProfil.Rows[0]["A143"]);
            if (A143 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FGetData();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
        System.Threading.Thread.Sleep(500);
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT TOP (1) TGA.*,TA.FirstName,TA.FirstName,[Email],[PhoneNumber],[A3],TA.[AddImgSignature] FROM [dbo].[tbl_General_Assmply] TGA With(NoLock) Inner Join tbl_Admin TA on TA.ID_Item = TGA.ID_Admin_Account_  Where [Number_Rigstry_] = @0 And [Is_Delete_] = @1",
                txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblNumber_Rigstry.Text = dt.Rows[0]["Number_Rigstry_"].ToString();
                lblNumber_Rigstry2.Text = lblNumber_Rigstry.Text;
                lblFull_Name.Text = dt.Rows[0]["FirstName"].ToString();
                lblFull_Name2.Text = lblFull_Name.Text;
                lblDate_Bridth.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["Date_Bridth_"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["Date_Bridth_"]).ToString("dd/MM/yyyy") + "مـ";
                lblThe_Job.Text = dt.Rows[0]["The_Job_"].ToString();
                lblAddress_Job.Text = dt.Rows[0]["Address_Job_"].ToString();
                lblID_Card.Text = dt.Rows[0]["A3"].ToString();
                lblDate_Card.Text = Convert.ToDateTime(dt.Rows[0]["Date_Card_"]).ToString("dd/MM/yyyy");
                lblCard_Source.Text = dt.Rows[0]["Card_Source_"].ToString();
                lblPhone_Home.Text = dt.Rows[0]["Phone_Home_"].ToString();
                lblPhone_Work.Text = dt.Rows[0]["Phone_Work_"].ToString();
                lblPhone_Other.Text = dt.Rows[0]["Phone_Other_"].ToString();
                lblPhone_Personal.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblEmail.Text = dt.Rows[0]["Email"].ToString();
                lblBox_Email.Text = dt.Rows[0]["Box_Email_"].ToString();
                lblSerial_Email.Text = dt.Rows[0]["Serial_Email_"].ToString();
                lblAddress.Text = dt.Rows[0]["Address_"].ToString();
                lbl_Date_Rigstry.Text = Convert.ToDateTime(dt.Rows[0]["Date_Rigstry_"]).ToString("dd/MM/yyyy");
                CBIs_Aamel.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Aamel_"]);
                CBIs_Montaseeb.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Montaseeb_"]);
                Img_Signature.ImageUrl = "/" + dt.Rows[0]["AddImgSignature"].ToString();
                lblNumber_Qarar.Text = dt.Rows[0]["Number_Qarar_"].ToString();
                lblDate_Qarar.Text = Convert.ToDateTime(dt.Rows[0]["Date_Qarar_"]).ToString("dd/MM/yyyy");
                lblDate_Qobol.Text = Convert.ToDateTime(dt.Rows[0]["Date_Qobol_"]).ToString("dd/MM/yyyy");
                if (CBIs_Aamel.Checked && CBIs_Montaseeb.Checked == false)
                    lblCheck.Text = "عامل";
                else if (CBIs_Aamel.Checked == false && CBIs_Montaseeb.Checked)
                    lblCheck.Text = "منتسب";

                lblReesAlmaglis.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Raees_AlMagles_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Is_Allow_"]))
                {
                    ImgRaees_AlMagles.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Raees_AlMagles_"]), Convert.ToBoolean(dt.Rows[0]["Is_Allow_"]));
                    ImgRaees_AlMagles.Visible = true;
                    IDKhatm.Visible = true;
                }
                else
                {
                    ImgRaees_AlMagles.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgRaees_AlMagles.Width = 30;
                    ImgRaees_AlMagles.Visible = true;
                    IDKhatm.Visible = false;
                }
                lblDateEntery.Text = Convert.ToDateTime(dt.Rows[0]["Date_Add_"]).ToString("dd/MM/yyyy");
                lblDataEntery.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Admin_"].ToString()));
                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/CPGeneralAssembly/PageGeneralAssemblyView.aspx?ID=" + txtSearch.Text.Trim();
                Class_QRScan.FGetQRCode(code, imgBarCode);

                pnlPrint.Visible = true;
                pnlSelect.Visible = false;
            }
            else
            {
                pnlPrint.Visible = false;
                pnlSelect.Visible = true;
            }
        }
        catch (Exception)
        {
            Response.Redirect("PageGeneralAssembly.aspx");
        }
    }

}