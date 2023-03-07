<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageVideo.aspx.cs" Inherits="ar_PageVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                <span>مكتبة الفيديو
                </span>
            </div>
        </div>
        <div class="page_body">
            <div class="page_content container">
                <asp:Panel ID="pnlData" runat="server">
                    <asp:Repeater ID="RPTVideoArabic" runat="server">
                        <ItemTemplate>
                            <div class="col-md-6 col-lg-4 col-xl-4 d-flex ">
                                <article class="thumbnail eventItem ColorAlbum btnClick" onclick="location.href='#voice-cont<%# Eval("IDVideo") %>';" data-toggle="modal" data-target="#voice-cont<%# Eval("IDVideo") %>">
                                    <iframe src="<%# Eval("VideoSrc") %>" width="100%" height="200"
                                                data-toggle="tooltip" title="<%# Eval("VideoNameAr") %>" style="border: none; overflow: hidden; pointer-events: none;"></iframe>
                                    <h5 class="media-content">
                                        <span class="fa fa-star"></span> <%# Eval("VideoNameAr") %>
                                    </h5>
                                    <h6 class="heading-with-icon"><span class="fa fa-calendar"></span>
                                        <%# Library_CLS_Arn.ERP.DataAccess.ClassDataAccess.FChangeF((DateTime) Eval("DateAddVideo")) %> 
                                    </h6>
                                </article>
                            </div>
                            <div class="modal fade voice-cont-modal " id="voice-cont<%# Eval("IDVideo") %>" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="srv-title"><%# Eval("VideoNameAr") %></h4>
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
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="pager_cont">
                        <div class="clearfix visible-sm-block"></div>
                        <span id="MainContent_PageContent_lstPager">
                            <asp:Button ID="btnfirst" runat="server" Text="<< البداية" CssClass="btn btn_green numericLink" Font-Size="12px" Font-Bold="true" OnClick="btnfirst_Click" />
                            <asp:Button ID="btnprevious" runat="server" Text="< السابق" CssClass="btn btn_green numericLink" Font-Size="12px" Font-Bold="true" OnClick="btnprevious_Click" />
                            <asp:Button ID="btnnext" runat="server" Text="التالي >" CssClass="btn btn_green numericLink" Font-Size="12px" Font-Bold="true" OnClick="btnnext_Click" />
                            <asp:Button ID="btnlast" runat="server" Text="النهاية >>" CssClass="btn btn_green numericLink" Font-Size="12px" Font-Bold="true" OnClick="btnlast_Click" />
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

