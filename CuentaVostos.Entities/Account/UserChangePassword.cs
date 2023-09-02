using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Account
{
    public class UserChangePassword
    {
        [Required(ErrorMessage ="Campo obligatorio")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage ="Campo obligatorio")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage ="Campo obligatorio")]
        [Compare("NewPassword", ErrorMessage ="La confirmación debe coincidir con la nueva contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
