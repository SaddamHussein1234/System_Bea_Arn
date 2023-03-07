<%@ Page Title="" Language="C#" MasterPageFile="~/ar2/MPAr.master" AutoEventWireup="true" CodeFile="PageContact-Us.aspx.cs" Inherits="ar_PageContact_Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSend.ClientID %>").disabled = true;
            document.getElementById("<%=btnOk.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <link href="../view/sites/default/files/css/css_wq7sHoDzaBIE35go1iXZWBnlbGLo7Ej5ANYbHFjeIjc.css" rel="stylesheet" />
    <style>
        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 48%;
                margin: 1px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Modal ends here -->
    <!-- This script let the modal only show when clicked for recording-->
    <script>
        $("#speak-btn").click(
          function (event) {
              var value = $('#speak-btn').attr('class');
              if (value === 'voice-btn:hover') {
                  $("#speak-btn").removeAttr('data-toggle');
              } else if (value === 'voice-btn') {
                  $("#speak-btn").attr('data-toggle', 'modal');
              }

          }
        );
    </script>

    <!-- Main wrapper   -->
    <div class="main-content inner contact-inner">
        <section class="content-section  content-section-contact-form ">
            <div class="container  px-0">
                <div class="row">
                    <div class="col-12">
                        <asp:Panel ID="pnlMessage" runat="server">
                            <div class="contact-form-cont">
                                <h2 class="text-center">هل لديك استفسار؟</h2>
                                <div id="contact-form" class="contact-form">
                                    <div class="user-info-from-cookie contact-form">
                                        <div>
                                            <div class="form-item form-item-name form-group WidthTex">
                                                <label class="control-label" for="edit-name">اسمك <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip">*</span></label>
                                                <asp:TextBox ID="txtName" runat="server" MaxLength="255" CssClass="form-control form-text required" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" Display="Dynamic" Font-Size="11px" runat="server" ControlToValidate="txtName" ErrorMessage="* إدخل الاسم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-item form-item-name form-group WidthTex">
                                                <label class="control-label" for="edit-mail">بريدك الإلكتروني <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip">*</span></label>
                                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="255" CssClass="form-control form-text required" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Font-Size="11px" Display="Dynamic" ErrorMessage="* إدخل البريد " ValidationGroup="g2" ControlToValidate="txtEmail" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                    ControlToValidate="txtEmail"
                                                    ErrorMessage="بريد خاطئ"
                                                    Font-Size="11px"
                                                    Display="Dynamic"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ForeColor="Red" ValidationGroup="g2"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-item form-item-name form-group WidthTex">
                                                <label class="control-label" for="edit-subject">الموضوع <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip">*</span></label>
                                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="255" CssClass="form-control form-text required" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" Display="Dynamic" Font-Size="11px" runat="server" ControlToValidate="txtTitle" ErrorMessage="* إدخل موضوع الرسالة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-item form-item-name form-group WidthTex">
                                                <label class="control-label" for="edit-subject">رقم الهاتف <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip">*</span></label>
                                                <asp:TextBox ID="txtPhone" runat="server" MaxLength="255" CssClass="form-control form-text required" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" Display="Dynamic" Font-Size="11px" runat="server" ControlToValidate="txtPhone" ErrorMessage="* رقم الهاتف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-item form-item-name form-group">
                                                <label class="control-label" for="edit-message">الرسالة <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip">*</span></label>
                                                <div class="form-textarea-wrapper resizable">
                                                    <asp:TextBox TextMode="MultiLine" ID="txtMessage" runat="server" Rows="5" CssClass="form-control form-textarea required" ValidationGroup="g2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Font-Size="11px" Display="Dynamic" ErrorMessage="* نص الرسالة ..." ValidationGroup="g2" ControlToValidate="txtMessage" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="" id="edit-actions">
                                                <asp:Button ID="btnSend" runat="server" Text="إرسال رسالة" Style="border-radius: 8px" class="btn btn-primary" OnClick="btnSend_Click" ValidationGroup="g2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlOK" runat="server" Visible="false">
                            <div class="contact-form-cont">
                                <h2 class="text-center">تأكيد الإرسال</h2>
                                <div id="contact-form" class="contact-form">
                                    <div class="user-info-from-cookie contact-form">
                                        <div>
                                            <br />
                                            <br />
                                            <br />
                                            <h2 class="text-center">تم إرسال رسالتك بنجاح <i class="fa fa-check"></i>
                                            </h2>
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnOk" runat="server" Text="حسناً" OnClick="btnOk_Click" Style="border-radius: 8px" class="btn btn-primary" />
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
        </section>
        <div class="position-relative">
            <asp:Panel ID="pnlTitle" runat="server">
                <div class="main-welcome">
                    <div class="cover-image"></div>
                    <div class="container px-lg-0">
                        <div class="breadcrumb-cont">
                            <%--<ol class="breadcrumb">
                                <li> معنا</li>
                            </ol>--%>
                            <h5>
                                <a href="/ar">الرئيسية</a> > تواصلوا معنا
                            </h5>
                        </div>
                        <h1>تواصلوا معنا</h1>
                    </div>
                </div>
                <section class="content-section  content-section-map  pt-0 ">
                    <div class="container px-0">
                        <div class="row">
                            <div class="position-relative map-det-cont  col-md-8  col-lg-7   col-xl-6">
                                <div class="contact-box">
                                    <p class="box-contact">
                                        العنوان<br />
                                        <a class="link-phone" target="_blank" runat="server" id="IDLocation">
                                            <asp:Label ID="lblNameOrg" runat="server"></asp:Label>
                                        </a>
                                    </p>
                                    <p class="box-contact box-contact-bo mb-0">
                                        الصندوق البريدي : [العنوان الوطني]
                                        50580
                                    </p>
                                    <p class="box-contact box-contact-bo-content">

                                        <br />
                                        الرمز البريدي : 
                                        41533
                                    </p>
                                    <p class="box-contact box-contact-email">
                                        البريد الإلكتروني:<br>

                                        <a class="link-site" runat="server" id="IDEmail">
                                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        </a>
                                    </p>
                                    <p class="article-contact">مواقع التواصل الاجتماعي</p>
                                    <div class="d-flex">
                                        <ul class="list-inline list-unstyled sm-list  pr-0 d-flex justify-content-center">
                                            <li><a runat="server" id="IDtwitter"><span class="fab fa-twitter"></span></a></li>
                                            <li><a runat="server" id="IDFacebook"><span class="fab fa-facebook-f"></span></a></li>
                                            <li><a runat="server" id="IDyoutube"><span class="fab fa-youtube"></span></a></li>
                                            <li><a runat="server" id="IDGoogleplus"><span class="fab fa-google-plus"></span></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="position-static  map-google-cont col-md-4  col-lg-5   col-xl-6">
                                <div id="map"></div>
                            </div>
                        </div>
                    </div>
                </section>
            </asp:Panel>
        </div>
    </div>
    <!-- End main Wrapper -->

    <script defer="defer" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyATcTe6_Ujsix8SbD5qNTzU6DjFjvt11X0&amp;callback=initMap"></script>
    <script src="../view/sites/default/files/js/js_8FRl3nRJx6n1D9bQJC2ebuA32SXnw91n09ESpdkNdkg.js"></script>
    <script src="../view/sites/default/files/js/js_FbpwIZNwgzwEuuL4Q2HOM07BOSCY5LxL_gwSK4ohQBM.js"></script>
</asp:Content>

