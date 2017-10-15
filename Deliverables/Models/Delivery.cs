using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Deliverables.Models
{
    public sealed class Delivery
    {
        public Delivery(int hoursToComplete)
        {
            if (hoursToComplete <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(hoursToComplete), $"{nameof(hoursToComplete)} must be a positive integer");
            }

            HoursToComplete = hoursToComplete;
            Status = DeliveryStatus.Queued;
        }

        public int HoursToComplete { get; }
        public Guid? TrackingNumber { get; internal set; }
        public int DepartureTime { get; internal set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DeliveryStatus Status { get; internal set; }
    }

    public enum DeliveryStatus
    {
        Queued,
        Active,
        Delivered
    }
}