using AppointmentScheduling.Models;
using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<DoctorVM> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join useroles in _db.UserRoles on user.Id equals useroles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on useroles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return doctors;
        }



        public List<PatientVM> GetPatientList()
        {
            var patients = (from user in _db.Users
                           join useroles in _db.UserRoles on user.Id equals useroles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on useroles.RoleId equals roles.Id
                           select new PatientVM
                           {
                               id = user.Id,
                               Name = user.Name
                           }
                          ).ToList();

            return patients;
        }
    }
}
