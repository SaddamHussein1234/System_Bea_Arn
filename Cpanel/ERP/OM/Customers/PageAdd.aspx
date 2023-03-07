<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAdd.aspx.cs" Inherits="Cpanel_ERP_OM_Customers_PageAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        function OnTextKeyUp(txt) {
            document.getElementById("<%=txtEmail.ClientID %>").value = txt.value + "@gmail.com";
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
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClientClick="return insertConfirmation();" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        OnClick="LBExit_Click" OnClientClick="return insertConfirmation();" CssClass="btn btn-default">
                     <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageAdd.aspx">إضافة/تعديل</a></li>
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
                                        <h5>الارشيف :
                                        </h5>
                                        <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="VGCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="ddlYears" ErrorMessage="* الارشيف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="VGCustomer" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>رقم التسجيل : <i style="color: red">*</i></h5>
                                        <asp:TextBox ID="txtRegistration_No" runat="server" class="form-control" ValidationGroup="VGCustomer"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="txtRegistration_No" ErrorMessage="* رقم التسجيل" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الإسم الأول : <i style="color: red">*</i></h5>
                                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control" ValidationGroup="VGCustomer" MaxLength="70"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtFirstName" ErrorMessage="* الإسم الأول" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الإسم الأخير : <i style="color: red">*</i></h5>
                                        <asp:TextBox ID="txtFamilyName" runat="server" class="form-control" ValidationGroup="VGCustomer" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtFamilyName" ErrorMessage="* الإسم الأخير" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>تاريخ الإضافة : <span style="color: red">*</span>
                                        </h5>
                                        <div class="input-group date " style="margin-right: -10px">
                                            <asp:TextBox ID="txtAdd" runat="server" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGCustomer" Style="direction: ltr"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                                ControlToValidate="txtAdd" ErrorMessage="* تاريخ الإضافة" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الوظيفة / الجهة : </h5>
                                        <asp:TextBox ID="txtOrganization" runat="server" class="form-control" ValidationGroup="VGCustomer" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>المدينة : </h5>
                                        <asp:TextBox ID="txtCity" runat="server" class="form-control" ValidationGroup="VGCustomer" MaxLength="512"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtCity" ErrorMessage="* المدينة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم الجوال : <i style="color: red">*</i></h5>
                                        <asp:TextBox ID="txtPhone" runat="server" class="form-control" ValidationGroup="VGCustomer" onkeyup="OnTextKeyUp(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtPhone" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>البريد الالكتروني : <i style="color: red">*</i></h5>
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" ValidationGroup="VGCustomer" Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="* إسم المستخدم" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCustomer" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                            ControlToValidate="txtEmail" ErrorMessage="* بريد خاطئ" Font-Size="11px" Display="Dynamic"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ForeColor="Red" ValidationGroup="VGCustomer"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <nav class="navbar-dark bg-dark">
                                <div align="center">
                                    <a class="btn navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                                        <span class="fa fa-list"></span>&nbsp;عرض المزيد
                                    </a>
                                </div>
                            </nav>
                            <div class="collapse" id="navbarToggleExternalContent" style="background-color: #ecedec">
                                <div class="bg-dark p-4" style="padding: 5px 5px 10px 0">
                                    <div class="container-fluid" dir="rtl">
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <h5>الرقم المباشر : </h5>
                                                <asp:TextBox ID="txtDirect_Number" runat="server" class="form-control" Style="direction: ltr" ValidationGroup="VGCustomer" MaxLength="20" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <h5>رقم المكتب : </h5>
                                                <asp:TextBox ID="txtOffice_Number" runat="server" class="form-control" Style="direction: ltr" ValidationGroup="VGCustomer" MaxLength="20" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <h5>العنوان الأول : </h5>
                                                <asp:TextBox ID="txtAddress_line_1" runat="server" class="form-control" ValidationGroup="VGCustomer" MaxLength="512"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <h5>العنوان الثاني : </h5>
                                                <asp:TextBox ID="txtAddress_line_2" runat="server" class="form-control" ValidationGroup="VGCustomer" MaxLength="512"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" dir="rtl">
                            <div class="col-lg-10">
                                <div class="form-group">
                                    <h5>ملاحظة : </h5>
                                    <asp:TextBox ID="txtNote" runat="server" class="form-control" ValidationGroup="VGCustomer" MaxLength="1024" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <br />
                                    <div align="center">
                                        <asp:LinkButton ID="LBNew" runat="server" Style="margin-right: 4px;"
                                            OnClientClick="return insertConfirmation();" OnClick="LBNew_Click"
                                            class="btn btn-success btn-fill pull-left" ValidationGroup="VGCustomer">تحديث البيانات</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

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
    </script>
    <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

