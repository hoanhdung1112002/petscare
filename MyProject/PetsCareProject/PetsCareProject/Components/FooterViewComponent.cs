using Microsoft.AspNetCore.Mvc;
using PetsCareProject.Models;


namespace PetsCareProject.Components
{
    [ViewComponent(Name = "FooterView")]
    public class FooterViewComponent : ViewComponent
    {
        private DbPetsCare _context;
        public FooterViewComponent(DbPetsCare context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofMenu = (from m in _context.Footer
                              where (m.IsActive == true)
                              select m).ToList();
            //m.Position == 1 là những menu nằm phía trên
            //m.Position == 2 là những menu nằm phía dưới
            return await Task.FromResult((IViewComponentResult)View("Default", listofMenu));

        }
    }
}

