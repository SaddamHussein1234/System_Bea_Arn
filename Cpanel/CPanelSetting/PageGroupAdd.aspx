<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelSetting/MPCPanel.master" AutoEventWireup="true" CodeFile="PageGroupAdd.aspx.cs" Inherits="Cpanel_CPanelSetting_PageGroupAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <style type="text/css">
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
                Width: 13%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
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
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText40 {
                float: right;
                Width: 40%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText4 {
                float: right;
                Width: 49%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText5 {
                float: right;
                Width: 59%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText80 {
                float: right;
                Width: 79%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }
            .WidthText100 {
                float: right;
                Width: 99%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText20 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText1 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText2 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText3 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText4 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText40 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText5 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }

            .WidthText80 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }
            .WidthText100 {
                Width: 95%;
                padding-right: 5px;
                background-color: #cecaca;
                border-radius: 5px;
                margin: 1px;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><li class="fa fa-refresh"></li></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageGroup.aspx">إدارة المجموعات</a></li>
                    <li><a href="PageGroupAdd.aspx">إضافة مجموعة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة مجموعة جديدة"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="col-sm-12">
                        <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-warning">تحذير</span>
                            <asp:Label ID="lblMessageWarning" runat="server"></asp:Label>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                            <span class="badge badge-pill badge-success">عملية ناجحة</span>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> عنوان المجموعة : </h5>
                                        <asp:TextBox ID="txtTitleGroup" runat="server" class="form-control" ValidationGroup="g2" Width="98%"></asp:TextBox>
                                        <asp:Label ID="lblTitleGroup" runat="server" ForeColor="Red" Font-Size="11px"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> حالة تفعيل المجموعة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBActive" runat="server" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne1" aria-expanded="false"
                        aria-controls="collapseOne" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label3" runat="server" Text="1 - نظام الإعدادات والصلاحيات"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne1" aria-expanded="false"
                        aria-controls="collapseOne" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne1" class="collapse">
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> إعدادات النظام : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBSettingMainAdd" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">الإعدادات الرئيسية </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> المجموعات  : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBGroupView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض المجموعات </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBGroupAdd" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">إضافة المجموعات </span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> المستخدمين : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBAdminView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض المستخدمين </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBAdminAdd" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">إضافة المستخدمين </span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>  
                </div>
            </div>

        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne2" aria-expanded="false"
                        aria-controls="collapseTwo" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label4" runat="server" Text="2 - نظام إدارة الموقع"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne2" aria-expanded="false"
                        aria-controls="collapseTow" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne2" class="collapse">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إدارة الموقع : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSettingTitleAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إعدادات العناوين </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSettingDataAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إعدادات البيانات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSettingAboutAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">صفحة من نحن </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> القائمة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBMenu" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">تصفح قائمة الموقع </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBMenuAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة قوائم للموقع </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> المقالات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBArticle" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض المقالات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBArticleAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة مقالة </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlAlBarakahView" runat="server" Visible="false">
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5>مجالات عمل المؤسسه : </h5>
                                            <asp:CheckBox ID="CBObjectivesFoundation" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>

                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5>مكتبة الفيديو : </h5>
                                            <asp:CheckBox ID="CBVideo" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5>شركاؤنا : </h5>
                                            <asp:CheckBox ID="CBPartner" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5>تغريدات تويتر : </h5>
                                            <asp:CheckBox ID="CBTwitter" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>

                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5>تتبع زوار الموقع : </h5>
                                            <asp:CheckBox ID="CBTrikerUser" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5>تتبع البيانات : </h5>
                                            <asp:CheckBox ID="CBTrikerAdmin" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> البوم الصور : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAlbum" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض البوم الصور </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAlbumAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة البوم الصور </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> الرسائل : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBMessage" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض الرسائل </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBMessageAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">حذف الرسائل </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne3" aria-expanded="false"
                        aria-controls="collapseThree" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label5" runat="server" Text="3 - نظام إدارة البحث الإجتماعي"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne3" aria-expanded="false"
                        aria-controls="collapseThree" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne3" class="collapse">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات العرض : </h4>
                                </div>
                                <div class="WidthText100">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> قائمة الجمعية (صلاحيات العرض) : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVillageView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض القُرى </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBBeneficiaryStatusView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض حالة المستفيد </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTypeOfDwellingView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض نوع المسكن </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBMonthlyIncomeView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض الدخل الشهري </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBInitiativesView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض المبادرات والداعمين </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHousingStatusView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض حالة المسكن </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSupportTypeView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض نوع الدعم </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBBeneficiaryFamilyView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض قرابة عائلة المستفيد </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBBeneficiaryRelationshipView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض صلة قرابة المستفيد </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> المستفيدين : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBBeneficiaryBySearchView" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">عرض المستفيدين </span>
                                        </label>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBBeneficiaryByView" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">عرض الإستمارة </span>
                                        </label>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> بحث حالة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSearchStatusView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">بحوث الحالات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSearchStatusDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إستمارة بحث </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> قرارات القبول : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAcceptanceDecisionView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">قرارات القبول </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAcceptanceDecisionDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إستمارة قرار </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> قرارات الإستبعاد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTecisionToExcludeView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">قرارات الإستبعاد </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTecisionToExcludeDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إستمارة إستبعاد </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText40">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> الزيارات الميدانية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CVisitBApprovalView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">زيارات تم الموافقه عليها </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitNotApprovedView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">زيارات لم يوافق عليها </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض كشف الزيارة </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> تقارير الزيارات الميدانية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitReportView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">تقارير الزيارات </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitReportDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نتيجة زيارة ميدانية </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إعادة مستفيد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBRe_beneficiaryView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">طلبات الإعادة </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBRe_beneficiaryDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إستمارة طلب إعادة </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إستبعاد مستفيد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBExclusionOfTheBeneficiaryView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">طلبات الإستبعاد </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBExclusionOfTheBeneficiaryDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إستمارة طلب إستبعاد </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إستبعاد مستفيد مؤقت : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTemporaryExclusionView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">طلبات الإستبعاد المؤقت </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTemporaryExclusionDetails" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إستمارة طلب إستبعاد </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText40">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> طلبات تحويل الحالات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBConvertedCasesView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">طلبات التحويل </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBConvertedCasesWaitingForApprovalView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">طلبات تحتاج إلى مراجعة </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBConvertedCasesDetailsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إستمارة طلب تحويل </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="container-fluid">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات الإضافة : </h4>
                                </div>
                                <div class="WidthText100">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> قائمة الجمعية (صلاحيات الإضافة) : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVillageAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة القُرى </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBBeneficiaryStatusAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة حالات المستفيد </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTypeOfDwellingAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة أنواع المسكن </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBMonthlyIncomeAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة الدخل الشهري </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBInitiativesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة المبادرات والداعمين </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHousingStatusAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة حالات المسكن </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSupportTypeAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة أنواع الدعم </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBBeneficiaryFamilyAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة قرابة عائلة المستفيد </span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBBeneficiaryRelationshipAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة صلة قرابة المستفيد </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText40">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> المستفيدين : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBAddBeneficiaryAdd" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">إضافة وتعديل المستفيدين </span>
                                        </label>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBBeneficiaryAddBoysAdd" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">إضافة وتعديل أفراد الأسرة </span>
                                        </label>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBBeneficiaryByRaeesAlLagnah" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme" style="margin-right: 50px">موافقة رئيس اللجنة </span>
                                        </label>

                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBBeneficiaryByModer" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme" style="margin-right: 50px">موافقة المدير </span>
                                        </label>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBBeneficiaryByRaeesAlMaglis" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme" style="margin-right: 50px">رئيس المجلس </span>
                                        </label>
                                    </div>
                                </div>
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> بحث حالة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSearchStatusAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة وتعديل بحث حالة </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSearchStatusManagerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة المدير </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBSearchStatusLagnatAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس اللجنة </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <%--نظام المؤسسة--%>
                                <asp:Panel ID="pnlAlBarakahAdd" runat="server" Visible="false">
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> مجالات عمل المؤسسه : </h5>
                                            <asp:CheckBox ID="CBObjectivesFoundationAdd" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> مكتبة الفيديو : </h5>
                                            <asp:CheckBox ID="CBVideoAdd" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> شركاؤنا : </h5>
                                            <asp:CheckBox ID="CBPartnerAdd" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                    <div class="WidthText">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> تغريدات تويتر : </h5>
                                            <asp:CheckBox ID="CBTwitterAdd" runat="server" Font-Size="14px" CssClass="checkbox-inline"
                                                ValidationGroup="g2" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> قرارات القبول : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAcceptanceDecisionAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة قرار </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAcceptanceDecisionApprovedAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">الموافقه على القرار </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> قرارات الإستبعاد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTecisionToExcludeAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة قرار </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTecisionToExcludeApprovedAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">الموافقه على القرار </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText40">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> الزيارات الميدانية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAfieldVisitAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة زيارة ميدانية </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAfieldVisitPendingApprovalAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة المدير </span>
                                            </label>
                                        </div>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAfieldVisitPendingApprovalByRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس المجلس </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> تقارير الزيارات الميدانية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitReportAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة تقرير زيارة </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitReportByModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة المدير </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitReportByRaeesAllagnahAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة لحنة البحث الاجتماعي </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إعادة مستفيد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBRe_beneficiaryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة طلب إعادة </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBRe_beneficiaryByModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة المدير </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBRe_beneficiaryByRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس المجلس </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إستبعاد مستفيد : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBExclusionOfTheBeneficiaryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة طلب إستبعاد </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBExclusionOfTheBeneficiaryByModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة المدير </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBExclusionOfTheBeneficiaryByRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس المجلس </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText2">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إستبعاد مستفيد مؤقت : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTemporaryExclusionAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة طلب إستبعاد </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTemporaryExclusionByModer" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة المدير </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBTemporaryExclusionByRaees" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس اللجنة </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> طلبات تحويل الحالات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBConvertedCasesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة طلب </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBConvertedCasesByModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة المدير </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
        
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne5" aria-expanded="false"
                        aria-controls="collapseFive" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label2" runat="server" Text="4 - نظام إدارة المستودع"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne5" aria-expanded="false"
                        aria-controls="collapseFive" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne5" class="collapse">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i>  صلاحيات العرض : </h4>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>  الإنتماء : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAffiliationView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض الإنتمائات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductByAffiliationView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">المنتجات حسب الإنتماء </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>  الاصناف : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBCategoryView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض الاصناف </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductByCategoryView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">المنتجات حسب الصنف </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> أماكن التخزين والمنتجات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBStoragePlacesView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض أماكن التخزين </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض المنتجات </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>  فواتير الشحن : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBIDBillView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض الفواتير </span>
                                            </label>
                                            <br /><br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText40">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> المستودع : </h5>
                                        <div class="keepmeLogged">

                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBWarehousebyContainedView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">بحث تفصيلي للوارد </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBWarehousebyIssuedView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">بحث تفصيلي للصادر </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBContainedAndIssuedView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">تفصيلي الوارد والصادر </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <hr />
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات الإضافة والتعديل والحذف : </h4>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> الإنتماء والاصناف : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAffiliationAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة الإنتمائات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBCategoryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة الاصناف </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> أماكن التخزين والمنتجات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBStoragePlacesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة أماكن التخزين </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة المنتجات </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>  شحن المستودع : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBManageProductShippingWarehouseAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">شحن المستودع(الوارد) </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText40">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>  موافقة الشحن : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductWarehouseStorekeeperAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">أمين المستودع </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductWarehouseApprovalOfTheDirectorAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">مدير الجمعية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductWarehouseCashierAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">المشرف المالي </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductWarehouseChairmanOfTheBoardAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">رئيس المجلس </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne4" aria-expanded="false"
                        aria-controls="collapseFour" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label6" runat="server" Text="5 - نظام إدارة أوامر الصرف"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne4" aria-expanded="false"
                        aria-controls="collapseFour" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne4" class="collapse">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات العرض : </h4>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> عرض أوامر الصرف : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBExchangeOrdersView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">فرز أوامر الصرف </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBSupportByBeneficiaryView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">فرز دعم المستفيد </span>
                                                </label>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> تفاصيل الفواتير : </h5>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAddThePriceToOrderView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">تفاصيل الفاتورة </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> الإحصاء المالي : </h5>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBFinancialStatisticsView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض الإحصاء </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات الإضافة والتعديل والحذف : </h4>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إنشاء أوامر الصرف : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBProductMatterOfExchangeAdd" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">أوامر الصرف</span>
                                        </label>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                                <div class="WidthText5">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> موافقة الصرف : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductStorekeeperAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">أمين المستودع </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductApprovalOfTheDirectorAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">مدير الجمعية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductCashierAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">المشرف المالي </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductViceBoardAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">نائب الرئيس </span>
                                            </label>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBProductChairmanOfTheBoardAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">رئيس المجلس </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> ملف المراجعة : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBProductFileSearchersAdd" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">ملف مراجعة الباحثين </span>
                                        </label>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne6" aria-expanded="false"
                        aria-controls="collapseSix" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label1" runat="server" Text="6 - نظام إدارة الزكاة"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne6" aria-expanded="false"
                        aria-controls="collapseSix" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne6" class="collapse">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات العرض : </h4>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> أصناف زكاة الفطر : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBCategoryZakatView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الأصناف </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> فواتير التبرع العيني : </h5>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBZakatAlfiterBillView" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض فواتير التبرع </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> الإحصاء المالي : </h5>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBZakatAlfiterBillInCome" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عرض الإحصاء المالي </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات الإضافة والتعديل والحذف : </h4>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> أصناف زكاة الفطر : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBCategoryZakatAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة الأصناف </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> فواتير التبرع العيني : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBZakatAlfiterBillAdd" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">إضافة الفواتير </span>
                                        </label>
                                        <br />
                                        <br />
                                    </div>
                                </div>                                
                                <div class="WidthText4">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> ملف المراجعة : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBZakatAlfiterBillAllowAmeenAlsondoq" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">موافقة المشرف المالي </span>
                                        </label>
                                        <i class="fa fa-minus"></i> 
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBZakatAlfiterBillAllowRaeesAlMajlis" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">موافقة رئيس المجلس </span>
                                        </label>
                                        <br />
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBZakatAlfiterBillAllowAmeenAlMostodaa" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">مصادقة أمين المستودع </span>
                                        </label>                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne7" aria-expanded="false"
                        aria-controls="collapseSix" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label7" runat="server" Text="7 - نظام الجمعية العمومية"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne7" aria-expanded="false"
                        aria-controls="collapseSix" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne7" class="collapse">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات العرض : </h4>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> بيانات الأعضاء : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBGeneralAssembly" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">أعضاء الجمعية العمومية </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> سندات الإشتراكات : </h5>
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBGeneralAssemblyBill" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">سندات الإشتراكات </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات الإضافة والتعديل والحذف : </h4>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> الجمعية العمومية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBGeneralAssemblyAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة عضو جديد </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> موافقة على الإستمارة : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBGeneralAssemblyAllow" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">موافقة رئيس المجلس </span>
                                        </label>
                                        <br />
                                        <br />
                                    </div>
                                </div>                                
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> سندات الإشتراكات : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBGeneralAssemblyBillAdd" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">إضافة سند جديد </span>
                                        </label>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> موافقة على السندات : </h5>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBGeneralAssemblyBillAmeen" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">موافقة المشرف المالي </span>
                                        </label>
                                        <br />
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBGeneralAssemblyBillRaees" runat="server" />
                                            <span class="slider round"></span>
                                            <span class="keepme">موافقة رئيس المجلس </span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <button type="button" data-toggle="collapse" data-target="#collapseOne8" aria-expanded="false"
                        aria-controls="collapseSix" class="btn">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label8" runat="server" Text="8 - نظام الموارد البشرية"></asp:Label>
                        </h3>
                    </button>
                    <div style="float:left">
                        <button type="button" data-toggle="collapse" data-target="#collapseOne8" aria-expanded="false"
                        aria-controls="collapseSix" class="btn">
                         <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div data-parent="#accordion" id="collapseOne8" class="collapse">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات العرض : </h4>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> إدارة الموظفين : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMEmpDetialsView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">بيانات الموظفين </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> إضافة الرواتب للموظفين : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMEmpSalaeryView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض رواتب الموظفين </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام مهام العمل : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMJobAssignmentView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض مهام العمل </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام الإجازات : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMCompensatoryView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الإجازات التعويضية </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMLeaveCategoryView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الإجازات </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMLeaveCategoryListView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">رصيد الإجازات </span>
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام الإستئذان : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMPermissionView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الإستئذان </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام المساءلات : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMAccountableView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض المساءلات </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام الإنذارات : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMWarningView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الحسومات </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام الحسومات : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMResolvedView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الحسومات </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام القروض : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMLoanView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض القروض </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام الإنتدابات : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMMandateView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الإنتدابات </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMMandateListView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">مسير الإنتدابات </span>
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام العمل الإضافي : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMOvertimeView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض العمل الإضافي </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMOvertimeListView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">مسير العمل الإضافي </span>
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام المكافآت : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMBonusesView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض المكافآت </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMBonusesListView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">مسير المكافآت </span>
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام الحضور : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMAttendanceEntryView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الحضور لموظف </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMAttendanceEntryAllView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض الحضور لاكثر من موظف </span>
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> نظام تسليم الرواتب : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMAddSalaryView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض كشف رواتب موظف </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBHRMAddSalaryListView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">عرض مسير الرواتب </span>
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="form-group">
                                    <h4><i class="fa fa-star"></i> صلاحيات الإضافة والتعديل والحذف : </h4>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> الجمعية العمومية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMSettingAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إعدادات النظام </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إدارة الموظفين : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMEmpDetialsAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة الموظفين </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> إضافة الرواتب للموظفين : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMEmpSalaeryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة رواتب الموظفين </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام مهام العمل : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMJobAssignmentAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة مهام العمل </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMJobAssignmentModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام الإجازات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMCompensatoryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة الإجازات التعويضية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMLeaveCategoryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة الإجازات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMLeaveCategoryModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMLeaveCategoryRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس الشؤون </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام الإستئذان : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMPermissionAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة إستئذان </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMPermissionModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMPermissionRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس الشؤون </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام المساءلات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMAccountableAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة مساءلات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMAccountableModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس الشؤون </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام الإنذارات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMWarningAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة إنذار </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMWarningModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMWarningRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس الشؤون </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام الحسومات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMResolvedAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة حسومات </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMResolvedModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMResolvedRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس الشؤون </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام القروض : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMLoanAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة قرض لموظف </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMLoanRepaymentAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">تسديد قرض لموظف </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMLoanModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMLoanRaeesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة رئيس الشؤون </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام الإنتدابات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMMandateAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة إنتداب لموظف </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMMandateModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام العمل الإضافي : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMOvertimeAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة قرار عمل إضافي </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMOvertimeModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام المكافآت : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMBonusesAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة قرار </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMBonusesModerAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">موافقة مدير الجمعية </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام الحضور : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMAttendanceEntryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إضافة سجل حضور </span>
                                            </label>
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> نظام تسليم الرواتب : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBHRMAddSalaryAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">تسليم الرواتب للموظفين </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
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
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px; font-size: medium" OnClick="btnAdd_Click"
                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="margin-right: 4px; font-size: medium"
                    class="btn btn-danger btn-fill pull-left" PostBackUrl="~/Cpanel/CPanelSetting/PageGroup.aspx">رجوع</asp:LinkButton>
            </div>
            <div style="width: 50%">
            </div>
            <br />
            <br />
        </div>
        <br />
        <br />
        <br />
</asp:Content>

