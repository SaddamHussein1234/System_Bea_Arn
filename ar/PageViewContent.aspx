<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="PageViewContent.aspx.cs" Inherits="ar_PageViewContent" enableEventValidation="false" %>

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
                <h2>
                    <asp:Label ID="lblName" runat="server"></asp:Label></h2>
                <h1><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
            </div>

            <!-- Breadcrumbs -->
            <div class="breadcrumb_container">
                <ul id="IDMulti" runat="server" class="breadcrumb" visible="false">
                    <i class="fas fa-bookmark"></i>
                    <li><a href='/ar/'>الرئيسية</a></li>
                </ul>
                <asp:Repeater ID="RPTTitle" runat="server" Visible="false">
                    <ItemTemplate>
                        <ul class="breadcrumb">
                            <i class="fas fa-bookmark"></i>
                            <li><a href='/ar/'>الرئيسية</a></li>
                            <li><%# Eval("TitleArticle") %></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!-- Start Container -->
    <div class="body_container">
        <div class="container inner">
            <div id="pnlOneArticle" runat="server" class="row">
                <div class="col-md-9">
                    <div class="page_container margin-bottom-20 margin-bottom-progressive">
                        <div class="html_content">
                            <div runat="server" visible="false">
                                <strong style="line-height: 22.75px;">
                                    <span style="font-size: 16px; color: #236fa1;">
                                        <img style="line-height: 28px; display: block; margin-left: auto; margin-right: auto;"
                                            runat="server" id="ImgMain" alt="" width="640" height="334" />
                                    </span>
                                </strong>
                            </div>
                            <hr />
                            <div style="text-align: justify;">
                                <asp:Label ID="lblDetails" runat="server"></asp:Label>
                            </div>
                            <hr />
                            <h6 class="heading-with-icon"><span class="fa fa-calendar"></span>
                                <asp:Label ID="lblDateAdd" runat="server"></asp:Label>
                                <span class="fa fa-minus"></span>
                                 <span>عدد المشاهدات : </span>
                                <span class="fa fa-eye"></span>
                                <asp:Label ID="lblCountViews" runat="server"></asp:Label>
                            </h6>
                            <hr />
                            <asp:Label ID="lblAttach" runat="server" Text="المرفقات"></asp:Label>
                            <asp:Repeater ID="RPTPath" runat="server" Visible="false">
                                <ItemTemplate>                                          
                                    <%# Library_CLS_Arn.Saddam.ClassSaddam.FGetPath((string) Eval("AttachFile")) %>
                                    <br />
                                    <asp:Button ID="blnAttach" runat="server" Text="تحميل الملف" CssClass="submit"
                                        OnClick="blnAttach_Click" CommandArgument='<%# Eval("AttachFile") %>' />
                                </ItemTemplate>
                            </asp:Repeater>
                            <hr />
                        </div>
                    </div>
                </div>
                <div class="col-md-3 side-column">
                    <div class="margin-bottom margin-bottom-progressive">
                        <a data-fancybox="images" runat="server" id="IDMain">
                            <img preload="true" class="side-image center-block" runat="server" id="ImgMain2" /></a>
                    </div>

                    <div class="margin-bottom margin-bottom-progressive">
                        <div class="page_subtitle">إقرأ أيضا</div>
                        <nav id="side_menu">
                            <ul>
                                <asp:Repeater ID="RPTPar" runat="server">
                                    <ItemTemplate>
                                        <li><a href='PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>'><%# Eval("TitleArticle") %></a></li>
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
                            url: decodeURIComponent("<%= "https://" + HFLink.Value + HFQuery.Value %>"),
                            showCount: false,
                            showLabel: false,
                            shareIn: "popup",
                            text: "<%= lblTitle.Text %>",
                            shares: shares
                        });
                    </script>
                </div>
            </div>
            <div class="row grid-container-10">
                <asp:Repeater ID="RPTViewContentNetwork" runat="server" Visible="false">
                    <ItemTemplate>
                        <div class="col-md-3 col-sm-6 grid-item">
                            <a class="generic_block1" href='PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>'>
                                <div class="image_container" preload="true" 
                                    style="background-image: url('/<%# Eval("ImgArt") %>')">
                                    <div class="overlay">
                                        <div>إقرأ المزيد</div>
                                    </div>
                                </div>
                                <div class="content_container">
                                    <h2><%# Eval("TitleArticle") %></h2>
                                    <small><%# Eval("DateAddArticle", "{0:ddd , dd-MMM-yyyy}") %></small> - <small><i class="fa fa-eye"></i> <%# Eval("CountViews") %></small>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="PNLNullNetwork" runat="server" Visible="false">
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
            <!-- Main Content -->
            <div class="page_container">                                   
                <asp:Repeater ID="RPTViewContentList" runat="server" Visible="false">
                    <ItemTemplate>
                        <div class="file_container">
                            <div>
                                <i class="fas fa-angle-left"></i>
                                <h3><%# Eval("TitleArticle") %> </h3>
                            </div>
                            <a class="btn btn-default btn-sm download_button" style="<%# FCheckNullFile(Eval("AttachFile").ToString()) %>"
                                href="/<%# Eval("AttachFile") %>" data-file="pdf"
                                data-fancybox data-type="iframe">
                                <i class="fas fa-file-pdf"></i>
                                <div><span>عرض الملف</span></div>
                            </a>
                            <a class="btn btn-default btn-sm download_button" style="<%# FCheckNotNullFile(Eval("AttachFile").ToString()) %>" data-file="pdf">
                                <div><span>بدون ملف مرفق</span></div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="PNLNullList" runat="server" Visible="false">
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
    <asp:HiddenField ID="HFQuery" runat="server" />
</asp:Content>

