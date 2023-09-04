using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblDoctors")]
    public class tblDoctors
    {
        [Key]
        public int DoctorID { get; set; }
        public string? DoctorName { get; set; }
        public int PositionID { get; set; }
        public int ServiceID { get; set; }
        public int DoctorFeeID { get; set; }
        public string? Thumb { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }


    }
}
