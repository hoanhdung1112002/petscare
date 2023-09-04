using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PetsCareProject.Models;

namespace PetsCareProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly DbPetsCare _dataContext;

        public DoctorController(DbPetsCare dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var listofDoctor = (from d in _dataContext.tblDoctors
                                    join p in _dataContext.tblPosition
                                    on d.PositionID equals p.PositionID
                                    join f in _dataContext.tblDoctorFee
                                    on d.DoctorFeeID equals f.DoctorFeeID
                                    where (d.IsActive == true)
                                    orderby d.DoctorID ascending
                                    select new ViewDoctors()
                                    {
                                        DoctorID = d.DoctorID,
                                        DoctorName = d.DoctorName,
                                        Thumb = d.Thumb,
                                        FeeAmount = f.FeeAmount,
                                        PositionID = p.PositionID,
                                        PositionName = p.PositionName,
                                        PhoneNumber = d.PhoneNumber,
                                        Email = d.Email,
                                        Address = d.Address,
                                        IsActive = d.IsActive

                                    }).ToList();
            return View(listofDoctor);
        }


        //Action thêm nhân viên
        public IActionResult Create()
        {
            //Ds tblPosition
            var listofPosition = (from c in _dataContext.tblPosition
                         where (c.IsActive == true)
                         orderby c.PositionID ascending
                         select new SelectListItem()
                         {
                             Text = c.PositionName,
                             Value = c.PositionID.ToString(),
                         }).ToList();
            listofPosition.Insert(0, new SelectListItem()
            {
                Text = "----Chức vụ tại phòng khám----",
                Value = "0"
            });
            ViewBag.positionList = listofPosition;


            //service
            var listofService = (from s in _dataContext.tblServices
                                  where (s.IsActive == true)
                                  orderby s.ServiceID ascending
                                  select new SelectListItem()
                                  {
                                      Text = s.ServiceName,
                                      Value = s.ServiceID.ToString(),
                                  }).ToList();
            listofService.Insert(0, new SelectListItem()
            {
                Text = "----Phụ trách chuyên môn----",
                Value = "0"
            });
            ViewBag.serviceList = listofService;

            //doctor fee
            var listofDoctorFee = (from f in _dataContext.tblDoctorFee
                         orderby f.DoctorFeeID ascending
                         select new SelectListItem()
                         {
                             Text = f.FeeAmount,
                             Value = f.DoctorFeeID.ToString(),
                         }).ToList();
            listofDoctorFee.Insert(0, new SelectListItem()
            {
                Text = "----Giá khám ----",
                Value = "0"
            });
            ViewBag.doctorfeeList = listofDoctorFee;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Action thêm bài viết vào CSDL
        public IActionResult Create(tblDoctors pro)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblDoctors.Add(pro);
                _dataContext.SaveChanges();
                TempData["AlertMessage"] = "Tạo mới nhân viên thành công!!!";
                return RedirectToAction("Index");
            }
            return View(pro);
        }

        //action sửa thông tin nhân viên
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fd = _dataContext.tblDoctors.Find(id);
            if (fd == null)
            {
                return NotFound();
            }
            //Ds tblPosition
            var listofPosition = (from c in _dataContext.tblPosition
                                  where (c.IsActive == true)
                                  orderby c.PositionID ascending
                                  select new SelectListItem()
                                  {
                                      Text = c.PositionName,
                                      Value = c.PositionID.ToString(),
                                  }).ToList();
            listofPosition.Insert(0, new SelectListItem()
            {
                Text = "----Chức vụ tại phòng khám----",
                Value = string.Empty
            });
            ViewBag.positionList = listofPosition;


            //service
            var listofService = (from s in _dataContext.tblServices
                                 where (s.IsActive == true)
                                 orderby s.ServiceID ascending
                                 select new SelectListItem()
                                 {
                                     Text = s.ServiceName,
                                     Value = s.ServiceID.ToString(),
                                 }).ToList();
            listofService.Insert(0, new SelectListItem()
            {
                Text = "----Phụ trách chuyên môn----",
                Value = string.Empty
            });
            ViewBag.serviceList = listofService;

            //doctor fee
            var listofDoctorFee = (from f in _dataContext.tblDoctorFee
                                   orderby f.DoctorFeeID ascending
                                   select new SelectListItem()
                                   {
                                       Text = f.FeeAmount,
                                       Value = f.DoctorFeeID.ToString(),
                                   }).ToList();
            listofDoctorFee.Insert(0, new SelectListItem()
            {
                Text = "----Giá khám ----",
                Value = string.Empty
            });
            ViewBag.doctorfeeList = listofDoctorFee;
            return View(fd);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Action thêm bài viết vào CSDL
        public IActionResult Edit(tblDoctors pro)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblDoctors.Update(pro);
                _dataContext.SaveChanges();
                TempData["AlertMessage"] = "Cập nhật thông tin thành công!!!";
                return RedirectToAction("Index");
            }
            return View(pro);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var dl = _dataContext.tblDoctors.Find(id);

            if (dl == null)
            {
                return NotFound();
            }
            return View(dl);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var delDoctor = _dataContext.tblDoctors.Find(id);
            if (delDoctor == null)
            {
                return NotFound();
            }
            _dataContext.tblDoctors.Remove(delDoctor);
            _dataContext.SaveChanges();
            TempData["AlertMessage"] = "Xoá nhân viên thành công!!!";
            return RedirectToAction("Index");
        }
    }
}

