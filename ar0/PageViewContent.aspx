<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageViewContent.aspx.cs" Inherits="ar_PageViewContent" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    img {
        border-radius:6px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main_body">
        <!-- Begin page content -->
        <div class="page-header">
            <div class="container">
                <span>
                    <asp:Label ID="lblMenu" runat="server"></asp:Label>
                </span>
            </div>
        </div>
        <asp:Panel ID="pnlOneArticle" runat="server" Visible="false">
            <div class="page_body">
                <div class="page_content container">
                    <div class="block newsDetails">
                        <div class="img_container col-md-4 col-sm-4 col-xs-12">
                            <img runat="server" id="MainContent_PageContent_img_NewsImage" class="img img-responsive" />
                            <h6 class="heading-with-icon"><span class="fa fa-calendar"></span> <asp:Label ID="lblDateAdd" runat="server"></asp:Label> <span class="fa fa-minus"></span>
                                <span class="fa fa-eye"></span> <asp:Label ID="lblCountViews" runat="server"></asp:Label>
                            </h6>
                        </div>
                        <div class="block_title">
                            <h4>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="block_data">
                            <p class="text-justify">
                                <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                <span runat="server" id="IDViewPDF"></span>
                                <hr />
                                <asp:Label ID="lblAttach" runat="server" Text="المرفقات"></asp:Label>
                                <asp:Repeater ID="RPTPath" runat="server" Visible="false">
                                    <ItemTemplate>
                                        <%# Library_CLS_Arn.Saddam.ClassSaddam.FGetPath((string) Eval("AttachFile")) %>
                                        <br />
                                        <asp:Button ID="blnAttach" runat="server" Text="تحميل الملف" CssClass="btn btn-success"
                                            OnClick="blnAttach_Click"  CommandArgument='<%# Eval("AttachFile") %>' />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlOtherArticle" runat="server" Visible="false">
            <div class="page_body">
                <div class="page_content container">
                    <asp:Panel ID="PNLData" runat="server" Visible="false">
                        <asp:Repeater ID="RPTViewContent" runat="server">
                            <ItemTemplate>
                                <div class="block">
                                    <div class="img_container col-md-2 col-xs-3 col-sm-2">
                                        <img src="<%# Library_CLS_Arn.Saddam.ClassSaddam.CheckImg((String) (Eval("ImgArt"))) %>" height="100%" />
                                    </div>
                                    <div class="block_data col-md-10 col-xs-9 col-sm-10">
                                        <h4 class="ways">
                                            <a href="PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>">
                                                <%# Eval("TitleArticle") %> 
                                            </a>
                                        </h4>
                                        <h6 class="heading-with-icon"><span class="fa fa-calendar"></span> 
                                            <%# Library_CLS_Arn.ERP.DataAccess.ClassDataAccess.FChangeF((DateTime) Eval("DateAddArticle")) %> 
                                            <span class="fa fa-minus"></span>
                                            <span class="fa fa-eye"></span> <%# Eval("CountViews") %> 
                                        </h6>
                                        <h6 class="heading-with-icon"><span class="fa fa-list"></span> <%# Eval("TitleManageAr") %> 
                                        </h6>
                                        <h6 class="CropLongTexts3">
                                            <%# Eval("DetailsArticle") %> 
                                        </h6>
                                        <a class="btn btn_green" href="PageViewDetails.aspx?ID=<%# Eval("IDUniqArticle") %>">... المزيد</a>
                                    </div>
                                </div>
                                <hr />
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
        </asp:Panel>
    </div>
</asp:Content>

