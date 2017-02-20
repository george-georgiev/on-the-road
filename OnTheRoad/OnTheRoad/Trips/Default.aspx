<%@ Page Language="C#" MasterPageFile="~/SiteNavigation.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OnTheRoad.Trips.Default" %>

<%@ Register TagPrefix="uc" TagName="TripItem" Src="~/CustomControllers/TripItem.ascx" %>
<%@ Register TagPrefix="uc" TagName="DataPager" Src="~/CustomControllers/DataPager.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderCSSMaster" runat="server">
    <link href="/Css/TripsContainer.css" rel="stylesheet" />
    <link href="/Css/DataPager.css" rel="stylesheet" />
    <link href="/Css/Trips.css" rel="stylesheet" />
</asp:Content>

<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder runat="server" ID="PlaceHolderTrip" Visible="false">
        <div class="trip-container box-shadow">
            <div class="trip-head text-center">
                <div class="categories">
                    <asp:ListView runat="server" DataSource="<%# this.Model.Trip?.Categories %>" ItemType="ICategory">
                        <ItemTemplate>
                            <asp:HyperLink CssClass="info" NavigateUrl='<%#: "~/categories/" + Item.Name %>' runat="server">
                                        <asp:label CssClass="hyperlink" text=<%#: Item.Name %> runat="server" />
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <h1><%#: this.Model.Trip?.Name %></h1>
                <asp:Image ID="ImageTrip" runat="server"
                    ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String(this.Model.Trip != null ? this.Model.Trip?.CoverImage : new byte[0]) %>'
                    CssClass="img-responsive image-trip box-shadow"></asp:Image>
                <div class="meta-image-organiser">
                    <asp:Image ID="ImageOrganiser" runat="server"
                        ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String(this.Model.Trip != null ? this.Model.Trip?.Organiser.Image : new byte[0]) %>'
                        CssClass="img-responsive image-organiser"></asp:Image>
                </div>
                <div>
                    <asp:HyperLink CssClass="organiser-name" NavigateUrl='<%# "~/profile/profileInfo.aspx?name=" + this.Model.Trip?.Organiser.Username %>'
                        Text='<%#: this.Model.Trip?.Organiser.FirstName + " " +  this.Model.Trip?.Organiser.LastName %>'
                        runat="server" />
                </div>
            </div>
            <div class="row trip-details">
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <div class="trip-info">
                        <i class="fa fa-map-marker font-awesome" aria-hidden="true"></i>
                        <%#: this.Model.Trip?.Location %>
                    </div>
                    <hr />
                    <div class="trip-info ">
                        <i class="fa fa-clock-o font-awesome" aria-hidden="true"></i>
                        <asp:Label Text='<%#: "Начало: " + this.Model.Trip?.StartDate.ToString("dd.MM.yyyy")%>' runat="server" />
                        <asp:Label CssClass="end-date" Text='<%#:  "Край: " + this.Model.Trip?.EndDate.ToString("dd.MM.yyyy") %>' runat="server" />
                    </div>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:DropDownList ID="DropDownListAttendance" CssClass="attend-dropdown"
                        runat="server" OnSelectedIndexChanged="DropDownListAttendance_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Не мога да ида" Value="None" />
                        <asp:ListItem Text="Ще присъствам" Value="Attending" />
                        <asp:ListItem Text="Интересувам се" Value="Interested" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="trip-description">
                <%#: this.Model.Trip?.Description %>
            </div>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="PlaceHolderTripsResult" Visible="false">
        <div class="trips-container text-center box-shadow">
            <div class="head box-shadow">
                <asp:Label Text='<%#: this.TripsResultsTitle  %>' runat="server" />
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
    </asp:PlaceHolder>
</asp:Content>
