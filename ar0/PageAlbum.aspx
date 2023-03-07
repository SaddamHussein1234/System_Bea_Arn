<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageAlbum.aspx.cs" Inherits="ar_PageAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .CropLongTexts3 {
            overflow: hidden;
            text-overflow: ellipsis;
            height: 80px;
        }
    </style>
    <style>
        .clearfix {
    *zoom: 1;
}

    .clearfix:before,
    .clearfix:after {
        display: table;
        content: "";
        line-height: 0;
    }

    .clearfix:after {
        clear: both;
    }

    .visible-sm-block {
        display: block !important;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main_body">
        <!-- Begin page content -->
        <div class="page-header">
            <div class="container">
                <span>البوم الصور
                </span>
            </div>
        </div>
        <div class="page_body">
            <div class="page_content container">
                <asp:Panel ID="pnlData" runat="server">
                    <asp:Repeater ID="RPTAlbumArabic" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6 col-lg-4 col-xl-4 d-flex ">
                                <article class="thumbnail eventItem ColorAlbum btnClick" onclick="location.href='PageAlbumGallery.aspx?ID=<%# Eval("IDItem") %>&IDX=<%# Eval("RandomUniq") %>';">
                                    <%--<a href="PageAlbumGallery.aspx?ID=<%# Eval("IDItem") %>&IDX=<%# Eval("RandomUniq") %>" class="media-image">--%>
                                    <img src="<%# "../" + Eval("imgAlbum") %>" class="img-fluid" alt="Loading ... ">
                                    <%--</a>--%>
                                    <h5 class="media-content">
                                        <span class="fa fa-star"></span> <%# Eval("TitleAlbumAr") %>
                                    </h5>
                                    <h6 class="heading-with-icon"><span class="fa fa-calendar"></span> <%# Library_CLS_Arn.ERP.DataAccess.ClassDataAccess.FChangeF((DateTime) Eval("DateAddAlbum")) %> <span class="fa fa-minus"></span>
                                        <span class="fa fa-picture-o"></span> عدد صور الالبوم :  <%# FCountImg((Int32) Eval("IDItem")) %></h6>
                                    <p class="CropLongTexts3">
                                        <%# Eval("DetailsAR") %>
                                    </p>
                                </article>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="pager_cont">
                        <div class="clearfix visible-sm-block"></div>
                        <span id="MainContent_PageContent_lstPager">
                            <asp:Button ID="btnfirst" runat="server" Text="<< البداية" CssClass="btn btn_green numericLink" Font-Size="12px" OnClick="btnfirst_Click" />
                            <asp:Button ID="btnprevious" runat="server" Text="< السابق" CssClass="btn btn_green numericLink" Font-Size="12px" OnClick="btnprevious_Click" />
                            <asp:Button ID="btnnext" runat="server" Text="التالي >" CssClass="btn btn_green numericLink" Font-Size="12px" OnClick="btnnext_Click" />
                            <asp:Button ID="btnlast" runat="server" Text="النهاية >>" CssClass="btn btn_green numericLink" Font-Size="12px" OnClick="btnlast_Click" />
                        </span>
                    </div>
                </asp:Panel>
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
</asp:Content>

