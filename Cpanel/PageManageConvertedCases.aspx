<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageConvertedCases.aspx.cs" Inherits="Cpanel_PageManageConvertedCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageManageConvertedCases.aspx">طلبات تحويل الحالات</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align:center">
                <div class="col-md-12" style="border:solid; border-width:3px; border-color:#006011; border-radius:5px">
                    <br />
                    <h3 style=" font-family: 'Alwatan';">
                       واجهة تحويل الحالة
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDConvertedCasesAdd" visible="false">
                    <a href="PageConvertedCasesAdd.aspx" data-toggle="tooltip" title="إضافة طلب تحول">
                        <div class="tile">
                            <div class="tile-heading">
                                إضافة طلب تحول <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDConvertedCasesByModerAdd" visible="false">
                    <a href="PageConvertedCasesByModer.aspx" data-toggle="tooltip" title="موافقة المدير">
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
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDConvertedCasesView" visible="false">
                    <a href="PageConvertedCases.aspx" data-toggle="tooltip" title="طلبات التحويل">
                        <div class="tile">
                            <div class="tile-heading">
                                طلبات التحويل <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDConvertedCasesWaitingForApprovalView" visible="false">
                    <a href="PageConvertedCasesWaitingForApproval.aspx" data-toggle="tooltip" title="طلبات تحتاج إلى مراجعة">
                        <div class="tile">
                            <div class="tile-heading">
                                طلبات تحتاج إلى مراجعة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDConvertedCasesDetailsView" visible="false">
                    <a href="PageConvertedCasesDetails.aspx" data-toggle="tooltip" title="إستمارة طلب تحويل">
                        <div class="tile">
                            <div class="tile-heading">
                                إستمارة طلب تحويل <span class="pull-right"></span>
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

