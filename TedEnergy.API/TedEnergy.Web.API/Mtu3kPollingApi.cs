using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Logger;
using TedEnergy.Web.API.DataObjects;
using TedEnergy.Web.API.DataObjects.Mtu3k;

namespace TedEnergy.Web.API
{
    public class Mtu3kPollingApi : TedEnergyWebApi
    {
        private const ServiceType serviceType = ServiceType.MTU3K_POLLING;

        public Mtu3kPollingApi(ILogger logger) : base(logger) { }

        public override ServiceType Type { get { return serviceType; } }

        public override void RefreshDataObjectCache()
        {
            lock (base.lockObj)
            {
                try
                {
                    base.dataObjectCache = new List<DataObject>();
                    base.dataObjectCache.Add(new Settings());
                    base.dataObjectCache.Add(new Stats());
                }
                catch (Exception exc)
                {
                    logger.LogException(exc);
                }
            }
        }
    }
}
