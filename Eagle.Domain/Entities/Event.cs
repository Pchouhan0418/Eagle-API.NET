using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Domain.Entities
{

        public class Event
        {
            [Key]
            [Column("id")]
            public int Id { get; set; }

            [Column("name")]
            [StringLength(200)]
            public string Name { get; set; }

            [Column("starttime")]
            [DataType(DataType.DateTime)]
            public DateTime StartTime { get; set; }

            [Column("endtime")]
            [DataType(DataType.DateTime)]
            public DateTime EndTime { get; set; }

            [Column("venue")]
            [StringLength(300)]
            public string Venue { get; set; }

            [Column("attendees")]
            public int Attendees { get; set; }

            [Column("email")]
            [StringLength(100)]
            public string Email { get; set; }

            [Column("createdon")]
            [DataType(DataType.DateTime)]
            public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

            [Column("updatedon")]
            [DataType(DataType.DateTime)]
            public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
    }

}
