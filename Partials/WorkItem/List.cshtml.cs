using Microsoft.AspNetCore.Mvc;

namespace ajax_unobtrusive_kitchensink.Partials.WorkItem;

[Route("/partials/workitems")]
public class ListController : Controller
{
    public static string GetLink(IUrlHelper urlHelper) => urlHelper.Content("~/partials/workitems");
    [HttpGet]
    public IActionResult Get(int workItemId)
    {
        var partial = ListPartial.Create();
        return PartialView(ListPartial.PartialName, partial);
    }
}

public class ListPartial
{
    public static readonly string PartialName = "/Partials/WorkItem/List.cshtml";
    private ListPartial() { }
    public static ListPartial Create()
    {
        var partial = new ListPartial
        {
            TableRows = new List<TableRow>
            {
                new TableRow
                {
                    Name = "Work Item 1",
                    Description = "Description 1",
                    Status = "Active",
                    Priority = "High",
                    DueDate = "2022-01-01"
                },
                new TableRow
                {
                    Name = "Work Item 2",
                    Description = "Description 2",
                    Status = "Active",
                    Priority = "Medium",
                    DueDate = "2022-01-02"
                },
                new TableRow
                {
                    Name = "Work Item 3",
                    Description = "Description 3",
                    Status = "Active",
                    Priority = "Low",
                    DueDate = "2022-01-03"
                }
            }
        };
        return partial;
    }
    public List<TableRow> TableRows { get; set; } = new();
}

public class TableRow
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string DueDate { get; set; }
}