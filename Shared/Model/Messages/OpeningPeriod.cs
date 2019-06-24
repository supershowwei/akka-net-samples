using System;
using Newtonsoft.Json;

namespace Shared.Model.Messages
{
    public sealed class OpeningPeriod
    {
        [JsonConstructor]
        public OpeningPeriod(DateTime startTime, DateTime endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public OpeningPeriod(TimeSpan startTime, TimeSpan duration, DateTime? specificDate = null)
        {
            this.StartTime = specificDate?.Add(startTime) ?? DateTime.Today.Add(startTime);
            this.EndTime = this.StartTime.Add(duration);
        }

        public DateTime StartTime { get; }

        public DateTime EndTime { get; }
    }
}