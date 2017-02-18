<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPager.ascx.cs" Inherits="OnTheRoad.CustomControllers.DataPager" %>

<div class="pager text-center">
    <asp:LinkButton ID="ButtonPrev" CssClass="arrow arrow-left" runat="server" OnClick="ButtonPrev_Click" />
    <asp:LinkButton ID="ButtonNext" CssClass="arrow arrow-right" runat="server" OnClick="ButtonNext_Click" />
</div>
