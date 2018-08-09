using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MeasurementPoint
    {
        private string _id;

        internal MeasurementPoint(string id)
        {
            _id = id;
        }

        public string Id { get { return _id; } }
    }
}
