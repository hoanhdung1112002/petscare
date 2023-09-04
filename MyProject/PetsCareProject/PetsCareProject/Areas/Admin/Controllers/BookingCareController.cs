using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PetsCareProject.Models;
using X.PagedList;
using MailKit.Net.Smtp;
using System.Numerics;
using System.Text;

namespace PetsCareProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingCareController : Controller
    {
        private readonly DbPetsCare _dataContext;

        public BookingCareController(DbPetsCare dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var dlList = (from b in _dataContext.tblBookingCares
                          join s in _dataContext.tblServices
                          on b.ServiceID equals s.ServiceID
                          join t in _dataContext.tblSchedules
                          on b.ScheduleID equals t.ScheduleID
                          where (b.IsActive == true)
                          orderby b.BookingCareID descending
                          select new ViewBookingCare()
                          {
                              BookingCareID = b.BookingCareID,
                              FullName = b.FullName,
                              PhoneNumber = b.PhoneNumber,
                              Email = b.Email,
                              Time = b.Time,
                              TimeSlot = t.TimeSlot,
                              Image = b.Image,
                              ServiceName = s.ServiceName,
                              PetName = b.PetName,
                              Description = b.Description,
                              StatusBooking = b.StatusBooking,
                          }).ToList();
            return View(dlList);
        }

        //Cập nhật lại trạng thái
        public IActionResult Accept(int id)
        {
            var booking = _dataContext.tblBookingCares.FirstOrDefault(b => b.BookingCareID == id);
            if (booking != null)
            {
                booking.StatusBooking = true;
                _dataContext.SaveChanges();

                //send email SMTP
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com");
                    client.Authenticate("petscareshop248@gmail.com", "mnhcfutdmupqulkf");


                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"<p>Mã: {booking.BookingCareID}</p> <p>Họ tên: {booking.FullName}</p> <p>Số điện thoai: {booking.PhoneNumber}</p> <p>Email: {booking.Email}</p> <p>Giống vật nuôi: {booking.PetName}</p></p> <p>Tình trạng: {booking.Description}</p>",
                        TextBody = "{booking.BookingCareID} \r\n {booking.FullName} \r\n {booking.PhoneNumber} \r\n {booking.Email} \r\n {booking.PetName} \r\n {booking.BookTime} \r\n {booking.Description} \r\n"
                    };

                    var message = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("PetsCare Shop", "petscareshop248@gmail.com"));
                    message.To.Add(new MailboxAddress("Gmail", booking.Email));
                    message.Subject = "Thông tin đăng ký khám bệnh cho thú cưng!";
                    client.Send(message);

                    client.Disconnect(true);
                }
            }

            return RedirectToAction("AcceptBooking");
        }


        //View Chi tiết lịch hẹn đã được duyệt
        public IActionResult AcceptBooking()
        {
            var dlList = (from b in _dataContext.tblBookingCares
                          join s in _dataContext.tblServices
                          on b.ServiceID equals s.ServiceID
                          join t in _dataContext.tblSchedules
                          on b.ScheduleID equals t.ScheduleID
                          where (b.IsActive == true && b.StatusBooking == true)
                          orderby b.BookingCareID descending
                          select new ViewBookingCare()
                          {
                              BookingCareID = b.BookingCareID,
                              FullName = b.FullName,
                              PhoneNumber = b.PhoneNumber,
                              Email = b.Email,
                              Time = b.Time,
                              TimeSlot = t.TimeSlot,
                              Image = b.Image,
                              ServiceName = s.ServiceName,
                              PetName = b.PetName,
                              Description = b.Description,
                              StatusBooking = b.StatusBooking,
                          }).ToList();
            return View(dlList);
        }

        //Lịch khám chưa được duyệt hoặc là không cho phép duyệt
        public IActionResult NoAcceptBooking()
        {
            var dlList = (from b in _dataContext.tblBookingCares
                          join s in _dataContext.tblServices
                          on b.ServiceID equals s.ServiceID
                          join t in _dataContext.tblSchedules
                          on b.ScheduleID equals t.ScheduleID
                          where (b.IsActive == true && b.StatusBooking == false)
                          orderby b.BookingCareID descending
                          select new ViewBookingCare()
                          {
                              BookingCareID = b.BookingCareID,
                              FullName = b.FullName,
                              PhoneNumber = b.PhoneNumber,
                              Email = b.Email,
                              Time = b.Time,
                              TimeSlot = t.TimeSlot,
                              Image = b.Image,
                              ServiceName = s.ServiceName,
                              PetName = b.PetName,
                              Description = b.Description,
                              StatusBooking = b.StatusBooking,
                          }).ToList();
            return View(dlList);
        }


        public IActionResult BookDetails(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _dataContext.tblBookingCares
                .FirstOrDefault(b => (b.BookingCareID == id) && (b.IsActive == true));
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //View Lịch đã được duyệt

        

      
        //Action Sưa Đặt lịch khám bệnh
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fd = _dataContext.tblBookingCares.Find(id);
            if (fd == null)
            {
                return NotFound();
            }
            var sList = (from s in _dataContext.tblServices
                         where (s.IsActive == true)
                         orderby s.ServiceID ascending
                         select new SelectListItem()
                         {
                             Text = s.ServiceName,
                             Value = s.ServiceID.ToString(),
                         }).ToList();
            sList.Insert(0, new SelectListItem()
            {
                Text = "Chọn dịch vụ cần đặt lịch",
                Value = "0"
            });
            ViewBag.listService = sList;
            //
            var listofDoctor = (from d in _dataContext.tblDoctors
                                where (d.IsActive == true)
                                orderby d.DoctorID ascending
                                select new SelectListItem()
                                {
                                    Text = d.DoctorName,
                                    Value = d.DoctorID.ToString(),
                                }).ToList();
            listofDoctor.Insert(0, new SelectListItem()
            {
                Text = "Chọn bác sĩ khám",
                Value = "0"
            });
            ViewBag.listDoctor = listofDoctor;

            var tList = (from t in _dataContext.tblSchedules
                         where (t.IsBooked == false)
                         orderby t.ScheduleID ascending
                         select new SelectListItem()
                         {
                             Text = t.TimeSlot,
                             Value = t.ScheduleID.ToString(),
                         }).ToList();
            tList.Insert(0, new SelectListItem()
            {
                Text = "Chọn khung giờ khám",
                Value = "0"
            });
            ViewBag.listSchedule = tList;

            return View(fd);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Action thêm booking vào CSDL
        public IActionResult Edit(tblBookingCare b)
        {
            if (ModelState.IsValid)
            {
                _dataContext.tblBookingCares.Update(b);
                _dataContext.SaveChanges();
                TempData["AlertMessage"] = "Cập nhật thành công!!!";
                return RedirectToAction("Index");
            }
            return View(b);
        }

        //Actionn xóa
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var dl = _dataContext.tblBookingCares.Find(id);

            if (dl == null)
            {
                return NotFound();
            }
            return View(dl);
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var del = _dataContext.tblBookingCares.Find(id);
            if (del == null)
            {
                return NotFound();
            }
            _dataContext.tblBookingCares.Remove(del);
            _dataContext.SaveChanges();
            TempData["AlertMessage"] = "Xoá thành công!!!";
            return RedirectToAction("Index");
        }
        //Action Cập nhật trạng thái cho lịch hẹn
       



    }
}

