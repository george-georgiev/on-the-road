using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRoad.Domain.Models
{
    public interface IEvent
    {
        string Name { get; set; }

        string Destination { get; set; }
    }
}
