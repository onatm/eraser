using Eraser.Domain.Service;

namespace Eraser.ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var svc = new DataService();
            var adapterList = svc.GetAdapterList(true).Result;
        }
    }
}
