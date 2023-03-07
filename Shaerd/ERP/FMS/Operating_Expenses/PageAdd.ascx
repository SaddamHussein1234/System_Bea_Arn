<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageAdd.ascx.cs" Inherits="Shaerd_ERP_FMS_Operating_Expenses_PageAdd" %>
<script type="text/javascript">
        function Confirmation() {
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

<div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="" class="btn btn-primary">
                        <i class="fa fa-plus"></i>
                    </a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClientClick="return Confirmation();" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" 
                        OnClientClick="return Confirmation();" OnClick="LBExit_Click" CssClass="btn btn-default">
                     <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../">الرئيسية</a></li>
                    <li><a href="">إضافة/تعديل مصروفات تشغيلية</a></li>
                </ul>
            </div>
        </div>
        <div class="col-md-9">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="المالية"></asp:Label>
                    </h3>
                    <div style="float: left">
                        رقم الفاتورة :
                        <asp:TextBox ID="txtNumberOrder" runat="server" class="form-control2" ValidationGroup="GBill" Enabled="false" Width="100"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtNumberOrder" ErrorMessage="* رقم الفاتورة " ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumberOrder"
                            ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GBill" Font-Size="10px"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                    </div>
                    <div style="float: left">
                        <span>حدد المشروع : <span style="color: red">*</span>
                        </span>
                        <asp:DropDownList ID="DLSupportType" runat="server" ValidationGroup="GBill" CssClass="form-control2" 
                            AutoPostBack="true" OnSelectedIndexChanged="DLSupportType_SelectedIndexChanged" style="margin-left:10px;"
                            Width="150">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                            ControlToValidate="DLSupportType" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الارشيف : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="GBill" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                                            Height="25px" Width="100%" CssClass="form-control chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="ddlYears" ErrorMessage="* الارشيف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>السحب من حساب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAccount" runat="server" ValidationGroup="GBill" Width="100%" CssClass="form-control chzn-select dropdown"
                                            Style="font-size: 12px;" AutoPostBack="true" OnSelectedIndexChanged="DLAccount_SelectedIndexChanged" OnLoad="DLAccount_Load">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="الصندوق">الصندوق</asp:ListItem>
                                            <asp:ListItem Value="البنك">البنك</asp:ListItem>
                                            <asp:ListItem Value="تبرع_عام">تبرع_عام</asp:ListItem>
                                            <asp:ListItem Value="مصاريف_تشغيلية">مصاريف_تشغيلية</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="DLAccount" ErrorMessage="* حدد الحساب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <script type="text/javascript">
                                    function Validate() {
                                        var ddlFruits = document.getElementById("<%=DLAccount.ClientID %>");
                                        var PanelID = document.getElementById("pnlBank");

                                        if (ddlFruits.value == "البنك") {
                                            if (PanelID.style.display == "none") {
                                                PanelID.style.display = "block";
                                            }
                                            else {
                                                PanelID.style.display = "none";
                                            }
                                        }
                                        else {
                                            PanelID.style.display = "none";
                                        }
                                        return true;
                                    }
                                </script>
                                <div class="col-lg-5">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="lblSend" runat="server" Text="إرسال إشعار SMS"></asp:Label> <span style="color: red">تحت الصيانة</span>
                                            <i class="fa fa-envelope"></i>: <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLSend" runat="server" ValidationGroup="GBill" Enabled="false"
                                            CssClass="form-control" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="Yes">نعم أرسل</asp:ListItem>
                                            <asp:ListItem Value="No">لا تقم بالإرسل</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator50" runat="server" Visible="false"
                                            ControlToValidate="DLSend" ErrorMessage="* حدد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" id="pnlBank" style="<%= FCheck("Bank") %>">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5>حدد البنك : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Bank" runat="server" ValidationGroup="GBill" Width="300" 
                                            AutoPostBack="true" OnSelectedIndexChanged="DL_Bank_SelectedIndexChanged"
                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5>حدد الحساب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Account" runat="server" ValidationGroup="GBill" Width="350" 
                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div runat="server" id="pnlMostafeed">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="lbmsg" runat="server" Text="المصروفات التشغيلية"></asp:Label>
                        </h3>
                        <div style="float: left; margin-right:15px;">
                            <div class="input-group date " style="margin-right: -10px; width:120px;">
                                <asp:TextBox ID="txtDateAdd" runat="server" class="form-control" Width="100" data-date-format="YYYY-MM-DD"
                                    placeholder=" تاريخ الإضافة" ValidationGroup="GBill" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="txtDateAdd" ErrorMessage="* تاريخ الإضافة" ForeColor="#FF0066"
                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <asp:Panel ID="pnlAlDaam" runat="server" Visible="False">
                                    <div class="container-fluid" dir="rtl">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <h5>حدد الداعم : <span style="color: red">*</span>
                                                </h5>
                                                <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="GBill" Width="100%" CssClass="form-control2 chzn-select dropdown"
                                                        Style="font-size: 12px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                                    ControlToValidate="DLCompany" ErrorMessage="* حدد الداعم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <h5>المبلغ : <span style="color: red">*</span>
                                                </h5>
                                                <asp:TextBox ID="txtThe_Mony" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                                    ControlToValidate="txtThe_Mony" ErrorMessage="* المبلغ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$" ErrorMessage="* أرقام فقط"
                                                    ControlToValidate="txtThe_Mony" Font-Size="10" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <h5>نوع الدفع : <span style="color: red">*</span>
                                                </h5>
                                                <asp:RadioButton ID="RBIsCash_Money" runat="server" GroupName="Type" AutoPostBack="true" OnCheckedChanged="RBIsCash_Money_CheckedChanged" />
                                                <span>نقداً </span>
                                                <asp:RadioButton ID="RBIsShayk_Bank" runat="server" GroupName="Type" AutoPostBack="true" OnCheckedChanged="RBIsShayk_Bank_CheckedChanged" />
                                                <span>شيك </span>
                                                <asp:RadioButton ID="RBIsConvert_Bank" runat="server" GroupName="Type" AutoPostBack="true" OnCheckedChanged="RBIsConvert_Bank_CheckedChanged" />
                                                <span>تحويل على الحساب </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid" dir="rtl" runat="server" id="NumberShayk" visible="false"
                                        style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <h5>رقم الشيك :
                                                </h5>
                                                <asp:TextBox ID="txtNumber_Shayk_Bank" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumber_Shayk_Bank"
                                                    ErrorMessage="* أرقام فقط" Font-Size="11px" ValidationExpression="^[0-9]+$" ValidationGroup="GBill"
                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <h5>تاريخ الشيك :
                                                </h5>
                                                <div class="col-sm-3">
                                                    <div class="input-group date " style="margin-right: -10px">
                                                        <asp:TextBox ID="txtDate_Shayk" runat="server" class="form-control" Width="170" data-date-format="YYYY-MM-DD"
                                                            ValidationGroup="GBill" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-default" type="button">
                                                                <i class="fa fa-calendar"></i>
                                                            </button>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <h5>على بنك :
                                                </h5>
                                                <asp:TextBox ID="txtFor_Bank" runat="server" class="form-control" ValidationGroup="GBill"
                                                    Style="direction: ltr; text-align:left;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid" dir="rtl" runat="server" id="Transfer_On_Account" visible="false"
                                        style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <h5>على الحساب رقم :
                                                </h5>
                                                <asp:TextBox ID="txtNumber_Account" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumber_Shayk_Bank"
                                                    ErrorMessage="* أرقام فقط" Font-Size="11px" ValidationExpression="^[0-9]+$" ValidationGroup="GBill"
                                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <h5>تاريخ الإيداع :
                                                </h5>
                                                <div class="col-sm-3">
                                                    <div class="input-group date " style="margin-right: -10px">
                                                        <asp:TextBox ID="txtDate_Bank_Transfer" runat="server" class="form-control" Width="170" data-date-format="YYYY-MM-DD"
                                                            ValidationGroup="GBill" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-default" type="button">
                                                                <i class="fa fa-calendar"></i>
                                                            </button>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <h5>على بنك :
                                                </h5>
                                                <asp:TextBox ID="txtFor_Bank_Transfer" runat="server" class="form-control" ValidationGroup="GBill"
                                                    Style="direction: ltr; text-align:left;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid" dir="rtl">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <h5>وذلك لغرض :
                                                </h5>
                                                <asp:TextBox ID="txtDetails" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtDetails" ErrorMessage="* الغرض" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <h5>ملاحظة :
                                                </h5>
                                                <asp:TextBox ID="txt_Note" runat="server" class="form-control" ValidationGroup="GBill" MaxLength="512"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid" dir="rtl">
                                        <div class="col-md-3">
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
                                        <div class="col-md-5" runat="server" visible="false">
                                            <div class="form-group">
                                                <h5>نائب رئيس المجلس : <span style="color: red">*</span>
                                                </h5>
                                                <asp:DropDownList ID="DLNaeebRaeesMagles" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                                    Width="100%">
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                                    ControlToValidate="DLNaeebRaeesMagles" ErrorMessage="* نائب رئيس مجلس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <h5>المشرف المالي : <span style="color: red">*</span>
                                                </h5>
                                                <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                                    Width="100%">
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                                    ControlToValidate="DLAmeenAlSondoq" ErrorMessage="* المشرف المالي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <h5>مدير الجمعية : <span style="color: red">*</span>
                                                </h5>
                                                <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                                    Width="100%">
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                                    ControlToValidate="DLModerAlGmeiah" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-5" runat="server" visible="false">
                                            <div class="form-group">
                                                <h5>أمين المستودع : <span style="color: red">*</span>
                                                </h5>
                                                <asp:DropDownList ID="DLIDStorekeeper" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                                    Width="100%">
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                                    ControlToValidate="DLIDStorekeeper" ErrorMessage="* أمين المخزن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-5" runat="server" visible="false">
                                            <div class="form-group">
                                                <div>
                                                    <h5>الباحث الذي سيقوم بالتسليم : <span style="color: red">*</span>
                                                    </h5>
                                                    <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                                        Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                                        ControlToValidate="DLAlBaheth" ErrorMessage="* حدد الباحث" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group" align="left">
                                                <br />
                                                <asp:LinkButton ID="btnAdd" runat="server"
                                                    CssClass="btn btn-info" ValidationGroup="GBill" OnClick="btnAdd_Click">
                                                        تحديث الفاتورة
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LBExit2" runat="server" OnClientClick="return Confirmation();" OnClick="LBExit_Click"
                                                    class="btn btn-danger">رجوع</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlStarView" runat="server" Visible="False">
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
                                        <h3 style="font-size: 20px">
                                            <asp:Label ID="lblStar" runat="server" Text="يرجى تحديد المشروع"></asp:Label>
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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="container-fluid" dir="rtl">
                <div class="tile tile-heading" style="border-radius: 8px">
                    <div class="tile-body">
                        <h5>
                            <asp:Label ID="lblReceipt" runat="server" Text="الرصيد اللذي تم شحنة"></asp:Label>
                        </h5>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 35%; border: thin double #808080; border-width: 1px; padding: 3px" align="center">المجموع
                                </td>

                                <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:HiddenField ID="HFSumReceipt" runat="server" />
                                    <asp:Label ID="lblSumReceipt" runat="server" Text="0" Style='font-size: 12px'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:Label ID="lblSumWordReceipt" runat="server" Text="0" class="form-control" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 16px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="tile tile-heading" style="border-radius: 8px">
                    <div class="tile-body">
                        <h5>
                            <asp:Label ID="lblCashing" runat="server" Text="الرصيد اللذي تم سحبة"></asp:Label>
                        </h5>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 35%; border: thin double #808080; border-width: 1px; padding: 3px" align="center">المجموع
                                </td>
                                <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:HiddenField ID="HFSumCashing" runat="server" />
                                    <asp:Label ID="lblSumCashing" runat="server" Text="0" Style='font-size: 12px'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:Label ID="lblSumWordCashing" runat="server" Text="0" class="form-control" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 16px;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="tile tile-heading" style="border-radius: 8px">
                    <div class="tile-body">
                        <h4>
                            <asp:Label ID="lblSumTotal" runat="server"></asp:Label>
                        </h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
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