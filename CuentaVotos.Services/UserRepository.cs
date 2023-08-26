using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entiies.Account;
using CuentaVotos.Entiies.Shared;
using CuentaVotos.Repository;
using CuentaVotos.Sqlite;
using LiteDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly LiteDbContext _context;

        public UserRepository(LiteDbContext context)
        {
            _context = context;
        }


        public ModelResult<User> Login(string email, string password)
        {

            var user = _context.Users?.Find(x => x.Email == email
                 && x.PasswordHash == password).FirstOrDefault();

            if (user == null)
            {
                return new ModelResult<User>
                {
                    IsSuccess = false,
                    Message = "Usuario o clave no válidos"
                };
            }

            return new ModelResult<User>
            {
                IsSuccess = true,
                Message = "OK",
                Model = user
            };
        }

        public ResultBase Create(User user)
        {
            var result = new ResultBase();

            try
            {
                _context.Users.Insert(user);
                result.IsSuccess = true;
                result.Message = "OK";
                result.Code = user.Id;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                result.Exception = ex;
            }

            return result;
        }

        public ModelResult<List<User>> List()
        {
            var result = new ModelResult<List<User>>();

            try
            {
                var query = _context.Users.FindAll().ToList();
                result.IsSuccess = true;
                result.Message = "OK";
                result.Count = query.Count;
                result.Model = query;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar los datos";
                result.Exception = ex;
            }
            return result;
        }
    }
}
