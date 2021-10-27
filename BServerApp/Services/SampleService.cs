using BServerApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
namespace BServerApp.Services
{
    public class SampleService
    {
        private static readonly string[] Summaries = new[]
           {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<SampleClass[]> GetForecastAsync(DateTime startDate)
        {
             
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new SampleClass
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
