using CuentaVostos.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Account
{
    public class UserModel
    {
        public int Id { get; set; }
        public Guid Codigo { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int StateId { get; set; }
        public string? State
        {
            get
            {
                return UserStateList.UserStates.FirstOrDefault(x=>x.Id == StateId)?.Name;
            }
        }

        public int RoleId { get; set; }
        public string? Role
        {
            get
            {
               return RoleList.Roles.FirstOrDefault(x=>x.Id == RoleId)?.Name;
            }
        }
        public DateTime Created { get; set; }
    }
}
