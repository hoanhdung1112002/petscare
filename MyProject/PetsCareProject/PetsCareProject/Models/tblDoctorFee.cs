using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblDoctorFees")]
    public class tblDoctorFee
    {
        [Key]
        public int DoctorFeeID { get; set; }
        public string? FeeAmount { get; set; }
    }
}
