<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Trips.aspx.cs" Inherits="OnTheRoad.Admin.Trips" %>

<asp:Content ID="ContentTrips" runat="server" ContentPlaceHolderID="ContentPlaceholderAdmin">
    <h2 class="page-headers">Всички пътешествия</h2>
    <asp:GridView ID="GridViewTrips" runat="server" DataSourceID="SqlDataSourceTrips" AutoGenerateColumns="False"
        AllowSorting="true"
        AllowPaging="true"
        PageSize="10"
        AutoGenerateSelectButton="True"
        CellPadding="4" DataKeyNames="Id" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:HyperLinkField DataTextField="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="/trips/{0}" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
            <asp:HyperLinkField DataTextField="UserName" DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="/profile/profileinfo?name={0}" HeaderText="UserName" SortExpression="UserName" />
        </Columns>
        <EditRowStyle BackColor="Skyblue" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceTrips" runat="server" ConnectionString="<%$ ConnectionStrings:OnTheRoadDB %>"
        SelectCommand="SELECT t.Id, t.Name, t.StartDate, t.EndDate, t.CreateDate, t.Description, t.Location, u.UserName FROM Trips AS t INNER JOIN Users AS u ON t.OrganiserId = u.Id"></asp:SqlDataSource>
</asp:Content>
