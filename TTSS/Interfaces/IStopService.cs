using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSSLib.Models.Data;

namespace TTSSLib.Interfaces
{
    public interface IStopService
    {
        Task<List<StopBase>> GetCompletionFromService(string name);
        Task<List<StopData>> GetAllStops();
    }
}
