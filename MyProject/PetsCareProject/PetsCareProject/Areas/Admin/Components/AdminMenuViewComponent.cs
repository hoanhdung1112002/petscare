using Microsoft.AspNetCore.Mvc;
using PetsCareProject.Models;

namespace PetsCareProject.Areas.Admin.Components
{
    [ViewComponent(Name = "AdminMenuView")]
    public class AdminMenuViewComponent : ViewComponent
    {
        private readonly DbPetsCare _context;

        public AdminMenuViewComponent(DbPetsCare context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var AdminmnList = (from mnu in _context.AdminMenus
                          where (mnu.IsActive == true)
                          select mnu).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", AdminmnList));
        }
    }
}


