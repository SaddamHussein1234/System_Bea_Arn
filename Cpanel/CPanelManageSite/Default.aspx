<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_CPanelManageSite_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            <%--<div class="row" style="margin: 5px; text-align:center">
                <div class="col-md-12" style="border:solid; border-width:3px; border-color:#006011; border-radius:5px; background-color:#8b0101; color:#f5f5f5">
                    <h3 style=" font-family: 'Alwatan';">
                       نسخة تجريبية
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
            <div class="row" style="margin: 5px; text-align:center">
                <div class="col-md-12" style="border:solid; border-width:3px; border-color:#006011; border-radius:5px">
                    <br />
                    <h3 style=" font-family: 'Alwatan';">                      
                       <asp:Label ID="lblQariah" runat="server" ></asp:Label>
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
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlSystem" runat="server" visible="false">
                    <div class="tile">
                        <div class="tile-heading">
                            إدارة الموقع <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <a href="PageSettingTitle.aspx" data-toggle="tooltip" title="إعدادات العناوين" runat="server" id="IDSettingTitle" visible="false">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </a>
                            <a href="PageSettingInfo.aspx" data-toggle="tooltip" title="إعدادات البيانات" runat="server" id="IDSettingInfo" visible="false">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </a>
                            <a href="PageAboutAr.aspx" data-toggle="tooltip" title="صفحة من نحن عربي" runat="server" id="IDAboutAr" visible="false">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </a>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlMenuSite" runat="server" visible="false">
                    <a href="PageMenu.aspx" data-toggle="tooltip" title="إدارة القائمة">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة القائمة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlArticle" runat="server" visible="false">
                    <a href="PageArticle.aspx" data-toggle="tooltip" title="إدارة المقالات والفعاليات">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة المقالات والفعاليات <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlMyAlbum" runat="server" visible="false">
                    <a href="PageAlbum.aspx" data-toggle="tooltip" title="إدارة ألبوم الصور">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة ألبوم الصور <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="IDMessageView" runat="server" visible="false">
                    <a href="PageMassageVisit.aspx" data-toggle="tooltip" title="رسائل الزوار">
                        <div class="tile">
                            <div class="tile-heading">
                                رسائل الزوار <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-envelope"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="../CHome/" data-toggle="tooltip" title="رجوع البوابة الإلكترونية">
                        <div class="tile">
                            <div class="tile-heading">
                                رجوع البوابة الإلكترونية <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-backward"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        
</asp:Content>

