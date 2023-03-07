<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageViewDetails.aspx.cs" Inherits="ar_PageViewDetails" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        img {
            border-radius: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body onload="window.scroll(0, 340 )">
    <div class="main_body">
        <!-- Begin page content -->
        <div class="page-header">
            <div class="container">
                <span>
                    <asp:Label ID="lblMenu" runat="server"></asp:Label>
                </span>
            </div>
        </div>
        <div class="page_body">
            <div class="page_content container">
                <div class="block newsDetails">
                    <div class="img_container col-md-4 col-sm-4 col-xs-12">
                        <img runat="server" id="MainContent_PageContent_img_NewsImage" class="img img-responsive" />
                        <h6 class="heading-with-icon"><span class="fa fa-calendar"></span>
                            <asp:Label ID="lblDateAdd" runat="server"></asp:Label>
                            <span class="fa fa-minus"></span>
                            <span class="fa fa-eye"></span>
                            <asp:Label ID="lblCountViews" runat="server"></asp:Label>
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
                        </p>
                    </div>
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
                </div>
            </div>
        </div>
    </div>
    </body>
</asp:Content>

