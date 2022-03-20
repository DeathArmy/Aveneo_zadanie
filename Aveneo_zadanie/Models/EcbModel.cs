using System;
namespace Aveneo_zadanie.Models
{
    public class EcbModel
    {
        public string currency1 { get; set; }
        public string currency2 { get; set; }
        public double exchangeRate { get; set; }
        public DateTime dataDate { get; set; }
    }
}
