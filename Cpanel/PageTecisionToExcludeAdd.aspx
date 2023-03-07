<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageTecisionToExcludeAdd.aspx.cs" Inherits="Cpanel_PageTecisionToExcludeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        function insertConfirmation() {
            var answer = confirm("هل أنت متأكد من البيانات ؟")
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

            .WidthText20 {
                Width: 150px;
                height: 36px;
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

            .WidthText4 {
                Width: 95%;
            }

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }

        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }
    </style>
    <link href="css/chosen.css" rel="stylesheet" />
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
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><li class="fa fa-refresh"></li></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageTecisionToExclude.aspx">قرارات الإستبعاد</a></li>
                    <li><a href="PageTecisionToExcludeAdd.aspx">إضافة قرار إستبعاد</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="بيانات المستفيد"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="container-fluid" dir="rtl">
                            <div class="WidthText2">
                                <div class="form-group">
                                    <h5>رقم المستفيد :
                                    </h5>
                                    <asp:TextBox ID="txtNumberMostafeed" runat="server" class="form-control" ValidationGroup="g2" AutoPostBack="true" OnTextChanged="txtNumberMostafeed_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtNumberMostafeed" ErrorMessage="* رقم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberMostafeed"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="WidthText2">
                                <div class="form-group">
                                    <h5>أو إسم المستفيد :
                                    </h5>
                                    <asp:DropDownList ID="DLName" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" AutoPostBack="true" OnSelectedIndexChanged="DLName_SelectedIndexChanged">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="WidthText2">
                                <div class="form-group">
                                    <h5>رقم القرار :
                                    </h5>
                                    <asp:TextBox ID="txtNumberQarar" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtNumberQarar" ErrorMessage="* رقم القرار" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberQarar"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlData" runat="server" Visible="false">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText4">
                                    <div class="form-group">
                                        الاسم :
                                    
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        القرية :
                                    
                                    <asp:Label ID="lblAlQariah" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        الجنس :
                                    
                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>رقم الهاتف :
                                        </h5>
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>حالة المستفيد :
                                        </h5>
                                        <asp:Label ID="lblHalatAlmostafeed" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>السجل المدني :
                                        </h5>
                                        <asp:Label ID="lblNumberSigal" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>تاريخ الميلاد :
                                        </h5>
                                        <asp:Label ID="lblDateBrithDay" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>العمر :
                                        </h5>
                                        <asp:Label ID="lblAge" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlGetData" runat="server" Visible="false">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="float:right">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label4" runat="server" Text="إضافة قرار إستبعاد"></asp:Label>
                        </h3>
                    </div>
                    <div align="left">
                        <asp:CheckBox ID="CBAllow" runat="server" Font-Size="14px" CssClass="checkbox-inline" Checked="true" />
                        <span>الموافقة بدل مجلس الإدارة</span>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText4">
                                        <div class="container-fluid" dir="rtl">
                                            <div class="WidthText2">
                                                <div class="form-group">
                                                    <h5>تاريخ القرار : <span style="color: red">*</span>
                                                    </h5>
                                                    <div class="col-sm-3">
                                                        <div class="input-group date " style="margin-right: -10px">
                                                            <asp:TextBox ID="txtDateQarar" runat="server" class="form-control" Width="200" data-date-format="DD-MM-YYYY" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button class="btn btn-default" type="button">
                                                                    <i class="fa fa-calendar"></i>
                                                                </button>
                                                            </span>
                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                                                ControlToValidate="txtDateQarar" ErrorMessage="* تاريخ القرار" ForeColor="#FF0066"
                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="container-fluid" dir="rtl">
                                            <div class="WidthText2">
                                                <div class="form-group">
                                                    <h5>رقم الطلب : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:TextBox ID="txtNumberReport" runat="server" class="form-control" Width="250" ValidationGroup="g2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtNumberReport" ErrorMessage="* رقم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumberReport"
                                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                        Display="Dynamic">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="container-fluid" dir="rtl">
                                            <div class="WidthText2">
                                                <div class="form-group">
                                                    <h5>تاريخ الطلب : <span style="color: red">*</span>
                                                    </h5>
                                                    <div class="col-sm-3">
                                                        <div class="input-group date " style="margin-right: -10px">
                                                            <asp:TextBox ID="txtDateReport" runat="server" class="form-control" Width="200" data-date-format="DD-MM-YYYY" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <button class="btn btn-default" type="button">
                                                                    <i class="fa fa-calendar"></i>
                                                                </button>
                                                            </span>
                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtDateReport" ErrorMessage="* تاريخ التقرير" ForeColor="#FF0066"
                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText4" align="center">
                                        <div class="form-group">
                                            <strong>أعضاء مجلس الإدارة
                                            </strong>
                                            <br />
                                            <br />
                                            <table style="width: 100%">
                                                <thead>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <strong>م
                                                            </strong>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <strong>الإسم
                                                            </strong>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <strong>الصفة
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <asp:Label ID="lblOrderOne" runat="server" Text="1"></asp:Label>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:Label ID="lblCommentOne" runat="server" Text="رئيس مجلس الإدارة"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <asp:Label ID="lblOrderTow" runat="server" Text="2"></asp:Label>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:DropDownList ID="DLNaeebMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:Label ID="lblCommentTow" runat="server" Text="نائب رئيس مجلس الإدارة"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <asp:Label ID="lblOrderThree" runat="server" Text="3"></asp:Label>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:DropDownList ID="DLAmeenAlsondoq" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:Label ID="lblCommentThree" runat="server" Text="المشرف المالي"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <asp:Label ID="lblOrderFour" runat="server" Text="4"></asp:Label>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:DropDownList ID="DLAmeenGeneral" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:Label ID="lblCommentFour" runat="server" Text="الأمين العام"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <asp:Label ID="lblOrderFive" runat="server" Text="5"></asp:Label>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:DropDownList ID="DLAdminInEdarahFive" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList><br />
                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                                ControlToValidate="DLAdminInEdarahFive" ErrorMessage="* يرجى التحديد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:Label ID="lblCommentFive" runat="server" Text="عضو مجلس الإدارة"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <asp:Label ID="lblOrderSix" runat="server" Text="6"></asp:Label>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:DropDownList ID="DLAdminInEdarahSix" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                                                ControlToValidate="DLAdminInEdarahSix" ErrorMessage="* يرجى التحديد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:Label ID="lblCommentSix" runat="server" Text="عضو مجلس الإدارة"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="StyleTD" style="width: 10%;">
                                                            <asp:Label ID="lblOrderSiven" runat="server" Text="7"></asp:Label>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:DropDownList ID="DLAdminInEdarahSiven" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                                                ControlToValidate="DLAdminInEdarahSiven" ErrorMessage="* يرجى التحديد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="StyleTD" style="width: 40%;">
                                                            <asp:Label ID="lblCommentSiven" runat="server" Text="عضو مجلس الإدارة"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
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

                    <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px; font-size: medium" OnClientClick="return insertConfirmation();"
                        class="btn btn-info btn-fill pull-left" ValidationGroup="g2" OnClick="btnAdd_Click" />
                    <asp:LinkButton ID="LinkButton1" runat="server" Style="margin-right: 4px; font-size: medium"
                        class="btn btn-danger btn-fill pull-left">خروج بدون حفظ</asp:LinkButton>
                </div>
                <div style="width: 50%">
                </div>
                <br />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlGetNull" runat="server" Visible="false">
            <div class="container-fluid" dir="rtl">
                <asp:Panel ID="Panel1" runat="server">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div align="center">
                        <h3 style="font-size: 20px">الرجاء تحديد بيانات صحيحة
                        </h3>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </asp:Panel>
            </div>
        </asp:Panel>
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

