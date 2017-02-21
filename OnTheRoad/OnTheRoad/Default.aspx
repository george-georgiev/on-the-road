<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/SiteNavigation.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OnTheRoad._Default" %>

<%@ Register TagPrefix="uc" TagName="TripItem" Src="~/CustomControllers/TripItem.ascx" %>

<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<asp:Content ID="ContentCss" ContentPlaceHolderID="ContentPlaceHolderCSSMaster" runat="server">
    <link href="/Css/Home.css" rel="stylesheet" />
    <link href="/Css/TripsContainer.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="ContentHead" ContentPlaceHolderID="ContentPlaceHolderHeading" runat="server">
    <div class="img-wrapper container">
        <div class="row">
            <div class="col-md-12 col-img">
                <asp:Image CssClass="main-img img-responsive" ImageUrl="~/Content/Images/main-image.jpg" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 main-container trips-container text-center box-shadow">
            <div class="head box-shadow">
                <h3>Последно Добавени Пътешествия</h3>
            </div>

            <div class="main">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:ListView ID="ListViewTrips" runat="server" ItemType="ITrip">
                            <ItemTemplate>
                                <uc:TripItem runat="server" Trip="<%# Item %>" />
                            </ItemTemplate>
                        </asp:ListView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="row  stats-item">
                <div class="col-md-6">
                    <label>Качени пътешествия: </label>
                    <span class="statistics">
                        <asp:Literal Text='<%# this.Model.AllTripsCount %>' runat="server" /></span>
                </div>
                <div class="col-md-6">
                    <label>Регистрирани протребители: </label>
                    <span class="statistics">
                        <asp:Literal Text='<%# this.Model.AllUsersCount %>' runat="server" /></span>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
