using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using EagleApi.Controllers;
using Eagle.Application.Interfaces;
using Eagle.Application.DTOs.RequestModels;
using Eagle.Application.DTOs.ResponseModels;
using Eagle.Domain.Enums;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Eagle.Domain.Models;

public class EventControllerTests
{
    private readonly Mock<IEvent> _mockEventService;
    private readonly Mock<ILogger<EventController>> _mockLogger;
    private readonly EventController _controller;

    public EventControllerTests()
    {
        _mockEventService = new Mock<IEvent>();
        _mockLogger = new Mock<ILogger<EventController>>();
        _controller = new EventController(_mockEventService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task CreateEvent_ReturnsCreatedResult_WhenSuccessful()
    {
        var request = new EventRequestDto
        {
            Name = "Tech Conference 2025",
            StartTime = new DateTime(2025, 5, 10, 9, 0, 0),
            EndTime = new DateTime(2025, 5, 10, 17, 0, 0),
            Venue = "International Expo Center, Accra",
            Attendees = 150,
            Email = "organizer@techconf.com"
        };

        var response = new ApiResponse<object>
        {
            Code = ResponseStatusCode.Created,
            Message = "Event created successfully",
            Error = false,
            Data = new { Id = 1 }
        };

        _mockEventService.Setup(s => s.CreateEvent(request)).ReturnsAsync(response);

        var result = await _controller.CreateEvent(request);

        var objectResult = result as ObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe((int)ResponseStatusCode.Created);
        var apiResponse = objectResult.Value as ApiResponse<object>;
        apiResponse.Error.ShouldBeFalse();
    }

    [Fact]
    public async Task GetAllEvents_ReturnsOkResult_WhenSuccessful()
    {
        var response = new ApiResponse<object>
        {
            Code = ResponseStatusCode.Success,
            Message = "Events retrieved successfully",
            Error = false,
            Data = new[] { new { Id = 1, Name = "Tech Conference 2025" }, new { Id = 2, Name = "AI Summit 2025" } }
        };

        _mockEventService.Setup(s => s.GetAllEvents()).ReturnsAsync(response);

        var result = await _controller.GetAllEvents();

        var objectResult = result as ObjectResult;
        objectResult.ShouldNotBeNull();
        objectResult.StatusCode.ShouldBe((int)ResponseStatusCode.Success);
        var apiResponse = objectResult.Value as ApiResponse<object>;
        apiResponse.Error.ShouldBeFalse();
        apiResponse.Data.ShouldNotBeNull();
    }
}
