using AutoMapper;
using Eagle.Application.DTOs.RequestModels;
using Eagle.Application.DTOs.ResponseModels;
using Eagle.Application.Interfaces;
using Eagle.Domain.Entities;
using Eagle.Domain.Enums;
using Eagle.Domain.Middlewares;
using Eagle.Domain.Models;
using Eagle.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Eagle.Application.Middlewares;

namespace Eagle.Application.Services
{
    public class EventService : IEvent
    {
        private readonly EagleContext _eagleContext;
        private readonly IMapper _mapper;
        private readonly ILogger<EventService> _logger;
        private readonly ITenantProvider _tenantProvider;

        public EventService(EagleContext eventContext, IMapper mapper, ILogger<EventService> logger, ITenantProvider tenantProvider)
        {
            _eagleContext = eventContext;
            _mapper = mapper;
            _logger = logger;
            _tenantProvider = tenantProvider;
        }

        
        public async Task<ApiResponse<object>> CreateEvent(EventRequestDto eventRequestDto)
        {
            try
            {
                if (eventRequestDto == null)
                {
                    _logger.LogWarning("Event creation request was null.");
                    throw new CustomException("Event creation request cannot be null", (int)HttpStatusCode.BadRequest);
                }

                 
                var tenantId = _tenantProvider.GetTenant();
                if (string.IsNullOrEmpty(tenantId))
                {
                    _logger.LogWarning("TenantId is null or empty.");
                    throw new CustomException("TenantId is not set", (int)HttpStatusCode.BadRequest);
                }
                 
                var newEvent = _mapper.Map<Event>(eventRequestDto);
                newEvent.TenantId = tenantId; 

                _eagleContext.Events.Add(newEvent);
                await _eagleContext.SaveChangesAsync();

                if (eventRequestDto.AttendeesList != null && eventRequestDto.AttendeesList.Any())
                {
                    var attendees = eventRequestDto.AttendeesList.Select(a => new Attendee
                    {
                        EventId = newEvent.Id,
                        FullName = a.Name,
                        Email = a.Email
                        
                    }).ToList();

                    await _eagleContext.Attendees.AddRangeAsync(attendees);
                    await _eagleContext.SaveChangesAsync();

                    foreach (var attendee in attendees)
                    {
                        var emailMessage = $"Mocked Email: Sending invitation to {attendee.FullName} at {attendee.Email} for event '{newEvent.Name}'";
                        _logger.LogInformation(emailMessage);

                        _eagleContext.MockedEmailLogs.Add(new MockedEmailLog
                        {
                            Email = attendee.Email,
                            FullName = attendee.FullName,
                            EventName = newEvent.Name,
                            SentAt = DateTime.UtcNow,
                            TenantId = tenantId 
                        });
                    }

                    await _eagleContext.SaveChangesAsync();
                }

                return new ApiResponse<object>
                {
                    Code = ResponseStatusCode.Success,
                    Message = "Event created successfully",
                    Error = false
                };
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Custom exception occurred: {Message}", ex.Message);
                return new ApiResponse<object>
                {
                    Code = (ResponseStatusCode)ex.StatusCode,
                    Message = ex.Message,
                    Error = true,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred during event creation: {ExceptionMessage}", ex.Message);
                return new ApiResponse<object>
                {
                    Code = ResponseStatusCode.InternalServerError,
                    Message = "An unexpected error occurred: " + ex.Message,
                    Error = true,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<object>> GetAllEvents()
        {
            try
            {
                 
                var tenantId = _tenantProvider.GetTenant();
                if (string.IsNullOrEmpty(tenantId))
                {
                    _logger.LogWarning("TenantId is null or empty.");
                    throw new CustomException("TenantId is not set", (int)HttpStatusCode.BadRequest);
                }

                
                var events = await _eagleContext.Events
                    .Where(e => e.TenantId == tenantId)  
                    .Include(e => e.Attendees)
                    .ToListAsync();

                if (events == null || !events.Any())
                {
                    _logger.LogWarning("No events found for the current tenant.");
                    throw new CustomException("No events found.", (int)HttpStatusCode.NotFound);
                }

                var eventDtos = _mapper.Map<List<EventResponseDto>>(events);

                return new ApiResponse<object>
                {
                    Code = ResponseStatusCode.Success,
                    Message = "Events retrieved successfully.",
                    Error = false,
                    Data = eventDtos
                };
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Custom exception occurred while retrieving events: {Message}", ex.Message);
                return new ApiResponse<object>
                {
                    Code = (ResponseStatusCode)ex.StatusCode,
                    Message = ex.Message,
                    Error = true,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving events.");
                return new ApiResponse<object>
                {
                    Code = ResponseStatusCode.InternalServerError,
                    Message = "An unexpected error occurred while retrieving events.",
                    Error = true,
                    Data = null
                };
            }
        }

        
        public async Task<ApiResponse<object>> GetEventById(int id)
        {
            try
            {
                
                var tenantId = _tenantProvider.GetTenant();
                if (string.IsNullOrEmpty(tenantId))
                {
                    _logger.LogWarning("TenantId is null or empty.");
                    throw new CustomException("TenantId is not set", (int)HttpStatusCode.BadRequest);
                }

                var eventEntity = await _eagleContext.Events
                    .Where(e => e.TenantId == tenantId && e.Id == id) 
                    .Include(e => e.Attendees)
                    .FirstOrDefaultAsync();

                if (eventEntity == null)
                {
                    _logger.LogWarning("No event found for ID: {EventId}", id);
                    throw new CustomException("Event not found.", (int)HttpStatusCode.NotFound);
                }

                var eventDto = _mapper.Map<EventResponseDto>(eventEntity);

                return new ApiResponse<object>
                {
                    Code = ResponseStatusCode.Success,
                    Message = "Event retrieved successfully.",
                    Error = false,
                    Data = eventDto
                };
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Custom exception occurred while retrieving event with ID {EventId}: {Message}", id, ex.Message);
                return new ApiResponse<object>
                {
                    Code = (ResponseStatusCode)ex.StatusCode,
                    Message = ex.Message,
                    Error = true,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving event with ID {EventId}.", id);
                return new ApiResponse<object>
                {
                    Code = ResponseStatusCode.InternalServerError,
                    Message = "An unexpected error occurred while retrieving event.",
                    Error = true,
                    Data = null
                };
            }
        }
    }
}
