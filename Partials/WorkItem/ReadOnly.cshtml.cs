using Microsoft.AspNetCore.Mvc;

namespace htmx_kitchensink.Partials.YourName;


[Route("/partials/yourname")]
public class ReadOnlyController : Controller
{
    public static readonly string PartialName = "/Partials/YourName/ReadOnly.cshtml";

    [HttpGet]
    public IActionResult Get()
    {
        return PartialView(PartialName);
    }
}

public class ReadOnlyModel
{
    public string? Name { get; set; }
}
