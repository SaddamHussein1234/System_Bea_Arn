<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageAlbumGallery.aspx.cs" Inherits="ar_PageAlbumGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main_body">
        <!-- Begin page content -->
        <div class="page-header">
            <div class="container">
                <span>
                    البوم الصور
                </span>
            </div>
        </div>
        <div class="page_body">
            <div class="page_content container">
                <link href="../Themes/Content/media_gallery.css" rel="stylesheet" />
                <div id="home_tabs" class="mediaTabs">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#tab1" data-toggle="tab">
                            <asp:Label ID="lblNameAlbum" runat="server" Font-Size="14px"></asp:Label>
                                           </a></li>
                        <li runat="server" visible="false"><a href="#tab2" data-toggle="tab">الفيديوهات</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab1" runat="server">
                            <div id="slider1_container" style="background-color:#dcdada">
                                <!-- Loading Screen -->
                                <div u="loading" class="loader">
                                    <div class="loader_overlay">
                                    </div>
                                    <div class="loader_img">
                                    </div>
                                </div>
                                <!-- Slides Container -->
                                <div u="slides" class="slides_container">
                                    <asp:Repeater ID="RPTAlbumImg" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <img u="image" src="<%# "../" + Eval("PathImg") %>" />
                                                <img u="thumb" src="<%# "../" + Eval("PathImg") %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <!-- Thumbnail Navigator Skin Begin -->
                                <div u="thumbnavigator" class="jssort07">
                                    <div class="thumbnail_cont">
                                    </div>
                                    <!-- Thumbnail Item Skin Begin -->
                                    <div u="slides" style="cursor: move;">
                                        <div u="prototype" class="p">
                                            <div u="thumbnailtemplate" class="i">
                                            </div>
                                            <div class="o">
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Arrow Left -->
                                    <span u="arrowleft" class="jssora11l"></span>
                                    <!-- Arrow Right -->
                                    <span u="arrowright" class="jssora11r"></span>
                                    <!-- Arrow Navigator Skin End -->
                                </div>
                            </div>
                            <i class="fa fa-photo"></i> عدد صور الالبوم <asp:Label ID="lblCount" runat="server"></asp:Label>
                        </div>
                        <div class="tab-pane" id="tab2">

                        </div>
                        <asp:Panel ID="PNLNull" runat="server" Visible="false">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <h2 align="center" style="color: #F0F0F0; font-size: 30px">لا توجد بيانات
                            </h2>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
                <script src="../Themes/Scripts/jssor.slider.mini.js"></script>
                <script>
                    jQuery(document).ready(function ($) {
                        var options = {
                            $AutoPlay: false,                                    //[Optional] Whether to auto play, to enable slideshow, this option must be set to true, default value is false
                            $AutoPlayInterval: 4000,                            //[Optional] Interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
                            $SlideDuration: 500,                                //[Optional] Specifies default duration (swipe) for slide in milliseconds, default value is 500
                            $DragOrientation: 3,                                //[Optional] Orientation to drag slide, 0 no drag, 1 horizental, 2 vertical, 3 either, default value is 1 (Note that the $DragOrientation should be the same as $PlayOrientation when $DisplayPieces is greater than 1, or parking position is not 0)
                            $UISearchMode: 0,                                   //[Optional] The way (0 parellel, 1 recursive, default value is 1) to search UI components (slides container, loading screen, navigator container, arrow navigator container, thumbnail navigator container etc).

                            $ThumbnailNavigatorOptions: {
                                $Class: $JssorThumbnailNavigator$,              //[Required] Class to create thumbnail navigator instance
                                $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always

                                $Loop: 2,                                       //[Optional] Enable loop(circular) of carousel or not, 0: stop, 1: loop, 2 rewind, default value is 1
                                $SpacingX: 3,                                   //[Optional] Horizontal space between each thumbnail in pixel, default value is 0
                                $SpacingY: 3,                                   //[Optional] Vertical space between each thumbnail in pixel, default value is 0
                                $DisplayPieces: 6,                              //[Optional] Number of pieces to display, default value is 1
                                $ParkingPosition: 204,                          //[Optional] The offset position to park thumbnail,

                                $ArrowNavigatorOptions: {
                                    $Class: $JssorArrowNavigator$,              //[Requried] Class to create arrow navigator instance
                                    $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always
                                    $AutoCenter: 2,                                 //[Optional] Auto center arrows in parent container, 0 No, 1 Horizontal, 2 Vertical, 3 Both, default value is 0
                                    $Steps: 6                                       //[Optional] Steps to go for each navigation request, default value is 1
                                }
                            }
                        };

                        var jssor_slider1 = new $JssorSlider$("slider1_container", options);

                        //responsive code begin
                        //you can remove responsive code if you don't want the slider scales while window resizes
                        function ScaleSlider() {
                            var parentWidth = jssor_slider1.$Elmt.parentNode.clientWidth;
                            if (parentWidth)
                                jssor_slider1.$ScaleWidth(parentWidth);
                            else
                                window.setTimeout(ScaleSlider, 30);
                        }
                        ScaleSlider();

                        $(window).bind("load", ScaleSlider);
                        $(window).bind("resize", ScaleSlider);
                        $(window).bind("orientationchange", ScaleSlider);
                        //responsive code end
                    });
                </script>
            </div>
        </div>
    </div>
</asp:Content>

