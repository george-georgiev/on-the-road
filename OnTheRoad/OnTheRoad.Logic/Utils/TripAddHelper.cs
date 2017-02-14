using OnTheRoad.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using OnTheRoad.Domain.Models;

namespace OnTheRoad.Logic.Utils
{
    public class TripAddHelper : ITripAddHelper
    {
        private readonly ICategoryGetService categoryGetService;
        private readonly ITagService tagService;
        private readonly IUserService userService;

        public TripAddHelper(ITagService tagService, ICategoryGetService categoryGetService, IUserService userService)
        {
            if (tagService == null)
            {
                throw new ArgumentNullException("tagService can not be null!");
            }

            if (categoryGetService == null)
            {
                throw new ArgumentNullException("categoryGetService can not be null!");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService can not be null!");
            }

            this.tagService = tagService;
            this.categoryGetService = categoryGetService;
            this.userService = userService;
        }

        public void SetTripCategoriesById(ITrip trip, IEnumerable<int> idCollection)
        {
            var categories = this.categoryGetService.GetCategoriesByIdCollection(idCollection);
            trip.Categories = categories.ToList();
        }

        public void SetTripOrganiserByUsername(ITrip trip, string username)
        {
            var user = this.userService.GetUserInfo(username);
            trip.Organiser = user;
        }

        public void SetTripTagsByName(ITrip trip, IEnumerable<string> tagNames)
        {
            var tags = this.tagService.GetOrCreateTags(tagNames);
            trip.Tags = tags.ToList();
        }
    }
}
