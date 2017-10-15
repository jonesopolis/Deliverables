using System.Collections.Generic;
using System.Threading.Tasks;
using Deliverables.Models;

namespace Deliverables {
    public sealed class AutomatedDeliverySimulator : DeliverySimulator
    {
        public AutomatedDeliverySimulator(IEnumerable<ITruck> trucks, IEnumerable<Delivery> deliveries) : base(trucks, deliveries) { }

        protected override Task<(bool RanToCompletion, IEnumerable<string> Errors)> RunProcessAsync()
        {
            while (!SimulationTick()) { }

            return Task.FromResult<(bool, IEnumerable<string>)>((true, null));
        }
    }
}