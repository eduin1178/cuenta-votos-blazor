using CuentaVotos.Core.Shared;
using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entiies.Shared;
using CuentaVotos.Entities.Account;
using CuentaVotos.Repository;

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
        public ResultBase ChangeRol(int userId, int newRole)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (entity == null)
            {
                return new ResultBase
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
                    return new ResultBase
                    {
                        IsSuccess = true,
                        Message = "Rol del usuario modificado correctamente",
                    };
                }
                else
                {
                    return new ResultBase { IsSuccess = false, Message = "Error al modificiar el rol del usuario" };
                }
            }
            catch (Exception ex)
            {

                return new ResultBase { IsSuccess = false, Message = "Error al modificiar el rol del usuario", Exception = ex };
            }

        }
        public ResultBase ChangeState(int userId, int newState)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (entity == null)
            {
                return new ResultBase
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
                    return new ResultBase
                    {
                        IsSuccess = true,
                        Message = "Estado del usuario modificado correctamente",
                    };
                }
                else
                {
                    return new ResultBase { IsSuccess = false, Message = "Error al modificiar el estado del usuario" };
                }
            }
            catch (Exception ex)
            {

                return new ResultBase { IsSuccess = false, Message = "Error al modificiar el estado del usuario", Exception = ex };
            }
        }
        public ResultBase RestorePassword(int userId, string newPassword)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (entity == null)
            {
                return new ResultBase
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };
            }

            entity.PasswordHash = newPassword.MD5Encrypt();

            try
            {
                var res = _context.Users.Update(entity);
                if (res)
                {
                    return new ResultBase
                    {
                        IsSuccess = true,
                        Message = "Contraseña del usuario restablecida correctamente",
                    };
                }
                else
                {
                    return new ResultBase { IsSuccess = false, Message = "Error al restablecer la contraseña del usuario" };
                }
            }
            catch (Exception ex)
            {

                return new ResultBase { IsSuccess = false, Message = "Error al restablecer la contraseña del usuario", Exception = ex };
            }

        }
        public ResultBase Delete(int userId)
        {
            try
            {
                var res = _context.Users.Delete(userId);
                if (res)
                {
                    return new ResultBase { IsSuccess = true, Message = "Usuario eliminado correctamente" };
                }
                else
                {
                    return new ResultBase { IsSuccess = false, Message = "Error al elimimar el usuario " };
                }
            }
            catch (Exception ex)
            {
                return new ResultBase { IsSuccess = false, Message = "Error al elimimar el usuario ", Exception = ex };
            }
        }


    }
}
