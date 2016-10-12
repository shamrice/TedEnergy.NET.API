using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TedEnergy.Web.API.WebClients;

namespace TedEnergy.Web.API.DataObjects.Eec
{
    public class DashData : DataObject
    {
        private const string _objName = @"DashData";
        private const string _rootXmlName = @"DashData";
        private const ServiceType serviceType = ServiceType.EEC_POLLING;

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
        /// <summary>
        /// The most recent reading from the MTU/household.
        /// Power/Energy Units: Watts
        /// Cost Units: Cents
        /// </summary>
        public int Now { get; private set; }

        /// <summary>
        /// The cumulative readings since midnight.
        /// Power/Energy Units: Watts
        /// Cost Units: Cents
        /// </summary>
        public int Tdy { get; private set; }

        /// <summary>
        /// The cumulative readings since the beginning of the billing cycle.
        /// Power/Energy Units: Watts
        /// Cost Units: Cents
        /// </summary>
        public int Mtd { get; private set; }

        /// <summary>
        /// The average daily cumulative readings.
        /// Power/Energy Units: Watts
        /// Cost Units: Cents
        /// </summary>
        public int Avg { get; private set; }

        /// <summary>
        /// The projected cumulative readings for the current billing cycle.
        /// Power/Energy Units: Watts
        /// Cost Units: Cents
        /// </summary>
        public int Proj { get; private set; }

        /// <summary>
        /// The most recent voltage reading.
        /// Power Energy Units: Volts * 100
        /// Cost Units: Volts * 100
        /// </summary>
        public int Voltage { get; private set; }

        /// <summary>
        /// The type of phase being reported for the MTU. For NET/LOAD/GEN this is the value from MTU1.
        /// Power/Energy Units: MTU6K, PROMODE, 193: 1PH_2W, 195: 1PH_Split
        /// Cost Units: MTU3K, 0-3P_4W_WYE, 1-3P_3W_DELTA, 3-3P_4W_DELTA
        /// </summary>
        public string Phase { get; private set; }


        public DashData() {
            base.webClient = new XmlWebClient(objectName);
            base.rawXml = webClient.GetXmlData();

            if (!ParseRawXML())
                Console.WriteLine("Failure attempting to parse data from web services.");
        }

        /// <summary>
        /// HACK - I don't like how any of this works.
        /// </summary>
        /// <returns></returns>
        private bool ParseRawXML()
        {
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
        }

        
    }
}
