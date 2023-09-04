using Microsoft.AspNetCore.Mvc;
using PetsCareProject.Models;

namespace PetsCareProject.Components
{
    [ViewComponent(Name = "ProductFeaturedView")]
    public class ProductFeaturedView : ViewComponent
    {
        private DbPetsCare _context;

        public ProductFeaturedView(DbPetsCare context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var fProduct = (from p in _context.tblProducts
                            join c in _context.tblCategories
                            on p.CategoryID equals c.CategoryID
                            where (p.IsActive == true) && (c.Position == 3)
                            orderby p.ProductID descending
                            select new ViewProductFeatured()
                            {
                                ProductID = p.ProductID,
                                ProductName = p.ProductName,
                                CategoryID = c.CategoryID,
                                CategoryName = c.CategoryName,
                                Price = p.Price,
                                Discount = p.Discount,
                                Image = p.Image,
                                IsActive = p.IsActive,
                                DataFilter = c.DataFilter
                            }).Take(12).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", fProduct));

        }
    }
}
