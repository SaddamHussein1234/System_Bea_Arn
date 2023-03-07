<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelAttach/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBalance.aspx.cs" Inherits="Cpanel_CPanelAttach_PageBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBalance.aspx">شاشة الرصيد</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="شاشة الرصيد"></asp:Label>
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
                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h3 style="font-family: 'Alwatan';">
                                            <i class="fa fa-check"></i> حالة الإتصال :
                                            <br />
                                            <asp:Label ID="lblAuthentication" runat="server" Text="0"></asp:Label>
                                        </h3>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h3 style="font-family: 'Alwatan';">
                                            <i class="fa fa-dollar"></i> الرصيد :
                                            <br />
                                            <asp:Label ID="lblBlance" runat="server" Text="0"></asp:Label>
                                        </h3>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h3 style="font-family: 'Alwatan';">
                                            <i class="fa fa-user"></i> أسماء المستخدمين :
                                            <br />
                                            <asp:Label ID="lblUser" runat="server" Text="0"></asp:Label>
                                        </h3>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <br />
                                <div style="float: left">
                                    <asp:Button ID="btnAdd" runat="server" Text="حسناً" OnClick="btnAdd_Click" Width="100" style="margin-left:5px"
                                        class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

