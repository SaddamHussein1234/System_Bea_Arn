<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeMandateAdd.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeMandateAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    <li><a href="PageEmployeeMandates.aspx">قائمة إنتداب الموظفين</a></li>
                    <li><a href="#">إضافة/تعديل إنتداب الموظفين </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title" style="float:right">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل إنتداب الموظفين"></asp:Label>
                    </h3>
                    <div align="left">
                        <label class="control-label">
                            الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                        </label>
                        <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true"
                            Width="100" ValidationGroup="g2" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
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
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                            رقم الإنتداب : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtNumberMandate" runat="server" CssClass="form-control" ValidationGroup="g2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" ControlToValidate="txtNumberMandate" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* رقم القرض" runat="server"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberMandate"
                                                ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                Display="Dynamic" Font-Size="10px">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                              حدد الإدارة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                             حدد الموظف :  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label> <span id="lblPhone" runat="server"></span>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                            عنوان العملية : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtTitle" MaxLength="20" runat="server" CssClass="form-control" ValidationGroup="g2"
                                                onkeypress="return Common.isNumericKey(event,this)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtTitle" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* عنوان العملية" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label" id="lblBasic" runat="server">
                                             مبلغ الإنتداب لكل يوم  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtAmount" MaxLength="10" runat="server" CssClass="form-control" ValidationGroup="g2"
                                                onkeyup="EmployeeMandateSave.CalculateInstallment()" onkeypress="return Common.isNumericKey(event,this);" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBasic" SetFocusOnError="true" ControlToValidate="txtAmount" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد المبلغ" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label" id="Label3" runat="server">
                                           عدد أيام الإنتداب <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlTotalDays" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2" onkeyup="EmployeeMandateSave.CalculateInstallment()" onkeypress="return Common.isNumberKey(event)">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1">01</asp:ListItem>
                                                <asp:ListItem Value="2">02</asp:ListItem>
                                                <asp:ListItem Value="3">03</asp:ListItem>
                                                <asp:ListItem Value="4">04</asp:ListItem>
                                                <asp:ListItem Value="5">05</asp:ListItem>
                                                <asp:ListItem Value="6">06</asp:ListItem>
                                                <asp:ListItem Value="7">07</asp:ListItem>
                                                <asp:ListItem Value="8">08</asp:ListItem>
                                                <asp:ListItem Value="9">09</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ControlToValidate="ddlTotalDays" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الأيام" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label" id="Label2" runat="server">
                                            من تاريخ : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtStartDate" runat="server" placeholder="من تاريخ ... " class="form-control"
                                                data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtStartDate" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label" id="Label1" runat="server">
                                            إلى تاريخ : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtEndDate" runat="server" placeholder="إلى تاريخ ... " class="form-control"
                                                data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="txtEndDate" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <label class="control-label">
                                            مزيد من التفاصيل :
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtDescrption" runat="server" TextMode="MultiLine" Rows="6"
                                                CssClass="form-control" ValidationGroup="g2" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">
                                                إرسال إشعار نصي <i class="fa fa-envelope"></i> : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="DLSend" runat="server" ValidationGroup="g2"
                                                CssClass="form-control2 chzn-select dropdown" Width="100%" >
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="Yes">نعم أرسل</asp:ListItem>
                                                <asp:ListItem Value="No">لا تقم بالإرسل</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                                ControlToValidate="DLSend" ErrorMessage="* حدد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label4" runat="server" Text="المشرف المختص"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                             مدير الجمعية :  <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                            <label class="switch">
                                                <input runat="server" id="RB_Moder" type="radio" name="cars" onchange="show2()" checked />
                                                <span class="slider round"></span>
                                            </label> 
                                        </h5>
                                        <div id="pnlModer" style="display: none; <%= FCheck("_Moder") %>" class="col-md-12"><br />
                                            <asp:DropDownList ID="DLModer" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" ControlToValidate="DLModer" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* مدير الجمعية" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                             رئيس مجلس الإدارة :  <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                            <label class="switch">
                                                <input runat="server" type="radio" id="RB_Raees" name="cars" onchange="show(this.value)" />
                                                <span class="slider round"></span>
                                            </label> 
                                        </h5>
                                        <script type="text/javascript">
                                            function show(str) {
                                                document.getElementById('pnlModer').style.display = 'none';
                                                document.getElementById('pnlRaees').style.display = 'block';
                                            }
                                            function show2(sign) {
                                                document.getElementById('pnlModer').style.display = 'block';
                                                document.getElementById('pnlRaees').style.display = 'none';
                                            }
                                        </script>
                                        <div id="pnlRaees" style="display: none; <%= FCheck("_Raees") %>" class="col-md-12"><br />
                                            <asp:DropDownList ID="DLIDRaees" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" ControlToValidate="DLIDRaees" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد مجلس الإدارة" runat="server"></asp:RequiredFieldValidator>
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
            <div align="left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="font-size: medium"
                    class="btn btn-info" OnClick="btnAdd_Click" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="font-size: medium" OnClick="LBBack_Click"
                    class="btn btn-danger">خروج</asp:LinkButton>
            </div>
            <br />
            <br />
        </div>
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
        <asp:HiddenField ID="HFPhone" runat="server" />
        <asp:HiddenField ID="HFEmail" runat="server" />
</asp:Content>

