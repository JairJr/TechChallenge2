using TechChallenge.NoticiasAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TechChallenge.Identity.Controllers;
using TechChallenge.Identity.Extensions;
using NoticiasAPI.Controllers;
using NoticiasAPI.Service;
using ElmahCore.Mvc;
using ElmahCore;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//AddDbContext
// Decode a Connection String em Base64
string base64EncodedConnectionString = builder.Configuration.GetConnectionString("AppNoticiasConnection") ?? string.Empty;
byte[] bytes = Convert.FromBase64String(base64EncodedConnectionString);
string connectionString = Encoding.UTF8.GetString(bytes);
builder.Services.AddDbContext<NoticiasContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));

//services
builder.Services.AddScoped<IEmailService, BrevoEmailService>();

//HttpClient
builder.Services.AddHttpClient<AuthenticateController>();
builder.Services.AddHttpClient<NoticiasController>();

builder.Services.AddDIAuthentication(builder);

builder.Services.AddDICors(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Acesso protegido utilizando o accessToken obtido em \"api/Authenticate/login\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddScoped<INoticiaService, NoticiaService>();
builder.Services.AddElmah<XmlFileErrorLog>(options =>
{
    options.LogPath = "~/log";
});

builder.Services.AddHttpClient("Brevo", c =>
{
	c.BaseAddress = new Uri("https://api.sendinblue.com/v3/");
	c.DefaultRequestHeaders.Add("api-key", "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");
	c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Aplicar migrações para NoticiasContext
    var noticiasContext = services.GetRequiredService<NoticiasContext>();
    noticiasContext.Database.Migrate();

    // Aplicar migrações para IdentityContext
    var identityContext = services.GetRequiredService<IdentityContext>();
    identityContext.Database.Migrate();
}

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

//cors
app.UseCors("CorsPolicy-public");

app.MapControllers();
app.UseElmah();
app.Run();
