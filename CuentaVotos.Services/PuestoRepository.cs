using CuentaVotos.Entities.Puestos;
using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Shared;
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

                var queryMesas = _context.Mesas.AsEnumerable()
                        .Select(y => new MesaModel
                        {
                            Id = y.Id,
                            Code = y.Code,
                            Name = y.Name,
                            Number = y.Number,
                            PuestoId = y.PuestoId,
                            UserId = y.UserId,
                        }).ToList();

                var query = _context.Puestos.AsEnumerable()
                    .Select(x => new PuestoModel
                    {
                        Id = x.Id,
                        Number = x.Number,
                        Code = x.Code,
                        Name = x.Name,
                        ListaMesas = queryMesas.Where(y=>y.PuestoId == x.Id).ToList(),
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
                        UserFirstName = _context.Users.FirstOrDefault(y => y.Id == x.UserId)?.FirstName,
                        UserLastName = _context.Users.FirstOrDefault(y => y.Id == x.UserId)?.LastName,
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

        public ModelResult<string> Create(PuestoCreate model)
        {
            var result = new ModelResult<string>();

            var entity = new Puesto
            {
                Code = Guid.NewGuid().ToString(),
                Number = model.Number,
                Name = model.Name
            };

            try
            {
                _context.Puestos.Add(entity);


                for (int i = 0; i < model.TableCount; i++)
                {
                    var mesa = new Mesa
                    {
                        Name = $"Mesa {i + 1}",
                        Number = i + 1,
                        PuestoId = entity.Id,
                        Code = Guid.NewGuid().ToString(),
                    };

                    _context.Mesas.Add(mesa);
                }

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
        public ModelResult<string> Update(int puestoId, Puesto model)
        {
            var result = new ModelResult<string>();

            try
            {

                var entity = _context.Puestos.FirstOrDefault(x => x.Id == puestoId);
                if (entity == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Puesto no encontrado";

                    return result;
                }

                entity.Number = model.Number;
                entity.Name = model.Name;

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
        public ModelResult<string> Delete(int puestoId)
        {
            var result = new ModelResult<string>();

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

        public ModelResult<PuestoModel> AddTable(int puestoId, Mesa model)
        {
            var puesto = _context.Puestos.FirstOrDefault(x => x.Id == puestoId);
            if (puesto == null)
            {
                return new ModelResult<PuestoModel>
                {
                    IsSuccess = false,
                    Message = "Puesto no encontrado",
                };
            }

            var result = new ModelResult<PuestoModel>();

            try
            {
                var entity = new Mesa
                {
                    PuestoId = puestoId,
                    Number = model.Number,
                    Name = model.Name,
                    Code = Guid.NewGuid().ToString(),
                    UserId = model.UserId
                };

                _context.Mesas.Add(entity);

                result.IsSuccess = true;
                result.Message = "Mesa agregada correctamente";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al agregar la mesa";
                result.Exception = ex;
            }

            return result;

        }

        public ModelResult<PuestoModel> RemoveTable(int puestoId, int mesaId)
        {
            var entity = _context.Mesas.FirstOrDefault(x => x.Id == mesaId);
            if (entity == null)
            {
                return new ModelResult<PuestoModel>
                {
                    IsSuccess = false,
                    Message = "Mesa no encontrada",
                };
            }

            var result = new ModelResult<PuestoModel>();

            try
            {
                _context.Mesas.Delete(mesaId);

                result.IsSuccess = true;
                result.Message = "Mesa eliminada correctamente";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al eliminar la mesa";
                result.Exception = ex;
            }

            return result;
        }
    }
}
