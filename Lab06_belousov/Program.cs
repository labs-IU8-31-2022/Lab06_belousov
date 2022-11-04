// See https://aka.ms/new-console-template for more information
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Lab06_belousov;


class Program
{
    static async Task Main(string[] args)
    {
        SortedSet<string> country_set = new SortedSet<string>();
        List<Weather> weathers = new List<Weather>();
        for (int i = 0; i < 20; ++i)
        {
            weathers.Add(new Weather());
        }
        double average_temp = 0;
        foreach (var w in weathers)
        {
            await w.connect_and_request();
            average_temp += Convert.ToDouble(w.Temp);
            country_set.Add(w.Country);
        }
        Console.WriteLine($"Average temperature: {average_temp}");
        /*foreach (var w in weathers)
        {
            w.Info();
        }*/
        
        var select_min_max = from weath in weathers
            orderby weath.Temp
            select weath;
        Console.WriteLine($"Max: {select_min_max.Last()}, Min: {select_min_max.First()}");
        
        /*foreach (var w in select_min_max)
        {
            w.Info();
        }*/
        Console.WriteLine($"Number of countries: {country_set.Count}");
        
        var clear_sky_country = from weath in weathers
            where (weath.Description == "clear sky")
            select weath;
        Console.WriteLine($"First country with clear sky: {clear_sky_country.First().Country}");

        var rainy_country = from weath in weathers
            where (weath.Description == "rain")
            select weath;
        Console.WriteLine($"First country with rain: {rainy_country.First().Description}");
        
        var few_clouds_country = from weath in weathers
            where (weath.Description == "few clouds")
            select weath;
        Console.WriteLine($"First country with few clouds: {few_clouds_country.First().Description}");
    }
}

