using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Aveneo_zadanie.Models;
using Newtonsoft.Json.Linq;

namespace Aveneo_zadanie.Services
{
    public class GetData : IGetData
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string url = "https://sdw-wsrest.ecb.europa.eu/service/data/EXR/";

        public async Task<List<EcbModel>> DownloadData(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate)
        {
            try
            {
                var newUrl = url + $"D.{currencyCodes.Keys.First()}.{currencyCodes.Values.First()}.SP00.A?startPeriod={startDate.ToString("yyyy-MM-dd")}&endPeriod={endDate.ToString("yyyy-MM-dd")}&format=jsondata";
                HttpResponseMessage response = await client.GetAsync(newUrl);

                if (response.IsSuccessStatusCode)
                {
                    List<EcbModel> result = new List<EcbModel>();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var responseBodyJson = JObject.Parse(responseBody);
                    EcbJsonResponse mappedResponse = responseBodyJson.ToObject<EcbJsonResponse>();
                    var dataSetsData = ((JObject)((JArray)responseBodyJson.SelectToken("$.dataSets"))[0]).SelectToken("$.series.0:0:0:0:0.observations");

                    for (int i = 0; i < mappedResponse.structure.dimensions.observation[0].values.Count(); i++)
                    {
                        EcbModel ecbModel = new EcbModel();
                        ecbModel.currency1 = mappedResponse.structure.dimensions.series[1].values[0].id;
                        ecbModel.currency2 = mappedResponse.structure.dimensions.series[2].values[0].id;
                        ecbModel.exchangeRate = Convert.ToDouble(((JArray)dataSetsData.SelectToken(i.ToString()))[0]);
                        ecbModel.dataDate = mappedResponse.structure.dimensions.observation[0].values[i].start;
                        result.Add(ecbModel);
                    }
                    return result;
                }
                else
                {
                    Console.WriteLine($"Error occurred, the status code is: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
