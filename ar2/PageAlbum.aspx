<%@ Page Title="" Language="C#" MasterPageFile="~/ar2/MPAr.master" AutoEventWireup="true" CodeFile="PageAlbum.aspx.cs" Inherits="ar_PageAlbum" %>

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
                            <li><a href="/ar">الرئيسية</a> <i class="fa fa-angle-left"></i> <span class="nolink">المركز الإعلامي</span> <i class="fa fa-angle-left"></i> <a href="PageAlbum.aspx">معرض الصور</a></li>
                        </ol>
                    </div>
                </ol>
            </div>
            <h1>معرض الصور</h1>
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
                    <section class="content-section  content-section-more-news  photos ">
                        <div class="container">
                            <div class="events-filter-cont">
                                <div class="filter-btns">
                                    <a href="PageVideo.aspx" class="btn    ">
                                        <span class="fas fa-list-ul"></span>
                                        <span>مكتبة الفيديو</span>
                                    </a>
                                    <a href="PageAlbum.aspx" class="btn active">
                                        <span class="far fa-calendar"></span>
                                        <span>معرض الصور</span>
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Repeater ID="RPTAlbumArabic" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-6 col-lg-4 col-xl-4 d-flex">
                                            <article class="media-item">
                                                <a href="PageAlbumGallery.aspx?ID=<%# Eval("IDItem") %>&IDX=<%# Eval("RandomUniq") %>" class="media-image">
                                                    <img src="<%# "../" + Eval("imgAlbum") %>" class="img-fluid" alt="Loading ... ">
                                                </a>
                                                <h3 class="media-content">
                                                    <a href="PageAlbumGallery.aspx?ID=<%# Eval("IDItem") %>&IDX=<%# Eval("RandomUniq") %>"><%# Eval("TitleAlbumAr") %></a>
                                                </h3>
                                                <h6 class="heading-with-icon"><span class="fa fa-calendar"></span> <%# Library_CLS_Arn.ERP.DataAccess.ClassDataAccess.FChangeF((DateTime) Eval("DateAddAlbum")) %> <span class="fa fa-minus"></span>
                                                    <span class="fa fa-picture-o"></span> عدد صور الالبوم :  <%# FCountImg((Int32) Eval("IDItem")) %></h6>
                                                <p class="CropLongTexts3">
                                                    <%# Eval("DetailsAR") %>
                                                </p>
                                            </article>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="d-flex" style="width: 100%" runat="server" visible="false" id="IDNullData">
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
                            <div class="text-center">
                                <ul class="pagination">
                                    <li class="active"><span>1</span></li>
                                    <li><a title="الذهاب إلى الصفحة 2" href="/ar/photo-gallery?page=1">2</a></li>
                                    <li><a title="الذهاب إلى الصفحة 3" href="/ar/photo-gallery?page=2">3</a></li>
                                    <li><a title="الذهاب إلى الصفحة 4" href="/ar/photo-gallery?page=3">4</a></li>
                                    <li><a title="الذهاب إلى الصفحة 5" href="/ar/photo-gallery?page=4">5</a></li>
                                    <li><a title="الذهاب إلى الصفحة 6" href="/ar/photo-gallery?page=5">6</a></li>
                                    <li><a title="الذهاب إلى الصفحة 7" href="/ar/photo-gallery?page=6">7</a></li>
                                    <li><a title="الذهاب إلى الصفحة 8" href="/ar/photo-gallery?page=7">8</a></li>
                                    <li><a title="الذهاب إلى الصفحة 9" href="/ar/photo-gallery?page=8">9</a></li>
                                    <li class="pager-ellipsis disabled"><span>…</span></li>
                                    <li class="next"><a title="الذهاب إلى الصفحة التالية" href="/ar/photo-gallery?page=1">التالية ›</a></li>
                                    <li class="pager-last"><a title="الذهاب إلى الصفحة الأخيرة" href="/ar/photo-gallery?page=56">الأخيرة »</a></li>
                                </ul>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
            <!-- End main Wrapper -->
        </section>
    </div>


</asp:Content>

