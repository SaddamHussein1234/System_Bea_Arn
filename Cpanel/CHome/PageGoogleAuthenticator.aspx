<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CHome/MPCPanel.master" AutoEventWireup="true" CodeFile="PageGoogleAuthenticator.aspx.cs" Inherits="Cpanel_CHome_PageGoogleAuthenticator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            if (confirm("هل تريد إلغاء المصادقة , لا ينصح بذلك سيعرض حسابك للخطر ؟") == true)
                return true;
            else
                return false;
        }
    </script>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageMyAccount.aspx">حسابي</a></li>
                    <li><a href="PageGoogleAuthenticator.aspx">المصادقة الثنائية </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <img src="../icon/GA.png" width="30" />
                        <asp:Label ID="lbmsg" runat="server" Text="تفعيل المصادقة الثنائية"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-warning">تحذير</span>
                            <asp:Label ID="lblWarning" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-success">عملية ناجحة</span>
                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="content-box-large">
                        <div class="widget-box">
                            <asp:Panel ID="pnlActive" runat="server" Visible="false">
                                <div style="float: left; padding: 5px;">
                                    <h5><strong>قم بفتح التطبيق وألتقت الباركود : </strong></h5>
                                    <asp:Image ID="ImgAdmin" runat="server" Style="border-radius: 6px" />
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5><strong> <asp:Label ID="lblCheck" runat="server"></asp:Label> : </strong></h5>
                                            <div class="col-md-12">
                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5><strong>رقم الهاتف : </strong></h5>
                                            <div class="col-md-12">
                                                <asp:HiddenField ID="HFPhone" runat="server" />
                                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5><strong>إسم الحساب : </strong></h5>
                                            <div class="col-md-12">
                                                <asp:HiddenField ID="HFIDUniq" runat="server" />
                                                <asp:HiddenField ID="HFUser_Name" runat="server" />
                                                <asp:Label ID="lblUser" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" align="left">
                                        <div class="form-group">
                                            <h5><strong>: Secret Key</strong></h5>
                                            <div class="col-md-12">
                                                <asp:Label ID="lblManualSetupCode" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <hr />
                                            <h5><strong>قم بكتباة الرمز بعد إلتقاط الباركود : </strong></h5>
                                            <asp:TextBox ID="txt_Code" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txt_Code" ErrorMessage="* الكود" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5>
                                                <i class="fa fa-star"></i><strong> إذا كان جهازك أندرويد قم بتحميل التطبيق من خلال 
                                            <a target="_blank" data-toggle="tooltip" title="الذهاب للمتجر"
                                                href="https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&amp;hl=en">Play Store
                                                <img src="../icon/Android.png" width="30" /></a>
                                                </strong>
                                            </h5>
                                            <h5>
                                                <i class="fa fa-star"></i><strong> أما إذا كان جهازك أيفون قم بتحميل التطبيق من خلال 
                                                <a target="_blank" data-toggle="tooltip" title="الذهاب للمتجر"
                                                    href="https://itunes.apple.com/us/app/google-authenticator/id388497605?mt=8">App Store
                                                    <img src="../icon/IOS.png" width="30" /></a>
                                                </strong>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlActived" runat="server" Visible="false">
                                <div class="col-md-7">
                                    <div class="container-fluid" dir="rtl">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <h5><strong> <asp:Label ID="lblCheck2" runat="server"></asp:Label> : </strong></h5>
                                            <div class="col-md-12">
                                                <asp:Label ID="lblName2" runat="server"></asp:Label>
                                            </div>
                                            <hr />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <h5><strong>إسم الحساب : </strong></h5>
                                            <div class="col-md-12">
                                                <asp:Label ID="lblUser2" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <hr />
                                            <asp:LinkButton ID="LBUnActive" runat="server" CssClass="btn btn-warning" OnClientClick="return ConfirmDelete();"
                                                OnClick="LBUnActive_Click"
                                                style="Font-Size:14px;font-weight:bold">
                                                إلغاء تفعيل المصادقة الثنائية <img src="../icon/GA.png" width="30" /></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <img src="../icon/BGSpy.jpg" Style="border-radius: 6px; width:99%;" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <div class="container-fluid">
            <div style="float: left">
                <asp:Button ID="btnAdd" runat="server" Text="تفعيل المصادقة" Visible="false"
                    Style="margin-right: 4px; font-size: medium" OnClick="btnAdd_Click"
                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                <a href="PageMyAccount.aspx" Style="margin-right: 4px; font-size: medium" 
                    class="btn btn-danger btn-fill pull-left">خروج</a>
            </div>
            <div style="width: 50%">
            </div>
            <br />
            <br />
        </div>
        <br />
        <br />
        <br />
</asp:Content>

