<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelSetting/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_CPanelSetting_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <%--<div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px; background-color: #8b0101; color: #f5f5f5">
                    <h3 style="font-family: 'Alwatan';">نسخة تجريبية
                    </h3>
                </div>
            </div>--%>
            <div class="col-sm-12">
                <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                    <span class="badge badge-pill badge-warning">تحذير</span>
                    <span>يرجى تفعيل المصادقة الثنائية لزيادة حماية حسابك 
                        <a href="<%=ResolveUrl("~/Cpanel/CHome/PageGoogleAuthenticator.aspx")%>">من هنا ... </a>
                    </span>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </div>
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">نظام الإعدادات والصلاحيات - System Settings And Permissions
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="Default.aspx" data-toggle="tooltip" title="الرئيسية">
                        <div class="tile">
                            <div class="tile-heading">
                                الرئيسية <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-home"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="PageSetting.aspx" data-toggle="tooltip" title="إعدادات النظام" id="IDSetting" runat="server" visible="false">
                        <div class="tile">
                            <div class="tile-heading">
                                إعدادات النظام <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-gears" id="faSetting" runat="server"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="PageGroup.aspx" data-toggle="tooltip" title="إدارة المجموعات" id="IDGroup" runat="server" visible="false">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة المجموعات <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-group" id="faGroup" runat="server"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="PageAdmin.aspx" data-toggle="tooltip" title="إدارة المستخدمين" id="IDAdmin" runat="server" visible="false">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة المستخدمين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-user-plus" id="faAdmin" runat="server"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="PageGroupAdmin.aspx" data-toggle="tooltip" title="إدارة مجموعات المستفيدين" id="IDGroupAdmin" runat="server" visible="false">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة مجموعات المستفيدين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-group" id="I1" runat="server"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="../CHome/" data-toggle="tooltip" title="رجوع للبوابة الإلكترونية">
                        <div class="tile">
                            <div class="tile-heading">
                                رجوع للبوابة الإلكترونية <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-share"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="LogOut.aspx" data-toggle="tooltip" title="خروج">
                        <div class="tile">
                            <div class="tile-heading">
                                خروج <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-sign-out"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
</asp:Content>

