using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Deliverables.Models;

namespace Deliverables
{
    public sealed class EventingDeliverySimulator : DeliverySimulator
    {
        public event EventHandler<SimulationTickedEventArgs> SimulationTicked;
        private readonly Timer _timer;

        private static EventingDeliverySimulator _instance;
        private TaskCompletionSource<(bool RanToCompletion, IEnumerable<string> Errors)> _tcs;

        public EventingDeliverySimulator(IEnumerable<ITruck> trucks, IEnumerable<Delivery> deliveries) : base(trucks, deliveries)
        {
            _timer = new Timer(1000);
        }
        
        public (IEnumerable<ITruck> Trucks, IEnumerable<Delivery> Deliveries) GetItems() => (_trucks, _deliveries);

        public void Stop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        protected override Task<(bool RanToCompletion, IEnumerable<string> Errors)> RunProcessAsync()
        {
            _tcs = new TaskCompletionSource<(bool RanToCompletion, IEnumerable<string> Errors)>();

            _timer.Elapsed += (s, e) =>
                              {
                                  if (!SimulationTick())
                                  {
                                      SimulationTicked?.Invoke(this, new SimulationTickedEventArgs(_trucks, _deliveries, false));
                                      return;
                                  }

                                  SimulationTicked?.Invoke(this, new SimulationTickedEventArgs(_trucks, _deliveries, true));
                                  _tcs.SetResult((true, null));
                                  Stop();
                              };

            _timer.Start();

            return _tcs.Task;
        }
    }
}