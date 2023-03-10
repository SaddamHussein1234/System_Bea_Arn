<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageView.aspx.cs" Inherits="Cpanel_ERP_OM_Cash_Donation_PageView" %>

<%@ Register Src="~/Shaerd/ERP/OM/Cash_Donation/PageView.ascx" TagPrefix="uc1" TagName="PageView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <div>
                        <a runat="server" id="ID_Edit" class="btn btn-info" visible="false">الذهاب إلى وضع التعديل <span class="fa fa-edit"></span></a>
                        السنة :
                        <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="VGDetails"
                            Height="25px" CssClass="form-control2" Style="font-size: 12px; height:36px;">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم الفاتورة ... "></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="بحث" OnClick="btnSearch_Click" class="btn btn-info"
                            data-toggle="tooltip" title="بحث"  />
                        <asp:LinkButton ID="LBPrintSaraf" runat="server" class="btn btn-success" data-toggle="tooltip" ValidationGroup="DLType"
                            title="طباعة" OnClick="LBPrintSaraf_Click">
                        <span class="fa fa-print"></span></asp:LinkButton>
                        <asp:LinkButton ID="LbRefreshSaraf" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LbRefreshSaraf_Click"
                            title="تحديث"><span class="fa fa-refresh"></span></asp:LinkButton>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="">إيصال تبرع نقدي</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="ProductByUser">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إيصال تبرع نقدي"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                        <uc1:PageView runat="server" id="PageView" />
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

