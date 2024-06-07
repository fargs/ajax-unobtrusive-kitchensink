var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.MapRazorPages();
app.UseRouting();

app.Run();
