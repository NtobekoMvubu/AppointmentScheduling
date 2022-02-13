using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="The {0} must be at least {2} characcters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "Pleasure make sure that both Password and Confirm Passwirds are matching") ]
        public string ConfirmPassword { get; set; }
        [Required]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
    }
}
