using System;

namespace Messages
{
    public class ProductQuantityUpdateMessage
    {
        public ProductQuantityUpdateMessage() { }

        public string MeasurementPointId { get; set; }

        public DateTime ContractDayStart { get; set; }
        public DateTime ContractDayEnd { get; set; }
    }
}
