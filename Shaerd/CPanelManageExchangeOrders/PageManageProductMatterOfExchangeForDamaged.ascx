<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageManageProductMatterOfExchangeForDamaged.ascx.cs" Inherits="Shaerd_CPanelManageExchangeOrders_PageManageProductMatterOfExchangeForDamaged" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>

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
    function ConfirmDeleteTaleef() {
        var count = document.getElementById("<%=hfCountTaleef.ClientID %>").value;
            var gv = document.getElementById("<%=GVMatterOfExchangeByIDTaleef.ClientID%>");
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
<link href="../GridView.css" rel="stylesheet" type="text/css" />


<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href="" class="btn btn-primary">
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
            <li><a href="">إضافة أمر صرف تالف</a></li>
        </ul>
    </div>
</div>
<div class="container-fluid" runat="server" id="pnlTTaleef">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="إنشاء أمر صرف تالف"></asp:Label>
                <asp:Label ID="lblCountProduct" runat="server"></asp:Label>
            </h3>
            <div style="float: left">
                رقم الفاتورة
                        <asp:TextBox ID="txtNumberOrder" runat="server" Width="50px"></asp:TextBox>
                <asp:LinkButton ID="LBGetBill" runat="server" OnClick="LBGetBill_Click" data-toggle="tooltip" title="جلب فاتورة سابقة للتعديل"> <i class="fa fa-refresh"></i> </asp:LinkButton>
            </div>
        </div>
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
        <div class="panel-body">
            <div class="content-box-large">
                <div class="form-group" align="center">
                    <h2>إنشاء أمر صرف تالف
                    </h2>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="container-fluid" runat="server" id="pnlAlDaam">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="Label2" runat="server" Text="حدد بيانات التالف"></asp:Label>
            </h3>
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
                                    Width="100%" OnSelectedIndexChanged="DLCategory_SelectedIndexChanged">
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
                                <asp:DropDownList ID="DLProduct" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                    Width="100%" OnSelectedIndexChanged="DLProduct_SelectedIndexChanged">
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
                                <asp:TextBox ID="txtPriceOfTheGrain" runat="server" class="form-control" Text="0" ValidationGroup="g2"
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
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
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
                        <div class="col-md-5">
                            <div class="form-group">
                                <h5>مزيد من التفاصيل :
                                </h5>
                                <asp:TextBox ID="txtdescription" runat="server" class="form-control" ValidationGroup="g2" Text="لا يوجد"
                                    TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <br />
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
<div class="container-fluid" runat="server" id="ProductByTalef" visible="false">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-list"></i>قائمة فاتورة أمر صرف تالف
            </h3>
            <div style="float: left">
                <asp:LinkButton ID="LBRefresh" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBRefresh_Click"
                    title="تحديث"><l class="fa fa-refresh"></l></asp:LinkButton>
                <asp:LinkButton ID="LbPrintTaleef" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LbPrintTaleef_Click"
                    title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDeleteTaleef" runat="server" class="btn btn-danger" OnClick="btnDeleteTaleef_Click"
                    OnClientClick="return ConfirmDeleteTaleef();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            </div>
        </div>
        <div class="panel-body">
            <asp:Panel ID="pnlDataTalef" runat="server" Direction="RightToLeft">
                <div class="">
                    <div align="center" class="w">
                        <table style="width: 100%; background-color: #ffffff; color: #393939">
                            <tr>
                                <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                    <asp:TextBox ID="txtTitleTalef" runat="server" class="form-control" Text="عقد حصر وإتلاف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                </td>
                                <td style="border: thin double #808080; border-width: 1px; width: 20%">
                                    <table style="width: 100%; font-size: 12px">
                                        <tr>
                                            <td align="left" style="width: 30%">رقم الأمر /  
                                            </td>
                                            <td style="width: 70%">
                                                <asp:Label ID="lblNumberTaleef" runat="server"></asp:Label>
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
                                                <asp:Label ID="lblDateHideTaleef" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="float: right; padding: 10px 10px 0 10px;" class="w">
                    <p style="font-size: 13px">
                        أنه في يوم
                                <asp:Label ID="lblToday" runat="server"></asp:Label>
                        <i class="fa fa-minus" style="color: #fff"></i>
                        بتاريخ
                                <asp:Label ID="lblDateToDay" runat="server"></asp:Label>
                    </p>
                </div>
                <div style="float: left; padding: 10px 0 0 10px" class="w">
                    <table style="font-size: 12px">
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                        <asp:Label ID="lblDataEntryTaleef" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryTaleef" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="IDUpdateTaleef" visible="false">
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                <asp:Label ID="lblDataEntryEditTaleef" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryEditTaleef" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align='center' class="w">
                    <asp:Image ID="IDBarcodeTalef" runat="server" alt='Loding' />
                </div>
                <div align="right">
                    <p style="font-size: 13px">
                        تم إجتماع اللجنة لعقد الحصر والإتلاف في مقر الجمعية 
                                <br />
                        وتم حصر المواد التي تحتاج إلى الإتلاف حسب اللائحه على إتلاف المواد التاليه
                    </p>
                </div>
                <span class="hr"></span>
                <asp:GridView ID="GVMatterOfExchangeByIDTaleef" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVMatterOfExchangeByIDTaleef_RowDataBound"
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
                                <%# Eval("CategoryName")%>
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
                                <asp:Label ID="lblCountTaleef" runat="server" Font-Size="12px" Text='<%# Eval("_CountProduct")%>'></asp:Label>
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
                                <asp:Label ID="lblCountTotalPriceTaleef" runat="server" Font-Size="12px" Text='<%# Eval("_TotalPrice")%>'></asp:Label>
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
                <asp:HiddenField ID="hfCountTaleef" runat="server" Value="0" />
                <div style="display: none">
                    <span style="font-size: 12px; padding-right: 5px">عدد السجلات : </span>
                    <asp:Label ID="lblCountTaleef" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                    - <span style="font-size: 12px; padding-right: 5px">عدد التالف : </span>
                    <asp:Label ID="lblSumTaleef" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                </div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                        </td>
                        <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:TextBox ID="lblSumTalef" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:Label ID="lblTotalPriceTaleef" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
                <div align="center">
                    <table style="width: 80%">
                        <tr>
                            <td class="StyleTD" style="width: 10%;">
                                <strong>م
                                </strong>
                            </td>
                            <td class="StyleTD" style="width: 30%;">
                                <strong>الإسم
                                </strong>
                            </td>
                            <td class="StyleTD" style="width: 30%;">
                                <strong>الصفة
                                </strong>
                            </td>
                            <td class="StyleTD" style="width: 30%;">
                                <strong>التوقيع
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD">
                                <strong>1
                                </strong>
                            </td>
                            <td class="StyleTD">
                                <asp:Label ID="lblRaees" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="StyleTD">رئيس مجلس الإدارة
                            </td>
                            <td class="StyleTD">
                                <asp:Image Width='100' Height='30' ID="IDRaees" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD">
                                <strong>2
                                </strong>
                            </td>
                            <td class="StyleTD" style="width: 30%;">
                                <asp:Label ID="lblNaeeb" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="StyleTD" style="width: 30%;">نائب رئيس مجلس الإدارة
                            </td>
                            <td class="StyleTD" style="width: 30%;">
                                <asp:Image Width='100' Height='30' ID="IDNeeb" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" style="width: 10%;">
                                <strong>3
                                </strong>
                            </td>
                            <td class="StyleTD" style="width: 30%;">
                                <asp:Label ID="lblAmeen" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td class="StyleTD" style="width: 30%;">أمين المستودع
                            </td>
                            <td class="StyleTD" style="width: 30%;">
                                <asp:Image Width='100' Height='30' ID="IDAmeen" runat="server" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
                <hr />
                <div align="left" style="margin-top: -60px" runat="server" id="IDKhatmTaleef" visible="false">
                    <img src="/ImgSystem/ImgSignature/الختم.png" width="120" alt="" />
                </div>

            </asp:Panel>
            <asp:Panel ID="pnlNullTalef" runat="server" Visible="False">
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

