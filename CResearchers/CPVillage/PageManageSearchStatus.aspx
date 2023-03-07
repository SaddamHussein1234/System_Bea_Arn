<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="PageManageSearchStatus.aspx.cs" Inherits="CResearchers_CPVillage_PageManageSearchStatus" %>

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
                    <li><a href="PageManageSearchStatus.aspx">بحث حالة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">واجهة بحث حالة
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-3 col-sm-6" runat="server" id="IDSearchStatusAdd" visible="false">
                    <a href="PageSearchStatusAdd.aspx" data-toggle="tooltip" title="إضافة بحث حالة">
                        <div class="tile">
                            <div class="tile-heading">
                                إضافة بحث حالة <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-plus"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-6 col-md-3 col-sm-6" runat="server" id="IDSearchStatus" visible="false">
                    <a href="PageSearchStatus.aspx" data-toggle="tooltip" title="قائمة بحث الحالات">
                        <div class="tile">
                            <div class="tile-heading">
                                قائمة بحث الحالات <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-list"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
                <div class="col-lg-6 col-md-3 col-sm-6" runat="server" id="IDSearchStatusDetails" visible="false">
                    <a href="PageSearchStatusDetails.aspx" data-toggle="tooltip" title="إستمارة بحث حالة">
                        <div class="tile">
                            <div class="tile-heading">
                                إستمارة بحث حالة <span class="pull-right"></span>
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

