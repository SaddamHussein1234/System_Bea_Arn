<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSettingInfo.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageSettingInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

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
                    <li><a href="PageSettingInfo.aspx">إعدادات البيانات</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إعدادات البيانات"></asp:Label>
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
                                <div class="container-fluid" dir="rtl">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <h5>رابط الفيسبوك : </h5>
                                            <asp:TextBox ID="txtFacebook" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <h5>رابط يوتيوب : </h5>
                                            <asp:TextBox ID="txtYouTube" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <h5>رابط تويتر : </h5>
                                            <asp:TextBox ID="txtTweter" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <h5>رابط جوجل بلس : </h5>
                                            <asp:TextBox ID="txtGooglePlus" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <h5>رابط الموقع : </h5>
                                            <asp:TextBox ID="txtSite" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <h5>بريد الموقع : </h5>
                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5>رقم الهاتف : </h5>
                                            <asp:TextBox ID="txtPhone" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5>اقصى عدد للسلايد : </h5>
                                            <asp:TextBox ID="txtSlide" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr" MaxLength="1"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSlide"
                                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5>اقصدى عدد لاهم الاخبار : </h5>
                                            <asp:TextBox ID="txtNews" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr" MaxLength="1"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNews"
                                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5>اقصدى عدد لاهداف المؤسسه : </h5>
                                            <asp:TextBox ID="txtComp" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr" MaxLength="1"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtComp"
                                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <h5>موقع المؤسسة عربي : </h5>
                                            <asp:TextBox ID="txtLocation" style="direction:ltr" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="container-fluid">
                                    <div class="WidthText2">
                                        <div class="form-group">
                                            <h5>موقع المؤسسة تركي : </h5>
                                            <asp:TextBox ID="txtLocationTr" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="WidthText2">
                                        <div class="form-group">
                                            <h5>موقع المؤسسة إنجليزي : </h5>
                                            <asp:TextBox ID="txtLocationEn" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="container-fluid" style="text-align:left">
                                <br />
                                 <div style="float: left">
                                     <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" OnClick="btnAdd_Click" Width="100" style="margin-left:5px"
                                                class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                                            <asp:LinkButton ID="LBBack" runat="server"
                                                class="btn btn-danger btn-fill pull-left">رجوع</asp:LinkButton>
                                 </div>
                                <br />
                                <br /><br /><br /><br /><br />
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

