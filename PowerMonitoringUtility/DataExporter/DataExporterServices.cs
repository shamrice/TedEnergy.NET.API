using System;
using System.Collections.Generic;
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
        private readonly IList<TedEnergyWebApi> webApis;
        private readonly ServicesConfiguration config;

        public DataExporterServices(ServicesConfiguration configuration)
        {
            this.config = configuration;
            this.webApis = new List<TedEnergyWebApi>();

            foreach (ServiceType serviceType in this.config.ConfiguredTypesOfServices)            
                this.webApis.Add(TedEnergyWebApiBuilder.Build(serviceType));

            foreach (TedEnergyWebApi webApi in this.webApis)
            {
                if (null != webApi)
                    webApi.RefreshDataObjectCache();
                else
                    throw new NullReferenceException("TedEnergyWebApiBuilder returned a null value attempting to build TedEnergyApi using the " +
                        "configuration suppled.");
            }
        }

        public string DebugTests()
        {
            string result = string.Empty;            
            
            var eecApi = webApis.OfType<EccPollingApi>().SingleOrDefault();
            DashData dashData = eecApi.GetDataObjectCache().OfType<DashData>().SingleOrDefault();
            Rate rate = eecApi.GetDataObjectCache().OfType<Rate>().SingleOrDefault();
            SystemOverview sysOverview = eecApi.GetDataObjectCache().OfType<SystemOverview>().SingleOrDefault();

            var tedApi = webApis.OfType<Ted500PollingApi>().SingleOrDefault();
            Stats tedStats = tedApi.GetDataObjectCache().OfType<Stats>().SingleOrDefault();
                    
            if (null != dashData)            
                result += dashData.ToString();

            if (null != rate)
                result += rate.ToString();

            if (null != sysOverview)
                result += sysOverview.ToString();

            if (null != tedStats)
                result += tedStats.ToString();

            return result;
        }

    }
}
