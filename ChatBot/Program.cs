using ChatBot.Components; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiKey = builder.Configuration.GetValue<string>("ApiKey");
if (string.IsNullOrEmpty(apiKey))
{
    throw new Exception("ApiKey is required in configuration file, check readme for details");
}
builder.Services.AddHttpClient("", opts =>
{
    opts.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
