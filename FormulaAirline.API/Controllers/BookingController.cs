using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers;

[ApiController]
[Route("bookings")]
public class BookingController: ControllerBase 
{
    private readonly IMessageProducer _messageProducer;
    private readonly List<Booking> _bookings;
    public BookingController(IMessageProducer messageProducer) 
    {
        _messageProducer = messageProducer;
        _bookings = new List<Booking>
        {
            new() { Id = 1, Name = "Ron" },
            new() { Id = 2, Name = "Quang" }
        };
    }

    [HttpGet]
    public IActionResult GetAll() 
    {
        _messageProducer.SendMessage(_bookings);
        return Ok(_bookings);
    }
}