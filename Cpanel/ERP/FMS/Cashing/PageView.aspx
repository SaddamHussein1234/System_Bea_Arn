<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageView.aspx.cs" Inherits="Cpanel_ERP_FMS_Cashing_PageView" %>

<%@ Register Src="~/Shaerd/ERP/OM/Cashing/PageView.ascx" TagPrefix="uc1" TagName="PageView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    <li><a href="">بيانات سند القبض</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="ProductByUser">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="بيانات سند القبض"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                        <uc1:PageView runat="server" ID="PageView" />
                    </asp:Panel>
                </div>
            </div>
        </div>
</asp:Content>

