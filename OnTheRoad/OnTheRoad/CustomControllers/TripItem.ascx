<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TripItem.ascx.cs" Inherits="OnTheRoad.CustomControllers.TripItem" %>

<div class="trip-item row trips-wrapper box-shadow">
    <div class="col col-md-6 col-sm-6 col-xs-12">
        <asp:HyperLink NavigateUrl='<%# "/trips/" + this.Trip.Id %>' runat="server">
            <div class="meta-image text-center">
                <asp:Image ID="ImageTrip" runat="server"
                    ImageUrl='<%# "data:image/jpeg;base64," + Convert.ToBase64String(this.Trip.CoverImage) %>'
                    CssClass="img-responsive resizable"></asp:Image>
            </div>
        </asp:HyperLink>
    </div>
    <div class="col-md-6 col-sm-6 col-xs-12">
        <h4>
            <asp:HyperLink NavigateUrl='<%# "/trips/" + this.Trip.Id %>' Text="<%#: this.Trip.Name %>" runat="server" />
        </h4>

        <div class="info">
            Място:
                <asp:Label Text="<%#: this.Trip.Location %>" runat="server" />
        </div>
        <div class="info">
            Начало:
                <asp:Label Text='<%#: this.Trip.StartDate.ToString("dd.MM.yyyy") %>' runat="server" />
        </div>
        <asp:HyperLink NavigateUrl='<%# "~/profile/profileInfo.aspx?name=" + this.Trip.Organiser.Username %>' runat="server">
        <div class="info">
            Потребител:
            <asp:label CssClass="hyperlink" text='<%#: this.Trip.Organiser.FirstName + " " +  this.Trip.Organiser.LastName %>' runat="server" />
        </div>
        </asp:HyperLink>
    </div>
</div>
