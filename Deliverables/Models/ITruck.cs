using System;
using System.Collections.Generic;

namespace Deliverables.Models
{
    public interface ITruck
    {
        string Name { get; }
        Delivery ActiveDelivery { get; set; }
        int ActiveDeliveryHoursLeft { get; set; }
        int TotalTravelTime { get; set; }
        List<Delivery> Deliveries { get; }
    }
}
