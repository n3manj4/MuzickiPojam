using SignalMonitoring.API.Models;
using SignalMonitoring.API.Persistence;
using System;
using System.Threading.Tasks;

namespace SignalMonitoring.API.Services
{
    public class SignalService : ISignalService
    {
        private readonly MainDbContext m_mainDbContext;

        public SignalService(MainDbContext mainDbContext)
        {
            m_mainDbContext = mainDbContext;
        }

        public async Task<bool> SaveSignalAsync(SignalInputModel inputModel)
        {
            try
            {
                //map input model to data model
                //at this point we assume a signal arrives only one time and it's uniue
                SignalDataModel signalModel = new SignalDataModel()
                {
                    CustomerName = inputModel.CustomerName,
                    Description = inputModel.Description,
                    AccessCode = inputModel.AccessCode,
                    Area = inputModel.Area,
                    Zone = inputModel.Zone,
                    SignalDate = DateTime.Now
                };

                //execute some business rules according to your cases.

                //if you decide to save signal add it to the db context
                m_mainDbContext.Signals.Add(signalModel);

                //save changes and if the signal has stored in db return true;
                return await m_mainDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                //log the exception or take some actions

                return false;
            }
        }
    }
}
