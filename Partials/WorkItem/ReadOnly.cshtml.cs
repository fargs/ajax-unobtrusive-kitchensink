using Microsoft.AspNetCore.Mvc;

namespace ajax_unobtrusive_kitchensink.Partials.WorkItem;

[Route("/partials/workitem/{workItemId}")]
public class ReadOnlyController : Controller
{
    public static readonly string PartialName = "/Partials/WorkItem/ReadOnly.cshtml";

    [HttpGet]
    [Route("/partials/workitem/{workItemId}")]
    public IActionResult Get(int workItemId)
    {
        return PartialView(PartialName);
    }
}

public class ReadOnlyModel
{
    public string? Name { get; set; }
}
