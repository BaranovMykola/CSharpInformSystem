using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaxiGUI.Annotations;

namespace TaxiGUI
{
    class TaxiViewModel : INotifyPropertyChanged
    {
        private TaxiParkModel taxiPark;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}