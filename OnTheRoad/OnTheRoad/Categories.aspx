<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="OnTheRoad.Categories" %>

<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<%@ Register Src="~/CustomControllers/CategoryOverview.ascx" TagName="CategoryOverview"
    TagPrefix="uc" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-8 col-md-8">
            <%--<asp:ListView runat="server" ID="ListViewCategories" >
                <GroupTemplate>

                </GroupTemplate>
                <ItemTemplate></ItemTemplate>
            </asp:ListView>--%>
            <asp:Repeater ID="CategoryRepeater" runat="server" ItemType="ICategory">
                <ItemTemplate>
                    <asp:Label Text="<%#: Item.Name %>" runat="server" />
                    <uc:CategoryOverview runat="server" CategoryName="<%#: Item?.Name %>" />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
