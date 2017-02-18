using OnTheRoad.Data;
using OnTheRoad.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;

namespace OnTheRoad.Admin
{
    public partial class Users : System.Web.UI.Page
    {
        private const string ROLES = "roles";
        private const string CITIES = "cities";

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

                this.Session.Add(ROLES, rolesDict);

                var cities = context.Cities.ToList();
                var citiesDict = new Dictionary<int, string>();
                for (int i = 0; i < cities.Count; i++)
                {
                    citiesDict.Add(cities[i].Id, cities[i].Name);
                }

                this.Session.Add(CITIES, citiesDict);
            }
        }

        public string GetRoleName(string id)
        {
            var roles = (IDictionary<string, string>)this.Session[ROLES];

            return roles[id];
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
            var citiesDictionary = (Dictionary<int, string>)this.Session[CITIES];
            return citiesDictionary.Values;
        }

        public IEnumerable<string> DropDownListRole_GetData()
        {
            var rolesDictionary = (Dictionary<string, string>)this.Session[ROLES];
            return rolesDictionary.Values;
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void GridViewUsers_UpdateItem()
        {
            int index = this.GridViewUsers.EditIndex;
            GridViewRow row = this.GridViewUsers.Rows[index];

            string userId = (row.FindControl("LiteralUserId") as Literal).Text;
            string firstName = (row.FindControl("TextBoxFirstName") as TextBox).Text;
            string lastName = (row.FindControl("TextBoxLastName") as TextBox).Text;
            string phoneNumber = (row.FindControl("TextBoxPhoneNumber") as TextBox).Text;
            string city = (row.FindControl("DropDownListCityName") as DropDownList).SelectedValue;
            var listBoxRoles = (row.FindControl("ListBoxRole") as ListBox);
            var roles = listBoxRoles.GetSelectedIndices();
            var selectedRoles = new List<string>();
            foreach (int i in roles)
            {
                var value = listBoxRoles.Items[i].Value;
                selectedRoles.Add(value);
            }

            var context = new OnTheRoadIdentityDbContext();
            var user = context.Users.Find(userId);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.City.Name = city;

            //user.Roles.Clear();

            //foreach (string r in selectedRoles)
            //{
            //    user.Roles.Add(context.Roles.Where(x => x.Name == r).Single());
            //}

            var entry = context.Entry(user);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            
        }

        protected void GridViewUsers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            this.GridViewUsers.EditIndex = -1;
        }
    }
}