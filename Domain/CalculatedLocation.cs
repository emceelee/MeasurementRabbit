using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CalculatedLocation : Location
    {
        public CalculatedLocation(string _id, bool addMeasurementPoint = false) : base(_id, addMeasurementPoint) { }

        public List<Location> LocationAssignments { get; set; } = new List<Location>();
    }
}
