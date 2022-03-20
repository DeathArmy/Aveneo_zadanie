using Microsoft.AspNetCore.Mvc;
using Aveneo_zadanie.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.Json;
using Aveneo_zadanie.Data;
using Aveneo_zadanie.Models;
using System.Security.Cryptography;

namespace Aveneo_zadanie.Controllers
{
    [ApiController]
    [Route("controller")]
    public class HomeController : Controller
    {
        private readonly IGetData _getData;
        private readonly DataContext _context;
        public HomeController(IGetData getData, DataContext context)
        {
            _getData = getData;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Dictionary<string, string> currency, DateTime startPeriod, DateTime? endPeriod = null, string? apiKey = "")
        {
            try
            {
                if (apiKey == "") return new StatusCodeResult(401);
                else
                {
                    var checkApiKey = _context.ApiKeys.FirstOrDefault(e => e.key == apiKey);
                    if (checkApiKey == null) return new StatusCodeResult(401);
                }
                if (startPeriod > DateTime.Now) return NotFound("Wrong date!");
                else
                {
                    if (endPeriod == null) endPeriod = startPeriod;
                    var dbData = _context.EcbModels.Where(e => e.currency1 == currency.Keys.First() && e.currency2 == currency.Values.First() && e.dataDate >= startPeriod && e.dataDate <= endPeriod).ToList();
                    if (dbData.Count == ((DateTime)endPeriod - startPeriod).TotalDays + 1)
                    {
                        return Ok(JsonSerializer.Serialize(dbData));
                    }
                    else
                    {
                        var response = await _getData.DownloadData(currency, startPeriod, (DateTime)endPeriod);
                        if (response != null)
                        {
                            foreach (var element in response)
                            {
                                var checkIfAlreadyExist = dbData.FirstOrDefault(el => el.dataDate == element.dataDate);
                                if (checkIfAlreadyExist != null) _context.Update(checkIfAlreadyExist);
                                else await _context.AddAsync(element);
                            }
                            await _context.SaveChangesAsync();
                            return Ok(JsonSerializer.Serialize(response));
                        }
                        else
                        {
                            return NotFound(JsonSerializer.Serialize(response));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("api")]
        public async Task<IActionResult> GetApiKey()
        {
            try
            {
                ApiKey api = new ApiKey();

                using var provider = new RNGCryptoServiceProvider();
                var bytes = new byte[32];
                provider.GetBytes(bytes);

                api.key = Convert.ToBase64String(bytes);

                await _context.ApiKeys.AddAsync(api);
                await _context.SaveChangesAsync();

                return Ok(JsonSerializer.Serialize(api.key));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}
