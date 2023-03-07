<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="PageSoon.aspx.cs" Inherits="ar_PageSoon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <body  onload="window.scroll(0, 270)">
    <div class="section_header original" style="background: url('/Themes/Ar_Qader/images/section_header_background.jpg');">
        <div class="container">
            <!-- Description -->
            <div class="section_description">
                <h1>سنعود قريباً</h1>
            </div>
            <!-- Breadcrumbs -->
            <div class="breadcrumb_container">
                <ul class="breadcrumb">
                    <i class="fas fa-bookmark"></i>
                    <li><a href='/ar/'>الرئيسية</a></li>
                    <li>سنعود قريباً</li>
                </ul>
            </div>
        </div>
    </div>
    <!-- Start Container -->
    <div class="body_container">
        <div class="container inner">
            <div class="row">
                <div class="col-md-12">
                    <div class="page_container">
                        <!-- ========== Sign-Up ========== -->
                        <div align="center">
                            <br />
                            <br />
                            <br />
                            <i class="glyphicon glyphicon-warning-sign fa-5x"></i>
                            <h2 class="text-center">هذه الصفحة قيد التطوير سيتم الإنتهاء منها قريباً <i class="fa fa-file"></i>
                            </h2>
                            <h3 class="text-center">فريق تقنية المعلومات بالجمعية ,,, 
                            </h3>
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnOk" runat="server" Text="حسناً" OnClick="btnOk_Click" 
                                Style="border-radius: 8px" class="submit" />
                            <br />
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </body>
</asp:Content>

