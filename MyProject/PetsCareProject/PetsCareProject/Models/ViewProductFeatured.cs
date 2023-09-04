using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsCareProject.Models
{
    [Table("tblProducts")]
    public class ViewProductFeatured
    {
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string? Image { get; set; }
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        public string? DataFilter { get; set; }
        public IList<tblServices> Services { get; set; }
        public IList<tblDoctors> Employees { get; set; }
        public int TotalPrice
        {
            get { return (int)(Price * (1 - (double)Discount / 100)); }
        }
    }
}
