using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Taxi;
using TaxiGUI.Annotations;

namespace TaxiGUI
{
    internal class TaxiParkModel : TaxiPark, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Taxi> Taxis
        {
            get
            {
                return new ObservableCollection<Taxi>(base.Taxis);
            }

            set
            {
                base.Taxis = value.ToList();
                OnPropertyChanged(nameof(Taxis));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ObservableCollection<Customer> ClientsQueue
        {
            get
            {
                return new ObservableCollection<Customer>(clientsQueue);
            }

            set
            {
                clientsQueue = value.ToList();
                OnPropertyChanged(nameof(ClientsQueue));
            }
        }

        public void AddClient(Customer client)
        {
            base.AddClient(client);
            OnPropertyChanged(nameof(ClientsQueue));
            OnPropertyChanged(nameof(Taxis));
        }

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}