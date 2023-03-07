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

public partial class Cpanel_CPanelManageSite_PageMenuEdit : System.Web.UI.Page
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
            bool MenuAdd;
            MenuAdd = Convert.ToBoolean(dtViewProfil.Rows[0]["A10"]);
            if (MenuAdd == false)
            {
                lbmsg.Text = "لا تمتلك صلاحية التعديل ";
                lbmsg.ForeColor = System.Drawing.Color.Red;
                txtAr.Enabled = false;
                txtTr.Enabled = false;
                txtEn.Enabled = false;
                txtOrder.Enabled = false;
                txtDetails.Enabled = false;
                CBFork.Disabled = false;
                CBView.Disabled = false;
                rblCheck.Enabled = false;
                DLMenu.Enabled = false;
                btnAdd.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetMenu();
            FGetDetails();
        }
    }

    private void FGetMenu()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_MenuSite] Where TypeSection = @0 Order By IDOrder", Convert.ToString(1));
        if (dt.Rows.Count > 0)
        {
            DLMenu.Items.Clear();
            DLMenu.Items.Add("");
            DLMenu.AppendDataBoundItems = true;
            DLMenu.DataValueField = "IDItem";
            DLMenu.DataTextField = "TitleManageAr";
            DLMenu.DataSource = dt;
            DLMenu.DataBind();
        }
    }

    private void FGetDetails()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_MenuSite] Where IDItem = @0", Request.QueryString["ID"]);
            if (dt.Rows.Count > 0)
            {
                txtAr.Text = dt.Rows[0]["TitleManageAr"].ToString();
                Session["OldNameAr"] = txtAr.Text;
                txtTr.Text = dt.Rows[0]["TitleManageTr"].ToString();
                txtEn.Text = dt.Rows[0]["TitleManageEn"].ToString();
                txtOrder.Text = dt.Rows[0]["IDOrder"].ToString();

                if (Convert.ToInt16(dt.Rows[0]["TypeSection"]) == 1)
                {
                    rblCheck.SelectedValue = "1";
                }
                else if (Convert.ToInt16(dt.Rows[0]["TypeSection"]) == 2)
                {
                    rblCheck.SelectedValue = "2";
                    if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
                    {
                        IDMenu.Visible = false;
                    }
                    else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
                    {
                        IDMenu.Visible = true;
                    }
                }
                DLMenu.SelectedValue = dt.Rows[0]["IDPartSection"].ToString();
                CBView.Checked = Convert.ToBoolean(dt.Rows[0]["ViewMenu"]);
                CBFork.Checked = Convert.ToBoolean(dt.Rows[0]["IsFork"]);
                txtDetails.Text = dt.Rows[0]["DescriptionManage"].ToString();
            }
            else
            {
                Response.Redirect("PageMenu.aspx");
            }
        }
        catch (Exception)
        {

        }
    }

    protected void rblCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
        {
            IDMenu.Visible = false;
            CBFork.Disabled = false;
            CBFork.Checked = true;
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
        {
            IDMenu.Visible = true;
            CBFork.Disabled = true;
            CBFork.Checked = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtAr.Text.Trim() != Session["OldNameAr"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_MenuSite] Where TitleManageAr = @0", txtAr.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                lbmsg.Text = "تم إضافة القائمة سابقاً";
                lbmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Session["OldNameAr"] = txtAr.Text.Trim();
                if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
                {
                    FEditMenu();
                }
                else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
                {
                    if (DLMenu.SelectedItem.ToString() == string.Empty || DLMenu.SelectedItem.ToString() == "")
                    {
                        lbmsg.Text = "الرجاء تحديد قائمة رئيسية للقائمة الفرعية";
                        lbmsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        FEditMenu();
                    }
                }
            }
        }
        else
        {
            FEditMenu();
        }
    }

    private void FEditMenu()
    {
        ClassMenuSite CMS = new ClassMenuSite();
        CMS.IDItem = Convert.ToInt32(Request.QueryString["ID"]);
        CMS.TitleManageAr = Session["OldNameAr"].ToString();
        CMS.TitleManageTr = txtTr.Text.Trim();
        CMS.TitleManageEn = txtEn.Text.Trim();
        CMS.IDOrder = Convert.ToInt32(txtOrder.Text.Trim());
        if (Convert.ToInt32(rblCheck.SelectedValue) == 1)
        {
            CMS.TypeSection = 1;
            CMS.IDPartSection = 0;
        }
        else if (Convert.ToInt32(rblCheck.SelectedValue) == 2)
        {
            CMS.TypeSection = 2;
            CMS.IDPartSection = Convert.ToInt32(DLMenu.SelectedValue);
        }
        CMS.ViewMenu = Convert.ToBoolean(CBView.Checked);
        CMS.IsFork = Convert.ToBoolean(CBFork.Checked);
        CMS.DescriptionManage = txtDetails.Text.Trim();
        CMS.BArnMenuEdit();
        lbmsg.Text = "تم تعديل القائمة بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        FGetDetails();

        //GetCookie();
        //ClassTrickerAdmin.TrickerAdd(Convert.ToInt32(IDUser), "تعديل", " تعديل قائمة " + txtAr.Text.Trim(), ClassKhwarism.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Remove("OldNameAr");
        Response.Redirect("PageMenu.aspx");
    }

}