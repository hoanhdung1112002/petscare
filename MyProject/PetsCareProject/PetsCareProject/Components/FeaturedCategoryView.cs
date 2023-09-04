using Microsoft.AspNetCore.Mvc;
using PetsCareProject.Models;

namespace PetsCareProject.Components
{
    [ViewComponent(Name = "FeaturedCategoryView")]
    public class FeaturedCategoryView : ViewComponent
    {
        private DbPetsCare _context;

        public FeaturedCategoryView(DbPetsCare context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofCategories = (from c in _context.tblCategories
                                    where (c.IsActive == true) && (c.Position == 3)
                                    select c).ToList();
            //m.Position == 1 là những menu nằm phía trên
            return await Task.FromResult((IViewComponentResult)View("Default", listofCategories));

        }
    }
}
