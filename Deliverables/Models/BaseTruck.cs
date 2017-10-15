using System;
using System.Collections.Generic;

namespace Deliverables.Models
{
    public abstract class BaseTruck : ITruck
    {
        private Delivery _activeDelivery;

        protected BaseTruck(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Deliveries = new List<Delivery>();
        }

        public string Name { get; }
        public string Image { get; }

        public Delivery ActiveDelivery
        {
            get => _activeDelivery;
            set
            {
                if (value != null)
                {
                    TotalTravelTime += value.HoursToComplete;
                }
                _activeDelivery = value;
            } 
        }

        public int ActiveDeliveryHoursLeft { get; set; }
        public int TotalTravelTime { get; set; }
        public List<Delivery> Deliveries { get; }
    }
}
