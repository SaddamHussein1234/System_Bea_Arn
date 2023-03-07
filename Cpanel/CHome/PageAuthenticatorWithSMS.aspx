<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CHome/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAuthenticatorWithSMS.aspx.cs" Inherits="Cpanel_CHome_PageAuthenticatorWithSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <script type="text/javascript">
        function ConfirmSend() {
            if (confirm("هل تريد المتابعة , سيتم إرسال رمز التحقق للهاتف الخاص بك ؟") == true)
                return true;
            else
                return false;
        }
    </script>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageMyAccount.aspx">حسابي</a></li>
                    <li><a href="PageAuthenticatorWithSMS.aspx">المصادقة الثنائية </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-envelope" style="font-size:30px;"></i> 
                        <asp:Label ID="lbmsg" runat="server" Text="تفعيل المصادقة الثنائية عبر SMS"></asp:Label>
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
                                    <h5><strong>ربط الحساب بالهاتف : </strong></h5>
                                    <i class="fa fa-envelope" style="font-size:150px;"></i>
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
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <br />
                                            <div class="col-md-12">
                                                <asp:LinkButton ID="LBSend" runat="server" CssClass="btn btn-info" OnClick="LBSend_Click"
                                                    OnClientClick="return ConfirmSend();">إرسال رمز التحقق <i class="fa fa-envelope"></i> </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="clearfix"></div>--%>
                                    <div class="col-lg-8">
                                        <div id="IDCode" class="form-group" runat="server" visible="false">
                                            <hr />
                                            <h5><strong>قم بكتباة الكود المرسل لهاتفك : </strong></h5>
                                            <asp:TextBox ID="txt_Code" runat="server" class="form-control" ValidationGroup="g2" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txt_Code" ErrorMessage="* الكود" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                        <div class="col-md-4">
                                        <div class="form-group">
                                            <h5><strong>رقم الهاتف : </strong></h5>
                                            <div class="col-md-12">
                                                <asp:Label ID="lblPhone2" runat="server"></asp:Label>
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
                                    
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <br />
                                            <div class="col-md-12">
                                                <asp:LinkButton ID="LBSendUnActive" runat="server" CssClass="btn btn-warning" OnClick="LBSendUnActive_Click"
                                                    OnClientClick="return ConfirmDelete();">إرسال رمز التحقق للإلغاء <i class="fa fa-envelope-o"></i> </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div id="IDCodeUnActive" class="form-group" runat="server" visible="false">
                                            <hr />
                                            <h5><strong>قم بكتباة الكود المرسل لهاتفك : </strong></h5>
                                            <asp:TextBox ID="txt_CodeUnActive" runat="server" class="form-control" ValidationGroup="g2UnActive" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator30" runat="server"
                                                ControlToValidate="txt_CodeUnActive" ErrorMessage="* الكود" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2UnActive" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                <asp:Button ID="LBUnActive" runat="server" CssClass="btn btn-warning btn-fill pull-left" ValidationGroup="g2UnActive"
                     Style="margin-right: 4px; font-size: medium" Visible="false" OnClick="LBUnActive_Click" Text="إلغاء تفعيل المصادقة الثنائية" />
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

