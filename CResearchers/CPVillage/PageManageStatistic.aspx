<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="PageManageStatistic.aspx.cs" Inherits="CResearchers_CPVillage_PageManageStatistic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageManageStatistic.aspx">إحصائية المستفيدين</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">واجهة الإحصائيات
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryBySearchComprehensive" visible="false">
                    <a href="PageBeneficiaryBySearchComprehensive.aspx" data-toggle="tooltip" title="بحث شامل عن المستفيدين">
                        <div class="tile">
                            <div class="tile-heading">
                                بحث شامل عن المستفيدين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryBySearchBoysComprehensive" visible="false">
                    <a href="PageBeneficiaryBySearchBoysComprehensive.aspx" data-toggle="tooltip" title="بحث شامل عن أبناء المستفيدين">
                        <div class="tile">
                            <div class="tile-heading">
                                بحث شامل عن أبناء المستفيدين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryStatistic" visible="false">
                    <a href="PageBeneficiaryStatistic.aspx" data-toggle="tooltip" title="حسب الدخل الشهري">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب الدخل الشهري <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiarySourceOfIncome" visible="false">
                    <a href="PageBeneficiarySourceOfIncome.aspx" data-toggle="tooltip" title="حسب مصدر الدخل">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب مصدر الدخل <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryFamliyCases" visible="false">
                    <a href="PageBeneficiaryFamliyCases.aspx" data-toggle="tooltip" title="حسب حالات الاُسر">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب حالات الاُسر <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryAccommodationType" visible="false">
                    <a href="PageBeneficiaryAccommodationType.aspx" data-toggle="tooltip" title="حسب نوع السكن">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب نوع السكن <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryHousingStatus" visible="false">
                    <a href="PageBeneficiaryHousingStatus.aspx" data-toggle="tooltip" title="حسب حالة المسكن">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب حالة المسكن <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDBeneficiaryOrphans" visible="false">
                    <a href="PageBeneficiaryOrphans.aspx" data-toggle="tooltip" title="حسب الايتام">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب الايتام <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDEducationalSituations" visible="false">
                    <a href="PageBeneficiaryEducationalSituations.aspx" data-toggle="tooltip" title="حسب الحالات التعليمية">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب الحالات التعليمية <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDMaleAndFemale" visible="false">
                    <a href="PageBeneficiaryMaleAndFemale.aspx" data-toggle="tooltip" title="حسب الذكور والإناث">
                        <div class="tile">
                            <div class="tile-heading">
                                حسب الذكور والإناث <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
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

