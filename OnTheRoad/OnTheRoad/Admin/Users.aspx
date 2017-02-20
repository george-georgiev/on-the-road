<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="OnTheRoad.Admin.Users" %>

<%@ Register TagPrefix="uc" TagName="CitiesDropDown" Src="~/CustomControllers/CitiesDropDown.ascx" %>

<asp:Content ContentPlaceHolderID="ContentPlaceholderAdmin" ID="ContentUsers" runat="server">
    <asp:GridView runat="server" ID="GridViewUsers"
        SelectMethod="GridViewUsers_GetData"
        UpdateMethod="GridViewUsers_UpdateItem"
        OnRowUpdated="GridViewUsers_RowUpdated"
        OnRowCommand="GridViewUsers_RowCommand"
        ItemType="OnTheRoad.Data.Models.User"
        DataKeyNames="Id"
        PageSize="10"
        AllowPaging="true"
        AllowSorting="True"
        AutoGenerateSelectButton="True"
        AutoGenerateEditButton="True"
        AutoGenerateColumns="False" CellPadding="8" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="User Id" SortExpression="Id">
                <EditItemTemplate>
                    <asp:Literal ID="LiteralUserId" runat="server" Text='<%#Item.Id %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal ID="LiteralUserId" runat="server" Text='<%# Item.Id %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Username" SortExpression="UserName">
                <EditItemTemplate>
                    <asp:Literal runat="server" Text='<%# Item.UserName %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink NavigateUrl='<%#: "/profile/profileinfo?name=" + Item.UserName %>' Text="<%#: Item.UserName %>" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxFirstName" CssClass="form-control" runat="server" Text='<%#: Item.FirstName %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal runat="server" Text='<%# Item.FirstName %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name" SortExpression="LastName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxLastName" CssClass="form-control" runat="server" Text='<%#: Item.LastName %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal runat="server" Text='<%# Item.LastName %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" SortExpression="Email">
                <EditItemTemplate>
                    <asp:Literal runat="server" Text='<%#Item.Email %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal runat="server" Text='<%# Item.Email %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone Number" SortExpression="PhoneNumber">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxPhoneNumber" CssClass="form-control" runat="server" Text='<%#: Item.PhoneNumber %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal runat="server" Text='<%# Item.PhoneNumber %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City" SortExpression="City.Name">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListCityName" CssClass="form-control" runat="server" OnDataBound="DropDownListCityName_DataBound" SelectMethod="DropDownListCityId_GetData">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal ID="LiteralCityName" runat="server" Text='<%# Item.City?.Name  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Role">
                <EditItemTemplate>
                    <asp:CheckBoxList runat="server" ID="CheckBoxListRoles" OnDataBound="CheckBoxListRoles_DataBound" SelectMethod="CheckBoxListRoles_GetData">
                    </asp:CheckBoxList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:BulletedList runat="server" ID="BulletedListRoles" DataSource="<%# GetRolesAsNames(Item.Roles.Select(x => x.RoleId)) %>">
                    </asp:BulletedList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Given Reviews">
                <ItemTemplate>
                    <asp:LinkButton Text="go to reviews" PostBackUrl='<%# "/admin/userreviews?name=" + Item.UserName + "&id=" + Item.Id %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <EditRowStyle BackColor="Skyblue" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>

</asp:Content>
