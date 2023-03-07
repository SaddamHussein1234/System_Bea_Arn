<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductWarehousebyContainedAndIssued.aspx.cs" Inherits="CResearchers_CPanelManageWarehouse_PageManageProductWarehousebyContainedAndIssued" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSearch.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <style>
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 250px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText2 {
                Width: 150px;
                height: 36px;
            }
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

            .WidthText1 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
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

            .WidthText3 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }
        }
    </style>

    <script type="text/javascript">
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
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVByContainedAndIssued.ClientID%>");
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

    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageManageProductShippingWarehouse.aspx" data-toggle="tooltip" title="إضافة شحنة جديدة" class="btn btn-primary" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>

                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a>المستودع</a></li>
                        <li><a href="PageManageProductWarehousebyContainedAndIssued.aspx">بحث تفاصيل الوارد والصادر</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>بحث تفاصيل الوارد والصادر
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" ValidationGroup="g2"
                                class="btn btn-info btn-fill pull-right" OnClick="btnSearch_Click" />
                            &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                        <div style="float: left">
                            <span>حدد الصنف : </span>
                            <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="g2" Width="150px" CssClass="form-control2 WidthText2" Style="font-size: 12px;">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblCategory" runat="server" ForeColor="Red" Font-Size="10px" Visible="false" Text="* حدد الصنف"></asp:Label>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <div class="HideNow">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                            </div>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="بحث تفاصيل الوارد" placeholder='عنوان البحث' Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVByContainedAndIssued" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVByContainedAndIssued_RowDataBound"
                                                UseAccessibleHeader="False">
                                                <Columns>
                                                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="ProductID" Visible="false" />
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="رقم المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("IDNumberProduct")%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ينتمي إلى" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("AffiliationName")%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="عنوان المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("ProductName")%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الوارد" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSumSet" runat="server" Font-Size="12px" Text='<%# FSetSum((Int64) Eval("ProductID"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الصادر" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSumGet" runat="server" Font-Size="12px" Text='<%# FGetSum((Int64) Eval("ProductID"))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="المتبقي" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <a href='PageManageProductByDetails.aspx?XID=<%# Eval("ProductID")%>&ID=<%# Eval("IDUniq")%>' title="عرض المنتجات" data-toggle="tooltip" target="_blank" class="btn btn-info">
                                                                <span style="font-size: 12px;">
                                                                    <i class="fa fa-list"></i>
                                                                    <%# Eval("CountProduct") %>  
                                                                </span>
                                                            </a>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                    PreviousPageText=" السابق - " PageButtonCount="30" />
                                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div class="container-fluid" dir="rtl" runat="server">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <div class="WidthMaglis" align='center' runat="server" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                    أمين المستودع
                                                                    <br />
                                                                    <asp:Image ID="ImgIDStorekeeper" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblIDStorekeeper" runat="server" Font-Size="20px"></asp:Label>
                                                                    <asp:DropDownList ID="DLIDStorekeeper" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLIDStorekeeper_SelectedIndexChanged"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                    مدير الجمعية
                                                                    <br />
                                                                    <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="20px"></asp:Label>
                                                                    <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah_SelectedIndexChanged"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                    أمين الصندوق
                                                                    <br />
                                                                    <asp:Image ID="ImgAmeenAlSondoq" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblAmeenAlSondoq" runat="server" Font-Size="20px"></asp:Label>
                                                                    <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLAmeenAlSondoq_SelectedIndexChanged"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                    رئيس مجلس الإدارة
                                                                    <br />
                                                                    <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="20px"></asp:Label>
                                                                    <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah_SelectedIndexChanged"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis" align="center">
                                                                    <div runat="server" id="IDKhatm" align='left' style="margin-top: 0px">
                                                                        <img src="../../ImgSystem/ImgSignature/الختم.png" width="120" />
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                            <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            / <span style="font-size: 12px; padding-right: 5px">مجموع الوارد : </span>
                                            <asp:Label ID="lblSumSet" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            / <span style="font-size: 12px; padding-right: 5px">المجموع : </span>
                                            <asp:Label ID="lblSumGet" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
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
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
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
                                <h3 style="font-size: 20px">يرجى تحديد البيانات
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
                        </asp:Panel>
                    </div>
                </div>
            </div>
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
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

