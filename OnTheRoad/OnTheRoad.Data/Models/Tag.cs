using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Data.Models
{
    public class Tag : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
