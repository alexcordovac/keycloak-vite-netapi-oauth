using Weather_API.Endpoints;
using Weather_API.Weather.Infraestructure;

namespace Weather_API.Weather
{
    public class DeleteWeather(WeatherRepository weatherRepository)
    {
        internal async Task<bool> Handle(Guid id)
        {
            return await weatherRepository.DeleteAsync(id);
        }

        internal sealed class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapDelete("weather/{id}", async (Guid id, DeleteWeather useCase) =>
                    await useCase.Handle(id))
                    .WithTags(WeatherEndpoints.Tag)
                    .RequireAuthorization();
            }
        }
    }
}
