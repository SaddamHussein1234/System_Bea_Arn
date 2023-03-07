<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageBeneficiary.aspx.cs" Inherits="Cpanel_PageManageBeneficiary" %>

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
                    <li><a href="PageManageBeneficiary.aspx">المستفيدين</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align:center">
                <div class="col-md-12" style="border:solid; border-width:3px; border-color:#006011; border-radius:5px">
                    <br />
                    <h3 style=" font-family: 'Alwatan';">
                       واجهة المستفيدين
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDAddBeneficiary" visible="false">
                    <a href="PageAddBeneficiary.aspx" data-toggle="tooltip" title="إضافة مستفيد">
                        <div class="tile">
                            <div class="tile-heading">
                                إضافة مستفيد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryByRaeesAlLagnah" visible="false">
                    <a href="PageBeneficiaryByRaeesAlLagnah.aspx" data-toggle="tooltip" title="موافقة رئيس اللجنة">
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
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryByModer" visible="false">
                    <a href="PageBeneficiaryByModer.aspx" data-toggle="tooltip" title="موافقة المدير">
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
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryByRaeesAlMaglis" visible="false">
                    <a href="PageBeneficiaryByRaeesAlMaglis.aspx" data-toggle="tooltip" title="موافقة رئيس المجلس">
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
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryBySearch" visible="false">
                    <a href="PageBeneficiaryBySearch.aspx" data-toggle="tooltip" title="بيانات المستفيدين">
                        <div class="tile">
                            <div class="tile-heading">
                                بيانات المستفيدين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryByView" visible="false">
                    <a href="PageBeneficiaryByView.aspx" data-toggle="tooltip" title="إستمارة المستفيدين">
                        <div class="tile">
                            <div class="tile-heading">
                                إستمارة المستفيدين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryAddBoys" visible="false">
                    <a href="PageBeneficiaryAddBoys.aspx" data-toggle="tooltip" title="معلومات أفراد الاسرة">
                        <div class="tile">
                            <div class="tile-heading">
                                معلومات أفراد الاسرة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBack">
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

