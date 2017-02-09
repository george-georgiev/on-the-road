<%@ Page Language="C#" MasterPageFile="~/Profile/UserProfile.master" AutoEventWireup="true" CodeBehind="ProfileInfo.aspx.cs" Inherits="OnTheRoad.Profile.ProfileInfo" %>

<%@ Register TagPrefix="uc" TagName="CitiesDropDown" Src="~/CustomControllers/CitiesDropDown.ascx" %>

<asp:Content ContentPlaceHolderID="ProfileContent" ID="ProfileInfo" runat="server">

    <div class="row text-center">
        <div class="col-md-12">

            <asp:UpdatePanel ID="UpdatePanelResults" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="PanelError" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" Text="asddasdasdasdsad" ID="FailureText" />
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:FormView ID="FormViewProfileInfo" runat="server"
                ItemType="OnTheRoad.Mvp.Models.ProfileInfoModel">
                <ItemTemplate>
                    <h2 class="page-headers">
                        <asp:Literal Text='<%# this.Model.Username %>' runat="server" />
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
                    <asp:LinkButton ID="EditButton" runat="server" OnClick="EditButton_Click" Text="ПРОМЕНИ" CssClass="btn btn-warning"></asp:LinkButton>

                </ItemTemplate>
                <EditItemTemplate>
                    <h2 class="page-headers">Промяна на профил</h2>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Image ID="ImageUser" runat="server" ImageUrl="http://klassa.bg/images/pictures/class_bg/img_47303.jpg" CssClass="img-responsive"></asp:Image>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group ">
                                <label>първо име</label>
                                <asp:TextBox ID="FirstName" Text='<%# Item.FirstName%>' runat="server" CssClass="form-control" />
                            </div>

                            <asp:UpdatePanel runat="server" UpdateMode="Always" ID="upDetails">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label>потребителско име</label>
                                        <asp:TextBox ID="Username" OnTextChanged="Username_TextChanged" AutoPostBack="true" Text="<%# this.GetUsername %>" runat="server" CssClass="form-control" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="form-group">
                                <label>град</label>
                                <uc:CitiesDropDown ID="City" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>фамилно име</label>
                                <asp:TextBox ID="LastName" Text='<%# Item.LastName %>' runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <label>тел. номер</label>
                                <asp:TextBox ID="PhoneNumber" Text='<%#Item.PhoneNumber%>' runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>кратка информация</label>
                        <asp:TextBox ID="Info" TextMode="MultiLine" Rows="10" Text='<%# Item.Info %>' runat="server" CssClass="form-control" />
                    </div>
                    <asp:LinkButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="ЗАПАЗИ" CssClass="btn btn-success"></asp:LinkButton>
                </EditItemTemplate>
            </asp:FormView>

        </div>
    </div>
</asp:Content>
