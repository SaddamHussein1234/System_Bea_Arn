<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCFooter.ascx.cs" Inherits="WUCFooter" %>
<div class="container-fluid" dir="rtl" runat="server">
    <table style="width: 100%">
        <tr>
            <td>
                <div class="WidthMaglis24" align="center" runat="server" visible="false">
                    الباحث الإجتماعي
                    <br />
                    <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                    <br />
                    <asp:Label ID="lblAlBaheth" runat="server" Font-Size="11px"></asp:Label>
                    <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true"
                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="WidthMaglis24" align="center">
                    مدير الجمعية
                    <br />
                    <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                    <br />
                    <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="11px"></asp:Label>
                    <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah_SelectedIndexChanged"
                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="WidthMaglis24" align="center">
                    رئيس لجنة البحث 
                    <br />
                    <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                    <br />
                    <asp:Label ID="lblRaeesLagnatAlBahath" runat="server" Font-Size="11px"></asp:Label>
                    <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesLagnatAlBahath_SelectedIndexChanged"
                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="WidthMaglis24" align="center">
                    رئيس مجلس الإدارة
                    <br />
                    <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                    <br />
                    <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="11px"></asp:Label>
                    <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah_SelectedIndexChanged"
                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="WidthMaglis24" align="center">
                    <div runat="server" id="IDKhatm" align="left" style="margin-top: 0px">
                        <img src="/ImgSystem/ImgSignature/الختم.png" width="120" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>
