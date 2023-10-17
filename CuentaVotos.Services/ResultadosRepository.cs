using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Services
{
    public class ResultadosRepository : IResultadosRepository
    {
        private readonly LiteDbContext _context;

        public ResultadosRepository(LiteDbContext context)
        {
            _context = context;
        }
        public ModelResult<List<ResultadoModel>> ListaRegistro(int idPuesto, int idMesa, int idCargo)
        {
            var candidatos = _context.Candidatos
                .Where(x => x.CargoId == idCargo)
                .ToList();

            var partidos = _context.Partidos.AsEnumerable()
                .Where(x => candidatos.Any(y => y.PartidoId == x.Id))
                .ToList();

            var resultados = _context.Resultados
                .Where(x => x.IdPuesto == idPuesto && x.IdMesa == idMesa && x.IdCargo == idCargo)
                .ToList();

            var model = new List<ResultadoModel>();

            foreach (var partido in partidos)
            {
                var item = new ResultadoModel
                {
                    IdPartido = partido.Id,
                    Code = resultados.Where(x => x.IdPartido == partido.Id).FirstOrDefault()?.Code,
                    NombrePartido = partido.Nombre,
                    LogoPartido = partido.LogoUrl,
                    ColorPartido = partido.Color,
                    VotosPartido = Convert.ToInt32(resultados.Where(x => x.IdPartido == partido.Id).Sum(x => x.VotosPartido)),
                    Detalles = new List<DetallesResultadoModel>(),
                };

                foreach (var candidato in candidatos.Where(x => x.PartidoId == partido.Id))
                {
                    item.Detalles.Add(new DetallesResultadoModel
                    {
                        IdCantidato = candidato.Id,
                        VotosCandidato = resultados.FirstOrDefault(x => x.IdPartido == partido.Id)?.Detalles.FirstOrDefault(x => x.IdCantidato == candidato.Id)?.VotosCandidato ?? 0,
                        NombreCandidato = candidato.Nombre,
                        Numero = candidato.Numero,
                        FotoCandidatoUrl = candidato.FotoUrl,
                    });
                }

                model.Add(item);
            }
            return new ModelResult<List<ResultadoModel>>
            {
                IsSuccess = true,
                Message = "OK",
                Model = model.OrderBy(x=>x.Detalles.Max(x=>x.Numero)).ToList(),
                Count = model.Count,
                Code = 1,
            };
        }
        public ModelResult<string> Guardar(string userCode, int idPuesto, int idMesa, int idCargo, List<ResultadoModel> resultados)
        {
            var usuario = _context.Users.FirstOrDefault(x => x.Codigo == Guid.Parse(userCode));
            if (usuario == null)
                return new ModelResult<string>
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };

            var entities = _context.Resultados
                .Where(x => x.IdPuesto == idPuesto && x.IdMesa == idMesa && x.IdCargo == idCargo).ToList();

            foreach (var resultado in resultados)
            {


                var entity = entities.FirstOrDefault(x => x.IdPartido == resultado.IdPartido);
                if (entity == null)
                {
                    var newEntity = new Resultado
                    {
                        IdCargo = idCargo,
                        IdPuesto = idPuesto,
                        IdMesa = idMesa,
                        IdPartido = resultado.IdPartido,
                        Code = Guid.NewGuid(),
                        Confirmado = false,
                        Registrado = DateTime.Now,
                        IdUsuarioRegistro = usuario.Id,
                        VotosPartido = resultado.VotosPartido,
                        Detalles = resultado.Detalles.Select(x => new DetallesResultado
                        {
                            IdCantidato = x.IdCantidato,
                            VotosCandidato = x.VotosCandidato,
                        }).ToList(),
                    };

                    _context.Resultados.Add(newEntity);
                }
                else
                {
                    entity.Confirmado = false;
                    entity.Registrado = DateTime.Now;
                    entity.VotosPartido = resultado.VotosPartido;
                    entity.Detalles = resultado.Detalles.Select(x => new DetallesResultado
                    {
                        IdCantidato = x.IdCantidato,
                        VotosCandidato = x.VotosCandidato,
                    }).ToList();

                    _context.Resultados.Update(entity);
                }

            }

            return new ModelResult<string>
            {
                IsSuccess = true,
                Message = "Resultados registrados correctamente",
                Count = resultados.Count,
                Code = 1,
            };
        }

        public ModelResult<List<ResultadoGeneralModel>> Resultados(int idCargo, int idPuesto, int idMesa)
        {
            var res = new ModelResult<List<ResultadoGeneralModel>>();

            var candidatos = _context.Candidatos
               .Where(x => x.CargoId == idCargo)
               .ToList();

            var partidos = _context.Partidos.AsEnumerable()
                .Where(x => candidatos.Any(y => y.PartidoId == x.Id))
                .ToList();

            var query = _context.Resultados
                .Where(x => x.IdCargo == idCargo
                && (x.IdPuesto == idPuesto || idPuesto == 0)
                && (x.IdMesa == idMesa || idMesa == 0))
                .GroupBy(x => new { x.IdCargo, x.IdPartido })
                .Select(x => new ResultadoGeneralModel
                {
                    IdCargo = x.Key.IdCargo,
                    IdPartido = x.Key.IdPartido,
                    NombrePartido = partidos.FirstOrDefault(y => y.Id == x.Key.IdPartido)?.Nombre,
                    LogoPartido = partidos.FirstOrDefault(y => y.Id == x.Key.IdPartido)?.LogoUrl,
                    Detalles = x.SelectMany(y => y.Detalles).ToList()
                    .GroupBy(g => g.IdCantidato)
                    .Select(x => new DetallesResultadoModel
                    {
                        IdCantidato = x.Key,
                        NombreCandidato = candidatos.FirstOrDefault(y => y.Id == x.Key)?.Nombre,
                        FotoCandidatoUrl = candidatos.FirstOrDefault(y => y.Id == x.Key)?.FotoUrl,
                        Numero = candidatos.FirstOrDefault(y => y.Id == x.Key)?.Numero,
                        VotosCandidato = x.Sum(x => x.VotosCandidato),
                    }).OrderByDescending(x => x.VotosCandidato).ToList(),
                })
                .ToList();


            var model = new List<ResultadoGeneralModel>();
            foreach (var partido in partidos)
            {
                var item = new ResultadoGeneralModel
                {
                    IdPartido = partido.Id,
                    NombrePartido = partido.Nombre,
                    LogoPartido = partido.LogoUrl,
                    ColorPartido = partido.Color,
                    Detalles = new List<DetallesResultadoModel>(),

                };

                foreach (var candidato in candidatos.Where(x => x.PartidoId == partido.Id))
                {
                    item.Detalles.Add(new DetallesResultadoModel
                    {
                        IdCantidato = candidato.Id,
                        VotosCandidato = query.FirstOrDefault(x => x.IdPartido == partido.Id)?
                        .Detalles.FirstOrDefault(x => x.IdCantidato == candidato.Id)?.VotosCandidato ?? 0,
                        NombreCandidato = candidato.Nombre,
                        Numero = candidato.Numero,
                        FotoCandidatoUrl = candidato.FotoUrl,
                    });
                }

                model.Add(item);
            }


            res.IsSuccess = true;
            res.Message = "OK";
            res.Count = model.Count;
            res.Model = model.OrderByDescending(x => x.VotosCandidato)
                .ThenBy(x => x.Detalles.Max(x => x.Numero))
                .ToList();
            return res;
        }

        public ModelResult<object> EliminarResultados(int idCargo, int idPuesto)
        {
            var res = new ModelResult<object>();

            try
            {
                _context.Resultados.DeleteByExp(x => x.IdCargo == idCargo && x.IdPuesto == idPuesto);
                res.IsSuccess = true;
                res.Message = "Resultados eliminados correctamente";
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Error al eliminar los resultados";
                res.Exception = ex;
            }

            return res;
        }

    }
}
