namespace Elecciones.Client.Application
{
    public class FiltrosState
    {

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
        string _nombre;
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                NotifyStateChanged();
            }
        }
        string _apellidos;
        public string Apellidos
        {
            get
            {
                return _apellidos;
            }
            set
            {
                _apellidos = value;
                NotifyStateChanged();
            }
        }
    }
}
