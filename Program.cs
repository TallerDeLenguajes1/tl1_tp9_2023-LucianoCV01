using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using EspacioMonedas;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        Monedas mon = JsonSerializer.Deserialize<Monedas>(responseBody);
                        
                        Console.WriteLine($"Precio " + mon.bpi.EUR.code + ": " + mon.bpi.EUR.rate_float);
                        Console.WriteLine($"Precio " + mon.bpi.GBP.code + ": " + mon.bpi.GBP.rate_float);
                        Console.WriteLine($"Precio " + mon.bpi.USD.code + ": " + mon.bpi.USD.rate_float);

                        Console.WriteLine("Ingrese una Moneda: \nEUR \nUSD \nGBP");
                        string? respuesta = Console.ReadLine();

                        switch (respuesta)
                        {
                            case "EUR":
                                Console.WriteLine("Code: "+mon.bpi.EUR.code);
                                Console.WriteLine("Simbolo: "+mon.bpi.EUR.symbol);
                                Console.WriteLine("Descripcion: "+mon.bpi.EUR.description);
                                Console.WriteLine("Tasa: "+mon.bpi.EUR.rate);
                            break;
                            case "USD":
                                Console.WriteLine("Code: "+mon.bpi.USD.code);
                                Console.WriteLine("Simbolo: "+mon.bpi.USD.symbol);
                                Console.WriteLine("Descripcion: "+mon.bpi.USD.description);
                                Console.WriteLine("Tasa: "+mon.bpi.USD.rate);
                            break;
                            case "GBP":
                                Console.WriteLine("Code: "+mon.bpi.GBP.code);
                                Console.WriteLine("Simbolo: "+mon.bpi.GBP.symbol);
                                Console.WriteLine("Descripcion: "+mon.bpi.GBP.description);
                                Console.WriteLine("Tasa: "+mon.bpi.GBP.rate);
                            break;
                            default:
                                Console.WriteLine("No se ingreso Correctamente la moneda");
                            break;
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
        }
    }
}