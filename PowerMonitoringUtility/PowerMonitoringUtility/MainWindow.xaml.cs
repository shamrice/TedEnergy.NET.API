using DataExporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace PowerMonitoringUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataExporterServices eecDataExporterServices;
        private DataExporterServices mtuDataExporterServices;
        public MainWindow()
        {
            InitializeComponent();
            this.eecDataExporterServices = new DataExporterServices(
                new DataExporterServicesConfiguration(DataExporterServicesConfiguration.TypesOfServices.EEC));

            this.mtuDataExporterServices = new DataExporterServices(
                new DataExporterServicesConfiguration(ServicesConfiguration.TypesOfServices.MTU));

            DebugTextBlock.Text = this.eecDataExporterServices.DebugTests();
            DebugTextBlock.Text += this.mtuDataExporterServices.DebugTests();
        }
    }
}
