using System;
using System.Collections.Generic;

namespace Domain
{
    public class Location
    {
        private string _id;

        public Location(string id, bool addMeasurementPoint = false)
        {
            _id = id;
            if(addMeasurementPoint)
            {
                this.AddMeasurementPoint(id);
            }
        }

        public string Id { get { return _id; } }
        List<MeasurementPoint> MeasurementPoints { get; set; }

        public MeasurementPoint AddMeasurementPoint(string _id)
        {
            MeasurementPoint mp = new MeasurementPoint(_id);
            MeasurementPoints.Add(mp);

            return mp;
        }
    }
}
