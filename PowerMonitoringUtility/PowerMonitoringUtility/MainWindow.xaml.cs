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
        private DataExporterServices exporterServices;
        public MainWindow()
        {
            InitializeComponent();

            ServicesConfiguration config = new DataExporterServicesConfiguration(
                new List<DataExporter.ServicesConfiguration.TypesOfServices> {
                    DataExporter.ServicesConfiguration.TypesOfServices.EEC,
                    DataExporter.ServicesConfiguration.TypesOfServices.MTU,
                    DataExporter.ServicesConfiguration.TypesOfServices.TED,
            });

            this.exporterServices = new DataExporterServices(config);

            //DebugTextBlock.Text = this.exporterServices.DebugTests();
            
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DataExporterServices.IsExportRunning)
            {
                exporterServices.StartExportSchedule();
                ExportButton.Content = "STOP";
                ExportButton.Background = Brushes.Red;
                DebugTextBlock.Text = "Exporting data from API every \ntwo seconds to output directory.";  
            }
            else
            {
                DataExporterServices.IsExportRunning = false;
                ExportButton.Content = "START";
                ExportButton.Background = Brushes.Green;
                DebugTextBlock.Text = "Export stopped.";
            }

        }

    }
}
