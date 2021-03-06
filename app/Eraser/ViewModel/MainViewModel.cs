﻿using Eraser.Domain.Service;
using Eraser.Helper;
using Eraser.Model.Network;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace Eraser.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public const string remoteDNSProvider = "http://onatm.github.io/eraser-dns-config/dns.json";

        private Adapter _selectedAdapter;
        private DNS _selectedDNSProvider;

        public RelayCommand OpenModalDialog { get; private set; }

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

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            OpenModalDialog = 
                new RelayCommand(() =>
                    Messenger.Default.Send<OpenWindowMessage>(new OpenWindowMessage()
                    {
                        Type = WindowType.MODAL, 
                        SelectedDNSProvider = SelectedDNSProvider,
                        SelectedAdapter = SelectedAdapter
                    }));

            Messenger.Default.Register<Adapter>(this, a => SelectedAdapter = a != null ? a : SelectedAdapter);
            Messenger.Default.Register<DNS>(this, d => SelectedDNSProvider = d != null ? d : SelectedDNSProvider);

            Initialize();
        }

        public void Initialize()
        {
            _dataService.GetAdapterList(true)
                .ContinueWith(t => 
                {
                    if (t.Result.Count > 0)
                        SelectedAdapter = t.Result[0];
                });

            _dataService.GetDNSList(remoteDNSProvider)
                .ContinueWith(t =>
                {
                    if (t.Result.Count > 0)
                        SelectedDNSProvider = t.Result[0];
                });
        }

        public void SetDNS()
        {
            _dataService.SetDNS(SelectedAdapter.Id, SelectedDNSProvider.Address, true);
        }

        public void CheckConnection()
        {
            //TODO: Check connection whether DNS change could solve the problem.
        }
    }
}
