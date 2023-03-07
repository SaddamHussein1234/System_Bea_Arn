<%@ Page Title="" Language="C#" MasterPageFile="~/ar0/MPAr.master" AutoEventWireup="true" CodeFile="PageContact-Us.aspx.cs" Inherits="ar_PageContact_Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body onload="window.scroll(0, 340 )">
    <div class="main_body">
        <!-- Begin page content -->
        <div class="page-header">
            <div class="container">
                <span>إترك لنا رسالة
                </span>
            </div>
        </div>
        <div class="page_body">
            <div class="page_content container">
                <asp:Panel ID="pnlMessage" runat="server">
                    <div class="col-sm-12">
                        <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-warning">تحذير</span>
                            <asp:Label ID="lblWarning" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-success">عملية ناجحة</span>
                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-item form-item-name form-group WidthTex">
                            <label class="control-label" for="edit-name">غرض المراسلة <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip"><span style="color: red">*</span></span></label>
                            <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" CssClass="form-control">
                                <asp:ListItem Value='general'>عام</asp:ListItem>
                                <asp:ListItem Value='Inquiry'>طلب إستفسار</asp:ListItem>
                                <asp:ListItem Value='suggestion'>تقديم إقتراح</asp:ListItem>
                                <asp:ListItem Value='complaint'>تقديم شكوي</asp:ListItem>
                                <asp:ListItem Value='violation'>إبلاغ عن مخالفة</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="DLType" ErrorMessage="* الغرض" ForeColor="#FF0066"
                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-item form-item-name form-group WidthTex">
                            <label class="control-label" for="edit-name">اسمك الكريم <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip"><span style="color: red">*</span></span></label>
                            <asp:TextBox ID="txtName" runat="server" MaxLength="50" CssClass="form-control form-text required" ValidationGroup="g2"></asp:TextBox>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" Display="Dynamic" Font-Size="11px" runat="server" ControlToValidate="txtName" ErrorMessage="* إدخل الاسم" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-item form-item-name form-group WidthTex">
                            <label class="control-label" for="edit-mail">بريدك الإلكتروني </label>
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control form-text required" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Font-Size="11px" Display="Dynamic" ErrorMessage="* إدخل البريد " ForeColor="Red" ValidationGroup="g2" ControlToValidate="txtEmail" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                ControlToValidate="txtEmail"
                                ErrorMessage="بريد خاطئ"
                                Font-Size="11px"
                                Display="Dynamic"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ForeColor="#760000" ValidationGroup="g2"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="clearfix visible-sm-block"></div>
                    <div class="col-md-6">
                        <div class="form-item form-item-name form-group WidthTex">
                            <label class="control-label" for="edit-subject">موضوع رسالتك <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip"><span style="color: red">*</span></span></label>
                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="5000" CssClass="form-control form-text required" ValidationGroup="g2"></asp:TextBox>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" Display="Dynamic" Font-Size="11px" runat="server" ControlToValidate="txtTitle" ErrorMessage="* إدخل موضوع الرسالة" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-item form-item-name form-group WidthTex">
                            <label class="control-label" for="edit-subject">رقم هاتفك </label>
                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="20" CssClass="form-control form-text required" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" Display="Dynamic" Font-Size="11px" runat="server" ControlToValidate="txtPhone" ErrorMessage="* رقم الهاتف" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="clearfix visible-sm-block"></div>
                    <div class="col-md-12">
                        <div class="form-item form-item-name form-group">
                            <label class="control-label" for="edit-message">نص رسالتك <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip"><span style="color: red">*</span></span></label>
                            <div class="form-textarea-wrapper resizable">
                                <asp:TextBox TextMode="MultiLine" ID="txtMessage" runat="server" Rows="5" CssClass="form-control form-textarea required" ValidationGroup="g2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Font-Size="11px" Display="Dynamic" ErrorMessage="* نص الرسالة ..." ForeColor="Red" ValidationGroup="g2" ControlToValidate="txtMessage" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-item form-item-name form-group WidthTex">
                            <div style="margin-right: 70px">
                                <img src="../captcha.aspx" height="35" width="100" />
                            </div>
                            <label class="control-label" for="edit-subject">رمز التحقق <span class="form-required" title="هذا الحقل مطلوب." data-toggle="tooltip"><span style="color: red">*</span></span></label>
                            <asp:TextBox ID="txtCapatsha" runat="server" MaxLength="20" CssClass="form-control form-text required" ValidationGroup="g2" Style="direction: ltr"
                                TextMode="Number" placeholder="رمز التحقق ..." autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" Display="Dynamic" Font-Size="11px" runat="server" ControlToValidate="txtCapatsha" ErrorMessage="* رمز التحقق" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="" id="edit-actions" align="left">
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnSend" runat="server" Text="إرسال رسالتك" Style="border-radius: 8px" class="btn btn-info" OnClick="btnSend_Click" ValidationGroup="g2" />
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
                                    <asp:Button ID="btnOk" runat="server" Text="حسناً" OnClick="btnOk_Click" Style="border-radius: 8px" class="btn btn_green" />
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
    </body>
</asp:Content>

