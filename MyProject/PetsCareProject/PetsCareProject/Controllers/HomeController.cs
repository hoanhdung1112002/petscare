using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetsCareProject.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using X.PagedList;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MailKit.Net.Smtp;
using MimeKit;
using NuGet.Protocol.Core.Types;
using System.Diagnostics.Metrics;

namespace PetsCareProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbPetsCare _context;
        public HomeController(ILogger<HomeController> logger, DbPetsCare context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //Action Hiện Danh sách bài viết
        public ActionResult Blog(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lsPost = (from p in _context.tblPosts.AsNoTracking()
                              where p.IsActive == true
                              orderby p.PostID descending
                              select p).ToList();
            PagedList<tblPosts> lst = new PagedList<tblPosts>(lsPost, pageNumber, pageSize);
            return View(lst);
        }

        //Action Hiện bài viết theo loại

        public ActionResult BlogOfType(int? categoryid, int? page)
        {
            if (categoryid == null)
            {
                ViewBag.Message = "ID danh mục không hợp lệ";
                return View();
            }
            
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lsPost = (from p in _context.tblPosts.AsNoTracking()
                          where (p.IsActive == true) && (p.CategoryID == categoryid)
                          orderby p.PostID descending
                          select p).ToList();

            //Kiểm tra nếu bài viết không tồn tại thì thông báo cho người dùng

            PagedList<tblPosts> lst = new PagedList<tblPosts>(lsPost, pageNumber, pageSize);
            ViewBag.id = categoryid;
            return View(lst);
        }

        //Action Details
        [Route("/post-{slug}-{id:long}.html", Name = "Details")]

        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _context.tblPosts
                .FirstOrDefault(m => (m.PostID == id) && (m.IsActive == true));
            if (post == null)
            {
                return NotFound();
            }
            return View(post); 
        }

        //Action Chi tiết sản phẩm
        //Action Details
        [Route("/product-{slug}-{id:long}.html", Name = "ProductDetails")]
        public IActionResult ProductDetail(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _context.tblProducts
                .FirstOrDefault(m => (m.ProductID == id) && (m.IsActive == true));
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        //Action Đặt lịch khám bệnh
        public IActionResult BookingCare()
        {
            List<tblServices> servicesList = new List<tblServices>();

            servicesList = (from service in _context.tblServices
                            where service.IsActive == true
                            select service).ToList();

            servicesList.Insert(0, new tblServices { ServiceID = 0, ServiceName = "Select" });

            ViewBag.listofService = servicesList;
            return View();
        }
        //get Doctors
        [HttpGet]
        public JsonResult GetDoctors(int ServiceID)
        {
            List<tblDoctors> doctorsList = new List<tblDoctors>();

            doctorsList = (from doctor in _context.tblDoctors
                           where (doctor.ServiceID == ServiceID) && (doctor.IsActive == true)
                           select doctor).ToList();

            doctorsList.Insert(0, new tblDoctors { DoctorID = 0, DoctorName = "Select" });

            return Json(new SelectList(doctorsList, "DoctorID", "DoctorName"));
        }
        //
        //get schedules
        [HttpGet]

        public JsonResult GetSchedules(int DoctorID)
        {
            List<tblSchedules> scheduleList = new List<tblSchedules>();

            scheduleList = (from schedule in _context.tblSchedules
                            where (schedule.DoctorID == DoctorID) && (schedule.IsBooked == false)
                            select schedule).ToList();

            scheduleList.Insert(0, new tblSchedules { ScheduleID = 0, TimeSlot = "Select" });

            return Json(new SelectList(scheduleList, "ScheduleID", "TimeSlot"));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // Action thêm booking vào CSDL
        public IActionResult BookingCare(tblBookingCare b, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem khung giờ đã được đặt trước hay chưa
                var existingBooking = _context.tblBookingCares.FirstOrDefault(booking => booking.ScheduleID == b.ScheduleID);
                if (existingBooking != null)
                {
                    // Khung giờ đã được đặt trước, xử lý thông báo cho người dùng
                    TempData["ErrorMessage"] = "Khung giờ đã được đặt bởi người dùng khác. Vui lòng chọn khung giờ khác.";
                    // Lấy lại danh sách dịch vụ, bác sĩ và khung giờ cho dropdown
                    List<tblServices> servicesList = _context.tblServices.Where(service => service.IsActive == true).ToList();
                    servicesList.Insert(0, new tblServices { ServiceID = 0, ServiceName = "Select" });
                    ViewBag.listofService = servicesList;

                    List<tblDoctors> doctorsList = new List<tblDoctors>();
                    doctorsList.Insert(0, new tblDoctors { DoctorID = 0, DoctorName = "Select" });
                    ViewBag.listofDoctors = doctorsList;

                    List<tblSchedules> scheduleList = new List<tblSchedules>();
                    scheduleList.Insert(0, new tblSchedules { ScheduleID = 0, TimeSlot = "Select" });
                    ViewBag.listofSchedules = scheduleList;

                    return View(b);
                }

                // Khung giờ chưa được đặt trước, tiếp tục xử lý lưu thông tin người dùng vào cơ sở dữ liệu
                _context.tblBookingCares.Add(b);
                _context.SaveChanges();

                // Cập nhật trạng thái của khung giờ trong bảng tblSchedules thành false
                var schedule = _context.tblSchedules.FirstOrDefault(s => s.ScheduleID == b.ScheduleID);
                if (schedule != null)
                {
                    schedule.IsBooked = false;
                    _context.SaveChanges();
                }

                if (image != null && image.Length > 0)
                {
                    // Xử lý lưu ảnh vào thư mục hoặc lưu thông tin ảnh vào cơ sở dữ liệu
                    // Ví dụ, lưu ảnh vào thư mục wwwroot/images
                    var fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/bookings", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    // Lưu thông tin về ảnh vào cơ sở dữ liệu nếu cần
                    b.Image = "/files/bookings/" + fileName; // Lưu đường dẫn ảnh vào trường ImagePath trong bản ghi

                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                }

                TempData["AlertMessages"] = "Bạn đã đặt lịch thành công. Vui lòng kiểm tra email và phản hồi lại với chúng tôi nếu đó là bạn!!!";
                return RedirectToAction("BookingCare");
            }

            // Model không hợp lệ, trả về View với thông tin lỗi
            return View(b);
        }


        [HttpGet]
        public JsonResult CheckSchedule(int scheduleId)
        {
            var schedule = _context.tblSchedules.FirstOrDefault(s => s.ScheduleID == scheduleId);
            if (schedule != null)
            {
                return Json(new { isBooked = schedule.IsBooked });
            }
            return Json(new { isBooked = false });
        }


        //Danh sách sản phẩm theo danh mục
        public ActionResult Product(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lsProducts = (from p in _context.tblProducts.AsNoTracking()
                              where p.IsActive == true
                              orderby p.ProductID descending
                              select p).ToList();
            PagedList<tblProduct> lst = new PagedList<tblProduct>(lsProducts, pageNumber, pageSize);

            return View(lst);
        }

        //Action Hiện sản phẩm theo loại
        public ActionResult ProductOfType(int? categoryid, int? page)
        {
            if (categoryid == null)
            {
                ViewBag.Message = "ID danh mục không hợp lệ";
                return View();
            }

            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lsProduct = (from p in _context.tblProducts.AsNoTracking()
                          where (p.IsActive == true) && (p.CategoryID == categoryid)
                          orderby p.ProductID descending
                          select p).ToList();

            //Kiểm tra nếu bài viết không tồn tại thì thông báo cho người dùng

            PagedList<tblProduct> lst = new PagedList<tblProduct>(lsProduct, pageNumber, pageSize);
            ViewBag.id = categoryid;
            return View(lst);
        }

        //Action tìm kiếm sản phẩm
        public ActionResult SearchProduct(int? page, string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                ModelState.AddModelError("str", "Bạn phải nhập tên tìm kiếm.");
            }

            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var products = _context.tblProducts.AsQueryable();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToLower();
                products = products.Where(p => p.ProductName.ToLower().Contains(str));
            }
            //kiểm tra nếu người dùng nhập sai từ khoá tìm kiếm
            if (products.Any())
            {
                IPagedList<tblProduct> lst = products.ToPagedList(pageNumber, pageSize);
                return View(lst);
            }
            else
            {
                var matchedProducts = _context.tblProducts
                    .Where(p => p.ProductName.ToLower().Contains(str))
                    .Select(p => p.ProductName)
                    .ToList();

                if (matchedProducts.Count > 0)
                {
                    ViewBag.Mess = "Không tìm thấy sản phẩm phù hợp. Bạn có thể thử các từ khóa sau: " + string.Join(", ", matchedProducts);
                }
                else
                {
                    ViewBag.Mess = "Không tìm thấy sản phẩm nào phù hợp";
                }

                return View();
            }

        }

        //Action tìm kiếm bài viết theo tên bài viết
        public ActionResult SearchPost(int? page, string search)
        {
            if (search == null)
            {
                ViewBag.Message = "Tìm kiếm không hợp lệ";
                return View();
            }

            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lsPost = _context.tblPosts.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                lsPost = lsPost.Where(p => p.Title.ToLower().Contains(search));
            }

            if (lsPost.Any())
            {
                IPagedList<tblPosts> lst = lsPost.ToPagedList(pageNumber, pageSize);
                return View(lst);
            }
            else
            {
                var emptyList = new List<tblPosts>().ToPagedList(1, pageSize);
                return View(emptyList);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
