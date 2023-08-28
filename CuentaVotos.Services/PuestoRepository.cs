using CuentaVostos.Entities.Puestos;
using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entiies.Shared;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CuentaVotos.Services
{
    public class PuestoRepository : IPuestoRepository
    {
        private readonly LiteDbContext _context;

        public PuestoRepository(LiteDbContext context)
        {
            _context = context;
        }

        public ModelResult<List<PuestoModel>> List()
        {
            var res = new ModelResult<List<PuestoModel>>();

            try
            {
                var query = _context.Puestos.AsEnumerable()
                    .Select(x => new PuestoModel
                    {
                        Id = x.Id,
                        Number = x.Number,
                        Code = x.Code,
                        Name = x.Name,
                    }).ToList();

                res.IsSuccess = true;
                res.Message = "OK";
                res.Count = query.Count;
                res.Model = query;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Error al cargar la lista de puestos de votación";
                res.Exception = ex;
            }

            return res;
        }
        public ModelResult<PuestoModel> One(int puestoId)
        {
            var res = new ModelResult<PuestoModel>();

            try
            {

                var mesas = _context.Mesas.Where(x => x.PuestoId == puestoId)
                    .Select(x => new MesaModel
                    {
                        PuestoId = x.PuestoId,
                        Code = x.Code,
                        Id = x.Id,
                        Name = x.Name,
                        Number = x.Number,
                        UserId = x.UserId,
                        UserFirstName = _context.Users.FirstOrDefault(y => y.Id == x.UserId).FirstName,
                        UserLastName = _context.Users.FirstOrDefault(y => y.Id == x.UserId).LastName,
                    }).ToList();

                var query = _context.Puestos.Where(x => x.Id == puestoId).ToList()
                    .Select(x => new PuestoModel
                    {
                        Id = x.Id,
                        Number = x.Number,
                        Code = x.Code,
                        Name = x.Name,
                        ListaMesas = mesas
                    }).ToList();


                res.IsSuccess = true;
                res.Message = "OK";
                res.Count = query.Count;
                res.Model = query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Error al cargar los datos del puestos de votación";
                res.Exception = ex;
            }

            return res;
        }

        public ResultBase Create(int number, string name)
        {
            var result = new ResultBase();

            var entity = new Puesto
            {
                Number = number,
                Name = name
            };

            try
            {
                _context.Puestos.Add(entity);
                result.IsSuccess = true;
                result.Message = "Puesto creado correctamente";
                result.Code = entity.Id;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al crear el puesto";
                result.Exception = ex;
            }

            return result;
        }
        public ResultBase Update(int id, int number, string name)
        {
            var result = new ResultBase();

            try
            {

                var entity = _context.Puestos.FirstOrDefault(x=>x.Id == id);
                if (entity == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Puesto no encontrado";

                    return result;
                }

                entity.Number = number;
                entity.Name = name;

                _context.Puestos.Update(entity);

                result.IsSuccess = true;
                result.Message = "Datos del puesto de votación modificados correctamente";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al modificar los datos el puesto de votación";
                result.Exception = ex;
            }

            return result;
        }
        public ResultBase Delete(int puestoId)
        {
            var result = new ResultBase();

            try
            {
                var count = _context.Mesas.DeleteByExp(x => x.PuestoId == puestoId);
                var res = _context.Puestos.Delete(puestoId);
                if (res)
                {
                    result.IsSuccess = true;
                    result.Message = "Puesto eliminado correctamente";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Error al eliminar el puesto";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al eliminar el puesto";
                result.Exception = ex;
            }

            return result;
        }


    }
}
