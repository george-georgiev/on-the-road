using OnTheRoad.Domain.Models;
using OnTheRoad.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OnTheRoad.MVC.Models
{
    public class AddTripViewModel : IMapTo<ITrip>
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Trips.AddTripMessages), ErrorMessageResourceName = "TitleRequired")]
        public string Name { get; set; }

        public byte[] CoverImage { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Trips.AddTripMessages), ErrorMessageResourceName = "LocationRequierd")]
        public string Location { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Trips.AddTripMessages), ErrorMessageResourceName = "StartDateRequired")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Trips.AddTripMessages), ErrorMessageResourceName = "EndDateRequired")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Trips.AddTripMessages), ErrorMessageResourceName = "DescriptionRequired")]
        [UIHint("Description")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Trips.AddTripMessages), ErrorMessageResourceName = "CategoriesRequired")]
        public IEnumerable<string> CategoryIds { get; set; }

        public IEnumerable<SelectListItem> AllCategories { get; set; }

        public IEnumerable<string> TagNames { get; set; }
    }
}