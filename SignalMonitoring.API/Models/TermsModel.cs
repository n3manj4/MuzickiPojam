using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalMonitoring.API.Persistence
{
    public class TermsModel
    {
        [Key]
        public int Id { get; set; }

        public string Term { get; set; }

        public int Frequency { get; set; }
		public string CompositeTerm { get; set; }
	}
}