using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Application.DTOs.ResponseModels
{
    public class EventResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Venue { get; set; }

        public List<AttendeeResponseDto> AttendeesList { get; set; }


    }

}
