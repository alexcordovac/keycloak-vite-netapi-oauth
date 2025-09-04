using Weather_API.Endpoints;
using Weather_API.Weather.Infraestructure;

namespace Weather_API.Weather
{
    public class CreateWeather(WeatherRepository weatherRepository)
    {
        public sealed record Request(int temperatureC, int temperatureF, string? Summary);

        internal async Task<WeatherForecast> Handle(Request request)
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                TemperatureC = request.temperatureC,
                Summary = request.Summary
            };

            return await weatherRepository.CreateAsync(weatherForecast);
        }

        internal sealed class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPost("weather", async (Request request, CreateWeather useCase) =>
                    await useCase.Handle(request))
                    .WithTags(WeatherEndpoints.Tag)
                    .RequireAuthorization();
            }
        }
    }
}
