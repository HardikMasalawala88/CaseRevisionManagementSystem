using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.ServiceResponse
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; }

        public T Result { get; set; }
    }
}
