using Application.Services;
using Domain;
using Infrastructure;
using Infrastructure.ExternalApis;
using Observability.Middleware;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Configurar os serviços e dependências
builder.Services.AddScoped<IUsuariosApi, UsuariosApi>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddHttpClient<UsuariosApi>();
builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("ApiOptions"));

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<RequestIdMiddleware>();
app.UseMiddleware<RequestResponseMiddleware>();

app.UseRouting();


// Crie o contador dentro do método Main
var requestsCounter = Metrics.CreateCounter("requestCounter", "Total number of requests received");

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        // Lógica do seu aplicativo

        // Incrementa o contador de solicitações
        requestsCounter.Inc();

        // ...
    });
});

app.UseMetricServer();
app.UseHttpMetrics();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); ;

app.Run();
