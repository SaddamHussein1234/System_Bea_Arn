<%@ Page Title="" Language="C#" MasterPageFile="~/ITSupport/HRM/MPITSupportHRM.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ITSupport_HRM_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(function () {
            $("#prod_nav ul").tabs("#panes > div", {
                effect: 'fade',
                fadeOutSpeed: 400
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $(".pane-list li").click(function () {
                window.location = $(this).find("a").attr("href");
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <!-- tab panes -->
        <div id="prod_wrapper">
            
            <div align="right">
                <h4> : (نظام الموارد البشرية) قائمة الدعم الفني * </h4>
            </div>
            <!-- navigator -->
            <div id="prod_nav" align="right">
                <ul>
                    <asp:Repeater ID="RPTITSupport" runat="server">
                        
                        <ItemTemplate>
                            <li style="float:right"><a href='PageDetailsVideo.aspx?ID=<%# Eval("IDUniq") %>' class="AClick">
                            <img src="../<%# Eval("ImgLesson_") %>" class="WithImg" alt=""><span style="font-size:16px;"><%# Eval("TitleLesson_") %></span> </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <!-- END navigator -->
        </div>
        <!-- END prod wrapper -->
    </div>
    <!-- END container -->
</asp:Content>

