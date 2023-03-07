<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAdd.aspx.cs" Inherits="Cpanel_ERP_OM_Performance_Index_PageAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
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
                    <a href="PageAdd.aspx" class="btn btn-primary">
                        <i class="fa fa-plus"></i>
                    </a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default" OnClick="LBExit_Click">
                     <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageAdd.aspx">إضافة بطاقة مؤشر الأداء</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="1 - معلومات الموظف"></asp:Label>
                    </h3>
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>حدد الموظف : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAdmin" runat="server" ValidationGroup="GBill" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                            ControlToValidate="DLAdmin" ErrorMessage="* حدد التغذية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>  
                                <div class="col-lg-4">
                                    <div class="form-group">
                                    <h5>
                                        رقم بطاقة المؤشر : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtNumberBill" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtNumberBill" ErrorMessage="* رقم المؤشر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberBill"
                                            Font-Size="11px" ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GBill"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>تاريخ بطاقة المؤشر : <span style="color: red">*</span>
                                        </h5>
                                        <div class="input-group date " >
                                            <asp:TextBox ID="txtDateAdd" runat="server" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="GBill" Style="direction: ltr"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                ControlToValidate="txtDateAdd" ErrorMessage="* التاريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label2" runat="server" Text="2 - الربط مع بطاقة الأداء"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                    <h5>
                                        المنظور (BSC) : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtBSC" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtBSC" ErrorMessage="* المنظور" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                    <h5>
                                        المحور الرئيسي (KRA) : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtKRA" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator21" runat="server"
                                            ControlToValidate="txtKRA" ErrorMessage="* المحور الرئيسي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label3" runat="server" Text="3 - معلومات الهدف الإستراتيجي"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                    <h5>
                                        رمز الهدف الإستراتيجي : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtStrategic_Goal_Icon" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtStrategic_Goal_Icon" ErrorMessage="* رمز الهدف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                    <h5>
                                        نص الهدف الإستراتيجي : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtStrategic_Goal_text" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtStrategic_Goal_text" ErrorMessage="* نص الهدف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label4" runat="server" Text="4 - الإدارات المسؤولة عن التنفيذ"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtManagement" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtManagement" ErrorMessage="* الإدارات" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                        <asp:Label ID="Label5" runat="server" Text="5 - معلومات المؤشر"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>
                                        رمز المؤشر : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtpointer_Icon" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtpointer_Icon" ErrorMessage="* رمز المؤشر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-9">
                                    <div class="form-group">
                                    <h5>
                                        المؤشر : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtpointer" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtpointer" ErrorMessage="* المؤشر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>
                                        مالك المؤشر : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtPointer_Owner" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="txtPointer_Owner" ErrorMessage="* مالك المؤشر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>
                                        وحدة القياس : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtMeasruing_Unit" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="txtMeasruing_Unit" ErrorMessage="* وحدة القياس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>
                                        خط الأساس : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtBaseline" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                            ControlToValidate="txtBaseline" ErrorMessage="* خط الأساس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>
                                        القطبية : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtPolar" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="txtPolar" ErrorMessage="* القطبية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>
                                        دورية القياس : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtMeasurement_Periodicity" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="txtMeasurement_Periodicity" ErrorMessage="* دورية القياس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                    <h5>
                                        التراكمية : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtCumulative" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                            ControlToValidate="txtCumulative" ErrorMessage="* التراكمية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                    <h5>
                                        القيمة المرجعية : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtReference_Value" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                            ControlToValidate="txtReference_Value" ErrorMessage="* القيمة المرجعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                    <h5>
                                        الغرض من القياس : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtPurpose_of_the_Measurement" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="txtPurpose_of_the_Measurement" ErrorMessage="* الغرض من القياس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label6" runat="server" Text="6 - معادلة صيغة المؤشر"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="txtPointer_Formula_Equation_One" runat="server" ValidationGroup="GBill" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                                ControlToValidate="txtPointer_Formula_Equation_One" ErrorMessage="* حدد القيمة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-lg-9"><div class="form-group"><hr style='border: solid; border-width: 1px; width: 100%' /></div></div>
                                        <div class="col-lg-3"><div class="form-group"><h5 style="margin:10px;">100 X</h5></div></div>
                                        <div class="col-lg-12">
                                            <asp:TextBox ID="txtPointer_Formula_Equation_Two" runat="server" ValidationGroup="GBill" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                                ControlToValidate="txtPointer_Formula_Equation_Two" ErrorMessage="* حدد القيمة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label7" runat="server" Text="7 - الهدف (المستوى المستهدف)"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <h5>حدد النسبة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLTarget" runat="server" ValidationGroup="GBill" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="86 % أو أكثر">86 % أو أكثر</asp:ListItem>
                                            <asp:ListItem Value="71 % - 85 %">71 % - 85 %</asp:ListItem>
                                            <asp:ListItem Value="70 %">70 %</asp:ListItem>
                                            <asp:ListItem Value="60 % - 69 %">60 % - 69 %</asp:ListItem>
                                            <asp:ListItem Value="59 % أو أقل">59 % أو أقل</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator19" runat="server"
                                            ControlToValidate="DLTarget" ErrorMessage="* حدد النسبة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="container-fluid col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label8" runat="server" Text="8 - مصدر البيانات"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtData_Source" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label9" runat="server" Text="9 - الشواهد المرفقة"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtAttached_Evidence" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label10" runat="server" Text="10 - الملاحظات"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNote" runat="server" ValidationGroup="GBill" CssClass="form-control"></asp:TextBox>
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
                        <asp:Label ID="lbmsg" runat="server" Text="لجنة القبول"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3" runat="server">
                                    <div class="form-group">
                                        <h5>مسؤول القياس : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLMeasurement_Officer" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                            ControlToValidate="DLMeasurement_Officer" ErrorMessage="* مسؤول القياس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3" runat="server">
                                    <div class="form-group">
                                        <h5>مسؤول التنفيذ : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLImplementation_Officer" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator22" runat="server"
                                            ControlToValidate="DLImplementation_Officer" ErrorMessage="* مسؤول التنفيذ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3" runat="server">
                                    <div class="form-group">
                                        <h5>مدير الجمعية : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLGeneral_Director" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown"
                                            Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator131" runat="server"
                                            ControlToValidate="DLGeneral_Director" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" align="left">
                                        <br />
                                        <asp:LinkButton ID="LBNew" runat="server"  ValidationGroup="GBill" OnClick="LBNew_Click"
                                            class="btn btn-info">حفظ البيانات</asp:LinkButton>
                                        <asp:LinkButton ID="LB_Back" runat="server"  OnClick="LBExit_Click"
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
</asp:Content>

