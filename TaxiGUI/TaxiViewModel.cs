using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using TaxiCore.Entities.Position;
using TaxiGUI.Annotations;

namespace TaxiGUI
{
    class TaxiViewModel : INotifyPropertyChanged
    {
        private TaxiParkModel taxiPark;
        private Location _clientLocation;
        private string _c1;
        private string _clientLocInput;
        private Location _clientTarget;
        private string _clientTargetInput;

        public TaxiViewModel()
        {
             CloseCurrentWindowCommand = new RelayCommand(CloseCurrentWindow);
             AddClientAndCloseCommand = new RelayCommand(AddClientAndClose);
             ClientLocation = new Location() {Address = "unknowns"};
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCurrentWindowCommand { get; set; }

        public ICommand AddClientAndCloseCommand { get; set; }

        public Location ClientLocation
        {
            get { return _clientLocation; }
            set
            {
                _clientLocation = value;
                OnPropertyChanged(nameof(ClientLocation));
            }
        }

        public string ClientLocInput
        {
            get { return _clientLocInput; }
            set
            {
                if (value != null && value.ToString() != string.Empty)
                {
                    try
                    {
                        var response = GoogleApiProcessing.FindLocation(value.ToString());
                        var loc = GoogleApiProcessing.ParseJsonAddress(response);
                        ClientLocation = loc;
                    }
                    catch (WebException)
                    {
                        ClientLocation = new Location(-1, -1, "Check your internet connection");
                    }
                    catch (ArgumentException)
                    {
                        ClientLocation = new Location(-1, -1, "Nothing found");
                    }
                }
                _clientLocInput = value;
            }
        }

        public Location ClientTarget
        {
            get { return _clientTarget; }
            set
            {
                _clientTarget = value;
                OnPropertyChanged(nameof(ClientTarget));
            }
        }

        public string ClientTargetInput
        {
            get { return _clientTargetInput; }
            set
            {
                if (value != null && value.ToString() != string.Empty)
                {
                    try
                    {
                        var response = GoogleApiProcessing.FindLocation(value.ToString());
                        var loc = GoogleApiProcessing.ParseJsonAddress(response);
                        ClientTarget = loc;
                    }
                    catch (WebException)
                    {
                        ClientTarget = new Location(-1, -1, "Check your internet connection");
                    }
                    catch (ArgumentException)
                    {
                        ClientTarget = new Location(-1, -1, "Nothing found");
                    }
                }
                _clientTargetInput = value;
            }
        }

        public void CloseCurrentWindow(object win)
        {
            (win as Window)?.Close();
        }

        public void AddClientAndClose(object win)
        {
            CloseCurrentWindow(win);
            //LocationConverter.SetProp += s => ClientLocation = s;
            //PropertyChanged += new PropertyChangedEventHandler();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void propchange(string prop)
        {
            OnPropertyChanged(prop);
        }
    }
}