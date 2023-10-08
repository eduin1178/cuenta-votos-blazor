using CuentaVotos.Core.Shared;
using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Entities.Account;
using CuentaVotos.Repository;
using CuentaVotos.Entities.Puestos;

namespace CuentaVotos.Services
{
    public class UserRepository : IUserRespository
    {
        private readonly LiteDbContext _context;

        public UserRepository(LiteDbContext context)
        {
            _context = context;
        }
        public ModelResult<IEnumerable<UserModel>> List(int? roleId = null, int? stateId = null)
        {
            var result = new ModelResult<IEnumerable<UserModel>>();

            try
            {
                var query = _context.Users
                    .Where(x => (x.RoleId == roleId || roleId == null)
                     && (x.StateId == stateId || stateId == null))
                    .Select(x => new UserModel
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        PhoneNumber = x.PhoneNumber,
                        RoleId = x.RoleId,
                        StateId = x.StateId,
                        Created = x.Created,
                    }).ToList();

                result.IsSuccess = true;
                result.Model = query;
                result.Count = query.Count;
                result.Code = 1;
                result.Message = "OK";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar la lista de usuarois";
                result.Exception = ex;
            }

            return result;
        }
        public ModelResult<string> ChangeRol(int userId, int newRole)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (entity == null)
            {
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };
            }

            entity.RoleId = newRole;

            try
            {
                var res = _context.Users.Update(entity);
                if (res)
                {
                    return new ModelResult<string>
                    {
                        IsSuccess = true,
                        Message = "Rol del usuario modificado correctamente",
                    };
                }
                else
                {
                    return new ModelResult<string> { IsSuccess = false, Message = "Error al modificiar el rol del usuario" };
                }
            }
            catch (Exception ex)
            {

                return new ModelResult<string> { IsSuccess = false, Message = "Error al modificiar el rol del usuario", Exception = ex };
            }

        }
        public ModelResult<string> ChangeState(int userId, int newState)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (entity == null)
            {
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };
            }

            entity.StateId = newState;

            try
            {
                var res = _context.Users.Update(entity);
                if (res)
                {
                    return new ModelResult<string>
                    {
                        IsSuccess = true,
                        Message = "Estado del usuario modificado correctamente",
                    };
                }
                else
                {
                    return new ModelResult<string> { IsSuccess = false, Message = "Error al modificiar el estado del usuario" };
                }
            }
            catch (Exception ex)
            {

                return new ModelResult<string> { IsSuccess = false, Message = "Error al modificiar el estado del usuario", Exception = ex };
            }
        }
        public ModelResult<string> RestorePassword(UserRestorePassword model)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Id == model.UserId);
            if (entity == null)
            {
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };
            }

            entity.PasswordHash = model.NewPassword.MD5Encrypt();

            try
            {
                var res = _context.Users.Update(entity);
                if (res)
                {
                    return new ModelResult<string>
                    {
                        IsSuccess = true,
                        Message = "Contraseña del usuario restablecida correctamente",
                    };
                }
                else
                {
                    return new ModelResult<string> { IsSuccess = false, Message = "Error al restablecer la contraseña del usuario" };
                }
            }
            catch (Exception ex)
            {

                return new ModelResult<string> { IsSuccess = false, Message = "Error al restablecer la contraseña del usuario", Exception = ex };
            }

        }
        public ModelResult<string> Delete(int userId)
        {
            try
            {
                var res = _context.Users.Delete(userId);
                if (res)
                {
                    return new ModelResult<string> { IsSuccess = true, Message = "Usuario eliminado correctamente" };
                }
                else
                {
                    return new ModelResult<string> { IsSuccess = false, Message = "Error al elimimar el usuario " };
                }
            }
            catch (Exception ex)
            {
                return new ModelResult<string> { IsSuccess = false, Message = "Error al elimimar el usuario ", Exception = ex };
            }
        }

        public ModelResult<List<PuestoModel>> Puestos(string userCode)
        {

            var usuario = _context.Users.FirstOrDefault(x => x.Codigo == Guid.Parse(userCode));
            if (usuario == null)
                return new ModelResult<List<PuestoModel>>
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };

            try
            {
                var mesas = _context.Mesas
                        .Where(x => x.UserId == usuario.Id).ToList();

                var puestos = _context.Puestos.AsEnumerable();

                var model = puestos.Where(x => mesas.Any(y => y.PuestoId == x.Id))
                        .Select(x => new PuestoModel
                        {
                            Id = x.Id,
                            Code = x.Code,
                            Name = x.Name,
                            Number = x.Number,
                            ListaMesas = mesas.Where(y => y.PuestoId == x.Id)
                        .Select(mesa => new MesaModel
                        {
                            Id = mesa.Id,
                            Code = mesa.Code,
                            Number = mesa.Number,
                            Name = mesa.Name,
                        }).ToList(),

                        }).ToList();

                return new ModelResult<List<PuestoModel>>
                {
                    IsSuccess = true,
                    Message = "OK",
                    Model = model
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<List<PuestoModel>>
                {
                    IsSuccess = false,
                    Message = "Error al cargar la lista de puestos",
                    Model = null,
                    Exception = ex,
                };
            }

        }
    }
}
