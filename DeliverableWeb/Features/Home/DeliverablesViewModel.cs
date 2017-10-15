using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deliverables.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverableWeb.Features.Home
{
    [ModelBinder(BinderType = typeof(SimulationViewModelBinder))]
    public class SimulationViewModel
    {
        public SimulationViewModel()
        {
            Deliveries = new List<Delivery>();
            Trucks = new List<ITruck>();

            Errors = new List<string>();
        }

        public bool Valid { get; set; }
        public List<string> Errors { get; }
        public List<Delivery> Deliveries { get; }
        public List<ITruck> Trucks { get; }
    }
}
