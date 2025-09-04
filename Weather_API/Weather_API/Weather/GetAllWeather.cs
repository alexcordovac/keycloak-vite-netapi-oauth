using Weather_API.Endpoints;
using Weather_API.Weather.Infraestructure;

namespace Weather_API.Weather
{
    public class GetAllWeather(WeatherRepository weatherRepository)
    {
        internal async Task<IEnumerable<WeatherForecast>> Handle()
        {
            return await weatherRepository.GetAllAsync();
        }

        internal sealed class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("weather", async (GetAllWeather useCase) =>
                    await useCase.Handle())
                    .WithTags(WeatherEndpoints.Tag);
            }
        }
    }
}
