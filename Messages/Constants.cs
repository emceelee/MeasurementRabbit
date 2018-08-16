using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public static class Constants
    {
        public static class Queues
        {
            public const string Summarization = "Summarization";
        }

        public static class Exchanges
        {
            public const string ProductQuantityUpdate = "ProductQuantityUpdate";
            public const string SummarizationComplete = "SummarizationComplete";
        }
    }
}
