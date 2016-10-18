using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Logger;
using TedEnergy.Web.API.DataObjects;
using TedEnergy.Web.API.DataObjects.Eec;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API
{
    public class EccPollingApi : TedEnergyWebApi
    {
        private const ServiceType serviceType = ServiceType.EEC_POLLING;

        public EccPollingApi(ILogger logger) : base(logger) { }

        public override ServiceType Type
        {
            get { return serviceType; }
        }

        public override void RefreshDataObjectCache()
        {
            lock (base.lockObj)
            {
                try
                {
                    base.dataObjectCache = new List<DataObject>();
                    base.dataObjectCache.Add(new DashData());
                    base.dataObjectCache.Add(new Rate());
                    base.dataObjectCache.Add(new SystemOverview());
                    base.dataObjectCache.Add(new SystemSettings());
                    base.dataObjectCache.Add(new UtilitySettings());
                }                                
                catch (Exception exc)
                {
                    logger.LogException(exc);
                }
            }
        }
    }
}
