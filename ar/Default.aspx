<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ar_Default" %>

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
    <meta name="url" content="<%= "https://" + HFLink.Value + "/ar/" %>" />

    <meta property="og:description" content="<%= HFDescrption.Value %>" />
    <meta property="og:title" content="<%= HFTitle.Value %>" />
    <meta property="og:type" content="site" />
    <meta property="og:url" content="<%= "https://" + HFLink.Value + "/ar/" %>" />
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
    <meta name="twitter:url" content="<%= "https://" + HFLink.Value + "/ar/" %>" />

    <link href="/Themes/Ar_Qader/Content/modern-ticker.css?v=1.24.0" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body onload="window.scroll(0, 60)">
    <div class="ticker1 modern-ticker" id="IDPar" runat="server">
        <div class="mt-body">
            <div class="mt-news">
                <ul>
                    <asp:Repeater ID="RPTNews" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>' target="_self" style="color: #FFF;"><%# Eval("TitleArticle") %></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="mt-controls">
                <div class="fas mt-prev"></div>
                <div class="mt-play"></div>
                <div class="fas mt-next"></div>
            </div>
        </div>
    </div>
    <script src="/Themes/Ar_Qader/Scripts/jquery.modern-ticker.min.js?v=1.24.0"></script>
    <script type="text/javascript">
        $(function () {
            $(".ticker1").modernTicker({
                effect: "scroll", scrollType: "continuous", scrollStart: "inside", scrollInterval: 20, transitionTime: 500, autoplay: true
            });
            $(".scrollContent").mCustomScrollbar({
                theme: "light"
            });
        })
    </script>
    <div id="IDSlide" class="module_slider1" style="" runat="server" visible="false">
        <div id='slider1' class="swiper-container">
            <div class="swiper-wrapper">
                <asp:Repeater ID="RPTSlide" runat="server">
                    <ItemTemplate>
                        <div class="swiper-slide">
                            <div class='swiper-slide-container'>
                                <div class='image'>
                                    <a href='<%# Eval("_Link_Click_") %>' target="_blank">
                                        <img preload=true src='/<%# Eval("ImgArt") %>' alt="" style="width:100%">
                                    </a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="swiper-button-next original"></div>
            <div class="swiper-button-prev original"></div>
        </div>
        <div class="swiper-pagination original"></div>
    </div>

    <script type="text/javascript">
        var slider_swiper = new Swiper("#slider1", {
            slidesPerView: 1,
            spaceBetween: 0,
            loop: true,
            autoHeight: true,
            pagination: {
                el: ".module_slider1 .swiper-pagination",
                clickable: true,
            },
            navigation: {
                nextEl: ".module_slider1 .swiper-button-next",
                prevEl: ".module_slider1 .swiper-button-prev",
            },
            autoplay: {
                delay: 4000,
            }
        });

        slider_swiper.on("slideChange", function () {
            animateSliderComponents(slider_swiper.realIndex);
        });

        function animateSliderComponents(index) {
            var swiperObject = $(".module_slider1").find("[data-swiper-slide-index=" + index + "]");
            swiperObject.find("[data-animation]").each(function () {
                animateCSS($(this)[0], $(this).attr("data-animation"));
            });
        }
    </script>

    <div class="module_custom module original" style="">
        <div class="container container-basic">
            <div style="height:30px"></div>
            <!-- Title -->
            <!-- Content -->
            <div class="row grid-container-15" style="justify-content:flex-start; align-items:stretch; flex-wrap:wrap">
                <div class="col-md-6 col-sm-6 grid-item">
                    <div class='html_content'>
                        <p><span style="font-size: 20px;"><strong><i class="fas fa-star"></i> نبذة عنا</strong></span></p>
                        <p>&nbsp;</p>
                        <p>
                            <strong>
                                <span style="color: #236fa1; font-size: 18px;">
                                    <asp:Label ID="lblAbout" runat="server"></asp:Label>
                                </span>
                            </strong>
                            <br /><br />
                            <span style="font-size: 14px; color: #ffffff;">
                                <asp:Label ID="lblAbout2" runat="server"></asp:Label>
                            </span>
                        </p>
                    </div>
                </div>
                <div class="col-md-6 col-sm-6 grid-item">
                    <div class="module_inline_services1 inline_module">
                        <!-- Title -->
                        <div class='module_description'>
                            <h1 data-aos=fade-down style=''>روابط سريعة</h1>
                        </div>
                        <!-- Content -->
                        <div class=module_content data-aos=fade-in>
                            <div id=inline_services1 class=swiper-container>
                                <div class=swiper-wrapper>
                                    <div class="swiper-slide service_block_container" data-aos='zoom-in'>
                                        <a class="service_block service_block1" href="PageAbout.aspx">
                                            <i class="fas fa-book"></i>
                                            <div>نبذة عنا</div>
                                            <span class="fas fa-book"></span>
                                        </a>
                                    </div>
                                    <div class="swiper-slide service_block_container" data-aos='zoom-in'>
                                        <a class="service_block service_block1" href="#pnlMobadarah">
                                            <i class="fas fa-handshake"></i>
                                            <div>المبادرات</div>
                                            <span class="fas fa-handshake"></span>
                                        </a>
                                    </div>
                                    <div class="swiper-slide service_block_container" data-aos=zoom-in>
                                        <a class="service_block service_block1" href="PageViewcontent.aspx?ID=6062&Uniq=9c73998e-8ef2-40f5-8c80-0646634936c6">
                                            <i class="fas fa-newspaper"></i>
                                            <div>أخبار الجمعية</div>
                                            <span class="fas fa-newspaper"></span>
                                        </a>
                                    </div>
                                    <div class="swiper-slide service_block_container" data-aos=zoom-in>
                                        <a class="service_block service_block1" href="PageAlbum.aspx">
                                            <i class="fas fa-images"></i>
                                            <div>معرض الصور</div>
                                            <span class="fas fa-images"></span>
                                        </a>
                                    </div>
                                    <div class="swiper-slide service_block_container" data-aos=zoom-in>
                                        <a class="service_block service_block1" href="PageVideo.aspx">
                                            <i class="fas fa-video"></i>
                                            <div>مكتبة الفيديو</div>
                                            <span class="fas fa-video"></span>
                                        </a>
                                    </div>
                                    <div class="swiper-slide service_block_container" data-aos=zoom-in>
                                        <a class="service_block service_block1" href="javaScript:void(0)">
                                            <i class="fab fa-youtube"></i>
                                            <div>البث المباشر</div>
                                            <span class="fab fa-youtube"></span>
                                        </a>
                                    </div>
                                    <div class="swiper-slide service_block_container" data-aos=zoom-in>
                                        <a class="service_block service_block1" href="PageContact-Us.aspx">
                                            <i class="fas fa-mobile-alt"></i>
                                            <div>إتصل بنا</div>
                                            <span class="fas fa-mobile-alt"></span>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-button-next original"></div>
                            <div class="swiper-button-prev original"></div>
                            <div class="swiper-pagination original"></div>
                            <div class=more_buttons><a class="btn btn-primary btn-sm" href="javaScript:void(0)">عرض المزيد</a></div>
                            <script>
                                var swiper = new Swiper("#inline_services1", {
                                    slidesPerView: Math.max(Math.round($(".module_inline_services1").width() / 200), 1),
                                    spaceBetween: 10,
                                    centerInsufficientSlides: true,
                                    pagination: {
                                        el: ".module_inline_services1 .swiper-pagination",
                                        clickable: true,
                                    },
                                    navigation: {
                                        nextEl: ".module_inline_services1 .swiper-button-next",
                                        prevEl: ".module_inline_services1 .swiper-button-prev",
                                    },
                                    breakpoints: {
                                        446: { slidesPerView: 1 }
                                    }
                                });
                            </script>

                        </div>

                    </div>
                </div>
            </div>
            <!-- Buttons -->
            <div style="height:30px"></div>
        </div>
    </div>

    <div id="IDSite" runat="server" visible="false" class="module_custom module original">
        <div class="container container-basic">
            <!-- Title -->
            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <%--<li data-target="#myCarousel" data-slide-to="1"></li>--%>
                </ol>
                <!-- Indicators -->
                <div class="container">
                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">
                        <asp:Repeater ID="RPTSite" runat="server">
                            <ItemTemplate>
                                <div class="item active ">
                                    <div class="col-md-3">
                                        <div class="IconArtical Priv Style_Manager" style="background-image: url('../<%# Eval("ImgArt") %>'); background-repeat: no-repeat; background-size: 100% 100%;">
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="IconArtical fontService">
                                            <p class="FontRight labelColorTow FontSize25">
                                                <%# Eval("TitleArticle") %>
                                            </p>
                                            <p class="FontRight">
                                                <%# FText((string) Eval("DetailsArticle")).ToString().PadRight(200).Substring(0,800).TrimEnd() %>
                                                            
                                                                ... 
                                                                <a href="PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>" title="<%# Eval("TitleArticle") %>">عرض المزيد</a>

                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="module_custom module original" style="">
        <div class="container container-basic">
            <div style="height:30px"></div>
            <!-- Title -->
            <div class="module_description default">
                <h1 data-aos=fade-down style=''>المركز الإعلامي</h1>	<h3 data-aos=fade-in style=''>شاهد احدث الصور و الفيديوهات</h3>
            </div>
            <!-- Content -->
            <div class="row grid-container-15" style="justify-content:flex-start; align-items:stretch; flex-wrap:wrap">
                <div class="col-md-5 col-sm-5 grid-item">
                    <div class="module_inline_gallery1 inline_module">
                        <!-- Title -->
                        <div class=module_description>
                            <h1 data-aos=fade-down style=''>معرض الصور</h1>
                        </div>
                        <!-- Content -->
                        <div class="module_content" data-aos=fade-in>
                            <!-- Load Images -->
                            <!-- Swiper -->
                            <div class="swiper-container gallery-top">
                                <div class="swiper-wrapper">
                                    <asp:Repeater ID="RPTAlbumImages" runat="server">
                                        <ItemTemplate>
                                            <a href='/<%# Eval("PathImg") %>' data-fancybox class='swiper-slide' style='background-image: url(/<%# Eval("PathImg") %>)'></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="swiper-button-next original"></div>
                                <div class="swiper-button-prev original"></div>
                            </div>
                            <!-- Thumbnails -->
                            <div class="swiper-container gallery-thumbs">
                                <div class="swiper-wrapper">
                                    <asp:Repeater ID="RPTAlbumImagesmin" runat="server">
                                        <ItemTemplate>
                                            <div class=swiper-slide style='background-image: url(/<%# Eval("PathImg") %>)'></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <script type="text/javascript">
                                var galleryTop = new Swiper(".module_inline_gallery1 .gallery-top", {
                                    spaceBetween: 10,
                                    navigation: {
                                        nextEl: ".module_inline_gallery1 .swiper-button-next",
                                        prevEl: ".module_inline_gallery1 .swiper-button-prev",
                                    },
                                });
                                var galleryThumbs = new Swiper(".module_inline_gallery1 .gallery-thumbs", {
                                    spaceBetween: 0,
                                    centeredSlides: true,
                                    slidesPerView: "auto",
                                    touchRatio: 0.2,
                                    slideToClickedSlide: true,
                                });
                                galleryTop.controller.control = galleryThumbs;
                                galleryThumbs.controller.control = galleryTop;
                            </script>
                            <div class="more_buttons no_padding"><a class="btn btn-primary btn-sm" href="PageAlbum.aspx">عرض المزيد</a></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4 col-sm-4 grid-item">
                    <div class="module_inline_videos1 inline_module">
                        <!-- Title -->
                        <div class=module_description>
                            <h1 data-aos=fade-down style=''>مكتبة الفيديو</h1>
                        </div>
                        <!-- Content -->
                        <div class=module_content data-aos=fade-in>
                            <asp:Repeater ID="RPTVideosLast" runat="server">
                                <ItemTemplate>
                                    <a class='video' data-fancybox='videos' href='<%# Eval("VideoSrc") %>'>
                                        <div class="image_container" preload="true" style="background-image: url('https://i.ytimg.com/vi/<%# Library_CLS_Arn.Saddam.ClassSaddam.FGetIconYoutube(Eval("VideoSrc").ToString()) %>/mqdefault.jpg')"></div>
                                        <div class=content_container>
                                            <h2 class=single_line><%# Eval("VideoNameAr") %></h2>
                                            <small><%# Eval("DateAddVideo", "{0:ddd , dd-MMM-yyyy}") %></small>
                                        </div>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="more_buttons no_padding"><a class="btn btn-primary btn-sm" href="PageVideo.aspx">عرض المزيد</a></div>

                        </div>

                    </div>
                </div>
                <div class="col-md-3 col-sm-6 grid-item">
                    <div class="module_inline_twitter1 inline_module">
                        <!-- Title -->
                        <div class=module_description>
                            <h1 data-aos=fade-down style=''>تابعنا علي تويتر</h1>
                        </div>
                        <!-- Content -->
                        <div class=module_content data-aos=fade-in>
                            <a class="twitter-timeline" data-chrome="noheader nofooter" data-height="400" href="https://twitter.com/ber_arn?ref_src=twsrc%5Etfw"></a>
                            <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Buttons -->
            <div class='module_buttons delay-half' style='position:relative; margin-top:30px !important' fullpage-animation='fadeIn'>
                <a class='btn btn-primary btn-sm' href="PageAlbum.aspx">عرض معرض الصور</a>
                <a class='btn btn-primary btn-sm' href="PageVideo.aspx">عرض مكتبة الفيديو</a>
            </div>
            <div style="height:30px"></div>
        </div>
    </div>
    <script src="/Themes/Ar_Qader/plugins/number-scroller.min.js?v=1.24.0"></script>

    <div runat="server" id="IDStatistical" class="module_counter1 module negative BackgroundAllOpacity">
        <div class=container>
            <!-- Title -->
            <div class="module_description default">
                <h1 data-aos=fade-down style='color:#ffffff'>الإحصائيات</h1>	<h3 data-aos=fade-in style='color:#f7f7f7'>تعرف علي إحصائية الجمعية</h3>
            </div>
            <!-- Content -->
            <div class='module_content' data-aos='fade-in'>
                <div class="row grid-container-15" style="justify-content:center">
                    <asp:Repeater ID="RPTStatistical" runat="server" OnPreRender="RPTStatistical_PreRender">
                        <ItemTemplate>
                            <div class="col-md-six col-sm-3 col-xs-6 col-6 grid-item">
                                <div class='counter_block'>
                                    <i class="fas fa-tv"></i>
                                    <%# (Eval("HalatMostafeed").ToString() == "معيل_أسره") ? " <span> مُعيلي أسر </span> " 
                                        : (Eval("HalatMostafeed").ToString() == "مطلقه") ? "<span> مطلقات </span>"
                                        : (Eval("HalatMostafeed").ToString() == "بلا_معيل") ? "<span> بلا معيل </span>"
                                        : (Eval("HalatMostafeed").ToString() == "اسرة_سجين") ? "<span> أسر سجناء </span>"
                                        : (Eval("HalatMostafeed").ToString() == "ارمله") ? "<span> أرامل </span>"
                                        : (Eval("HalatMostafeed").ToString() == "ايتام") ? "<span> أيتام </span>"
                                        : (Eval("HalatMostafeed").ToString() == "ربة_منزل") ? "<span> ربة منزل </span>"
                                        : (Eval("HalatMostafeed").ToString() == "معيلة_أسره") ? "<span> معيلة أسره </span>"
                                        : ""
                                    %>
                                    
                                </div>
                                <div class="col-sm-6 col-xs-6 col-6">
                                    <div align="center" style="font-size: 30px; font-weight: bold; direction: ltr; line-height: 1; color: #fff">
                                        <span style="font-size: 15px;">أسرة</span>
                                        <div class='numscroller' counter-suffix="+" counter-min="0" counter-delay="3" counter-increment="5"
                                            counter-max="<%# (Eval("HalatMostafeed").ToString() != "ايتام") ?  FGetCount(Convert.ToInt32(Eval("IDItem"))).ToString()  
                                            : FGetMostafeedByHalafAlMosTafeed() %>">

                                            <asp:Label ID="lblCountAosra" runat="server" Text='
                                                <%# (Eval("HalatMostafeed").ToString() != "ايتام") ?  FGetCount(Convert.ToInt32(Eval("IDItem"))).ToString()  
                                                : FGetMostafeedByHalafAlMosTafeed()
                                                %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-xs-6 col-6">
                                    <div align="center" style="font-size: 30px; font-weight: bold; direction: ltr; line-height: 1; color: #fff">
                                        <span style="font-size: 15px;">فرد</span>
                                        <div class='numscroller' counter-suffix="+" counter-min="0" counter-delay="3" counter-increment="5"
                                            counter-max="<%# (Eval("HalatMostafeed").ToString() != "ايتام") ?  FGetCuntAfradAlOsrah(Convert.ToInt32(Eval("IDItem"))).ToString()
                                            : FGetCuntAfradAlOsrahByAitaam(Convert.ToInt32(Eval("IDItem"))) %>">

                                            <asp:Label ID="lblCountMostafeed" runat="server" Text='
                                                 <%# (Eval("HalatMostafeed").ToString() != "ايتام") ?  FGetCuntAfradAlOsrah(Convert.ToInt32(Eval("IDItem"))).ToString()
                                                    : FGetCuntAfradAlOsrahByAitaam(Convert.ToInt32(Eval("IDItem")))
                                                    %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="col-md-six col-sm-3 col-xs-6 col-6 grid-item">
                        <div class='counter_block'>
                            <i class="fas fa-tv"></i>
                            <span>الإجمالي</span>
                        </div>
                        <div class="col-sm-6 col-xs-6 col-6">
                            <div align="center" style="font-size: 30px; font-weight: bold; direction: ltr; line-height: 1; color: #eee">
                                <span style="font-size: 15px;">الأسر</span>
                                <div class='numscroller' counter-suffix="+" counter-min="0" counter-delay="3" counter-increment="5"
                                    counter-max="<%= HFCountAosra.Value %>">
                                    <%= HFCountAosra.Value %>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 col-xs-6 col-6">
                            <div align="center" style="font-size: 30px; font-weight: bold; direction: ltr; line-height: 1; color: #eee">
                                <span style="font-size: 15px;">الأفراد</span>
                                <div class='numscroller' counter-suffix="+" counter-min="0" counter-delay="3" counter-increment="5"
                                    counter-max="<%= HFCountMostafeed.Value %>">
                                    <%= HFCountMostafeed.Value %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-six col-sm-3 col-xs-6 col-6 grid-item hide">
                        <div class=counter_block>
                            <i class="fas fa-tv"></i>
                            <span>عدد الزوار</span><br />
                            <div class='numscroller' counter-suffix="+" counter-min="0" counter-delay="10" counter-increment="15" counter-max="<%= FGetCountVisit() %>"><%= FGetCountVisit() %></div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Buttons -->
            <div class=module_buttons><a class='btn btn-default-transparent btn-sm' href="PageAbout.aspx">نبذة عنا</a><a class='btn btn-default-transparent btn-sm' href="PageContact-Us.aspx">إتصل بنا</a></div>
        </div>
    </div>

    <div class="module_custom module original" style="">
        <div class="container container-basic">
            <div style="height:30px"></div>
            <!-- Title -->
            <!-- Content -->
            <div class="row grid-container-15" style="justify-content:flex-start; align-items:stretch; flex-wrap:wrap">
                <div class="col-md-7 col-sm-7 grid-item">
                    <div class="module_inline_news1 inline_module">

                        <!-- Title -->
                        <div class=module_description>
                            <h1 data-aos=fade-down style=''>آخر الأخبار</h1>
                        </div>
                        <!-- Content -->
                        <div class=module_content data-aos=fade-in>
                            <div class="row grid-container">
                                <div class="col-md-6 grid-item news_block_large">
                                    <a runat="server" id="IDNewsLast" data-aos=flip-right>
                                        <div class='image_container' preload=true style="background-image: url('/<%= XImgNewsLast %>')">
                                            <div class=overlay>
                                                <div>إقرأ المزيد</div>
                                            </div>
                                        </div>
                                        <div class=content_container>
                                            <h2 class=single_line>
                                                <asp:Label ID="lblTitleNewsLast" runat="server"></asp:Label>
                                            </h2>
                                            <small>
                                                <asp:Label ID="lblDateNewsLast" runat="server"></asp:Label>
                                            </small>
                                        </div>
                                    </a>
                                </div>
                                <div class="col-md-6 grid-item news_block_small">
                                    <asp:Repeater ID="RPTPar" runat="server">
                                        <ItemTemplate>
                                            <a href='PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>' data-aos='fade-left'>
                                                <div class="image_container" preload="true" 
                                                    style="background-image: url('/<%# Eval("ImgArt") %>')"></div>
                                                <div class='content_container'>
                                                    <h2 class='single_line'><%# Eval("TitleArticle") %></h2>
                                                    <small><%# Eval("DateAddArticle", "{0:ddd , dd-MMM-yyyy}") %></small>
                                                </div>
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="more_buttons no_padding"><a class="btn btn-primary btn-sm" href="javaScript:void(0)">اقرأ المزيد</a></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5 col-sm-5 grid-item">
                    <div class="module_inline_testimonials1 inline_module">
                        <!-- Title -->
                        <div class=module_description>
                            <h1 data-aos=fade-down style=''>قالوا عنا</h1>
                        </div>
                        <!-- Content -->
                        <div class=module_content data-aos=fade-in>
                            <div class=no_content><h2>عفوا، لا يوجد محتوي في الوقت الحالي</h2>إنتظرونا قريبا مع المزيد من التحديثات..</div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Buttons -->
            <div style="height:30px"></div>
        </div>
    </div>

    <div class="module_custom module original BackgroundAllOpacity">
        <div class="container container-basic">
            <div style="height:30px"></div>
            <!-- Title -->
            <!-- Content -->
            <div class="row grid-container-15" style="justify-content:flex-start; align-items:stretch; flex-wrap:wrap">
                <div class="col-md-12 col-sm-12 grid-item">
                    <div class=html_content>
                        <div style="text-align: center; line-height: 36px;">
                            <span style="font-size: 20px; color: #ecf0f1;"><strong>تواصل معنا<br /></strong></span>
                            <span style="font-size: 16px; color: #ecf0f1;">إذا كان لديك أي إستفسار</span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Buttons -->
            <div class='module_buttons delay-half' style='position:relative; margin-top:30px !important'
                 fullpage-animation=fadeIn>
                <a class='btn btn-default-transparent btn-sm' href="PageAbout.aspx">نبذة عنا</a>
                <a class='btn btn-default-transparent btn-sm' href="PageContact-Us.aspx">إتصل بنا</a>
            </div>
            <div style="height:30px"></div>
        </div>
    </div>

    <div id="IDAlbum" runat="server" class="module_gallery1 module original">
        <div class=container>
            <!-- Title -->
            <div class="module_description default">
                <h1 data-aos=fade-down style=''>معرض الصور</h1>	<h3 data-aos=fade-in style=''>شاهد احدث الصور</h3>
            </div>
            <!-- Content -->
            <div class=module_content data-aos=fade-in>
                <div class=swiper_box>
                    <div id=gallery1 class=swiper-container>
                        <div class=swiper-wrapper>
                            <asp:Repeater ID="RPTAlbumArabic" runat="server">
                                <ItemTemplate>
                                    <div class=swiper-slide>
                                        <div class=module_content_block data-aos=zoom-in>
                                            <a class=gallery_block1 href='PageAlbumGallery.aspx?ID=<%# Eval("IDItem") %>&IDX=<%# Eval("RandomUniq") %>' title='<%# Eval("TitleAlbumAr") %>'>
                                                <div class='image_container' preload=true style="background-image: url(/<%# Eval("imgAlbum") %>)">
                                                    <div class=count><b><%# FCountImg((Int32) Eval("IDItem")) %></b><span>صورة</b></div>
                                                </div>
                                                <div class=content_container>
                                                    <h2><%# Eval("TitleAlbumAr") %> </h2>
                                                    <small><%# Eval("DateAddAlbum", "{0:ddd , dd-MMM-yyyy}") %></small>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="swiper-button-next original"></div>
                    <div class="swiper-button-prev original"></div>
                </div>
                <div class="swiper-pagination original"></div>
                <script>
                    var swiper = new Swiper("#gallery1", {
                        slidesPerView: 4,
                        spaceBetween: 0,
                        centerInsufficientSlides: true,
                        pagination: {
                            el: ".module_gallery1 .swiper-pagination",
                            clickable: true,
                        },
                        navigation: {
                            nextEl: ".module_gallery1 .swiper-button-next",
                            prevEl: ".module_gallery1 .swiper-button-prev",
                        },
                        breakpoints: {
                            992: { slidesPerView: 3 },
                            768: { slidesPerView: 2 },
                            446: { slidesPerView: 1 }
                        }
                    });
                </script>
            </div>
            <!-- Buttons -->
            <div class=module_buttons><a class='btn btn-primary btn-sm' href="PageAlbum.aspx">عرض معرض الصور</a></div>
        </div>
    </div>

    <div id="IDAlbumVideo" runat="server" class="module_videos3 module original">
        <div class=container>
            <!-- Title -->
            <div class="module_description default">
                <h1 data-aos=fade-down style=''>مكتبة الفيديو</h1>	<h3 data-aos=fade-in style=''>شاهد احدث الفيديوهات</h3>
            </div>
            <!-- Content -->
            <div class=module_content data-aos=fade-in>
                <div class=swiper_box>
                    <div id=videos3 class=swiper-container>
                        <div class=swiper-wrapper>
                            <asp:Repeater ID="RPTAlbumVideo" runat="server">
                                <ItemTemplate>
                                    <div class=swiper-slide>
                                        <div class=module_content_block data-aos=zoom-in>
                                            <a class=videos_block1 data-fancybox=videos href='<%# Eval("VideoSrc") %>'>
                                                <div class="image_container" preload="true" style="background-image: url('https://i.ytimg.com/vi/<%# Library_CLS_Arn.Saddam.ClassSaddam.FGetIconYoutube(Eval("VideoSrc").ToString()) %>/mqdefault.jpg')">
                                                    <i class="fas fa-play"></i>
                                                </div>
                                                <div class=content_container>
                                                    <h2 class=single_line><%# Eval("VideoNameAr") %></h2>
                                                    <small><%# Eval("DateAddVideo", "{0:ddd , dd-MMM-yyyy}") %></small>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="swiper-button-next original"></div>
                        <div class="swiper-button-prev original"></div>
                    </div>
                    <div class="swiper-pagination original"></div>
                    <script>
                        var swiper = new Swiper("#videos3", {
                            slidesPerView: 5,
                            spaceBetween: 0,
                            centeredSlides: true,
                            loop: true,
                            pagination: {
                                el: ".module_videos3 .swiper-pagination",
                                clickable: true,
                            },
                            navigation: {
                                nextEl: ".module_videos3 .swiper-button-next",
                                prevEl: ".module_videos3 .swiper-button-prev",
                            },
                            breakpoints: {
                                768: { slidesPerView: 3 },
                                446: { slidesPerView: 1 }
                            }
                        });
                    </script>
                </div>
                <!-- Buttons -->
                <div class=module_buttons><a class='btn btn-primary btn-sm' href="PageVideo.aspx">عرض كافة الفيديوهات</a></div>
            </div>
        </div>
    </div>

    <div id="IDPartner" runat="server" class="module_partners1 module original" style="">
        <div class=container>
            <!-- Title -->
            <div class="module_description default">
                <h1 data-aos=fade-down style=''>شركاء النجاح</h1><%--<h3 data-aos=fade-in style=''>شركاء النجاح، سعداء دائما بخدمتكم</h3>--%>
            </div>
            <!-- Content -->
            <div class='module_content' data-aos=fade-in>
                <div class='swiper_box'>
                    <div id='partners1' class='swiper-container'>
                        <div class='swiper-wrapper'>
                            <asp:Repeater ID="RPTPartner" runat="server">
                                <ItemTemplate>
                                    <div class=swiper-slide>
					                    <a class='partner_block' data-aos=fade-in target="_blank" href='<%# Eval("PathClick") %>' title="<%# Eval("TitlePartnerAr") %>" data-toggle="tooltip">
						                    <img src='/<%# Eval("ImgPartner") %>'><b><%# Eval("TitlePartnerAr") %></b>
					                    </a>
				                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="swiper-button-next original"></div>
                    <div class="swiper-button-prev original"></div>
                </div>
                <div class="swiper-pagination original"></div>
                <script>
                    var swiper = new Swiper("#partners1", {
                        slidesPerView: 5,
                        spaceBetween: 0,
                        centerInsufficientSlides: true,
                        pagination: {
                            el: ".module_partners1 .swiper-pagination",
                            clickable: true,
                        },
                        navigation: {
                            nextEl: ".module_partners1 .swiper-button-next",
                            prevEl: ".module_partners1 .swiper-button-prev",
                        },
                        breakpoints: {
                            992: { slidesPerView: 3 },
                            768: { slidesPerView: 2 },
                            446: { slidesPerView: 1 }
                        }
                    });
                </script>
            </div>
            <!-- Buttons -->
            <%--<div class=module_buttons><a class='btn btn-primary btn-sm' href="PagePartners.aspx">عرض كافة الشركاء</a></div>--%>
        </div>
    </div>

    <asp:HiddenField ID="HFTitle" runat="server" />
    <asp:HiddenField ID="HFDescrption" runat="server" />
    <asp:HiddenField ID="HFKeyWord" runat="server" />
    <asp:HiddenField ID="HFImage" runat="server" />
    <asp:HiddenField ID="HFLink" runat="server" />
    <asp:HiddenField ID="HFCount" runat="server" />
    <asp:HiddenField ID="HFCountAosra" runat="server" />
    <asp:HiddenField ID="HFCountMostafeed" runat="server" />


    </body>
</asp:Content>

