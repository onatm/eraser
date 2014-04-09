using Eraser.Domain.Service;
using System;

namespace Eraser.ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var svc = new DataService();
            var adapterList = svc.GetAdapterList(true).Result;
            var dnsList = svc.GetDNSList("http://onatm.github.io/eraser-dns-config/dns.json").Result;
        }
    }
}
