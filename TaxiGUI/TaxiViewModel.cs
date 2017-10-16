using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        
        #region Commands

        public ICommand CloseCurrentWindowCommand { get; set; }

        public ICommand AddClientAndCloseCommand { get; set; }

        public ICommand ArrivedCommand { get; set; }

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
            CloseCurrentWindow(win);
            var client = new Customer(ClientLocation, ClientTarget, (uint)PeopleCount, ClientName);
            TaxiParkModel.AddClient(client);
            OnPropertyChanged(nameof(CurrentTaxi));
        }

        public void Arrive()
        {
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
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}