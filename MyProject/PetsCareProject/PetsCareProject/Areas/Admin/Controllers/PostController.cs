using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using PetsCareProject.Models;

namespace PetsCareProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DbPetsCare _dataContext;

        public PostController(DbPetsCare dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var dlList = _dataContext.tblPosts.OrderBy(p => p.PostID).ToList();
            return View(dlList);
        }

        //Action thêm bài viết
     
        public IActionResult Create()
        {
            var mList = (from c in _dataContext.tblCategories
                         where (c.IsActive == true) && (c.Position == 2)
                         orderby c.CategoryID ascending
                         select new SelectListItem()
                         {
                             Text = c.CategoryName,
                             Value = c.CategoryID.ToString(),
                         }).ToList();
            mList.Insert(0, new SelectListItem()
            {
                Text = "----Lựa chọn danh mục----",
                Value = "0"
            });
            ViewBag.mnList = mList;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Action thêm bài viết vào CSDL
        public IActionResult Create(tblPosts p)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblPosts.Add(p);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        //Action sửa bài viết
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var p = _dataContext.tblPosts.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            var pList = (from c in _dataContext.tblCategories
                         where (c.IsActive == true) && (c.Position == 2)
                         orderby c.CategoryID descending
                         select new SelectListItem()
                         {
                             Text = c.CategoryName,
                             Value = c.CategoryID.ToString(),
                         }).ToList();
            pList.Insert(0, new SelectListItem()
            {
                Text = "----Lựa chọn danh mục----",
                Value = "0"
            });
            ViewBag.mnList = pList;
            return View(p);
        }
        //Action sửa bài viết trong CSDL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(tblPosts p)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblPosts.Update(p);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var dl = _dataContext.tblPosts.Find(id);

            if (dl == null)
            {
                return NotFound();
            }
            return View(dl);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var delePost = _dataContext.tblPosts.Find(id);
            if (delePost == null)
            {
                return NotFound();
            }
            _dataContext.tblPosts.Remove(delePost);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

