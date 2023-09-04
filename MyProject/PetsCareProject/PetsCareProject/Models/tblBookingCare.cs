using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblBookingCare")]
    public class tblBookingCare
    {
        [Key]
        public int BookingCareID { get; set; }
        public int ServiceID { get; set; }
        public int DoctorID { get; set; }
        public int ScheduleID { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? PetName { get; set; }
        public string? Image { get; set; }
        public DateTime? Time { get; set; }
        public bool? IsActive { get; set; }
        public bool? StatusBooking { get; set; }
        public string? Description { get; set; }


    }
}
