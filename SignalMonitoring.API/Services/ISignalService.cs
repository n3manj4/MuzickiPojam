using SignalMonitoring.API.Models;
using System.Threading.Tasks;

namespace SignalMonitoring.API.Services
{
    public interface ISignalService
    {
        Task<bool> SaveSignalAsync(SignalInputModel inputModel);
    }
}
