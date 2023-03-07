<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageExclusionOfTheBeneficiary.aspx.cs" Inherits="Cpanel_PageManageExclusionOfTheBeneficiary" %>

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
                    <li><a href="PageManageExclusionOfTheBeneficiary.aspx">إستبعاد مستفيد</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">واجهة إستبعاد المستفيدين
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDExclusionOfTheBeneficiaryAdd" visible="false">
                    <a href="PageExclusionOfTheBeneficiaryAdd.aspx" data-toggle="tooltip" title="إضافة طلب إستبعاد">
                        <div class="tile">
                            <div class="tile-heading">
                                إضافة طلب إستبعاد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDExclusionOfTheBeneficiaryByModerViewAdd" visible="false">
                    <a href="PageExclusionOfTheBeneficiaryByModer.aspx" data-toggle="tooltip" title="موافقة المدير">
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
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDExclusionOfTheBeneficiaryByRaeesAdd" visible="false">
                    <a href="PageExclusionOfTheBeneficiaryByRaees.aspx" data-toggle="tooltip" title="موافقة رئيس المجلس">
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
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDExclusionOfTheBeneficiaryView" visible="false">
                    <a href="PageExclusionOfTheBeneficiary.aspx" data-toggle="tooltip" title="طلبات الإستبعاد">
                        <div class="tile">
                            <div class="tile-heading">
                                طلبات الإستبعاد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDExclusionOfTheBeneficiaryDetailsView" visible="false">
                    <a href="PageExclusionOfTheBeneficiaryDetails.aspx" data-toggle="tooltip" title="إستمارة طلب إستبعاد">
                        <div class="tile">
                            <div class="tile-heading">
                                إستمارة طلب إستبعاد <span class="pull-right"></span>
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

