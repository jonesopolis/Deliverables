using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deliverables.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverableWeb.Features.Home
{
    public class SimulationCompleteViewModel
    {
        public SimulationCompleteViewModel(IEnumerable<ITruck> trucks, IEnumerable<Delivery> deliveries)
        {
            Trucks = trucks ?? throw new ArgumentNullException(nameof(trucks));
            Deliveries = deliveries ?? throw new ArgumentNullException(nameof(deliveries));
        }
        public IEnumerable<Delivery> Deliveries { get; }
        public IEnumerable<ITruck> Trucks { get; }
    }
}
