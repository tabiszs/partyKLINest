using PartyKlinest.Infrastructure;
using PartyKlinest.WebApi.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string CORS_POLICY_DEV = "CorsPolicyDev";
const string CORS_POLICY_PROD = "CorsPolicyProd";
string[] allowedOrigins = builder.Configuration.GetAllowedOrigins();

builder.Services.AddCors(options =>
{
    options.AddPolicy(CORS_POLICY_DEV,
        corsBuilder =>
        {
            corsBuilder.AllowAnyOrigin(); // unsafe, okay for testing
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
        });
    options.AddPolicy(CORS_POLICY_PROD,
        corsBuilder =>
        {
            corsBuilder.WithOrigins(allowedOrigins);
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
        });
});


Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddControllers()
    .AddJsonOptions(j =>
    {
        // Convert C# enums (int wrappers) to strings
        j.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseCors(CORS_POLICY_DEV);
}
else
{
    app.UseCors(CORS_POLICY_PROD);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
