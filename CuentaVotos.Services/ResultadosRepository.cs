using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Resultados;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        VotosCandidato = resultados.FirstOrDefault(x => x.IdPartido == partido.Id)?.Detalles.FirstOrDefault(x=>x.IdCantidato == candidato.Id)?.VotosCandidato?? 0,
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
                Model = model,
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

                var entity = entities.FirstOrDefault(x => x.IdPartido == resultado.IdPartido);
                if (entity != null)
                {
                    _context.Resultados.Delete(entity.Id);
                }
                _context.Resultados.Add(newEntity);
            }

            return new ModelResult<string>
            {
                IsSuccess = true,
                Message = "Resultados registrados correctamente",
                Count = resultados.Count,
                Code = 1,
            };
        }
      


        public ModelResult<List<ResultadoGeneralModel>> ResultadosGenales(int idCargo)
        {
            throw new NotImplementedException();
        }

        public ModelResult<List<ResultadoMesaModel>> ResultadosMesa(int idCargo, int idPuesto, int idMesa)
        {
            throw new NotImplementedException();
        }

        public ModelResult<List<ResultadoPuestoModel>> ResultadosPuesto(int idCargo, int idPuesto)
        {
            throw new NotImplementedException();
        }
    }
}
