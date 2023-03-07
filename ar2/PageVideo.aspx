<%@ Page Title="" Language="C#" MasterPageFile="~/ar2/MPAr.master" AutoEventWireup="true" CodeFile="PageVideo.aspx.cs" Inherits="ar_PageVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../view/sites/default/files/css/css_wq7sHoDzaBIE35go1iXZWBnlbGLo7Ej5ANYbHFjeIjc.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-welcome" style="background-image: url(../Img/LoginBackground.jpg)">
        <div class="cover-image">
        </div>
        <div class="container px-lg-0">
            <div class="breadcrumb-cont">
                <ol class="breadcrumb">
                    <div class="breadcrumb-cont">
                        <ol class="breadcrumb">
                            <li><a href="/ar">الرئيسية</a> <i class="fa fa-angle-left"></i> <span class="nolink">المركز الإعلامي</span> <i class="fa fa-angle-left"></i> <a href="PageVideo.aspx" class="active">مكتبة الفيديو</a></li>
                        </ol>
                    </div>
                </ol>
            </div>
            <h1>مكتبة الفيديو</h1>
        </div>
    </div>

    <div class="logged-tabs---nav">
        <div class="container ">
        </div>
    </div>

    <div class="region region-content">
        <section id="block-system-main" class="block block-system clearfix">
            <!-- Main wrapper -->
            <div class="main-content inner">
                <div class="greyBg">
                    <!-- section-most-news-->
                    <!--end section-most-news-->
                    <section class="content-section  content-section-more-news videos  ">
                        <div class="container">
                            <div class="events-filter-cont">
                                <div class="filter-btns">
                                    <a href="PageVideo.aspx" class="btn  active  ">
                                        <span class="fas fa-list-ul"></span>
                                        <span>المكتبة المرئية</span>
                                    </a>
                                    <a href="PageAlbum.aspx" class="btn">
                                        <span class="far fa-calendar"></span>
                                        <span>معرض الصور</span>
                                    </a>
                                </div>
                            </div>
                            <div id="lightgallery-video">
                                <div class="row">
                                    <%--<div class="col-md-6 col-lg-4 col-xl-4 d-flex">
                                        <a href="#voice-cont123" data-toggle="modal" data-target="#voice-cont123">
                                        <article class="media-item">
                                            <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" class="img-fluid" alt="Loading ... ">
                                        </article>
                                        </a>
                                    </div>
                                    <div class="col-md-6 col-lg-4 col-xl-4 d-flex">
                                        <a href="#voice-cont12" data-toggle="modal" data-target="#voice-cont12">
                                        <article class="media-item">
                                            <img src="../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png" class="img-fluid" alt="Loading ... ">
                                        </article>
                                        </a>
                                    </div>--%>
                                    <asp:Repeater ID="RPTVideoArabic" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-6 col-lg-4 col-xl-4 d-flex">
                                                <a href="#voice-cont<%# Eval("IDVideo") %>" data-toggle="modal" data-target="#voice-cont<%# Eval("IDVideo") %>">
                                                    <article class="media-item">
                                                        <article class="media-item">
                                                            <iframe src="<%# Eval("VideoSrc") %>" width="100%" height="200"
                                                                data-toggle="tooltip" title="<%# Eval("VideoNameAr") %>" style="border: none; overflow: hidden; pointer-events: none;"></iframe>
                                                        </article>
                                                        <h3 class="media-content">
                                                            <a><%# Eval("VideoNameAr") %></a>
                                                        </h3>
                                                    </article>
                                                </a>
                                            </div>
                                            <div class="modal fade voice-cont-modal " id="voice-cont<%# Eval("IDVideo") %>" tabindex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog modal-lg" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h3 class="srv-title"><%# Eval("VideoNameAr") %></h3>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="p-4">
                                                                <iframe src="<%# Eval("VideoSrc") %>" width="100%" height="360" style="border: none; overflow: hidden"
                                                                    scrolling="no" frameborder="0" allowtransparency="true" allow="encrypted-media"></iframe>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix visible-sm-block"></div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="d-flex" style="width: 100%" runat="server" visible="false" id="PNLNull">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="d-flex" style="width: 100%">
                                        <article class="media-item">
                                            <h2 align="center" style="color: #393939">لا توجد بيانات
                                            </h2>
                                        </article>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                </div>
                            </div>
                            <div class="text-center">
                                <ul class="pagination">
                                    <li>
                                        <asp:Button ID="btnfirst" runat="server" Text="<< البداية" CssClass="btn btn-primary bcolor" Font-Size="12px" OnClick="btnfirst_Click" />
                                    </li>
                                    <li>
                                        <asp:Button ID="btnprevious" runat="server" Text="< السابق" CssClass="btn btn-primary bcolor" Font-Size="12px" OnClick="btnprevious_Click" />
                                    </li>
                                    <li class="next">
                                        <asp:Button ID="btnnext" runat="server" Text="التالي >" CssClass="btn btn-primary bcolor" Font-Size="12px" OnClick="btnnext_Click" />
                                    </li>
                                    <li class="pager-last">
                                        <asp:Button ID="btnlast" runat="server" Text="النهاية >>" CssClass="btn btn-primary bcolor" Font-Size="12px" OnClick="btnlast_Click" />
                                    </li>
                                </ul>
                                <hr />
                                <iframe src="http://docs.google.com/gview?url=http://alberlive.net/Pages/Docs/Finance Policies.pdf&embedded=true" style="width: 100%; height: 700px;" frameborder="0"></iframe>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
            <!-- End main Wrapper -->
        </section>
    </div>

    <div class="modal fade voice-cont-modal " id="voice-cont123" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="srv-title">قائمة الأوامر الصوتية</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="p-4">
                        <iframe src="https://www.youtube.com/watch?v=LiNDSjVbSok" 
                            width="100%" height="100%" style="border:none;overflow:hidden" scrolling="no" frameborder="0" allowTransparency="true" allow="encrypted-media"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade voice-cont-modal " id="voice-cont12" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="srv-title">قائمة الأوامر الصوتية</h3>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="p-4">
                        <div class="voicebanner-cont">
                            <img src="../Img/voicebanner.png" alt="img" class="img-fluid">
                        </div>
                        <div class="p-content mb-3">
                            مثال نموذج 2 
                        </div>
                        <h3 class="heading right">مثال نموذج 2 :</h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

