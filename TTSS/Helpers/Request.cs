using System.Threading.Tasks;
using TTSSLib.Models.Enums;

namespace TTSSLib.Helpers
{
    internal static class Request
    {
        internal static async Task<string> AllStops()
        {
            return await HttpHelper.GetString(Addresses.AllStops);
        }

        internal static async Task<string> AutoComplete(string query)
        {
            return await HttpHelper.GetString(string.Format(Addresses.Autocomplete, query));
        }

        internal static async Task<string> StopPassages(int stopId, StopPassagesType type)
        {
            string stype = string.Empty;
            switch (type)
            {
                case StopPassagesType.Arrival:
                    stype = "arrival";
                    break;
                case StopPassagesType.Departure:
                    stype = "departure";
                    break;
            }

            return await HttpHelper.GetString(string.Format(Addresses.PassageInfo, stopId, stype));
        }
    }
}
