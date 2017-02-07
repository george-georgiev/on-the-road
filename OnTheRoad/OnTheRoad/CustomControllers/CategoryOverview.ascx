﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryOverview.ascx.cs" Inherits="OnTheRoad.CustomControllers.CategoryOverview" %>
<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<div>
    <asp:Repeater ID="TripRepeater" runat="server" ItemType="ITrip">
        <ItemTemplate><%#: Item.Name %></ItemTemplate>
    </asp:Repeater>
</div>