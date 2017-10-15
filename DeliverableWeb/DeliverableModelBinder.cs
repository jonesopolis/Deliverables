using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Deliverables.Models;
using DeliverableWeb.Features.Home;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace DeliverableWeb
{
    public class SimulationViewModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            
            string jsonStr;
            using (var sr = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                jsonStr = sr.ReadToEnd();
            }

            if (string.IsNullOrEmpty(jsonStr))
            {
                return Task.CompletedTask;
            }

            var json = JObject.Parse(jsonStr);

            var vm = new SimulationViewModel();
            vm.Trucks.AddRange(GetStandardTrucks((JArray) json["standardTrucks"]));
            vm.Trucks.AddRange(GetCustomTrucks((JArray)json["customTrucks"]));
            vm.Deliveries.AddRange(GetDeliveries((JArray)json["deliveries"]));

            bindingContext.Result = ModelBindingResult.Success(vm);

            return Task.CompletedTask;
        }

        private IEnumerable<Delivery> GetDeliveries(JArray jArray)
        {
            foreach (string hour in jArray)
            {
                if (int.TryParse(hour, out int i))
                {
                    yield return new Delivery(i);
                }
            }
        }

        private IEnumerable<ITruck> GetStandardTrucks(JArray jArray)
        {
            foreach (string truck in jArray)
            {
                switch (truck.ToLower())
                {
                    case "red":
                        yield return new RedTruck();
                        break;
                    case "blue":
                        yield return new BlueTruck();
                        break;
                }
            }
        }

        private IEnumerable<ITruck> GetCustomTrucks(JArray jArray)
        {
            foreach (string custom in jArray)
            {
                yield return new CustomTruck(custom);
            }
        }
    }
}
