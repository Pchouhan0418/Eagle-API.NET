using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Domain.Entities
{
    public class MockedEmailLog
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? EventName { get; set; }
        public DateTime SentAt { get; set; }
        public string TenantId { get; set; }
    }

}
