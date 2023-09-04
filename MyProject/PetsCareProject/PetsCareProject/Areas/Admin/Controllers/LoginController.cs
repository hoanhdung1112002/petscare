using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsCareProject.Areas.Admin.Models;
using PetsCareProject.Models;
using PetsCareProject.Utilities;

namespace PetsCareProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DbPetsCare _context;
        public LoginController(DbPetsCare context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Action Đăng nhập khi đã có tài khoản trên hệ thống
        [HttpPost]
        public IActionResult Index(tblAdminUser user)
        {
            if (user == null)
            {
                return NotFound();
            }

            string pw = Functions.MD5Password(user.Password);

            var check = _context.AdminUsers.Where(m => (m.Email == user.Email)).FirstOrDefault();
            if (check == null)
            {
                Functions._Message = "Invalid UserName or Password";
                return RedirectToAction("Index", "Login");
            }

            Functions._Message = String.Empty;
            Functions._UserID = check.UserID;
            Functions._UserName = String.IsNullOrEmpty(check.UserName) ? String.Empty : check.UserName;
            Functions._Email = String.IsNullOrEmpty(check.Email) ? String.Empty : check.Email;

            return RedirectToAction("Index", "Home");
        }
    }
}
