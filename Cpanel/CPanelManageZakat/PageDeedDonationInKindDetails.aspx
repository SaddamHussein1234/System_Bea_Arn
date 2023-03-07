<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageZakat/MPCPanel.master" AutoEventWireup="true" CodeFile="PageDeedDonationInKindDetails.aspx.cs" Inherits="Cpanel_CPanelManageZakat_PageDeedDonationInKindDetails" %>

<%@ Register Src="~/Shaerd/CPanelManageZakat/PageView.ascx" TagPrefix="uc1" TagName="PageView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" />

    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <div>
                        مشروع :
                        <asp:DropDownList ID="DL_ProjectNew" runat="server" ValidationGroup="GBill" CssClass="WidthText20">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="DL_ProjectNew" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        الإرشيف :
                        <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="GBill" CssClass="WidthText20">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="ddlYears" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnSearch" runat="server" Text="بحث" OnClick="btnSearch_Click" class="btn btn-info pull-right" data-toggle="tooltip"
                            ValidationGroup="GBill" title="بحث" Style="margin-right: 4px" />
                        &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم الفاتورة ... " ValidationGroup="GBill"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="txtSearch" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageDeedDonationInKindDetails.aspx">تفاصيل الفاتورة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="ProductByUser">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="فاتورة سند تبرع عيني"></asp:Label>
                    </h3>
                    <div style="float: left">
                        <asp:LinkButton ID="LbRefreshSaraf" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LbRefreshSaraf_Click"
                            title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                        <asp:LinkButton ID="LBPrintSaraf" runat="server" class="btn btn-success" data-toggle="tooltip" ValidationGroup="DLType"
                            title="طباعة" OnClick="LBPrintSaraf_Click">
                    <i class="fa fa-print"></i></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" Visible="false"
                            OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="panel-body">
                    <div align="left">
                        <a runat="server" id="ID_Edit" class="btn btn-info" visible="false">الذهاب إلى وضع التعديل <span class="fa fa-edit"></span></a>
                        <asp:CheckBox ID="CBViewKHatm" runat="server" Text="_عرض الختم" Checked="true" AutoPostBack="true" OnCheckedChanged="CBViewKHatm_CheckedChanged" />
                    </div>
                    <div class="col-sm-12">
                        <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-warning">تحذير</span>
                                <asp:Label ID="lblMessageWarning" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-success">عملية ناجحة</span>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <hr style='border: solid; border-width: 1px; width: 100%' />
                    <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                        <uc1:PageView runat="server" ID="PageView" />
                    </asp:Panel>
                </div>
            </div>
        </div>
        <style type="text/css">
            .modal-open {
                overflow: hidden
            }

            .modal {
                position: fixed;
                top: 0;
                right: 0;
                bottom: 0;
                left: 0;
                z-index: 1050;
                display: none;
                overflow: hidden;
                -webkit-overflow-scrolling: touch;
                outline: 0;
                background-color: hsla(120, 3%, 82%, 0.30);
            }
        </style>
</asp:Content>

