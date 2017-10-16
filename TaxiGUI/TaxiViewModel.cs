using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiGUI.Annotations;

namespace TaxiGUI
{
    internal class TaxiViewModel : INotifyPropertyChanged
    {
        private Location clientLocation;

        private string clientLocInput;

        private Location clientTarget;

        private string clientTargetInput;

        private Taxi currenTaxi;

        public TaxiViewModel()
        {
            CloseCurrentWindowCommand = new RelayCommand(CloseCurrentWindow);
            AddClientAndCloseCommand = new RelayCommand(AddClientAndClose);
            ArrivedCommand = new RelayCommand(s => Arrive());
            OpenMap = new RelayCommand(s => System.Diagnostics.Process.Start(MapUrl), s => MapUrl != null);
            ClientLocation = null;
            ClientTarget = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TaxiParkModel TaxiParkModel { get; set; }

        public Taxi CurrentTaxi
        {
            get
            {
                return TaxiParkModel.Taxis.FirstOrDefault(taxi => taxi == currenTaxi);
            }

            set
            {
                currenTaxi = value;
            }
        }

        public string MapUrl
        {
            get
            {
                if (CurrentTaxi == null || CurrentTaxi.Location == null || CurrentTaxi.TaxiTarget == null)
                {
                    return null;
                }
                NumberFormatInfo info = new NumberFormatInfo();
                info.NumberDecimalSeparator = ".";
                var latFrom = CurrentTaxi.Location.Lattitude.ToString(info);
                var lngFrom = CurrentTaxi.Location.Longtitude.ToString(info);

                var latTo = CurrentTaxi.TaxiTarget.Lattitude.ToString(info);
                var lngTo = CurrentTaxi.TaxiTarget.Longtitude.ToString(info);

                var url = $"https://www.google.com.ua/maps/dir/{latFrom},{lngFrom}/{latTo},{lngTo}/";
                return url;
            }
        }
        
        #region Commands

        public ICommand CloseCurrentWindowCommand { get; set; }

        public ICommand AddClientAndCloseCommand { get; set; }

        public ICommand ArrivedCommand { get; set; }

        public ICommand OpenMap { get; set; }

        #endregion

        #region Add client properties

        public Location ClientLocation
        {
            get
            {
                return clientLocation;
            }

            set
            {
                clientLocation = value;
                OnPropertyChanged(nameof(ClientLocation));
            }
        }

        public string ClientLocInput
        {
            get
            {
                return clientLocInput; 
            }

            set
            {
                ClientLocation = ExtractLocation(value);

                clientLocInput = value;
            }
        }

        public Location ClientTarget
        {
            get
            {
                return clientTarget;
            }

            set
            {
                clientTarget = value;
                OnPropertyChanged(nameof(ClientTarget));
            }
        }

        public string ClientTargetInput
        {
            get
            {
                return clientTargetInput;
            }

            set
            {
                ClientTarget = ExtractLocation(value);

                clientTargetInput = value;
            }
        }

        public int PeopleCount { get; set; }

        public string ClientName { get; set; }

        #endregion

        public void CloseCurrentWindow(object win)
        {
            (win as Window)?.Close();
        }

        public void AddClientAndClose(object win)
        {
            if (ClientLocation == null || ClientTarget == null || ClientName == null)
            {
                MessageBox.Show("You should fill all fields");
                return;
            }

            var client = new Customer(ClientLocation, ClientTarget, (uint)PeopleCount, ClientName);
            try
            {
                TaxiParkModel.AddClient(client);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("You cannot apply this route");
                return;
            }

            CloseCurrentWindow(win);
            OnPropertyChanged(nameof(CurrentTaxi));
            (OpenMap as RelayCommand)?.RaiseCanExecuteChanged();
        }

        public void Arrive()
        {
            var b = OpenMap.CanExecute(MapUrl);
            
            var currTaxi = CurrentTaxi;
            if (currTaxi.CurrentState == Taxi.State.InWay)
            {
                currTaxi.CurrentState = Taxi.State.Busy;
                currTaxi.TaxiTarget = currTaxi.Client.TargetLocation;
            }
            else if (currTaxi.CurrentState == Taxi.State.Busy)
            {
                currTaxi.TaxiTarget = null;
                currTaxi.Client = null;
                currTaxi.CurrentState = Taxi.State.Free;
            }

            OnPropertyChanged(nameof(CurrentTaxi));
            TaxiParkModel.OnPropertyChanged("ClientsQueue");
            TaxiParkModel.OnPropertyChanged("Taxis");
            (OpenMap as RelayCommand)?.RaiseCanExecuteChanged();
        }

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Location ExtractLocation(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

                try
                {
                    var response = GoogleApiProcessing.FindLocation(value.ToString());
                    var loc = GoogleApiProcessing.ParseJsonAddress(response);
                    return loc;
                }
                catch (WebException)
                {
                    return new Location(-1, -1, "Check your internet connection");
                }
                catch (ArgumentException)
                {
                    return new Location(-1, -1, "Nothing found");
                }
        }
    }
}