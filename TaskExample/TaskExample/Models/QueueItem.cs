namespace TaskExample.Models
{
    using System;

    public class QueueItem
    {
        public int QueueID { get; set; }
        public int ConsumerThreadID { get; set; }
        public int ProducerThreadID { get; set; }
        public string RandomString { get; set; }
        public DateTime EnqueueDateTime { get; set; }
        public DateTime DequeueDateTime { get; set; }
    }
}
