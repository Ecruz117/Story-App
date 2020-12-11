using System;

namespace Story_App
{



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class HackerNews
    {

        public string text { get; set; }
        public string by { get; set; }
      
        public int id { get; set; }

        public string title { get; set; }
   
        public string url { get; set; }
    }





    //public class WeatherForecast
    //{
    //    public DateTime Date { get; set; }

    //    public int TemperatureC { get; set; }

    //    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    //    public string Summary { get; set; }
    //}
}
