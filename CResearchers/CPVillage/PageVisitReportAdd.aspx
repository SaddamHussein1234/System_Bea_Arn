<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="PageVisitReportAdd.aspx.cs" Inherits="CResearchers_CPVillage_PageVisitReportAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
            document.getElementById("<%=btnAddDevice.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;

        function insertConfirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <style type="text/css">
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .Dir {
            text-align: center;
        }

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

            .WidthText5 {
                float: right;
                Width: 100%;
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

            .WidthText5 {
                Width: 95%;
            }

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>
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
            var gv = document.getElementById("<%=pnlDevice.ClientID%>");
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
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>
    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="">تقرير الزيارات</a></li>
                    <li><a href="PageVisitReportAdd.aspx">إضافة تقرير زيارة ميدانية</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbMsg" runat="server" Text="بيانات المستفيد"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="container-fluid" dir="rtl">
                            <div class="WidthText1">
                                <div class="form-group">
                                    <h5>رقم المستفيد :
                                    </h5>
                                    <asp:TextBox ID="txtNumberMostafeed" runat="server" class="form-control" ValidationGroup="g2" AutoPostBack="true" OnTextChanged="txtNumberMostafeed_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator60" runat="server"
                                        ControlToValidate="txtNumberMostafeed" ErrorMessage="* رقم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="txtNumberMostafeed"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="WidthText1">
                                <div class="form-group">
                                    <h5>أو إسم المستفيد : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLName" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" AutoPostBack="true" OnSelectedIndexChanged="DLName_SelectedIndexChanged">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="WidthText1">
                                <div class="form-group">
                                    <h5>رقم التقرير :
                                    </h5>
                                    <asp:TextBox ID="txtNumberReport" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtNumberReport" ErrorMessage="* رقم الزيارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberReport"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="WidthText1">
                                <div class="form-group">
                                    <h5>تاريخ التقرير : <span style="color: red">*</span>
                                    </h5>
                                    <div class="col-sm-3">
                                        <div class="input-group date " style="margin-right: -10px">
                                            <asp:TextBox ID="txtDateReport" runat="server" class="form-control" Width="120" data-date-format="DD-MM-YYYY" ValidationGroup="g5" Style="direction: ltr"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                                ControlToValidate="txtDateReport" ErrorMessage="* تاريخ الزيارة" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g5" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlData" runat="server" Visible="false">
                            <div class="container-fluid" dir="rtl">
                                <div class="WidthText4">
                                    <div class="form-group">
                                        الاسم :
                                    
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        القرية :
                                    
                                    <asp:Label ID="lblAlQariah" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText1">
                                    <div class="form-group">
                                        الجنس :
                                    
                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>رقم الهاتف :
                                        </h5>
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>حالة المستفيد :
                                        </h5>
                                        <asp:Label ID="lblHalatAlmostafeed" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>السجل المدني :
                                        </h5>
                                        <asp:Label ID="lblNumberSigal" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>تاريخ الميلاد :
                                        </h5>
                                        <asp:Label ID="lblDateBrithDay" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="WidthText3">
                                    <div class="form-group">
                                        <h5>العمر :
                                        </h5>
                                        <asp:Label ID="lblAge" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlCheckData" runat="server" Visible="false">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label7" runat="server" Text="إحتياجات الاجهزة الكهربائية"></asp:Label>
                        </h3>
                        <div style="float: left">
                            <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                                OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                            <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>حدد الجهاز :
                                            </h5>
                                            <asp:DropDownList ID="DLDivice" runat="server" Font-Size="Larger" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="GDevice">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                                ControlToValidate="DLDivice" ErrorMessage="* حدد الجهاز" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GDevice" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>العدد :
                                            </h5>
                                            <asp:TextBox ID="txtNumber" runat="server" Width="90%" ValidationGroup="GDevice" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtNumber" ErrorMessage="* العدد" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GDevice" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnAddDevice" runat="server" Text="إضافة الجهاز" Style="margin-right: 4px;" OnClick="btnAddDevice_Click"
                                                class="btn btn-info btn-fill pull-left" ValidationGroup="GDevice" />
                                        </div>
                                    </div>
                                </div>
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlDevice" runat="server" Visible="False">
                                        <asp:GridView ID="GVDevice" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItam"
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
                                                <asp:BoundField DataField="IDItam" HeaderText="IDItam" InsertVisible="False" ReadOnly="True"
                                                    SortExpression="IDItam" Visible="false" />
                                                <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <span style="margin-right: 5px"><%# Container.DataItemIndex + 1 %></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="الجهاز" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("ProductName")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("IDNumberCount")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Library_CLS_Arn.ERP.DataAccess.ClassDataAccess.FCheckAndChangeF((DateTime) (Eval("DateAddDevice")))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                            <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي "
                                                PreviousPageText=" السابق - " />
                                            <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                            <RowStyle CssClass="rows"></RowStyle>
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
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
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label4" runat="server" Text="إحتياجات السلة الغذائية"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>سلات غذائية :
                                            </h5>
                                            <asp:CheckBox ID="CBEgathy" runat="server" Font-Size="14px" ValidationGroup="g5" />
                                            <span style="margin-right: 10px">عدد : </span>
                                            <asp:TextBox ID="txtEgathy" runat="server" Width="50%" CssClass="Dir" Text="0"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtEgathy"
                                                ErrorMessage="رقم" ValidationExpression="^[0-9]+$" ValidationGroup="g5"
                                                Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="WidthText4">
                                        <div class="form-group">
                                            <h5>اُخرى :
                                            </h5>
                                            <asp:CheckBox ID="CBOther" runat="server" Font-Size="14px" ValidationGroup="g5" />
                                            <span style="margin-right: 10px">
                                                <asp:TextBox ID="txtOther" runat="server" Width="90%"></asp:TextBox></span>
                                        </div>
                                    </div>
                                    <div class="WidthText4">
                                        <div class="form-group">
                                            <h5>نسبة الإحتياج :
                                            </h5>
                                            <asp:DropDownList ID="DLPercint" runat="server" ValidationGroup="g2" Font-Size="Larger" Width="100%" CssClass="form-control2 " Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                <asp:ListItem Value="5">*****</asp:ListItem>
                                                <asp:ListItem Value="4">****</asp:ListItem>
                                                <asp:ListItem Value="3">***</asp:ListItem>
                                                <asp:ListItem Value="2">**</asp:ListItem>
                                                <asp:ListItem Value="1">*</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                                ControlToValidate="DLPercint" ErrorMessage="* نسبة الإحتياج" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g5" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                            <asp:Label ID="Label1" runat="server" Text="دعم بناء المنزل"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>بناء منزل :
                                            </h5>
                                            <asp:CheckBox ID="CBBenaaHome" runat="server" Font-Size="14px" ValidationGroup="g5" />
                                        </div>
                                    </div>
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>صور بناء منزل
                                            </h5>
                                            <asp:FileUpload ID="FBenaaHome" runat="server" AllowMultiple="true" ValidationGroup="gBenaaHome" />
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="FBenaaHome" ErrorMessage="* حدد الصور" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="gBenaaHome" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:LinkButton ID="LBBenaaHome" runat="server" CssClass="btn btn-info" data-toggle="tooltip" OnClick="LBBenaaHome_Click" title="رفع الصور" ValidationGroup="gBenaaHome">
                                        رفع الصور <span class="tip-bottom"><i class="fa fa-upload" style="font-size:16px"></i></span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="WidthText4">
                                        <asp:Repeater ID="RPTBenaaHome" runat="server">
                                            <ItemTemplate>
                                                <div class="WidthText3">
                                                    <a href='<%# "../" + Eval("ImgReport") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                        <img src='<%# "../" + Eval("ImgReport") %>' style="margin: 4px; border-radius: 5px" width="100%" height="92" />
                                                    </a>
                                                    <div align="center">
                                                        <asp:LinkButton ID="LBDeleteBenaaHome" runat="server" OnClientClick="return insertConfirmation();" title="حذف" data-toggle="tooltip" OnClick="LBDeleteBenaaHome_Click" CommandArgument='<%# Eval("IDItam") %>'>
                                                            <i class="fa fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
                            <asp:Label ID="Label2" runat="server" Text="دعم ترميم منزل"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>ترميم منزل :
                                            </h5>
                                            <asp:CheckBox ID="CBTarmemHome" runat="server" Font-Size="14px" ValidationGroup="g5" />
                                        </div>
                                    </div>
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>صور ترميم منزل
                                            </h5>
                                            <asp:FileUpload ID="FTarmemHome" runat="server" AllowMultiple="true" ValidationGroup="gTarmemHome" />
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="FTarmemHome" ErrorMessage="* حدد الصور" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="gTarmemHome" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:LinkButton ID="LBTarmemHome" runat="server" CssClass="btn btn-info" data-toggle="tooltip" OnClick="LBTarmemHome_Click" title="رفع الصور" ValidationGroup="gTarmemHome">
                                        رفع الصور <span class="tip-bottom"><i class="fa fa-upload" style="font-size:16px"></i></span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="WidthText4">
                                        <asp:Repeater ID="RPTTarmemHome" runat="server">
                                            <ItemTemplate>
                                                <div class="WidthText3">
                                                    <a href='<%# "../" + Eval("ImgReport") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                        <img src='<%# "../" + Eval("ImgReport") %>' style="margin: 4px; border-radius: 5px" width="100%" height="92" />
                                                    </a>
                                                    <div align="center">
                                                        <asp:LinkButton ID="LBDeleteTarmemHome" runat="server" OnClientClick="return insertConfirmation();" title="حذف" data-toggle="tooltip" OnClick="LBDeleteTarmemHome_Click" CommandArgument='<%# Eval("IDItam") %>'>
                                                            <i class="fa fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
                            <asp:Label ID="Label3" runat="server" Text="دعم تأثيث منزل"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>تأثيث منزل :
                                            </h5>
                                            <asp:CheckBox ID="CBTathithHome" runat="server" Font-Size="14px" ValidationGroup="g5" />
                                        </div>
                                    </div>
                                    <div class="WidthText3">
                                        <div class="form-group">
                                            <h5>صور تأثيث منزل
                                            </h5>
                                            <asp:FileUpload ID="FTathithHome" runat="server" AllowMultiple="true" ValidationGroup="gTathithHome" />
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="FTathithHome" ErrorMessage="* حدد الصور" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="gTathithHome" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:LinkButton ID="LBTathithHome" runat="server" CssClass="btn btn-info" data-toggle="tooltip" OnClick="LBTathithHome_Click" title="رفع الصور" ValidationGroup="gTathithHome">
                                        رفع الصور <span class="tip-bottom"><i class="fa fa-upload" style="font-size:16px"></i></span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="WidthText4">
                                        <asp:Repeater ID="RPTTathithHome" runat="server">
                                            <ItemTemplate>
                                                <div class="WidthText3">
                                                    <a href='<%# "../" + Eval("ImgReport") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                        <img src='<%# "../" + Eval("ImgReport") %>' style="margin: 4px; border-radius: 5px" width="100%" height="92" />
                                                    </a>
                                                    <div align="center">
                                                        <asp:LinkButton ID="LBDeleteTathithHome" runat="server" OnClientClick="return insertConfirmation();" title="حذف" data-toggle="tooltip" OnClick="LBDeleteTathithHome_Click" CommandArgument='<%# Eval("IDItam") %>'>
                                                            <i class="fa fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
                            <asp:Label ID="Label5" runat="server" Text="الإدارة"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>إسم الباحث : 
                                            </h5>
                                            <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>مدير الجمعية :
                                            </h5>
                                            <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="WidthText1">
                                        <div class="form-group">
                                            <h5>رئيس لجنة البحث : 
                                            </h5>
                                            <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
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
                        class="btn btn-info btn-fill pull-left" ValidationGroup="g5" OnClick="btnAdd_Click" />
                    <asp:LinkButton ID="LinkButton1" runat="server" Style="margin-right: 4px; font-size: medium"
                        class="btn btn-danger btn-fill pull-left">خروج بدون حفظ</asp:LinkButton>
                </div>
                <div style="width: 50%">
                </div>
                <br />
                <br />
            </div>
            <br />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlNullData" runat="server" Direction="RightToLeft" Visible="false">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label6" runat="server" Text="يرجى إدخال رقم زيارة صحيح"></asp:Label>
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
                                            <h3 style="font-size: 20px">حدد البيانات
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
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

