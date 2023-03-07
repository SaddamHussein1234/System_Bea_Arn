<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CResearchers_CPVillage_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" />
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
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; display: none">
                <img src="../view/image/logo.png" style="width: 100%; height: 50%; float: left" />
            </div>
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px; background-color: #8b0101; color: #f5f5f5">
                    <h3 style="font-family: 'Alwatan';">نسخة تجريبية
                    </h3>
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
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlMostafeed" runat="server" visible="false">
                    <a href="PageManageBeneficiary.aspx" data-toggle="tooltip" title="إدارة المستفيدين">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة المستفيدين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-user"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlMostafeedStatistic" runat="server" visible="false">
                    <a href="PageManageStatistic.aspx" data-toggle="tooltip" title="إحصائية المستفيدين">
                        <div class="tile">
                            <div class="tile-heading">
                                إحصائية المستفيدين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-line-chart"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlSearchStatus" runat="server" visible="false">
                    <a href="PageManageSearchStatus.aspx" data-toggle="tooltip" title="بحث حالة">
                        <div class="tile">
                            <div class="tile-heading">
                                بحث حالة <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-edit"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlAcceptanceDecision" runat="server" visible="false">
                    <a href="PageManageAcceptanceDecision.aspx" data-toggle="tooltip" title="قرارات القبول">
                        <div class="tile">
                            <div class="tile-heading">
                                قرارات القبول <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-check-square-o"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlTecisionToExclude" runat="server" visible="false">
                    <a href="PageManageTecisionToExclude.aspx" data-toggle="tooltip" title="قرارات الإستبعاد">
                        <div class="tile">
                            <div class="tile-heading">
                                قرارات الإستبعاد <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-check-square-o"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlAfieldVisi" runat="server" visible="false">
                    <a href="PageManageAfieldVisit.aspx" data-toggle="tooltip" title="الزيارات الميدانية">
                        <div class="tile">
                            <div class="tile-heading">
                                الزيارات الميدانية <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-edit"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlVisitReport" runat="server" visible="false">
                    <a href="PageManageVisitReport.aspx" data-toggle="tooltip" title="تقارير الزيارات الميدانيه">
                        <div class="tile">
                            <div class="tile-heading">
                                تقارير الزيارات الميدانيه <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-edit"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlRe_beneficiary" runat="server" visible="false">
                    <a href="PageManageRe_beneficiary.aspx" data-toggle="tooltip" title="إعادة مستفيد">
                        <div class="tile">
                            <div class="tile-heading">
                                إعادة مستفيد <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-user-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlExclusionOfTheBeneficiary" runat="server" visible="false">
                    <a href="PageManageExclusionOfTheBeneficiary.aspx" data-toggle="tooltip" title="إستبعاد مستفيد">
                        <div class="tile">
                            <div class="tile-heading">
                                إستبعاد مستفيد <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-user-times"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" id="pnlConvertedCases" runat="server" visible="false">
                    <a href="PageManageConvertedCases.aspx" data-toggle="tooltip" title="طلبات تحويل الحالات">
                        <div class="tile">
                            <div class="tile-heading">
                                طلبات تحويل الحالات <span class="pull-center"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-connectdevelop"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="pnlProductMatterOfExchange" visible="false">
                    <a href="PageManageProductMatter.aspx" data-toggle="tooltip" title="دعم المستفيدين والصرف">
                        <div class="tile">
                            <div class="tile-heading">
                                دعم المستفيدين والصرف <span class="pull-center"></span>
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

