<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestLoginWithGoogle.aspx.cs" Inherits="Cpanel_ERP_TestLoginWithGoogle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="jumbotron">
                <h2 class="text-info text-center">Google Authenticator</h2>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <div class="section">
                            <h3 class="text-info">Step 1: Install Google Authenticator  
                            </h3>
                            <p>Please download and install Google Authenticator on your IPhone/IPad/Android device, if already not installed.</p>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="section">
                            <h3 class="text-info">Step 2: Link your device to your account:  
                            </h3>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <asp:Image ID="imgQrCode" runat="server" />
                        </div>
                        <div class="col-md-6">
                            <div>
                                <span style="font-weight: bold; font-size: 14px;">Account Name:</span>
                            </div>
                            <div>
                                <asp:Label runat="server" ID="lblAccountName"></asp:Label>
                            </div>

                            <div>
                                <span style="font-weight: bold; font-size: 14px;">Secret Key:</span>
                            </div>
                            <div>
                                <asp:Label runat="server" ID="lblManualSetupCode"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="col-md-12" style="margin-top: 2%">
                        <div class="form-group col-md-4">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtSecurityCode" MaxLength="50" ToolTip="Please enter security code you get on your authenticator application">  
                            </asp:TextBox>
                        </div>
                        <asp:Button ID="btnValidate" OnClick="btnValidate_Click" CssClass="btn btn-primary" runat="server" Text="Validate" />
                    </div>
                    <h3>Result:</h3>
                    <div class="alert alert-success col-md-12" runat="server" role="alert">
                        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
