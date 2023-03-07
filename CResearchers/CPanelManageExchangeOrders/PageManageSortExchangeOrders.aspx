<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageSortExchangeOrders.aspx.cs" Inherits="CResearchers_CPanelManageExchangeOrders_PageManageSortExchangeOrders" %>

<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductExchangeOrdersDetails.ascx" TagPrefix="uc1" TagName="PageManageProductExchangeOrdersDetails" %>
<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductExchangeOrdersDetailsHouse.ascx" TagPrefix="uc1" TagName="PageManageProductExchangeOrdersDetailsHouse" %>
<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductExchangeOrdersDetailsForDamaged.ascx" TagPrefix="uc1" TagName="PageManageProductExchangeOrdersDetailsForDamaged" %>
<%@ Register Src="~/Shaerd/CPanelManageExchangeOrders/PageManageProductExchangeSortExchangeOrders.ascx" TagPrefix="uc1" TagName="PageManageProductExchangeSortExchangeOrders" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        <%--function DisableButton() {
            document.getElementById("<%=btnSearch.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;--%>
    </script>

    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <link href="../css/chosen.css" rel="stylesheet" />

    <style>
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 250px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText2 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }

            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }
        }

        @media screen and (min-width: 768px) {
            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis24 {
                Width: 95%;
            }
        }

        .checkbox label input[type="checkbox"] {
            display: none;
        }

            .checkbox label input[type="checkbox"] + .cr > .cr-icon {
                opacity: 0;
            }

            .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon {
                opacity: 1;
            }

            .checkbox label input[type="checkbox"]:disabled + .cr {
                opacity: .5;
            }
    </style>

    <script type="text/javascript">
        function insertConfirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageManageSortExchangeOrders.aspx">قائمة فرز أوامر الصرف</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة فرز أوامر الصرف
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="panel-body">
                            <span><i class="fa fa-star"></i> حدد نوع أومار الصرف </span>
                            <br />
                            <asp:RadioButton ID="RBTathith" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RBTathith_CheckedChanged" />
                            <span>فرز أوامر السلل الغذائية - الأجهزة الكهربائية - تأثيث المنازل </span>
                            <br />
                            <asp:RadioButton ID="RPTarmem" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPTarmem_CheckedChanged" />
                            <span>فرز أوامر بناء المنازل - ترميم المنازل </span>
                            <br />
                            <asp:Panel ID="PnlTalef" runat="server" Visible="false">
                                <asp:RadioButton ID="RPTalef" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPTalef_CheckedChanged" />
                                <span>فرز أوامر التالف </span>
                                <br />
                            </asp:Panel>
                            <asp:RadioButton ID="RPSupportForPrisms" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPSupportForPrisms_CheckedChanged" />
                            <span>فرز أوامر صرف الدعم المالي </span>
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                            <asp:Panel ID="pnlSelect" runat="server">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <div align="center">
                                    <h3 style="font-size: 20px">يرجى تحديد أمر الصرف
                                    </h3>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                            </asp:Panel>
                        </div>
                        <div id="IDTathith" runat="server" visible="false" dir="rtl">
                            <uc1:PageManageProductExchangeOrdersDetails runat="server" ID="PageManageProductExchangeOrdersDetails" />
                        </div>
                        <div id="IDTarmem" runat="server" visible="false" dir="rtl">
                            <uc1:PageManageProductExchangeOrdersDetailsHouse runat="server" ID="PageManageProductExchangeOrdersDetailsHouse" />
                        </div>
                        <div id="IDTalef" runat="server" visible="false" dir="rtl">
                            <uc1:PageManageProductExchangeOrdersDetailsForDamaged runat="server" ID="PageManageProductExchangeOrdersDetailsForDamaged" />
                        </div>
                        <div id="IDPrisms" runat="server" visible="false" dir="rtl">
                            <uc1:PageManageProductExchangeSortExchangeOrders runat="server" ID="PageManageProductExchangeSortExchangeOrders" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <br />
        <br />
        <br />
</asp:Content>

