using Microsoft.AspNetCore.Mvc;
using PetsCareProject.Models;


namespace PetsCareProject.Components
{
    [ViewComponent(Name = "MenuView")]
    public class MenuViewComponent : ViewComponent
    {
        private DbPetsCare _dataContext;
        public MenuViewComponent(DbPetsCare dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofMenu = (from m in _dataContext.Menu
                              where (m.IsActive == true)
                              select m).ToList();
            //m.Position == 1 là những menu nằm phía trên
            //m.Position == 2 là những menu nằm phía dưới
            return await Task.FromResult((IViewComponentResult)View("Default", listofMenu));

        }
    }
}

