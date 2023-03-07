<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/EOS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMatterOfExchangeGroup.aspx.cs" Inherits="Cpanel_ERP_EOS_In_Kind_Donation_PageMatterOfExchangeGroup" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.WSM" %>

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
            var gv = document.getElementById("<%=GVBeneficiaryAll.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي مستفيد بعد");
                return false;
            }
            else {
                return confirm(" هل أنت متأكد من الإستمرار , لقد قمت بالتحديد على " + count +" مستفيد ؟");
            }
        }
    </script>

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageMatterOfExchangeGroup.aspx" class="btn btn-primary"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default">
                     <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="">إضافة أمر صرف عيني</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="pnlMostafeed">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إنشاء أمر صرف عيني"></asp:Label>

                        <asp:Label ID="lblCountProduct" runat="server"></asp:Label>
                    </h3>
                    <div style="float: left">
                        <span>حدد المشروع : <span style="color: red">*</span>
                        </span>
                        <asp:DropDownList ID="DLSupportType" runat="server" ValidationGroup="GDetails" CssClass="form-control2" AutoPostBack="true" 
                            OnSelectedIndexChanged="DLSupportType_SelectedIndexChanged" Width="150">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                            ControlToValidate="DLSupportType" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        <label class="control-label">
                            الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                        </label>
                        <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                            Width="100" ValidationGroup="GDetails">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="panel-body">
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
                            <asp:HiddenField ID="HFCountProduct" runat="server" />
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="content-box-large">
                        <div class="container-fluid" dir="rtl">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>حدد الداعم : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="GDetails" Width="100%" CssClass="form-control2 chzn-select dropdown" Enabled="true"
                                         Style="font-size: 12px;" AutoPostBack="true" OnSelectedIndexChanged="DLCompany_SelectedIndexChanged">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                        ControlToValidate="DLCompany" ErrorMessage="* حدد الداعم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GDetails" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>المبادرة : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLInitiatives" runat="server" ValidationGroup="GDetails" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                        ControlToValidate="DLInitiatives" ErrorMessage="* المبادرة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5>ر/الفاتورة
                                    </h5>
                                    <asp:TextBox ID="txtNumberBill" runat="server" class="form-control" ValidationGroup="GDetails" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="txtNumberBill" ErrorMessage="* رقم الفاتورة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumberBill"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GDetails"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
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
                    <div style="float:left; width:150px;">
                        <div class="input-group date ">
                            <asp:TextBox ID="txt_Add" runat="server" class="form-control" Width="120"
                                data-date-format="YYYY-MM-DD" ValidationGroup="GDetails" Style="direction: ltr"></asp:TextBox>
                            <span class="input-group-btn">
                                <button class="btn btn-default" type="button">
                                    <i class="fa fa-calendar"></i>
                                </button>
                            </span>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                ControlToValidate="txt_Add" ErrorMessage="* تاريخ الإضافة" ForeColor="#FF0066"
                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div runat="server" id="IDSelectDoner" class="col-md-12">
                                    <small style="color:red;">حدد الداعم أولاً</small>
                                </div> 
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>حدد الصنف : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="GDetails" Enabled="false"
                                            AutoPostBack="true" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%" OnSelectedIndexChanged="DLCategory_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="DLCategory" ErrorMessage="* حدد الصنف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>حدد المنتج : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLProduct" runat="server" ValidationGroup="GDetails" Enabled="false"
                                            CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                            Width="100%" OnSelectedIndexChanged="DLProduct_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="DLProduct" ErrorMessage="* حدد المنتج" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div runat="server" id="PnlSelectPrice" class="col-md-5" visible="false">
                                    <div class="form-group">
                                        <h5>حدد السعر المتوفر : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Price" runat="server" ValidationGroup="GDetails" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DL_Price_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="DL_Price" ErrorMessage="* حدد السعر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div runat="server" id="PnlInputPrice" class="col-md-5" visible="false">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5>السعر الفردي : <span style="color: red">*</span>
                                            </h5>
                                            <asp:TextBox ID="txtPrice" runat="server" class="form-control" ValidationGroup="GDetails"
                                                Style="direction: ltr"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                                ControlToValidate="txtPrice" ErrorMessage="* السعر الفردي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPrice"
                                                ErrorMessage="* أرقام فقط" Font-Size="10px" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$" ValidationGroup="GDetails" Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5>التجزئة : <span style="color: red">*</span>
                                            </h5>
                                            <asp:TextBox ID="txtCount_Partition" runat="server" class="form-control" ValidationGroup="GDetails"
                                                Style="direction: ltr" Text="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator19" runat="server"
                                                ControlToValidate="txtCount_Partition" ErrorMessage="* التجزئة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCount_Partition"
                                                ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GDetails" Font-Size="10px"
                                                Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>العدد ( لكل شخص) : <span style="color: red">*</span>
                                            <asp:Label ID="lblCheckCountProduct" runat="server" ForeColor="Red"></asp:Label>
                                        </h5>
                                        <asp:TextBox ID="txtCountProduct" runat="server" class="form-control" ValidationGroup="GDetails"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtCountProduct" ErrorMessage="* الكمية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCountProduct"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GDetails"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>رئيس مجلس الإدارة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="GDetails" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2" style="display:none;">
                                    <div class="form-group">
                                        <h5>نائب رئيس المجلس : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLNaeebRaeesMagles" runat="server" ValidationGroup="GDetails" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="DLNaeebRaeesMagles" ErrorMessage="* نائب رئيس مجلس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>المشرف المالي : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="GDetails" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="DLAmeenAlSondoq" ErrorMessage="* المشرف المالي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>مدير الجمعية : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="GDetails" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="DLModerAlGmeiah" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>أمين المستودع : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLIDStorekeeper" runat="server" ValidationGroup="GDetails" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                            ControlToValidate="DLIDStorekeeper" ErrorMessage="* أمين المخزن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>الباحث : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="GDetails" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                            ControlToValidate="DLAlBaheth" ErrorMessage="* حدد الباحث" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <h5>ملاحظة :
                                        </h5>
                                        <asp:TextBox ID="txtdescription" runat="server" class="form-control" ValidationGroup="GDetails"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:RadioButton ID="RBAll" runat="server" Text="جميع المستفيدين" GroupName="XCheck" Checked="true" 
                                            AutoPostBack="true" OnCheckedChanged="RBAll_CheckedChanged" />
                                        <asp:RadioButton ID="RBDaeem" runat="server" Text="الدائم" GroupName="XCheck" 
                                            AutoPostBack="true" OnCheckedChanged="RBAll_CheckedChanged" />
                                        <asp:RadioButton ID="RBMostabeed" runat="server" Text="المستبعد" GroupName="XCheck" 
                                            AutoPostBack="true" OnCheckedChanged="RBAll_CheckedChanged" />
                                        <asp:Button ID="btnAdd" runat="server" Text="إضافة للفاتورة" Style="margin-right: 4px;" Visible="false"
                                            class="btn btn-info btn-fill pull-left" ValidationGroup="GDetails" OnClick="btnAdd_Click" />

                                        

                                        <asp:LinkButton ID="LBNew" runat="server" Style="margin-right: 4px; margin-right:10px;" OnClick="LBNew_Click" 
                                            OnClientClick="return ConfirmDelete();"
                                            class="btn btn-success btn-fill pull-left" ValidationGroup="GDetails">حفظ بيانات الفاتورة</asp:LinkButton>

                                        <a href='javaScript:void(0)' class="btn btn-info btn-fill pull-left" 
                                            data-toggle="modal" data-target="#ConsultationModal">عرض المستفيدين</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid" id="pnlStar" runat="server" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label3" runat="server" Text="يرجى تحديد نوع المشروع"></asp:Label>
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
                                    <h3 style="font-size: 20px">حدد نوع المشروع
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

        <div class="modal fade" id="ConsultationModal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>
                        <h4 class="modal-title">قائمة بيانات المستفيدين</h4>
                    </div>
                    <div class="modal-body">
                        <div id="modal_form_login">
                            <div class="page_container">
                                <div class="fancy_inputs">
                                    <div class="container-fluid" dir="rtl">
                                        <div class="col-md-12">
                                            <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                                                <asp:GridView ID="GVBeneficiaryAll" runat="server" AutoGenerateColumns="False" DataKeyNames="NumberMostafeed"
                                                        Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                        UseAccessibleHeader="False" OnDataBound="GVBeneficiaryAll_DataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="10px">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Font-Size="12px" Text='<%# Eval("NumberMostafeed") %>' Visible="false"></asp:Label>
                                                                    <span style="margin-right: 5px; font-size: 11px">
                                                                        <asp:Label ID="lblM" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="NumberMostafeed" HeaderText="رقم الملف" SortExpression="NumberMostafeed"
                                                                HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <span style="font-size: 11px">
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("NameMostafeed")%>'></asp:Label>
                                                                    </span>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="حالة الحساب" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_TypeMostafeed" runat="server" Text='<%# ClassMosTafeed.FChangColor((string) Eval("TypeMostafeed"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="الحالة" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_HalafAlMosTafeed" runat="server" Text='<%# ClassQuaem.FHalatMostafeed((Int32) Eval("HalafAlMosTafeed"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AlQaryah" runat="server" Text='<%# ClassQuaem.FAlQarabah((Int32) Eval("AlQaryah"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
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
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                <div style="float: right">
                                                    <span style="font-size: 12px; padding-right: 5px">عدد الأسر : </span>
                                                    <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                </div>

                                                <script src="quicksearch.js"></script>
                                                <script type="text/javascript">
                                                    $(function () {
                                                        $('.search_textbox').each(function (i) {
                                                            $(this).quicksearch("[id*=GVBeneficiaryAll] tr:not(:has(th))", {
                                                                'testQuery': function (query, txt, row) {
                                                                    return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                                                                }
                                                            });
                                                        });
                                                    });
                                                </script>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                                <br />
                                                <hr />
                                                <br />
                                                <div align="center">
                                                    <h3 style="font-size: 20px">لا توجد نتائج
                                                    </h3>
                                                </div>
                                                <br />
                                                <hr />
                                                <br />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">اغلاق</button>
                    </div>
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
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
        <asp:HiddenField ID="HFXID" runat="server" />
</asp:Content>

