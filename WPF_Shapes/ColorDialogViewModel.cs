using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPF_Shapes.Annotations;

namespace WPF_Shapes
{
    public class ColorDialogViewModel : INotifyPropertyChanged
    {
        private int r;
        private int g;
        private int b;
        private double alpha = 255;
        private Brush colorPicker = Brushes.Black;

        public ColorDialogViewModel()
        {
            ColorClickCommand = new RelayCommand(ColorClick);

            Colors =
                new ObservableCollection<Brush>(
                    typeof(Brushes).GetProperties().Select(p => p.GetValue(null) as Brush).OrderBy(s =>
                   {
                       var pp = (Color)s.GetValue(SolidColorBrush.ColorProperty);
                       return pp.R;
                   }).ThenBy(s =>
                   {
                       var pp = (Color)s.GetValue(SolidColorBrush.ColorProperty);
                       return pp.G;
                   }).ThenBy(s =>
                   {
                       var pp = (Color)s.GetValue(SolidColorBrush.ColorProperty);
                       return pp.B;
                   }).Where(s =>
                   {
                       var pp = (Color)s.GetValue(SolidColorBrush.ColorProperty);
                       return pp.A != 0;
                   }).ToList());

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int R
        {
            get
            {
                return r;
            }

            set
            {
                r = value;
                OnPropertyChanged(nameof(R));
                UpdateBrush();
            }
        }

        public int G
        {
            get
            {
                return g;
            }

            set
            {
                g = value;
                OnPropertyChanged(nameof(G));
                UpdateBrush();
            }
        }

        public int B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
                OnPropertyChanged(nameof(B));
                UpdateBrush();
            }
        }

        public double Alpha
        {
            get
            {
                return alpha; 
            }

            set
            {
                alpha = value;
                OnPropertyChanged(nameof(B));
                UpdateBrush();
            }
        }

        public Brush ColorPicker
        {
            get
            {
                return colorPicker; 
            }

            set
            {
                colorPicker = value;
                OnPropertyChanged(nameof(ColorPicker));
            }
        }

        public ICommand ColorClickCommand { get; set; }

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ObservableCollection<Brush> Colors { get; set; }

        public bool DialogResult { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateBrush()
        {
            ColorPicker = new SolidColorBrush(Color.FromArgb((byte)Alpha, (byte)R, (byte)G, (byte)B));
        }

        private void ColorClick(object parameter)
        {
            var brush = parameter as Brush;
            var color = (Color)brush.GetValue(SolidColorBrush.ColorProperty);
            R = color.R;
            B = color.B;
            G = color.G;
            Alpha = color.A;
        }

        private void Ok(object parameter)
        {
            DialogResult = true;
            CloseWindow(parameter);
        }

        private void Cancel(object parameter)
        {
            DialogResult = false;
            CloseWindow(parameter);
        }

        private void CloseWindow(object window)
        {
            (window as Window)?.Close();
        }
    }
}
