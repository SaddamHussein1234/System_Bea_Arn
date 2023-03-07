<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMessageVisitView.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageMessageVisitView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnOK.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <style type="text/css">
        
        @media all and (max-width:1680px) and (min-width:780px){
            .DetailsMessage
        {
            padding:7px 30px 0 0;
        }
        }
        @media all and (max-width:780px) and (min-width:150px)
        {
            .DetailsMessage
        {
            padding:3px 15px 0 0;
        }
        }
    </style>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث">
                    <li class="fa fa-refresh"></li></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageMassageVisit.aspx">رسائل الزوار </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>تفاصيل الرسالة
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive" style="padding: 10px 15px 0 10px">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <h5 style='color:#287c04'><span class="fa fa-paperclip"></span> غرض الرسالة : </h5>
                                    <p class="DetailsMessage">
                                        <asp:Label ID="lblTypeMessage" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <h5 style='color:#287c04'><span class="fa fa-paperclip"></span> عنوان الرسالة : </h5>
                                    <p class="DetailsMessage">
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <h5 style='color:#287c04''><span class="fa fa-user"></span> إسم الزائر : </h5>
                                    <p class="DetailsMessage">
                                        <asp:Label ID="lblNameUser" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <h5 style='color:#287c04''><span class="fa fa-location-arrow"></span> البلد : </h5>
                                    <p class="DetailsMessage">
                                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5 style='color:#287c04''><span class="fa fa-mail-forward"></span> البريد الالكتروني : </h5>
                                    <p class="DetailsMessage">
                                        <a runat="server" id="IDEmail" data-tooltip="مراسلة" target="_blank">
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </a>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5 style='color:#287c04''><span class="fa fa-phone"></span> الهاتف : </h5>
                                    <p class="DetailsMessage">
                                        <a runat="server" id="IDPhone" data-tooltip="إتصال" target="_blank">
                                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                        </a>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h5 style='color:#287c04''><span class="fa fa-calendar"></span> تاريخ الرسالة : </h5>
                                    <p class="DetailsMessage">
                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <h5 style='color:#287c04''><span class="fa fa-envelope"></span> نص الرسالة : </h5>
                                    <p class="DetailsMessage">
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div align="left">
                                    <asp:Button ID="btnOK" runat="server" CssClass="btn btn-info" Text=" حسناً "
                                        OnClick="btnEdit_Click" />
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
            <br />
            <br />
            <br />
            <br />
            <br />
</asp:Content>

