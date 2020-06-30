using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalMonitoring.API.Persistence
{
    public class AnswerModel
    {
        public int Id { get; set; }

        public string Answer { get; set; }

    }
}