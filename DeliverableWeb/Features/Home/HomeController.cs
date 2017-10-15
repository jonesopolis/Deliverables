using System;
using System.Collections;
using Deliverables;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace DeliverableWeb.Features.Home
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IHubContext<SimulatorHub> _hub;
        private static EventingDeliverySimulator _simulator;

        public HomeController(IHubContext<SimulatorHub> hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost("simulation")]
        public IActionResult Run(SimulationViewModel vm)
        {
            _simulator?.Stop();
            _simulator = new EventingDeliverySimulator(vm.Trucks, vm.Deliveries);

            if (!_simulator.Validate(out IEnumerable<string> errors))
            {
                vm.Valid = false;
                vm.Errors.AddRange(errors);
            }
            else
            {
                vm.Valid = true;
                _simulator.SimulationTicked += (sender, args) => _hub.Clients.All.InvokeAsync("update", args);
                _simulator.RunAsync();
            }

            return PartialView("_Simulation", vm);
        }

        [HttpPost("complete")]
        public IActionResult Complete()
        {
            if (_simulator == null)
            {
                return BadRequest();
            }

            var models = _simulator.GetItems();
            var vm = new SimulationCompleteViewModel(models.Trucks, models.Deliveries);

            return PartialView("_SimulationComplete", vm);
        }
    }
}