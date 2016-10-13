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
        protected readonly IList<ServiceType> configuredServiceTypes;

        public enum TypesOfServices
        {
            EEC,
            MTU,
            TED
        };

        public IList<ServiceType> ConfiguredTypesOfServices { get { return this.configuredServiceTypes; } }

        public ServicesConfiguration(IList<TypesOfServices> typesOfServices)
        {
            this.configuredServiceTypes = new List<ServiceType>();

            if (typesOfServices.Contains(TypesOfServices.EEC))
                configuredServiceTypes.Add(ServiceType.EEC_POLLING);

            if (typesOfServices.Contains(TypesOfServices.MTU))
                configuredServiceTypes.Add(ServiceType.MTU3K_POLLING);

            if (typesOfServices.Contains(TypesOfServices.TED))
                configuredServiceTypes.Add(ServiceType.TED500_POLLING);
            
        }

    }
}
