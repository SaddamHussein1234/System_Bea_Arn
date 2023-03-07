using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageContact_Us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + " إترك لنا رسالة ";
            if (Request.QueryString["Type"] != null)
                DLType.SelectedValue = Request.QueryString["Type"];
            txtName.Focus();
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (this.Page.IsValid && txtCapatsha.Text.ToString() == Session["randomNumber"].ToString())
                FContentAdd();
            else
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رمز التحقق غير صحيح ,,, ";
                txtCapatsha.Text = string.Empty;
                txtCapatsha.Focus();
            }
        }
        catch (Exception)
        {

        }
    }

    private void FContentAdd()
    {
        ClassContent Ccont = new ClassContent();
        Ccont.TitleMeassge = txtTitle.Text.Trim();
        Ccont.NameUser = txtName.Text.Trim();
        Ccont.CountryUser = "Saudi Arabia";
        Ccont.EmailUser = txtEmail.Text.Trim();
        Ccont.PhoneUser = txtPhone.Text.Trim();
        Ccont.DetailsMessega = txtMessage.Text.Trim().Replace(Environment.NewLine, "<br />");
        Ccont.DateSend = Convert.ToString(ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
        Ccont.TypeMessage = DLType.SelectedItem.Text;
        Ccont.BContentAdd();
        pnlMessage.Visible = false;
        pnlOK.Visible = true;
        this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + " إتصل بنا ";
    }

}