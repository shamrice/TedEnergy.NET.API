using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.DataObjects.Ted500;

namespace TedEnergy.Web.API
{
    public class Ted500PollingApi : TedEnergyWebApi
    {
        private ServiceType serviceType = ServiceType.TED500_POLLING;
        public override ServiceType Type
        {
            get { return serviceType; }
        }

        public override void RefreshDataObjectCache()
        {
            base.dataObjectCache = new List<DataObjects.DataObject>();
            base.dataObjectCache.Add(new Stats());
        }
    }
}
