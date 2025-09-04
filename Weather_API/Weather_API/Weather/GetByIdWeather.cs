using Weather_API.Endpoints;
using Weather_API.Weather.Infraestructure;

namespace Weather_API.Weather
{
    public class GetByIdWeather(WeatherRepository weatherRepository)
    {
        

        internal async Task<WeatherForecast?> Handle(Guid Id)
        {
            return await weatherRepository.GetByIdAsync(Id);
        }

        internal sealed class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("weather/{id}", async (Guid id, GetByIdWeather useCase) =>
                    await useCase.Handle(id))
                    .WithTags(WeatherEndpoints.Tag)
                    .RequireAuthorization();
            }
        }
    }
}
