using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalMonitoring.API.Hubs;
using SignalMonitoring.API.Models;
using SignalMonitoring.API.Services;
using System;
using System.Threading.Tasks;

namespace SignalMonitoring.API.Controllers
{
    [Route("api/v1/signals")]
    [ApiController]
    public class SignalController : ControllerBase
    {
        private readonly ISignalService m_signalService;
        private readonly IHubContext<SignalHub> m_hubContext;

        public SignalController(ISignalService signalService, IHubContext<SignalHub> hubContext)
        {
            m_signalService = signalService;
            m_hubContext = hubContext;
        }

        [HttpPost]
        [Route("deliverypoint")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(bool))]
        public async Task<IActionResult> SignalArrived(SignalInputModel inputModel)
        {
            //you can validate input here
            //then if the inputmodel is valid then you can save the signal
            var saveResult = await m_signalService.SaveSignalAsync(inputModel);

            //if you can save the signal you can notify all clients by using SignalHub
            if (saveResult)
            {
                SignalViewModel signalViewModel = new SignalViewModel()
                {
                    CustomerName = inputModel.CustomerName,
                    Description = inputModel.Description,
                    Area = inputModel.Area,
                    Zone = inputModel.Zone,
                    SignalStamp = Guid.NewGuid().ToString()
                };
                await m_hubContext.Clients.All.SendAsync("SignalMessageReceived", signalViewModel);
            }

            return StatusCode(200, saveResult);
        }


    }
}
