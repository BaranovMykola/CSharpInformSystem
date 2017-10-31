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
        private int _r;
        private int _g;
        private int _b;
        private double _alpha = 255;
        private Brush _colorPicker = Brushes.Black;

        public ColorDialogViewModel()
        {
            ColorClickCommand = new RelayCommand(ColorClick);

            Colors =
                new ObservableCollection<Brush>(
                    typeof (Brushes).GetProperties().Select(p => p.GetValue(null) as Brush).OrderBy(s =>
                    {
                        var pp = ((Color) (s.GetValue(SolidColorBrush.ColorProperty)));
                        return pp.R;
                    }).ThenBy(s =>
                    {
                        var pp = ((Color) (s.GetValue(SolidColorBrush.ColorProperty)));
                        return pp.G;
                    }).ThenBy(s =>
                    {
                        var pp = ((Color) (s.GetValue(SolidColorBrush.ColorProperty)));
                        return pp.B;
                    }).Where(s =>
                    {
                        var pp = ((Color) (s.GetValue(SolidColorBrush.ColorProperty)));
                        return pp.A != 0;
                    }).ToList());

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int R
        {
            get { return _r; }
            set
            {
                _r = value;
                OnPropertyChanged(nameof(R));
                UpdateBrush();
            }
        }

        public int G
        {
            get { return _g; }
            set
            {
                _g = value;
                OnPropertyChanged(nameof(G));
                UpdateBrush();
            }
        }

        public int B
        {
            get { return _b; }
            set
            {
                _b = value;
                OnPropertyChanged(nameof(B));
                UpdateBrush();
            }
        }

        public double Alpha
        {
            get { return _alpha; }
            set
            {
                _alpha = value;
                OnPropertyChanged(nameof(B));
                UpdateBrush();
            }
        }

        public Brush ColorPicker
        {
            get { return _colorPicker; }
            set
            {
                _colorPicker = value; 
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
            ColorPicker = new SolidColorBrush(Color.FromArgb((byte)Alpha, (byte) R, (byte) G, (byte) B));
        }

        private void ColorClick(object parameter)
        {
            var p1 = (parameter as Brush);
            var pp = ((Color) (p1.GetValue(SolidColorBrush.ColorProperty)));
            R = pp.R;
            B = pp.B;
            G = pp.G;
            Alpha = pp.A;
            var g = Colors[2];
            var values = typeof(Brushes).GetProperties().Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).
    ToArray();
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
