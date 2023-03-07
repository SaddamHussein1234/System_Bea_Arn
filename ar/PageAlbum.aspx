<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="PageAlbum.aspx.cs" Inherits="ar_PageAlbum" %>

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
    <meta name="url" content="<%= "https://" + HFLink.Value + "/ar/PageAlbum.aspx" %>" />

    <meta property="og:description" content="<%= HFDescrption.Value %>" />
    <meta property="og:title" content="<%= HFTitle.Value %>" />
    <meta property="og:type" content="site" />
    <meta property="og:url" content="<%= "https://" + HFLink.Value + "/ar/PageAlbum.aspx" %>" />
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
    <meta name="twitter:url" content="<%= "https://" + HFLink.Value + "/ar/PageAlbum.aspx" %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <body  onload="window.scroll(0, 270)">
    <div class="section_header original" style="background: url('/Themes/Ar_Qader/images/section_header_background.jpg')">
        <div class="container">
            <!-- Description -->
            <div class="section_description">
                <h1>معرض الصور</h1>
            </div>
            <!-- Breadcrumbs -->
            <div class="breadcrumb_container">
                <ul class="breadcrumb">
                    <i class="fas fa-bookmark"></i>
                    <li><a href='/ar/'>الرئيسية</a></li>
                    <li>معرض الصور</li>
                </ul>
            </div>
        </div>
    </div>

    <!-- Start Container -->
    <div class="body_container">
        <div class="container inner">
            <!-- Home Page -->
            <div class="row grid-container">
                <asp:Repeater ID="RPTAlbumArabic" runat="server" Visible="false">
                    <ItemTemplate>
                        <div class="col-md-3 col-sm-6 grid-item">
                            <a class="gallery_block1" href="PageAlbumGallery.aspx?ID=<%# Eval("IDItem") %>&IDX=<%# Eval("RandomUniq") %>" title="<%# Eval("TitleAlbumAr") %>" data-toggle="tooltip">
                                <div class="image_container" preload="true" style="background-image: url(<%# "../" + Eval("imgAlbum") %>)">
                                    <div class="count"><b><%# FCountImg((Int32) Eval("IDItem")) %></b><span>صورة</b></div>
                                </div>
                                <div class="content_container">
                                    <h2><%# Eval("TitleAlbumAr") %></h2>
                                    <small><%# Eval("DateAddAlbum", "{0:ddd , dd-MMM-yyyy}") %></small>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlNull" runat="server" Visible="false">
                    <div class="contact-form-cont">
                        <h2 class="text-center">رسالة التحقق</h2>
                        <div id="contact-form" class="contact-form">
                            <div class="user-info-from-cookie contact-form">
                                <div>
                                    <br />
                                    <br />
                                    <br />
                                    <h2 class="text-center">لا توجد نتائج <i class="fa fa-trash-o"></i>
                                    </h2>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    </body>
    <asp:HiddenField ID="HFNameSite" runat="server" />
    <asp:HiddenField ID="HFTitle" runat="server" />
    <asp:HiddenField ID="HFDescrption" runat="server" />
    <asp:HiddenField ID="HFKeyWord" runat="server" />
    <asp:HiddenField ID="HFImage" runat="server" />
    <asp:HiddenField ID="HFLink" runat="server" />
</asp:Content>

