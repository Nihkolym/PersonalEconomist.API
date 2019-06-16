using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PersonalEconomist.Entities.Models.Indication;
using PersonalEconomist.Services.Stores.IndicationStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Hubs
{
    public class IndicationHub : Hub
    {
        public IIndicationStore _indicationStore;

        public IndicationHub(IIndicationStore indicationStore)
        {
            _indicationStore = indicationStore;
        }
        public async Task AddIndication(string indicationJSON)
        {
            var indication = JsonConvert.DeserializeObject<IndicationDTO>(indicationJSON);

            var newIndication = await _indicationStore.AddIndication(indication);

            await Clients.All.SendAsync("AddIndication", JsonConvert.SerializeObject(newIndication));
        }
    }
}
