using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblBookingCare")]
    public class ViewBookingCare
    {
        [Key]
        public int BookingCareID { get; set; }
        public int ServiceID { get; set; }
        public int DoctorID { get; set; }
        public int ScheduleID { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ServiceName { get; set; }
        public string? DoctorName { get; set; }
        public string? FeeAmount { get; set; }
        public string? TimeSlot { get; set; }
        public string? Email { get; set; }
        public string? PetName { get; set; }
        public DateTime? Time { get; set; }
        public bool? IsActive { get; set; }
        public bool? StatusBooking { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public IList<tblDoctors> tblDoctors { get; set; }
        public IList<tblServices> tblServices { get; set; }
        public IList<tblSchedules> tblSchedules { get; set; }
        public IList<tblDoctorFee> tblDoctorFees { get; set; }

    }
}
