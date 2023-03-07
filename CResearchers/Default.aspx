<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CResearchers_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لوحة التحكم</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />
    <link rel="shortcut icon" href="../ImgSystem/ImgSetting/StartLogo.png" type="image/vnd.microsoft.icon" />
    <script type="text/javascript" src="../view/javascript/jquery/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../view/javascript/bootstrap/js/bootstrap.min.js"></script>
    <link href="../view/javascript/font-awesome/css/font-awesome.min.css" type="text/css"
        rel="stylesheet" />
    <link href="../view/javascript/summernote/summernote.css" rel="stylesheet" />
    <link href="../view/javascript/jquery/datetimepicker/bootstrap-datetimepicker.min.css"
        type="text/css" rel="stylesheet" media="screen" />
    <link href="../view/stylesheet/bootstrap-a.css" rel="stylesheet" media="screen" />
    <link type="text/css" href="../view/stylesheet/stylesheet-a.css" rel="stylesheet" media="screen" />
    <script src="../view/javascript/common.js" type="text/javascript"></script>
    <link href="../font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="../fonts/font-awesome.css" rel="stylesheet" />
    <style type="text/css">
        body, h1, h2, h3, h4, h5, h6, p, ul, a, input {
            font-family: 'Droid Arabic Kufi', serif;
        }

        @media screen and (min-width: 768px) {
            .FoDesktop {
            }
        }

        @media screen and (max-width: 767px) {
            .FoDesktop {
                display: none;
            }
        }
    </style>
    <link href="GridView.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <header id="header" class="navbar navbar-static-top ColorHome">
                <div class="navbar-header">
                    <%--<a type="button" id="button-menu" class="pull-left"><i class="fa fa-indent fa-lg"></i></a>--%>
                    <a href="Default.aspx" class="navbar-brand FoDesktop">لوحة تحكم الباحث
                        <%--<img src="../view/image/logo.png" id="ImgHome" runat="server" alt="لوحة التحكم" title="لوحة التحكم" width="123" height="23" />--%>
                    </a>
                </div>
                <ul class="nav pull-right">
                    <li><a href="##" class="FoDesktop" onclick="history.go(-1); return false;"><span class="hidden-xs hidden-sm hidden-md">رجوع</span> <i class="fa fa-share fa-lg"></i></a></li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown">
                            <span class="label label-danger pull-left">
                                <asp:Label ID="lblCountNotReed2" runat="server" Text="0"></asp:Label></span>
                            <span class="hidden-xs hidden-sm hidden-md">إشعارات</span> <i class="fa fa-bell fa-lg"></i></a>
                        <ul class="dropdown-menu dropdown-menu-right alerts-dropdown">
                            <%--<li class="dropdown-header">الرسائل</li>
                            <li><a href="PageMessage.aspx" style="display: block; overflow: auto;">
                                <span class="label label-warning pull-right">
                                    <asp:Label ID="lblCountNotReed" runat="server" Text="0"></asp:Label></span>لم تقرأ</a>
                            </li>
                            <li><a href="PageMessage.aspx">
                                <span class="label label-success pull-right">
                                    <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label></span>جميع الرسائل</a></li>
                            <li class="divider"></li>
                            <li class="dropdown-header">الاعضاء</li>
                            <li><a href="PageUserNewsAdmin.aspx">
                                <span class="label label-success pull-right">
                                    <asp:Label ID="lblCountAdmin" runat="server" Text="0"></asp:Label></span>الاعضاء المشرفين</a>
                            </li>
                            <li><a href="PageUserNewsBlock.aspx">
                                <span class="label label-danger pull-right">
                                    <asp:Label ID="lblCountBlock" runat="server" Text="0"></asp:Label></span>الاعضاء المحضورين</a></li>
                            <li><a href="PageUserNewsActive.aspx">
                                <span class="label label-danger pull-right">
                                    <asp:Label ID="lblCountActive" runat="server" Text="0"></asp:Label></span>جميع الاعضاء</a></li>--%>
                        </ul>
                    </li>
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown"><span class="hidden-xs hidden-sm hidden-md">حسابي</span> <i class="fa fa-user fa-lg"></i></a>
                        <ul class="dropdown-menu dropdown-menu-right">
                            <li class="dropdown-header"><i class="fa fa-file fa-lg"></i>بياناتي </li>
                            <li><a href="CHome/PageMyAccount.aspx">تعديل بياناتي</a></li>
                            <li class="divider"></li>
                            <li class="dropdown-header"><i class="fa fa-lock fa-lg"></i>كلمة المرور </li>
                            <li><a href="CHome/PageMyPassword.aspx">تعديل كلمة المرور</a></li>
                        </ul>
                    </li>
                    <li><a href="LogOut.aspx"><span class="hidden-xs hidden-sm hidden-md">خروج</span> <i class="fa fa-sign-out fa-lg"></i></a></li>
                </ul>
            </header>
            <div>
                <div id="content">
                    <div class="page-header">
                        <div class="container-fluid">
                            <h1>لوحة التحكم</h1>
                            <ul class="breadcrumb">
                                <li><a href="Default.aspx">الرئيسية</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="container-fluid" style="width: 70%">
                        <div class="row" style="margin: 5px; display: none">
                            <img src="../view/image/logo.png" style="width: 100%; height: 50%; float: left" />
                        </div>
                        <div class="row" style="margin: 5px; text-align: center">
                            <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px; background-color: #8b0101; color: #f5f5f5">
                                <h3 style="font-family: 'Alwatan';">نسخة تجريبية
                                </h3>
                            </div>
                        </div>
                        <div class="row" style="margin: 5px; text-align: center">
                            <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                                <br />
                                <h3 style="font-family: 'Alwatan';">القُرى المسموحة لك
                                </h3>
                                <br />
                            </div>
                        </div>
                        <div class="row" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px; margin: 5px;">
                            <br />
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <a href="Default.aspx" data-toggle="tooltip" title="الرئيسية">
                                    <div class="tile">
                                        <div class="tile-heading">
                                            الرئيسية <span class="pull-right"></span>
                                        </div>
                                        <div class="tile-body">
                                            <i class="fa fa-home"></i>
                                            <h2 class="pull-right"></h2>
                                        </div>
                                        <div class="tile-footer"></div>
                                    </div>
                                </a>
                            </div>
                            <asp:Repeater ID="RPTQariah" runat="server" Visible="false">
                                <ItemTemplate>
                                    <div class="col-lg-4 col-md-3 col-sm-6">
                                        <a href="SetQariahByUrl.aspx?IDQariah=<%# Eval("IDQariah") %>&IDUniq=<%# Convert.ToString(Guid.NewGuid()) %>" data-toggle="tooltip" title="<%# Eval("AlQriah") %>">
                                            <div class="tile">
                                                <div class="tile-heading">
                                                    <%# Eval("AlQriah") %> <span class="pull-right"></span>
                                                </div>
                                                <div class="tile-body">
                                                    <i class="fa fa-location-arrow"></i>
                                                    <h2 class="pull-right"></h2>
                                                </div>
                                                <div class="tile-footer"></div>
                                            </div>
                                        </a>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                <div class="col-lg-6 col-md-3 col-sm-6">
                                    <a href="Default.aspx" data-toggle="tooltip" title="الرئيسية">
                                        <div class="tile">
                                            <div class="tile-heading">
                                                لم يتم ربط القُرى بحسابك , راجع الإدارة <span class="pull-right"></span>
                                            </div>
                                            <div class="tile-body">
                                                <i class="fa fa-warning"></i>
                                                <h2 class="pull-right"></h2>
                                            </div>
                                            <div class="tile-footer"></div>
                                        </div>
                                    </a>
                                </div>
                            </asp:Panel>
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <a href="LogOut.aspx" data-toggle="tooltip" title="خروج">
                                    <div class="tile">
                                        <div class="tile-heading">
                                            خروج <span class="pull-right"></span>
                                        </div>
                                        <div class="tile-body">
                                            <i class="fa fa-share"></i>
                                            <h2 class="pull-right"></h2>
                                        </div>
                                        <div class="tile-footer"></div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="loading2" align="center" id="lodi">
                    <div>
                        <img src="../Img/Logo.png" width="200" style="background-color: #349301; padding: 5px; border-radius: 4px" />
                        <br />
                        <span style="background-color: #349301; padding: 5px; border-radius: 4px">يرجى الإنتظار , جاري تنفيذ المهام</span><br />
                        <br />
                        <img src="loader.gif" alt="" />
                    </div>
                </div>
                <footer id="footer">
                    جميع الحقوق محفوظة
                <a runat="server" id="IDSite" target="_blank">
                    <asp:Label ID="lblSite" runat="server" Text="جمعية البر والخدمات الاجتماعية بأرن"></asp:Label>
                </a>&copy;
                    <asp:Label ID="lblYears" runat="server"></asp:Label>
                </footer>
            </div>
    </form>
</body>
</html>
