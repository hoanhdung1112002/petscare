using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PetsCareProject.Models
{
    [Table("tblPosition")]
    public class tblPosition
    {
        [Key]
        [NotNull] public int PositionID { get; set; }
        public string? PositionName { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
