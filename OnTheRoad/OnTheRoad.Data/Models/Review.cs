using OnTheRoad.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Data.Models
{
    public class Review: BaseEntity
    {

        public Rating Rating { get; set; }

        public string Comment { get; set; }
    }
}
