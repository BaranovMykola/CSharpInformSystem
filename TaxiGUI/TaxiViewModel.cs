using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiGUI.Annotations;

namespace TaxiGUI
{
    class TaxiViewModel : INotifyPropertyChanged
    {
        private TaxiParkModel taxiPark;
        private ObservableCollection<int> myVar;

	    public ObservableCollection<int> MyProperty
	    {
		    get { return myVar;}
	        set
	        {
	            myVar = value;
	            OnPropertyChanged(nameof(MyProperty));
	        }
	    }
	

        public TaxiParkModel TaxiParkModel
        {
            get { return taxiPark; }
            set
            {
                taxiPark = value;
                OnPropertyChanged(nameof(TaxiParkModel));
            }
        }

        private Location _clientLocation;
        private string _clientLocInput;
        private Location _clientTarget;
        private string _clientTargetInput;

        public TaxiViewModel()
        {
            CloseCurrentWindowCommand = new RelayCommand(CloseCurrentWindow);
            AddClientAndCloseCommand = new RelayCommand(AddClientAndClose);
            ClientLocation = null;
            ClientTarget = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseCurrentWindowCommand { get; set; }

        public ICommand AddClientAndCloseCommand { get; set; }

        #region Add client properties

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

        public int PeopleCount { get; set; }

        public string ClientName { get; set; }

        public TaxiParkModel TaxiPark
        {
            get { return taxiPark; }
            set { taxiPark = value; }
        }

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
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}