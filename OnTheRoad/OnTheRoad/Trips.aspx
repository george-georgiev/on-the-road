<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Trips.aspx.cs" Inherits="OnTheRoad.Trips" %>

<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<%@ Register Src="~/CustomControllers/CategoryOverview.ascx" TagName="CategoryOverview"
    TagPrefix="userControls" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-8 col-md-8">
                <asp:Repeater ID="CategoryRepeater" runat="server" ItemType="ICategory">
                    <ItemTemplate>
                        <userControls:CategoryOverview runat="server" CategoryName="<%#: Item.Name %>" />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col-lg-4 col-md-4">
            </div>
        </div>
    </div>
</asp:Content>
