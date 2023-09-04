using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using PetsCareProject.Models;

namespace PetsCareProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly DbPetsCare _dataContext;

        public AccountController(DbPetsCare dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var dlList = _dataContext.AdminUsers.OrderBy(p => p.UserID).ToList();
            return View(dlList);
        }
       
    }
}

