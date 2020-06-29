using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalMonitoring.API.Persistence
{
    public class TermsModel
    {
        [Key]
        public int TermId { get; set; }

        public string Terms { get; set; }

    }
}