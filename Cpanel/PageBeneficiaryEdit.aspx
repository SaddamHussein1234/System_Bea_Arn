<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryEdit.aspx.cs" Inherits="Cpanel_PageBeneficiaryEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
            document.getElementById("<%=LBBenaaHome.ClientID %>").disabled = true;
    }
    window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        function insertConfirmation() {
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
        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }
            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText30 {
                float: right;
                Width: 19%;
                padding-right: 5px;
                background-color:#cecaca;
                border-radius:5px;
                margin:1px;
            }

            .WidthText2 {
                float: right;
                Width: 32%;
                padding-left: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }
            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText2 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText30 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }
        }
    </style>
    <link href="css/chosen.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default" OnClick="LBExit_Click"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBeneficiaryBySearch.aspx">إدارة المستفيدين</a></li>
                    <li><a href="">تعديل إستمارة مستفيد </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="تعديل إستمارة مستفيد للنظام - البيانات الاساسية للنظام"></asp:Label> <asp:Label ID="lblIDImg" runat="server" Visible="false"></asp:Label>
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
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>رقم المستفيد : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNumberMostafeed" runat="server" class="form-control" ValidationGroup="g2" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtNumberMostafeed" ErrorMessage="* رقم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberMostafeed"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>نوع المستفيد : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLTypeMostafeed" runat="server" ValidationGroup="g2" CssClass="dropdown" Enabled="false"
                                            Height="34" Width="95%">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="دائم">دائم</asp:ListItem>
                                            <asp:ListItem Value="مستبعد">مستبعد</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="DLTypeMostafeed" ErrorMessage="* نوع المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>تاريخ التسجيل : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                        <div class="input-group date " style="margin-right:-10px">
                                            <asp:TextBox ID="txtDateRegistry" runat="server" class="form-control" Width="150" data-date-format="DD-MM-YYYY" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                        ControlToValidate="txtDateRegistry" ErrorMessage="* تاريخ التسجيل" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>إسم المستفيد : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNameMostafeed" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtNameMostafeed" ErrorMessage="* إسم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>القرية : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAlQriah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="DLAlQriah" ErrorMessage="* القرية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>الجنس : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLGender" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="DLGender" ErrorMessage="* الجنس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>رقم الجوال 1 : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtCellPhoneOne" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtCellPhoneOne" ErrorMessage="* رقم الجوال 1" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCellPhoneOne"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>رقم الجوال 2 : 
                                        </h5>
                                        <asp:TextBox ID="txtCellPhoneTow" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                            ControlToValidate="txtCellPhoneOne" ErrorMessage="-" ForeColor="White" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCellPhoneTow"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>هاتف ثابت 1 : 
                                        </h5>
                                        <asp:TextBox ID="txtPhoneOne" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="txtCellPhoneOne" ErrorMessage="-" ForeColor="White" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPhoneOne"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>هاتف ثابت 2 : 
                                        </h5>
                                        <asp:TextBox ID="txtPhoneTow" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="txtCellPhoneOne" ErrorMessage="-" ForeColor="White" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPhoneTow"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>رقم السجل المدني : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNumberAlSegelAlMadany" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                            ControlToValidate="txtNumberAlSegelAlMadany" ErrorMessage="* رقم السجل المدني" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtNumberAlSegelAlMadany"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>حالة المستفيد : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLHalafAlMosTafeed" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                            ControlToValidate="DLHalafAlMosTafeed" ErrorMessage="* حالة المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>رقم القريب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNumberQareb" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtNumberQareb" ErrorMessage="* رقم القريب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtNumberQareb"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>إسم القريب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNameQareb" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtNameQareb" ErrorMessage="* إسم القريب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>صلة القرابة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLSelatAlQarabah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="DLSelatAlQarabah" ErrorMessage="* صلة القرابة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>ملاحظه :
                                        </h5>
                                        <asp:TextBox ID="txtNote" runat="server" class="form-control" ValidationGroup="g2" Text="---"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>حدد صلاحيات المجموعة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLGroup" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator24" runat="server"
                                            ControlToValidate="DLGroup" ErrorMessage="* حدد المجموعة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlUserAccount" runat="server" Visible="false">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>إسم الدخول للنظام : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtUserName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator37" runat="server"
                                            ControlToValidate="txtUserName" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
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
                                <div class="WidthText1">
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
                                <div class="WidthText1">
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
                                </div>
                                </asp:Panel>
                            </div>
                                <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>تاريخ الميلاد (هجري) : <span style="color: red">*</span>
                                        </h5>
                                        <table>
                                            <tr>
                                                <td style="padding-left: 3px">
                                                    <asp:DropDownList ID="ddlYearsH" runat="server" Width="55">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding-left: 3px">
                                                    <asp:DropDownList ID="ddlMonthsH" runat="server" Width="40">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Text="01" Value="01" />
                                                        <asp:ListItem Text="02" Value="02" />
                                                        <asp:ListItem Text="03" Value="03" />
                                                        <asp:ListItem Text="04" Value="04" />
                                                        <asp:ListItem Text="05" Value="05" />
                                                        <asp:ListItem Text="06" Value="06" />
                                                        <asp:ListItem Text="07" Value="07" />
                                                        <asp:ListItem Text="08" Value="08" />
                                                        <asp:ListItem Text="09" Value="09" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="11" Value="11" />
                                                        <asp:ListItem Text="12" Value="12" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDatesH" runat="server" Width="40" OnSelectedIndexChanged="ddlDatesH_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Text="01" Value="01" />
                                                        <asp:ListItem Text="02" Value="02" />
                                                        <asp:ListItem Text="03" Value="03" />
                                                        <asp:ListItem Text="04" Value="04" />
                                                        <asp:ListItem Text="05" Value="05" />
                                                        <asp:ListItem Text="06" Value="06" />
                                                        <asp:ListItem Text="07" Value="07" />
                                                        <asp:ListItem Text="08" Value="08" />
                                                        <asp:ListItem Text="09" Value="09" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="11" Value="11" />
                                                        <asp:ListItem Text="12" Value="12" />
                                                        <asp:ListItem Text="13" Value="13" />
                                                        <asp:ListItem Text="14" Value="14" />
                                                        <asp:ListItem Text="15" Value="15" />
                                                        <asp:ListItem Text="16" Value="16" />
                                                        <asp:ListItem Text="17" Value="17" />
                                                        <asp:ListItem Text="18" Value="18" />
                                                        <asp:ListItem Text="19" Value="19" />
                                                        <asp:ListItem Text="20" Value="20" />
                                                        <asp:ListItem Text="21" Value="21" />
                                                        <asp:ListItem Text="22" Value="22" />
                                                        <asp:ListItem Text="23" Value="23" />
                                                        <asp:ListItem Text="24" Value="24" />
                                                        <asp:ListItem Text="25" Value="25" />
                                                        <asp:ListItem Text="26" Value="26" />
                                                        <asp:ListItem Text="27" Value="27" />
                                                        <asp:ListItem Text="28" Value="28" />
                                                        <asp:ListItem Text="29" Value="29" />
                                                        <asp:ListItem Text="30" Value="30" />
                                                        <asp:ListItem Text="31" Value="31" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>تاريخ الميلاد (ميلادي) : <span style="color: red">*</span>
                                        </h5>
                                        <table>
                                            <tr>
                                                <td style="padding-left: 3px">
                                                    <asp:DropDownList ID="ddlYears" runat="server" Width="55">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding-left: 3px">
                                                    <asp:DropDownList ID="ddlMonths" runat="server" Width="40">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Text="01" Value="01" />
                                                        <asp:ListItem Text="02" Value="02" />
                                                        <asp:ListItem Text="03" Value="03" />
                                                        <asp:ListItem Text="04" Value="04" />
                                                        <asp:ListItem Text="05" Value="05" />
                                                        <asp:ListItem Text="06" Value="06" />
                                                        <asp:ListItem Text="07" Value="07" />
                                                        <asp:ListItem Text="08" Value="08" />
                                                        <asp:ListItem Text="09" Value="09" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="11" Value="11" />
                                                        <asp:ListItem Text="12" Value="12" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDates" runat="server" Width="40" OnSelectedIndexChanged="ddlDates_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Text="01" Value="01" />
                                                        <asp:ListItem Text="02" Value="02" />
                                                        <asp:ListItem Text="03" Value="03" />
                                                        <asp:ListItem Text="04" Value="04" />
                                                        <asp:ListItem Text="05" Value="05" />
                                                        <asp:ListItem Text="06" Value="06" />
                                                        <asp:ListItem Text="07" Value="07" />
                                                        <asp:ListItem Text="08" Value="08" />
                                                        <asp:ListItem Text="09" Value="09" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="11" Value="11" />
                                                        <asp:ListItem Text="12" Value="12" />
                                                        <asp:ListItem Text="13" Value="13" />
                                                        <asp:ListItem Text="14" Value="14" />
                                                        <asp:ListItem Text="15" Value="15" />
                                                        <asp:ListItem Text="16" Value="16" />
                                                        <asp:ListItem Text="17" Value="17" />
                                                        <asp:ListItem Text="18" Value="18" />
                                                        <asp:ListItem Text="19" Value="19" />
                                                        <asp:ListItem Text="20" Value="20" />
                                                        <asp:ListItem Text="21" Value="21" />
                                                        <asp:ListItem Text="22" Value="22" />
                                                        <asp:ListItem Text="23" Value="23" />
                                                        <asp:ListItem Text="24" Value="24" />
                                                        <asp:ListItem Text="25" Value="25" />
                                                        <asp:ListItem Text="26" Value="26" />
                                                        <asp:ListItem Text="27" Value="27" />
                                                        <asp:ListItem Text="28" Value="28" />
                                                        <asp:ListItem Text="29" Value="29" />
                                                        <asp:ListItem Text="30" Value="30" />
                                                        <asp:ListItem Text="31" Value="31" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="col-sm-3" runat="server" visible="false">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtdateBrith" runat="server" class="form-control" Width="150" data-date-format="DD-MM-YYYY" ValidationGroup="g2" Style="direction: ltr" AutoPostBack="True" OnTextChanged="txtdateBrith_TextChanged"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator33" runat="server"
                                                    ControlToValidate="txtdateBrith" ErrorMessage="* تاريخ الميلاد" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>العمر : <span style="color: red">*</span> <a href="http://dirarab.net/dateconversion" target="_blank">الذهاب لموقع التحويل</a>
                                        </h5>
                                        <asp:TextBox ID="txtAge" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                            ControlToValidate="txtAge" ErrorMessage="* العمر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAge"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="الحالة العلمية والتعليمية للمستفيد"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>المهنة الحالية للمستفيد : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtAlMehnahAlHaliyahllmostafeed" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtAlMehnahAlHaliyahllmostafeed" ErrorMessage="* المهنة الحالية للمستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>الحالة التعليمية للمستفيد : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAlHalahAlTaelimiahllmostafeed" runat="server" ValidationGroup="g2" CssClass="dropdown"
                                            Height="34" Width="95%">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="دكتوراة">دكتوراة</asp:ListItem>
                                            <asp:ListItem Value="ماجستير">ماجستير</asp:ListItem>
                                            <asp:ListItem Value="بكالوريوس">بكالوريوس</asp:ListItem>
                                            <asp:ListItem Value="جامعي">جامعي</asp:ListItem>
                                            <asp:ListItem Value="دبلوم">دبلوم</asp:ListItem>
                                            <asp:ListItem Value="ثانوي">ثانوي</asp:ListItem>
                                            <asp:ListItem Value="ابتدائي">ابتدائي</asp:ListItem>
                                            <asp:ListItem Value="متوسط">متوسط</asp:ListItem>
                                            <asp:ListItem Value="امي">امي</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="DLAlHalahAlTaelimiahllmostafeed" ErrorMessage="* الحالة التعليمية للمستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>مهنة الاب قبل الوفاة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtMehnahAlAAbKablAlWafah" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="DLAlHalahAlTaelimiahllmostafeed" ErrorMessage="* مهنة الاب قبل الوفاة" ForeColor="White" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>حالة تفعيل الحساب :
                                        </h5>
                                        <asp:CheckBox ID="CBActive" runat="server" Font-Size="14px" CssClass="checkbox-inline" ValidationGroup="g2" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label2" runat="server" Text="الحالة الصحية للمستفيد"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>سليم :
                                        </h5>
                                        <asp:CheckBox ID="CBSaleem" runat="server" Font-Size="14px" CssClass="checkbox-inline" ValidationGroup="g2" />
                                    </div>
                                </div>
                                <div class="WidthText">
                                <div class="form-group">
                                    <h5>
                                        معاق :
                                    </h5>
                                    <asp:CheckBox ID="CBMoalek" runat="server" Font-Size="14px" CssClass="checkbox-inline" ValidationGroup="g2" />
                                </div>
                            </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>نوع الإعاقة :
                                        </h5>
                                        <asp:TextBox ID="txtTypeAleakah" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="WidthText">
                                <div class="form-group">
                                    <h5>
                                        مريض :
                                    </h5>
                                    <asp:CheckBox ID="CBMareedh" runat="server" Font-Size="14px" CssClass="checkbox-inline" ValidationGroup="g2" />
                                </div>
                            </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>نوع المرض :
                                        </h5>
                                        <asp:TextBox ID="txtTypeAlmaradh" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
        <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label3" runat="server" Text="الحالة المادية والسكنية للمستفيد"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>الدخل الشهري : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtAlDakhlAlShahryllMostafeed" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                            ControlToValidate="txtAlDakhlAlShahryllMostafeed" ErrorMessage="* الدخل الشهري" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAlDakhlAlShahryllMostafeed"
                                            ErrorMessage="أرقام فقط" Font-Size="10px" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>مصدر الدخل : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLMasderAlDakhl" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator21" runat="server"
                                            ControlToValidate="DLMasderAlDakhl" ErrorMessage="* مصدر الدخل" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>نوع السكن : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLTypeAlMasken" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator22" runat="server"
                                            ControlToValidate="DLTypeAlMasken" ErrorMessage="* نوع السكن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>حالة المسكن : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLHaletAlMasken" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                            ControlToValidate="DLHaletAlMasken" ErrorMessage="* حالة المسكن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label7" runat="server" Text="بيانات الحساب"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>حدد البنك : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLBank" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="البنك الأهلي">البنك الأهلي</asp:ListItem>
                                            <asp:ListItem Value="بنك ساب">بنك ساب</asp:ListItem>
                                            <asp:ListItem Value="البنك السعودي للاستثمار">البنك السعودي للاستثمار</asp:ListItem>
                                            <asp:ListItem Value="البنك السعودي للاستثمار">البنك السعودي للاستثمار</asp:ListItem>
                                            <asp:ListItem Value="مصرف الإنماء">مصرف الإنماء</asp:ListItem>
                                            <asp:ListItem Value="البنك السعودي الفرنسي">البنك السعودي الفرنسي</asp:ListItem>
                                            <asp:ListItem Value="بنك الرياض">بنك الرياض</asp:ListItem>
                                            <asp:ListItem Value="مصرف الراجحي">مصرف الراجحي</asp:ListItem>
                                            <asp:ListItem Value="البنك العربي الوطني">البنك العربي الوطني</asp:ListItem>
                                            <asp:ListItem Value="بنك البلاد">بنك البلاد</asp:ListItem>
                                            <asp:ListItem Value="بنك الجزيرة">بنك الجزيرة</asp:ListItem>
                                            <asp:ListItem Value="بنك الخليج الدولي">بنك الخليج الدولي</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator26" runat="server"
                                            ControlToValidate="DLBank" ErrorMessage="* حدد البنك" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>رقم الحساب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtBank_Account" runat="server" class="form-control" ValidationGroup="g2" ></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator25" runat="server"
                                            ControlToValidate="txtBank_Account" ErrorMessage="* رقم الحساب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtBank_Account"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>رقم IBAN : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtIBAN_Account" runat="server" class="form-control" ValidationGroup="g2" ></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator27" runat="server"
                                            ControlToValidate="txtIBAN_Account" ErrorMessage="* رقم IBAN" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
        <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label5" runat="server" Text="الإدارة"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>الباحث : 
                                        </h5>
                                        <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>مدير الجمعية :
                                        </h5>
                                        <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>لحنة البحث الاجتماعي : 
                                        </h5>
                                        <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>رئيس مجلس الإدارة : 
                                        </h5>
                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label6" runat="server" Text="المرفقات"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>عنوان الصورة : 
                                        </h5>
                                        <asp:TextBox ID="txtTitleImg" runat="server" class="form-control" ValidationGroup="gImg"></asp:TextBox>
                                        <asp:Label ID="lblTitleImg" runat="server" Text="* عنوان الصورة" Font-Size="11px" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>المسموح :(bmp , png , jpg , jpeg)
                                        </h5>
                                        <asp:FileUpload ID="FBenaaHome" runat="server" ValidationGroup="gImg" />
                                        <asp:Label ID="lblBenaaHome" runat="server" Text="* حدد الصور" Font-Size="11px" ForeColor="Red" Visible="false"></asp:Label>
                                        <br />
                                        <asp:Button ID="LBBenaaHome" runat="server" Text="رفع الصور" data-toggle="tooltip" title="رفع الصور" CssClass="btn btn-info" ValidationGroup="gImg" OnClick="LBBenaaHome_Click" />
                                    </div>
                                </div>
                                <div class="WidthText4">
                                    <asp:Repeater ID="RPTImgMosTafeed" runat="server">
                                        <ItemTemplate>
                                            <div class="WidthText30">
                                                <span><%# Eval("TitleImg") %></span>
                                                <a href='<%# "../" + Eval("ImgMosTafeed") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                    <img src='<%# "../" + Eval("ImgMosTafeed") %>' style="margin: 4px; border-radius: 5px" width="90%" height="92" />
                                                </a>
                                                <div align="center">
                                                    <asp:LinkButton ID="LBDeleteBenaaHome" runat="server" OnClientClick="return insertConfirmation();" title="حذف" data-toggle="tooltip" OnClick="LBDeleteBenaaHome_Click" CommandArgument='<%# Eval("_IDItam") %>'>
                                                         <i class="fa fa-trash"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
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
                <asp:LinkButton ID="btnAdd" runat="server"  ValidationGroup="g2" style="margin-right:4px; font-size:medium"
                    class="btn btn-info btn-fill pull-left" OnClick="btnAdd_Click">حفظ البيانات</asp:LinkButton>
                <asp:LinkButton ID="LBAddBoys" runat="server"  ValidationGroup="g2" style="margin-right:4px; font-size:medium"
                    class="btn btn-info btn-fill pull-left" OnClick="LBAddBoys_Click">إضافة أفراد الاسرة</asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" style="margin-right:4px; font-size:medium"
                    class="btn btn-danger btn-fill pull-left">خروج بدون حفظ</asp:LinkButton>
            </div>
            <div style="width: 50%">
                
            </div>
            <br />
            <br />
        </div>
        <div class="container-fluid" runat="server" visible="false">
        <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label4" runat="server" Text="معلومات أفراد الإسرة"></asp:Label>
                        - أفراد الاسرة : <span style="color: red">*</span>
                        <asp:TextBox ID="TextBox15" runat="server" ValidationGroup="g2" Width="150" Text="0"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator28" runat="server"
                            ControlToValidate="TextBox15" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextBox15"
                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                    </h3>
                </div>
   
            </div>
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

    <script src="css/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

