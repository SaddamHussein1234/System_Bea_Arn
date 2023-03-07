<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/WSM/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_ERP_WSM_Default" %>

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
                    <li><a href="Default.aspx">الرئيسية</a></li>
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
                    <h3 style="font-family: 'Alwatan';">
                        <asp:Label ID="lblQariah" runat="server" ></asp:Label>
                    </h3>
                    <br />
                </div>
            </div>
<%--            <div class="col-lg-3 col-md-3 col-sm-6">
                <a href='<%=ResolveUrl("~/Cpanel/ERP/CRM/PageCompany/PageCompany.aspx")%>' data-toggle="tooltip" title="الداعمين">
                    <div class="tile tile-heading" style="border-radius: 8px">
                        <div class="tile-body">
                            <h4>عدد الداعمين : 
                                <asp:Label ID="lblCountCompany" runat="server"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </a>
            </div>--%>
            <div class="clearfix"></div>
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


            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageAffiliation" visible="false">
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageAffiliation/PageAll.aspx")%>' data-toggle="tooltip" title="إدارة الإنتماء">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageCategory/PageAll.aspx")%>' data-toggle="tooltip" title="إدارة الاصناف">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageProduct/PageAll.aspx")%>' data-toggle="tooltip" title="إدارة المنتجات">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageProduct/PageProductByAffiliation.aspx")%>' data-toggle="tooltip" title="المنتجات حسب الإنتماء">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageProduct/PageProductByCategory.aspx")%>' data-toggle="tooltip" title="المنتجات حسب الصنف">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageStoragePlaces/PageAll.aspx")%>' data-toggle="tooltip" title="أماكن التخزين">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageShipping/PageAdd.aspx")%>' data-toggle="tooltip" title="شحن المستودع(الوارد)">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageShipping/PageApprovalOfTheDirector.aspx")%>' data-toggle="tooltip" title="موافقة مدير الجمعية">
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
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageShipping/PageStorekeeper.aspx")%>' data-toggle="tooltip" title="مصادقة أمين المستودع">
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
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBill" visible="false">
                <a href='<%=ResolveUrl("~/Cpanel/ERP/WSM/PageShipping/PageAll.aspx")%>' data-toggle="tooltip" title="فوتير الشحن">
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
                <a href="/Cpanel/CPanelManageWarehouse/" data-toggle="tooltip" title="عرض المستودع السابق">
                    <div class="tile">
                        <div class="tile-heading">
                            عرض المستودع السابق <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-share"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6" id="Div4" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/ITSupport/")%>" data-toggle="tooltip" title="الدعم الفني" target="_blank">
                    <div class="tile">
                        <div class="tile-heading">
                            الدعم الفني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-desktop"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div5" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/CHome/")%>" data-toggle="tooltip" title="رجوع للبوابة الإلكتروني" target="_blank">
                    <div class="tile">
                        <div class="tile-heading">
                            رجوع للبوابة الإلكتروني <span class="pull-right"></span>
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

