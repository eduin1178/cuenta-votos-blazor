using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentaVotos.Entiies.Shared
{

    public class ResultBase
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public int Code { get; set; }
        public int Count { get; set; }
        public Exception? Exception { get; set; }
    }
    public class ModelResult<TModel> : ResultBase
    {
        public TModel? Model { get; set; }
    }
}
