<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageAdd.ascx.cs" Inherits="Shaerd_ERP_FMS_Receipt_PageAdd" %>
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
        function ShowIDModelEdit() {
            $("#IDEditCompany").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                showModal();
            });
        });
    </script>
<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href="PageAdd.aspx" class="btn btn-primary">
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
            <li><a href="PageAdd.aspx">إضافة سند قبض</a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="Label1" runat="server" Text="المالية"></asp:Label>
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
                    <div class="container-fluid">
                        <div class="col-md-4">
                            <div class="form-group">
                                <h5>تغذية حساب : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLAccount" runat="server" ValidationGroup="GBill" Width="100%" 
                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" OnLoad="DLAccount_Load">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="الصندوق">الصندوق</asp:ListItem>
                                    <asp:ListItem Value="البنك">البنك</asp:ListItem>
                                    <asp:ListItem Value="تبرع_عام">تبرع_عام</asp:ListItem>
                                    <asp:ListItem Value="مصاريف_تشغيلية">مصاريف_تشغيلية</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                    ControlToValidate="DLAccount" ErrorMessage="* حدد التغذية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
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
                        <div runat="server" class="col-md-4">
                            <div class="form-group">
                                <h5>لمشروع : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DL_Project" runat="server" ValidationGroup="GBill" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                    ControlToValidate="DL_Project" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>  
                        <div class="col-lg-4">
                            <div class="form-group">
                            <h5>
                                <asp:Label ID="lblSend" runat="server" Text="إرسال SMS"></asp:Label> <i class="fa fa-envelope"></i> : <span style="color: red">*</span>
                            </h5>
                            <asp:DropDownList ID="DLSend" runat="server" ValidationGroup="GBill"
                                    CssClass="form-control" Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="Yes">نعم أرسل</asp:ListItem>
                                    <asp:ListItem Value="No" Selected="True">لا تقم بالإرسل</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator50" runat="server"
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
                                    AutoPostBack="true" OnSelectedIndexChanged="DL_Account_SelectedIndexChanged"
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
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="إضافة سند قبض"></asp:Label>
            </h3>
            <div style="float: left">
                رقم الفاتورة :
                    <asp:TextBox ID="txtNumberBill" runat="server" ValidationGroup="GBill" Width="100" Style="padding-right: 5px"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="txtNumberBill" ErrorMessage="* رقم الفاتورة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberBill"
                    Font-Size="11px" ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GBill"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="panel-body">
            <div class="content-box-large">
                <div class="widget-box">
                    <div class="container-fluid">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>الارشيف : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="GBill" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                                    Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                    ControlToValidate="ddlYears" ErrorMessage="* الارشيف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <asp:HiddenField ID="HFPhoneSender" runat="server" />
                                <h5>حدد الجهة أو الشخص : <span runat="server" id="lblPhone"></span> <asp:LinkButton ID="LBEdit" runat="server" OnClick="LBEdit_Click"></asp:LinkButton> <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="GBill" AutoPostBack="true"
                                    OnSelectedIndexChanged="DLCompany_SelectedIndexChanged"
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
                                                                <asp:Label ID="lblTitle" runat="server" Text="إضافة جهة أو شخص : "></asp:Label> 
                                                            </label>
                                                            <div align="">
                                                                <div class="" dir="rtl">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <h5>التصنيف : <i style="color: red">*</i></h5>

                                                                            <asp:DropDownList ID="D_Category" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGAdd2">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator24" runat="server"
                                                                                ControlToValidate="D_Category" ErrorMessage="*  حدد التصنيف" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <h5>الفئة : <i style="color: red">*</i></h5>

                                                                            <asp:DropDownList ID="DLType_Customer" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGAdd2">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                                <asp:ListItem Value="شركات">شركات</asp:ListItem>
                                                                                <asp:ListItem Value="أفراد">أفراد</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                                                ControlToValidate="DLType_Customer" ErrorMessage="*  حدد الفئة" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="form-group">
                                                                            <h5 style="text-align:right"> الإسم : <i style="color: red">*</i></h5>
                                                                            <asp:HiddenField ID="HFID" runat="server" />
                                                                            <asp:HiddenField ID="HFPhone" runat="server" />
                                                                            <asp:TextBox ID="txtName" runat="server" class="form-control" ValidationGroup="VGAdd2"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator19" runat="server"
                                                                                ControlToValidate="txtName" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="form-group">
                                                                            <h5>رقم الجوال (رسائل SMS) : <i style="color: red">*</i></h5>
                                                                            <asp:TextBox ID="txtPhone_Number1" runat="server" class="form-control" ValidationGroup="VGAdd2" TextMode="Number"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                                                                ControlToValidate="txtPhone_Number1" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhone_Number1"
                                                                                ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="VGAdd2" Font-Size="10px"
                                                                                Display="Dynamic">
                                                                            </asp:RegularExpressionValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:LinkButton ID="LBSave2" runat="server" Style="margin-right: 4px;" OnClick="LBSave2_Click"
                                                                OnClientClick="return insertConfirmation();" Visible="true"
                                                                class="btn btn-success btn-fill pull-left" ValidationGroup="VGAdd2">تحديث البيانات</asp:LinkButton>
                                                            <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="IDEditCompany" class="modal fade in modal_New_Style">
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
                                                                <asp:Label ID="lblTitleEdit" runat="server" Text="تعديل جهة أو شخص : "></asp:Label> 
                                                            </label>
                                                            <div align="">
                                                                <div class="" dir="rtl">
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <h5>التصنيف : <i style="color: red">*</i></h5>
                                                                            <asp:DropDownList ID="D_CategoryEdit" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGEdit2">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                                                                ControlToValidate="D_CategoryEdit" ErrorMessage="*  حدد التصنيف" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12">
                                                                        <div class="form-group">
                                                                            <h5>الفئة : <i style="color: red">*</i></h5>
                                                                            <asp:DropDownList ID="DLType_CustomerEdit" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" 
                                                                                Style="font-size: 12px;" ValidationGroup="VGEdit2">
                                                                                <asp:ListItem Value=""></asp:ListItem>
                                                                                <asp:ListItem Value="شركات">شركات</asp:ListItem>
                                                                                <asp:ListItem Value="أفراد">أفراد</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator200" runat="server"
                                                                                ControlToValidate="DLType_CustomerEdit" ErrorMessage="*  حدد الفئة" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="form-group">
                                                                            <h5 style="text-align:right"> الإسم : <i style="color: red">*</i></h5>
                                                                            <asp:TextBox ID="txtNameEdit" runat="server" class="form-control" ValidationGroup="VGEdit2"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator190" runat="server"
                                                                                ControlToValidate="txtNameEdit" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="form-group">
                                                                            <h5>رقم الجوال (رسائل SMS) : <i style="color: red">*</i></h5>
                                                                            <asp:TextBox ID="txtPhone_Number1Edit" runat="server" class="form-control" ValidationGroup="VGEdit2" TextMode="Number"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator55" runat="server"
                                                                                ControlToValidate="txtPhone_Number1Edit" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator40" runat="server" ControlToValidate="txtPhone_Number1Edit"
                                                                                ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="VGEdit2" Font-Size="10px"
                                                                                Display="Dynamic">
                                                                            </asp:RegularExpressionValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:LinkButton ID="LBSaveEdit" runat="server" Style="margin-right: 4px;" OnClick="LBSaveEdit_Click"
                                                                OnClientClick="return insertConfirmation();" Visible="true"
                                                                class="btn btn-success btn-fill pull-left" ValidationGroup="VGEdit2">تحديث البيانات</asp:LinkButton>
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
                            <div class="col-lg-6">
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        ميلادي
                                        <br />
                                        <asp:RadioButton ID="RBDateM" runat="server" GroupName="GADate" Checked="true" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        هجري
                                        <br />
                                        <asp:RadioButton ID="RBDateH" runat="server" GroupName="GADate" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <script type="text/javascript">
                                $(function () {
                                    $(document.getElementById("<%=RBDateM.ClientID %>")).click(function () {
                                        if ($(this).is(":checked")) {
                                            $("#pnlMelady").show();
                                            $("#pnlHijri").hide();
                                        } else {
                                            $("#pnlMelady").hide();
                                            $("#pnlHijri").show();
                                        }
                                    });
                                });
                            </script>
                            <script type="text/javascript">
                                $(function () {
                                    $(document.getElementById("<%=RBDateH.ClientID %>")).click(function () {
                                        if ($(this).is(":checked")) {
                                            $("#pnlHijri").show();
                                            $("#pnlMelady").hide();
                                        } else {
                                            $("#pnlHijri").hide();
                                            $("#pnlMelady").show();
                                        }
                                    });
                                });
                            </script>
                            <div class="form-group">
                                <h5>تاريخ الفاتورة : <span style="color: red">*</span>
                                </h5>
                                <div id="pnlMelady" style="display: none; <%= FCheck("Melady") %>">
                                    <div class="input-group date " >
                                        <asp:TextBox ID="txtDateAdd" runat="server" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="GBill" 
                                            Style="direction: ltr; text-align:left;"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtDateAdd" ErrorMessage="* تاريخ الفاتورة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div id="pnlHijri" style="display: none; <%= FCheck("Hijri") %>">
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
                                                <asp:DropDownList ID="ddlDatesH" runat="server" Width="40">
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
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <h5>المبلغ : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtThe_Mony" runat="server" class="form-control" ValidationGroup="GBill"
                                    Style="direction: ltr; text-align:left;"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="txtThe_Mony" ErrorMessage="* المبلغ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="(?:\d*\.\d{1,2}|\d+)$" ErrorMessage="* أرقام فقط"
                                        ControlToValidate="txtThe_Mony" ValidationGroup="GBill" Font-Size="10" />
                            </div>
                        </div>
                        <div class="col-md-3" align="center">
                            <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        نقداً : 
                                        <br />
                                        <asp:RadioButton ID="RBIsCash_Money" runat="server" GroupName="GAdow" AutoPostBack="true" OnCheckedChanged="RBIsCash_Money_CheckedChanged" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-3" align="center">
                            <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        شيك : 
                                        <br />
                                        <asp:RadioButton ID="RBIsShayk_Bank" runat="server" GroupName="GAdow" AutoPostBack="true" OnCheckedChanged="RBIsShayk_Bank_CheckedChanged" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-3" align="center">
                            <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        إيداع بنكي : 
                                        <br />
                                        <asp:RadioButton ID="RBIsConvert_Bank" runat="server" GroupName="GAdow" AutoPostBack="true" OnCheckedChanged="RBIsConvert_Bank_CheckedChanged" />
                                        <span class="slider round"></span>
                                    </label>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl" runat="server" id="NumberShayk" visible="false"
                        style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <h5>رقم الشيك :
                                </h5>
                                <asp:TextBox ID="txtNumber_Shayk_Bank" runat="server" class="form-control" ValidationGroup="GBill" Style="direction: ltr; text-align:left;"></asp:TextBox>
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
                                    <div class="input-group date ">
                                        <asp:TextBox ID="txtDate_Shayk" runat="server" class="form-control" Width="200" data-date-format="YYYY-MM-DD"
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
                                <asp:TextBox ID="txtFor_Bank" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl" runat="server" id="Transfer_On_Account" visible="false"
                        style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <h5>على الحساب رقم :
                                </h5>
                                <asp:TextBox ID="txtNumber_Account" runat="server" class="form-control" ValidationGroup="GBill" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumber_Shayk_Bank"
                                    ErrorMessage="* أرقام فقط" Font-Size="11px" ValidationExpression="^[0-9]+$" ValidationGroup="GBill"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <h5>تاريخ الإيداع :
                                </h5>
                                <div class="col-sm-3">
                                    <div class="input-group date ">
                                        <asp:TextBox ID="txtDate_Bank_Transfer" runat="server" class="form-control" data-date-format="YYYY-MM-DD"
                                            ValidationGroup="GBill" Style="direction: ltr; text-align:left;" Width="200"></asp:TextBox>
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
                                <asp:TextBox ID="txtFor_Bank_Transfer" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl" style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                        <div class="col-lg-12">
                            <h5 style="margin-top:5px;">البنود : </h5>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <h5 style="text-align: right">عدد البنود : <span style="color: red">*</span></h5>
                                    <asp:DropDownList ID="DLCount" runat="server" ValidationGroup="GBill"
                                        CssClass="form-control" OnLoad="DLCount_Load">
                                        <asp:ListItem Value="1" Selected="True"> بند واحد </asp:ListItem>
                                        <asp:ListItem Value="2"> بندين </asp:ListItem>
                                        <asp:ListItem Value="3"> ثلاثة بنود </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* طبيعة الإستخدام" CssClass="font"
                                        ControlToValidate="DLCount" ValidationGroup="GBill" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <h5>البند الرئيسي : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLMainItems" runat="server" ValidationGroup="GBill" AutoPostBack="true" 
                                        OnSelectedIndexChanged="DLMainItems_SelectedIndexChanged" CssClass="form-control" Style="font-size: 12px;">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="DLMainItems" ErrorMessage="* حدد البند" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <h5>البند الفرعي الأول : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLSubItems" runat="server" ValidationGroup="GBill" AutoPostBack="true"
                                        OnSelectedIndexChanged="DLSubItems_SelectedIndexChanged" CssClass="form-control" Style="font-size: 12px;">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                        ControlToValidate="DLSubItems" ErrorMessage="* حدد البند" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <h5 style="text-align: right">البند الفرعي الثاني : <i style="color: red">*</i></h5>
                                    <asp:DropDownList ID="DLSubItemsTow" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"
                                        OnSelectedIndexChanged="DLSubItemsTow_SelectedIndexChanged" ValidationGroup="GBill">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    <h5 style="text-align: right">البند الفرعي الثالث : <i style="color: red">*</i></h5>
                                    <asp:DropDownList ID="DLSubItemsThree" runat="server" CssClass="form-control" Enabled="false" ValidationGroup="GBill">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <script type="text/javascript">
                                function ValidateAdd() {
                                    var ddlFruits = document.getElementById("<%=DLCount.ClientID %>");

                                    if (ddlFruits.value == "2") {
                                        document.getElementById("<%=DLSubItemsTow.ClientID %>").disabled = false;
                                        document.getElementById("<%=DLSubItemsThree.ClientID %>").disabled = true;
                                    }
                                    else if (ddlFruits.value == "3") {
                                        document.getElementById("<%=DLSubItemsTow.ClientID %>").disabled = false;
                                        document.getElementById("<%=DLSubItemsThree.ClientID %>").disabled = false;
                                    }
                                    else {
                                        document.getElementById("<%=DLSubItemsTow.ClientID %>").disabled = true;
                                                document.getElementById("<%=DLSubItemsThree.ClientID %>").disabled = true;
                                    }
                                    return true;
                                }
                            </script>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl" style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                        <div class="col-lg-12">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <h5>وذلك مقابل :
                                    </h5>
                                    <asp:TextBox ID="txt_Note" runat="server" class="form-control" ValidationGroup="GBill" Text="-"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txt_Note" ErrorMessage="* الغرض" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-4" runat="server">
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
                                <h5>المشرف المالي : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="GBill" CssClass="form-control chzn-select dropdown">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                    ControlToValidate="DLAmeenAlSondoq" ErrorMessage="* المشرف المالي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div runat="server" visible="false" class="col-lg-4">
                            <div class="form-group">
                                <h5>رئيس مجلس الإدارة : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="GBill" CssClass="form-control chzn-select dropdown">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                    ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group" align="left">
                                <br />
                                <asp:LinkButton ID="LBNew" runat="server"  ValidationGroup="GBill"  OnClick="LBNew_Click"
                                    class="btn btn-info">حفظ البيانات</asp:LinkButton>
                                <asp:LinkButton ID="LB_Back" runat="server"  OnClick="LB_Back_Click"
                                    class="btn btn-danger">خروج</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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