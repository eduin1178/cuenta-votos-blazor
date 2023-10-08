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

            var partidos = _context.Partidos
                .Where(x => candidatos.Any(y => y.Id == x.Id))
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
                    Code = resultados.Where(x => x.IdPartido == partido.Id).First().Code,
                    NombrePartido = partido.Nombre,
                    LogoPartido = partido.LogoUrl,
                    VotosPartido = resultados.Where(x => x.IdPartido == partido.Id).Sum(x => x.VotosPartido),
                    Detalles = new List<DetallesResultadoModel>(),
                    Confirmacion = resultados.Where(x => x.IdPartido == partido.Id).First().Confirmacion,
                    Confirmado = resultados.Where(x => x.IdPartido == partido.Id).First().Confirmado,
                    IdUsuarioRegistro = resultados.Where(x => x.IdPartido == partido.Id).First().IdUsuarioRegistro,
                    Registrado = resultados.Where(x => x.IdPartido == partido.Id).First().Registrado,
                };

                foreach (var candidato in candidatos)
                {
                    item.Detalles.Add(new DetallesResultadoModel
                    {
                        IdCantidato = candidato.Id,
                        VotosCandidato = resultados.FirstOrDefault(x => x.IdPartido == partido.Id
                                        && x.Detalles.Any(y => y.IdCantidato == candidato.Id))?.VotosPartido ?? 0,
                        NombreCandidato = candidato.Nombre,
                        Numero   = candidato.Numero,
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
        public ModelResult<string> Guardar(int idPuesto, int idMesa, int idCargo, List<ResultadoModel> resultados)
        {
            throw new NotImplementedException();
        }
        public ModelResult<string> Confirmar(int idPuesto, int idMesa, int idCargo)
        {
            throw new NotImplementedException();
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
