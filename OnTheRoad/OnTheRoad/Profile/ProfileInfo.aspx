<%@ Page Language="C#" MasterPageFile="~/Profile/UserProfile.master" AutoEventWireup="true" CodeBehind="ProfileInfo.aspx.cs" Inherits="OnTheRoad.Profile.ProfileInfo" %>

<%@ Register TagPrefix="uc" TagName="CitiesDropDown" Src="~/CustomControllers/CitiesDropDown.ascx" %>

<asp:Content ContentPlaceHolderID="ProfileContent" ID="ProfileInfo" runat="server">

    <div class="row text-center">
        <div class="col-md-12">
            <asp:LinkButton ID="ButtonFollow" runat="server"
                OnClick="ButtonFollow_Click"
                Visible="false"
                Text="ПОСЛЕДВАЙ"
                CssClass="btn btn-success">
            </asp:LinkButton>

            <asp:FormView ID="FormViewProfileInfo" runat="server"
                ItemType="OnTheRoad.Mvp.Models.ProfileInfoModel">
                <ItemTemplate>
                    <h2 class="page-headers">
                        <asp:Literal Text='<%# Item.Username %>' runat="server" />
                    </h2>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Image ID="ImageUser" runat="server" ImageUrl="http://klassa.bg/images/pictures/class_bg/img_47303.jpg" CssClass="img-responsive"></asp:Image>
                        </div>
                        <div class="col-md-8">
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
                                <asp:Literal Text='<%# Item.City %>' runat="server" />
                            </div>
                            <div class="form-group">
                                <i class="fa fa-mobile font-awesome" aria-hidden="true"></i>
                                <asp:Literal Text='<%# Item.PhoneNumber %>' runat="server" />
                            </div>
                            <div class="form-group">
                                <i class="fa fa-id-card-o font-awesome" aria-hidden="true"></i>
                                <asp:Literal Text='<%# Item.Info %>' runat="server" />
                            </div>
                        </div>
                    </div>

                </ItemTemplate>
                <EditItemTemplate>
                    <h2 class="page-headers">Промяна на профил</h2>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Image ID="ImageUser" runat="server" ImageUrl="http://klassa.bg/images/pictures/class_bg/img_47303.jpg" CssClass="img-responsive"></asp:Image>
                        </div>
                        <div class="col-md-8">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group ">
                                        <label class="input-labels">първо име</label>
                                        <asp:TextBox ID="FirstName" Text='<%# Item.FirstName%>' runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="input-labels">град</label>
                                        <uc:CitiesDropDown ID="City" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="input-labels">фамилно име</label>
                                        <asp:TextBox ID="LastName" Text='<%# Item.LastName %>' runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label class="input-labels">тел. номер</label>
                                        <asp:TextBox ID="PhoneNumber" Text='<%#Item.PhoneNumber%>' runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="input-labels">кратка информация</label>
                                <asp:TextBox ID="Info" TextMode="MultiLine" Rows="4" Text='<%# Item.Info %>' runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>

                    <asp:LinkButton ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="ЗАПАЗИ" CssClass="btn btn-success"></asp:LinkButton>
                </EditItemTemplate>
            </asp:FormView>
        </div>
    </div>

    <asp:LinkButton ID="ButtonEdit" runat="server" OnClick="ButtonEdit_Click" Text="ПРОМЕНИ" CssClass="btn btn-default"></asp:LinkButton>

    <div class="row">
        <asp:Panel runat="server" ID="PanelFavouriteUsers" CssClass="col-md-12 fav-users">
            <h3 class="page-headers fav-users-header">Любими пътешественици</h3>
            <asp:Repeater runat="server" ID="RepeaterFavouriteUsers"
                ItemType="OnTheRoad.Domain.Models.IUser">
                <ItemTemplate>
                    <asp:HyperLink runat="server"
                        Text="<%#: Item.Username %>"
                        NavigateUrl='<%# "~/Profile/ProfileInfo.aspx?name=" + Item.Username %>' />
                    <asp:Image runat="server" CssClass="favUserImage img-circle"
                        ImageUrl="https://truejuggalofamily.com/wp-content/uploads/2016/05/tiwh1.jpg" />

                    <div class="btn-group">
                        <button type="button" class="btn-dropdown btn btn-xs btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:Button
                                    runat="server"
                                    Text="премахни"
                                    ID="ButtonUnfollow"
                                    CssClass="btn-unfollow"
                                    CommandArgument="<%#: Item.Username %>"
                                    OnClick="ButtonUnfollow_Click" />
                            </li>
                        </ul>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
</asp:Content>