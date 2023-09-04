using Microsoft.EntityFrameworkCore;
using PetsCareProject.Areas.Admin.Models;

namespace PetsCareProject.Models
{
    public class DbPetsCare : DbContext
    {
        public DbPetsCare(DbContextOptions<DbPetsCare> options) : base(options)
        {

        }
        //tblMenu
        public DbSet<tblMenu> Menu { get; set; }

        //tblFooter
        public DbSet<tblFooter> Footer { get; set; }

        //tblDanhMuc
        public DbSet<tblCategories> tblCategories { get; set; }


        //tblSản Phẩm
        public DbSet<tblProduct> tblProducts { get; set; }

        //tblBaiViet
        public DbSet<tblPosts> tblPosts { get; set; }

        //tblServices
        public DbSet<tblServices> tblServices { get; set; }

        //tblSchedules
        public DbSet<tblSchedules> tblSchedules { get; set; }

        //tblDoctorFee
        public DbSet<tblDoctorFee> tblDoctorFee { get; set; }


        //tblBookingCare
        public DbSet<tblBookingCare> tblBookingCares { get; set; }

        //tblNhanVien
        public DbSet<tblDoctors> tblDoctors { get; set; }


        public DbSet<tblPosition> tblPosition { get; set; }

        //tblAdminMenu
        public DbSet<tblAdminMenu> AdminMenus { get; set; }

        //tblAdminUser
        public DbSet<tblAdminUser> AdminUsers { get; set; }
    }
}
