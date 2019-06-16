using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PersonalEconomist.Domain.Models.User;
using PersonalEconomist.Entities.Models.Counter;
using PersonalEconomist.Services.Stores.CounterStore;
using System;
using System.Threading.Tasks;

namespace PersonalEconomist.Hubs
{
    public class CounterHub : Hub
    {
        public ICounterStore _counterStore;
        private readonly UserManager<User> _userManager;

        public CounterHub(ICounterStore counterStore, UserManager<User> userManager)
        {
            _counterStore = counterStore;
            _userManager = userManager;
        }

        public async Task AddCounter(string counterJSON)
        {
            var counter = JsonConvert.DeserializeObject<CounterDTO>(counterJSON);

            var newCounter = await _counterStore.AddCounter(counter);

            await Clients.All.SendAsync("AddCounter", JsonConvert.SerializeObject(newCounter));
        }

        public async Task GetCounters(string id)
        {
            var counters = await _counterStore.GetCounters(id);

            await Clients.All.SendAsync("GetCounters", JsonConvert.SerializeObject(counters));
        }
    }
}
