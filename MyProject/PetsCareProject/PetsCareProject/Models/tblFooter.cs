using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblFooter")]
    public class tblFooter
    {
        [Key]
        public int FooterID { get; set; }
        public string? FooterName { get; set; }
        public bool? IsActive { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public int Levels { get; set; }
        public int ParentID { get; set; }
        public string? Link { get; set; }
        public int FooterOrder { get; set; }
    }
}
