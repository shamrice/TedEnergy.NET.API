using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Logger;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API
{
    public static class TedEnergyWebApiBuilder
    {
        public static TedEnergyWebApi Build(ServiceType serviceType, ILogger logger)
        {
            switch (serviceType)
            {
                case ServiceType.EEC_POLLING:
                    return new EccPollingApi(logger);
                    
                case ServiceType.MTU3K_POLLING:
                    return new Mtu3kPollingApi(logger);

                case ServiceType.TED500_POLLING:
                    return new Ted500PollingApi(logger);
                    
            }

            return null;
        }
    }
}
