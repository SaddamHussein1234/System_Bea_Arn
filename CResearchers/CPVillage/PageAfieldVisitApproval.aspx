<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="PageAfieldVisitApproval.aspx.cs" Inherits="CResearchers_CPVillage_PageAfieldVisitApproval" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <style>
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText2 {
                Width: 100px;
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
                Width: 24%;
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
            var gv = document.getElementById("<%=GVAfieldVisitApproval.ClientID%>");
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
                    <a href="PageAfieldVisitAdd.aspx" data-toggle="tooltip" title="إضافة طلب جديد" class="btn btn-primary" id="IDAdd" runat="server"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                        OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageAfieldVisitApproval.aspx">قائمة زيارات تم الموافقه عليها</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة زيارات تم الموافقه عليها
                        </h3>
                        <div style="float: left">
                            القرية : 
                            <asp:DropDownList ID="DLAlQriah" runat="server" CssClass="dropdown" Height="34" Style="font-size: 12px; width: 140px">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:LinkButton ID="btnSearch" runat="server" data-toggle="tooltip" title="بحث" OnClick="btnSearch_Click"
                                class="btn btn-info pull-right"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                            &nbsp;
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Visible="false">
                            <div class="table table-responsive">
                                <asp:GridView ID="GVAfieldVisitApproval" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
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
                                        <asp:TemplateField HeaderText="ر/زيارة" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# Eval("NumberAlZyarah")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ر/مستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# Eval("NumberAlMosTafeed")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# Eval("NameMostafeed")%>
                                                <br />
                                                الباحث <%# ClassQuaem.FAlBaheth((Int32) (Eval("NameAlBaheth")))%>
                                                <br />
                                                <span style="font-size: 11px">موافقة </span><%# ClassSaddam.FCheckAllowModer2((bool) (Eval("IsModer")))%>
                                                <%--, <%# ClassSaddam.FCheckAllowRaeesMaglis((bool) (Eval("IsRaeesMaglesAEdarah")))%>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="نوع البحث" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassSaddam.FChangeStyleCheckbox2((bool) (Eval("BahthHalatMosTafeed")))%> <span style="font-size: 12px">بحث حالة مستفيد جديد</span>
                                                <br />
                                                <%# ClassSaddam.FChangeStyleCheckbox2((bool) (Eval("EadatAlBahthLestafeedHaly")))%> <span style="font-size: 12px">إعادة البحث لمستفيد حالي</span>
                                                <br />
                                                <%# ClassSaddam.FChangeStyleCheckbox2((bool) (Eval("EadatAlBahthLeMostafeedSabeq")))%> <span style="font-size: 12px">إعادة البحث لمستفيد سابق</span>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="تاريخ الزيارة" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("DayAlZeyarah")))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassQuaem.FAlBaheth((Int32) (Eval("IDAdmin")))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="موافقه من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassQuaem.FAlBaheth((Int32) (Eval("IDManage")))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16px">
                                            <ItemTemplate>
                                                <a href='PageAfieldVisitDetails.aspx?ID=<%# Eval("NumberAlZyarah")%>&XID=<%# Eval("IDUniq")%>' title="عرض الملف" data-toggle="tooltip"
                                                    class="btn btn-info"><span class="fa fa-eye"></span></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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
                            </div>
                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                            <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            <div align="Left">
                                <img src='../../Img/IconTrue.png' style='width: 20px' />
                                <span style="font-size: 11px">إطلع</span>
                                <img src='../../Img/IconFalse.png' style='width: 20px' />
                                <span style="font-size: 11px">لم يطلع</span>
                            </div>
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
                                <h3 style="font-size: 20px">يرجى تحديد القرية
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
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
</asp:Content>

