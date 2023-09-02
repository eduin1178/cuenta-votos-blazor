using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entities.Shared
{
      
    public class ModelResult<TModel>
    {
        public TModel? Model { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public int Code { get; set; }
        public int Count { get; set; }
        public Exception? Exception { get; set; }
    }
}
