<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ar_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @media screen and (min-width: 768px) {
            .WidthFrame {
                width: 100%;
                height: 250px;
            }

            .fontService {
                font-size: 16px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthFrame {
                Width: 100%;
                height: 250px;
            }

            .fontService {
                font-size: 14px;
            }
        }

        .FontRight {
            text-align: right;
        }

        .labelColorTow {
            color: #27bac0;
            font-weight: bold;
        }

        .FontSize25 {
            font-size: 25px;
        }

        .IconArtical {
            text-align: center;
            width: 100%;
            margin-top: 40px;
        }

        .Priv {
            background: #27bac0;
            color: #fff;
        }

        .Style_Manager {
            border-radius: 30% 0 30% 30%;
            height: 250px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="main_body">
        <!-- Begin page content -->

        <div id="fb-root"></div>
        <script>
            (function (d, s, id) {
                var js, fjs = d.getElementsByTagName(s)[0];
                if (d.getElementById(id)) return;
                js = d.createElement(s); js.id = id;
                js.src = 'https://connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v3.2';
                fjs.parentNode.insertBefore(js, fjs);
            }(document, 'script', 'facebook-jssdk'));
        </script>
        <script src="../../Scripts/video.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                var isLoggedIn = "0";
                if (isLoggedIn == "0") {
                    $(".CompetitionsResult").attr("href", "/pages/eservices/eservices_login.aspx?src=/Pages/Projects/CompetitionsResult.aspx?PId=1");
                    $(".eservices").attr("href", "/pages/eservices/eservices_login.aspx?src=/pages/eservices/eservices.aspx");
                }
            });

            var ValidationSummaryOnSubmitOrig = ValidationSummaryOnSubmit;
            var ValidationSummaryOnSubmit = function () {
                var scrollToOrig = window.scrollTo;
                window.scrollTo = function () { };
                var retVal = ValidationSummaryOnSubmitOrig();
                window.scrollTo = scrollToOrig;
                return retVal;
            };
        </script>
        <script>
            $(document).ready(function () {
                $(".sidePopup").animate({ "margin-right": '+=' + $(".sidePopup").outerWidth() }, "slow");
                setTimeout(function () {
                    $(".sidePopup").animate({ "margin-right": '-=' + $(".sidePopup").outerWidth() }, "slow")
                }, 30000);
                $('.sidePopup .close').click(function () {
                    $(".sidePopup").animate({ "margin-right": '-=' + $(".sidePopup").outerWidth() }, "slow")

                });
            });
        </script>
        <div runat="server">
            <div class="home_galleryCon" id="IDSlide" runat="server">
                <div class="container">
                    <div id="home_gallery" class="carousel carousel-fade slide" data-interval="false">
                        <% Response.Write(FArnArticleByViewInSlideByFanction()); %>
                    </div>
                </div>
            </div>
            <section id="aboutSection">
                <div class="container">
                    <div class="owl-carousel owl-theme owl-branches">
                        <asp:Repeater ID="RPTLastNews" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <div class="branchItem">
                                        <a href="PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>" title="<%# Eval("TitleArticle") %>">
                                            <img class="service_img" src='<%# Library_CLS_Arn.Saddam.ClassSaddam.CheckImg((String) (Eval("ImgArt"))) %>' style="height: 50px" />
                                        </a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <section id="IDSite" runat="server" visible="false">
                        <div class="clearfix"></div>
                        <div class="about">
                            <div style="margin: 0 0 20px 10px;" id="myCarousel" class="carousel slide" data-ride="carousel">
                                <ol class="carousel-indicators">
                                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                                    <%--<li data-target="#myCarousel" data-slide-to="1"></li>
                                    <li data-target="#myCarousel" data-slide-to="2"></li>
                                    <li data-target="#myCarousel" data-slide-to="3"></li>
                                    <li data-target="#myCarousel" data-slide-to="4"></li>--%>
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
                                <!-- Left and right controls -->
                                <%--<a class="left carousel-control" href="#myCarousel" data-slide="prev">
                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                    <span class="sr-only">Previous</span>
                                </a>
                                <a class="right carousel-control" href="#myCarousel" data-slide="next">
                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                    <span class="sr-only">Next</span>
                                </a>--%>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </section>

                    <div class="about  ">
                        <h3 class="section_title"><a href="PageVision.aspx">عـن الـجـمـعـيـة</a></h3>
                        <p class="about_desc ">
                            <asp:Label ID="lblAbout" runat="server" CssClass="fontService"></asp:Label>
                        </p>
                        <div class="col-md-4 col-xs-12 col-sm-4">
                            <div class="aboutItem">
                                <i class="fa fa-eye"></i>
                                <h3><a>رؤيتنا</a></h3>
                                <p>
                                    <asp:Label ID="lblVision" runat="server" CssClass="fontService"></asp:Label>
                                </p>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-4 col-xs-12 col-sm-4">
                            <div class="aboutItem">
                                <i class="fa fa-bullseye"></i>
                                <h3><a>رسالتنا</a></h3>
                                <p>
                                    <asp:Label ID="lblMessage" runat="server" CssClass="fontService"></asp:Label>
                                </p>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-4 col-xs-12 col-sm-4">
                            <div class="aboutItem">
                                <i class="fa fa-align-center"></i>
                                <h3><a>قيمنا</a></h3>
                                <p>
                                    <asp:Label ID="lblValus" runat="server" CssClass="fontService"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="container about" style="padding-bottom: 20px">
                        <h3 class="section_title"><a href="">مواقع التواصل الإجتماعي</a></h3>
                        <div class="col-sm-6 col-xs-12">
                            <div class="twitterContainer">
                                <div class="twitter-title"><i class="fa fa-twitter"></i>تويتر </div>
                                <div class="twitter-body scrollContent">
                                    <a class="twitter-timeline " href="https://twitter.com/ber_arn?ref_src=twsrc%5Etfw"></a>
                                    <script async src="//platform.twitter.com/widgets.js" charset="utf-8"></script>
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-sm-4 col-xs-12">
                            <div class="facebookContainer">
                                <div class="fb-page" data-href="https://www.facebook.com/%D8%AC%D9%85%D8%B9%D9%8A%D8%A9-%D8%A7%D9%84%D8%A8%D8%B1-%D9%88%D8%A7%D9%84%D8%AE%D8%AF%D9%85%D8%A7%D8%AA-%D8%A7%D9%84%D8%A7%D8%AC%D8%AA%D9%85%D8%A7%D8%B9%D9%8A%D8%A9-%D8%A8%D8%A3%D8%B1%D9%86-809338099164386/" data-tabs="timeline" data-width="350px" data-height="250px" data-small-header="true" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="true">
                                    <blockquote cite="https://www.facebook.com/facebook" class="fb-xfbml-parse-ignore">
                                        <a href="https://www.facebook.com/facebook">Facebook</a>
                                    </blockquote>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-sm-6 col-xs-12">
                            <div class="youtubeContainer">
                                <iframe src="https://www.youtube.com/embed/d20RhiToPTI" class="WidthFrame"></iframe>
                            </div>
                        </div>

                    </div>
                </div>
            </section>
        </div>


        <%--<section id="partners_Proj_contactus">
        <div class="container">
            <div class="partners">
                <h3 class="section_title"><a href="#">شركاؤنا في التنمية</a></h3>
                <div id="partnersCarousel" class="carousel slide">
                    <!-- Carousel items -->
                    <div class="carousel-inner">
                        <!-- loop through the list -->
                        <div class="item active">
                            <div class="row">
                                
                                <div class="col-md-3 col-sm-3">
                                    <a target="_blank" href="https://www.rf.org.sa" class="thumbnail" title="مؤسسة سليمان بن عبد العزيز الراجحي الخيرية">
                                        <img src="../Themes/Images/page_title_bg1.png" alt="Image" style="max-width: 100%;">
                                    </a>
                                </div>
                                
                                <div class="col-md-3 col-sm-3">
                                    <a target="_blank" href="https://www.saudiaramco.com/ar/" class="thumbnail" title="أرامكو السعودية">
                                        <img src="../Themes/Images/page_title_bg1.png" alt="Image" style="max-width: 100%;">
                                    </a>
                                </div>
                                
                                <div class="col-md-3 col-sm-3">
                                    <a target="_blank" href="https://www.saib.com.sa/ar" class="thumbnail" title="البنك السعودي للاستثمار">
                                        <img src="../Themes/Images/page_title_bg1.png" alt="Image" style="max-width: 100%;">
                                    </a>
                                </div>
                                
                                <div class="col-md-3 col-sm-3">
                                    <a target="_blank" href="https://www.sipchem.com/ar/" class="thumbnail" title="سبكيم">
                                        <img src="../Themes/Images/page_title_bg1.png" alt="Image" style="max-width: 100%;">
                                    </a>
                                </div>

                                <div class="col-md-3 col-sm-3">
                                    <a target="_blank" href="https://www.sipchem.com/ar/" class="thumbnail" title="سبكيم">
                                        <img src="../Themes/Images/page_title_bg1.png" alt="Image" style="max-width: 100%;">
                                    </a>
                                </div>

                                <div class="col-md-3 col-sm-3">
                                    <a target="_blank" href="https://www.sipchem.com/ar/" class="thumbnail" title="سبكيم">
                                        <img src="../Themes/Images/page_title_bg1.png" alt="Image" style="max-width: 100%;">
                                    </a>
                                </div>
                                
                            </div>
                        </div>
                        
                    </div>
                    <!--.carousel-inner-->
                    <div class="carousel-controls">
                        <a data-slide="prev" href="#partnersCarousel" class="left carousel-control"><i class="fa fa-angle-left"></i></a>
                        <a data-slide="next" href="#partnersCarousel" class="right carousel-control"><i class="fa fa-angle-right"></i></a>
                    </div>
                </div>
                <!--.Carousel-->
            </div>--%>
        <%--<div class="projects">
                <div class="row">
                    <div class="col-sm-6 col-xs-12">
                        <section>
                            <div class="col-xs-6">
                                <img src="../Themes/Images/egmalydaam.png" />
                                <h1 id="MainContent_H1">93,600,646 ريال</h1>
                                <p>إجمالي المساعدات</p>
                                <h2>29,687</h2>
                                <p>أسرة</p>
                            </div>
                            <div class="col-xs-6">
                                <img src="../Themes/Images/daammobasher.png" />
                                <h1>78,040,105  ريال</h1>
                                <p>دعم مباشر</p>
                                <h2>24,715</h2>
                                <p>أسرة</p>
                            </div>

                        </section>
                        <section>
                            <div class="col-xs-6">
                                <img src="../Themes/Images/daamghermobasher.png" />
                                <h1>13,449,480  ريال</h1>
                                <p>دعم تشجيعي </p>
                                <h2>4,862</h2>
                                <p>أسرة</p>
                            </div>
                            <div class="col-xs-6">
                                <img src="../Themes/Images/zakah.png" />
                                <h1>2,111,061  ريال</h1>
                                <p>دعم نوعي</p>
                                <h2>110</h2>
                                <p>أسرة</p>
                            </div>
                        </section>
                    </div>
                    <div class="col-sm-6 col-xs-12">
                        <img src="../Themes/Images/Donate2017.png" class="donate-img" />
                    </div>
                </div>
            </div>
            <div class="contactus">
                <h3 class="section_title"><a href="#">تواصل معنا</a></h3>
                <div id="MainContent_ctl01">
                        <div class="form-horizontal">
                            <div class="col-md-5 col-sm-5 col-xs-12">
                                <div class="">
                                    <input name="ctl00$MainContent$txtName" type="text" id="MainContent_txtName" class="form-control" placeholder="الاسم" />
                                    <span id="MainContent_ctl03" style="color:Red;visibility:hidden;">*</span>
                                </div>
                                <div class="">
                                    <input name="ctl00$MainContent$txtEmail" type="text" id="MainContent_txtEmail" class="form-control" placeholder="البريد الالكتروني" />
                                    <span id="MainContent_ctl04" style="color:Red;visibility:hidden;">*</span>
                                    <span id="MainContent_ctl05" class="validation_class" style="visibility:hidden;">البريد الاليكتروني غير صحيح</span>
                                </div>
                            </div>
                            <div class="col-md-5 col-sm-5 col-xs-12">
                                
                                <textarea name="ctl00$MainContent$txtMsg" rows="4" cols="20" id="MainContent_txtMsg" class="form-control" placeholder="الرسالة">
                        </textarea>
                                <span id="MainContent_ctl06" style="color:Red;visibility:hidden;">*</span>
                            </div>
                            <div class="form-group col-md-2 text-center">
                                <input type="submit" name="ctl00$MainContent$btnSend" value="ارسال" 
                                    onclick="" id="MainContent_btnSend" class="btn contactBtn" />
                            </div>
                        </div>
            </div>
            </div>
        </div>
    </section>--%>
    </div>
    <script type="text/javascript">
        //<![CDATA[
        var Page_Validators = new Array(document.getElementById("MainContent_ctl03"), document.getElementById("MainContent_ctl04"), document.getElementById("MainContent_ctl05"), document.getElementById("MainContent_ctl06"), document.getElementById("ctl26"), document.getElementById("ctl27"));
        //]]>
    </script>

    <script type="text/javascript">
        //<![CDATA[
        var MainContent_ctl03 = document.all ? document.all["MainContent_ctl03"] : document.getElementById("MainContent_ctl03");
        MainContent_ctl03.controltovalidate = "MainContent_txtName";
        MainContent_ctl03.errormessage = "*";
        MainContent_ctl03.validationGroup = "contactUs";
        MainContent_ctl03.evaluationfunction = "RequiredFieldValidatorEvaluateIsValid";
        MainContent_ctl03.initialvalue = "";
        var MainContent_ctl04 = document.all ? document.all["MainContent_ctl04"] : document.getElementById("MainContent_ctl04");
        MainContent_ctl04.controltovalidate = "MainContent_txtEmail";
        MainContent_ctl04.errormessage = "*";
        MainContent_ctl04.validationGroup = "contactUs";
        MainContent_ctl04.evaluationfunction = "RequiredFieldValidatorEvaluateIsValid";
        MainContent_ctl04.initialvalue = "";
        var MainContent_ctl05 = document.all ? document.all["MainContent_ctl05"] : document.getElementById("MainContent_ctl05");
        MainContent_ctl05.controltovalidate = "MainContent_txtEmail";
        MainContent_ctl05.errormessage = "البريد الاليكتروني غير صحيح";
        MainContent_ctl05.validationGroup = "contactUs";
        MainContent_ctl05.evaluationfunction = "RegularExpressionValidatorEvaluateIsValid";
        MainContent_ctl05.validationexpression = "\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        var MainContent_ctl06 = document.all ? document.all["MainContent_ctl06"] : document.getElementById("MainContent_ctl06");
        MainContent_ctl06.controltovalidate = "MainContent_txtMsg";
        MainContent_ctl06.errormessage = "*";
        MainContent_ctl06.validationGroup = "contactUs";
        MainContent_ctl06.evaluationfunction = "RequiredFieldValidatorEvaluateIsValid";
        MainContent_ctl06.initialvalue = "";
        var ctl26 = document.all ? document.all["ctl26"] : document.getElementById("ctl26");
        ctl26.controltovalidate = "txtEmail";
        ctl26.errormessage = "*";
        ctl26.validationGroup = "newsletter";
        ctl26.evaluationfunction = "RequiredFieldValidatorEvaluateIsValid";
        ctl26.initialvalue = "";
        var ctl27 = document.all ? document.all["ctl27"] : document.getElementById("ctl27");
        ctl27.controltovalidate = "txtEmail";
        ctl27.errormessage = "البريد الاليكتروني غير صحيح";
        ctl27.validationGroup = "newsletter";
        ctl27.evaluationfunction = "RegularExpressionValidatorEvaluateIsValid";
        ctl27.validationexpression = "\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        //]]>
    </script>

    <script type="text/javascript">
        //<![CDATA[

        var Page_ValidationActive = false;
        if (typeof (ValidatorOnLoad) == "function") {
            ValidatorOnLoad();
        }

        function ValidatorOnSubmit() {
            if (Page_ValidationActive) {
                return ValidatorCommonOnSubmit();
            }
            else {
                return true;
            }
        }

        document.getElementById('MainContent_ctl03').dispose = function () {
            Array.remove(Page_Validators, document.getElementById('MainContent_ctl03'));
        }

        document.getElementById('MainContent_ctl04').dispose = function () {
            Array.remove(Page_Validators, document.getElementById('MainContent_ctl04'));
        }

        document.getElementById('MainContent_ctl05').dispose = function () {
            Array.remove(Page_Validators, document.getElementById('MainContent_ctl05'));
        }

        document.getElementById('MainContent_ctl06').dispose = function () {
            Array.remove(Page_Validators, document.getElementById('MainContent_ctl06'));
        }

        document.getElementById('ctl26').dispose = function () {
            Array.remove(Page_Validators, document.getElementById('ctl26'));
        }

        document.getElementById('ctl27').dispose = function () {
            Array.remove(Page_Validators, document.getElementById('ctl27'));
        }
        //]]>
    </script>
</asp:Content>

