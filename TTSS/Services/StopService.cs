using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TTSSLib.Converters;
using TTSSLib.Helpers;
using TTSSLib.Interfaces;
using TTSSLib.Models.API;
using TTSSLib.Models.Data;

namespace TTSSLib.Services
{
    public class StopService : IStopService
    {
        public async Task<List<StopData>> GetAllStops()
        {
            string response = await Request.AllStops();
            var preparsed = JsonConvert.DeserializeObject<GeoStops>(response).Stops;
            return preparsed.Select(s => new StopData {
                ID = int.Parse(s.ShortName),
                Latitude = s.Latitude.ToCoordinate(),
                Longitude = s.Longitude.ToCoordinate(),
                Name = s.Name,
                Type = StopCategoryConverter.Convert(s.Category)
            })
            .OrderBy(s => s.Name)
            .ToList();
        }

        public async Task<List<StopBase>> GetCompletionFromService(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length <= 1)
            {
                return new List<StopBase>();
            }

            string response = await Request.AutoComplete(name);

            var preparsed = JsonConvert.DeserializeObject<List<Stop>>(response);

            return preparsed
                .Where(s => s.Type == "stop")
                .Select(s => new StopBase { ID = s.ID, Name = WebUtility.HtmlDecode(s.Name) })
                .ToList();
        }
    }
}
