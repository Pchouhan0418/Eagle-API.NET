using Eagle.Application.DTOs.RequestModels;
using Eagle.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Application.Interfaces
{
    public interface IEvent
    {
        Task<ApiResponse<object>> CreateEvent(EventRequestDto eventRequestDto);
        Task<ApiResponse<object>> GetAllEvents();
        Task<ApiResponse<object>> GetEventById(int id);
    }
}
