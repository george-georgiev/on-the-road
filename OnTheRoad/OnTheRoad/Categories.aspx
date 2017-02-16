<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="OnTheRoad.Categories" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolderCSSMaster" runat="server">
    <link href="/Css/Categories.css" rel="stylesheet" />
</asp:Content>

<%@ Import Namespace="OnTheRoad.Domain.Models" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-8 col-md-8">
            <asp:PlaceHolder runat="server" ID="PlaceHolderCategories"/>
        </div>
    </div>
</asp:Content>
