using Eraser.Domain.Service;
using Eraser.Model.Network;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Eraser.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public const string remoteDNSProvider = "http://onatm.github.io/eraser-dns-config/dns.json";

        public const string adapterListPropertyName = "AdapterList";
        public const string dnsListPropertyName = "DNSList";
        public const string selectedAdapterPropertyName = "SelectedAdapter";
        public const string selectedDNSProviderPropertyName = "SelectedDNSProvider";

        private IEnumerable<Adapter> _adapterList;
        private IEnumerable<DNS> _dnsList;
        private Adapter _selectedAdapter;
        private DNS _selectedDNSProvider;

        public IEnumerable<Adapter> AdapterList
        {
            get { return _adapterList; }
            set
            {
                _adapterList = value;
                RaisePropertyChanged(adapterListPropertyName);
            }
        }

        public IEnumerable<DNS> DNSList
        {
            get { return _dnsList; }
            set
            {
                _dnsList = value;
                RaisePropertyChanged(dnsListPropertyName);
            }
        }

        public Adapter SelectedAdapter
        {
            get { return _selectedAdapter; }
            set
            {
                if (_selectedAdapter == value)
                    return;

                _selectedAdapter = value;
                RaisePropertyChanged(selectedAdapterPropertyName);
            }
        }

        public DNS SelectedDNSProvider
        {
            get { return _selectedDNSProvider; }
            set
            {
                if (_selectedDNSProvider == value)
                    return;

                _selectedDNSProvider = value;
                RaisePropertyChanged(selectedDNSProviderPropertyName);
            }
        }

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Initialize();
        }

        public void Initialize()
        {
            _dataService.GetAdapterList(true).ContinueWith(t => { AdapterList = t.Result; });
        }

        public void CheckConnection()
        {
        }

        public void GetDNSList()
        {
            _dataService.GetDNSList(remoteDNSProvider).ContinueWith(t => { DNSList = t.Result; });
        }

        public void SetDNS()
        {
            _dataService.SetDNS(_selectedAdapter.Id, _selectedAdapter.DNS, true);
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}