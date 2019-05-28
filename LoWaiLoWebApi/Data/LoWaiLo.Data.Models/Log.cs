namespace LoWaiLo.Data.Models
{
    using LoWaiLo.Data.Common;

    public class Log : BaseModel<int>
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public string LogLevel { get; set; }

        public string StackTrace { get; set; }

        public string LogMessage { get; set; }
    }
}
