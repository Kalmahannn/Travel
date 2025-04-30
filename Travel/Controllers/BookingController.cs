using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

[Authorize(AuthenticationSchemes = "MyCookieAuth")]
public class BookingController : Controller
{
    private readonly ILogger<BookingController> _logger;

    public BookingController(ILogger<BookingController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Қолданушы {User} бронирование бетіне кірді.", User.Identity?.Name ?? "Анықталмаған");
        return View();
    }

    [HttpPost]
    public IActionResult Book(string destination, DateTime date)
    {
        _logger.LogInformation("Пайдаланушы {User} {Destination} бағытына {Date} күніне брондауға тырысты.",
            User.Identity?.Name ?? "Анықталмаған", destination, date);

        try
        {
            // Брондау логикасы (мысалы, мәліметтер қорына сақтау)

            _logger.LogInformation("Брондау сәтті аяқталды.");
            return RedirectToAction("Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Брондау кезінде қате пайда болды");
            return RedirectToAction("Error");
        }
    }
}
