<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageManageEyeSupport.ascx.cs" Inherits="Shaerd_CPanelManageExchangeOrders_PageManageEyeSupport" %>

<div class="page-header">
    <div class="container-fluid">
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="PageManageEyeSupport.aspx">إنشاء أمر صرف ( دعم عيني )</a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="row" style="margin: 5px; text-align: center">
        <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
            <br />
            <h3 style="font-family: 'Alwatan';">إنشاء أمر صرف ( دعم عيني )
            </h3>
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductMatterOfExchangeAdd" visible="false">
            <a href="PageManageProductMatterOfExchange.aspx" data-toggle="tooltip" title="أمر صرف سلة غذائية">
                <div class="tile">
                    <div class="tile-heading">
                        أمر صرف سلة غذائية <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-plus"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductMatterOfExchangeForDeviceAdd" visible="false">
            <a href="PageManageProductMatterOfExchangeForDevice.aspx" data-toggle="tooltip" title="أمر صرف أجهزة كهربائية">
                <div class="tile">
                    <div class="tile-heading">
                        أمر صرف أجهزة كهربائية <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-plus"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDAnOrderToTxchangeHomeFurnishingAdd" visible="false">
            <a href="PageManageProductAnOrderToTxchangeHomeFurnishing.aspx" data-toggle="tooltip" title="أمر صرف تأثيث منزل">
                <div class="tile">
                    <div class="tile-heading">
                        أمر صرف تأثيث منزل <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-plus"></i>
                        <h2 class="pull-right"></h2>
                    </div>
                    <div class="tile-footer"></div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6" runat="server" id="IDManageProductRestorationAndConstructionAdd" visible="false">
            <a href="PageManageProductRestorationAndConstruction.aspx" data-toggle="tooltip" title="أمر بناء وترميم منازل">
                <div class="tile">
                    <div class="tile-heading">
                        أمر بناء وترميم منازل <span class="pull-right"></span>
                    </div>
                    <div class="tile-body">
                        <i class="fa fa-plus"></i>
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
