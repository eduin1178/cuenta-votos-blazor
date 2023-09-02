using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Account
{
    public class UserCreate
    {
        [Required(ErrorMessage ="Campo obligatorio")]
        public string FirstName { get; set; } = null!;
     
        [Required(ErrorMessage = "Campo obligatorio")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Campo obligatorio")]
        public string PhoneNumber { get; set; } = null!;
    }
}
