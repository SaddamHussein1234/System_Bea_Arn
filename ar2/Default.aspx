<%@ Page Title="" Language="C#" MasterPageFile="~/ar2/MPAr.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ar_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 29%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
                padding: 5px;
            }
        }
    </style>
    <link href="../view/AlBarakah/AlBarakah.css" rel="stylesheet" />
    <link href="../view/sites/default/files/css/css_wq7sHoDzaBIE35go1iXZWBnlbGLo7Ej5ANYbHFjeIjc.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- The modal is in the header-2.php file -->
    <!-- This script let the modal only show when clicked for recording-->
    <script>
        $("#speak-btn").click(
          function (event) {
              var value = $('#speak-btn').attr('class');
              if (value === 'voice-btn:hover') {
                  $("#speak-btn").removeAttr('data-toggle');
              } else if (value === 'voice-btn') {
                  $("#speak-btn").attr('data-toggle', 'modal');
              }
          }
        );
    </script>

    <script>
        var input = document.getElementById("searchTxt");
        function searchURL() {
            window.location = window.location.origin + '/ar' + "/PageSearch.aspx?Title=" + input.value;
        }
    </script>
    <!-- Preloader -->
    <div class="preloader" id="preloader-logo">
        <div class="loader">
            <div class="loader-logo">
                <img src="../view/sites/all/themes/mlsd2019/assets/img/Logo.png" alt="logo" style="width: 150px" />
            </div>
            <div class="loader-figure"></div>
            <p class="loader-label">
                جمعية البر والخدمات الإجتماعية بأرن       
            </p>
        </div>
    </div>

    <div class="main-slider">
        <div class="carousel-gr"></div>
        <div class="carousel carousel-fade slide" data-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item">
                    <img src="../view/sites/default/files/01_3_10000.jpg" />
                </div>
            </div>
        </div>
        <div class="main-slider-content">
            <div class="main-slider-content-tb">
                <div class="carousel-item-caption">
                    <div class="vision-logo" style="margin-top:-150px">
                        <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="vision" style="width: 40%; height: 40%" />
                    </div>
                    <p class="carousel-item-sub-header" style="font-size: 28px">
                        <%--المـوقـع التفـاعلـي الأول مـن نـوعـه علـى مستـوى الجمعيـات الخيـريــة--%>
                    </p>
                    <a href="../Cpanel/" class="btn btn-info" style="border-radius: 8px; background-color:rgb(33, 130, 42); margin-top:5px" target="_blank">
                        <i class="fa fa-lock"></i> لدخول البوابة الإلكترونية التفاعلية الشاملة
                    </a>
                     <a href="PageContact-Us.aspx" class="btn btn-info" style="border-radius:8px; background-color:rgb(33, 130, 42); margin-top:5px">هل لديك إقتراح / فكرة / شكوى / نقد بناء ؟</a>
                    <br />
                    <a href="PageContact-Us.aspx" class="btn btn-info" style="border-radius:12px; background-color:rgb(33, 130, 42); margin-top:5px"><i class='fa fa-envelope'></i> إتصل بنا</a>
                    <a href="PageSoon.aspx" class="btn btn-info" style="border-radius:12px; background-color:rgb(33, 130, 42); margin-top:5px"><i class="fa fa-plus"></i> تسجيل مستفيد</a>
                </div>
                <div class="highlight-menu">
                    <ul>
                        <li>
                            <a class="highlight-item">
                                <i class="fa fa-globe" style="font-size: 40px; color: #980202;"></i>
                                <span>الخدمات الإلكترونية </span>
                            </a>
                            <ul>
                                <li>
                                    <a class="highlight-item" href=''>
                                        <span><i class="fa fa-user"></i>لأعضاء مجلس الإدارة</span>
                                    </a>
                                </li>
                                <li>
                                    <a class="highlight-item" href=''>
                                        <span><i class="fa fa-user"></i>لأعضاء الجمعية العمومية </span>
                                    </a>
                                </li>
                                <li>
                                    <ng-switch ng-animate=""></ng-switch>
                                    <l class="highlight-item">
                                        <span><i class="fa fa-user"></i> للموظفين </span>
                                    </l>
                                </li>
                                <li>
                                    <a class="highlight-item" href="">
                                        <span><i class="fa fa-user"></i>للمستفيدين </span>
                                    </a>
                                </li>
                                <li>
                                    <a class="highlight-item" href="">
                                        <span><i class="fa fa-user"></i>لغير المستفيدين </span>
                                    </a>
                                </li>
                                <li>
                                    <a class="highlight-item" href="">
                                        <span><i class="fa fa-money-bill"></i>البوابة الإلكترونية للشفافية والايضاح المالي </span>
                                    </a>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <a class="highlight-item">
                                <i class="fa fa-desktop" style="font-size: 40px; color: rgb(33, 130, 42);"></i>
                                <span>برامج الجمعية </span>
                            </a>

                        </li>
                        <li>
                            <a class="highlight-item">
                                <i class="fa fa-list" style="font-size: 40px; color: #057093;"></i>
                                <span>اللوائح والأنظمة </span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <hr style="color: rgb(33, 130, 42)" />
    <!-- section help   -->
    <section class="section section-help">
        <div class="container px-md-0">
            <div class="row">
                <div class="col-md-12">
                    <div id="carouselNoteControls" class="carousel carousel-fade  slide" data-ride="carousel">
                        <div class="carousel-inner">
                            <div class="carousel-item active">
                                <a href="" class="note-item">
                                    <img src="../view/image/logo.png" alt="Lodaing ... " />
                                </a>
                            </div>
                            <div class="carousel-item ">
                                <a href="" class="note-item">
                                    <img src="../view/image/logo.png" alt="Lodaing ... " />
                                </a>
                            </div>
                            <div class="carousel-item ">
                                <a href="" class="note-item">
                                    <img src="../view/image/logo.png" alt="Lodaing ... " />
                                </a>
                            </div>
                        </div>
                        <ol class="carousel-indicators">
                            <li data-target="#carouselNoteControls" data-slide-to="0" class="active"></li>
                            <li data-target="#carouselNoteControls" data-slide-to="1"></li>
                            <li data-target="#carouselNoteControls" data-slide-to="2"></li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- end section help   -->

    <hr style="color: rgb(33, 130, 42)" />
    <!-- section-most-news-->
    <section class="section section-most-news">
        <div class="container px-md-0 ">
            <div class="row">
                <div class="col-lg-6   col-md-12 position-relative">
                    <h3 class="heading right">
                        <a href="/ar/news">آخر الأخبار </a></h3>
                    <div class="owlCarousel-news-slider">
                        <div class="owl-carousel owl-theme">

                            <div class="item">
                                <article class="media-item">
                                    <a href="" class="media-image">
                                        <img typeof="foaf:Image" class="img-responsive" src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " width="278" height="190" title="" title="" />
                                    </a>
                                    <h3 class="media-content">
                                        <a href="">مثال خبر ... الخ</a>
                                    </h3>
                                    <div class="media-meta">
                                        <span class="meta-item date">الجمعة, 23 جمادى الثانية 1440</span>
                                    </div>
                                </article>
                            </div>

                            <div class="item">
                                <article class="media-item">
                                    <a href="" class="media-image">
                                        <img typeof="foaf:Image" class="img-responsive" src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " width="278" height="190" title="" />
                                    </a>
                                    <h3 class="media-content">
                                        <a href="">مثال خبر ... الخ</a>
                                    </h3>
                                    <div class="media-meta">
                                        <span class="meta-item date">الخميس, 22 جمادى الثانية 1440</span>
                                    </div>
                                </article>
                            </div>

                            <div class="item">
                                <article class="media-item">
                                    <a href="" class="media-image">
                                        <img typeof="foaf:Image" class="img-responsive" src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " width="278" height="190" title="" />
                                    </a>
                                    <h3 class="media-content">
                                        <a href="">مثال خبر ... الخ</a>
                                    </h3>
                                    <div class="media-meta">
                                        <span class="meta-item date">الخميس, 15 جمادى الثانية 1440</span>
                                    </div>
                                </article>
                            </div>

                            <div class="item">
                                <article class="media-item">
                                    <a href="" class="media-image">
                                        <img typeof="foaf:Image" class="img-responsive" src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " width="278" height="190" title="" />
                                    </a>
                                    <h3 class="media-content">
                                        <a href="">مثال خبر ... الخ</a>
                                    </h3>
                                    <div class="media-meta">
                                        <span class="meta-item date">الخميس, 15 جمادى الثانية 1440</span>
                                    </div>
                                </article>
                            </div>

                            <div class="item">
                                <article class="media-item">
                                    <a href="" class="media-image">
                                        <img typeof="foaf:Image" class="img-responsive" src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " width="278" height="190" title="" />
                                    </a>
                                    <h3 class="media-content">
                                        <a href="">مثال خبر ... الخ</a>
                                    </h3>
                                    <div class="media-meta">
                                        <span class="meta-item date">الخميس, 15 جمادى الثانية 1440</span>
                                    </div>
                                </article>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-12">
                    <h3 class="heading right"><a href="">آخر الإعلانات  </a></h3>
                    <div class="announcement-box">
                        <div class="announcement-cont  ">

                            <article class="media-item">
                                <div class="media-meta">
                                    <a href="">مثال اعلان ... الخ</a>
                                </div>
                                <h3 class="media-content">
                                    <a href="">مثال خبر ... الخ</a>
                                </h3>
                            </article>

                            <article class="media-item">
                                <div class="media-meta">
                                    <span class="meta-item date">الأحد, 18 جمادى الثانية 1440</span>
                                </div>
                                <h3 class="media-content">
                                    <a href="">مثال اعلان ... الخ</a>
                                </h3>
                            </article>

                            <article class="media-item">
                                <div class="media-meta">
                                    <span class="meta-item date">الأحد, 18 جمادى الثانية 1440</span>
                                </div>
                                <h3 class="media-content">
                                    <a href="">مثال اعلان ... الخ</a>
                                </h3>
                            </article>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6   col-md-12 ">
                    <h3 class="heading right">
                        <a href="">تغريدات تويتر </a></h3>
                    <div class="owlCarousel-news-slider">
                        <a class="twitter-timeline" data-lang="ar" data-height="500" data-theme="light" data-link-color="#047c11" href="https://twitter.com/ber_arn?ref_src=twsrc%5Etfw">Tweets by ber_arn</a>
                        <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>
                    </div>
                </div>
                <div class="col-lg-6 col-md-12 position-relative">
                    <h3 class="heading right"><a href="">منشوارات الفيسبوك</a></h3>
                    <div class="owlCarousel-news-slider">
                        <iframe src="https://www.facebook.com/plugins/page.php?href=https%3A%2F%2Fwww.facebook.com%2F%D8%AC%D9%85%D8%B9%D9%8A%D8%A9-%D8%A7%D9%84%D8%A8%D8%B1-%D9%88%D8%A7%D9%84%D8%AE%D8%AF%D9%85%D8%A7%D8%AA-%D8%A7%D9%84%D8%A7%D8%AC%D8%AA%D9%85%D8%A7%D8%B9%D9%8A%D8%A9-%D8%A8%D8%A3%D8%B1%D9%86-809338099164386%2F&tabs=timeline&width=500&height=500&small_header=true&adapt_container_width=true&hide_cover=false&show_facepile=true&appId" 
                            width="500" height="500" style="border:none;overflow:hidden" scrolling="no" frameborder="0" allowTransparency="true" allow="encrypted-media"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- end section-most-news-->
    <hr style="color: rgb(33, 130, 42)" />

    <!-- section-clients-->
    <section class="section section-clients">
        <div class="container px-md-0">
            <h2 class="heading">الخدمات الإلكترونية</h2>
        </div>
        <div class="container px-md-0">
            <div class="item WidthTex">
                <div class="client-box">
                    <div class="srv-logo">
                        <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Loding ... ">
                    </div>
                    <h3 class="srv-title">الخدمات الإلكترونية
                        <br />
                        لأعضاء مجلس الإدارة</h3>
                    <!-- </a> -->
                    <ul class="srv-tags">
                    </ul>
                    <div class="srv-content">
                        <div class="srv-btns">
                            <a href="" class="btn srv-more">التفاصيل</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item WidthTex">
                <div class="client-box">
                    <div class="srv-logo">
                        <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " />
                    </div>
                    <h3 class="srv-title">الخدمات الإلكترونية
                        <br />
                        لأعضاء الجمعية العمومية</h3>
                    <!-- </a> -->
                    <ul class="srv-tags">
                    </ul>
                    <div class="srv-content">
                        <div class="srv-btns">
                            <a href="" class="btn srv-more">التفاصيل</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item WidthTex" style="padding-bottom: 5px">
                <div class="client-box">
                    <div class="srv-logo">
                        <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Loding ... ">
                    </div>
                    <h3 class="srv-title">الخدمات الإلكترونية
                        <br />
                        للموظفين</h3>
                    <!-- </a> -->
                    <ul class="srv-tags">
                    </ul>
                    <div class="srv-content">
                        <div class="srv-btns">
                            <a href="" class="btn srv-more">التفاصيل</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item WidthTex">
                <div class="client-box">
                    <div class="srv-logo">
                        <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " />
                    </div>
                    <h3 class="srv-title">الخدمات الإلكترونية
                        <br />
                        للمستفيدين</h3>
                    <!-- </a> -->
                    <ul class="srv-tags">
                    </ul>
                    <div class="srv-content">
                        <div class="srv-btns">
                            <a href="" class="btn srv-more">التفاصيل</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item WidthTex">
                <div class="client-box">
                    <div class="srv-logo">
                        <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Loding ... ">
                    </div>
                    <h3 class="srv-title">الخدمات الإلكترونية
                        <br />
                        لغير المستفيدين</h3>
                    <!-- </a> -->
                    <ul class="srv-tags">
                    </ul>
                    <div class="srv-content">
                        <div class="srv-btns">
                            <a href="" class="btn srv-more">التفاصيل</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item WidthTex">
                <div class="client-box">
                    <div class="srv-logo">
                        <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" alt="Lodaing ... " />
                    </div>
                    <h3 class="srv-title">البوابة الإلكترونية
                        <br />
                        للشفافية والايضاح المالي</h3>
                    <!-- </a> -->
                    <ul class="srv-tags">
                    </ul>
                    <div class="srv-content">
                        <div class="srv-btns">
                            <a href="" class="btn srv-more">التفاصيل</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix visible-sm-block"></div>
        </div>
    </section>
    <!--  end section-clients-->
    <hr style="color: rgb(33, 130, 42)" />

    <asp:Panel ID="IDObjectivesFoundation" runat="server">
        <section class="section section-clients">
            <div class="container px-md-0">
                <h2 class="heading">مجالات عمل الجمعية</h2>
            </div>
            <div class="container px-md-0">
                <div class="sppb-row">
                    <asp:Repeater ID="RPTObjectivesFoundation" runat="server">
                        <ItemTemplate>
                            <div class="item WidthTex " style="height: 135px">
                                <div class="service ColorContent" style="padding: 5px 10px 15px 0">
                                    <div class="icon"><i class="fa fa-leaf" style="font-size: 30px"></i></div>
                                    <h5 class="heading-with-icon" style="color: #151515"><%# Eval("Title") %></h5>
                                    <h6 class="heading-with-icon "><%# Eval("Details") %></h6>
                                </div>
                            </div>
                            <%--<div class="clearfix visible-sm-block"></div>--%>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clearfix visible-sm-block"></div>
                    <br />
                </div>
            </div>
        </section>
    </asp:Panel>
    <script defer="defer" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyATcTe6_Ujsix8SbD5qNTzU6DjFjvt11X0&amp;callback=initMap"></script>
    <script src="../view/sites/default/files/js/js_8FRl3nRJx6n1D9bQJC2ebuA32SXnw91n09ESpdkNdkg.js"></script>
    <script src="../view/sites/default/files/js/js_FbpwIZNwgzwEuuL4Q2HOM07BOSCY5LxL_gwSK4ohQBM.js"></script>
</asp:Content>

