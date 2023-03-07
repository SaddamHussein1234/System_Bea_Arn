<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAboutAr.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageAboutAr" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <style type="text/css">
        @media screen and (min-width: 768px) {
            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText2 {
                float: right;
                Width: 32%;
                padding-left: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 100%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText2 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }
        }
    </style>
    <script src="/SADDAMEditor4.19.1/ckeditor.js"></script>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageSettingTitle.aspx">إعدادات العناوين</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إعدادات العناوين"></asp:Label>
                    </h3>
                </div>
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
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText4">
                                    <div class="form-group">
                                        <h5>فيديو العرض التقديمي عربي : </h5>
                                        <iframe width="100%" height="313px" runat="server" id="IDVideo" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>
                                    </div>
                                </div>
                                <div style="float: left; padding: 5px;">
                                    <h5>شعار الرؤيا عربي  : </h5>
                                    <asp:Image ID="Img" runat="server" Style="border-radius: 6px" Width="220px" Height="147px" />
                                    <br />
                                    <span>سيقوم النظام بتحويل العرض 220 بيكسل والطول الى 147 بكسل</span>
                                    <asp:FileUpload ID="FUImgVision" runat="server" />

                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText2">
                                        <div class="form-group">
                                            <h5>رابط فيديو العرض التقديمي عربي : </h5>
                                            <asp:TextBox ID="txtLink" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="txtLink" ErrorMessage="مطلوب" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText5">
                                        <div class="form-group">
                                            <h5>نبذة تعريفية عربي : </h5>
                                            <asp:TextBox ID="txtAbout" runat="server" Style="direction: ltr; text-align: right" class="form-control textarea_editor span12" Rows="6" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                            <script type="text/javascript" lang="javascript">CKEDITOR.replace('<%= txtAbout.ClientID %>');</script> 
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText5">
                                        <div class="form-group">
                                            <h5>الرؤية عربي : </h5>
                                            <asp:TextBox ID="txtVision" runat="server" Style="direction: ltr; text-align: right" class="form-control textarea_editor span12" Rows="3" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                            <script type="text/javascript" lang="javascript">CKEDITOR.replace('<%= txtVision.ClientID %>');</script> 
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText5">
                                        <div class="form-group">
                                            <h5>الرسالة عربي : </h5>
                                            <asp:TextBox ID="txtMessage" runat="server" Style="direction: ltr; text-align: right" class="form-control textarea_editor span12" Rows="3" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                            <script type="text/javascript" lang="javascript">CKEDITOR.replace('<%= txtMessage.ClientID %>');</script> 
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText5">
                                        <div class="form-group">
                                            <h5>الاهداف عربي : </h5>
                                            <asp:TextBox ID="txtGoals" runat="server" Style="direction: ltr; text-align: right" class="form-control textarea_editor span12" Rows="10" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                            <script type="text/javascript" lang="javascript">CKEDITOR.replace('<%= txtGoals.ClientID %>');</script> 
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText5">
                                        <div class="form-group">
                                            <h5>القيم عربي : </h5>
                                            <asp:TextBox ID="txtValus" runat="server" Style="direction: ltr; text-align: right" class="form-control textarea_editor span12" Rows="3" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                            <script type="text/javascript" lang="javascript">CKEDITOR.replace('<%= txtValus.ClientID %>');</script> 
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="WidthText4">
                                    <div class="form-group">
                                        <h5>تعديل صفحة من نحن : </h5>
                                        <asp:TextBox ID="txtAbout" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                        <script type="text/javascript">
                                            var textbox = document.getElementById('<%= txtAbout.ClientID %>');
                                            CKEDITOR.replace(textbox,
                                          {
                                              filebrowserImageUploadUrl: '../Saddam/imupload.ashx'

                                          });
                                        </script>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="container-fluid">
                                <br />
                                <div style="float: left">
                                    <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" OnClick="btnAdd_Click"
                                        class="btn btn-info" ValidationGroup="g2" />
                                    <asp:LinkButton ID="LBBack" runat="server"
                                        class="btn btn-danger">رجوع</asp:LinkButton>
                                </div>
                                <br />
                                <br /><br /><br /><br /><br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

