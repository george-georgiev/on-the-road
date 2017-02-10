<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTrip.aspx.cs" Inherits="OnTheRoad.Trips.AddTrip" %>

<%@ Register TagPrefix="uc" TagName="CategoriesMultiSelect" Src="~/CustomControllers/CategoriesMultiSelect.ascx" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4 class="page-headers">Създай пътешествие</h4>
        <hr />

        <div class="form-group row">
            <asp:Label runat="server" AssociatedControlID="TripTitle" CssClass="col-md-2 control-label input-labels">Заглавие</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="TripTitle" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TripTitle"
                    CssClass="text-danger" ErrorMessage="Моля въведете заглавие." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Description" CssClass="col-md-2 control-label input-labels">Описание</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Description" CssClass="form-control" TextMode="MultiLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Description"
                    CssClass="text-danger" ErrorMessage="Моля въведете описание." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Location" CssClass="col-md-2 control-label input-labels">Локация</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Location" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Location"
                    CssClass="text-danger" ErrorMessage="Моля въведете локация." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="StartDate" CssClass="col-md-2 control-label input-labels">Начална Дата</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="StartDate" TextMode="Date" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDate"
                    CssClass="text-danger" ErrorMessage="Моля въведете начална дата." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="EndDate" CssClass="col-md-2 control-label">Крайна Дата</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="EndDate" TextMode="Date" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="EndDate"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Моля въведете крайна дата." />
                <asp:CompareValidator runat="server" ControlToCompare="StartDate" ControlToValidate="EndDate"
                    Type="Date" Operator="GreaterThanEqual"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Крайната дата не може да бъде преди началната" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Categories" CssClass="col-md-2 control-label">Категории</asp:Label>
            <div class="col-md-10">
                <uc:CategoriesMultiSelect ID="Categories" runat="server"  CssClass="form-control" 
                    IsRequired="true" 
                    ErrorMessage="Моля изберете поне една категория." />
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="CreateTripButton" OnClick="CreateTripButton_Click" Text="Създай" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
