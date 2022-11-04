using System.Net;
using Newtonsoft.Json;
namespace Lab06_belousov;

public class Weather : IComparable<Weather>
{
    public string? Temp;
    public string? Name;
    public string? Country;
    public string? Description;
    
    private static string API_KEY = "d37806bbb02cecdb04dabf01fdfa2975";
    private string _Lat; // широта
    private string _Lon; // долгота
    private IComparable<Weather> _comparableImplementation;

    public Weather()
    {
        _Lat = get_random_lat();
        _Lon = get_random_lon();
    }

    private string get_random_lat()
    {
        string result = "";
        Random rnd = new();
        int first_part = rnd.Next(-89, 89);
        int second_part = rnd.Next(0, 99);
        if (first_part > -10 && first_part < 0)
        {
            result = "0";
        }
        result += $"{Convert.ToString(first_part)}.";
        if (second_part < 10)
        {
            result += "0";
        }
        result += $"{Convert.ToString(second_part)}";
        
        return result;
    }
    
    private string get_random_lon()
    {
        string result = "";
        Random rnd = new();
        int first_part = rnd.Next(-179, 179);
        int second_part = rnd.Next(0, 99);
        if (first_part > -10 && first_part < 0)
        {
            result = "0";
        }
        result += $"{Convert.ToString(first_part)}.";
        if (second_part < 10)
        {
            result += "0";
        }
        result += $"{Convert.ToString(second_part)}";
        
        return result;
    }

    public async Task connect_and_request()
    {
        HttpClient web_reqiest = new HttpClient();
        string str_request = $"https://api.openweathermap.org/data/2.5/weather?lat={_Lat}&lon={_Lon}&appid={API_KEY}";
        
        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, str_request);
        using HttpResponseMessage response = await web_reqiest.SendAsync(request);
        
        string content = await response.Content.ReadAsStringAsync();
        
        var dynamicObject = JsonConvert.DeserializeObject<dynamic>(content)!;
        
        //Console.WriteLine(dynamicObject);
        //string govno = dynamicObject.main.temp;
        //Console.WriteLine(govno);
        
        Temp = dynamicObject.main.temp;
        Name = dynamicObject.name;
        Country = dynamicObject.sys.country;
        Description = dynamicObject.weather[0].description;
    }

    public void Info()
    {
        Console.WriteLine($"Contry: {Country}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Temperature: {Temp}");
    }

    public int CompareTo(Weather other)
    {
        if (Temp.CompareTo(other.Temp) != 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}