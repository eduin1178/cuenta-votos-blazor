using Microsoft.AspNetCore.SignalR;

namespace Elecciones.Server.Hubs
{
    public class NotifyResultHub : Hub
    {
        public void SendMessage(string user, int idCargo, int idPuesto)
        {
            Clients.All.SendAsync("NotifyResult", user, idCargo, idPuesto);
        }
    }
}
