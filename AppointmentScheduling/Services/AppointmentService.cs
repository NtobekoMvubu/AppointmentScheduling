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
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return doctors;
        }

        public async Task<int> AddUpdate(AppointmentVM model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));

            if (model != null && model.Id > 0)
            {
                //update}
                return 1;
            } 
            else
            {
                //create
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    isDoctApproved = false,
                    AdminId = model.AdminId
                };
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public List<PatientVM> GetPatientList()
        {
            var patients = (from user in _db.Users
                           join useroles in _db.UserRoles on user.Id equals useroles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on useroles.RoleId equals roles.Id
                           select new PatientVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                          ).ToList();

            return patients;
        }

        public List<AppointmentVM> DoctorsEventById(string doctorId)
        {
            return _db.Appointments.Where(x => x.DoctorId == doctorId).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                isDoctApproved = c.isDoctApproved

            }).ToList();
        }

        public List<AppointmentVM> PatientsEventById(string patientId)
        {
            return _db.Appointments.Where(x => x.PatientId == patientId).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                isDoctApproved = c.isDoctApproved

            }).ToList();
        }

        public AppointmentVM GetById(int id)
        {
            return _db.Appointments.Where(x => x.Id == id).ToList().Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                isDoctApproved = c.isDoctApproved,
                PatientId = c.PatientId,
                DoctorId = c.DoctorId,
                PatientName = _db.Users.Where(x => x.Id == c.PatientId).Select(x => x.Name).FirstOrDefault(),
                DoctorName = _db.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault(),

            }).SingleOrDefault();
        }
    }
}
