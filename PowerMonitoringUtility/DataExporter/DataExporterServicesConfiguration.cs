using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API;
using TedEnergy.Web.API.WebClients;

namespace DataExporter
{
    public class DataExporterServicesConfiguration : ServicesConfiguration
    {
        public DataExporterServicesConfiguration(IList<TypesOfServices> typesOfServices) : base(typesOfServices) { }
    }
}
