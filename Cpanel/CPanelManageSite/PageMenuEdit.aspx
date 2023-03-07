<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMenuEdit.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageMenuEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                Width: 13%;
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
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>
    <link href="../css/chosen.css" rel="stylesheet" />
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">

                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageMenu.aspx">إدارة القوائم</a></li>
                    <li><a href="">تعديل قائمة للموقع </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="تعديل قائمة للموقع"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <h5>إسم القائمة عربي : </h5>
                                        <asp:TextBox ID="txtAr" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="txtAr" ErrorMessage="* إسم القائمة عربي" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5>إسم القائمة تركي : </h5>
                                        <asp:TextBox ID="txtTr" runat="server" class="form-control" ValidationGroup="g2" Text="-"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtTr" ErrorMessage="* إسم القائمة تركي" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="WidthText1" runat="server" visible="false">
                                    <div class="form-group">
                                        <h5>إسم القائمة إنجليزي : </h5>
                                        <asp:TextBox ID="txtEn" runat="server" class="form-control" ValidationGroup="g2" Text="-"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtEn" ErrorMessage="* إسم القائمة إنجليزي" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>ترتيب : </h5>
                                        <asp:TextBox ID="txtOrder" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="مطلوب" CssClass="font"
                                            ControlToValidate="txtOrder" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOrder"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>عرض بالقائمة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBView" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>حالة التفرع : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBFork" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <h5>نوع القائمة :
                                    </h5>
                                    <asp:RadioButtonList ID="rblCheck" runat="server" AutoPostBack="true"
                                        RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblCheck_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1" Text="رئيسي"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="فرعي"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group" runat="server" id="IDMenu" visible="false">
                                        <h5>حدد القائمة الرئيسية :
                                        </h5>
                                        <asp:DropDownList ID="DLMenu" runat="server" ValidationGroup="g2" CssClass="form-control">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class="form-group">
                                        <h5>تفاصيل القائمة : </h5>
                                        <asp:TextBox ID="txtDetails" runat="server" class="form-control" ValidationGroup="g2" Text="-" TextMode="MultiLine"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="مطلوب" CssClass="font"
                                            ControlToValidate="txtDetails" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
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
                    class="btn btn-danger btn-fill pull-left">خروج</asp:LinkButton>
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
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

