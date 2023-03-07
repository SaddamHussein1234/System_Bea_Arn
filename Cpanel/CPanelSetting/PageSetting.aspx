<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelSetting/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSetting.aspx.cs" Inherits="Cpanel_CPanelSetting_PageSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                Width: 50%;
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
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageSetting.aspx">إعدادات النظام</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إعدادات النظام"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div style="float: left; padding: 5px;">
                                <asp:Image ID="Img" runat="server" Style="border-radius: 6px" Width="264" Height="220" />
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>تاريخ إنتهاء السيرفر : </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtEndeSite" runat="server" Width="220px" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                    ControlToValidate="txtEndeSite" ErrorMessage="مطلوب" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>إسم السيرفر : </h5>
                                        <asp:TextBox ID="txtNameAr" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="txtNameAr" ErrorMessage="* إسم السيرفر" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <br />
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>إسم مدير النظام : </h5>
                                        <asp:TextBox ID="txtNameManager" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtNameManager" ErrorMessage="* إسم مدير النظام" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>شاشة البداية : </h5>
                                        <asp:DropDownList ID="DLStart" runat="server" ValidationGroup="g2" CssClass="dropdown-submenu DLYear" Width="95%" Enabled="false">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="ar" Selected="True">اللغة العربية</asp:ListItem>
                                            <asp:ListItem Value="tr">اللغة التركي</asp:ListItem>
                                            <asp:ListItem Value="en">اللغة الإنجليزي</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="DLStart" ErrorMessage="* شاشة البداية" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <h5>رسالة الإغلاق : </h5>
                                        <asp:TextBox ID="txtMessageClse" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>إغلاق الموقع : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBClose" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>شعار الموقع  : </h5>
                                            <span>الملفات المسموح بها "bmp", "gif", "png", "jpg", "jpeg"</span>
                                            <asp:FileUpload ID="FUImgTeacher" runat="server" ToolTip="العرض 317px * الطول 264px" data-toggle="tooltip" />
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <br />
                                <div style="float: left">
                                    <asp:Button ID="btnAdd" runat="server" Text="حفظ" OnClick="btnAdd_Click" Width="100px" style="margin-left:5px"
                                        class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                                    <asp:LinkButton ID="LBBack" runat="server"
                                        class="btn btn-danger btn-fill pull-left">رجوع</asp:LinkButton>
                                </div>
                                <div style="width: 50%">
                                    
                                </div>
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
</asp:Content>

