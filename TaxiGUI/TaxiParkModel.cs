using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaxiCore.Entities.Taxi;
using TaxiGUI.Annotations;

namespace TaxiGUI
{
    class TaxiParkModel: TaxiPark, INotifyPropertyChanged
    {
        private List<Taxi> taxis;

        public List<Taxi> Taxis
        {
            get { return taxis; }
            set
            {
                taxis = value; 
                OnPropertyChanged("Taxis");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}