<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelAttach/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSMSSetting.aspx.cs" Inherits="Cpanel_CPanelAttach_PageSMSSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <link href="../test/LoginAr.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageSMSSetting.aspx">إعدادات المزود</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إعدادات المزود"></asp:Label>
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>رابط المزود : </h5>
                                        <asp:TextBox ID="txt_Url" runat="server" class="form-control" ValidationGroup="g2" style="direction:ltr;"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txt_Url" ErrorMessage="* رابط المزود" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>إسم المستخدم : </h5>
                                        <asp:TextBox ID="txt_User_Name" runat="server" class="form-control" ValidationGroup="g2" style="direction:ltr;"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txt_User_Name" ErrorMessage="* إسم المستخدم" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_User_Name"
                                                ErrorMessage="* حروف انجليزي فقط" Font-Size="10px" ValidationExpression="^[a-zA-Z]+$" ValidationGroup="g2" Display="Dynamic">
                                            </asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>كلمة المرور : </h5>
                                        <asp:TextBox ID="txt_Pass" runat="server" class="form-control" type="Password"
                                            ValidationGroup="g2" style="direction:ltr;"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txt_Pass" ErrorMessage="* كلمة المرور" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>حالة التفعيل : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActive" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام الصلاحيات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveSetting" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام إدارة الموقع : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveSite" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام البحث الاجتماعي : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveSocialSearch" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام المستودع : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveWSM" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام أوامر الصرف : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveEOS" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام الزكاة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveZakat" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام الجمعية العمومية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveGeneralAssembly" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام الموارد البشرية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveHR" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام الاستثمار وتنمية الموارد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveCRM" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>إشعارات نظام الجمعية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActiveOM" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نعم/لا </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <br />
                                <div style="float: left">
                                    <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" OnClick="btnAdd_Click" Width="100" style="margin-left:5px"
                                        class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                                    <asp:LinkButton ID="LBBack" runat="server"
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

