using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API
{
    public static class TedEnergyWebApiBuilder
    {
        public static TedEnergyWebApi Build(ServiceType serviceType)
        {
            switch (serviceType)
            {
                case ServiceType.EEC_POLLING:
                    return new EccPollingApi();
                    
                case ServiceType.MTU3K_POLLING:
                    return new Mtu3kPollingApi();

                case ServiceType.TED500_POLLING:
                    return new Ted500PollingApi();
                    
            }

            return null;
        }
    }
}
