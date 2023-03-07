<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/Count/MPCPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Cpanel_Count_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" />
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
                    <h3 style="font-family: 'Alwatan';">إحصاء الملفات المؤرشفة
                    </h3>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-6" runat="server">
                    <a href="LogOut.aspx" data-toggle="tooltip" title="خروج">
                        <div class="tile">
                            <div class="tile-heading">
                                خروج <span class="pull-right"></span>
                            </div>
                            <div class="tile-body">
                                <i class="fa fa-sign-out"></i>
                                <h2 class="pull-right"></h2>
                            </div>
                            <div class="tile-footer"></div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <div class="loading2" align="center" id="lodi">
            <div>
                <img src="/Img/Logo.png" width="200" style="background-color: #349301; padding: 5px; border-radius: 4px" />
                <br />
                <span style="background-color: #349301; padding: 5px; border-radius: 4px">يرجى الإنتظار , جاري تنفيذ المهام</span><br />
                <br />
                <img src="../loader.gif" alt="" />
            </div>
        </div>
</asp:Content>

