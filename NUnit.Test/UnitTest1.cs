using NUnit.Framework;
using System;
using System.Collections.Generic;
using Aveneo_zadanie.Services;
using System.Threading.Tasks;

namespace NUnit.Test
{
    [TestFixture]
    public class Tests
    {
        private GetData _getData;

        [SetUp]
        public void Setup()
        {
            _getData = new GetData();
        }

        [Test]
        public async Task LoadTest()
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            Dictionary<string, string> currency = new Dictionary<string, string>();
            currency.Add("PLN", "EUR");
            timer.Start();
            for (int i = 0; i < 50; i++)
            {
                Random r = new Random();
                int random = r.Next(1, 100);
                var result = await _getData.DownloadData(currency, DateTime.Now.AddDays(random * -1), DateTime.Now.AddDays(random * -1));
            }    
            timer.Stop();
            bool responseTime = (timer.Elapsed.TotalSeconds < 10);
            Assert.IsTrue(responseTime, "Test with 50 request ends within 10 seconds");
        }
    }
}