using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using PetsCareProject.Models;

namespace PetsCareProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DbPetsCare _dataContext;

        public ProductController(DbPetsCare dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var fProduct = (from p in _dataContext.tblProducts
                            join c in _dataContext.tblCategories
                            on p.CategoryID equals c.CategoryID
                            where (p.IsActive == true)
                            orderby p.ProductID ascending
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
                            }).ToList();
            return View(fProduct);
        }
        //action thêm sản phẩm
        public IActionResult Create()
        {
            var cList = (from c in _dataContext.tblCategories
                         where (c.IsActive == true)
                         orderby c.CategoryID ascending
                         select new SelectListItem()
                         {
                             Text = c.CategoryName,
                             Value = c.CategoryID.ToString(),
                         }).ToList();
            cList.Insert(0, new SelectListItem()
            {
                Text = "----Lựa chọn danh mục----",
                Value = "0"
            });
            ViewBag.prodList = cList;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Action thêm bài viết vào CSDL
        public IActionResult Create(tblProduct pro)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblProducts.Add(pro);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);
        }

        //Action sửa thông tin sản phẩm
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fd = _dataContext.tblProducts.Find(id);
            if (fd == null)
            {
                return NotFound();
            }
            var mnList = (from c in _dataContext.tblCategories
                          where (c.IsActive == true)
                          orderby c.CategoryID ascending
                          select new SelectListItem()
                          {
                              Text = c.CategoryName,
                              Value = c.CategoryID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View(fd);
        }

        //Action sửa món trong CSDL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(tblProduct fd)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblProducts.Update(fd);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fd);
        }


        //Actionn xóa
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var dl = _dataContext.tblProducts.Find(id);

            if (dl == null)
            {
                return NotFound();
            }
            return View(dl);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var del = _dataContext.tblProducts.Find(id);
            if (del == null)
            {
                return NotFound();
            }
            _dataContext.tblProducts.Remove(del);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

