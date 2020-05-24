using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalMonitoring.API.Models;

namespace SignalMonitoring.API.Services
{
    public interface ISignalService
    {
        Task<bool> SaveSignalAsync(SignalInputModel inputModel);
    }
}
