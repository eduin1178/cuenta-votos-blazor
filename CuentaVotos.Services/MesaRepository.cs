using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Puestos;
using CuentaVotos.Entities.Shared;
using CuentaVotos.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Services
{
    public class MesaRepository : IMesaRepository
    {
        private readonly LiteDbContext _context;

        public MesaRepository(LiteDbContext context)
        {
            _context = context;
        }

        public ModelResult<object> Create(MesaCreate model)
        {
            var entity = new Mesa
            {
                PuestoId = model.PuestoId,
                Name = model.Name,
                Number = model.Number,
                UserId = model.UserId,
                Code = Guid.NewGuid().ToString(),
            };

            try
            {
                _context.Mesas.Add(entity);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Mesa creada correctamente",
                    Code = entity.Id,
                    Count = 1,
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al crear la mesa",
                    Exception = ex
                };
            }
        }

        public ModelResult<object> Delete(int mesaId)
        {
            try
            {
                 _context.Mesas.Delete(mesaId);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Mesa eliminada correctamente",
                };

            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al eliminar la mesa",
                    Exception = ex
                };
            }

        }

        public ModelResult<List<MesaModel>> List()
        {
            throw new NotImplementedException();
        }

        public ModelResult<MesaModel> One(int mesaId)
        {
            throw new NotImplementedException();
        }

        public ModelResult<object> Update(int mesaId, Mesa model)
        {
            var entity = _context.Mesas.FirstOrDefault(x=>x.Id == mesaId);
            if (entity== null)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Mesa no encontrada",
                };
            }

            entity.Name = model.Name;
            entity.Number = model.Number;
            entity.UserId = model.UserId;


            try
            {
                _context.Mesas.Update(entity);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Mesa actualizada correctamente",
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al actualizar la mesa",
                    Exception = ex
                };
            }

        }

     
    }
}
