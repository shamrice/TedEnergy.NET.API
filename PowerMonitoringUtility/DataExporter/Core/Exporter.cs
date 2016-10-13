using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API;
using TedEnergy.Web.API.DataObjects.Eec;
using TedEnergy.Web.API.DataObjects.Ted500;

namespace DataExporter.Core
{
    public class Exporter
    {
        private readonly string fileLocation;        
        private readonly IList<TedEnergyWebApi> webApis;
        private string fileName;

        private void SetCurrentFileName()
        {
            this.fileName = DateTime.Now.Ticks.ToString() + ".csv";
        }

        public Exporter(string fileExportLocation, IList<TedEnergyWebApi> apisToExport)
        {
            this.fileLocation = fileExportLocation;
            this.webApis = apisToExport;
            SetCurrentFileName();
        }

        public void ExportToCsv()
        {
            var eecApi = webApis.OfType<EccPollingApi>().SingleOrDefault();
            DashData dashData = eecApi.GetDataObjectCache().OfType<DashData>().SingleOrDefault();
            Rate rate = eecApi.GetDataObjectCache().OfType<Rate>().SingleOrDefault();
            SystemOverview sysOverview = eecApi.GetDataObjectCache().OfType<SystemOverview>().SingleOrDefault();

            var tedApi = webApis.OfType<Ted500PollingApi>().SingleOrDefault();
            Stats tedStats = tedApi.GetDataObjectCache().OfType<Stats>().SingleOrDefault();

            using (StreamWriter sw = new StreamWriter(this.fileLocation + "\\" + this.fileName , append: true))
            {
                if (null != dashData)
                    sw.Write(dashData.ToString());

                if (null != rate)
                    sw.Write(rate.ToString());

                if (null != sysOverview)
                    sw.Write(sysOverview.ToString());

                if (null != tedStats)
                    sw.Write(tedStats.ToString());
            }
        }
    }
}
