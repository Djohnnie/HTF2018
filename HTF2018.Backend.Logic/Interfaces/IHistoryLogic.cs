using System.Threading.Tasks;
using HTF2018.Backend.Common.Model;

namespace HTF2018.Backend.Logic.Interfaces
{
    public interface IHistoryLogic
    {
        Task<History> Pop();

        Task Push(Status status);
    }
}