using CuentaVostos.Entities.Shared;
using CuentaVotos.Entiies.Shared;
using CuentaVotos.Entities.Account;
using CuentaVotos.Repository;
using System.Reflection;
using System.Security.Principal;

namespace CuentaVotos.Core.Users
{
    public class UsersManager
    {
        private readonly IUserRespository _userRespository;
        public List<Role> Roles => RoleList.Roles;
        public List<UserState> States => UserStateList.UserStates;

        public UsersManager(IUserRespository userRespository)
        {
            _userRespository = userRespository;
            Load();
        }

        public IEnumerable<UserModel>? Users { get; set; }
        public int? RoleId { get; set; }
        public int? StateId { get; set; }
        public ModelResult<IEnumerable<UserModel>> Load()
        {
            var res = _userRespository.List(RoleId, StateId);
            if (res.IsSuccess)
            {
                Users = res.Model;
            }

            return res;
        }

        public ResultBase ChangeToAdmin(UserModel model)
        {
            var res =  _userRespository.ChangeRol(model.Id, 0);
            if (res.IsSuccess)
            {
                Load();
            }

            return res;
        }

        public ResultBase ChangeToUser(UserModel model)
        {
            var res = _userRespository.ChangeRol(model.Id, 1);
            if (res.IsSuccess)
            {
                Load();
            }

            return res;
        }

        public ResultBase Activate(UserModel model)
        {
            var res =  _userRespository.ChangeState(model.Id, 1);
            if (res.IsSuccess)
            {
                Load();
            }

            return res;
        }

        public ResultBase Deactivate(UserModel model)
        {
            var res = _userRespository.ChangeState(model.Id, 0);
            if (res.IsSuccess)
            {
                Load();
            }

            return res;
        }

        public ResultBase ResetPassword(UserModel model, string newPassword)
        {
            return _userRespository.RestorePassword(model.Id, newPassword);
        }

        public ResultBase Delete(UserModel model)
        {
            return _userRespository.Delete(model.Id);
        }
    }
}
