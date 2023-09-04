using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsCareProject.Models
{
    [Table("tblPosts")]
    public class tblPosts
    {
        [Key]
        public int PostID { get; set; }
        public int CategoryID { get; set; }
        public string? Title { get; set; }
        public string? Abstract { get; set; }
        public string? Contents{ get; set; }
        public string? Link { get; set; }
        public string? Thumb { get; set; }
        public int Viewer { get; set; }
        public int PostOrder { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }

    }
}
