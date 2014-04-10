using Eraser.Domain.Service;
using Eraser.Model.Network;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Eraser.ViewModel
{
    public class ModalViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public const string remoteDNSProvider = "http://onatm.github.io/eraser-dns-config/dns.json";

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
                RaisePropertyChanged(() => AdapterList);
            }
        }

        public IEnumerable<DNS> DNSList
        {
            get { return _dnsList; }
            set
            {
                _dnsList = value;
                RaisePropertyChanged(() => DNSList);
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
                RaisePropertyChanged(() => SelectedAdapter);
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
                RaisePropertyChanged(() => SelectedDNSProvider);
            }
        }

        public ModalViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Initialize();
        }

        public void Initialize()
        {
            _dataService.GetAdapterList(true)
                .ContinueWith(t =>
                {
                    AdapterList = t.Result;
                });
        }

        public void GetDNSList()
        {
            _dataService.GetDNSList(remoteDNSProvider)
                .ContinueWith(t =>
                {
                    DNSList = t.Result;
                });
        }

        public void CheckConnection()
        {
            //TODO: Check connection whether DNS change could solve the problem.
        }
    }
}
