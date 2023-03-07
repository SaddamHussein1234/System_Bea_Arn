<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="PageCompanyAdd.aspx.cs" Inherits="Cpanel_ERP_CRM_PageCompany_PageCompanyAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="/Default.aspx">الرئيسية</a></li>
                    <li><a href="PageCompany.aspx">قائمة الداعمين</a></li>
                    <li><a href="PageCompanyAdd.aspx">إضافة/تعديل </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل "></asp:Label>
                    </h3>
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
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>الفئة : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLType_Customer" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="شركات">شركات</asp:ListItem>
                                            <asp:ListItem Value="أفراد">أفراد</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="DLType_Customer" ErrorMessage="*  حدد الفئة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>نوع شركة الدعم : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlCompanyType" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="ddlCompanyType" ErrorMessage="* نوع شركة الدعم" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم التسجيل : <span class="required">*</span></h5>
                                        <asp:TextBox ID="txtRegistration_No" runat="server" class="form-control" ValidationGroup="g2" TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtRegistration_No" ErrorMessage="* رقم التسجيل" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtRegistration_No"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إسم الشركة : <span class="required">*</span></h5>
                                        <asp:TextBox ID="txtCompanyName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtCompanyName" ErrorMessage="* إسم الشركة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>حدد البلد : <span class="required">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="g2">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddlCountry" ErrorMessage="* حدد البلد" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>المدينة : <span class="required">*</span></h5>
                                        <asp:TextBox ID="txtCity" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtCity" ErrorMessage="* المدينة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>العنوان : <span class="required">*</span></h5>
                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control" ValidationGroup="g2" Text="0"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtAddress" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>البريد الإلكتروني : <span class="required">*</span>
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>الموقع الإلكتروني :
                                        </h5>
                                        <asp:TextBox ID="txtSite" runat="server" class="form-control" ValidationGroup="g2" Text="www.google.com"
                                            Style="direction: ltr"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtSite" ErrorMessage="* البريد الالكتروني" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>سنة التأسيس : </h5>
                                        <asp:TextBox ID="txtEstablished_Year" runat="server" class="form-control" ValidationGroup="g2" Text="0" TextMode="Number"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtEstablished_Year" ErrorMessage="* سنة التأسيس" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEstablished_Year"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الفاكس : </h5>
                                        <asp:TextBox ID="txtFax" runat="server" class="form-control" ValidationGroup="g2" Text="0"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="txtFax" ErrorMessage="* الفاكس" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم الجوال (رسائل SMS) : <span class="required">*</span></h5>
                                        <asp:TextBox ID="txtPhone_Number1" runat="server" class="form-control" ValidationGroup="g2" TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtPhone_Number1" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhone_Number1"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>هاتف ثابت : </h5>
                                        <asp:TextBox ID="txtMobile_Number1" runat="server" class="form-control" ValidationGroup="g2" TextMode="Number" Text="0"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtPhone_Number1" ErrorMessage="* هاتف ثابت" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMobile_Number1"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم الجوال 2 : </h5>
                                        <asp:TextBox ID="txtPhone_Number2" runat="server" class="form-control" ValidationGroup="g2" TextMode="Number" Text="0"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtPhone_Number2" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPhone_Number2"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>شعار الشركة : 
                                        </h5>
                                        <div class="col-md-12">
                                            <div id="divUploadPhoto" runat="server" class="divUploadPhoto" style="display: block;">
                                                <asp:FileUpload ID="fuPhoto" runat="server" data-toggle="tooltip" ToolTip="تحديد شعار الشركة" ValidationGroup="g2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>حالة التفعيل : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBIsActive" runat="server" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="font-size: medium"
                                            CssClass="btn btn-info" OnClick="btnAdd_Click" ValidationGroup="g2" />
                                        <asp:LinkButton ID="LBBack" runat="server" Style="font-size: medium" OnClick="LBBack_Click"
                                            CssClass="btn btn-danger">خروج</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
</asp:Content>

