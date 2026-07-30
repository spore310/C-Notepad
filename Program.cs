using App.Services.API;
using App.Services.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient(typeof(App.Services.Utils.Logger<>));

builder.Services.AddHttpClient<PokeAPI>(client =>
{
    string? Url = Environment.GetEnvironmentVariable("POKEAPIURL");

    if (string.IsNullOrWhiteSpace(Url))
        throw new();

    client.BaseAddress = new Uri(Url);

    client.Timeout = TimeSpan.FromMinutes(2);
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapStaticAssets();

app.Run();
