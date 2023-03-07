<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCLoding.ascx.cs" Inherits="Cpanel_CAttach_WUCLoding" %>
<div class="loading2" align="center" id="lodi">
    <div>
        <img src="<%=ResolveUrl("~/Img/LogoNew.png")%>" class="Colorloading" width="200" style="padding: 5px; border-radius: 4px" />
        <br />
        <span class="Colorloading" style="padding: 5px; border-radius: 4px">يرجى الإنتظار , جاري تنفيذ المهام
            <asp:LinkButton ID="LBRefresh" runat="server" OnClick="LBRefresh_Click">
                <i class="fa fa-refresh"></i>
            </asp:LinkButton>
        </span><br />
        <br />
        <img src="<%=ResolveUrl("~/Cpanel/loader.gif")%>" alt="" />
    </div>
</div>