using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedEnergy.Web.API.DataObjects.Ted500
{
    public class Stats : DataObject
    {
        private string objName = "stats";
        private string xmlRoot = "TED500STATS";
        private ServiceType serviceType = ServiceType.TED500_POLLING;

        protected override string objectName
        {
            get { return objName; }
        }

        protected override string rootXmlName
        {
            get { return xmlRoot; }
        }

        protected override bool ParseRawXML()
        {
            return false;
        }

        public override ServiceType Type
        {
            get { return serviceType; }
        }
    }
}
