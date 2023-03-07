<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Shaerd_CPanelManageWarehouse_Default" %>
<div id="content">
    <div class="page-header">
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">المستودع</a></li>
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
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageAffiliation" visible="false">
                <a <%--href="PageManageAffiliation.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="إدارة الإنتماء">
                    <div class="tile">
                        <div class="tile-heading">
                            إدارة الإنتماء <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageCategory" visible="false">
                <a <%--href="PageManageCategory.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="إدارة الاصناف">
                    <div class="tile">
                        <div class="tile-heading">
                            إدارة الاصناف <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProduct" visible="false">
                <a <%--href="PageManageProduct.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="إدارة المنتجات">
                    <div class="tile">
                        <div class="tile-heading">
                            إدارة المنتجات <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductByAffiliationView" visible="false">
                <a <%--href="PageManageProductByAffiliation.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="المنتجات حسب الإنتماء">
                    <div class="tile">
                        <div class="tile-heading">
                            المنتجات حسب الإنتماء <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductByCategoryView" visible="false">
                <a <%--href="PageManageProductByCategory.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="المنتجات حسب الصنف">
                    <div class="tile">
                        <div class="tile-heading">
                            المنتجات حسب الصنف <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageStoragePlaces" visible="false">
                <a <%--href="PageManageStoragePlaces.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="أماكن التخزين">
                    <div class="tile">
                        <div class="tile-heading">
                            أماكن التخزين <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductShippingWarehouseAdd" visible="false">
                <a <%--href="PageManageProductShippingWarehouse.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="شحن المستودع(الوارد)">
                    <div class="tile">
                        <div class="tile-heading">
                            شحن المستودع(الوارد) <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehouseApprovalOfTheDirectorAdd" visible="false">
                <a <%--href="PageManageProductWarehouseApprovalOfTheDirector.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة مدير الجمعية">
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
                <a <%--href="PageManageProductWarehouseCashier.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة المشرف المالي">
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
                <a <%--href="PageManageProductWarehouseChairmanOfTheBoard.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة رئيس المجلس">
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
                <a <%--href="PageManageProductWarehouseStorekeeper.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة أمين المستودع">
                    <div class="tile">
                        <div class="tile-heading">
                            موافقة أمين المستودع <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-check-square-o"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBill" visible="false">
                <a href="PageManageProductWarehouseInvoiceList.aspx" data-toggle="tooltip" title="فوتير الشحن">
                    <div class="tile">
                        <div class="tile-heading">
                            فوتير الشحن <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-file"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehousebyContainedView" visible="false">
                <a href="PageManageProductWarehousebyContained.aspx" data-toggle="tooltip" title="بحث تفصيلي للوارد">
                    <div class="tile">
                        <div class="tile-heading">
                            بحث تفصيلي للوارد <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-search"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehousebyIssuedView" visible="false">
                <a href="PageManageProductWarehousebyIssued.aspx" data-toggle="tooltip" title="بحث تفصيلي للصادر">
                    <div class="tile">
                        <div class="tile-heading">
                            بحث تفصيلي للصادر <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-search"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductWarehousebyContainedAndIssuedView" visible="false">
                <a href="PageManageProductWarehousebyContainedAndIssued.aspx" data-toggle="tooltip" title="تفصيلي الوارد والصادر">
                    <div class="tile">
                        <div class="tile-heading">
                            تفصيلي الوارد والصادر <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-search"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="pnlCloseToCompletion" visible="false">
                <a href="PageManageProductWarehousebyProductsCloseToCompletion.aspx" data-toggle="tooltip" title="منتجات قاربت على الإنتهاء">
                    <div class="tile">
                        <div class="tile-heading">
                            منتجات قاربت على الإنتهاء <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-calendar"></i>
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
