using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsCareProject.Areas.Admin.Models
{
    [Table("tblRoles")]
    public class tblRoles
    {
        [Key]
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }

    }
}

