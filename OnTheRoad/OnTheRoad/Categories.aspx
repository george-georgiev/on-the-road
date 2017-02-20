<%@ Page MasterPageFile="~/SiteNavigation.master" Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="OnTheRoad.Categories" %>

<%@ Register TagPrefix="uc" TagName="TripItem" Src="~/CustomControllers/TripItem.ascx" %>
<%@ Register TagPrefix="uc" TagName="DataPager" Src="~/CustomControllers/DataPager.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderCSSMaster" runat="server">
    <link href="/Css/TripsContainer.css" rel="stylesheet" />
    <link href="/Css/DataPager.css" rel="stylesheet" />
</asp:Content>

<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div runat="server" id="DivCategories">
        <asp:PlaceHolder runat="server" ID="PlaceHolderCategories" />
    </div>
    <div runat="server" visible="false" id="DivCategoryTrips">
        <div class="trips-container text-center box-shadow">
            <div class="head box-shadow">
                <asp:Label Text='<%#: "Категория " +  this.CategoryName %>' runat="server" />
            </div>

            <div class="main">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="ListViewTrips" runat="server" ItemType="ITrip">
                            <ItemTemplate>
                                <uc:TripItem runat="server" Trip="<%# Item %>" />
                            </ItemTemplate>
                        </asp:ListView>
                        <uc:DataPager ID="DataPager" runat="server" PageSize="<%# PageSize %>" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
