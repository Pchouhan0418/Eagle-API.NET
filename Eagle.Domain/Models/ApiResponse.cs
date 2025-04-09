using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Domain.Models
{
    public class ApiResponse<T> : ResponseStatusModel
    {
        public T? Data { get; set; }
    }
}
