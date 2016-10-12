using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API.DataObjects.Mtu
{
    public class Settings : DataObject
    {
        private const string _objName = @"Settings";
        private const string _rootXmlName = @"SystemSettings";
        private const ServiceType serviceType = ServiceType.MTU3K_POLLING;

        protected override string objectName
        {
            get { return _objName; }
        }

        protected override string rootXmlName
        {
            get { return _rootXmlName; }
        }

        public override ServiceType Type
        {
            get { return serviceType; }
        }

        public Settings()
        {
            base.webClient = new XmlWebClient(objectName);
            base.rawXml = webClient.GetXmlData();

            if (!ParseRawXML())
                Console.WriteLine("Failure attempting to parse data from web services.");
        }

        private bool ParseRawXML()
        {
            /*
            try
            {
                string tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Now", out tempValue))
                    this.Now = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("TDY", out tempValue))
                    this.Tdy = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("MTD", out tempValue))
                    this.Mtd = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Avg", out tempValue))
                    this.Avg = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Proj", out tempValue))
                    this.Proj = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Voltage", out tempValue))
                    this.Voltage = int.Parse(tempValue);

                tempValue = string.Empty;
                if (base.TryGetValueFromRawXml("Phase", out tempValue))
                    this.Phase = tempValue;

                return true;
            }
            catch
            {
                return false;
            }
             *
             */

            return false;
        }
    }
}
