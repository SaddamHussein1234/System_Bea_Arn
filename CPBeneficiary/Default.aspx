<%@ Page Title="" Language="C#" MasterPageFile="~/CPBeneficiary/MPBeneficiary.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CPBeneficiary_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css" rel="stylesheet" />
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
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; display: none">
                <img src="../view/image/logo.png" style="width: 100%; height: 50%; float: left" />
            </div>
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">
                        <asp:Label ID="lblLestName" runat="server"></asp:Label> 
                        <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
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
                <div class="col-lg-3 col-md-3 col-sm-6" id="IDBeneficiary" runat="server" visible="false">
                    <a href="PageBeneficiary.aspx" data-toggle="tooltip" title="بيانات الإستمارة">
                        <div class="tile">
                            <div class="tile-heading">
                                بيانات الإستمارة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="IDBeneficiaryFamily" runat="server" visible="false">
                    <a href="PageBeneficiaryFamily.aspx" data-toggle="tooltip" title="معلومات أفراد الأسرة">
                        <div class="tile">
                            <div class="tile-heading">
                                معلومات أفراد الأسرة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="IDStatusDetails" runat="server" visible="false">
                    <a href="PageStatusDetails.aspx" data-toggle="tooltip" title="تقرير بحث الحالة">
                        <div class="tile">
                            <div class="tile-heading">
                                تقرير بحث الحالة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="IDAcceptanceDecision" runat="server" visible="false">
                    <a href="PageAcceptanceDecision.aspx" data-toggle="tooltip" title="قرار القبول">
                        <div class="tile">
                            <div class="tile-heading">
                                قرار القبول <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="IDFormData" runat="server" visible="false">
                    <a href="PageFormData.aspx" data-toggle="tooltip" title="بيانات الإستمارة">
                        <div class="tile">
                            <div class="tile-heading">
                                بيانات الإستمارة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <asp:Repeater ID="RPTReportDevice" runat="server" visible="false">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <a href='PageVisitReportDetails.aspx?ID=<%# Eval("IDUniq")%>' data-toggle="tooltip" title="إحتياجات المستفيد">
                                <div class="tile">
                                    <div class="tile-heading">
                                        إحتياجات المستفيد <span class="pull-right"></span>
                                    </div>
                                    <div class="tile-body">
                                        <i class="fa fa-file"></i>
                                        <h2 class="pull-right"></h2>
                                    </div>
                                    <div class="tile-footer"></div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlSupport" runat="server" visible="false">
                    <div class="tile">
                        <div class="tile-heading">
                            الدعم العيني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <a href="PageSupport.aspx" data-toggle="tooltip" title="سلل - أجهزة - تأثيث المنزل" id="IDSupport" runat="server" visible="false">
                                <i class="fa fa-cart-plus"></i>
                                <h2 class="pull-right"></h2>
                            </a>
                            <i class="fa fa-minus"></i>
                            <a href="PageSupportHome.aspx" data-toggle="tooltip" title="بناء - ترميم المنزل" id="IDSupportHome" runat="server" visible="false">
                                <i class="fa fa-hospital-o"></i>
                                <h2 class="pull-right"></h2>
                            </a>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlSupportMony" runat="server" visible="false">
                    <a href="PageSupportMony.aspx" data-toggle="tooltip" title="الدعم النقدي">
                        <div class="tile">
                            <div class="tile-heading">
                                الدعم النقدي <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-money"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6">
                    <a href="CHome/" data-toggle="tooltip" title="رجوع">
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

