<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryOrphansIDBank.aspx.cs" Inherits="Cpanel_PageBeneficiaryOrphansIDBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBeneficiaryBySearch.aspx">إدارة المستفيدين</a></li>
                    <li><a href="PageAddBeneficiary.aspx">إضافة إستمارة مستفيد للنظام</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="تعديل رقم الحساب"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="content-box-large">
                                    <div class="widget-box">
                                        <div class="col-md-12">
                                            <div class="alert alert-info" runat="server" visible="false" id="IDMessage">
                                                <asp:Label ID="lbMessage" runat="server" Text="تم تعديل البيانات بنجاح "></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <h5>إسم المستفيد :
                                                </h5>
                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5>نوع المستفيد :
                                                </h5>
                                                <asp:Label ID="lblTypeMostafeed" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5>القرية :
                                                </h5>
                                                <asp:Label ID="lblAlQariah" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5>الجنس :
                                                </h5>
                                                <asp:Label ID="lblGender" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <h5>رقم الهاتف :
                                                </h5>
                                                0<asp:Label ID="lblPhone" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5>السجل المدني :
                                                </h5>
                                                <asp:Label ID="lblNumberSigal" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5>إسم اليتيم :
                                                </h5>
                                                <asp:Label ID="lblNameSun" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <h5>رقم الحساب البنكي : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtIDBank" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction:ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                            ControlToValidate="txtIDBank" ErrorMessage="* رقم الحساب البنكي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtIDBank"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic" Font-Size="10px">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <br />
            <div style="float: left">
                <asp:LinkButton ID="btnAdd" runat="server" ValidationGroup="g2" Style="margin-right: 4px; font-size: medium"
                    class="btn btn-info btn-fill pull-left" OnClick="btnAdd_Click">حفظ البيانات</asp:LinkButton>
                <asp:LinkButton ID="LBBack" runat="server" Style="margin-right: 4px; font-size: medium"
                    class="btn btn-danger btn-fill pull-left" OnClick="LBBack_Click">رجوع لشاشة الأيتام</asp:LinkButton>
            </div>
            <div style="width: 50%">
            </div>
            <br />
            <br />
        </div>

        <script type="text/javascript">
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
        </script>
        <script type="text/javascript"><!--
    $('#language a:first').tab('show');
    $('#option a:first').tab('show');
    //--></script>
</asp:Content>

