using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_TestLoginWithGoogle : System.Web.UI.Page
{
    String AuthenticationCode
    {
        get
        {
            if (ViewState["AuthenticationCode"] != null)
                return ViewState["AuthenticationCode"].ToString().Trim();
            return String.Empty;
        }
        set
        {
            ViewState["AuthenticationCode"] = value.Trim();
        }
    }

    String AuthenticationTitle
    {
        get
        {
            return "SADDAM";
        }
    }

    String AuthenticationBarCodeImage
    {
        get;
        set;
    }

    String AuthenticationManualCode
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblResult.Text = String.Empty;
            lblResult.Visible = false;
            GenerateTwoFactorAuthentication();
            imgQrCode.ImageUrl = AuthenticationBarCodeImage;
            lblManualSetupCode.Text = AuthenticationManualCode;
            lblAccountName.Text = AuthenticationTitle;
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        String pin = txtSecurityCode.Text.Trim();
        Boolean status = ValidateTwoFactorPIN(pin);
        if (status)
        {
            lblResult.Visible = true;
            lblResult.Text = "Code Successfully Verified.";
        }
        else
        {
            lblResult.Visible = true;
            lblResult.Text = "Invalid Code.";
        }
    }

    public Boolean ValidateTwoFactorPIN(String pin)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        return tfa.ValidateTwoFactorPIN(AuthenticationCode, pin);
    }

    public Boolean GenerateTwoFactorAuthentication()
    {
        AuthenticationCode = FGetSecrtKey();

        Dictionary<String, String> result = new Dictionary<String, String>();
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        var setupInfo = tfa.GenerateSetupCode("BerArn", AuthenticationTitle, AuthenticationCode, false, 3);
        if (setupInfo != null)
        {
            AuthenticationBarCodeImage = setupInfo.QrCodeSetupImageUrl;
            AuthenticationManualCode = setupInfo.ManualEntryKey;
            return true;
        }
        return false;
    }

    private string FGetSecrtKey()
    {
        Guid guid = new Guid("51B82097-8AD5-4939-8ED7-E5AABB08D8A5");
        String uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);
        return uniqueUserKey;
    }

}
