<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageMenu.aspx.cs" Inherits="Cpanel_PageManageMenu" %>

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
                    <li><a href="PageManageMenu.aspx">القوائم</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align:center">
                <div class="col-md-12" style="border:solid; border-width:3px; border-color:#006011; border-radius:5px">
                    <br />
                    <h3 style=" font-family: 'Alwatan';">
                       واجهة القوائم
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageVillage" visible="false">
                    <a href="PageManageVillage.aspx" data-toggle="tooltip" title="إدارة القُرى">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة القُرى <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageBeneficiaryStatus" visible="false">
                    <a href="PageManageBeneficiaryStatus.aspx" data-toggle="tooltip" title="إدارة حالة المستفيد">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة حالة المستفيد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageTypeOfDwelling" visible="false">
                    <a href="PageManageTypeOfDwelling.aspx" data-toggle="tooltip" title="إدارة نوع المسكن">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة نوع المسكن <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageMonthlyIncome" visible="false">
                    <a href="PageManageMonthlyIncome.aspx" data-toggle="tooltip" title="إدارة الدخل الشهري">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة الدخل الشهري <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageHousingStatus" visible="false">
                    <a href="PageManageHousingStatus.aspx" data-toggle="tooltip" title="إدارة حالة المسكن">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة حالة المسكن <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageSupportType" visible="false">
                    <a href="PageManageSupportType.aspx" data-toggle="tooltip" title="إدارة نوع الدعم">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة نوع الدعم <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageBeneficiaryFamily" visible="false">
                    <a href="PageManageBeneficiaryFamily.aspx" data-toggle="tooltip" title="إدارة قرابة عائلة المستفيد">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة قرابة عائلة المستفيد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageBeneficiaryRelationship" visible="false">
                    <a href="PageManageBeneficiaryRelationship.aspx" data-toggle="tooltip" title="إدارة صلة قرابة المستفيد">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة صلة قرابة المستفيد <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDInitiatives" visible="false">
                    <a href="PageInitiatives.aspx" data-toggle="tooltip" title="إدارة المبادرات والداعمين">
                        <div class="tile">
                            <div class="tile-heading">
                                إدارة المبادرات والداعمين <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-file"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
</asp:Content>

