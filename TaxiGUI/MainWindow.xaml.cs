using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaxiCore.Entities;
using TaxiCore.Entities.Demand;
using TaxiCore.Entities.Position;
using TaxiCore.Entities.Taxi;
using TaxiCore.Entities.Transport;
using TaxiGUI.Annotations;

namespace TaxiGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var park = EntityReader.EFWLoad();

            var tvm = new TaxiViewModel() { TaxiParkModel = new TaxiParkModel(park) };
            tvm.CurrentTaxi = park.Taxis.First();
            DataContext = tvm;
        }

        private void AddClientButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddClientWindow(DataContext);
            dialog.ShowDialog();
        }
    }
}
