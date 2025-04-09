using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Domain.Entities
{
    [Table("tblAttendees")]
    public class Attendee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("eventid")]
        public int EventId { get; set; }

        [Required]
        [Column("fullname")]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [Column("email")]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
