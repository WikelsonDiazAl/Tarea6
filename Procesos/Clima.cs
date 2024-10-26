using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class formato_clima
{
    public Main main { get; set; }
    public List<Weather> weather { get; set; }

    public class Main
    {
        public double temp { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
    }
}

public class ClimaServicio
{
    private readonly string apiKey = "a1140cad658ce9da9bbb38f21c5e0b5c";

    public async Task<ServerResult> ObtenerClimaAsync(string latitud, string longitud)
    {
        try
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitud}&lon={longitud}&appid={apiKey}&units=metric";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            var climaData = JsonSerializer.Deserialize<formato_clima>(json);

            var climaFinal = new Dictionary<string, string>();
            var temp = climaData.main.temp.ToString("0.0") + " °C";
            var descripcion = climaData.weather[0].description;

            climaFinal.Add("Temperatura", temp);
            climaFinal.Add("Descripción", Regex.Replace(descripcion, "<.*?>", string.Empty));

            return new ServerResult(true, "Clima obtenido", climaFinal);
        }
        catch (Exception ex)
        {
            return new ServerResult(false, ex.Message);
        }
    }
}
