﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TripsList.ascx.cs" Inherits="OnTheRoad.CustomControllers.TripsList" %>
<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<div class="trips-container text-center">
    <div class="head">
        <asp:Label Text='<%#: this.Label %>' runat="server" />
    </div>

    <div class="main">
        <asp:ListView ID="ListViewTrips" runat="server" ItemType="ITrip">
            <ItemTemplate>
                <div class="trip-item row trips-wrapper">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:HyperLink NavigateUrl="#" runat="server">
                            <div class="meta-image text-center">
                                <asp:Image ID="ImageTrip" runat="server"
                                    ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String(Item.CoverImage) %>'
                                    CssClass="img-responsive resizable"></asp:Image>
                            </div>
                        </asp:HyperLink>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <h4>
                            <asp:HyperLink NavigateUrl="#" Text="<%#: Item.Name %>" runat="server" />
                        </h4>

                        <div class="info">
                            Място:
                            <asp:Label Text="<%#: Item.Location %>" runat="server" />
                        </div>
                        <div class="info">
                            Начало:
                            <asp:Label Text='<%#: Item.StartDate.ToString("dd.MM.yyyy") %>' runat="server" />
                        </div>
                        <div class="info">
                            Потребител:
                            <asp:Label Text="<%#: Item.Organiser %>" runat="server" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>