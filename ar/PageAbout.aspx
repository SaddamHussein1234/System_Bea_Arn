<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="PageAbout.aspx.cs" Inherits="ar_PageAbout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta name="theme-color" content="#373533" />
    <meta name="msapplication-navbutton-color" content="#373533" />
    <meta name="apple-mobile-web-app-status-bar-style" content="#373533" />

    <link rel="icon" type="image/png" href="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <link rel="apple-touch-icon-precomposed" href="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />

    <meta name="title" content="<%= HFTitle.Value %>" />
    <meta name="description" content="<%= HFDescrption.Value %>" />
    <meta name="keywords" content="<%= HFKeyWord.Value %>" />
    <meta name="url" content="<%= "https://" + HFLink.Value + "/ar/PageAbout.aspx" %>" />

    <meta property="og:description" content="<%= HFDescrption.Value %>" />
    <meta property="og:title" content="<%= HFTitle.Value %>" />
    <meta property="og:type" content="site" />
    <meta property="og:url" content="<%= "https://" + HFLink.Value + "/ar/PageAbout.aspx" %>" />
    <meta property="og:locale" content="ar_AR" />
    <meta property="og:locale:alternate" content="ar_AR" />
    <meta property="og:locale:alternate" content="en_US" />
    <meta property="og:image" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta property="og:image:width" content="600" />
    <meta property="og:image:height" content="300" />

    <meta name="twitter:description" content="<%= HFDescrption.Value %>" />
    <meta name="twitter:image" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="<%= HFTitle.Value %>" />
    <meta name="twitter:url" content="<%= "https://" + HFLink.Value + "/ar/PageAbout.aspx" %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <body onload="window.scroll(0, 270)">
        <div class="section_header original" style="background: url('/Themes/Ar_Qader/images/section_header_background.jpg')">
            <div class="container">
                <!-- Description -->
                <div class="section_description">
                    <h1>نبذة عنا</h1>
                </div>
                <!-- Breadcrumbs -->
                <div class="breadcrumb_container">
                    <ul class="breadcrumb">
                        <i class="fas fa-bookmark"></i>
                        <li><a href='/ar/'>الرئيسية</a></li>
                        <li>نبذة عنا</li>
                    </ul>
                </div>
            </div>
        </div>
        <!-- Start Container -->
        <div class="body_container">
            <div class="container inner">
                <div class="row">
                    <div class="col-md-6">
                        <div class="page_container">
                            <!-- ========== Sign-Up ========== -->
                            <div class="page_subtitle"><i class="fas fa-star"></i>نبذة عنا </div>
                            <div class="row page_container" style="text-align: justify;">
                                <asp:Label ID="lblAbout" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <div class="page_subtitle"><i class="fas fa-star"></i>الرسالة</div>
                            <div class="row page_container" style="text-align: justify;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <div class="page_subtitle"><i class="fas fa-star"></i>الرؤية</div>
                            <div class="row page_container" style="text-align: justify;">
                                <asp:Label ID="lblVision" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <div class="page_subtitle"><i class="fas fa-star"></i>القيم</div>
                            <div class="row page_container" style="text-align: justify;">
                                <asp:Label ID="lblValus" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <div class="page_subtitle"><i class="fas fa-star"></i>أهداف الجمعية</div>
                            <div class="row page_container" style="text-align: justify;">
                                <asp:Label ID="lblGoals" runat="server"></asp:Label>
                            </div>
                            <!-- ========== Reset Password ========== -->
                        </div>
                    </div>
                    <div class="col-md-6 side-column">
                        <div class="page_container">
                            <div class="page_subtitle">تواصل معنا</div>
                            <table class="contact_block1">
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-phone'></span></td>
                                    <td>
                                        <div class="number">
                                            <a runat="server" id="IDPhone">
                                                <asp:Label ID="lblPhone" runat="server" Style="background: none; color: #393939;" Font-Size="13px"></asp:Label>
                                            </a>
                                        </div>
                                    </td>
                                    <td width="40"><span class='glyphicon glyphicon-envelope'></span></td>
                                    <td>
                                        <a runat="server" id="IDEmail">
                                            <asp:Label ID="lblEmail" runat="server" Style="background: none; color: #393939;" Font-Size="13px"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-map-marker'></span></td>
                                    <td>
                                        <a runat="server" id="IDLocation" target="_blank">
                                            <asp:Label ID="lblLocation" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="الموقع على الخريطة"></asp:Label>
                                        </a>
                                    </td>
                                    <td width="40"><span class='glyphicon glyphicon-minus'></span></td>
                                    <td>
                                        <a runat="server" id="IDFacebook" target="_blank">
                                            <asp:Label ID="lblFacebook" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="سناب شات"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-minus'></span></td>
                                    <td>
                                        <a runat="server" id="IDtwitter" target="_blank">
                                            <asp:Label ID="lbltwitter" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="صفحة التويتر"></asp:Label>
                                        </a>
                                    </td>
                                    <td width="40"><span class='glyphicon glyphicon-minus'></span></td>
                                    <td>
                                        <a runat="server" id="IDyoutube" target="_blank">
                                            <asp:Label ID="lblyoutube" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="قناة اليوتيوب"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!-- Map -->
                    <hr />
                        <div class="page_container">
                        <iframe src="https://www.google.com/maps?width=100%25&amp;height=300&amp;hl=en&amp;coord=22.872833,+40.492484&amp;q=%D8%AC%D9%85%D8%B9%D9%8A%D8%A9+%D8%A7%D9%84%D8%A8%D8%B1+%D9%88%D8%A7%D9%84%D8%AE%D8%AF%D8%A7%D9%85%D8%AA+%D8%A7%D9%84%D8%A5%D8%AC%D8%AA%D9%85%D8%A7%D8%B9%D9%8A%D8%A9+%D8%A8%D8%A3%D8%B1%D9%86+(%D8%AC%D9%85%D8%B9%D9%8A%D8%A9+%D8%A7%D9%84%D8%A8%D8%B1+%D9%88%D8%A7%D9%84%D8%AE%D8%AF%D8%A7%D9%85%D8%AA+%D8%A7%D9%84%D8%A5%D8%AC%D8%AA%D9%85%D8%A7%D8%B9%D9%8A%D8%A9+%D8%A8%D8%A3%D8%B1%D9%86)&amp;ie=UTF8&amp;t=h&amp;z=16&amp;iwloc=B&amp;output=embed"
                            width="100%" height="300" style="border: 0;" allowfullscreen="" loading="lazy"></iframe>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
    <asp:HiddenField ID="HFTitle" runat="server" />
    <asp:HiddenField ID="HFDescrption" runat="server" />
    <asp:HiddenField ID="HFKeyWord" runat="server" />
    <asp:HiddenField ID="HFImage" runat="server" />
    <asp:HiddenField ID="HFLink" runat="server" />

</asp:Content>

