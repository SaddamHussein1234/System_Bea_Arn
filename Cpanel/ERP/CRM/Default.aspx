<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_ERP_CRM_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/GridView.css?v=2.2" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
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
                    <h3 style="font-family: 'Alwatan';">نظام الإستثمار وتنمية الموارد - Customer Relationship Management
                    </h3>
                    <br />
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-6">
                <asp:Panel ID="pnlData" runat="server" Visible="false" CssClass="alert alert-info" Height="100%" ScrollBars="Auto">
                    <asp:Repeater ID="RPT_Remamber_" runat="server">
                        <ItemTemplate>
                            <div class="alert  alert-<%# Library_CLS_Arn.Saddam.ClassSaddam.FCheckDateAgo((DateTime) (Eval("_Remamber_Date_"))) %> alert-dismissible" role="alert">
                                    
                                <span class="badge badge-pill badge-success">رسالة تذكير</span>
                                      <i class="fa fa-hospital-o"></i> <%# Eval("_Company_Name_")%>
                                    / <i class="fa fa-calendar"></i> <%# Library_CLS_Arn.Saddam.ClassSaddam.FChangeDate((DateTime) (Eval("_Remamber_Date_")))%>
                                    / <i class="fa fa-envelope"></i> <span style="color:#393939;">( <%# Eval("_Remamber_Desc_")%> )</span>
                                <asp:LinkButton ID="LBDelete" runat="server" CssClass="close"
                                    CommandArgument='<%# Eval("_ID_Item_") %>' OnClick="LBDelete_Click"
                                    OnClientClick="return confirm('هل تريد الإستمرار ؟ ');" Style="color: #b10505"
                                    data-toggle='tooltip' title='إخفاء التنبية'><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6">
                <a href='<%=ResolveUrl("~/Cpanel/ERP/CRM/PageCompany/PageCompany.aspx")%>' data-toggle="tooltip" title="الداعمين">
                    <div class="tile tile-heading" style="border-radius:8px">
                        <div class="tile-body">
                            <h4>عدد الداعمين : 
                                <asp:Label ID="lblCountCompany" runat="server"></asp:Label>
                            </h4>
                        </div>
                    </div>
                </a>
            </div>
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
            <div class="col-lg-3 col-md-3 col-sm-6" id="pnlMenuArn" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/ERP/CRM/PageSetting/PageCompanyType.aspx")%>" data-toggle="tooltip" title="أنواع شركات الدعم">
                    <div class="tile">
                        <div class="tile-heading">
                            أنواع شركات الدعم <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-tags"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div1" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/ERP/CRM/PageCompany/PageCompany.aspx")%>" data-toggle="tooltip" title="إدارة الداعمين">
                    <div class="tile">
                        <div class="tile-heading">
                            إدارة الداعمين <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-users"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div2" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/ERP/CRM/PageCompany/PageCompanyTricker.aspx")%>" data-toggle="tooltip" title="شركات تابعتها هذا الأسبوع">
                    <div class="tile">
                        <div class="tile-heading">
                            شركات تابعتها هذا الأسبوع <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-hospital-o"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div3" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/ERP/CRM/PageRemamber/PageRemamber.aspx")%>" data-toggle="tooltip" title="رسائل التذكير">
                    <div class="tile">
                        <div class="tile-heading">
                            رسائل التذكير <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-envelope"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div6" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/ERP/CRM/PageKind_Support/PageKind_Support.aspx")%>" data-toggle="tooltip" title="متابعة الداعمين">
                    <div class="tile">
                        <div class="tile-heading">
                            متابعة الداعمين <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-file"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div7" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/Cpanel/ERP/CRM/PageKind_Support/PageKind_SupportByDate.aspx")%>" data-toggle="tooltip" title="فواتير الدعم العيني">
                    <div class="tile">
                        <div class="tile-heading">
                            فواتير الدعم العيني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-cart-plus"></i>
                            <h2 class="pull-right"></h2>
                        </div>
                        <div class="tile-footer"></div>
                    </div>
                </a>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6" id="Div4" runat="server" visible="true">
                <a href="<%=ResolveUrl("~/ITSupport/")%>" data-toggle="tooltip" title="الدعم الفني" target="_blank">
                    <div class="tile">
                        <div class="tile-heading">
                            الدعم الفني <span class="pull-right"></span>
                        </div>
                        <div class="tile-body">
                            <i class="fa fa-desktop"></i>
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

