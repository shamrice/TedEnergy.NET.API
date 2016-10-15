﻿using DataExporter.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        private Timer runTimer;

        public static bool IsExportRunning { get; set; }

        public DataExporterServices(ServicesConfiguration configuration)
        {
            IsExportRunning = false;
            this.config = configuration;
            this.webApis = new List<TedEnergyWebApi>();
            this.exporters = new List<Exporter>();

            foreach (ServiceType serviceType in this.config.ConfiguredTypesOfServices)
                webApis.Add(TedEnergyWebApiBuilder.Build(serviceType));

            foreach (TedEnergyWebApi api in webApis)
                exporters.Add(new Exporter(config.ExportLocation, api));

            this.runTimer = new Timer();
            runTimer.Interval = 2000;
            runTimer.Enabled = true;
            runTimer.Elapsed += runTimer_Elapsed;
        }

        private void runTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsExportRunning)
            {
                foreach (Exporter exporter in this.exporters)
                    exporter.ExportToCsv();
            }
        }

        public void StartExportSchedule()
        {
            IsExportRunning = true;
            runTimer.Start();
            
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
