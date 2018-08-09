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
        public DateTime ContractDayStart { get; set; }
        public DateTime ContractDayEnd { get; set; }
        public TransactionalRecordVersion TransactionalRecordVersion { get; set; }

    }

    public enum EntityType
    {
        MeasurementPoint,
        Location
    }

    public enum TransactionalRecordVersion
    {
        Daily,
        Monthly
    }
}
