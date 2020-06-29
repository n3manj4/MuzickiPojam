using SignalMonitoring.API.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalMonitoring.API.Persistence;

namespace SignalMonitoring.API.Services
{
    public interface ITermService
    {
        string GetRandomTerm();
    }
}
