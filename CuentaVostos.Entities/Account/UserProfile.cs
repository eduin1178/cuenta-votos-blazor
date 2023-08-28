using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Account
{
    public class UserProfile
    {
        public int Id { get; set; }
        public Guid Codigo { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int StateId { get; set; }
        public string State
        {
            get
            {
                string state = "Inactivo";
                switch (StateId)
                {
                    case 0:
                        state = "Inactivo";
                        break;

                    case 1:
                        state = "Activo";
                        break;

                    default:
                        state = "Activo";
                        break;
                }

                return state;
            }
        }

        public int RoleId { get; set; }
        public string Role
        {
            get
            {
                string role = "Usuario";
                switch (RoleId)
                {
                    case 0:
                        role = "Admin";
                        break;
                    case 1:
                        role = "User";
                        break;
                    default:
                        role = "User";
                        break;
                }

                return role;
            }
        }
        public DateTime Created { get; set; }
    }
}
