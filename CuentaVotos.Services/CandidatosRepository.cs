using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Procesos;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Services
{
    public class CandidatosRepository : ICandidatosRepository
    {
        private readonly LiteDbContext _context;

        public CandidatosRepository(LiteDbContext context)
        {
            _context = context;
        }

        public ModelResult<List<CandidatoModel>> Lista(int idCargo)
        {
            var result = new ModelResult<List<CandidatoModel>>();

            try
            {
                var partidos = _context.Partidos.AsEnumerable().ToList();
                var cargos = _context.Cargos.AsEnumerable().ToList();
                var query = _context.Candidatos
                    .Where(x=>x.CargoId == idCargo)
                    .Select(x=> new CandidatoModel
                    {
                        Id = x.Id,
                        CargoId = x.CargoId,
                        PartidoId = x.PartidoId,
                        Nombre = x.Nombre,
                        Numero = x.Numero,
                        FotoUrl = x.FotoUrl,
                        Partido = partidos.FirstOrDefault(y=>y.Id == x.PartidoId)?.Nombre,
                        LogoPartido = partidos.FirstOrDefault(y => y.Id == x.PartidoId)?.LogoUrl,
                        ColorPartido = partidos.FirstOrDefault(y => y.Id == x.PartidoId)?.Color,
                        Cargo = partidos.FirstOrDefault(y => y.Id == x.PartidoId)?.Nombre,
                    })
                    .ToList();

                result.IsSuccess = true;
                result.Message = "OK";
                result.Model = query;
                result.Count = query.Count();
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al cargar la lista de candidatos";
                result.Exception = ex;
            }

            return result;
        }

        public ModelResult<object> Crear(Candidato candidato)
        {
            try
            {
                _context.Candidatos.Add(candidato);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Candidato creado correctamente",
                    Model = candidato,
                    Count = 1,
                    Code = candidato.Id,
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al crear el candidato",
                    Exception = ex,
                };
            }
        }
        public ModelResult<object> Actualizar(int idCandidato, Candidato candidato)
        {
            var entity = _context.Candidatos.FirstOrDefault(x=>x.Id == idCandidato);
            if (entity == null)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Candidato no encontrado"
                };
            }
            entity.PartidoId = candidato.PartidoId;
            entity.Nombre = candidato.Nombre;
            entity.Numero = candidato.Numero;
            entity.FotoUrl = candidato.FotoUrl;

            try
            {
                _context.Candidatos.Update(entity);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Candidato actualizado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al actualizar el candidato",
                    Exception = ex,
                };
            }
        }
        public ModelResult<object> Eliminar(int idCandidato)
        {
            try
            {
                _context.Candidatos.Delete(idCandidato);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Candidato eliminado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess  = false,
                    Message = "Error al eliminar el candidato",
                    Exception = ex,
                };
            }
        }
    }
}
