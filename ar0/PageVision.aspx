<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageVision.aspx.cs" Inherits="ar_PageVision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main_body">
    <div class="page-header">
        <div class="container">
            <span>
                عن الجمعية
            </span>
        </div>
    </div>
    <div class="page_body">
        <div class="page_content container">
            <div class="block">
                <div class="block_body  head_text">
                    <p>
                        <asp:Label ID="lblAbout" runat="server"></asp:Label>
                    </p>
                </div>
            </div>
            <div class="col-md-6">
                <div class="block">
                    <div class="block_title">
                        <i class="fa fa-eye"></i>
                        <h4>رؤيتنا</h4>
                    </div>
                    <div class="block_body">
                        <p>
                            <asp:Label ID="lblVision" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
                <div class="block">
                    <div class="block_title">
                        <i class="fa fa-bullseye"></i>
                        <h4>رسالتنا</h4>
                    </div>
                    <div class="block_body">
                        <p>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
                <div class="block">
                    <div class="block_title">
                        <i class="fa fa-align-center"></i>
                        <h4>قيمنا </h4>
                    </div>
                    <div class="block_body">
                        <p>
                            <asp:Label ID="lblValus" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="block">
                    <div class="block_title">
                        <i class="fa fa-globe"></i>
                        <h4>أهدافنا</h4>
                    </div>
                    <div class="block_body">
                        <p>
                            <asp:Label ID="lblGoals" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>

