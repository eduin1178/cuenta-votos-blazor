using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Procesos;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;

namespace CuentaVotos.Services
{
    public class PartidosRespository : IPartidosRepository
    {
        private readonly LiteDbContext _context;

        public PartidosRespository(LiteDbContext context)
        {
            _context = context;
        }

        public ModelResult<List<Partido>> Lista()
        {
            try
            {
                var query = _context.Partidos.AsEnumerable().ToList();

                return new ModelResult<List<Partido>>
                {
                    IsSuccess = true,
                    Message = "OK",
                    Model = query,
                };

            }
            catch (Exception ex)
            {
                return new ModelResult<List<Partido>>
                {
                    IsSuccess = false,
                    Message = "Error al cargar la lista de partidos",
                    Exception = ex
                };
            }
        }

        public ModelResult<object> Crear(Partido partido)
        {

            try
            {
                _context.Partidos.Add(partido);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Partido creado correctamente",
                    Code = partido.Id,
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al crear el partido",
                    Exception = ex  
                };
            }
        }

        public ModelResult<object> Actualizar(int idPartido, Partido partido)
        {
            var entity = _context.Partidos.FirstOrDefault(x=> x.Id == idPartido);
            if (entity == null)
            {
                return new ModelResult<object>
                {
                    IsSuccess =     false,
                    Message = "Partido no encontrado"
                };
            }

            entity.Nombre = partido.Nombre;
            entity.LogoUrl = partido.LogoUrl;
            entity.Color = partido.Color;   

            try
            {
                _context.Partidos.Update(entity);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Partido actualizado correctamente",
                    Code = partido.Id,
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al actualizar el partido",
                    Exception = ex
                };
            }
        }

        public ModelResult<object> Eliminar(int idPartido)
        {
            var entity = _context.Partidos.FirstOrDefault(x => x.Id == idPartido);
            if (entity == null)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Partido no encontrado"
                };
            }


            var candidatos = _context.Candidatos.Where(x => x.CargoId == idPartido).Count();
            if (candidatos > 0)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "No se puede eliminar el partido porque tiene candidatos asociados"
                };
            }

            try
            {
                _context.Partidos.Delete(idPartido);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Partido eliminado correctamente",
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al eliminar el partido",
                    Exception = ex
                };
            }
        }

    }
}
