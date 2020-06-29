using System.ComponentModel.DataAnnotations.Schema;

namespace SignalMonitoring.API.Persistence
{
    public class UserDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
