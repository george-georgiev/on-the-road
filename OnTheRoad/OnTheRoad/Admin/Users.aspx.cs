using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;
using OnTheRoad.Data;
using OnTheRoad.Data.Models;
using OnTheRoad.Identity;
using Microsoft.AspNet.Identity;

namespace OnTheRoad.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        private const string ROLES = "roles";
        private const string CITIES = "cities";
        private const string CURRENT_USER_ROLES = "currentUserRoles";
        private const string CURRENT_USER_CITY = "currentUserCity";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                var context = new OnTheRoadIdentityDbContext();

                var roles = context.Roles.ToList();
                var rolesDict = new Dictionary<string, string>();
                for (int i = 0; i < roles.Count; i++)
                {
                    rolesDict.Add(roles[i].Id, roles[i].Name);
                }

                this.ViewState.Add(ROLES, rolesDict);

                var cities = context.Cities.ToList();
                var citiesDict = new Dictionary<int, string>();
                for (int i = 0; i < cities.Count; i++)
                {
                    citiesDict.Add(cities[i].Id, cities[i].Name);
                }

                this.ViewState.Add(CITIES, citiesDict);
            }
        }

        public IEnumerable<string> GetRolesAsNames(IEnumerable<string> userRoles)
        {
            var roles = (IDictionary<string, string>)this.ViewState[ROLES];
            var ids = userRoles.Intersect(roles.Keys);
            var rolesToReturn = new List<string>();
            foreach (var userRoleId in userRoles)
            {
                if (roles.Keys.Contains(userRoleId))
                {
                    rolesToReturn.Add(roles[userRoleId]);
                }
            }

            return rolesToReturn;
        }

        public IQueryable<User> GridViewUsers_GetData()
        {
            var context = new OnTheRoadIdentityDbContext();
            var result = context.Users
                .Include(x => x.City)
                .Include(x => x.Roles)
                .OrderBy(x => x.UserName);

            return result;
        }

        public IEnumerable<string> DropDownListCityId_GetData()
        {
            var citiesDictionary = (Dictionary<int, string>)this.ViewState[CITIES];
            return citiesDictionary.Values;
        }

        protected void DropDownListCityName_DataBound(object sender, EventArgs e)
        {
            var userCity = (string)this.ViewState[CURRENT_USER_CITY];
            var dropDownListCities = (DropDownList)sender;
            if (dropDownListCities.Items.FindByValue(userCity) != null)
            {
                dropDownListCities.Items.FindByValue(userCity).Selected = true;
            }
        }

        public IEnumerable<string> CheckBoxListRoles_GetData()
        {
            var rolesDictionary = (Dictionary<string, string>)this.ViewState[ROLES];
            return rolesDictionary.Values;
        }

        protected void CheckBoxListRoles_DataBound(object sender, EventArgs e)
        {
            var userRoles = (IEnumerable<string>)this.ViewState[CURRENT_USER_ROLES];

            var checkBoxList = (CheckBoxList)sender;
            for (int i = 0; i < checkBoxList.Items.Count; i++)
            {
                if (userRoles.Contains(checkBoxList.Items[i].Text))
                {
                    checkBoxList.Items[i].Selected = true;
                }
            }
        }

        public void GridViewUsers_UpdateItem()
        {
            int index = this.GridViewUsers.EditIndex;
            GridViewRow row = this.GridViewUsers.Rows[index];

            string userId = (row.FindControl("LiteralUserId") as Literal).Text;
            string firstName = (row.FindControl("TextBoxFirstName") as TextBox).Text;
            string lastName = (row.FindControl("TextBoxLastName") as TextBox).Text;
            string phoneNumber = (row.FindControl("TextBoxPhoneNumber") as TextBox).Text;
            string city = (row.FindControl("DropDownListCityName") as DropDownList).SelectedValue;

            var context = new OnTheRoadIdentityDbContext();
            var user = context.Users.Find(userId);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            if (user.City == null)
            {
                user.City = new City();
            }
            user.City.Name = city;

            var entry = context.Entry(user);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            var checkBoxListRoles = (row.FindControl("CheckBoxListRoles") as CheckBoxList);
            var selectedRoles = new List<string>();
            foreach (ListItem role in checkBoxListRoles.Items)
            {
                if (role.Selected)
                {
                    selectedRoles.Add(role.Text);
                }
            }

            var owinContex = this.Context.GetOwinContext();
            var appUserManager = owinContex.GetUserManager<ApplicationUserManager>();
            appUserManager.RemoveFromRoles(userId, appUserManager.GetRoles(userId).ToArray());
            appUserManager.AddToRoles(userId, selectedRoles.ToArray());
        }

        protected void GridViewUsers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            this.GridViewUsers.EditIndex = -1;
        }

        protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = this.GridViewUsers.Rows[index];
            var bulletedListRoles = row.FindControl("BulletedListRoles") as BulletedList;
            if (bulletedListRoles != null)
            {
                var userRoles = bulletedListRoles.Items;
                var rolesAsList = new List<string>();
                foreach (ListItem role in userRoles)
                {
                    rolesAsList.Add(role.Text);
                }

                this.ViewState.Add(CURRENT_USER_ROLES, rolesAsList);
            }

            var literalCityName = row.FindControl("LiteralCityName") as Literal;
            if (literalCityName != null)
            {
                this.ViewState.Add(CURRENT_USER_CITY, literalCityName.Text);
            }
        }
    }
}