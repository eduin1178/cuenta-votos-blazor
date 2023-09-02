using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Account
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Campo oblgiatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Campo oblgiatorio")]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}
