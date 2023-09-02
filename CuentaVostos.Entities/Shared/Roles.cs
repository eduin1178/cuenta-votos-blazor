using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVostos.Entities.Shared
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    public static class RoleList
    {
        public static List<Role> Roles
        {
            get
            {
                return new List<Role>()
                {
                    new Role() { Id = 0, Name = "Admin" },
                    new Role() { Id = 1, Name = "Usuario" }
                };
            }
        }
    }
}
