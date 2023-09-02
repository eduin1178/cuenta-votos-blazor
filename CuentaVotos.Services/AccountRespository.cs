using CuentaVotos.Core.Shared;
using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Entities.Account;
using CuentaVotos.Repository;

namespace CuentaVotos.Services
{
    public class AccountRespository : IAccountRepository
    {
        private readonly LiteDbContext _context;

        public AccountRespository(LiteDbContext context)
        {
            _context = context;
        }

        public ModelResult<string> ChangePassword(string userCode, UserChangePassword userChangePassword)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Codigo == Guid.Parse(userCode));
            if (entity == null)
            {
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            if (entity.PasswordHash != userChangePassword.CurrentPassword.MD5Encrypt())
            {
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "La contraseña actual no es válida"
                };
            }

            try
            {
                entity.PasswordHash = userChangePassword.NewPassword.MD5Encrypt();
                var isSuccess = _context.Users.Update(entity);
                return new ModelResult<string>
                {
                    IsSuccess = isSuccess,
                    Message = isSuccess ? "Contraseña modificada correctamente" : "Error al cambiar la contraseña",
                    Code = 1,
                    Count = 1,
                };
            }
            catch (Exception ex)
            {

                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "Error al actualizar los datos",
                    Exception = ex
                };
            }

        }

        public ModelResult<UserProfile> Login(UserLogin userLogin)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Email == userLogin.Email);
            if (entity == null)
            {
                return new ModelResult<UserProfile>
                {
                    IsSuccess = false,
                    Message = "Usuario no existe"
                };
            }

            if (entity.StateId == 0)
            {
                return new ModelResult<UserProfile>
                {
                    IsSuccess = false,
                    Message = "Usuario inactivo, solicite la activación a un administrador"
                };
            }

            if (entity.PasswordHash != userLogin.Password.MD5Encrypt())
            {
                return new ModelResult<UserProfile>
                {
                    IsSuccess = false,
                    Message = "Contraseña no válida. Si olvidó su clave, solicite un restablecimiento al administrador"
                };
            }

            return Profile(entity.Codigo.ToString());
        }

        public ModelResult<UserProfile> Profile(string userCode)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Codigo == Guid.Parse(userCode));
            if (entity == null)
            {
                return new ModelResult<UserProfile>
                {
                    IsSuccess = false,
                    Message = $"Usuario no encontrado"
                };
            }

            try
            {

                return new ModelResult<UserProfile>
                {
                    IsSuccess = true,
                    Message = "OK",
                    Code = 1,
                    Count = 1,
                    Model = new UserProfile
                    {
                        Id = entity.Id,
                        Codigo = entity.Codigo,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Email = entity.Email,
                        PhoneNumber = entity.PhoneNumber,
                        StateId = entity.StateId,
                        RoleId = entity.RoleId,
                        Created = entity.Created,
                    },
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<UserProfile>
                {
                    IsSuccess = false,
                    Message = "Error al cargar los datos del usuario",
                    Code = 0,
                    Count = 0,
                    Exception = ex
                };
            }
        }

        public ModelResult<UserProfile> Register(UserCreate user)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Email == user.Email);
            if (entity != null)
            {
                return new ModelResult<UserProfile>
                {
                    IsSuccess = false,
                    Message = $"El email {user.Email} ya se encuentra registrado"
                };
            }

            try
            {
                entity = new User
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    PasswordHash = user.Password.MD5Encrypt(),
                };
                _context.Users.Add(entity);

                return new ModelResult<UserProfile>
                {
                    IsSuccess = true,
                    Message = "Usuario registrado correctamente",
                    Count = 1,
                    Model = new UserProfile
                    {
                        Id = entity.Id,
                        Codigo = entity.Codigo,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Created = entity.Created,
                    },
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<UserProfile>
                {
                    IsSuccess = false,
                    Message = "Error al registrar el usuario",
                    Code = 0,
                    Count = 0,
                    Exception = ex
                };
            }
        }

        public ModelResult<string> UpdateProfile(string userCode, UserProfile profile)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Codigo == Guid.Parse(userCode));
            if (entity == null)
            {
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = $"Usuario no encontrado"
                };
            }

            try
            {
                entity.FirstName = profile.FirstName;
                entity.LastName = profile.LastName;
                entity.Email = profile.Email;
                entity.PhoneNumber = profile.PhoneNumber ?? "";

                _context.Users.Update(entity);
                return new ModelResult<string>
                {
                    IsSuccess = true,
                    Message = "Datos del usuario actualizados correctamente",
                    Code = 1,
                    Count = 1,
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "Error al actualizar los datos del usuario",
                    Code = 0,
                    Count = 0,
                    Exception = ex
                };
            }
        }
    }
}
