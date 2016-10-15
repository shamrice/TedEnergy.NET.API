using DataExporter.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API;
using TedEnergy.Web.API.DataObjects;
using TedEnergy.Web.API.DataObjects.Eec;
using TedEnergy.Web.API.DataObjects.Ted500;
using TedEnergy.Web.API.WebClients;

namespace DataExporter
{
    public class DataExporterServices
    {
        private readonly ServicesConfiguration config;
        private readonly IList<TedEnergyWebApi> webApis;
        private readonly IList<Exporter> exporters;

        public DataExporterServices(ServicesConfiguration configuration)
        {
            this.config = configuration;
            this.webApis = new List<TedEnergyWebApi>();
            this.exporters = new List<Exporter>();

            foreach (ServiceType serviceType in this.config.ConfiguredTypesOfServices)
                webApis.Add(TedEnergyWebApiBuilder.Build(serviceType));

            foreach (TedEnergyWebApi api in webApis)
                exporters.Add(new Exporter(config.ExportLocation, api));
        }

        public void Export()
        {
            foreach (Exporter exporter in this.exporters)
                exporter.ExportToCsv();
        }

        public string DebugTests()
        {
            string result = string.Empty;
            foreach (Exporter exporter in this.exporters)
                result += exporter.DebugTests();
            return result;
        }
        
    }
}
