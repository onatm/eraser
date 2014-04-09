using Eraser.Model.Network;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eraser.Domain.Service
{
    public interface IDataService
    {
        Task<IList<Adapter>> GetAdapterList(bool isIPEnabled);
        Task<bool> SetDNS(string settingId, string[] dnsArray, bool defined);
        Task<IList<DNS>> GetDNSList(string requestUri);
    }
}
