using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Application.DTOs.RequestModels
{
    public class EventRequestDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Venue is required")]
        public string Venue { get; set; }

        public List<AttendeeRequestDto> AttendeesList { get; set; }
    }




}
