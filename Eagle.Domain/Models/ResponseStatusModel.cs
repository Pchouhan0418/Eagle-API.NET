using Eagle.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Domain.Models
{
    public class ResponseStatusModel
    {
        public ResponseStatusCode Code { get; set; } 
        public string? Message { get; set; }
        public bool Error { get; set; }
    }
}
