<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBeneficiaryAddBoys.aspx.cs" Inherits="Cpanel_PageBeneficiaryAddBoys" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var headerCheckBox = inputList[0];
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVMenu.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي سجل");
                return false;
            }
            else {
                return confirm(" هل أنت متأكد من الحذف ؟");
            }
        }
    </script>
    <style>
        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

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

            .WidthText2 {
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

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }
        }
    </style>
    <link href="css/chosen.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnSearch" runat="server" data-toggle="tooltip" title="جلب" OnClick="btnSearch_Click"
                        class="btn btn-info"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                    &nbsp;
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" رقم المستفيد ... "></asp:TextBox>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>

                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBeneficiaryBySearch.aspx">إدارة المستفيدين</a></li>
                    <li><a href="PageBeneficiaryAddBoys.aspx">معلومات أفراد الاسرة</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="lbmsg" runat="server" Text="بيانات المستفيد "></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="WidthText1">
                                    <div class="form-group">
                                        <h5>الاسم :
                                        </h5>
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>نوع المستفيد :
                                        </h5>
                                        <asp:Label ID="lblTypeMostafeed" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>القرية :
                                        </h5>
                                        <asp:Label ID="lblAlQariah" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>الجنس :
                                        </h5>
                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>رقم الهاتف :
                                        </h5>
                                        0<asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText">
                                    <div class="form-group">
                                        <h5>السجل المدني :
                                        </h5>
                                        <asp:Label ID="lblNumberSigal" runat="server"></asp:Label>
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
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label4" runat="server" Text="إضافة بيانات أفراد الإسرة"></asp:Label>
                            - أفراد الاسرة : <span style="color: red">*</span>
                            <asp:TextBox ID="txtCountBoys" runat="server" ValidationGroup="g2" Width="150" Text="0"></asp:TextBox>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator28" runat="server"
                                ControlToValidate="txtCountBoys" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtCountBoys"
                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>
                            <asp:LinkButton ID="LBUpdate" runat="server" data-toggle="tooltip" title="تحديث عدد أفراد الاسرة" OnClick="LBUpdate_Click" ValidationGroup="g2"><span class="tip-bottom"><i class="fa fa-edit" style="font-size:16px"></i></span></asp:LinkButton>
                        </h3>

                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>الاسم : <span style="color: red">*</span>
                                            </h5>
                                            <asp:TextBox ID="txtName" runat="server" class="form-control" ValidationGroup="g1"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator24" runat="server"
                                                ControlToValidate="txtName" ErrorMessage="* إدخل الإسم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>القرابة : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLAlQarabah" runat="server" ValidationGroup="g1" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator25" runat="server"
                                                ControlToValidate="DLAlQarabah" ErrorMessage="* إدخل القرابة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>المستوى الدراسي : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="txtAlmostawaAlDerasy" runat="server" Width="100%" ValidationGroup="g1">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="01">الأول الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="02">الثاني الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="03">الثالث الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="04">الرابع الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="05">الخامس الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="06">السادس الابتدائي</asp:ListItem>
                                                <asp:ListItem Value="07">الأول المتوسط</asp:ListItem>
                                                <asp:ListItem Value="08">الثاني المتوسط</asp:ListItem>
                                                <asp:ListItem Value="09">الثالث المتوسط</asp:ListItem>
                                                <asp:ListItem Value="10">الأول الثانوي</asp:ListItem>
                                                <asp:ListItem Value="11">الثاني الثانوي</asp:ListItem>
                                                <asp:ListItem Value="12">الثالث الثانوي</asp:ListItem>
                                                <asp:ListItem Value="13">جامعة</asp:ListItem>
                                                <asp:ListItem Value="00">غير ذلك</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator30" runat="server"
                                                ControlToValidate="txtAlmostawaAlDerasy" ErrorMessage="* المستوى الدراسي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>العام الدراسي :
                                            </h5>
                                            <asp:DropDownList ID="DLYearStudy" runat="server" Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>رقم السجل المدني : <span style="color: red">*</span>
                                            </h5>
                                            <asp:TextBox ID="txtNumberSigal" runat="server" class="form-control" ValidationGroup="g1"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtNumberSigal" ErrorMessage="* رقم السجل" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumberSigal"
                                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g1" Font-Size="10px"
                                                Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>تاريخ الميلاد (هجري) : 
                                            </h5>
                                            <table>
                                                <tr>
                                                    <td style="padding-left: 3px">
                                                        <asp:DropDownList ID="ddlYearsH" runat="server" Width="70">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding-left: 3px">
                                                        <asp:DropDownList ID="ddlMonthsH" runat="server" Width="50">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDatesH" runat="server" Width="50" OnSelectedIndexChanged="ddlDatesH_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                            <asp:ListItem Text="13" Value="13" />
                                                            <asp:ListItem Text="14" Value="14" />
                                                            <asp:ListItem Text="15" Value="15" />
                                                            <asp:ListItem Text="16" Value="16" />
                                                            <asp:ListItem Text="17" Value="17" />
                                                            <asp:ListItem Text="18" Value="18" />
                                                            <asp:ListItem Text="19" Value="19" />
                                                            <asp:ListItem Text="20" Value="20" />
                                                            <asp:ListItem Text="21" Value="21" />
                                                            <asp:ListItem Text="22" Value="22" />
                                                            <asp:ListItem Text="23" Value="23" />
                                                            <asp:ListItem Text="24" Value="24" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="26" Value="26" />
                                                            <asp:ListItem Text="27" Value="27" />
                                                            <asp:ListItem Text="28" Value="28" />
                                                            <asp:ListItem Text="29" Value="29" />
                                                            <asp:ListItem Text="30" Value="30" />
                                                            <asp:ListItem Text="31" Value="31" />
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>تاريخ الميلاد (ميلادي) : 
                                            </h5>
                                            <table>
                                                <tr>
                                                    <td style="padding-left: 3px">
                                                        <asp:DropDownList ID="ddlYears" runat="server" Width="70">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding-left: 3px">
                                                        <asp:DropDownList ID="ddlMonths" runat="server" Width="50">
                                                            <asp:ListItem />
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDates" runat="server" Width="50" OnSelectedIndexChanged="ddlDates_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Text="01" Value="01" />
                                                            <asp:ListItem Text="02" Value="02" />
                                                            <asp:ListItem Text="03" Value="03" />
                                                            <asp:ListItem Text="04" Value="04" />
                                                            <asp:ListItem Text="05" Value="05" />
                                                            <asp:ListItem Text="06" Value="06" />
                                                            <asp:ListItem Text="07" Value="07" />
                                                            <asp:ListItem Text="08" Value="08" />
                                                            <asp:ListItem Text="09" Value="09" />
                                                            <asp:ListItem Text="10" Value="10" />
                                                            <asp:ListItem Text="11" Value="11" />
                                                            <asp:ListItem Text="12" Value="12" />
                                                            <asp:ListItem Text="13" Value="13" />
                                                            <asp:ListItem Text="14" Value="14" />
                                                            <asp:ListItem Text="15" Value="15" />
                                                            <asp:ListItem Text="16" Value="16" />
                                                            <asp:ListItem Text="17" Value="17" />
                                                            <asp:ListItem Text="18" Value="18" />
                                                            <asp:ListItem Text="19" Value="19" />
                                                            <asp:ListItem Text="20" Value="20" />
                                                            <asp:ListItem Text="21" Value="21" />
                                                            <asp:ListItem Text="22" Value="22" />
                                                            <asp:ListItem Text="23" Value="23" />
                                                            <asp:ListItem Text="24" Value="24" />
                                                            <asp:ListItem Text="25" Value="25" />
                                                            <asp:ListItem Text="26" Value="26" />
                                                            <asp:ListItem Text="27" Value="27" />
                                                            <asp:ListItem Text="28" Value="28" />
                                                            <asp:ListItem Text="29" Value="29" />
                                                            <asp:ListItem Text="30" Value="30" />
                                                            <asp:ListItem Text="31" Value="31" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddlDates" ErrorMessage="* إدخل العمر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                            ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>العمر : <span style="color: red">*</span> <a href="http://dirarab.net/dateconversion" target="_blank">الذهاب لموقع التحويل</a>
                                            </h5>
                                            <asp:TextBox ID="txtAge" runat="server" class="form-control" ValidationGroup="g1" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator27" runat="server"
                                                ControlToValidate="txtAge" ErrorMessage="* إدخل العمر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>المهنة الحالية : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLAlMehnah" runat="server" Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="رب منزل">رب منزل</asp:ListItem>
                                                <asp:ListItem Value="ربة منزل">ربة منزل</asp:ListItem>
                                                <asp:ListItem Value="موظف">موظف</asp:ListItem>
                                                <asp:ListItem Value="موظفة">موظفة</asp:ListItem>
                                                <asp:ListItem Value="طالب">طالب</asp:ListItem>
                                                <asp:ListItem Value="طالبة">طالبة</asp:ListItem>
                                                <asp:ListItem Value="عاطل">عاطل</asp:ListItem>
                                                <asp:ListItem Value="عاطلة">عاطلة</asp:ListItem>
                                                <asp:ListItem Value="طفل">طفل</asp:ListItem>
                                                <asp:ListItem Value="طفلة">طفلة</asp:ListItem>
                                                <asp:ListItem Value="غير ذلك">غير ذلك</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator29" runat="server"
                                                ControlToValidate="DLAlMehnah" ErrorMessage="* المهنة الحالية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>الحالة الصحية : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="txtAlHalahAlSehe" runat="server" Width="100%" ValidationGroup="g1">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="سليم">سليم</asp:ListItem>
                                                <asp:ListItem Value="سليمة">سليمة</asp:ListItem>
                                                <asp:ListItem Value="مريض">مريض</asp:ListItem>
                                                <asp:ListItem Value="مريضة">مريضة</asp:ListItem>
                                                <asp:ListItem Value="غير ذلك">غير ذلك</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator31" runat="server"
                                                ControlToValidate="txtAlHalahAlSehe" ErrorMessage="* الحالة الصحية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g1" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px;"
                                                class="btn btn-info btn-fill pull-left" ValidationGroup="g1" OnClick="btnAdd_Click" />
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
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label1" runat="server" Text="معلومات أفراد الإسرة"></asp:Label>
                        </h3>
                        <div style="float: left">
                            <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                                OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                            <li class="fa fa-trash-o"></li></span></asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlData" runat="server" Visible="False">
                                        <asp:GridView ID="GVMenu" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                            Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                            UseAccessibleHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="10px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IDItem" HeaderText="IDItem" InsertVisible="False" ReadOnly="True"
                                                    SortExpression="IDItem" Visible="false" />
                                                <asp:BoundField DataField="Name" HeaderText="الإسم" SortExpression="Name"
                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="القرابة" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# ClassQuaem.FQarabah((Int32) Eval("AlQarabah"))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="A2" HeaderText="السجل المدني" SortExpression="A2"
                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="تاريخ الميلاد" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# ClassSaddam.FCheckNullDate(ClassDataAccess.FChangeF((DateTime) Eval("DateBrith")))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="العمر" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# ClassSaddam.FGetAge((DateTime) (Eval("DateBrith")))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AlMehnahAlHaliah" HeaderText="المهنة الحالية" SortExpression="AlMehnahAlHaliah"
                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:BoundField DataField="A1" HeaderText="العام الدراسي" SortExpression="A1"
                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="المستوى الدراسي" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# ClassSaddam.FCheckAlmostawaAlDerasy(Convert.ToString(Eval("AlmostawaAlDerasy")))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AlHalahAlseHeyah" HeaderText="الحالة الصحية" SortExpression="AlHalahAlseHeyah"
                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin"))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="تاريخ التعديل" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# ClassSaddam.FCheckUpdateBoys(ClassDataAccess.FChangeF((DateTime) Eval("A3")))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                    <ItemTemplate>
                                                        <a href='PageBeneficiaryAddBoys.aspx?ID=<%# Eval("NumberMostafed")%>&XID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                                            class="btn btn-primary"><span class="fa fa-edit"></span></a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                            <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي "
                                                PreviousPageText=" السابق - " />
                                            <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                            <RowStyle CssClass="rows"></RowStyle>
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            <%--<SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />--%>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                        <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px">لا توجد نتائج
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlSelect" runat="server" Direction="RightToLeft" Visible="false">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label6" runat="server" Text="يرجى إدخال رقم سجل صحيح"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px">لا توجد نتائج
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>
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

