<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_ERP_FMS_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <%--<div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px; background-color: #8b0101; color: #f5f5f5">
                    <h3 style="font-family: 'Alwatan';">نسخة تجريبية
                    </h3>
                </div>
            </div>--%>
            <div class="col-sm-12">
                <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                    <span class="badge badge-pill badge-warning">تحذير</span>
                    <span>يرجى تفعيل المصادقة الثنائية لزيادة حماية حسابك 
                        <a href="<%=ResolveUrl("~/Cpanel/CHome/PageGoogleAuthenticator.aspx")%>">من هنا ... </a>
                    </span>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </div>
            <div class="row" style="margin: 5px; text-align: center">
                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                    <br />
                    <h3 style="font-family: 'Alwatan';">نظام إدارة المالية - FMS
                    </h3>
                    <br />
                </div>
            </div>
            <%--            <div class="col-lg-3 col-md-3 col-sm-6">
                <a href='<%=ResolveUrl("~/Cpanel/ERP/CRM/PageCompany/PageCompany.aspx")%>' data-toggle="tooltip" title="الداعمين">
                    <div class="tile tile-heading" style="border-radius: 8px">
                        <div class="tile-body">
                            <h4>عدد الداعمين : 
                                <asp:Label ID="lblCountCompany" runat="server"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </a>
            </div>--%>
            <div class="clearfix"></div>
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
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div5" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/CHome/")%>" data-toggle="tooltip" title="رجوع للبوابة الإلكتروني" target="_blank">
                    <div class="tile">
                        <div class="tile-heading">
                            رجوع للبوابة الإلكتروني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-share"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="clearfix"></div>
            <div class="row">
            </div>
        </div>
</asp:Content>

