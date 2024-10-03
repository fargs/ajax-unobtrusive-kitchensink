using Microsoft.AspNetCore.Mvc;

namespace ajax_unobtrusive_kitchensink.Partials.WorkItem;

[Route("/partials/workitem/new")]
public class NewController : Controller
{
    public static readonly string PartialName = "/Partials/WorkItem/New.cshtml";

    public static string GetLink(IUrlHelper urlHelper) => $"{urlHelper.Content("~/partials/workitem/new")}";
    [HttpGet]
    public IActionResult Get()
    {
        return PartialView(PartialName);
    }

    public static string PostLink(IUrlHelper urlHelper) => $"{urlHelper.Content("~/partials/workitem/new")}";
    [HttpPost]
    public IActionResult Post(NewModel model)
    {
        var listLink = ListController.GetLink(Url);
        return Created(listLink, new { ListLink = listLink });
    }
}

public class NewModel
{
    public string? Name { get; set; }
}
