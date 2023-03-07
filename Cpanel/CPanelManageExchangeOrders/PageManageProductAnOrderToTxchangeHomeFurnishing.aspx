<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductAnOrderToTxchangeHomeFurnishing.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageProductAnOrderToTxchangeHomeFurnishing" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <style type="text/css">
        @media screen and (min-width: 768px) {
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
                padding-right: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
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
        }

        @media screen and (min-width: 768px) {
            .WidthText20 {
                Width: 250px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText20 {
                Width: 150px;
                height: 36px;
            }
        }
    </style>
    <link href="../css/chosen.css" rel="stylesheet" />
    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var headerCheckBox = inputList[0];
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVMatterOfExchangeByID.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي سجل");
                return false;
            }
            else {
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageManageProductAnOrderToTxchangeHomeFurnishing.aspx" class="btn btn-primary">
                        <i class="fa fa-plus"></i>
                    </a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default">
                     <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="">المنتجات</a></li>
                    <li><a href="PageManageProductAnOrderToTxchangeHomeFurnishing.aspx">إضافة أمر صرف تأثيث منزل</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="pnlMostafeed">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="بيانات المستفيد"></asp:Label>
                        <asp:Label ID="lblCountProduct" runat="server"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="col-sm-12">
                            <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-warning">تحذير</span>
                                <asp:Label ID="lblMessageWarning" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                                <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                        <div class="container-fluid" dir="rtl">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <h5>حدد الداعم : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown"
                                         Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                        ControlToValidate="DLCompany" ErrorMessage="* حدد الداعم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <h5>رقم المستفيد : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtNumberMostafeed" runat="server" class="form-control" ValidationGroup="g2" AutoPostBack="true" OnTextChanged="txtNumberMostafeed_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtNumberMostafeed" ErrorMessage="* رقم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumberMostafeed"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <h5>أو إسم المستفيد : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLName" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" AutoPostBack="true" OnSelectedIndexChanged="DLName_SelectedIndexChanged">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <h5>المبادرة : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLInitiatives" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                        ControlToValidate="DLInitiatives" ErrorMessage="* المبادرة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <h5>ر/الفاتورة
                                    </h5>
                                    <asp:TextBox ID="txtNumberOrder" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox> 
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="txtNumberOrder" ErrorMessage="* رقم القرار" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumberOrder"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group">
                                    <br />
                                    <asp:LinkButton ID="LBGetBill" runat="server" OnClick="LBGetBill_Click" data-toggle="tooltip" title="جلب فاتورة سابقة للتعديل"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlDataMosTafeed" runat="server" Visible="false">
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
                                        0<asp:Label ID="lblPhone" runat="server"></asp:Label>
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
        <div class="container-fluid" runat="server" visible="false" id="pnlAlDaam">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label2" runat="server" Text="حدد بيانات الدعم"></asp:Label>
                    </h3>
                    <div runat="server" id="PnlOther" style="float:left;">
                        <span>
                            عدد الأسر : 
                            <asp:TextBox ID="txtCount_Families" runat="server" Width="100" TextMode="Number"></asp:TextBox>
                        </span>
                        <span>
                            عدد مرات الدعم : 
                            <asp:TextBox ID="txtCount_Cart" runat="server" Width="100" TextMode="Number"></asp:TextBox>
                        </span>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>حدد الصنف : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="g2" AutoPostBack="true" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%" OnSelectedIndexChanged="DLCategory_SelectedIndexChanged1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="DLCategory" ErrorMessage="* حدد الصنف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>حدد المنتج : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLProduct" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLProduct_SelectedIndexChanged1">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="DLProduct" ErrorMessage="* حدد المنتج" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>رقم الطلب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtIDNumberProduct" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtIDNumberProduct" ErrorMessage="* رقم الشحنة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtIDNumberProduct"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>الكمية (العدد) : <span style="color: red">*</span>
                                            <asp:Label ID="lblCheckCountProduct" runat="server" ForeColor="Red"></asp:Label>
                                        </h5>
                                        <asp:TextBox ID="txtCountProduct" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtCountProduct" ErrorMessage="* الكمية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCountProduct"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="WidthText" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5>سعر الحبة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtPriceOfTheGrain" runat="server" class="form-control" Text="0" ValidationGroup="g2" AutoPostBack="true"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtPriceOfTheGrain" ErrorMessage="* سعر الحبة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPriceOfTheGrain"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]{1,2}([,.]{1}[0-9]{1,2})?$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5>السعر الكلي : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtTotalPrice" runat="server" class="form-control" ValidationGroup="g2" Text="0"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtTotalPrice" ErrorMessage="* السعر الكلي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5>تاريخ الطلب : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtProductionDate" runat="server" class="form-control" Width="100" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                                    ControlToValidate="txtProductionDate" ErrorMessage="* تاريخ الطلب" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>تاريخ الإضافة : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txt_Add" runat="server" class="form-control" Width="100" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                                    ControlToValidate="txt_Add" ErrorMessage="* تاريخ الإضافة" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>لمشروع : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLSupportType" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="DLSupportType" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 50%">
                                                    <h5>رئيس مجلس الإدارة : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                        Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                                        ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 50%" runat="server" id="IDNaeeb">
                                                    <h5>نائب رئيس المجلس : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:DropDownList ID="DLNaeebRaeesMagles" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                        Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                                        ControlToValidate="DLNaeebRaeesMagles" ErrorMessage="* نائب رئيس مجلس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%" runat="server" id="IDAmeenSondoq">
                                                    <h5>المشرف المالي : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                        Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                                        ControlToValidate="DLAmeenAlSondoq" ErrorMessage="* المشرف المالي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 50%" runat="server" id="IDModer">
                                                    <h5>مدير الجمعية : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                        Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                                        ControlToValidate="DLModerAlGmeiah" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td style="width: 50%">
                                                    <h5>أمين المستودع : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:DropDownList ID="DLIDStorekeeper" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                        Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                                        ControlToValidate="DLIDStorekeeper" ErrorMessage="* أمين المخزن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 50%" runat="server" id="IDAlBaheth">
                                                    <h5>الباحث الذي سيقوم بالتسليم : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                        Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                                        ControlToValidate="DLAlBaheth" ErrorMessage="* حدد الباحث" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>ملاحظة :
                                        </h5>
                                        <asp:TextBox ID="txtdescription" runat="server" class="form-control" ValidationGroup="g2"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:CheckBox ID="CBFinish" runat="server" Text=" تحديد على أنه سيتم الصرف " Font-Size="12px" 
                                            AutoPostBack="true" OnCheckedChanged="CBFinish_CheckedChanged" />
                                        <br />
                                        <asp:Button ID="btnAdd" runat="server" Text="إضافة للفاتورة" Style="margin-right: 4px; font-size: medium"
                                            class="btn btn-info btn-fill pull-left" ValidationGroup="g2" OnClick="btnAdd_Click" />
                                        <asp:LinkButton ID="LinkButton1" runat="server" Style="margin-right: 4px; font-size: medium" Visible="false"
                                            class="btn btn-danger btn-fill pull-left">خروج بدون حفظ</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="ProductByUser" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>قائمة فاتورة أمر صرف
                    </h3>
                    <div style="float: left">
                        <asp:LinkButton ID="LBR" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBR_Click"
                            title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                            title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                            OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlDataSarf" runat="server" Direction="RightToLeft">
                        <div class="">
                            <div align="center" class="w">
                                <div class="table table-responsive">
                                    <table style="width: 100%; background-color: #ffffff; color: #393939">
                                        <tr>
                                            <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                            </td>
                                            <td style="border: thin double #808080; border-width: 1px; width: 20%">
                                                <table style="width: 100%; font-size: 12px">
                                                    <tr>
                                                        <td align="left" style="width: 60%; font-family: 'Alwatan'; font-size: 18px;">رقم الفاتورة / 
                                                        </td>
                                                        <td style="width: 40%">
                                                            <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="border: thin double #808080; border-width: 1px; width: 35%">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                                        </td>
                                                        <td style="width: 80%">
                                                            <asp:Label ID="lblDateHide" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div style="float: right; padding: 10px 10px 0 10px;" class="w">
                            <p style="font-size: 13px">
                                السيد / أمين المستودع<asp:Label ID="lblAmeenAlmosTodaa" runat="server" Visible="false"></asp:Label>
                            </p>
                            <p style="font-size: 13px">
                                <asp:Label ID="lblSarf" runat="server" Text="بموجبه يتم الصرف للسيد / "></asp:Label>
                            </p>
                        </div>
                        <div style="float: left; padding: 10px 0 0 10px" class="w">
                            <table style="font-size: 12px">
                                <tr>
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                        <asp:Label ID="lblDataEntry" runat="server"></asp:Label>
                                    </td>
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntry" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" id="IDUpdate" visible="false">
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                        <asp:Label ID="lblDataEntryEdit" runat="server"></asp:Label>
                                    </td>
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryEdit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div align='center' class="w">
                            <asp:Image ID="IDBarcode" runat="server" alt='Loding' />
                        </div>
                        <table style="width: 100%">
                            <tr runat="server" id="IDUserDetails">
                                <td style="width: 40%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                                    <p style="font-size: 11px">
                                        الإسم :
                                        <asp:Label ID="lblNameEvint" runat="server" Font-Size="11px"></asp:Label>
                                        <asp:Label ID="lblCategory" runat="server" Visible="false"></asp:Label>
                                    </p>
                                </td>
                                <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                    <p style="font-size: 11px">
                                        الجوال :
                                            <asp:Label ID="lblPhone2" runat="server" Font-Size="11px"></asp:Label>
                                    </p>
                                </td>
                                <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                    <p style="font-size: 11px">
                                        القرية :
                                            <asp:Label ID="lblAlQariah2" runat="server" Font-Size="11px"></asp:Label>
                                    </p>
                                </td>
                                <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                    <p style="font-size: 11px">
                                        رقم الملف :
                                            <asp:Label ID="txtNumberMostafeed2" runat="server" Font-Size="11px"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </table>
                        <div style="font-family: 'Alwatan'; font-size: 18px; float: right">
                            الأصناف الموضحة أدناه : 
                        </div>
                        <div align="left" style="font-family: 'Alwatan'; font-size: 18px">
                            <asp:Label ID="lbl_Initiatives" runat="server"></asp:Label>
                        </div>
                        <span class="hr"></span>
                        <div class="table table-responsive">
                            <asp:GridView ID="GVMatterOfExchangeByID" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVMatterOfExchangeByID_RowDataBound"
                                UseAccessibleHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="10px">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="_IDItem" HeaderText="_IDItem" InsertVisible="False" ReadOnly="True"
                                        SortExpression="_IDItem" Visible="false" />
                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="رقم الطلب" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("_IDNumberProduct")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Font-Size="12px" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("ProductName")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# Eval("_CountProduct")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="السعر الفردي" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("_PriceOfTheGrain")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="المجموع" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("_TotalPrice")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="تاريخ الطلب" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                        <ItemTemplate>
                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                        <ItemTemplate>
                                            <%# ClassQuaem.FAlBaheth((Int32) (Eval("_IDAdmin")))%>
                                            <br />
                                            <%# ClassQuaem.FAlBahethByEdit((Int32) (Eval("_IDUpdate")))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                        <ItemTemplate>
                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px" Visible="false">
                                        <ItemTemplate>
                                            <a href='PageManageProductMatterOfExchange.aspx?ID=<%# Eval("_IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                                class="btn btn-primary"><span class="fa fa-eye"></span></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                    PreviousPageText=" السابق - " PageButtonCount="30" />
                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                <RowStyle CssClass="rows"></RowStyle>
                                <RowStyle CssClass="rows"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </div>
                        <div style="display: none">
                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                            <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            - <span style="font-size: 12px; padding-right: 5px">عدد الطلبات : </span>
                            <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                        </div>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                                </td>
                                <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:TextBox ID="lblSumSaraf" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                </td>
                                <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <div class="table table-responsive">
                            <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                                <table style="width: 100%; margin: 5px; font-size: 12px">
                                    <tr>
                                        <td style="width: 45%;">مدير الجمعية : 
                                        </td>
                                        <td style="width: 55%;">
                                            <asp:Image ID="ImgModer" runat="server" Width='100px' Height='30px' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                                <table style="width: 100%; margin: 5px; font-size: 12px">
                                    <tr>
                                        <td style="width: 45%;">المشرف المالي : 
                                        </td>
                                        <td style="width: 55%;">
                                            <asp:Image ID="ImgAmeenAlsondoq" runat="server" Width='100px' Height='30px' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                                <table style="width: 100%; margin: 5px; font-size: 12px">
                                    <tr>
                                        <td style="width: 45%;">رئيس الجمعية : 
                                        </td>
                                        <td style="width: 55%;">
                                            <asp:Image ID="ImgRaees" runat="server" Width='100px' Height='30px' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:Panel ID="IDTableManager" runat="server" Visible="false">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 50%">أمين المستودع 
                                        <br />
                                        <br />
                                    </td>
                                    <td align="center" style="width: 50%">رئيس مجلس الإدارة
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <asp:Panel ID="pnllblPrint" runat="server" Visible="false">
                                    <tr>
                                        <td align="center" style="width: 50%">
                                            <asp:Label ID="lblIDStorekeeper2" runat="server" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center" style="width: 50%">
                                            <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="12px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pnlDlPrint" runat="server">
                                    <tr>
                                        <td align="center" style="width: 50%">
                                            <asp:DropDownList ID="DLIDStorekeeper2" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" style="width: 50%">
                                            <asp:DropDownList ID="DLRaeesMaglesAlEdarah2" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </asp:Panel>
                        <hr />
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                                    <p style="font-size: 13px">
                                        <div align="center" style="font-size: 13px">
                                            تم الصرف
                                            <asp:CheckBox ID="CBDone" runat="server" Enabled="false" />
                                            / لم يتم الصرف بعد
                                            <asp:CheckBox ID="CBNotDone" runat="server" Enabled="false" />
                                        </div>
                                    </p>
                                    <p style="font-size: 13px; padding-right: 5px">
                                        أمين المستودع / 
                                            <asp:Label ID="lblAmeenAlmosTodaa2" runat="server"></asp:Label>
                                        <asp:Image ID="ImgAmeenAlmosTodaa" runat="server" Width='100px' Height='30px' />
                                    </p>
                                    <p style="font-size: 13px; padding-right: 5px">
                                        بتاريخ / 
                                            <asp:Label ID="lblDateGo" runat="server"></asp:Label>
                                    </p>
                                </td>
                                <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                                    <p style="font-size: 13px">
                                        <div align="center" style="font-size: 13px">
                                            تم التسليم
                                            <asp:CheckBox ID="CBReceived" runat="server" Enabled="false" />
                                            / لم يتم التسليم بعد
                                            <asp:CheckBox ID="CBNotReceived" runat="server" Enabled="false" />
                                            <span runat="server" id="IDNotReceived" visible="false">/ السبب :
                                                <asp:Label ID="lblA2" runat="server"></asp:Label>
                                            </span>
                                        </div>
                                    </p>
                                    <p style="font-size: 13px; padding-right: 5px">
                                        إسم الباحث / 
                                            <asp:Label ID="lblNameEvint2" runat="server"></asp:Label>
                                        <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='30px' />
                                    </p>
                                    <p style="font-size: 13px; padding-right: 5px">
                                        تاريخ التسليم / 
                                            <asp:Label ID="lblDateRecived" runat="server"></asp:Label>
                                    </p>
                                </td>
                            </tr>
                        </table>
                        <div align="left" style="margin-top: -60px" runat="server" id="IDKhatm" visible="false">
                            <img src="../ImgSystem/ImgSignature/الختم.png" />
                        </div>
                        <div id="DivNote" runat="server" visible="false">
                            <hr />
                            <span><strong>* ملاحظة : </strong></span><br />
                            <span>
                                - <asp:Label ID="lblNote" runat="server"></asp:Label>
                            </span>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlNullSarf" runat="server" Visible="False">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div align="center">
                            <h3 style="font-size: 20px">لا توجد نتائج
                            </h3>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="container-fluid" id="pnlStar" runat="server" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label3" runat="server" Text="يرجى تحديد رقم المستفيد"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="container-fluid" dir="rtl">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <div align="center">
                                    <h3 style="font-size: 20px">إدخل رقم المستفيد
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
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
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
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

