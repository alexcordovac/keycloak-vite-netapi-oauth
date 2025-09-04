using Weather_API.Endpoints;
using Weather_API.Weather;
using Weather_API.Weather.Infraestructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/realms/weather"; // Keycloak realm
    options.Audience = "weather-api"; // must match clientId of API client
    options.RequireHttpsMetadata = !builder.Environment.IsDevelopment(); // dev only

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        RoleClaimType = "roles" // Keycloak puts roles inside "realm_access.roles"
    };
});

builder.Services.AddAuthorizationBuilder();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GetAllWeather>();
builder.Services.AddScoped<GetByIdWeather>();
builder.Services.AddScoped<CreateWeather>();
builder.Services.AddScoped<DeleteWeather>();
builder.Services.AddSingleton<WeatherRepository>();

// Add my endpoints
builder.Services.AddEndpoints();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://xqk2gpl1-5173.use2.devtunnels.ms") // Specify allowed origins
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigins"); // Use the name of the policy defined above

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

