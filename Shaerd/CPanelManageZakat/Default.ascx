<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Shaerd_CPanelManageZakat_Default" %>
<div id="content">
    <div class="page-header">
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">زكاة الفطر</a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="col-sm-12">
            <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                <span class="badge badge-pill badge-warning">تحذير</span>
                <span>يرجى تفعيل المصادقة الثنائية لزيادة حماية حسابك 
                    <a href="<%=ResolveUrl("~/Cpanel/CHome/PageMyAccount.aspx")%>">من هنا ... </a>
                </span>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        </div>
        <div class="row" style="margin: 5px; text-align: center">
            <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                <br />
                <h3 style="font-family: 'Alwatan';">
                    <asp:Label ID="lblQariah" runat="server" ></asp:Label>
                </h3>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDCategory" visible="false">
                <a href="PageCategory.aspx" data-toggle="tooltip" title="إدارة الأصناف">
                    <div class="tile">
                        <div class="tile-heading">
                            إدارة الأصناف <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDDeedDonationInKindAdd" visible="false">
                <a href="PageDeedDonationInKind.aspx" data-toggle="tooltip" title="إضافة سند تبرع عيني">
                    <div class="tile">
                        <div class="tile-heading">
                            إضافة سند تبرع عيني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-plus"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehouseApprovalOfTheDirectorAdd" visible="false">
                <a href="PageManageProductWarehouseApprovalOfTheDirector.aspx" data-toggle="tooltip" title="موافقة مدير الجمعية">
                    <div class="tile">
                        <div class="tile-heading">
                            موافقة مدير الجمعية <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-check-square-o"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehouseCashierAdd" visible="false">
                <a href="PageManageProductWarehouseCashier.aspx" data-toggle="tooltip" title="موافقة المشرف المالي">
                    <div class="tile">
                        <div class="tile-heading">
                            موافقة المشرف المالي <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-check-square-o"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehouseChairmanOfTheBoardAdd" visible="false">
                <a href="PageManageProductWarehouseChairmanOfTheBoard.aspx" data-toggle="tooltip" title="موافقة رئيس المجلس">
                    <div class="tile">
                        <div class="tile-heading">
                            موافقة رئيس المجلس <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-check-square-o"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehouseStorekeeperAdd" visible="false">
                <a href="PageManageProductWarehouseStorekeeper.aspx" data-toggle="tooltip" title="مصادقة أمين المستودع">
                    <div class="tile">
                        <div class="tile-heading">
                            مصادقة أمين المستودع <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-check-square-o"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDDeedDonationInKindAll" visible="false">
                <a href="PageDeedDonationInKindAll.aspx" data-toggle="tooltip" title="فواتير التبرع العيني">
                    <div class="tile">
                        <div class="tile-heading">
                            فواتير التبرع العيني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-file"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDDeedDonationInKindView" visible="false">
                <a href="PageDeedDonationInKindDetails.aspx" data-toggle="tooltip" title="تفاصيل فاتورة تبرع عيني">
                    <div class="tile">
                        <div class="tile-heading">
                            تفاصيل فاتورة تبرع عيني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-file"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDDeedDonationInKindInCome" visible="false">
                <a href="PageDeedDonationInKindInCome.aspx" data-toggle="tooltip" title="الإحصاء المالي للوارد">
                    <div class="tile">
                        <div class="tile-heading">
                            الإحصاء المالي للوارد <span class="pull-right"></span>
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
                <a href="../CHome/" data-toggle="tooltip" title="رجوع">
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