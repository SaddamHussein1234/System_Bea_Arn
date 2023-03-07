<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageArticleAdd.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageArticleAdd" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <style>
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 15%;
                margin: 1px;
            }

            .WidthText {
                float: right;
                Width: 13%;
                margin: 1px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
                padding-right: 5px;
            }

            .WidthText2 {
                float: right;
                Width: 32%;
                margin: 1px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                margin: 1px;
            }

            .WidthText40 {
                float: right;
                Width: 40%;
                margin: 1px;
            }

            .WidthText4 {
                float: right;
                Width: 49%;
                margin: 1px;
            }

            .WidthText5 {
                float: right;
                Width: 59%;
                margin: 1px;
            }

            .WidthText80 {
                float: right;
                Width: 79%;
                margin: 1px;
            }

            .WidthText20 {
                Width: 150px;
                height: 36px;
            }

            .WidthText50 {
                Width: 100%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }

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

            .WidthText40 {
                Width: 95%;
            }

            .WidthText5 {
                Width: 95%;
            }

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }

            .WidthText80 {
                Width: 95%;
            }

            .WidthText50 {
                Width: 95%;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>
    <script src="/SADDAMEditor4.19.1/ckeditor.js"></script>
    <link href="../css/chosen.css" rel="stylesheet" />
    <link href="../test/LoginAr.css" rel="stylesheet" />

    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><li class="fa fa-refresh"></li></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageArticle.aspx">إدارة المقالات</a></li>
                    <li><a href="PageArticleAdd.aspx">إضافة مقالة أو فعالية </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة مقالة أو فعالية"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>حدد القائمة : </h5>
                                        <asp:DropDownList ID="DLMenu" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown" Width="95%">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* حدد القائمة" ValidationGroup="g2" ControlToValidate="DLMenu" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>عرض المقالة في اللغة : </h5>
                                        <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" CssClass="dropdown-submenu DLYear" Width="95%" Enabled="false">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">عربي</asp:ListItem>
                                            <asp:ListItem Value="2">Türkçe</asp:ListItem>
                                            <asp:ListItem Value="3">English</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* حدد اللغة" ValidationGroup="g2" ControlToValidate="DLType" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>العنوان : </h5>
                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtTitle" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>صورة المقالة  : </h5>
                                        <span>سيقوم النظام بتحويل العرض 1772 بيكسل والطول الى 400 بكسل</span>
                                        <asp:FileUpload ID="FUArticle" runat="server" ToolTip="العرض 400 * الطول 1332px" data-toggle="tooltip" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="مطلوب" CssClass="font"
                                                ControlToValidate="FUArticle" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>حالة الظهور : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBView" runat="server" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>عرض في شريط الأخبار : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBBarView" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>عرض في السلايد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSlide" runat="server" onclick="ShowHideDiv(this)" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                        <script type="text/javascript">
                                            function ShowHideDiv(chkPassport) {
                                                var dvPassport = document.getElementById("IDCheck");
                                                dvPassport.style.display = chkPassport.checked ? "block" : "none";
                                            }
                                        </script>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>عرض في الكلمة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSite" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12" id="IDCheck" style="display: none;">
                                    <div class="form-group">
                                        <h5>رابط النقر على السلايد : </h5>
                                        <asp:TextBox ID="txtLinkClick" runat="server" class="form-control" ValidationGroup="g2" MaxLength="256" style="direction:ltr;" Text="#"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* الرابط" CssClass="font"
                                            ControlToValidate="txtLinkClick" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5>عرض في أهم الاخبار : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBLastNews" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5><span class="icon icon-twitter"></span>غرد في تويتر  : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTwitter" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>تاريخ المقالة :
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDateArticle" runat="server" class="form-control" Width="200" data-date-format="DD-MM-YYYY" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                                    ControlToValidate="txtDateArticle" ErrorMessage="* تاريخ المقالة" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-group">
                                        <h5>إرفق ملف (غير إجباري)  : </h5>
                                        <span>الملفات المسموح بها 
                                            "rar", "zip", "xls", "xlsx", "accdb", "pdf", "doc", "docx", "bmp", "gif", "png", "jpg", "jpeg"
                                        </span>
                                        <asp:FileUpload ID="FUImgTeacher" runat="server" />

                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <h5>تفاصيل المقالة : </h5>
                                        <asp:TextBox ID="txtDetails" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                        <script type="text/javascript" lang="javascript">CKEDITOR.replace('<%= txtDetails.ClientID %>');</script>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div style="float: left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px; font-size: medium"
                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" OnClick="btnAdd_Click" />
                <asp:LinkButton ID="LinkButton1" runat="server" Style="margin-right: 4px; font-size: medium" OnClick="LinkButton1_Click"
                    class="btn btn-danger btn-fill pull-left">رجوع</asp:LinkButton>
            </div>
            <div style="width: 50%">
            </div>
            <br />
            <br />
        </div>
        <br />
        <br />
        <br />

        <script type="text/javascript"><!--
    $('.date').datetimepicker({
        pickTime: false
    });

    $('.time').datetimepicker({
        pickDate: false
    });

    $('.datetime').datetimepicker({
        pickDate: true,
        pickTime: true
    });
    //--></script>
        <script type="text/javascript"><!--
    $('#language a:first').tab('show');
    $('#option a:first').tab('show');
    //--></script>
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

