using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVostos.Entities.Shared
{
    public class UserState
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public static class UserStateList
    {
        public static List<UserState> UserStates
        {
            get
            {
                return new List<UserState>() { 
                    new UserState
                    {
                        Id = 1,
                        Name = "Activo"
                    },
                    new UserState
                    {
                        Id = 0,
                        Name = "Inactivo"
                    }
                };
            }
        }
    }
}

