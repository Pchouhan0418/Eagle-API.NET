using Eagle.Application.DTOs.RequestModels;
using Eagle.Application.Interfaces;
using Eagle.Application.Middlewares;
using Eagle.Application.Services;
using Eagle.Domain.Enums;
using Eagle.Domain.Middlewares;
using Eagle.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EagleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     
    public class EventController : ControllerBase
    {
        private readonly IEvent _EventService;
        private readonly ILogger<EventController> _logger;

       public EventController(IEvent EventService, ILogger<EventController> logger)
        {
            _EventService = EventService;
            _logger = logger;

        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequestDto eventRequestDto)
        {
            _logger.LogInformation("Creating a new event");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validation failed for event creation. ModelState is not valid.");

                var validationErrors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new { Field = ms.Key, Errors = ms.Value.Errors.Select(e => e.ErrorMessage) })
                    .ToList();

                return BadRequest(new ApiResponse<object>
                {
                    Code = ResponseStatusCode.BadRequest,
                    Message = "Validation failed",
                    Error = true,
                    Data = validationErrors
                });
            }

            try
            {
                var response = await _EventService.CreateEvent(eventRequestDto);

                if (!response.Error)
                {
                    _logger.LogInformation("Event creation succeeded");
                    return StatusCode((int)HttpStatusCode.Created, response);
                }

                _logger.LogWarning("Event creation failed: {Message}", response.Message);
                return StatusCode((int)response.Code, response);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Validation failed for event. Reason: {Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse<object>
                {
                    Code = ResponseStatusCode.BadRequest,
                    Message = ex.Message,
                    Error = true,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during event creation.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>
                {
                    Code = ResponseStatusCode.InternalServerError,
                    Message = "An unexpected error occurred.",
                    Error = true,
                    Data = null
                });
            }
        }

        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var eventsResponse = await _EventService.GetAllEvents();

                if (eventsResponse.Error)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Code = eventsResponse.Code,
                        Message = eventsResponse.Message,
                        Error = true,
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Code = ResponseStatusCode.Success,
                    Message = "Events retrieved successfully.",
                    Error = false,
                    Data = eventsResponse.Data
                });
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Custom exception occurred while retrieving all events: {Message}", ex.Message);
                return StatusCode(ex.StatusCode, new ApiResponse<object>
                {
                    Code = (ResponseStatusCode)ex.StatusCode,
                    Message = ex.Message,
                    Error = true,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving all events.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>
                {
                    Code = ResponseStatusCode.InternalServerError,
                    Message = "An unexpected error occurred.",
                    Error = true,
                    Data = null
                });
            }
        }

        [HttpGet("GetEventById")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                var eventResponse = await _EventService.GetEventById(id);

                if (eventResponse.Error)
                {
                    return NotFound(new ApiResponse<object>
                    {
                        Code = eventResponse.Code,
                        Message = eventResponse.Message,
                        Error = true,
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Code = ResponseStatusCode.Success,
                    Message = "Event retrieved successfully.",
                    Error = false,
                    Data = eventResponse.Data
                });
            }
            catch (CustomException ex)
            {
                _logger.LogWarning("Custom exception occurred while retrieving event with ID {EventId}: {Message}", id, ex.Message);
                return StatusCode(ex.StatusCode, new ApiResponse<object>
                {
                    Code = (ResponseStatusCode)ex.StatusCode,
                    Message = ex.Message,
                    Error = true,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving event with ID {EventId}.", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse<object>
                {
                    Code = ResponseStatusCode.InternalServerError,
                    Message = "An unexpected error occurred.",
                    Error = true,
                    Data = null
                });
            } 
        }


    }
}
