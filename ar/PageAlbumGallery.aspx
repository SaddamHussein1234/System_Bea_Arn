<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="PageAlbumGallery.aspx.cs" Inherits="ar_PageAlbumGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <meta name="url" content="<%= "https://" + HFLink.Value + HFQuery.Value %>" />

    <meta property="og:description" content="<%= HFDescrption.Value %>" />
    <meta property="og:title" content="<%= HFTitle.Value %>" />
    <meta property="og:type" content="site" />
    <meta property="og:url" content="<%= "https://" + HFLink.Value + HFQuery.Value %>" />
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
    <meta name="twitter:url" content="<%= "https://" + HFLink.Value + HFQuery.Value %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body  onload="window.scroll(0, 270)">
    <div class="section_header original" style="background: url('/Themes/Ar_Qader/images/section_header_background.jpg')">
        <div class="container">
            <!-- Description -->
            <div class="section_description">
                <h2>معرض الصور</h2>
                <h1>
                    <asp:Label ID="lblNameAlbum" runat="server"></asp:Label>
                </h1>
            </div>

            <!-- Breadcrumbs -->
            <div class="breadcrumb_container">
                <ul class="breadcrumb">
                    <i class="fas fa-bookmark"></i>
                    <li><a href='/ar/'>الرئيسية</a></li>
                    <li><a href="PageAlbum.aspx">معرض الصور</a></li>
                </ul>
            </div>
        </div>
    </div>

    <!-- Start Container -->
    <div class="body_container">
        <div class="container inner">
            <!-- Home Page -->
            <div class="row">
                <div class="col-md-9">
                    <div class="page_subtitle margin-bottom-20 margin-bottom-progressive"> 
                        معرض الصور 
                         <i class="fas fa-images"></i> <small>عدد الصور : 
                                        <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label></small>
                    </div>
                    <div class="row grid-container">
                        <asp:Repeater ID="RPTAlbumImg" runat="server" Visible="false">
                            <ItemTemplate>
                                <div class="col-md-3 col-sm-3 col-xs-3 grid-item">
                                    <div class="image_block1">
                                        <a data-fancybox="images" href='<%# "../" + Eval("PathImg") %>'>
                                            <div preload="true" class="image_container" style="background-image: url('<%# "../" + Eval("PathImg") %>')"></div>
                                            <div class="overlay"><i class='fas fa-search-plus'></i></div>
                                        </a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Panel ID="PNLNull" runat="server" Visible="false">
                            <br />
                            <br />
                            <br />
                            <h2 align="center" style="color: #F0F0F0">لا توجد بيانات
                            </h2>
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-md-3 side-column">
                    <div class="margin-bottom margin-bottom-progressive">
                        <a id="IDAlbum" runat="server" data-fancybox="images" >
                            <img runat="server" id="ImgAlbum" preload="true" class="side-image center-block" />
                        </a>
                    </div>
                    <div runat="server" id="IDOtherAlbum" visible="false" class="margin-bottom margin-bottom-progressive">
                        <div class="page_subtitle">شاهد ايضا</div>
                        <nav id="side_menu">
                            <ul>
                                <asp:Repeater ID="RPTOtherAlbum" runat="server">
                                    <ItemTemplate>
                                        <li><a href='PageAlbumGallery.aspx?ID=<%# Eval("IDItem") %>&IDX=<%# Eval("RandomUniq") %>'><%# Eval("TitleAlbumAr") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </nav>
                    </div>
                    <div class="share_block">
                        <div class="page_subtitle"><small>مشاركة</small></div>
                        <div id="share"></div>
                    </div>
                    <script>
                        var shares = (typeof window.orientation !== "undefined" ? ["email", "twitter", "facebook", "linkedin", "pinterest", "messenger", "whatsapp", "viber"] : ["email", "twitter", "facebook", "linkedin", "pinterest", "whatsapp"]);
                        $("#share").jsSocials({
                            url: decodeURIComponent("<%= "https://" + HFLink.Value + HFQuery.Value%>"),
                            showCount: false,
                            showLabel: false,
                            shareIn: "popup",
                            text: "<%= lblNameAlbum.Text %>",
                            shares: shares
                        });
                    </script>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HFNameSite" runat="server" />
    <asp:HiddenField ID="HFTitle" runat="server" />
    <asp:HiddenField ID="HFDescrption" runat="server" />
    <asp:HiddenField ID="HFKeyWord" runat="server" />
    <asp:HiddenField ID="HFImage" runat="server" />
    <asp:HiddenField ID="HFLink" runat="server" />
    <asp:HiddenField ID="HFQuery" runat="server" />
    </body>
</asp:Content>

