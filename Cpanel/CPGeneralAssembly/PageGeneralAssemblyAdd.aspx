<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPGeneralAssembly/MPCPanel.master" AutoEventWireup="true" CodeFile="PageGeneralAssemblyAdd.aspx.cs" Inherits="Cpanel_CPGeneralAssembly_PageGeneralAssemblyAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LB_Add" runat="server" data-toggle="tooltip" title="إضافة ملف جديد" OnClick="LB_Add_Click"
                        class="btn btn-info"> <i class="fa fa-plus"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LB_Back_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageGeneralAssembly.aspx">إدارة الأعضاء</a></li>
                    <li><a href="PageGeneralAssemblyAdd.aspx">إضافة عضو جديد</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة عضو جديد"></asp:Label>
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
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>رقم العضوية : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNumber_Rigstry" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtNumber_Rigstry" ErrorMessage="* رقم العضوية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumber_Rigstry"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>حدد العضو : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Admin" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown"
                                            AutoPostBack="true" OnSelectedIndexChanged="DL_Admin_SelectedIndexChanged" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="DL_Admin" ErrorMessage="* حدد العضو" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>تاريخ الميلاد : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDate_Bridth" runat="server" class="form-control" Width="170" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                                    ControlToValidate="txtDate_Bridth" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2" runat="server" id="PnlAllow" visible="false">
                                    <div class="form-group">
                                        <h5>توقيع بدل كلاً من :
                                        </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                رئيس المجلس
                                                <br />
                                                <input name="RememberMe" type="checkbox" id="CBRaeesAlmaglis" runat="server" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>نوع الصفة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtThe_Job" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtThe_Job" ErrorMessage="* نوع الصفة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>عنوان العمل :
                                        </h5>
                                        <asp:TextBox ID="txtAddress_Job" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>رقم السجل المدني : 
                                        </h5>
                                        <asp:Label ID="lbl_IC_Card" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>تاريخها : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDate_Card" runat="server" class="form-control" Width="160" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                    ControlToValidate="txtDate_Card" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>مصدرها : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtCard_Source" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="txtCard_Source" ErrorMessage="* مصدرها" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>                           
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>جوال : 
                                        </h5>
                                        <asp:Label ID="lbl_Phone" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>البريد الإلكتروني : </h5>
                                        <asp:Label ID="lbl_Email" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>هاتف المنزل :
                                        </h5>
                                        <asp:TextBox ID="txtPhone_Home" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhone_Home"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>هاتف العمل :
                                        </h5>
                                        <asp:TextBox ID="txtPhone_Work" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtPhone_Work"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>هاتف آخر :
                                        </h5>
                                        <asp:TextBox ID="txtPhone_Other" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtPhone_Other"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>                               
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>العنوان : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="txtAddress" ErrorMessage="* العنوان" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>صندوق البريد : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtBox_Email" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                            ControlToValidate="txtBox_Email" ErrorMessage="* صندوق البريد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>الرمز البريدي : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtSerial_Email" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="txtSerial_Email" ErrorMessage="* الرمز البريدي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>تاريخ الإنتساب : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDate_Rigstry" runat="server" class="form-control" Width="100" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="txtDate_Rigstry" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                        <h5>عضو عامل : 
                                        </h5>
                                        <asp:RadioButton ID="RBIs_Aamel" runat="server" GroupName="GAdow" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                        <h5>عضو منتسب : 
                                        </h5>
                                        <asp:RadioButton ID="RBIs_Montaseeb" runat="server" GroupName="GAdow" />
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="IDManager" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label2" runat="server" Text="يعبأ بمعرفة الجمعية "></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>قرر مجلس الجمعية في جلستة رقم  : 
                                        </h5>
                                        <asp:TextBox ID="txtNumber_Qarar" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="txtNumber_Qarar" ErrorMessage="* رقم الجلسة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtNumber_Qarar"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>تاريخ الجلسة : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDate_Qarar" runat="server" class="form-control" Width="180" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                                    ControlToValidate="txtDate_Qarar" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>قبول العضوية إعتباراً من : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDate_Qobol" runat="server" class="form-control" Width="180" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                                    ControlToValidate="txtDate_Qobol" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>رئيس مجلس الإدارة : 
                                        </h5>
                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                <asp:LinkButton ID="btnAdd" runat="server"  ValidationGroup="g2" style="margin-right:4px; font-size:medium" OnClick="btnAdd_Click"
                    class="btn btn-info">حفظ البيانات</asp:LinkButton>
                <asp:LinkButton ID="LB_Back" runat="server" style="margin-right:4px; font-size:medium" OnClick="LB_Back_Click"
                    class="btn btn-danger">خروج</asp:LinkButton>
            </div>
            <div style="width: 50%">
                
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

    <script src="../css/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

