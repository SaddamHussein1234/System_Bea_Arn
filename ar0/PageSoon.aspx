<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageSoon.aspx.cs" Inherits="ar_PageSoon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main_body">
        <!-- Begin page content -->
        <div class="page-header">
            <div class="container">
                <span>صفحة قيد التطوير
                </span>
            </div>
        </div>
        <div class="page_body">
            <div class="page_content container">
                <div class="contact-form-cont">
                    <h2 class="text-center">صفحة قيد التطوير</h2>
                    <div id="contact-form" class="contact-form">
                        <div class="user-info-from-cookie contact-form">
                            <div>
                                <br />
                                <br />
                                <br />
                                <h2 class="text-center">هذه الصفحة قيد التطوير سيتم الإنتهاء منها قريباً <i class="fa fa-file"></i>
                                </h2>
                                <br />
                                <br />
                                <br />
                                <asp:Button ID="btnOk" runat="server" Text="حسناً" OnClick="btnOk_Click" Style="border-radius: 8px" class="btn btn_green" />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

