using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsCareProject.Models;

namespace PetsCareProject.Components
{
    [ViewComponent(Name = "PostView")]
    public class PostViewComponent : ViewComponent
    {
        private readonly DbPetsCare _context;

        public PostViewComponent(DbPetsCare context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfPost = (from p in _context.tblPosts
                              where (p.IsActive == true)
                              orderby p.PostID descending
                              select p).Take(6).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", listOfPost));
        }
    }
}
