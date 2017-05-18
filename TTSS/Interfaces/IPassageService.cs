using System.Threading.Tasks;
using TTSSLib.Models.Data;
using TTSSLib.Models.Enums;

namespace TTSSLib.Interfaces
{
    public interface IPassageService
    {
        ResponseReason ResponseReason { get; }
        Task<Passages> GetPassages(int id, StopPassagesType type = StopPassagesType.Departure);
        Task<Passages> GetPassages(StopBase stop, StopPassagesType type = StopPassagesType.Departure);
    }
}
