using ajax_unobtrusive_kitchensink.Partials.WorkItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ajax_unobtrusive_kitchensink.Pages;

public class IndexPage : PageModel
{
    public required ListPartial ListPartial { get; set; }
    public void OnGet()
    {
        ListPartial = ListPartial.Create();
    }
}
