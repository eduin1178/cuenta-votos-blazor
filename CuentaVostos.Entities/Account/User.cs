using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entiies.Account
{
    public class User
    {
        public int Id { get; set; }
        public Guid Codigo { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Addres { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
