<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CHome/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMyAccount.aspx.cs" Inherits="Cpanel_CHome_PageMyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        function Confirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <style type="text/css">
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>
    <link href="../css/chosen.css" rel="stylesheet" />
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    عدد الأيام المسموحة لجلسة الدخول : 
                <asp:TextBox ID="txtCount_Day" Width="70" runat="server" class="form-control2" ValidationGroup="GType" MaxLength="20" TextMode="Number" Height="30"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                    ControlToValidate="txtCount_Day" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                ValidationGroup="GType" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:LinkButton ID="LBEditCountDay" runat="server" class="btn btn-info" data-toggle="tooltip"
                        title="تحديث النوع" Style="margin-left: 5px"  ValidationGroup="GType" OnClick="LBEditCountDay_Click">
                <i class="fa fa-edit"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="">حسابي</a></li>
                    <li><a href="PageMyAccount.aspx">بياناتي </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="بياناتي"></asp:Label>
                    </h3>
                </div>
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
                        <span id="lblSuccess" runat="server"></span>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div style="float: left; padding: 5px;">
                                <h5>الصور الشخصية : </h5>
                                <asp:Image ID="ImgAdmin" runat="server" Style="border-radius: 6px" Width="256" Height="192" />
                                <br />
                                <span>سيقوم النظام بتحويل العرض 256 بيكسل والطول الى 192 بكسل</span>
                                <asp:FileUpload ID="FUImgAdmin" runat="server" />
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>تنتمي الى المجموعة : </h5>
                                    <asp:TextBox ID="lblGroup" runat="server" class="form-control" ValidationGroup="g2" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم الهاتف : </h5>
                                        <asp:TextBox ID="txtPhone" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtPhone" ErrorMessage="* رقم الهاتف" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الإسم : </h5>
                                        <asp:TextBox ID="txtNameFirst" runat="server" class="form-control" ValidationGroup="g2" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtNameFirst" ErrorMessage="* إدخل الإسم" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم السجل المدني : </h5>
                                        <asp:TextBox ID="txtNameLast" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="txtNameLast" ErrorMessage="* رقم السجل المدني" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إسم المستخدم : </h5>
                                        <asp:TextBox ID="txtUserName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtUserName" ErrorMessage="* إسم المستخدم" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>البريد الإلكتروني : </h5>
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* البريد الإلكتروني" CssClass="font"
                                        ControlToValidate="txtEmail" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                        ControlToValidate="txtEmail"
                                        ErrorMessage="بريد خطأ"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="Red" ValidationGroup="g2" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>حالة تنشيط الحساب : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBView" runat="server" disabled="disabled" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a href="PageAuthenticatorWithSMS.aspx" runat="server" id="IDActiveSMS" visible="true"
                                            class="btn btn-success" style="font-weight:bold"
                                            >تفعيل التحقق عبر SMS <i class="fa fa-envelope" style="font-size:30px;"></i></a>

                                        <a href="PageAuthenticatorWithSMS.aspx" runat="server" id="IDUnActiveSMS" visible="false"
                                            class="btn btn-warning" style="font-weight:bold"
                                            >إلغاء تفعيل التحقق عبر SMS <i class="fa fa-envelope" style="font-size:30px;"></i></a>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a href="PageGoogleAuthenticator.aspx" runat="server" id="IDActive" visible="false"
                                            class="btn btn-success" style="font-weight:bold"
                                            >تفعيل المصادقة الثنائية <img src="../icon/GA.png" width="30" /></a>

                                        <a href="PageGoogleAuthenticator.aspx" runat="server" id="IDUnActive" visible="false"
                                            class="btn btn-warning" style="font-weight:bold"
                                            >إلغاء تفعيل المصادقة الثنائية <img src="../icon/GA.png" width="30" /></a>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LBLogOutAll" runat="server" OnClientClick="return Confirmation();" OnClick="LBLogOutAll_Click" Visible="false"
                                            CssClass="btn btn-danger" style="font-weight:bold">تسجيل الخروج لجميع المستخدمين <i class="fa fa-sign-out fa-2x"></i></asp:LinkButton>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div style="float: left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px; font-size: medium" OnClick="btnAdd_Click"
                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="margin-right: 4px; font-size: medium" OnClick="LBBack_Click"
                    class="btn btn-danger btn-fill pull-left">خروج</asp:LinkButton>
            </div>
            <div style="width: 50%">
            </div>
            <br />
            <br />
        </div>
        <br />
        <br />
        <br />

        <script type="text/javascript"><!--
    $('.date').datetimepicker({
        pickTime: false
    });

    $('.time').datetimepicker({
        pickDate: false
    });

    $('.datetime').datetimepicker({
        pickDate: true,
        pickTime: true
    });
    //--></script>
        <script type="text/javascript"><!--
    $('#language a:first').tab('show');
    $('#option a:first').tab('show');
    //--></script>
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

