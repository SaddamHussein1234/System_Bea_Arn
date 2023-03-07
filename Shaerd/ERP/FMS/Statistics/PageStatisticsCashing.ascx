<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageStatisticsCashing.ascx.cs" Inherits="Shaerd_ERP_FMS_Statistics_PageStatisticsCashing" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterMony.ascx" TagPrefix="uc1" TagName="WUCFooterMony" %>
<script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <style type="text/css">
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
            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }
        }

        @font-face {
            font-family: 'Alwatan';
            font-size: 18px;
            src: url(/fonts/AlWatanHeadlines-Bold.ttf);
        }

        @media screen and (min-width: 768px) {
            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .Width10Percint {
                float: right;
                Width: 15%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }

            .Width10Percint {
                Width: 95%;
            }
        }
    </style>
    <script type="text/javascript">
        function ShowIDModelOrg() {
            $("#IDModelOrg").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                ShowIDModelOrg();
            });
        });
    </script>

<div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageStatisticsCashing.aspx">قائمة الإحصاء المالي لسندات الصرف</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i> الإحصاء المالي لسندات الصرف
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBoxList ID="CBAccount" runat="server"
                                    RepeatDirection="Horizontal" CssClass="styled" Width="300">
                                    <asp:ListItem Value="الصندوق">الصندوق</asp:ListItem>
                                    <asp:ListItem Value="البنك">البنك</asp:ListItem>
                                    <asp:ListItem Value="تبرع_عام">تبرع_عام</asp:ListItem>
                                    <asp:ListItem Value="مصاريف_تشغيلية">مصاريف_تشغيلية</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </h3>
                        <div style="float:left;">
                            <div style="float:left; margin-right:5px;">
                                <asp:Button ID="btnGet" runat="server" Text="بحث"
                                        CssClass="btn btn-info" ValidationGroup="VGDetails" OnClick="btnGet_Click" />
                            </div>
                            <div style="float:left; width:130px;">
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr;"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="txtDateTo" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="float:left; width:130px;">
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr;"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtDateFrom" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="float:left; margin-left:5px;">
                                <asp:DropDownList ID="DLCount" runat="server" ValidationGroup="GBill"
                                    CssClass="form-control" OnLoad="DLCount_Load">
                                    <asp:ListItem Value="ByYears"> الإرشيف فقط </asp:ListItem>
                                    <asp:ListItem Value="ByMain_"> البند الرئيسي </asp:ListItem>
                                    <asp:ListItem Value="ByOne" Selected="True"> بند فرعي واحد </asp:ListItem>
                                    <asp:ListItem Value="ByTwo"> بندين </asp:ListItem>
                                    <asp:ListItem Value="ByThree"> ثلاثة بنود </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="float:left; margin-left:5px; margin-top:10px;">
                                <span style="text-align: right;">الفلترة</span> : 
                            </div>
                            <div style="float:left; margin-left:5px;">
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        <asp:RadioButton ID="RBSingle" runat="server" GroupName="GADate" Checked="true" />
                                        <span class="slider round"></span> <span class="keepme">فردي</span>
                                    </label>
                                    <label class="switch">
                                        <asp:RadioButton ID="RBMulti" runat="server" GroupName="GADate" />
                                        <span class="slider round"></span> <span class="keepme">متعدد</span>
                                    </label>
                                </div>
                                <script type="text/javascript">
                                    $(function () {
                                        $(document.getElementById("<%=RBSingle.ClientID %>")).click(function () {
                                            if ($(this).is(":checked")) {
                                                $("#pnlSingle").show();
                                                $("#pnlMulti").hide();
                                            } else {
                                                $("#pnlSingle").hide();
                                                $("#pnlMulti").show();
                                            }
                                        });
                                    });
                                </script>
                                <script type="text/javascript">
                                    $(function () {
                                        $(document.getElementById("<%=RBMulti.ClientID %>")).click(function () {
                                            if ($(this).is(":checked")) {
                                                $("#pnlMulti").show();
                                                $("#pnlSingle").hide();
                                            } else {
                                                $("#pnlMulti").hide();
                                                $("#pnlSingle").show();
                                            }
                                        });
                                    });
                                </script>
                                <div id="pnlSingle" style="display: none; <%= FCheck("Single") %>">
                                    <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="GBill"
                                        CssClass="form-control2 chzn-select dropdown" Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div id="pnlMulti" style="display: none; <%= FCheck("Multi") %>">
                                    <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDModelOrg" title="btn btn-success" class="btn btn-success"><span class="fa fa-group"></span> عرض الجهات والأشخاص</a>
                                </div>
                            </div>
                            <script type="text/javascript">
                                function ValidateAdd() {
                                    var ddlFruits = document.getElementById("<%=DLCount.ClientID %>");

                                    if (ddlFruits.value == "ByMain_") {
                                        $("#IDMainItems").show();
                                        $("#IDSubItems").hide();
                                        $("#IDSubItemsTow").hide();
                                        $("#IDSubItemsThree").hide();
                                    }
                                    else if (ddlFruits.value == "ByOne") {
                                        $("#IDMainItems").show();
                                        $("#IDSubItems").show();
                                        $("#IDSubItemsTow").hide();
                                        $("#IDSubItemsThree").hide();
                                    }
                                    else if (ddlFruits.value == "ByTwo") {
                                        $("#IDMainItems").show();
                                        $("#IDSubItems").show();
                                        $("#IDSubItemsTow").show();
                                        $("#IDSubItemsThree").hide();
                                    }
                                    else if (ddlFruits.value == "ByThree") {
                                        $("#IDMainItems").show();
                                        $("#IDSubItems").show();
                                        $("#IDSubItemsTow").show();
                                        $("#IDSubItemsThree").show();
                                    }
                                    else {
                                        $("#IDMainItems").hide();
                                        $("#IDSubItems").hide();
                                        $("#IDSubItemsTow").hide();
                                        $("#IDSubItemsThree").hide();
                                    }
                                    return true;
                                }
                            </script>  
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="IDFilter" runat="server" ScrollBars="Auto" Height="250">
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
                            <div class="col-sm-1">
                                <div class="form-group">                          
                                    <h5><i class="fa fa-star"></i>الإرشيف</h5>
                                    <script type="text/javascript">
                                        function jsYears(ch) {
                                            var allcheckboxes = document.getElementById('<%=CBYears.ClientID %>').getElementsByTagName("input");
                                            for (i = 0; i < allcheckboxes.length; i++)
                                                allcheckboxes[i].checked = ch.checked;
                                        }
                                    </script>
                                    <div class="checkbox checkbox-primary" align="right">
                                        <asp:CheckBox ID="CBCheckAllYears" onclick="jsYears(this)" runat="server"
                                            Text="الكل" CssClass="styled" />
                                    </div>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBYears" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="250">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div id="IDMainItems" class="col-sm-2" style="display: none; <%= FCheck("_Main") %>">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> البند الرئيسي : </h5>
                                    <div runat="server" id="IDSelectAllMainItems" class="checkbox checkbox-primary" align="right">
                                        <asp:CheckBox ID="CBSelectAllMainItems" runat="server" AutoPostBack="true" 
                                            OnCheckedChanged="CBSelectAllMainItems_CheckedChanged"
                                            Text="تحديد الكل" CssClass="styled" />
                                    </div>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBMainItems" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="CBMainItems_SelectedIndexChanged"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div id="IDSubItems" class="col-sm-3" style="display: none; <%= FCheck("_One") %>">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> البند الفرعي الأول : </h5>
                                    <div runat="server" id="IDSelectAllSubItems" class="checkbox checkbox-primary" align="right">
                                        <asp:CheckBox ID="CBSelectAllSubItems" runat="server" AutoPostBack="true" 
                                            OnCheckedChanged="CBSelectAllSubItems_CheckedChanged"
                                            Text="تحديد الكل" CssClass="styled" />
                                    </div>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBSubItems" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="CBSubItems_SelectedIndexChanged"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div id="IDSubItemsTow" class="col-sm-3" style="display: none; <%= FCheck("_Two") %>">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> البند الفرعي الثاني : </h5>
                                    <div runat="server" id="IDSelectAllSubItemsTow" class="checkbox checkbox-primary" align="right">
                                        <asp:CheckBox ID="CBSelectAllSubItemsTow" runat="server" AutoPostBack="true" 
                                            OnCheckedChanged="CBSelectAllSubItemsTow_CheckedChanged"
                                            Text="تحديد الكل" CssClass="styled" />
                                    </div>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBSubItemsTow" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="CBSubItemsTow_SelectedIndexChanged"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div id="IDSubItemsThree" class="col-sm-3" style="display: none; <%= FCheck("_Three") %>">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> البند الفرعي الثالث : </h5>
                                    <script type="text/javascript">
                                        function jsThree(ch) {
                                            var allcheckboxes = document.getElementById('<%=CBSubItemsThree.ClientID %>').getElementsByTagName("input");
                                            for (i = 0; i < allcheckboxes.length; i++)
                                                allcheckboxes[i].checked = ch.checked;
                                        }
                                    </script>
                                    <div runat="server" id="IDSelectAllSubItemsThree" class="checkbox checkbox-primary" align="right">
                                        <asp:CheckBox ID="CBSelectAllSubItemsThree" onclick="jsThree(this)" runat="server"
                                            Text="تحديد الكل" CssClass="styled" />
                                    </div>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBSubItemsThree" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                            <div class="table table-responsive" runat="server" id="pnlDataPrint" dir="rtl">
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div align="center" class="w">
                                                    <div class="HideNow">
                                                        <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                                    </div>
                                                    <div class="col-lg-11">
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-1 HideThis">
                                                        <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVStatisticsByCashingByOrg" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    OnRowDataBound="GVStatisticsByCashingByOrg_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الجهة/الفرد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-user"></i> 
                                                                <%# Eval("_Name") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عدد الفواتير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-file"></i> 
                                                                <%# Eval("_Count") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-money"></i>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Size="12px" Text='<%# Eval("_Sum") %>'></asp:Label>
                                                                    <small>
                                                                        <%# XMony %>
                                                                    </small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                
                                                <asp:GridView ID="GVStatisticsByCashingByMain" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    OnRowDataBound="GVStatisticsByCashingByMain_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الجهة/الفرد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-user"></i> 
                                                                <%# Eval("_Name") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الرئيسي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("_Name_Ar_") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عدد الفواتير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-file"></i> 
                                                                <%# Eval("_Count") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-money"></i>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Size="12px" Text='<%# Eval("_Sum") %>'></asp:Label>
                                                                    <small>
                                                                        <%# XMony %>
                                                                    </small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                
                                                <asp:GridView ID="GVStatisticsByCashingByOne" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    OnRowDataBound="GVStatisticsByCashingByOne_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الجهة/الفرد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-user"></i> 
                                                                <%# Eval("_Name") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الرئيسي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("_Name_Ar_") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الفرعي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("NameOne") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عدد الفواتير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-file"></i> 
                                                                <%# Eval("_Count") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-money"></i>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Size="12px" Text='<%# Eval("_Sum") %>'></asp:Label>
                                                                    <small>
                                                                        <%# XMony %>
                                                                    </small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                
                                                <asp:GridView ID="GVStatisticsByCashingByTwo" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    OnRowDataBound="GVStatisticsByCashingByTwo_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الجهة/الفرد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-user"></i> 
                                                                <%# Eval("_Name") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الرئيسي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("_Name_Ar_") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الفرعي الأول" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("NameOne") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الفرعي الثاني" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("NameTwo") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عدد الفواتير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-file"></i> 
                                                                <%# Eval("_Count") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-money"></i>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Size="12px" Text='<%# Eval("_Sum") %>'></asp:Label>
                                                                    <small>
                                                                        <%# XMony %>
                                                                    </small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>

                                                <asp:GridView ID="GVStatisticsByCashingByThree" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    OnRowDataBound="GVStatisticsByCashingByThree_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الجهة/الفرد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-user"></i> 
                                                                <%# Eval("_Name") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الرئيسي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("_Name_Ar_") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الفرعي الأول" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("NameOne") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الفرعي الثاني" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("NameTwo") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البند الفرعي الثالث" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-map"></i> 
                                                                <%# Eval("NameThree") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عدد الفواتير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-file"></i> 
                                                                <%# Eval("_Count") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-money"></i>
                                                                    <asp:Label ID="lblTotal" runat="server" Font-Size="12px" Text='<%# Eval("_Sum") %>'></asp:Label>
                                                                    <small>
                                                                        <%# XMony %>
                                                                    </small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                                                        </td>
                                                        <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                                            <asp:TextBox ID="lblSumWord" runat="server" Text="0" class="form-control" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                                            <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                            <asp:Label ID="lblMony" runat="server" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div>
                                                    <span style="font-size: 12px; padding-right: 5px">
                                                        الحساب 
                                                        <asp:Label ID="lbl_AccountName" runat="server"></asp:Label> , 
                                                        إرشيف 
                                                        <asp:Label ID="lbl_Years" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div class="hide">
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div class="container-fluid" dir="rtl" runat="server">
                                        <uc1:WUCFooterMony runat="server" ID="WUCFooterMony" />
                                    </div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div class="HideNow">
                                        <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                    </div> 
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <hr />
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
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <hr />
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
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div id="IDModelOrg" class="modal fade in modal_New_Style">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header no-border">
                        <button type="button" class="close" data-dismiss="modal">×</button>
                    </div>
                    <div class="modal-body" id="modal_ajax_content">
                        <div class="page-container">
                            <div class="page-content">
                                <div class=" panel-body">
                                    <label>
                                        <i class="fa fa-star"></i>
                                        قائمة الجهات والأشخاص :
                                    </label>
                                    <div align="center">
                                        <div class="" dir="rtl">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                     <script type="text/javascript">
                                                         function js(ch) {
                                                             var allcheckboxes = document.getElementById('<%=CBCompany.ClientID %>').getElementsByTagName("input");
                                                             for (i = 0; i < allcheckboxes.length; i++)
                                                                 allcheckboxes[i].checked = ch.checked;
                                                         }
                                                     </script>
                                                    <div class="checkbox checkbox-primary" align="right">
                                                        <asp:CheckBox ID="CBCheckAllCompany" onclick="js(this)" runat="server" Checked="true"
                                                            Text="تحديد الكل" CssClass="styled" />
                                                    </div>
                                                    <div class="checkbox checkbox-primary">
                                                        <asp:CheckBoxList ID="CBCompany" runat="server"
                                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
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
        </script>
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>