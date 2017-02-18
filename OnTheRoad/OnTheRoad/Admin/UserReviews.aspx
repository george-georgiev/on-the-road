<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="UserReviews.aspx.cs" Inherits="OnTheRoad.Admin.UserReviews" %>

<asp:Content ID="ContentUserReviews" runat="server" ContentPlaceHolderID="ContentPlaceholderAdmin">
    <h2 class="page-headers"><%# this.Request.QueryString["name"] + " коментари" %></h2>
    <asp:GridView ID="GridViewReviews" runat="server" DataSourceID="ObjectDataSourceReviews"
        ItemType="OnTheRoad.Data.Models.Review"
        AutoGenerateColumns="False"
        OnRowUpdating="GridViewReviews_RowUpdating"
        PageSize="5"
        AllowPaging="True"
        AutoGenerateSelectButton="True"
        AutoGenerateEditButton="True" CellPadding="8" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Id">
                <EditItemTemplate>
                    <asp:Literal ID="LiteralReviewId" Text="<%# Item.Id %>" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal Text="<%# Item.Id %>" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From User">
                <EditItemTemplate>
                    <asp:Literal Text="<%# Item.FromUser.UserName %>" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal Text="<%# Item.FromUser.UserName %>" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To User">
                <EditItemTemplate>
                    <asp:Literal Text="<%# Item.ToUser.UserName %>" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal Text="<%# Item.ToUser.UserName %>" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rating">
                <EditItemTemplate>
                    <asp:Literal Text="<%# Item.Rating.Value %>" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal Text="<%# Item.Rating.Value %>" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Posting Date">
                <EditItemTemplate>
                    <asp:Literal Text="<%# Item.PostingDate%>" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal Text="<%# Item.PostingDate%>" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Review Content">
                <EditItemTemplate>
                    <asp:Literal Text="<%# Item.ReviewContent %>" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal Text="<%# Item.ReviewContent %>" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="is Deleted">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBoxIsDeleted" Checked="<%# Item.IsDeleted %>" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Literal Text="<%# Item.IsDeleted %>" runat="server" />
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

    <asp:ObjectDataSource ID="ObjectDataSourceReviews" runat="server" SelectMethod="GetUserReviews" TypeName="OnTheRoad.Admin.GetUseReviews" UpdateMethod="UpdateUserReviews">
        <SelectParameters>
            <asp:QueryStringParameter Name="userId" QueryStringField="Id" Type="String" />
        </SelectParameters>

        <UpdateParameters>
            <%--<asp:ControlParameter Name="IsDeleted" ControlID="CheckBoxChecked" Type="Boolean" />--%>
        </UpdateParameters>

    </asp:ObjectDataSource>
</asp:Content>
