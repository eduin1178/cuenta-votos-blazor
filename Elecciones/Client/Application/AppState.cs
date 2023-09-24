using CuentaVotos.Entities.Account;
using CuentaVotos.Entities.Puestos;

namespace Elecciones.Client.Application
{
    public class AppState
    {
        PuestoModel _puesto;
        public PuestoModel Puesto
        {
            get
            {
                return _puesto;
            }
            set
            {
                _puesto = value;
            }
        }

       
        UserProfile _profile;
        public UserProfile Profile
        {
            get
            {
                return _profile;
            }
            set
            {
                _profile= value;
            }
        }
    }
}
