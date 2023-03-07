<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageAdd.ascx.cs" Inherits="Shaerd_ERP_WSM_PageShipping_PageAdd" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.WSM" %>

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
        var gv = document.getElementById("<%=GVDeedDonationInKind.ClientID%>");
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

<script type="text/javascript">
    function ShowHideDiv(chkPassport) {
        var dvPassport = document.getElementById("IDCheck");
        dvPassport.style.display = chkPassport.checked ? "block" : "none";
    }
</script>

<style type="text/css">
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

    @media screen and (min-width: 768px) {
        .WidthMaglis {
            float: right;
            Width: 19%;
            padding-right: 5px;
        }
    }

    @media screen and (max-width: 767px) {
        .WidthMaglis {
            Width: 95%;
        }
    }
</style>

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href="PageAdd.aspx" class="btn btn-primary">
                <i class="fa fa-plus"></i>
            </a>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="LBRefresh2_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
            <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default">
                     <i class="fa fa-reply"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="PageAdd.aspx">إضافة إضافة شحنة للمستودع</a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="إضافة شحنة للمستودع"></asp:Label>
            </h3>
            <div style="float: left">
                رقم الفاتورة :
                            <asp:TextBox ID="txtNumberBill" runat="server" ValidationGroup="VGDetails" Width="100" Style="padding-right: 5px"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="txtNumberBill" ErrorMessage="* رقم الفاتورة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberBill"
                    Font-Size="11px" ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="VGDetails"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </div>
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
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>الارشيف : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="VGDetails" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                                    Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                    ControlToValidate="ddlYears" ErrorMessage="* الارشيف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <h5>حدد الداعم : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="GBill"
                                    Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                    ControlToValidate="DLCompany" ErrorMessage="* حدد الداعم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                <br />
                                <a  href='javaScript:void(0)' data-toggle="modal" data-target="#IDAddCompany" title="إضافة جديد" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>

                                <div id="IDAddCompany" class="modal fade in modal_New_Style">
                                    <div class="modal-dialog " style="max-width: 450px">
                                        <div class="modal-content">
                                            <div class="modal-header no-border">
                                                <button type="button" class="close" data-dismiss="modal">×</button>
                                            </div>
                                            <div class="modal-body" id="modal_ajax_content">
                                                <div class="page-container">
                                                    <div class="page-content">
                                                        <div class=" panel-body">
                                                            <label>
                                                                <i class="fa fa-star"></i>
                                                                <asp:Label ID="lblTitle" runat="server" Text="إضافة داعم جديد : "></asp:Label> 
                                                            </label>
                                                            <div align="">
                                                                <div class="" dir="rtl">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <h5>الفئة : <i style="color: red">*</i></h5>
                                                                            <asp:DropDownList ID="DLType_Customer" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGAdd">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                                <asp:ListItem Value="شركات">شركات</asp:ListItem>
                                                                                <asp:ListItem Value="أفراد">أفراد</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                                                ControlToValidate="DLType_Customer" ErrorMessage="*  حدد الفئة" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <h5>النوع : <i style="color: red">*</i></h5>
                                                                            <asp:DropDownList ID="ddlCompanyType" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGAdd">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator21" runat="server"
                                                                                ControlToValidate="ddlCompanyType" ErrorMessage="* حدد النوع" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="form-group">
                                                                            <h5 style="text-align:right"> الإسم : <i style="color: red">*</i></h5>
                                                                            <asp:TextBox ID="txtCompanyName" runat="server" class="form-control" ValidationGroup="VGAdd"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator19" runat="server"
                                                                                ControlToValidate="txtCompanyName" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="form-group">
                                                                            <h5>رقم الجوال (رسائل SMS) : <i style="color: red">*</i></h5>
                                                                            <asp:TextBox ID="txtPhone_Number1" runat="server" class="form-control" ValidationGroup="VGAdd" TextMode="Number"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                                                                ControlToValidate="txtPhone_Number1" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhone_Number1"
                                                                                ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="VGAdd" Font-Size="10px"
                                                                                Display="Dynamic">
                                                                            </asp:RegularExpressionValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12 hide">
                                                                        <div class="form-group">
                                                                            <h5>حدد البلد : <i style="color: red">*</i></h5>
                                                                            <asp:DropDownList ID="ddlCountry" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGAdd">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator22" runat="server"
                                                                                ControlToValidate="ddlCountry" ErrorMessage="* حدد البلد" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:LinkButton ID="LBSave" runat="server" Style="margin-right: 4px;"
                                                                    OnClientClick="return insertConfirmation();" OnClick="LBSave_Click" 
                                                                    class="btn btn-success" ValidationGroup="VGAdd">تحديث البيانات</asp:LinkButton>

                                                            <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <h5>حدد المبادرة : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLInitiatives" runat="server" ValidationGroup="GBill"
                                    CssClass="form-control2 chzn-select dropdown" Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="DLInitiatives" ErrorMessage="* حدد المبادرة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>تاريخ الفاتورة : <span style="color: red">*</span>
                                </h5>
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtDateAdd" runat="server" class="form-control"
                                        data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="txtDateAdd" ErrorMessage="* تاريخ الفاتورة" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 hide">
                            <div class="form-group">
                                <br />
                                <asp:LinkButton ID="LBGetBill" runat="server" data-toggle="tooltip" OnClick="LBGetBill_Click"
                                    title="جلب فاتورة سابقة للتعديل"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>حدد الصنف : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="VGDetails" AutoPostBack="true"
                                    CssClass="form-control2 chzn-select dropdown"
                                    Width="100%" OnSelectedIndexChanged="DLCategory_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="DLCategory" ErrorMessage="* حدد الصنف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>حدد المنتج : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLProduct" runat="server" ValidationGroup="VGDetails" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                    ControlToValidate="DLProduct" ErrorMessage="* حدد المنتج" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>هل يوجد تجزئة ؟ : </h5>
                                <div class="col-lg-4">
                                    <div class="keepmeLogged">
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBIs_There_Partition" runat="server" onclick="ShowHideDiv(this)" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <div id="IDCheck" class="col-lg-8" style="<%= FCheck() %>">
                                    <asp:TextBox ID="txtProduct_Weight" runat="server" class="form-control" 
                                        TextMode="Number" ValidationGroup="VGDetails" placeholder="العدد" Text="1" MaxLength="2"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtProduct_Weight"
                                        ErrorMessage="* أرقام فقط" Font-Size="10px" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$" ValidationGroup="VGDetails" Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>الكمية (العدد) : <span style="color: red">*</span>
                                            <asp:Label ID="lblCheckCountProduct" runat="server" ForeColor="Red"></asp:Label>
                                </h5>
                                <asp:TextBox ID="txtCountProduct" runat="server" class="form-control" ValidationGroup="VGDetails"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="txtCountProduct" ErrorMessage="* الكمية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCountProduct"
                                    ErrorMessage="* أرقام فقط" Font-Size="11px" ValidationExpression="^[0-9]+$" ValidationGroup="VGDetails"
                                    Display="Dynamic">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>السعر الفردي : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtPriceOfTheGrain" runat="server" class="form-control" ValidationGroup="VGDetails"
                                    AutoPostBack="true" OnTextChanged="txtPriceOfTheGrain_TextChanged"
                                    Style="direction: ltr"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="txtPriceOfTheGrain" ErrorMessage="* السعر الفردي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <h5>السعر الكلي : <span style="color: red">*</span>
                                </h5>
                                <asp:HiddenField ID="HFTotalPrice" runat="server" />
                                <asp:TextBox ID="txtTotalPrice" runat="server" class="form-control" ValidationGroup="VGDetails" Enabled="false"
                                    Style="direction: ltr"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="txtTotalPrice" ErrorMessage="* السعر الكلي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-9">
                        <div class="container-fluid" dir="rtl">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <h5>تاريخ الإنتهاء إن وجد :
                                    </h5>
                                    <div class="input-group date ">
                                        <asp:TextBox ID="txtExpiry_Date" runat="server" class="form-control" data-date-format="YYYY-MM-DD"
                                            ValidationGroup="VGDetails" Style="direction: ltr"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <h5>نوع الشحنة : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLType_Shipment" runat="server" ValidationGroup="VGDetails"
                                        CssClass="form-control2 chzn-select dropdown" Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                        ControlToValidate="DLType_Shipment" ErrorMessage="* نوع الشحنة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <br />
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <h5>مكان التخزين : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLProduct_Storage" runat="server" ValidationGroup="VGDetails"
                                        CssClass="form-control2 chzn-select dropdown" Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                        ControlToValidate="DLProduct_Storage" ErrorMessage="* مكان التخزين" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" dir="rtl">
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <h5>لمشروع : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DL_Project" runat="server" ValidationGroup="GBill"
                                        CssClass="form-control2 chzn-select dropdown" Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="DL_Project" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <br />
                                </div>
                            </div>
                            <div class="col-lg-3" style="display: none;">
                                <div class="form-group">
                                    <h5>رئيس مجلس الإدارة : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                        Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                        ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3" style="display: none;">
                                <div class="form-group">
                                    <h5>المشرف المالي : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                        Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                        ControlToValidate="DLAmeenAlSondoq" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <h5>مدير الجمعية : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                        Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                        ControlToValidate="DLModerAlGmeiah" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <h5>أمين المستودع : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLIDStorekeeper" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                        Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                        ControlToValidate="DLIDStorekeeper" ErrorMessage="* أمين المخزن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <asp:Image ID="Img" runat="server" ImageUrl="/ImgSystem/ImgProductStorage/no-img.jpg"
                            Style="border-radius: 6px" Width="100%" Height="130" />
                        <h5>صورة المنتج :
                        </h5>
                        <asp:FileUpload ID="FUArticle" runat="server" />
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <h5>وذلك لغرض : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txt_Note" runat="server" class="form-control" ValidationGroup="GBill" Text="-"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="txt_Note" ErrorMessage="* الغرض" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-5" runat="server" id="PnlAllow" visible="false">
                            <div class="form-group">
                                <h5>توقيع بدل كلاً من :
                                </h5>
                                <div class="keepmeLogged">
                                    <label class="switch" runat="server" id="IDRaeesAlmaglis" visible="false">
                                        رئيس المجلس
                                                <br />
                                        <input name="RememberMe" type="checkbox" id="CBRaeesAlmaglis" runat="server" />
                                        <span class="slider round"></span>
                                    </label>
                                    <i class="fa fa-minus"></i>
                                    <label class="switch" runat="server" id="IDAmeenAlsondoq" visible="false">
                                        المشرف المالي
                                                <br />
                                        <input name="RememberMe" type="checkbox" id="CBAmeenAlsondoq" runat="server" />
                                        <span class="slider round"></span>
                                    </label>
                                    <i class="fa fa-minus"></i>
                                    <label class="switch" runat="server" id="Label1" visible="false">
                                        مدر الجمعية
                                                <br />
                                        <input name="RememberMe" type="checkbox" id="CBModer" runat="server" />
                                        <span class="slider round"></span>
                                    </label>
                                    <i class="fa fa-minus"></i>
                                    <label class="switch" runat="server" id="IDAmeenAlMostodaa" visible="false">
                                        أمين المستودع
                                                <br />
                                        <input name="RememberMe" type="checkbox" id="CBAmeenAlMostodaa" runat="server" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnAdd" runat="server" Text="إضافة للفاتورة" Style="margin-right: 4px;"
                                    class="btn btn-info btn-fill pull-left" ValidationGroup="VGDetails" OnClick="btnAdd_Click" />
                                <asp:LinkButton ID="LBNew" runat="server" Style="margin-right: 4px;" OnClick="LBNew_Click"
                                    OnClientClick="return insertConfirmation();"
                                    class="btn btn-success btn-fill pull-left" ValidationGroup="GBill">حفظ بيانات الفاتورة</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="container-fluid" runat="server" visible="false" id="ProductByID">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-list"></i>قائمة شحنات الفاتورة
            </h3>
            <div style="float: left">
                <asp:LinkButton ID="LBRefresh2" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBRefresh2_Click"
                    title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                    OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                        <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            </div>
        </div>
        <div class="panel-body">
            <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft">
                <div class="table table-responsive">
                    <table class='table' style="width: 100%">
                        <thead>
                            <tr>
                                <th>
                                    <div class="HideNow">
                                    </div>
                                    <div align="center" class="w">
                                        <div>
                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                        </div>
                                    </div>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:GridView ID="GVDeedDonationInKind" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
                                        Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVDeedDonationInKind_RowDataBound"
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
                                            <asp:BoundField DataField="_ID_Item_" HeaderText="_ID_Item_" InsertVisible="False" ReadOnly="True"
                                                SortExpression="_ID_Item_" Visible="false" />
                                            <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# WSM_ClassCategory.FCategoryName((Int32) (Eval("_ID_Category_")))%>
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
                                            <asp:TemplateField HeaderText="التجزئة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# Convert.ToBoolean(Eval("_Is_There_Partition_")) ?
                                                     Eval("_Count_Partition_") + " / <small>" + Eval("UnitName") + "</small>"
                                                     :
                                                     " 1 " + " / <small>" + Eval("UnitName") + "</small>"
                                                    %>
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
                                                    <%# Eval("_One_Price_")%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="السعر الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("_Total_Price_")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
                                                <ItemTemplate>
                                                    <%# ClassQuaem.FAlBaheth((Int32) (Eval("_CreatedBy_")))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate_Add" runat="server"
                                                        Text='<%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") + " " + Eval("_CreatedDate_", "{0:HH:mm tt}")  %>' Font-Size="11px"></asp:Label>
                                                </ItemTemplate>
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
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th>
                                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                    <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                    <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    - <span style="font-size: 12px; padding-right: 5px">مجموع الشحنات : </span>
                                    <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    - <span style="font-size: 12px; padding-right: 5px">السعر الكلي : </span>
                                    <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                </th>
                            </tr>
                        </tfoot>
                    </table>
                </div>

            </asp:Panel>
            <asp:Panel ID="pnlNull" runat="server" Visible="False">
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
<style type="text/css">
    .modal-open {
        overflow: hidden
    }

    .modal {
        position: fixed;
        top: 0;
        right: 0;
        bottom: 0;
        left: 0;
        z-index: 1050;
        display: none;
        overflow: hidden;
        -webkit-overflow-scrolling: touch;
        outline: 0;
        background-color: hsla(120, 3%, 82%, 0.30);
    }
</style>
<hr />
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
    </script >
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn - select - deselect").chosen({allow_single_deselect: true }); </script>
<asp:HiddenField ID="HFXID" runat="server" />
