using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API.DataObjects.Eec
{
    public class UtilitySettings : DataObject
    {
        private const string objName = @"UtilitySettings";
        private const string _rootXmlName = @"UtilitySettings";
        private const ServiceType serviceType = ServiceType.EEC_POLLING;

        protected override string objectName
        {
            get { return objName; }
        }

        protected override string rootXmlName
        {
            get { return _rootXmlName; }
        }

        public override ServiceType Type
        {
            get { return serviceType; }
        }

        protected override bool ParseRawXML()
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
             */
            return false;
        }
    }
}
