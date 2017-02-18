<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TripItem.ascx.cs" Inherits="OnTheRoad.CustomControllers.TripItem" %>

<div class="trip-item row trips-wrapper">
    <div class="col-md-6 col-sm-6 col-xs-12">
        <asp:HyperLink NavigateUrl="#" runat="server">
            <div class="meta-image text-center">
                <asp:Image ID="ImageTrip" runat="server"
                    ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String(this.Trip.CoverImage) %>'
                    CssClass="img-responsive resizable"></asp:Image>
            </div>
        </asp:HyperLink>
    </div>
    <div class="col-md-6 col-sm-6 col-xs-12">
        <h4>
            <asp:HyperLink NavigateUrl="#" Text="<%#: this.Trip.Name %>" runat="server" />
        </h4>

        <div class="info">
            Място:
                <asp:Label Text="<%#: this.Trip.Location %>" runat="server" />
        </div>
        <div class="info">
            Начало:
                <asp:Label Text='<%#: this.Trip.StartDate.ToString("dd.MM.yyyy") %>' runat="server" />
        </div>
        <div class="info">
            Потребител:
                <asp:Label Text="<%#: this.Trip.Organiser %>" runat="server" />
        </div>
    </div>
</div>
