﻿using Eraser.Domain.Service;
using Eraser.Model.Network;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Eraser.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public const string adapterListPropertyName = "AdapterList";
        public const string selectedAdapterPropertyName = "SelectedAdapter";

        private IEnumerable<Adapter> _adapterList;
        private Adapter _selectedAdapter;
        private IEnumerable<DNS> _dnsList;

        public IEnumerable<Adapter> AdapterList
        {
            get { return _adapterList; }
            set
            {
                _adapterList = value;
                RaisePropertyChanged(adapterListPropertyName);
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
            _dataService.GetDNSList("http://onatm.github.io/eraser-dns-config/dns.json").ContinueWith(t => { _dnsList = t.Result; });
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