<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPResearcher.master" AutoEventWireup="true" CodeFile="PageElectronicGate.aspx.cs" Inherits="CResearchers_PageElectronicGate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" />
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">القُرى</a></li>
                    <li><a href="PageElectronicGate.aspx">البوابة الإلكترونية</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px; background-color: #8b0101; color: #f5f5f5">
                    <h3 style="font-family: 'Alwatan';">نسخة تجريبية
                    </h3>
                </div>
            </div>
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">البوابة الإلكترونية , 
                        <asp:Label ID="lblQariah" runat="server" ></asp:Label>
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
                    <a href="PageNotAccess.aspx" data-toggle="tooltip" title="الإعدادات والصلاحيات">
                        <div class="tile">
                            <div class="tile-heading">
                                الإعدادات والصلاحيات <span class="pull-right"></span>
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
                    <a href="PageNotAccess.aspx" data-toggle="tooltip" title="نظام إدارة الموقع" id="IDSite">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام إدارة الموقع <span class="pull-right"></span>
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
                    <a href="CPVillage/" data-toggle="tooltip" title="نظام البحث الإجتماعي">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام البحث الإجتماعي <span class="pull-right"></span>
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
                    <a href="CPanelManageExchangeOrders/" data-toggle="tooltip" title="نظام أوامر الصرف">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام أوامر الصرف <span class="pull-right"></span>
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
                    <a href="CPanelManageWarehouse/" data-toggle="tooltip" title="نظام المستودع">
                        <div class="tile">
                            <div class="tile-heading">
                                نظام المستودع <span class="pull-right"></span>
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
                <img src="../../Img/Logo.png" width="200" style="background-color: #349301; padding: 5px; border-radius: 4px" />
                <br />
                <span style="background-color: #349301; padding: 5px; border-radius: 4px">يرجى الإنتظار , جاري تنفيذ المهام</span><br />
                <br />
                <img src="../loader.gif" alt="" />
            </div>
        </div>
</asp:Content>

