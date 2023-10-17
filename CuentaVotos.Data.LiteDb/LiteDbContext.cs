using CuentaVotos.Entities.Puestos;
using CuentaVotos.Entities.Account;
using CuentaVotos.Entities.Procesos;
using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities;

namespace CuentaVotos.Data.LiteDb
{
    public class LiteDbContext : ALiteDbContext
    {

        public LiteDbContext(string connectionString) :base(connectionString)
        {
            Users = new LiteDbSet<User>(InternalDatabase);
            Users.ConfigureIndices(x=>x.Id, true);
            Users.ConfigureIndices(x=>x.Email, true);
            Users.ConfigureIndices(x=>x.Codigo, true);

            Puestos = new LiteDbSet<Puesto>(InternalDatabase);
            Puestos.ConfigureIndices(x=>x.Id, true);
            Puestos.ConfigureIndices(x=>x.Code, true);


            Mesas = new LiteDbSet<Mesa>(InternalDatabase);
            Mesas.ConfigureIndices(x => x.Id, true);
            Mesas.ConfigureIndices(x => x.Code, true);
            Mesas.ConfigureIndices(x => x.PuestoId, false);
            Mesas.ConfigureIndices(x => x.UserId, false);

            Cargos = new LiteDbSet<Cargo>(InternalDatabase);
            Cargos.ConfigureIndices(x => x.Id, true);

            Partidos = new LiteDbSet<Partido>(InternalDatabase);
            Partidos.ConfigureIndices(x => x.Id, true);

            Candidatos = new LiteDbSet<Candidato>(InternalDatabase);
            Candidatos.ConfigureIndices(x => x.Id, true);
            Candidatos.ConfigureIndices(x => x.PartidoId, false);
            Candidatos.ConfigureIndices(x => x.CargoId, false);

            Resultados = new LiteDbSet<Resultado>(InternalDatabase);
            Resultados.ConfigureIndices(x=>x.Id, true);
            Resultados.ConfigureIndices(x=>x.Code, true);
            Resultados.ConfigureIndices("UX_Resultados", x=> new {x.IdPuesto, x.IdMesa, x.IdPartido, x.IdCargo }, true);

            Procesos = new LiteDbSet<Proceso>(InternalDatabase);
            Procesos.ConfigureIndices(x=>x.Id, true);
            Procesos.ConfigureIndices(x=>x.Code, true);

        }

        public readonly LiteDbSet<Proceso> Procesos;
        public readonly LiteDbSet<User> Users;
        public readonly LiteDbSet<Puesto> Puestos;
        public readonly LiteDbSet<Mesa> Mesas;
        public readonly LiteDbSet<Cargo> Cargos;
        public readonly LiteDbSet<Partido> Partidos;
        public readonly LiteDbSet<Candidato> Candidatos;
        public readonly LiteDbSet<Resultado> Resultados;

    }
}
