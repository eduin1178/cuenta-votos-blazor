using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Services
{
    public class ProcesoRespository : IProcesoRespository
    {
        private readonly LiteDbContext _context;

        public ProcesoRespository(LiteDbContext context)
        {
            _context = context;
        }

        public ModelResult<Proceso> GetProceso(int idProceso)
        {
            var res = new ModelResult<Proceso>();

            try
            {
                var query = _context.Procesos.Where(x => x.Id == idProceso).ToList();

                res.IsSuccess = true;
                res.Message = "OK";
                res.Model = query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Error ";
                res.Exception = ex;
            }

            return res;
        }

        public ModelResult<object> GuardarProceso(Proceso model)
        {
            var res = new ModelResult<object>();
            try
            {

                var proceso = _context.Procesos.FirstOrDefault(x => x.Id == model.Id);
                if (proceso == null)
                {
                    proceso = new Proceso
                    {
                        Code = Guid.NewGuid().ToString(),
                        Inicia = model.Inicia,
                        Termina = model.Termina,
                    };

                    _context.Procesos.Add(proceso);
                }
                else
                {
                    proceso.Inicia = model.Inicia;
                    proceso.Termina = model.Termina;
                    _context.Procesos.Update(proceso);
                }
                res.IsSuccess = true;
                res.Message = "Proceso guardado correctamente";
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Error al guardar el proceso";
                res.Exception = ex;
            }


            return res;
        }
    }
}
