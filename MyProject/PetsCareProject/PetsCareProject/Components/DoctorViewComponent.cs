using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetsCareProject.Models;

namespace PetsCareProject.Components
{
    [ViewComponent(Name = "DoctorView")]
    public class DoctorViewComponent : ViewComponent
    {
        private DbPetsCare _context;

        public DoctorViewComponent(DbPetsCare context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofDoctor = (from d in _context.tblDoctors
                                    join p in _context.tblPosition
                                    on d.PositionID equals p.PositionID
                                    where (d.IsActive == true)
                                    orderby d.DoctorID ascending
                                    select new ViewDoctors()
                                    {
                                        DoctorID = d.DoctorID,
                                        DoctorName = d.DoctorName,
                                        Thumb = d.Thumb,
                                        PositionID = p.PositionID,
                                        PositionName = p.PositionName,
                                        PhoneNumber = d.PhoneNumber,
                                        Email = d.Email,
                                        Address = d.Address,
                                        IsActive = d.IsActive

                                    }).Take(8).ToList();
            //m.Position == 1 là những menu nằm phía trên
            return await Task.FromResult((IViewComponentResult)View("Default", listofDoctor));

        }
    }
}
