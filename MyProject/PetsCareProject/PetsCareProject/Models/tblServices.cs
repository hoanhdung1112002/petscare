using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblServices")]
    public class tblServices
    {
        [Key]
        public int ServiceID { get; set; }
        [NotMapped]
        public int DoctorID { get; set; }
        [NotMapped]
        public int ScheduleID { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
