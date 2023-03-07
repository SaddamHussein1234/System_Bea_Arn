<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/EOS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageStatistics.aspx.cs" Inherits="Cpanel_ERP_EOS_PageManage_PageManageStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="<%=ResolveUrl("~/Cpanel/ERP/EOS/Default.aspx")%>">الرئيسية</a></li>
                    <li><a href="">الإحصاء المالي</a></li>
                </ul>
            </div>
        </div>
<div class="container-fluid">
    <div class="row" style="margin: 5px; text-align: center">
        <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
            <br />
            <h3 style="font-family: 'Alwatan';">
                الإحصاء المالي العام
            </h3>
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductAddThePriceToOrder" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatisticsGeneral.aspx")%>' data-toggle="tooltip" title="الدعم العيني العام">
                <div class="tile">
                    <div class="tile-heading">
                        الدعم العيني العام <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="Div1" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatisticsGeneralHome.aspx")%>' data-toggle="tooltip" title="البناء والترميم">
                <div class="tile">
                    <div class="tile-heading">
                        البناء والترميم <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="Div2" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatisticsGeneralCashSupport.aspx")%>' data-toggle="tooltip" title="الدعم النقدي العام">
                <div class="tile">
                    <div class="tile-heading">
                        الدعم النقدي العام <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
    </div>
    <div class="row" style="margin: 5px; text-align: center">
        <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
            <br />
            <h3 style="font-family: 'Alwatan';">
                الإحصاء المالي التفصيلي
            </h3>
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="Div3" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatistics.aspx")%>' data-toggle="tooltip" title="إحصاء الدعم العيني">
                <div class="tile">
                    <div class="tile-heading">
                        إحصاء الدعم العيني <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="Div4" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatisticsByDevice.aspx")%>' data-toggle="tooltip" title="الأدوية والأجهزة">
                <div class="tile">
                    <div class="tile-heading">
                        الأدوية والأجهزة <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="Div5" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatisticsByHomeFurnishing.aspx")%>' data-toggle="tooltip" title="تأثيث المنازل">
                <div class="tile">
                    <div class="tile-heading">
                        تأثيث المنازل <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="Div6" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatisticsByBuilding.aspx")%>' data-toggle="tooltip" title="بناء وترميم المنازل">
                <div class="tile">
                    <div class="tile-heading">
                        بناء وترميم المنازل <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="Div7" visible="true">
            <a href='<%=ResolveUrl("~/Cpanel/ERP/EOS/PageStatistics/PageStatisticsCashSupport.aspx")%>' data-toggle="tooltip" title="الدعم النقدي">
                <div class="tile">
                    <div class="tile-heading">
                        الدعم النقدي <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-area-chart"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6">
            <a href="../" data-toggle="tooltip" title="رجوع">
                <div class="tile">
                    <div class="tile-heading">
                        رجوع <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-share"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
    </div>
</div>
</asp:Content>

