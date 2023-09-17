using CuentaVotos.Data.LiteDb;
using CuentaVotos.Entities.Procesos;
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
    public class CargosRepository : ICargosRepository
    {
        private readonly LiteDbContext _context;

        public CargosRepository(LiteDbContext context)
        {
            _context = context;
        }

        public ModelResult<List<Cargo>> Lista()
        {
            try
            {

                var model = _context.Cargos.AsEnumerable()
                    .ToList();

                return new ModelResult<List<Cargo>>
                {
                    IsSuccess = true,
                    Message = "OK",
                    Model = model,
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<List<Cargo>>
                {
                    IsSuccess = false,
                    Message = "Error al obtener la lista de Cargos",
                    Exception = ex
                };
            }

        }

        public ModelResult<object> Crear(Cargo model)
        {
            var entity = new Cargo
            {                
                Nombre = model.Nombre,
            };

            try
            {
                _context.Cargos.Add(entity);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Cargo creado correctamente",
                    Code = entity.Id,
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al crear el Cargo",
                    Exception = ex
                };
            }
        }

        public ModelResult<object> Actualizar(int idCargo, Cargo model)
        {
            var entity = _context.Cargos.FirstOrDefault(x=>x.Id == idCargo);
            if (entity == null)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Cargo no encontrado",
                };
            }

            entity.Nombre = model.Nombre;

            try
            {
                _context.Cargos.Update(entity);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Cargo actualizado correctamente",
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al actualizar el Cargo",
                    Exception = ex
                };
            }
        }

        public ModelResult<object> Eliminar(int idCargo)
        {
            var entity = _context.Cargos.FirstOrDefault(x => x.Id == idCargo);
            if (entity == null)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Cargo no encontrado",
                };
            }

            var candidatos = _context.Candidatos.Where(x=>x.CargoId == idCargo).Count();
            if (candidatos>0)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "No se puede eliminar el cargo porque tiene candidatos asociados"
                };
            }

            try
            {
                _context.Cargos.Delete(idCargo);
                return new ModelResult<object>
                {
                    IsSuccess = true,
                    Message = "Cargo eliminado correctamente",
                };
            }
            catch (Exception ex)
            {
                return new ModelResult<object>
                {
                    IsSuccess = false,
                    Message = "Error al eliminar el Cargo",
                    Exception = ex
                };
            }
        }


    }
}
