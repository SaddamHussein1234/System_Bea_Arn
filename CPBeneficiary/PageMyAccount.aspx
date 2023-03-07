<%@ Page Title="" Language="C#" MasterPageFile="~/CPBeneficiary/MPBeneficiary.master" AutoEventWireup="true" CodeFile="PageMyAccount.aspx.cs" Inherits="CPBeneficiary_PageMyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <style>
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
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="test/LoginAr.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
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
                                <div class="col-lg-5">
                                    <div class="form-group">
                                        <h5>رقم الملف : 
                                        </h5>
                                        <asp:Label ID="lblNumberMostafeed" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class="form-group">
                                        <h5>رقم السجل المدني :
                                        </h5>
                                        <asp:Label ID="lblSegelAlMadany" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <h5>إسم الدخول للنظام : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtUserName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator37" runat="server"
                                            ControlToValidate="txtUserName" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <h5>البريد الالكتروني : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator34" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ControlToValidate="txtEmail"
                                            ErrorMessage="بريد خطأ"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ForeColor="Red" ValidationGroup="g2" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <%--<div class="col-lg-5">
                                    <div class="form-group">
                                        <h5>كلمة المرور : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtPassword" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator35" runat="server"
                                            ControlToValidate="txtPassword" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ControlToValidate="txtPassword" ID="RegularExpressionValidator11" ValidationExpression="^[\s\S]{5,}$" runat="server"
                                            ErrorMessage="الحد الادنى 5 رموز" Display="Dynamic" ValidationGroup="g2"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class="form-group">
                                        <h5>تأكيد كلمة المرور : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtRePassword" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator36" runat="server"
                                            ControlToValidate="txtRePassword" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ControlToValidate="txtRePassword" ID="RegularExpressionValidator12" ValidationExpression="^[\s\S]{5,}$" runat="server"
                                            ErrorMessage="الحد الادنى 5 رموز" Display="Dynamic" ValidationGroup="g2"></asp:RegularExpressionValidator>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div style="float: left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px;" OnClick="btnAdd_Click"
                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="margin-right: 4px;" OnClick="LBBack_Click"
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

