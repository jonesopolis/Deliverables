using System;
using System.Collections.Generic;
using Deliverables.Models;

namespace Deliverables {
    public class SimulationTickedEventArgs : EventArgs
    {
        public SimulationTickedEventArgs(IEnumerable<ITruck> trucks, IEnumerable<Delivery> deliveries, bool complete)
        {
            Trucks = trucks;
            Deliveries = deliveries;
            Complete = complete;
        }
        public IEnumerable<ITruck> Trucks { get; }
        public IEnumerable<Delivery> Deliveries { get; }
        public bool Complete { get; }
    }
}