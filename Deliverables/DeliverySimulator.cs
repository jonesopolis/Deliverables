using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Deliverables.Models;

namespace Deliverables
{
    public abstract class DeliverySimulator
    {
        private const int REQUIRED_DELIVERY_COUNT = 5;
        private const int REQUIRED_TRUCK_COUNT = 2;
        
        protected readonly IEnumerable<Delivery> _deliveries;
        protected readonly IEnumerable<ITruck> _trucks;

        private int _currentHour;

        protected DeliverySimulator(IEnumerable<ITruck> trucks, IEnumerable<Delivery> deliveries)
        {
            _trucks = trucks ?? throw new ArgumentNullException(nameof(trucks));
            _deliveries = deliveries ?? throw new ArgumentNullException(nameof(deliveries));

            _currentHour = 0;
        }

        public Task<(bool RanToCompletion, IEnumerable<string> Errors)> RunAsync()
        {
            if (!Validate(out IEnumerable<string> errors))
            {
               throw new InvalidOperationException("Invalid Simulation");
            }

            return RunProcessAsync();
        }

        protected abstract Task<(bool RanToCompletion, IEnumerable<string> Errors)> RunProcessAsync();

        protected bool SimulationTick()
        {
            //process each truck out for delivery
            foreach (var truck in _trucks.Where(t => t.ActiveDelivery != null))
            {
                if (--truck.ActiveDeliveryHoursLeft == 0)
                {
                    truck.ActiveDelivery.Status = DeliveryStatus.Delivered;
                    truck.ActiveDelivery = null;
                }
            }

            //start delivery for available trucks
            foreach (var truck in _trucks.Where(t => t.ActiveDelivery == null))
            {
                var nextDelivery = _deliveries.FirstOrDefault(d => d.Status == DeliveryStatus.Queued);
                if (nextDelivery != null)
                {
                    nextDelivery.DepartureTime = _currentHour;
                    nextDelivery.TrackingNumber = Guid.NewGuid();
                    nextDelivery.Status = DeliveryStatus.Active;
                    
                    truck.ActiveDelivery = nextDelivery;
                    truck.ActiveDeliveryHoursLeft = nextDelivery.HoursToComplete;
                    truck.Deliveries.Add(nextDelivery);
                }
            }

            //check if we are done - no active deliveries and all deliveries started
            if ( _deliveries.All(d => d.Status == DeliveryStatus.Delivered))
            {
                return true;
            }

            ++_currentHour;
            return false;
        }

        public bool Validate(out IEnumerable<string> errors)
        {
            var errs = new List<string>();

            if (_trucks.Count() < REQUIRED_TRUCK_COUNT)
            {
                errs.Add($"Must have at least {REQUIRED_TRUCK_COUNT} trucks.");
            }

            if (_deliveries.Count() < REQUIRED_DELIVERY_COUNT)
            {
                errs.Add($"Must have at least {REQUIRED_DELIVERY_COUNT} deliveries.");
            }

            errors = errs;
            return !errs.Any();
        }
    }
}