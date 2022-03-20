using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aveneo_zadanie.Models;

namespace Aveneo_zadanie.Services
{
    public interface IGetData
    {
        public Task<List<EcbModel>> DownloadData(Dictionary<string, string> currencyCodes, DateTime startDate, DateTime endDate);
    }
}
