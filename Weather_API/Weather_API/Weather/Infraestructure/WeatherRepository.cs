namespace Weather_API.Weather.Infraestructure
{
    public class WeatherRepository
    {
        static readonly List<WeatherForecast> _forecasts = new List<WeatherForecast>();

        public WeatherRepository()
        {
            Seed();
        }

        private void Seed()
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecasts = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            {

                Date =  DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC =  Random.Shared.Next(-20, 55),
                Summary =  summaries[Random.Shared.Next(summaries.Length)]
            });

            _forecasts.AddRange(forecasts);
        }

        internal async Task<WeatherForecast?> GetByIdAsync(Guid Id)
        {
            return await Task.FromResult(_forecasts.FirstOrDefault(wf => wf.Id == Id));
        }

        internal async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            return await Task.FromResult(_forecasts);
        }

        internal async Task<WeatherForecast> CreateAsync(WeatherForecast weather)
        {
            _forecasts.Add(weather);

            return await Task.FromResult(weather);
        }

        internal async Task<bool> DeleteAsync(Guid id)
        {
            var item = _forecasts.FirstOrDefault(wf => wf.Id == id);
            if(item != null)
            {
                _forecasts.Remove(item);
                return true;
            }

            return false;
        }
    }
}
