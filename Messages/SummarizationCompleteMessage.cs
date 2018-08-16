using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class SummarizationCompleteMessage
    {
        public string EntityId { get; set; }
        public EntityType EntityType { get; set; }
        public IntervalType IntervalType { get; set; }

        public DateTime ContractDayStart { get; set; }
        public DateTime ContractDayEnd { get; set; }

        public override string ToString()
        {
            return $@"EntityId: {EntityId}, 
        EntityType: {EntityType}, 
        IntervalType: {IntervalType}, 
        ContractDayStart: {ContractDayStart.ToString("MM/dd/yyyy")}, 
        ContractDayEnd: {ContractDayEnd.ToString("MM/dd/yyyy")}";
        }
    }

    public enum EntityType
    {
        MeasurementPoint,
        Location
    }

    public enum IntervalType
    {
        Hourly,
        Daily,
        Monthly
    }
}
