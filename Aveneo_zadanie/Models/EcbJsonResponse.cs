using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Aveneo_zadanie.Models
{
    [Serializable()]
    public class EcbJsonResponse
    {
        [JsonProperty("structure")]
        public Structure structure { get; set; }
    }

    public class Structure
    {
        [JsonProperty("dimensions")]
        public Dimensions dimensions { get; set; }
    }

    public class Dimensions
    {
        [JsonProperty("series")]
        public List<Series> series { get; set; }
        [JsonProperty("observation")]
        public List<Observation> observation { get; set; }
    }

    public class Series
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("values")]
        public List<Values> values { get; set; }
    }

    public class Values
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class Observation
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("role")]
        public string role { get; set; }
        [JsonProperty("values")]
        public List<ValuesObs> values { get; set; }
    }

    public class ValuesObs
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("start")]
        public DateTime start { get; set; }
        [JsonProperty("end")]
        public DateTime end { get; set; }
    }
}
