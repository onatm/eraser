using Eraser.Core.Network;
using Eraser.Model.Network;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eraser.Domain.Service
{
    public class DataService : IDataService
    {
        public async Task<IList<Adapter>> GetAdapterList(bool isIPEnabled = true)
        {
            return await Task.Run(() => NIC.GetAdapterList(isIPEnabled).ToList());
        }

        public async Task<bool> SetDNS(string settingId, string[] dnsArray, bool defined)
        {
            return await Task.Run(() => NIC.SetDNS(settingId, dnsArray, defined));
        }
    }
}
