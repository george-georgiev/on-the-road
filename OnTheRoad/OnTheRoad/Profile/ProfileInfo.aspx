<%@ Page Language="C#" MasterPageFile="~/Profile/UserProfile.master" AutoEventWireup="true" CodeBehind="ProfileInfo.aspx.cs" Inherits="OnTheRoad.Profile.ProfileInfo" %>

<asp:Content ContentPlaceHolderID="ProfileContent" ID="ProfileInfo" runat="server">


    <div class="row text-center">
        <div class="col-md-12">
            <asp:FormView ID="FormViewProfileInfo" runat="server"
                ItemType="OnTheRoad.Models.ProfileInfoModel">
                <ItemTemplate>
                    <h2 id="username">
                        <asp:Literal Text="<%# this.Model.Username %>" runat="server" />
                    </h2>
                    <br />
                    <div class="form-group">
                        <i class="fa fa-user-circle font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.FirstName + " " + Item.LastName %>' runat="server" />
                    </div>

                    <div class="form-group">
                        <i class="fa fa-envelope-o font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.Email %>' runat="server" />
                    </div>
                    <div class="form-group">
                        <i class="fa fa-map-marker font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.City + " " + Item.Country%>' runat="server" />
                    </div>
                    <div class="form-group">
                        <i class="fa fa-mobile font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%#Item.PhoneNumber%>' runat="server" />
                    </div>
                    <div class="form-group">
                        <i class="fa fa-id-card-o font-awesome" aria-hidden="true"></i>
                        <asp:Literal Text='<%# Item.Info %>' runat="server" />
                    </div>
                    <asp:LinkButton ID="EditButton" runat="server" OnClick="EditButton_Click" Text="ПРОМЕНИ" CssClass="btn btn-warning"></asp:LinkButton>
                </ItemTemplate>

                <EditItemTemplate>
                    <div class="form-group">
                        <label>потребителско име</label>
                        <asp:TextBox Text="<%# this.Model.Username %>" runat="server" CssClass="form-control" />
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group ">
                                <label>първо име</label>
                                <asp:TextBox ID="FirstName" Text='<%# Item.FirstName %>' runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>град</label>
                                <asp:TextBox ID="City" Text='<%# Item.City %>' runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>имейл</label>
                                <asp:TextBox ID="Email" Text='<%# Item.Email %>' runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                 <label>фамилно име</label>
                                <asp:TextBox ID="LastName" Text='<%# Item.FirstName %>' runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>държава</label>
                                <asp:TextBox ID="Country" Text='<%# Item.City %>' runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>тел. номер</label>
                                <asp:TextBox ID="PhoneNumber" Text='<%#Item.PhoneNumber%>' runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>кратка информация</label>
                        <asp:TextBox TextMode="MultiLine" Rows="5" Text='<%# Item.Info %>' runat="server" CssClass="form-control" />
                    </div>
                    <asp:LinkButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="ЗАПАЗИ" CssClass="btn btn-success"></asp:LinkButton>
                </EditItemTemplate>
            </asp:FormView>
        </div>
    </div>
</asp:Content>
