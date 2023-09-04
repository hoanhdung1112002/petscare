using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblSchedules")]
    public class tblSchedules
    {
        [Key]
        public int ScheduleID { get; set; }
        public int DoctorID { get; set; }
        public string? TimeSlot { get; set; }
        public bool? IsBooked { get; set; }
    }
}
