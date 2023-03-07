<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductMatter.aspx.cs" Inherits="Cpanel_PageManageProductMatter" %>

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
                    <li><a href="PageManageProductMatter.aspx">دعم المستفيدين والصرف</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align:center">
                <div class="col-md-12" style="border:solid; border-width:3px; border-color:#006011; border-radius:5px">
                    <br />
                    <h3 style=" font-family: 'Alwatan';">
                       واجهة دعم المستفيدين والصرف
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductMatterOfExchangeAdd" visible="false">
                    <a href="PageManageProductMatterOfExchange.aspx" data-toggle="tooltip" title="إنشاء أمر صرف">
                        <div class="tile">
                            <div class="tile-heading">
                                إنشاء أمر صرف <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductExchangeOrdersView" visible="false">
                    <a href="PageManageProductExchangeOrdersDetails.aspx" data-toggle="tooltip" title="فرز أوامر الصرف">
                        <div class="tile">
                            <div class="tile-heading">
                                فرز أوامر الصرف <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductSupportByBeneficiaryView" visible="false">
                    <a href="PageManageProductSupportByBeneficiary.aspx" data-toggle="tooltip" title="فرز دعم المستفيد">
                        <div class="tile">
                            <div class="tile-heading">
                                فرز دعم المستفيد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductApprovalOfTheDirectorAdd" visible="false">
                    <a href="PageManageProductApprovalOfTheDirector.aspx" data-toggle="tooltip" title="موافقة مدير الجمعية">
                        <div class="tile">
                            <div class="tile-heading">
                                موافقة مدير الجمعية <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductCashierAdd" visible="false">
                    <a href="PageManageProductCashier.aspx" data-toggle="tooltip" title="موافقة المشرف المالي">
                        <div class="tile">
                            <div class="tile-heading">
                                موافقة المشرف المالي <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductChairmanOfTheBoardAdd" visible="false">
                    <a href="PageManageProductChairmanOfTheBoard.aspx" data-toggle="tooltip" title="موافقة رئيس المجلس">
                        <div class="tile">
                            <div class="tile-heading">
                                موافقة رئيس المجلس <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductStorekeeperAdd" visible="false">
                    <a href="PageManageProductStorekeeper.aspx" data-toggle="tooltip" title="مصادقة أمين المستودع">
                        <div class="tile">
                            <div class="tile-heading">
                                مصادقة أمين المستودع <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductFileSearchersAdd" visible="false">
                    <a href="PageManageProductFileSearchers.aspx" data-toggle="tooltip" title="ملف مراجعة الباحثين">
                        <div class="tile">
                            <div class="tile-heading">
                                ملف مراجعة الباحثين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductAddThePriceToOrder" visible="false">
                    <a href="PageManageProductAddThePriceToOrder.aspx" data-toggle="tooltip" title="تفاصيل الفاتورة">
                        <div class="tile">
                            <div class="tile-heading">
                                تفاصيل الفاتورة <span class="pull-right"></span>
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

