<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CHome/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_CHome_Default" %>

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
                    <li><a href="Default.aspx">البوابة الالكترونية</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
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
                    <h3 style="font-family: 'Alwatan';">البوابة الإلكترونية
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" style="display: none">
                    <a href="Default.aspx" data-toggle="tooltip" title="البوابة الإلكترونية">
                        <div class="tile">
                            <div class="tile-heading">
                                البوابة الإلكترونية <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-home"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" data-toggle="tooltip" title="الإعدادات والصلاحيات">
                    <a href="../CPanelSetting/" data-toggle="tooltip" title="الإعدادات والصلاحيات - System Settings And Permissions">
                        <div class="tile">
                            <div class="tile-heading">
                                الإعدادات والصلاحيات SSP <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer">
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="../CPanelManageSite/" data-toggle="tooltip" title="نظام إدارة الموقع - Content Management System" id="IDSite">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام إدارة الموقع CMS <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="../" data-toggle="tooltip" title="نظام البحث الاجتماعي - Social Search System">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام البحث الاجتماعي SSM <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../ERP/WSM/" data-toggle="tooltip" title="نظام المستودع - Warehouse System">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام المستودع WSM <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="../ERP/EOS/" data-toggle="tooltip" title="نظام أوامر الصرف - Exchange Order System">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام أوامر الصرف EOS <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="../ERP/FMS/" data-toggle="tooltip" title="نظام المالية - Financial Management System">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام المالية FMS <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-money"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../CPanelManageZakat/" data-toggle="tooltip" title="نظام الزكاة - Zakat System">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام الزكاة ZSM <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../CPGeneralAssembly/" data-toggle="tooltip" title="نظام الجمعية العمومية General Assembly System">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام الجمعية العمومية GAM <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../ERP/" data-toggle="tooltip" title="نظام الموارد البشرية HR System">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام الموارد البشرية HRM <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../ERP/CRM/" data-toggle="tooltip" title=" الإستثمار وتنمية الموارد - Customer Relationship Management">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام الإستثمار وتنمية الموارد CRM <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../CPanelAttach/" data-toggle="tooltip" title="مزود رسائل الجوال - SMS">
                        <div class="tile">
                            <div class="tile-heading">
                                مزود رسائل الجوال SMS <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../ERP/OM/" data-toggle="tooltip" title="نظام إدارة الجمعية - OM">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام إدارة الجمعية OM <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../ERP/DMS/" data-toggle="tooltip" title="نظام الإتصالات الإدارية - DMS">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام الإتصالات الإدارية  DMS <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="../ERP/CRS/" data-toggle="tooltip" title="نظام تقارير اللجان - CRS">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام تقارير اللجان CRS <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h3 class="pull-right">دخول النظام
                                </h3>
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
        <div class="loading2" align="center" id="lodi">
            <div>
                <img src="/Img/Logo.png" width="200" style="background-color: #349301; padding: 5px; border-radius: 4px" />
                <br />
                <span style="background-color: #349301; padding: 5px; border-radius: 4px">يرجى الإنتظار , جاري تنفيذ المهام</span><br />
                <br />
                <img src="../loader.gif" alt="" />
            </div>
        </div>
</asp:Content>

