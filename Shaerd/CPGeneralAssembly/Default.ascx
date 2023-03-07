<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Default.ascx.cs" Inherits="Shaerd_CPGeneralAssembly_Default" %>
<div id="content">
    <div class="page-header">
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">الجمعية العمومية</a></li>
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
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDAdminAdd" visible="false">
                <a href="PageAdminAdd.aspx" data-toggle="tooltip" title="إضافة عضو جديد">
                    <div class="tile">
                        <div class="tile-heading">
                            إضافة عضو جديد <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-plus"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDAdmin" visible="false">
                <a href="PageAdmin.aspx" data-toggle="tooltip" title="أعضاء الجمعية العمومية">
                    <div class="tile">
                        <div class="tile-heading">
                            أعضاء الجمعية العمومية <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyAdd" visible="false">
                <a href="PageGeneralAssemblyAdd.aspx" data-toggle="tooltip" title="إضافة إستمارة">
                    <div class="tile">
                        <div class="tile-heading">
                            إضافة إستمارة <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-plus"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssembly" visible="false">
                <a href="PageGeneralAssembly.aspx" data-toggle="tooltip" title="إستمارات الأعضاء">
                    <div class="tile">
                        <div class="tile-heading">
                            إستمارات الأعضاء <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyAllow" visible="false">
                <a href="PageGeneralAssemblyAllow.aspx" data-toggle="tooltip" title="موافقة على الإستمارة">
                    <div class="tile">
                        <div class="tile-heading">
                            موافقة على الإستمارة <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-check-square-o"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyView" visible="false">
                <a href="PageGeneralAssemblyView.aspx" data-toggle="tooltip" title="تفاصيل الإستمارة">
                    <div class="tile">
                        <div class="tile-heading">
                            تفاصيل الإستمارة <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-file"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyBillAdd" visible="false">
                <a href="PageAdd.aspx" data-toggle="tooltip" title="إضافة ايصال جديد">
                    <div class="tile">
                        <div class="tile-heading">
                            إضافة ايصال جديد <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-plus"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyBill" visible="false">
                <a href="PageAll.aspx" data-toggle="tooltip" title="قائة الايصالات">
                    <div class="tile">
                        <div class="tile-heading">
                            قائة الايصالات <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-list"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyBillAmeen" visible="false">
                <a href="PageAmeen.aspx" data-toggle="tooltip" title="موافقة المشرف المالي">
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
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyBillRaees" visible="false">
                <a href="PageRaees.aspx" data-toggle="tooltip" title="موافقة رئيس المجلس">
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
            <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDGeneralAssemblyBillView" visible="false">
                <a href="PageGeneralAssemblyBillView.aspx" data-toggle="tooltip" title="تفاصيل السند">
                    <div class="tile">
                        <div class="tile-heading">
                            تفاصيل السند <span class="pull-right"></span>
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