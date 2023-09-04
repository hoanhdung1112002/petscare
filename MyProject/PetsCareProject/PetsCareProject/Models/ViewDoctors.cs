using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblDoctors")]
    public class ViewDoctors
    {
        [Key]
        public int DoctorID { get; set; }
        public string? DoctorName { get; set; }
        public string? Thumb { get; set; }
        public int PositionID { get; set; }
        public int ServiceID { get; set; }
        public string? FeeAmount { get; set; }
        public int Price { get; set; }
        public int DoctorFeeID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PositionName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public IList<tblPosition> Positions { get; set; }
    }
}
