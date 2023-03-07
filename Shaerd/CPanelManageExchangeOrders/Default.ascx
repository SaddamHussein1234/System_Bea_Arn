<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Shaerd_CPanelManageExchangeOrders_Default" %>

<div class="page-header">
    <div class="container-fluid">
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="./">دعم المستفيدين والصرف</a></li>
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
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductMatterOfExchangeAdd" visible="false">
            <a <%--href="PageManageEyeSupport.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="إنشاء أمر صرف ( دعم عيني )">
                <div class="tile">
                    <div class="tile-heading">
                        إنشاء أمر صرف ( دعم عيني ) <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-plus"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductMatterOfExchangeForDamaged" visible="false">
            <a <%--href="PageManageProductMatterOfExchangeForDamaged.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="إنشاء أمر صرف تالف">
                <div class="tile">
                    <div class="tile-heading">
                        إنشاء أمر صرف تالف <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-plus"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDSupportForPrismsAdd" visible="false">
            <a <%--href="PageSupportForPrismsAdd.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="إنشاء أمر صرف ( دعم نقدي )">
                <div class="tile">
                    <div class="tile-heading">
                        إنشاء أمر صرف ( دعم نقدي ) <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-money"></i><i class="fa fa-dollar"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductApprovalOfTheDirectorAdd" visible="false">
            <a <%--href="PageManageProductApprovalOfTheDirector.aspx" --%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة مدير الجمعية">
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
            <a <%--href="PageManageProductCashier.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة المشرف المالي">
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
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductViceBoardAdd" visible="false">
            <a <%--href="PageManageProductViceBoard.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة نائب رئيس المجلس">
                <div class="tile">
                    <div class="tile-heading">
                        موافقة نائب رئيس المجلس <span class="pull-right"></span>
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
            <a <%--href="PageManageProductChairmanOfTheBoard.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="موافقة رئيس المجلس">
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
            <a <%--href="PageManageProductStorekeeper.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="مصادقة أمين المستودع">
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
            <a <%--href="PageManageProductFileSearchers.aspx"--%> href='javaScript:void(0)' data-toggle="modal" data-target="#ConsultationModal" title="ملف مراجعة الباحثين">
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
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductExchangeOrdersView" visible="false">
            <a href="PageManageSortExchangeOrders.aspx" data-toggle="tooltip" title="فرز أوامر الصرف">
                <div class="tile">
                    <div class="tile-heading">
                        فرز أوامر الصرف <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-list"></i>
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
                        <i class="fa fa-list"></i>
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
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="pnlFinancialStatistics" visible="false">
            <a href="../CHome/" data-toggle="tooltip" title="الإحصاء المالي">
                <div class="tile">
                    <div class="tile-heading">
                        الإحصاء المالي <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-share"></i>
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
