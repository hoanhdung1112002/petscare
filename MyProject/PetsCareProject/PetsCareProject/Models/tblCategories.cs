using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblCategories")]
    public class tblCategories
    {
        [Key]
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public int Levels { get; set; }
        public int ParentID { get; set; }
        public string? Link { get; set; }
        public int CategoryOrder { get; set; }
        public int Position { get; set; }
        public string? DataFilter { get; set; }
    }
}
