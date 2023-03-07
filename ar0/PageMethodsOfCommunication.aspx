<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageMethodsOfCommunication.aspx.cs" Inherits="ar_PageMethodsOfCommunication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <body onload="window.scroll(0, 340 )">
    <div class="main_body">
        <!-- Begin page content -->
        <div class="page-header">
            <div class="container">
                <span>الموقع الجغرافي
                </span>
            </div>
        </div>
        <div class="page_body">
            <div class="page_content container">
                <div id="map" class="block">
                    <%--<div style="width: 100%"><iframe width="100%" height="300" src="https://maps.google.com/maps?width=100%&amp;height=300&amp;hl=en&amp;coord=22.872833, 40.492484&amp;q=%D9%85%D8%B9%D9%87%D8%AF%20%D8%A3%D8%B1%D9%86%20%D9%84%D9%84%D8%AA%D8%AF%D8%B1%D9%8A%D8%A8%D8%8C%20%D8%A7%D9%84%D8%B5%D9%84%D8%AD%D8%A7%D9%86%D9%8A%D8%A9+(%D8%AC%D9%85%D8%B9%D9%8A%D8%A9%20%D8%A7%D9%84%D8%A8%D8%B1%20%D9%88%D8%A7%D9%84%D8%AE%D8%AF%D8%A7%D9%85%D8%AA%20%D8%A7%D9%84%D8%A5%D8%AC%D8%AA%D9%85%D8%A7%D8%B9%D9%8A%D8%A9%20%D8%A8%D8%A3%D8%B1%D9%86)&amp;ie=UTF8&amp;t=h&amp;z=16&amp;iwloc=B&amp;output=embed" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe></div><br />--%>
                    <div style="width: 100%"><iframe width="100%" height="300" src="https://www.google.com/maps?width=100%25&amp;height=300&amp;hl=en&amp;coord=22.872833,+40.492484&amp;q=%D8%AC%D9%85%D8%B9%D9%8A%D8%A9+%D8%A7%D9%84%D8%A8%D8%B1+%D9%88%D8%A7%D9%84%D8%AE%D8%AF%D8%A7%D9%85%D8%AA+%D8%A7%D9%84%D8%A5%D8%AC%D8%AA%D9%85%D8%A7%D8%B9%D9%8A%D8%A9+%D8%A8%D8%A3%D8%B1%D9%86+(%D8%AC%D9%85%D8%B9%D9%8A%D8%A9+%D8%A7%D9%84%D8%A8%D8%B1+%D9%88%D8%A7%D9%84%D8%AE%D8%AF%D8%A7%D9%85%D8%AA+%D8%A7%D9%84%D8%A5%D8%AC%D8%AA%D9%85%D8%A7%D8%B9%D9%8A%D8%A9+%D8%A8%D8%A3%D8%B1%D9%86)&amp;ie=UTF8&amp;t=h&amp;z=16&amp;iwloc=B&amp;output=embed" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"></iframe></div><br />
                </div>
                <div id="contact_block" class="block">
                    <div class="block_title form-title">
                        <h4>طرق الإتصال بنا</h4>
                    </div>
                    <div class="block_body">
                        <ul class="contacts">
                            <li>
                                <span>
                                    <a runat="server" id="IDLocation" title="الموقع على الخريطه" data-toggle="tooltip" target="_blank" class="fontColor">
                                        <i class="fa fa-home"></i>
                                        <asp:Label ID="lblLocation" runat="server" Text="المدينة المنور , مهد الذهب , الصلحانية"></asp:Label>
                                    </a>
                                </span>
                            </li>
                            <li><span>
                                <a id="IDPhone" runat="server" class="fontColor">
                                    <i class="fa fa-phone-square"></i>
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </a>
                            </span></li>
                            <li><span>
                                <a id="IDEmail" runat="server" class="fontColor">
                                    <i class="fa fa-envelope"></i>
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </a>
                            </span></li>
                        </ul>
                        <div class="social-bar ">
                            <br />
                            <br />
                            <a runat="server" id="IDFacebook" title="فيسبوك" data-toggle="tooltip" target="_blank">
                                <i class="fa fa-facebook fontColor"></i>
                            </a>
                            <a runat="server" id="IDtwitter" title="تويتر" data-toggle="tooltip" target="_blank">
                                <i class="fa fa-twitter fontColor"></i>
                            </a>
                            <a runat="server" id="IDyoutube" title="يوتيوب" data-toggle="tooltip" target="_blank">
                                <i class="fa fa-youtube-square fontColor"></i>
                            </a>
                            <a runat="server" id="IDGoogleplus" title="جوجل بلس" data-toggle="tooltip" target="_blank">
                                <i class="fa fa-google-plus fontColor"></i>
                            </a>
                            <a href="#" target="_blank">
                                <i class="fa fa-rss fontColor"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </body>
</asp:Content>

