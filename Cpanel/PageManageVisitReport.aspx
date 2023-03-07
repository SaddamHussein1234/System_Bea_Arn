<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageVisitReport.aspx.cs" Inherits="Cpanel_PageManageVisitReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageManageVisitReport.aspx">تقارير الزيارات الميدانية</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">واجهة تقارير الزيارات الميدانية
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportAdd" visible="false">
                    <a href="PageVisitReportAdd.aspx" data-toggle="tooltip" title="إضافة تقرير جديد">
                        <div class="tile">
                            <div class="tile-heading">
                                إضافة تقرير جديد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportByModerAdd" visible="false">
                    <a href="PageVisitReportByModer.aspx" data-toggle="tooltip" title="موافقة المدير">
                        <div class="tile">
                            <div class="tile-heading">
                                موافقة المدير <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-check-square"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportByRaeesAllagnahAdd" visible="false">
                    <a href="PageVisitReportByRaeesAllagnah.aspx" data-toggle="tooltip" title="موافقة رئيس اللجنة">
                        <div class="tile">
                            <div class="tile-heading">
                                موافقة رئيس اللجنة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-check-square"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportSearch" visible="false">
                    <a href="PageVisitReportSearch.aspx" data-toggle="tooltip" title="بحث تفصيلي لنتيجة الزيارة">
                        <div class="tile">
                            <div class="tile-heading">
                                بحث تفصيلي لنتيجة الزيارة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-search"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportView" visible="false">
                    <a href="PageVisitReport.aspx" data-toggle="tooltip" title="عرض تقارير الزيارات">
                        <div class="tile">
                            <div class="tile-heading">
                                عرض تقارير الزيارات <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>


                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportByDevice" visible="false">
                    <a href="PageVisitReportByDevice.aspx" data-toggle="tooltip" title="تقرير الاجهزة الكهربائية">
                        <div class="tile">
                            <div class="tile-heading">
                                تقرير الاجهزة الكهربائية <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-desktop"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportByHouse" visible="false">
                    <a href="PageVisitReportByHouse.aspx" data-toggle="tooltip" title="عرض تقارير الزيارات">
                        <div class="tile">
                            <div class="tile-heading">
                                تقرير المنازل <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-hospital-o"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDVisitReportDetailsView" visible="false">
                    <a href="PageVisitReportDetails.aspx" data-toggle="tooltip" title="تقرير نتيجة زيارة ميدانية">
                        <div class="tile">
                            <div class="tile-heading">
                                تقرير نتيجة زيارة ميدانية <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="Default.aspx" data-toggle="tooltip" title="رجوع">
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

