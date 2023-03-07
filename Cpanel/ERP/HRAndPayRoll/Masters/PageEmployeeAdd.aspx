<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeAdd.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeAdd" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <script type="text/javascript">
        function ValidateWorkingDays(sender, args) {
            var checkBoxList = document.getElementById("<%=chkListWorkingDays.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
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
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVEmployeeAttachment.ClientID%>");
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

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

    <style type="text/css">
        .required{color:red}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployee.aspx">قائمة الموظفين</a></li>
                    <li><a href="PageEmployeeAdd.aspx">إضافة/تعديل موظف </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
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
        </div>
        <asp:Panel ID="pnl_colums" runat="server">
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل الموظف - المعلومات الشخصية"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>ربطة بالمستخدم : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAdmin" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                            ControlToValidate="DLAdmin" ErrorMessage="* حدد المستخدم" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>الإسم الأول : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtFirstName" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>إسم الأب : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtMiddleName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtMiddleName" ErrorMessage="* إسم الأب" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>إسم الجد : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtLastName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtLastName" ErrorMessage="* إسم الجد" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>إسم العائلة : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtFatherName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtFatherName" ErrorMessage="* إسم العائلة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>تاريخ الإضافة : <span class="required">*</span>
                                        </h5>
                                            <div class="input-group date ">
                                                <asp:TextBox ID="txtDateAdd" runat="server" placeholder="تاريخ الإضافة ... " class="form-control"
                                                    data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator22" runat="server"
                                                ControlToValidate="txtDateAdd" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>تاريخ الميلاد : <span class="required">*</span>
                                        </h5>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtBirthDate" runat="server" placeholder="تاريخ الميلاد ... " class="form-control"
                                                 data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align: center;"></asp:TextBox>
                                            <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtBirthDate" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>الجنس : <span class="required">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtnMale" runat="server" Text="_ذكر" GroupName="Gender" Checked="true" />
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtnFeMale" runat="server" Text="_أنثى" GroupName="Gender" />
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>الحالة الإجتماعية : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlMaratialStatus" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="Married">متزوج</asp:ListItem>
                                            <asp:ListItem Value="Un-Married">عازب</asp:ListItem>
                                            <asp:ListItem Value="Widowed">أرمله</asp:ListItem>
                                            <asp:ListItem Value="Divorced">مطلقة</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="ddlMaratialStatus" ErrorMessage="* الحالة الإجتماعية" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>المظهر :
                                        </h5>
                                        <asp:TextBox ID="txtCast" runat="server" class="form-control" ValidationGroup="g2" Text="لائق"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5>حالة التفعيل : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBFork" runat="server" checked="checked" />
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
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="المعلومات الرسمية"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>نوع طبيعة العمل : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="ddlMaratialStatus" ErrorMessage="* حدد البيانات" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>ينتمي إلى إدارة : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="ddlDepartment" ErrorMessage="* حدد البيانات" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>الوظيفة المناسبة : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="ddlDesignation" ErrorMessage="* حدد البيانات" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>فصيلة الدم : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlEmployeeGrade" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="ddlEmployeeGrade" ErrorMessage="* حدد البيانات" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>تاريخ الإنضمام : <span class="required">*</span>
                                        </h5>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtJoinDate" runat="server" placeholder="تاريخ الإنظمام ... " class="form-control"
                                                data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align: center;"></asp:TextBox>
                                            <asp:Label ID="Label2" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                            ControlToValidate="txtJoinDate" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>رقم التوظيف : 
                                        </h5>
                                        <asp:TextBox ID="txtPFNumber" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="txtPFNumber" ErrorMessage="* رقم التوظيف" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>أوقات الدوام : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlShift" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="ddlShift" ErrorMessage="* حدد البيانات" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>                               
                                <div runat="server" id="divViewPhoto" class="col-md-1" style="display: none;">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfPhoto" runat="server" />
                                                <img id="imgPhoto" runat="server" alt="Photo" class="viewImage" width="70" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <h5>أيام الدوام للموظف : <span class="required">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:CheckBoxList runat="server" ID="chkListWorkingDays" RepeatColumns="7" Width="90%"
                                                ValidationGroup="g2" RepeatDirection="Horizontal" CssClass="form-control2">
                                                <%--<asp:ListItem Value="Sunday" Selected="True">الأحد</asp:ListItem>
                                                <asp:ListItem Value="Monday" Selected="True">الأثنين</asp:ListItem>
                                                <asp:ListItem Value="Tuesday" Selected="True">الثلاثاء</asp:ListItem>
                                                <asp:ListItem Value="Wednesday" Selected="True">الأربعاء</asp:ListItem>
                                                <asp:ListItem Value="Thursday" Selected="True">الخميس</asp:ListItem>
                                                <asp:ListItem Value="Friday">الجمعة</asp:ListItem>
                                                <asp:ListItem Value="Saturday">السبت</asp:ListItem>--%>
                                                <asp:ListItem Value="الأحد" Selected="True">الأحد</asp:ListItem>
                                                <asp:ListItem Value="الإثنين" Selected="True">الإثنين</asp:ListItem>
                                                <asp:ListItem Value="الثلاثاء" Selected="True">الثلاثاء</asp:ListItem>
                                                <asp:ListItem Value="الأربعاء" Selected="True">الأربعاء</asp:ListItem>
                                                <asp:ListItem Value="الخميس" Selected="True">الخميس</asp:ListItem>
                                                <asp:ListItem Value="الجمعة">الجمعة</asp:ListItem>
                                                <asp:ListItem Value="السبت">السبت</asp:ListItem>
                                            </asp:CheckBoxList>
                                            <asp:CustomValidator ID="cvWorkingDays" ErrorMessage="* يرجى تحديد أيام الدوام " ValidationGroup="g2"
                                                ClientValidationFunction="ValidateWorkingDays" runat="server" Display="Dynamic" CssClass="required" />
                                        </div>
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
                        <asp:Label ID="Label3" runat="server" Text="معلومات الاتصال"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>حدد البلد : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                            Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                            ControlToValidate="ddlCountry" ErrorMessage="* حدد البيانات" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>المحافظة : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlState" runat="server"
                                            Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                            ControlToValidate="ddlState" ErrorMessage="* حدد البيانات" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>المدينة : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtCity" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="txtCity" ErrorMessage="* المدينة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>العنوان : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                            ControlToValidate="txtAddress" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>رمز البلد : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtPinCode" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="txtPinCode" ErrorMessage="* رمز البلد" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>رقم الهاتف : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtMobile" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator19" runat="server"
                                            ControlToValidate="txtMobile" ErrorMessage="* رقم الهاتف" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobile"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>هاتق آخر : 
                                        </h5>
                                        <asp:TextBox ID="txtPhone" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>البريد الالكتروني : <span class="required">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" ValidationGroup="g2"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator34" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="* البريد الالكتروني" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            ControlToValidate="txtEmail" Font-Size="10px" ErrorMessage="بريد خطأ"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ForeColor="Red" ValidationGroup="g2" Display="Dynamic"></asp:RegularExpressionValidator>
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
                        <asp:Label ID="Label4" runat="server" Text="معلومات الحساب البنكي"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>اسم البنك : 
                                        </h5>
                                        <asp:TextBox ID="txtBankName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>إسم الفرع : 
                                        </h5>
                                        <asp:TextBox ID="txtBranchName" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>إسم صاحب الحساب : 
                                        </h5>
                                        <asp:TextBox ID="txtAccountName" MaxLength="150" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>رقم حساب : 
                                        </h5>
                                        <asp:TextBox ID="txtAccountNumber" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </asp:Panel>
        <div class="container-fluid" runat="server" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label5" runat="server" Text="مزيد من التفاصيل"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>Others : 
                                        </h5>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>Emergency Leave : 
                                        </h5>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>Sick Leave : 
                                        </h5>
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>Causal Leave : 
                                        </h5>
                                        <input type="text" class="form-control" />
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
                        <asp:Label ID="Label6" runat="server" Text="وثائق الموظف"></asp:Label>
                    </h3>
                    <div style="float: left">
                        <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" title="حذف الملفات المحددة" OnClick="btnDelete_Click"
                            data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" Visible="false" />
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <asp:Panel ID="pnl_colums2" runat="server">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>الصورة الشخصية : 
                                        </h5>
                                        <div class="col-md-12">
                                            <div id="divUploadPhoto" runat="server" class="divUploadPhoto" style="display: block;">
                                                <img src="/Img/loader.gif" id="img_Photo" runat="server" alt="Photo" class="viewImage" width="70" />
                                                <asp:FileUpload ID="fuPhoto" runat="server" data-toggle="tooltip" ToolTip="تحديد الصورة الشخصية" ValidationGroup="g2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>صورة التوقيع : 
                                        </h5>
                                        <div class="col-md-12">
                                            <img src="/Img/loader.gif" id="img_Signature" runat="server" alt="Photo" class="viewImage" width="70" />
                                            <asp:FileUpload ID="FAddImgSignature" runat="server" data-toggle="tooltip" ToolTip="تحديد توقيع الموظف" ValidationGroup="g2" />
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                </asp:Panel>
                                <hr />
                                <asp:Panel ID="pnlAdd" runat="server" Visible="False">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5>
                                                <asp:Label ID="lblResume" runat="server" Text="السيرة الذاتية"></asp:Label> : 
                                            </h5>
                                            <div class="col-md-12">
                                                <div id="divUploadResume" runat="server" class="divUploadResume" style="display: block;">
                                                    <asp:FileUpload ID="fuResume" runat="server" ValidationGroup="g2" />
                                                </div>
                                                <div id="divViewResume" runat="server" class="divViewResume" style="display: none;">
                                                    <asp:HiddenField ID="hfResume" runat="server" />
                                                    <a id="btnViewResume" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                    <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewResume','.divUploadResume');"><i class="fa fa-trash" title="Click to delete resume."></i>&nbsp;Delete</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5>
                                                <asp:Label ID="lblOfferLetter" runat="server" Text="رسالة العرض"></asp:Label> : 
                                            </h5>
                                            <div class="col-md-12">
                                                <div id="divUploadOfferLetter" runat="server" class="divUploadOfferLetter" style="display: block;">
                                                    <asp:FileUpload ID="fuOfferLetter" runat="server" ValidationGroup="g2" />
                                                </div>
                                                <div id="divViewOfferLetter" runat="server" class="divViewOfferLetter" style="display: none;">
                                                    <asp:HiddenField ID="hfOfferLetter" runat="server" />
                                                    <a id="btnViewOfferLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                    <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewOfferLetter','.divUploadOfferLetter');"><i class="fa fa-trash" title="Click to delete offer letter."></i>&nbsp;Delete</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5>
                                                <asp:Label ID="lblJoiningLetter" runat="server" Text="خطاب الانضمام"></asp:Label> : 
                                            </h5>
                                            <div class="col-md-12">
                                                <div id="divUploadJoiningLetter" runat="server" class="divUploadJoiningLetter" style="display: block;">
                                                    <asp:FileUpload ID="fuJoiningLetter" runat="server" ValidationGroup="g2" />
                                                </div>
                                                <div id="divViewJoiningLetter" runat="server" class="divViewJoiningLetter" style="display: none;">
                                                    <asp:HiddenField ID="hfJoiningLetter" runat="server" />
                                                    <a id="btnViewJoiningLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                    <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewJoiningLetter','.divUploadJoiningLetter');"><i class="fa fa-trash" title="Click to delete joining letter."></i>&nbsp;Delete</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5>
                                                <asp:Label ID="lblContractPaper" runat="server" Text="ورقة العقد"></asp:Label> : 
                                            </h5>
                                            <div class="col-md-12">
                                                <div id="divUploadContractPaper" runat="server" class="divUploadContractPaper" style="display: block;">
                                                    <asp:FileUpload ID="fuContractPaper" runat="server" ValidationGroup="g2" />
                                                </div>
                                                <div id="divViewContractPaper" runat="server" class="divViewContractPaper" style="display: none;">
                                                    <asp:HiddenField ID="hfContractPaper" runat="server" />
                                                    <a id="btnViewContractPaper" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                    <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewContractPaper','.divUploadContractPaper');"><i class="fa fa-trash" title="Click to delete contract paper."></i>&nbsp;Delete</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5>
                                                <asp:Label ID="lblIDProff" runat="server" Text="إثبات الهوية"></asp:Label> : 
                                            </h5>
                                            <div class="col-md-12">
                                                <div id="divUploadIDProff" runat="server" class="divUploadIDProff" style="display: block;">
                                                    <asp:FileUpload ID="fuIDProff" runat="server" ValidationGroup="g2" />
                                                </div>
                                                <div id="divViewIDProff" runat="server" class="divViewIDProff" style="display: none;">
                                                    <asp:HiddenField ID="hfIDProff" runat="server" />
                                                    <a id="btnViewIDProff" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                    <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewIDProff','.divUploadIDProff');"><i class="fa fa-trash" title="Click to delete id proff."></i>&nbsp;Delete</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h5> 
                                                <asp:Label ID="lblOtherDocument" runat="server" Text="وثائق آخرى"></asp:Label> : 
                                            </h5>
                                            <div class="col-md-12">
                                                <div id="divUploadOtherDocument" runat="server" class="divUploadOtherDocument" style="display: block;">
                                                    <asp:FileUpload ID="fuOtherDocument" runat="server" ValidationGroup="g2" />
                                                </div>
                                                <div id="divViewOtherDocument" runat="server" class="divViewOtherDocument" style="display: none;">
                                                    <asp:HiddenField ID="hfOtherDocument" runat="server" />
                                                    <a id="btnViewOtherDocument" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                    <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewOtherDocument','.divUploadOtherDocument');"><i class="fa fa-trash" title="Click to delete other document."></i>&nbsp;Delete</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlEdit" runat="server" Visible="False">
                                    <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <h5>عنوان الملف : 
                                                </h5>
                                                <asp:TextBox ID="txt_Name_Edit" runat="server" class="form-control" ValidationGroup="g2Edit"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                    ControlToValidate="txt_Name_Edit" ErrorMessage="* عنوان الملف" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2Edit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <h5>حدد الملف : <span class="required">*</span>
                                                </h5>
                                                <div class="col-md-12">
                                                    <div>
                                                        <asp:FileUpload ID="FUEdit" runat="server" data-toggle="tooltip" ToolTip="حدد الملف ... " ValidationGroup="g2Edit" />
                                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator21" runat="server"
                                                            ControlToValidate="FUEdit" ErrorMessage="* عنوان الملف" ForeColor="#FF0066"
                                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2Edit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnEdit" runat="server" Text="رفع الملف" Style="font-size: medium"
                                                    CssClass="btn btn-info" OnClick="btnEdit_Click" ValidationGroup="g2Edit" />
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GVEmployeeAttachment" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeAttachmentMapID"
                                                Width="100%" CssClass="footable1"
                                                EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-Width="10px">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Name" HeaderText="عنوان الملف" SortExpression="Name" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="المرفقات" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNullFile(Eval("AttachmentName").ToString()) %>"
                                                                href="/<%# Eval("AttachmentName") %>" data-file="pdf" data-fancybox data-type="iframe" title="عرض الملف" data-toggle="tooltip">
                                                                <i class="fas fa-file-pdf"></i>
                                                                <div>
                                                                    <span>عرض الملف </span><small><%# ClassSaddam.FGetTypeFileOutTitle(".pdf") %> </small>
                                                                </div>
                                                            </a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="تاريخ الإضافة" SortExpression="CreatedDate" HeaderStyle-ForeColor="#CCCCCC" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي " PreviousPageText=" السابق - " />
                                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>
                                        </div>
                                        <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                        <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                        <span class="fa fa-table"></span>

                                    </asp:Panel>
                                </asp:Panel>
                                <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                    <br />
                                    <br />
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
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div align="left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="font-size: medium"
                    class="btn btn-info" OnClick="btnAdd_Click" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="font-size: medium" OnClick="LBBack_Click"
                    class="btn btn-danger">خروج</asp:LinkButton>
            </div>
            <br />
            <br />
        </div>
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
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
        <link href="../../DMS/jquery.fancybox.min.css" rel="stylesheet" />
        <script src="../../DMS/jquery.fancybox.min.js"></script>
</asp:Content>

