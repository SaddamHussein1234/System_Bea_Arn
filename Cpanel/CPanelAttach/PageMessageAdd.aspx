<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelAttach/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMessageAdd.aspx.cs" Inherits="Cpanel_CPanelAttach_PageMessageAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <link href="../test/LoginAr.css" rel="stylesheet" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default" OnClick="LBBack_Click"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageMessageAdd.aspx">إضافة رسالة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة رسالة SMS"></asp:Label>
                    </h3>
                    <div style="float:left">
                            <asp:RadioButtonList ID="RBLFilter" runat="server" RepeatDirection="Horizontal" 
                                CssClass="left" AutoPostBack="true" OnSelectedIndexChanged="RBLFilter_SelectedIndexChanged">
                                <asp:ListItem Value="Other" Selected="True">رقم آخر</asp:ListItem>
                                <asp:ListItem Value="Admin">مستخدمين النظام</asp:ListItem>
                                <asp:ListItem Value="Mostafeed">المستفيدين</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>مزود الخدمة : </h5>
                                        <asp:DropDownList ID="DLURL" runat="server" Width="100%" ValidationGroup="g2"
                                            CssClass="form-control chzn-select dropdown">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="DLSenderName" ErrorMessage="* مزود الخدمة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إسم المرسل : </h5>
                                        <asp:DropDownList ID="DLSenderName" runat="server" Width="100%" ValidationGroup="g2" style="direction:ltr;"
                                            CssClass="form-control chzn-select dropdown">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="DLSenderName" ErrorMessage="* إسم المرسل" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" visible="false" id="IDAdmin">
                                    <div class="form-group">
                                        <h5>حدد المستخدم لجلب رقمه :
                                        </h5>
                                        <asp:DropDownList ID="DL_Admin" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control chzn-select dropdown"
                                            AutoPostBack="true" OnSelectedIndexChanged="DL_Admin_SelectedIndexChanged" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="DL_Admin" ErrorMessage="* حدد المستخدم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3"  runat="server" visible="false" id="IDMostafeed">
                                    <div class="form-group">
                                        <h5>حدد المستفيد لجلب رقمه : 
                                        </h5>
                                        <asp:DropDownList ID="DLMostafeed" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control chzn-select dropdown"
                                            AutoPostBack="true" OnSelectedIndexChanged="DLMostafeed_SelectedIndexChanged" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="DL_Admin" ErrorMessage="* حدد المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم الهاتف : </h5>
                                        <asp:TextBox ID="txt_Phone" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr;"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txt_Phone" ErrorMessage="* رقم الهاتف" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_Phone"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <div class="col-lg-10">
                                    <div class="form-group">
                                        <h5>نص الرسالة : </h5>
                                        <asp:TextBox ID="txt_Message" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txt_Message" ErrorMessage="* نص الرسالة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>نوع الرسالة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input runat="server" name="RememberMe" id="RArabic" type="radio" value="عربي" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عربي </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input runat="server" name="RememberMe" type="radio" id="REnglish" value="إنجليزي" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إنجليزي </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <br />
                                <div style="float: left">
                                    <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" OnClick="btnAdd_Click" Width="100" Style="margin-left: 5px"
                                        class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                                    <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click"
                                        class="btn btn-danger btn-fill pull-left">رجوع</asp:LinkButton>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </div>
    <script src="../css/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
</asp:Content>

