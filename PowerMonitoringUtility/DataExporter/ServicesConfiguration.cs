using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API;
using TedEnergy.Web.API.WebClients;

namespace DataExporter
{
    public abstract class ServicesConfiguration
    {
        protected readonly ServiceType serviceType;
        public enum TypesOfServices
        {
            EEC,
            MTU,
            TED
        };

        public ServiceType Type { get { return this.serviceType; } }

        public ServicesConfiguration(TypesOfServices typeOfService)
        {
            switch (typeOfService)
            {
                case TypesOfServices.MTU:
                    this.serviceType = ServiceType.MTU3K_POLLING;
                    break;
                case TypesOfServices.EEC:
                    this.serviceType = ServiceType.EEC_POLLING;
                    break;
                case TypesOfServices.TED:
                    this.serviceType = ServiceType.TED500_POLLING;
                    break;
                default:
                    throw new ArgumentException("Web API service type is invalid", "typeOfService");
            }

        }

    }
}
