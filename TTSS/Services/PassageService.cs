using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib.Converters;
using TTSSLib.Helpers;
using TTSSLib.Interfaces;
using TTSSLib.Models.API;
using TTSSLib.Models.Data;
using TTSSLib.Models.Enums;
using TTSSLib.Models.Internal;

namespace TTSSLib.Services
{
    public class PassageService : IPassageService
    {
        public async Task<Passages> GetPassages(int id, StopPassagesType type = StopPassagesType.Departure)
        {
            var response = await Request.StopPassages(id, type);
            var passage = JsonConvert.DeserializeObject<StopInfo>(response);
            var result = new Passages();
            result.ActualPassages = passage.ActualPassages.Select(ap => PassageConverter.Convert(ap)).ToList();
            result.OldPassages = passage.OldPassages.Select(ap => PassageConverter.Convert(ap)).ToList();
            return result;
        }

        public async Task<Passages> GetPassages(StopBase stop, StopPassagesType type = StopPassagesType.Departure)
        {
            return await GetPassages(stop.ID, type);
        }
    }
}
