using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Data.FormModels
{
    public class Paginate<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
