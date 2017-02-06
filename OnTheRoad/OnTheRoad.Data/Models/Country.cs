﻿using System.ComponentModel.DataAnnotations;

namespace OnTheRoad.Data.Models
{
    public class Country : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(60)]
        public string Name { get; set; }
    }
}
