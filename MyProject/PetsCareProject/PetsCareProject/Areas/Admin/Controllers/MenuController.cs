using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetsCareProject.Models;

namespace PetsCareProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly DbPetsCare _context;

        public MenuController(DbPetsCare context)
        {
            _context = context;
        }
        //Action Index
        public IActionResult Index()
        {
            var mnList = _context.Menu.OrderBy(m => m.MenuID).ToList();
            return View(mnList);
        }
        //Action Thêm Menu
        public IActionResult Create()
        {
            var mnList = (from m in _context.Menu
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Action thêm Menu vào CSDL
        public IActionResult Create(tblMenu mn)
        {
            if (ModelState.IsValid)
            {
                _context.Menu.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
        //Action sửa Menu
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Menu.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            var mnList = (from m in _context.Menu
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View(mn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(tblMenu mn)
        {
            if (ModelState.IsValid)
            {
                _context.Menu.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
        //Action sửa Menu trong CSDL

        //Action Xoá Menu
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Menu.Find(id);

            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Menu.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.Menu.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


