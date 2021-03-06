﻿using OnTheRoad.Domain.Models;
using System.Collections.Generic;

namespace OnTheRoad.MVC.Models
{
    public class HomeViewModel
    {
        public IEnumerable<TripViewModel> Trips { get; set; }

        public int AllTripsCount { get; set; }

        public int AllUsersCount { get; set; }
    }
}