<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelSetting/MPCPanel.master" AutoEventWireup="true" CodeFile="PageGroupAdminEdit.aspx.cs" Inherits="Cpanel_CPanelSetting_PageGroupAdminEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageGroupAdmin.aspx">إدارة مجموعات المستفيدين</a></li>
                    <li><a href="PageGroupAdminAdd.aspx">تعديل مجموعة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="تعديل مجموعة جديدة للمستفيدين"></asp:Label>
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
                                                <input name="RememberMe" type="checkbox" id="CBActive" runat="server" />
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
                            <asp:Label ID="Label3" runat="server" Text="1 - صلاحيات العرض"></asp:Label>
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
                                            <h5><i class="fa fa-star"></i> تقرير بحث الحالة : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBStatusDetailsView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">السماح بالعرض </span>
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> قرار القبول  : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBAcceptanceDecisionView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">السماح بالعرض </span>
                                                </label>
                                                <br /><br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> بيانات الإستمارة  : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBFormDataView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">السماح بالعرض </span>
                                                </label>
                                                <br /><br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> الزيارات الميدانية : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBAfieldVisitApprovalView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">السماح بالعرض </span>
                                                </label>
                                                <br /><br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> إحتياجات المستفيد : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBVisitReportView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">السماح بالعرض </span>
                                                </label>
                                                <br /><br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> الدعم العيني : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBSupportView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">سلل - أجهزة - تأثيث المنزل </span>
                                                </label>
                                                <br />
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBSupportHomeView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">بناء - ترميم المنزل </span>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5><i class="fa fa-star"></i> الدعم النقدي : </h5>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBSupportMonyView" runat="server" />
                                                    <span class="slider round"></span>
                                                    <span class="keepme">السماح بالعرض </span>
                                                </label>
                                                <br /><br />
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
                            <asp:Label ID="Label4" runat="server" Text="2 - صلاحيات الإضافة"></asp:Label>
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
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> تعديل بيانات الإستمارة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBFormDataAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">السماح بالتعديل </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> بيانات أفراد الأسرة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBFormDataBoysAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">السماح بالإضافة </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> طلب زيارة ميدانية : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBAfieldVisitApprovalAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">السماح بالإضافة </span>
                                            </label>
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> طلب إحتياجات : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBVisitReportAdd" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme">السماح بالإضافة </span>
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
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px;" OnClick="btnAdd_Click"
                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="margin-right: 4px;"
                    class="btn btn-danger btn-fill pull-left" PostBackUrl="~/Cpanel/CPanelSetting/PageGroupAdmin.aspx">رجوع</asp:LinkButton>
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

