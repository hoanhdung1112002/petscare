﻿using Microsoft.AspNetCore.Mvc;
using PetsCareProject.Models;

namespace PetsCareProject.Components
{
    [ViewComponent(Name = "RecentPostView")]
    public class RecentPostViewComponent : ViewComponent
    {
        private readonly DbPetsCare _context;

        public RecentPostViewComponent(DbPetsCare context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listOfRecentPost = (from p in _context.tblPosts
                              where (p.IsActive == true)
                              orderby p.PostID descending
                              select p).Take(5).ToList();

            return await Task.FromResult((IViewComponentResult)View("Default", listOfRecentPost));
        }
    }
}

