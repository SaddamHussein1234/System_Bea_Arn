<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageRe_beneficiary.aspx.cs" Inherits="Cpanel_PageManageRe_beneficiary" %>

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
                    <li><a href="PageManageRe_beneficiary.aspx">إعادة مستفيد</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">واجهة إعادة المستفيدين
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDRe_beneficiaryAdd" visible="false">
                    <a href="PageRe_beneficiaryAdd.aspx" data-toggle="tooltip" title="إضافة طلب إعادة">
                        <div class="tile">
                            <div class="tile-heading">
                                إضافة طلب إعادة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDRe_beneficiaryByModerAdd" visible="false">
                    <a href="PageRe_beneficiaryByModer.aspx" data-toggle="tooltip" title="موافقة المدير">
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
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDRe_beneficiaryByRaeesAdd" visible="false">
                    <a href="PageRe_beneficiaryByRaees.aspx" data-toggle="tooltip" title="موافقة رئيس المجلس">
                        <div class="tile">
                            <div class="tile-heading">
                                موافقة رئيس المجلس <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-check-square"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDRe_beneficiaryView" visible="false">
                    <a href="PageRe_beneficiary.aspx" data-toggle="tooltip" title="طلبات الإعادة">
                        <div class="tile">
                            <div class="tile-heading">
                                طلبات الإعادة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDRe_beneficiaryDetailsView" visible="false">
                    <a href="PageRe_beneficiaryDetails.aspx" data-toggle="tooltip" title="إستمارة طلب إعادة">
                        <div class="tile">
                            <div class="tile-heading">
                                إستمارة طلب إعادة <span class="pull-right"></span>
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

