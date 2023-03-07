<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="PageContact-Us.aspx.cs" Inherits="ar_PageContact_Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="theme-color" content="#373533" />
    <meta name="msapplication-navbutton-color" content="#373533" />
    <meta name="apple-mobile-web-app-status-bar-style" content="#373533" />

    <link rel="icon" type="image/png" href="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <link rel="apple-touch-icon-precomposed" href="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />

    <meta name="title" content="<%= HFTitle.Value %>" />
    <meta name="description" content="<%= HFDescrption.Value %>" />
    <meta name="keywords" content="<%= HFKeyWord.Value %>" />
    <meta name="url" content="<%= "https://" + HFLink.Value + HFQuery.Value %>" />

    <meta property="og:description" content="<%= HFDescrption.Value %>" />
    <meta property="og:title" content="<%= HFTitle.Value %>" />
    <meta property="og:type" content="site" />
    <meta property="og:url" content="<%= "https://" + HFLink.Value + HFQuery.Value %>" />
    <meta property="og:locale" content="ar_AR" />
    <meta property="og:locale:alternate" content="ar_AR" />
    <meta property="og:locale:alternate" content="en_US" />
    <meta property="og:image" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta property="og:image:width" content="600" />
    <meta property="og:image:height" content="300" />

    <meta name="twitter:description" content="<%= HFDescrption.Value %>" />
    <meta name="twitter:image" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="<%= HFTitle.Value %>" />
    <meta name="twitter:url" content="<%= "https://" + HFLink.Value + HFQuery.Value %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body  onload="window.scroll(0, 270)">
        <div class="section_header original" style="background: url('/Themes/Ar_Qader/images/section_header_background.jpg')">
            <div class="container">
                <!-- Description -->
                <div class="section_description">
                    <h1>إترك لنا رسالة</h1>
                </div>
                <!-- Breadcrumbs -->
                <div class="breadcrumb_container">
                    <ul class="breadcrumb">
                        <i class="fas fa-bookmark"></i>
                        <li><a href='/ar/'>الرئيسية</a></li>
                        <li>اتصل بنا</li>
                    </ul>
                </div>
            </div>
        </div>

        <!-- Start Container -->
        <div class="body_container">
            <div class="container inner">

                <style type="text/css">
                    .inline_form > div {
                        flex-basis: calc(50% - 10px);
                    }

                        .inline_form > div span {
                            min-width: 90px;
                        }
                </style>
                <div class="col-md-12">
                    <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                        <strong>تحذير ! </strong>
                        <asp:Label ID="lblWarning" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <div class="col-md-12">
                    <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                        <strong>عملية ناجحة </strong>
                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <div class="clearfix"></div>

                <!-- Start Tags -->
                <div class="row">
                    <div class="col-md-8">
                        <asp:Panel ID="pnlOK" runat="server" Visible="false">
                            <div class="page_container">
                                <div class="page_subtitle">تأكيد الإرسال</div>
                                <br />
                                <br />
                                <br />
                                <h4 align="center">تم إرسال رسالتك بنجاح <i class="fas fa-check-circle"></i>
                                </h4>
                                <br />
                                <br />
                                <br />
                                <div align="left">
                                    <asp:Button ID="btnOk" runat="server" Text="حسناً" OnClick="btnOk_Click"
                                        Style="border-radius: 8px" class="submit" />
                                </div>
                                <br />
                                <br />
                                <br />
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="pnlMessage" runat="server" class="col-md-8">
                        <div class="page_container">
                            <div class="page_subtitle">راسلنا</div>
                            <div class="inline_form fancy_inputs">
                                <div>
                                    <span>غرض المراسلة</span>
                                    <div class="input">
                                        <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2">
                                            <asp:ListItem Value='general'>عام</asp:ListItem>
                                            <asp:ListItem Value='suggestion'>تقديم إقتراح</asp:ListItem>
                                            <asp:ListItem Value='complaint'>تقديم شكوي</asp:ListItem>
                                            <asp:ListItem Value='violation'>إبلاغ عن مخالفة</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div>
                                    <span>إسمك الكريم</span>
                                    <div class="input">
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="50" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" Display="Dynamic" Font-Size="11px"
                                            runat="server" ControlToValidate="txtName" ErrorMessage="* إدخل الاسم"
                                            ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div>
                                    <span>رقم الجوال</span>
                                    <div class="input">
                                        <asp:TextBox ID="txtPhone" runat="server" TextMode="Number" MaxLength="20" ValidationGroup="g2"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" Display="Dynamic" Font-Size="11px" 
                                            runat="server" ControlToValidate="txtPhone" ErrorMessage="* رقم الهاتف" ForeColor="#FF0066" 
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPhone"
                                            ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                            Display="Dynamic" Font-Size="11px" SetFocusOnError="true">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div>
                                    <span>عنوان الرسالة</span>
                                    <div class="input">
                                        <asp:TextBox ID="txtTitle" runat="server" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" Display="Dynamic" Font-Size="11px"
                                            runat="server" ControlToValidate="txtTitle" ErrorMessage="* إدخل عناون الرسالة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div>
                                    <span>حدد البلد</span>
                                    <div class="input">
                                        <asp:DropDownList ID="DLCountry" runat="server" ValidationGroup="g2" Style="text-align: left; direction: ltr;">
                                            <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" Display="Dynamic"
                                            Font-Size="11px" runat="server" ControlToValidate="DLCountry" ErrorMessage="* حدد البلد"
                                            ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div>
                                    <span>البريد الإلكتروني</span>
                                    <div class="input">
                                        <asp:TextBox ID="txtEmail" runat="server" Style="direction: ltr;" MaxLength="50" ValidationGroup="g2"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Font-Size="11px" Display="Dynamic" 
                                            ErrorMessage="* إدخل البريد " ValidationGroup="g2" ControlToValidate="txtEmail" 
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                            ControlToValidate="txtEmail"
                                            ErrorMessage="بريد خاطئ" Font-Size="11px" Display="Dynamic"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ForeColor="Red" ValidationGroup="g2"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div style="flex-basis: 100%">
                                    <span>تفاصيل الرسالة</span>
                                    <div class="input">
                                        <asp:TextBox TextMode="MultiLine" ID="txtMessage" runat="server" Rows="5" ValidationGroup="g2" MaxLength="1000"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Font-Size="11px" Display="Dynamic" ErrorMessage="* نص الرسالة ..."
                                            ValidationGroup="g2" ControlToValidate="txtMessage" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div>
                                    <span>رمز التحقق</span><img src="../captcha.aspx" height="35" width="100" />
                                    <div class="input">
                                        <asp:TextBox ID="txtCapatsha" runat="server" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" Display="Dynamic" Font-Size="11px"
                                            runat="server" ControlToValidate="txtCapatsha" ErrorMessage="* رمز التحقق" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div align="center">
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" Text="إرسال الرسالة"
                                        CssClass="submit" ValidationGroup="g2" OnClick="btnSend_Click" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 side-column">
                        <!-- Contact -->
                        <div class="page_container">
                            <div class="page_subtitle">تواصل معنا</div>
                            <table class="contact_block1">
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-phone'></span></td>
                                    <td>
                                        <div class="number">
                                            <a runat="server" id="IDPhone">
                                                <asp:Label ID="lblPhone" runat="server" Style="background: none; color: #393939;" Font-Size="13px"></asp:Label>
                                            </a>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-envelope'></span></td>
                                    <td>
                                        <a runat="server" id="IDEmail">
                                            <asp:Label ID="lblEmail" runat="server" Style="background: none; color: #393939;" Font-Size="13px"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-map-marker'></span></td>
                                    <td>
                                        <a runat="server" id="IDLocation" target="_blank">
                                            <asp:Label ID="lblLocation" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="الموقع على الخريطة"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-minus'></span></td>
                                    <td>
                                        <a runat="server" id="IDFacebook" target="_blank">
                                            <asp:Label ID="lblFacebook" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="سناب شات"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-minus'></span></td>
                                    <td>
                                        <a runat="server" id="IDtwitter" target="_blank">
                                            <asp:Label ID="lbltwitter" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="صفحة التويتر"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40"><span class='glyphicon glyphicon-minus'></span></td>
                                    <td>
                                        <a runat="server" id="IDyoutube" target="_blank">
                                            <asp:Label ID="lblyoutube" runat="server" Style="background: none; color: #393939;" Font-Size="13px" Text="قناة اليوتيوب"></asp:Label>
                                        </a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <!-- Map -->
                    </div>
                    <!-- End Tags -->
                </div>
            </div>
        </div>
    </body>

    <asp:HiddenField ID="HFNameSite" runat="server" />
    <asp:HiddenField ID="HFTitle" runat="server" />
    <asp:HiddenField ID="HFDescrption" runat="server" />
    <asp:HiddenField ID="HFKeyWord" runat="server" />
    <asp:HiddenField ID="HFImage" runat="server" />
    <asp:HiddenField ID="HFLink" runat="server" />
    <asp:HiddenField ID="HFQuery" runat="server" />
</asp:Content>

